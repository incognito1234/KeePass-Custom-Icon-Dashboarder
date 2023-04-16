﻿/*
  CustomIconDashboarder - KeePass Plugin to get some information and 
  manage custom icons
  
  Copyright (C) 2023 Jareth Lomson <jareth.lomson@gmail.com>
 
 This program is free software; you can redistribute it and/or modify
  it under the terms of the GNU General Public License as published by
  the Free Software Foundation; either version 2 of the License, or
  (at your option) any later version.

  This program is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  GNU General Public License for more details.

  You should have received a copy of the GNU General Public License
  along with this program; if not, write to the Free Software
  Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
 */

using System;
using System.Drawing;

namespace CustomIconDashboarderPlugin
{
	
	
	/// <summary>
	/// Description of IconChooser.
	/// </summary>
	public class IconChooser
	{
		
		public BestIconFinder Bif {get; private set; }
		
		public Image ChoosenIcon {
			get { 
				if ((Bif.ListImageInfo != null) &&
				    ( Bif.ListImageInfo.Count > ChoosenIndex) &&
				    ( ChoosenIndex >= 0) ) {
					return Bif.ListImageInfo[ChoosenIndex].ImgData;
				}
				else {
					return null;
				}
			}
				
		}
		
		public Int32 ChoosenIndex {get; set;}
		
		public IconChooser(BestIconFinder bif)
		{
			this.Bif = bif;
			this.ChoosenIndex = bif.IndexOfBestImage;
		}
		
	}
}
