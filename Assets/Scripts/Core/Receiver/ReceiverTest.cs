using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ProjectBlue;
using ProjectBlue.ArtNetRecorder;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using BitConverter = ProjectBlue.unsafety.BitConverter;
using Debug = UnityEngine.Debug;

public class ReceiverTest : MonoBehaviour
{

    private ArtNetReceiver _artNetReceiver;

    private ArtNetRecorderNew _artNetRecorder;
    
    private void Start()
    {
        
        _artNetReceiver = new ArtNetReceiver(6454);
        _artNetRecorder = new ArtNetRecorderNew();
        
        _artNetReceiver.OnReceiveArtNetPacket = (universe, dmx) =>
        {
            _artNetRecorder.ReceiveArtNetPacket(universe, dmx);
        };
        
        _artNetReceiver.BeginReceive();
        
        _artNetRecorder.BeginRecord();
        
        _artNetRecorder.OnSaveCompleted = result =>
        {
            Debug.Log($"saved : {result.DataPath}");
        };

    }

    private void OnDestroy()
    {
        _artNetRecorder.Dispose();
        _artNetReceiver.Dispose();
    }
    
}


public class ArtNetRecorderNew : IDisposable
{

    private ArtNetPacket[] _artNetPacket;
    private ConcurrentQueue<DmxRecordingPacketNew> _dmxRecordingQueue = new();

    private bool _isRecording;
    private const double _fixedDeltaTime = 1.0d / 60.0d;    // 60fps

    private Stopwatch _fixedFramerateStopwatch = new();
    private double _deltaTime;
    private Stopwatch _recordingStopWatch = new();
    private uint _recordingSequenceNumber;

    public Action<SaveResult> OnSaveCompleted;

    public void BeginRecord()
    {
        if (_isRecording) return;
        
        EndRecord();
        
        var path = CreateRecordingFile();
        
        _isRecording = true;
        _recordingStopWatch.Start();
        
        BeginRecordTask(path);
    }
    
    public void EndRecord()
    {
        _isRecording = false;
        
        _fixedFramerateStopwatch.Stop();
        _fixedFramerateStopwatch.Reset();
        
        _deltaTime = 0.0d;
        
        _recordingSequenceNumber = 0;
        _recordingStopWatch.Stop();
        _recordingStopWatch.Reset();
    }

    public void Dispose()
    {
        EndRecord();
    }

    // 実際に一時バッファから値を取り出してファイルに書き込むプロセス
    private async void BeginRecordTask(string filePath)
    {
        var total = 0L;
        var bytesLen = 0L;
        
        await Task.Run(() =>
        {
            using var file = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
            
            // recording loop
            while (_isRecording)
            {

                _fixedFramerateStopwatch.Stop();
                
                // 1フレーム分溜まったデータを処理する
                if (_deltaTime <= _fixedFramerateStopwatch.Elapsed.TotalSeconds)
                {
                    
                    var sequence = ByteConvertUtility.GetBytes(_recordingSequenceNumber);
                    var milliseconds = ByteConvertUtility.GetBytes(_recordingStopWatch.ElapsedMilliseconds);
                    
                    var dataSegment = new Dictionary<uint, byte[]>();
                    
                    // run through all queue and create data array
                    while (_dmxRecordingQueue.Count > 0)
                    {
                        if (!_dmxRecordingQueue.TryDequeue(out var dmxRecordingPacket)) continue;
                    
                        var myUniverse = ByteConvertUtility.GetBytes(dmxRecordingPacket.Universe);
                        
                        // すでに格納されたUniverseと同じUniverseのデータがあった場合は最新のもので上書きする
                        dataSegment[dmxRecordingPacket.Universe] = ByteConvertUtility.Join(myUniverse, dmxRecordingPacket.Data);
                    }
                    
                    // TODO: 多分BinaryWriter継承したクラス作ってRecデータ構造定義してあげたほうがよさそう
                    var header = ByteConvertUtility.Join(sequence, milliseconds, ByteConvertUtility.GetBytes(dataSegment.Count));
                    var data = Array.Empty<byte>();
                    foreach (var (u, d) in dataSegment)
                    {
                        data = ByteConvertUtility.Join(data, d);
                    }

                    var bytes = ByteConvertUtility.Join(header, data);
                    
                    file.Write(bytes, 0, bytes.Length);
                    bytesLen += bytes.Length;

                    total++;

                    _recordingSequenceNumber++;
                }
                else
                {
                    _fixedFramerateStopwatch.Start();
                }
            }
        });
        
        
        if (total > 0)
        {
            OnSaveCompleted?.Invoke(new SaveResult{DataPath = filePath, PacketNum = total, Size = bytesLen});
        }
        else
        {
            File.Delete(filePath);
            Debug.Log("Zero!!");
        }
        
    }
    
    private static string CreateRecordingFile()
    {
        var fileName = "Dmx_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".dmx";
        Directory.CreateDirectory(Application.streamingAssetsPath);
        return  Path.Combine(Application.streamingAssetsPath, fileName);
    }
    
    // DMXのレコードのために、一時的にバッファに格納するプロセス
    public void ReceiveArtNetPacket(uint universe, byte[] dmxDataPacket)
    {
        if (!_isRecording) return;
        
        _dmxRecordingQueue.Enqueue(new DmxRecordingPacketNew
        {
            Universe = universe,
            Data = dmxDataPacket
        });
    }
}

public class DmxRecordingPacketNew
{
    public uint Universe;
    
    private byte[] _data;
    public unsafe byte[] Data
    {
        get => _data;
        set {
            // Capture to memory
            _data = new byte[512];
            fixed (byte* src = value, dst = _data)
            {
                UnsafeUtility.MemCpy(dst, src, _data.Length);
            }
        }
    }
}