/*
 CustomIconDashboarder - KeePass Plugin to get some information and 
  manage custom icons
  
  Copyright (C) 2015 Jareth Lomson <incognito1234@users.sourceforge.net>
 
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
	/// Description of ImageInfo.
	/// </summary>
	public class ImageInfo
	{
		public Image ImgData {get; private set; }
		public String Url {get; private set; }
		
		public ImageInfo(Image imgData, String url)
		{
			this.ImgData = imgData;
			this.Url = url;
		}
	}
}
