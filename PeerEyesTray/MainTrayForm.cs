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

namespace PeerEyesTray
{
    public partial class MainTrayForm : Form
    {
        private Broadcaster broadcast;
        private Tracker tracks;

        public MainTrayForm()
        {
            InitializeComponent();
            broadcast = new Broadcaster();
            tracks = new Tracker();
        }

        private void RefreshPeers()
        {
            tmiPeers.DropDownItems.Clear();
            foreach (string host in tracks.peers.Keys)
            {
                ToolStripMenuItem tmi = new ToolStripMenuItem();
                tmi.Text = host;
                tmiPeers.DropDownItems.Add(tmi);
            }
        }

        private void nicTray_MouseClick(object sender, MouseEventArgs e)
        {
            RefreshPeers();
        }
    }
}
