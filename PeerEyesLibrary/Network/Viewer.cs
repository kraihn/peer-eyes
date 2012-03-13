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
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using PeerEyesLibrary.Crypt;

namespace PeerEyesLibrary.Network
{
    public class Viewer
    {
        private IPAddress ipAddress;
        private int screenPort;
        private Thread view;
        public Queue<Image> screenshots;

        public Viewer(IPAddress hostAddress, int port)
        {
            this.ipAddress = hostAddress;
            this.screenPort = port;
            screenshots = new Queue<Image>();
            StartViewing();
        }

        public void StartViewing()
        {
            if (view == null || !view.IsAlive)
            {
                view = new Thread(new ThreadStart(RunViewer));
                view.Start();
            }
        }

        public void StopViewing()
        {
            view.Abort();
        }

        public void RestartViewing()
        {
            StopViewing();
            StartViewing();
        }

        public void RunViewer()
        {
            while (true)
            {
                try
                {
                    UdpClient recv = new UdpClient(this.screenPort);
                    IPEndPoint remote = new IPEndPoint(IPAddress.Any, this.screenPort);

                    Console.WriteLine("Receiving img...");
                    byte[] imgData = recv.Receive(ref remote);
                    imgData = BasicAes.DecryptBytes(imgData);
                    Console.WriteLine("Received img");

                    recv.Close();

                    MemoryStream ms = new MemoryStream(imgData);
                    Image img = Image.FromStream(ms);
                    //DateTime t = DateTime.Now;
                    //img.Save("C:\\pe-viewer " + t.Hour + " " + t.Minute + " " + t.Second + ".jpg", ImageFormat.Jpeg);
                    //form.BackgroundImage = img;
                    //form.Update();
                    screenshots.Enqueue(img);

                }
                catch (SocketException e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}
