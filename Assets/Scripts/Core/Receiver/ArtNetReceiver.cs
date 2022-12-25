using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using ProjectBlue;
using UnityEngine;

public class UdpReceiver : IDisposable
{

    private static volatile bool _loopFlag;
    
    protected int _port;

    protected SynchronizationContext _synchronizationContext;
    protected CancellationTokenSource _cts;

    // Called from sub thread
    public Action<byte[]> OnReceiveUdpPacket;
    public Action<string> OnEndSocket;
    
    public UdpReceiver(int port)
    {
        _port = port;
        _loopFlag = false;
    }

    public async void BeginReceive()
    {

        _loopFlag = true;
        _synchronizationContext = new SynchronizationContext();
        _cts = new CancellationTokenSource();

        var ip = new IPEndPoint(IPAddress.Any, _port);

        var task = Task.Run(() =>
        {

            try
            {

                using var udpClient = new UdpClient(ip);
                Debug.Log("Udp Client Established");

                while (_loopFlag)
                {

                    var result = udpClient.ReceiveAsync().WithCancellation(_cts.Token);

                    if (result.Result.Buffer.Length > 0)
                    {
                        OnReceivePacket(result.Result.Buffer);
                    }

                }


            }
            catch (Exception e)
            {
                _loopFlag = false;

                switch (e)
                {
                    case AggregateException _:
                    {
                        if (e.InnerException is TaskCanceledException)
                        {
                            Debug.Log("Udp Receive Task canceled");
                        }

                        break;
                    }
                    case TaskCanceledException _:
                        Debug.Log("Udp Receive Task canceled");
                        break;
                    case SocketException _:
                        throw;
                    default:
                        Debug.LogException(e);
                        break;
                }
                
                Debug.Log("Udp Server finished");
            }

        }, _cts.Token);

        // awaitできないので仕方なくContinueWithでエラーハンドリングする
        task.ContinueWith(continuationAction =>
        {
            if (continuationAction.Exception is AggregateException agg)
            {
                if (agg.InnerException is SocketException)
                {
                    _synchronizationContext.Post(__ =>
                    {
                        OnEndSocket?.Invoke($"ポート{_port}が他のアプリケーションによって専有されています");
                    }, null);
                }
            }
        }, _cts.Token);
        
    }

    protected virtual void OnReceivePacket(byte[] packet)
    {
        OnReceiveUdpPacket?.Invoke(packet);
    }

    public virtual void EndReceive()
    {
        _loopFlag = false;
        _cts.Cancel();
    }

    public virtual void Dispose()
    {
        EndReceive();
    }
}

public class ArtNetReceiver : UdpReceiver
{

    public Action<uint, byte[]> OnReceiveArtNetPacket;
    
    private static byte[][] _dmxBuffer;

    private int _maxUniverseCapacity;
    public int MaxUniverseCapacity => _maxUniverseCapacity;
    
    public ArtNetReceiver(int port, int maxUniverseCapacity = 32) : base(port)
    {
        _dmxBuffer = new byte[maxUniverseCapacity][];
        _maxUniverseCapacity = maxUniverseCapacity;
    }

    protected override void OnReceivePacket(byte[] packet)
    {
        if (ArtNetPacketUtillity.GetOpCode(packet) != ArtNetOpCodes.Dmx) return;
        
        var universe = ArtNetPacketUtillity.GetUniverse(packet);
        _dmxBuffer[universe] ??= new byte[512]; // 新しいUniverseが飛んできた場合はバッファに新規Universeの配列を生成する
        ArtNetPacketUtillity.GetDmx(packet, ref _dmxBuffer[universe]);
        
        OnReceiveArtNetPacket?.Invoke((uint)universe, _dmxBuffer[universe]);
    }

    public override void Dispose()
    {
        base.Dispose();
    }
    
}
