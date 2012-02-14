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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeerEyesLibrary.Network
{
    public class Peer
    {
        public string HostName;
        public DateTime LastSeen;

        public Peer(string host)
        {
            this.HostName = host;
            this.LastSeen = DateTime.Now;
        }

        public Peer(string host, DateTime time)
        {
            this.HostName = host;
            this.LastSeen = time;
        }

        public void Spotted()
        {
            this.LastSeen = DateTime.Now;
        }

        public override string ToString()
        {
            return this.HostName + ": " + LastSeen.ToString();
        }
    }
}