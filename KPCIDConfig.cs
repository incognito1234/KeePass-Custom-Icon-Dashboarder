/*
  CustomIconDashboarder - KeePass Plugin to get some information and 
  manage custom icons
  
  Copyright (C) 2016 Jareth Lomson <incognito1234@users.sourceforge.net>
 
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
using KeePass.App.Configuration;
using KeePass.Plugins;

namespace CustomIconDashboarderPlugin
{
	/// <summary>
	/// Description of KPCIDConfig.
	/// </summary>
	public class KPCIDConfig
	{
		
		private const String XMLPATH_PLUGINNAME = "KPCID";
		
		private const String XMLPATH_DASHBOARD_STATE =
			XMLPATH_PLUGINNAME + ".dashboardState";
		private const String VALUE_STATE_MAXIMIZED = "Maximized";
		private const String VALUE_STATE_NORMAL = "Normal";
		
		private const String XMLPATH_DASHBOARD_SIZE =
			XMLPATH_PLUGINNAME + ".dashboardSize";
		
		private const String XMLPATH_DASHBOARD_POSITION =
			XMLPATH_PLUGINNAME + ".dashboardPosition";
		
		private AceCustomConfig m_config = null;
	
		public KPCIDConfig(IPluginHost host)
		{
			m_config = host.CustomConfig;
		}
		
		public System.Windows.Forms.FormWindowState DashboardState {
			get {
				string strState = m_config.GetString(XMLPATH_DASHBOARD_STATE,"");
				return strState.ToUpper() == VALUE_STATE_MAXIMIZED.ToUpper() 
					? System.Windows.Forms.FormWindowState.Maximized
					: System.Windows.Forms.FormWindowState.Normal;
			}
			set {
				m_config.SetString(XMLPATH_DASHBOARD_STATE,
					value == System.Windows.Forms.FormWindowState.Maximized
						? VALUE_STATE_MAXIMIZED
						: VALUE_STATE_NORMAL
					);
			}
		}
		
		public Size DashboardSize {
			get {
				string strSize = m_config.GetString(XMLPATH_DASHBOARD_SIZE,"");
				Size readSize = Size.ParseSize(strSize);
				return readSize;
			}
			set {
				if (!value.IsNull) 
					m_config.SetString(XMLPATH_DASHBOARD_SIZE, value.ToString());
			}
		}
		
		public Size DashboardPosition {
			get {
				string strPosition = m_config.GetString(XMLPATH_DASHBOARD_POSITION,"");
				Size readPosition = Size.ParseSize(strPosition);
				return readPosition;
			}
			set {
				if (!value.IsNull) 
					m_config.SetString(XMLPATH_DASHBOARD_POSITION, value.ToString());
			}
		}
				
		
		public class Size
		{
			public int X {get; private set;}
			public int Y {get; private set;}
			public bool IsNull {get; private set;}
			
			public Size(int x, int y)
			{
				this.X = x;
				this.Y = y;
				this.IsNull = false;
			}
			
			// Null Size
			public Size()
			{
				this.X = 0; this.Y = 0;
				this.IsNull = true;
			}
			
			public String ToString()
			{
				if (!this.IsNull)
					return this.X.ToString("D") + "x" + this.Y.ToString("D");
				else
					return "NULL";
			}
			
			public static Size ParseSize(string str)
			{
				string[] sizeparts = str.Split('x');
				if (sizeparts.Length != 2)
					return new Size();
				
				string strSizeX = sizeparts[0].Trim();
				string strSizeY = sizeparts[1].Trim();
				if ( (string.IsNullOrEmpty(strSizeX))
				    || (string.IsNullOrEmpty(strSizeY) ) ) {
					return new Size();
				}
				
				bool parseOKX, parseOKY;
				int intSizeX; parseOKX = int.TryParse( strSizeX, out intSizeX);
				int intSizeY; parseOKY = int.TryParse( strSizeY, out intSizeY);
				
				if (!parseOKX || !parseOKY) {
					return new Size();
				}
				
				return new Size(intSizeX, intSizeY);
			}
			
		}
		
	}
}
