using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace WpfMobileMusicNotificationServer
{
    public class Receiver
    {
        public async Task ReceiveInfoAsync()
        {
            await Task.Run(() => ReceiveInfo());
        }

        public void ReceiveInfo()
        {
            int port = Int32.Parse(ConfigurationManager.AppSettings["port"]);
            if (port == 0)
                port = 9050;

            IPEndPoint ServerEndPoint = new IPEndPoint(IPAddress.Any, port);
            Socket WinSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            WinSocket.Bind(ServerEndPoint);
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint Remote = (EndPoint)(sender);

            while (true)
            {
                try
                {
                    byte[] data = new byte[128];

                    int recv = WinSocket.ReceiveFrom(data, ref Remote);
                    string message = Encoding.ASCII.GetString(data, 0, recv);

                    MusicInfo info = ParseDataToInfo(message);

                    ReceivedInfo?.Invoke(this, new ReceiveEventArgs(info));
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
        }

        public event EventHandler<ReceiveEventArgs> ReceivedInfo;



        private MusicInfo ParseDataToInfo(string message)
        {
            string[] data = message.Split(';');
            MusicInfo result = new MusicInfo();
            result.TrackNumber = Int32.Parse(data[0]);
            result.Artist = data[1];
            result.Album = data[2];
            result.Song = data[3];
            return result;
        }
    }

    public class ReceiveEventArgs : EventArgs
    {
        public ReceiveEventArgs(MusicInfo info)
        {
            this.Info = info;
        }
        public MusicInfo Info { get; set; }
    }

}