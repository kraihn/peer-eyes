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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using PeerEyesLibrary.Crypt;

namespace PeerEyesLibrary.Network
{
    public class Broadcaster
    {
        private Thread signal;
        private int screenPort;

        public Broadcaster(int port)
        {
            screenPort = port;
            StartBroadcasting();
        }

        public void StartBroadcasting()
        {
            if (signal == null || !signal.IsAlive)
            {
                signal = new Thread(new ThreadStart(RunBroadcaster));
                signal.Start();
            }
        }

        public void StopBroadcasting()
        {
            signal.Abort();
        }

        public void RestartBroadcasting()
        {
            StopBroadcasting();
            StartBroadcasting();
        }

        public void GoOffline()
        {

        }

        private void RunBroadcaster()
        {
            UdpClient udp = new UdpClient();
            IPEndPoint iep = new IPEndPoint(Info.sendAddress, Info.broadcastPort);

            string hostname = Dns.GetHostName();
            byte[] data = Encoding.ASCII.GetBytes(hostname + " SCREEN:" + screenPort);
            data = BasicAes.EncryptBytes(data);
            while (true)
            {
                try
                {
                    udp.Send(data, data.Length, iep);
                    Thread.Sleep(3000);
                }
                catch (SocketException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
