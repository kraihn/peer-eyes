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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PeerEyesLibrary.Network;
using System.Threading;

namespace PeerEyesTray
{
    public partial class MainTrayForm : Form
    {
        private Broadcaster broadcast;
        private Tracker tracks;
        private Screencaster screen;
        private int screenPort;

        public MainTrayForm()
        {
            InitializeComponent();

            // Start listening for peers
            tracks = new Tracker();

            // Wait for peer list to be generated
            Thread.Sleep(4000);

            // Pick a random port to screencast and check against
            // available local ports and other peers screencast ports
            // as to not duplicate and cause collisions. In the meantime
            // random offset will suffice.
            Random rand = new Random((int)DateTime.Now.ToFileTimeUtc());
            screenPort = Info.screencastPort + (rand.Next() % 1000);

            broadcast = new Broadcaster(screenPort);
            screen = new Screencaster(screenPort);
        }

        private void RefreshPeers()
        {
            tmiPeers.DropDownItems.Clear();
            foreach (string host in tracks.peers.Keys)
            {
                if (!tracks.peers[host].IsExpired())
                {
                    ToolStripMenuItem tmi = new ToolStripMenuItem();
                    tmi.Text = host;
                    tmi.Click += new EventHandler(tmi_Click);
                    tmiPeers.DropDownItems.Add(tmi);
                }
            }
        }

        void tmi_Click(object sender, EventArgs e)
        {
            string host = ((ToolStripMenuItem)sender).Text;
            ViewerForm frm = new ViewerForm(host, tracks.peers[host].IpAddress, tracks.peers[host].ScreenPort);
            frm.Show();
        }

        private void nicTray_MouseClick(object sender, MouseEventArgs e)
        {
            RefreshPeers();
        }

        private void tmiExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
