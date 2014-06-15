/*
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
using System.Collections.Generic;

using System.Windows.Forms;
using KeePass.Plugins;


namespace CustomIconDashboarderPlugin
{
	/// <summary>
	/// Description of CustomIconCounterPlugin.
	/// </summary>
	public sealed class CustomIconDashboarderPluginExt : Plugin
	{
		private IPluginHost m_host = null;
		
		public override string UpdateUrl
        {
            get { return "https://sourceforge.net/p/keepasscustomicondashboarder/KPCustomIconDashboarder/ci/master/tree/versionInfo.txt?format=raw"; }
        }

		
		private ToolStripSeparator m_tsSeparator = null;
		private ToolStripMenuItem m_tsmiMenuItem = null;
		
		public override bool Initialize(IPluginHost host)
		{
			m_host = host;
			
			// Get a reference to the 'Tools' menu item container
			ToolStripItemCollection tsMenu = m_host.MainWindow.ToolsMenu.DropDownItems;
		
			// Add a separator at the bottom
			m_tsSeparator = new ToolStripSeparator();
			tsMenu.Add(m_tsSeparator);
		
			// Add menu item 'Count Uses of Favicon'
			m_tsmiMenuItem = new ToolStripMenuItem();
			m_tsmiMenuItem.Text = Resource.menu_customIcon;
			m_tsmiMenuItem.Click += this.OnMenuFavicon;
			tsMenu.Add(m_tsmiMenuItem);
			
			
			return true;
		}
	
	
		/// <summary>
		/// Terminate plugin
		/// </summary>
		public override void Terminate()
		{
				// Remove all of our menu items
			ToolStripItemCollection tsMenu = m_host.MainWindow.ToolsMenu.DropDownItems;
			m_tsmiMenuItem.Click -= this.OnMenuFavicon;
			tsMenu.Remove(m_tsmiMenuItem);
			tsMenu.Remove(m_tsSeparator);
		}
		
		
		private void OnMenuFavicon(object sender, EventArgs e)
		{
			DashboarderForm cilf = new DashboarderForm(m_host);
			
			cilf.ShowDialog();
		}
	
		
		
	}

}
