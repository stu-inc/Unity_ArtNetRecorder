using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using com.kodai100.ArtNet;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace com.kodai100.ArtNetPlayer
{
    
    public class ArtNetPlayer
    {
        
        private DmxRecordData dmxRecordData;

        private byte[][] dmx;
        private float[] dmxRaw;

        UdpClient udpClient = new();

        private IPAddress _destinationIp;
        private int _destinationPort;
        
        private Subject<bool> _onLoadingStateChanged = new();
        public IObservable<bool> OnLoadingStateChanged => _onLoadingStateChanged;

        public ArtNetPlayer(int maxUniverseNum)
        {
            dmx = new byte[maxUniverseNum][];
            for (var i = 0; i < maxUniverseNum; i++)
            {
                dmx[i] = new byte[512];
            }

            dmxRaw = new float[maxUniverseNum * 512];
        }
        
        public void SetIp(IPAddress ip)
        {
            _destinationIp = ip;
        }

        public void SetPort(int port)
        {
            _destinationPort = port;
        }

        public async UniTask<DmxRecordData> Load(string path)
        {
            _onLoadingStateChanged.OnNext(true);
            dmxRecordData = await ReadFile(path);
            _onLoadingStateChanged.OnNext(false);
            return dmxRecordData;
        }

        private static async UniTask<DmxRecordData> ReadFile(string path)
        {
            var extension = Path.GetExtension(path);
            if (extension == ".dmx")
            {
                return await UniTask.Run(() => DmxRecordData.ReadFromFilePath(path));
            }
            if (extension == ".parquet")
            {
                return await UniTask.Run(async () => await DmxRecordData.ReadFromParquetFile(path));
            }

            Debug.LogError($"Unknown file extension: {extension}");
            return null;
        }

        public double GetDuration()
        {
            return dmxRecordData.Data.Last().time;
        }

        public float[] ReadAndSend(double header, bool isSend)
        {
            foreach (var packet in dmxRecordData.Data)
            {

                if (packet.time >= header)
                {

                    foreach (var universeData in packet.data)
                    {

                        Buffer.BlockCopy(universeData.data, 0, dmx[universeData.universe], 0, universeData.data.Length);

                        if (isSend)
                        {
                            var artNetPacket = new ArtNetDmxPacket
                            {
                                Universe = (short)universeData.universe,
                                DmxData = dmx[universeData.universe]
                            };
                            
                            var artNetPacketBytes = artNetPacket.ToArray();

                            udpClient.Send(artNetPacketBytes, artNetPacketBytes.Length, _destinationIp.ToString(), _destinationPort);
                        }

                        // universe
                        for (var universe = 0; universe < dmx.Length; universe++)
                        {
                            // channel
                            for (var channel = 0; channel < dmx[universe].Length; channel++)
                            {
                                dmxRaw[universe * dmx[universe].Length + channel] = dmx[universe][channel];
                            }

                        }

                    }

                    return dmxRaw;
                }
            }

            return dmxRaw;
        }
    }

}
