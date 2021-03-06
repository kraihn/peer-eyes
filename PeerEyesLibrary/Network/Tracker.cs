﻿/**
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
using PeerEyesLibrary.Crypt;

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
            peers.Clear();
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
                try
                {
                    Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                    IPEndPoint iep = new IPEndPoint(Info.recvAddress, Info.broadcastPort);
                    sock.Bind(iep);
                    EndPoint ep = (EndPoint)iep;
                    Console.WriteLine("Ready to receive...");

                    byte[] data = new byte[1024];
                    int recv = sock.ReceiveFrom(data, ref ep);
                    data = BasicAes.DecryptBytes(data);
                    string stringData = Encoding.ASCII.GetString(data, 0, recv);
                    Console.WriteLine("received: {0}  from: {1}", stringData, ep.ToString());

                    sock.Close();

                    string host = stringData.Split(' ')[0];
                    string ip = ep.ToString().Split(':')[0];
                    int port = Int32.Parse(stringData.Split(' ')[1].Split(':')[1]);

                    if (peers.Keys.Contains(host))
                    {
                        peers[host].Spotted();
                    }
                    else
                    {
                        peers.TryAdd(host, new Peer(host, ip, port));
                    }
                }
                catch (SocketException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
