using System;
using System.Net;
using System.Net.Sockets;
using com.kodai100.ArtNet;

namespace Assets.Scripts.View
{
    public class ArtNetSender : IDisposable
    {
        private readonly UdpClient _udpClient;
        
        public ArtNetSender()
        {
            _udpClient = new UdpClient();
        }

        public void SendUniverse(IPAddress destination, int port, ushort universe, byte[] universeDmxData)
        {
            // TODO: cache
            var artNetPacket = new ArtNetDmxPacket
            {
                Universe = (short)universe,
                DmxData = universeDmxData
            };
            
            var artNetPacketBytes = artNetPacket.ToArray();
            _udpClient.Send(artNetPacketBytes, artNetPacketBytes.Length, destination.ToString(), port);
        }

        public void Dispose()
        {
            _udpClient.Dispose();
        }
    }
}