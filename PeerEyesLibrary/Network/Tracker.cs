/**
 * This file is part of Peer Eyes.
 * 
 * Peer Eyes is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * Peer Eyes is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with Peer Eyes.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Concurrent;
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
        public ConcurrentDictionary<string, Peer> peers { get; private set; }

        public Tracker()
        {
            peers = new ConcurrentDictionary<string, Peer>();
            StartTracking();
        }

        public void StartTracking()
        {
            if (watch == null || !watch.IsAlive)
            {
                watch = new Thread(new ThreadStart(RunTracker));
                watch.Start();
            }
        }

        public void StopTracking()
        {
            watch.Abort();
        }

        public void RestartTracking()
        {
            StopTracking();
            StartTracking();
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

                string host = stringData.Split(' ')[0];
                if (peers.Keys.Contains(host))
                {
                    peers[host].Spotted();
                }
                else
                {
                    peers.TryAdd(host, new Peer(host));
                }

                sock.Close();
            }
        }
    }
}
