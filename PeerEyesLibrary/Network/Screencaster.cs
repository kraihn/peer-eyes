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
using PeerEyesLibrary.Images;

namespace PeerEyesLibrary.Network
{
    public class Screencaster
    {
        private Thread screen;

        public Screencaster()
        {
            StartScreencasting();
        }

        public void StartScreencasting()
        {
            if (screen == null || !screen.IsAlive)
            {
                screen = new Thread(new ThreadStart(RunScreencaster));
                screen.Start();
            }
        }

        public void StopScreencasting()
        {
            screen.Abort();
        }

        public void RestartScreencasting()
        {
            StopScreencasting();
            StartScreencasting();
        }

        public void RunScreencaster()
        {
            while (true)
            {
                try
                {
                    UdpClient send = new UdpClient();
                    send.Connect(Info.sendAddress, Info.screencastPort);

                    byte[] imgData = Screenshot.GetScreenshot();


                    Console.WriteLine("Sending img...");
                    send.Send(imgData, imgData.Length);
                    Console.WriteLine("Sent img");

                    send.Close();

                    Thread.Sleep(1000);
                }
                catch (SocketException e)
                {
                    Console.WriteLine(e);
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
