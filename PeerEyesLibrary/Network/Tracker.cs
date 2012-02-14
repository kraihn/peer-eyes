using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace PeerEyesLibrary.Network
{
    public class Tracker
    {
        private Thread watch;

        public Tracker()
        {
            watch = new Thread(new ThreadStart(RunTracker));
            StartTracking();
        }

        public void StartTracking()
        {
            watch.Start();
        }

        public void StopTracking()
        {
            watch.Abort();
        }

        private void RunTracker()
        {
            while (true)
            {
                Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                IPEndPoint iep = new IPEndPoint(Info.recvAddress, Info.port);
                sock.Bind(iep);
                EndPoint ep = (EndPoint)iep;
                Console.WriteLine("Ready to receive...");

                byte[] data = new byte[1024];
                int recv = sock.ReceiveFrom(data, ref ep);
                string stringData = Encoding.ASCII.GetString(data, 0, recv);
                Console.WriteLine("received: {0}  from: {1}", stringData, ep.ToString());

                sock.Close();
            }
        }
    }
}
