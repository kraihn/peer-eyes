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
using System.Net;

namespace PeerEyesTray
{
    public partial class ViewerForm : Form
    {
        private Viewer view;

        public ViewerForm(string hostname, IPAddress ip, int port)
        {
            InitializeComponent();
            this.Text = hostname;

            view = new Viewer(ip, port);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (view.screenshots.Count > 0)
            {
                this.pictureBox.Image = view.screenshots.Dequeue();
                this.Update();
            }
        }

        private void ViewerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            view.StopViewing();
        }
    }
}
