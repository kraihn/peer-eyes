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

namespace PeerEyesLibrary.Network
{
    public class Broadcaster
    {
        private Thread signal;

        public Broadcaster()
        {
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
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
            IPEndPoint iep = new IPEndPoint(Info.sendAddress, Info.port);

            string hostname = Dns.GetHostName();
            byte[] data = Encoding.ASCII.GetBytes(hostname);
            while (true)
            {
                sock.SendTo(data, iep);
                Thread.Sleep(3000);
            }
        }
    }
}
