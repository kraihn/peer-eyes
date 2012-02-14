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
        private static readonly IPAddress address = IPAddress.Parse("224.129.100.3");
        private const int port = 11000;

        public Broadcaster()
        {
            signal = new Thread(new ThreadStart(RunBroadcaster));
            StartBroadcasting();
        }

        public void StartBroadcasting()
        {
            signal.Start();
        }

        public void StopBroadcasting()
        {
            signal.Abort();
        }

        public void GoOffline()
        {

        }

        private void RunBroadcaster()
        {
            bool done = false;

            UdpClient sender = new UdpClient();
            IPEndPoint groupEP = new IPEndPoint(address, port);
            Random rand = new Random();
            string message = Environment.MachineName + " is alive!";

            try
            {
                sender.JoinMulticastGroup(address);
                sender.Connect(groupEP);

                while (!done)
                {
                    Console.WriteLine("Sending datagram : {0}", message);
                    byte[] bytes = Encoding.ASCII.GetBytes(message);

                    sender.Send(bytes, bytes.Length);

                    Thread.Sleep(1000 + (rand.Next() % 1000));
                }

                sender.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
