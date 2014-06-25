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
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

using KeePass.UI;
using KeePass.Plugins;
using KeePass.Util;

using KeePassLib;

using LomsonLib.UI;

namespace CustomIconDashboarderPlugin
{
	/// <summary>
	/// Description of CustomIconListing.
	/// </summary>
	public partial class DashboarderForm : Form
	{
		
		private IPluginHost m_PluginHost;
		private IconStatsHandler m_iconCounter = null;
		private Dictionary<int, PwCustomIcon> m_iconIndexer = null;

		private ListViewColumnSorter  m_lvwColumnSorter;
		
		public DashboarderForm(IPluginHost pluginHost)
		{
			
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			m_PluginHost = pluginHost;
			
		}
		
		private void OnFormLoad(object sender, EventArgs e)
		{
			Debug.Assert(m_PluginHost != null); if(m_PluginHost == null) throw new InvalidOperationException();
			
			GlobalWindowManager.AddWindow(this);
			
			m_iconCounter = new IconStatsHandler();
			m_iconCounter.Initialize( m_PluginHost.Database);
			
			buildCustomListView();

			this.m_lvViewIcon.ListViewItemSorter = m_lvwColumnSorter;
			
		}
		
		private void buildCustomListView()
		{
			Debug.Assert( m_iconCounter != null );
			
			m_lvViewIcon.Columns.Add( Resource.hdr_icon, 50 );
			m_lvViewIcon.Columns.Add( Resource.hdr_nEntry, 50, HorizontalAlignment.Center);
			m_lvViewIcon.Columns.Add( Resource.hdr_nGroup, 50, HorizontalAlignment.Center);
			m_lvViewIcon.Columns.Add( Resource.hdr_nTotal, 50, HorizontalAlignment.Center);
			
			m_lvUsedEntries.Columns.Add( Resource.hdr_titleEntry, 85 );
			m_lvUsedEntries.Columns.Add( Resource.hdr_userName, 85);
			m_lvUsedEntries.Columns.Add( Resource.hdr_groupName, 190 );
			
			m_lvUsedGroups.Columns.Add( Resource.hdr_groupName, 130 );
			m_lvUsedGroups.Columns.Add( Resource.hdr_fullPath, 230 );
			
						
			m_lvwColumnSorter = new ListViewColumnSorter();
			
			m_lvwColumnSorter.addColumnComparer(0, new IntegerAsStringComparer(false));
			m_lvwColumnSorter.addColumnComparer(1, new IntegerAsStringComparer(false));
			m_lvwColumnSorter.addColumnComparer(2, new IntegerAsStringComparer(false));
			m_lvwColumnSorter.addColumnComparer(3, new IntegerAsStringComparer(false));
			
			m_lvwColumnSorter.addDefaultSortedColumn(0,false);
			
			CreateCustomIconList();
		
		}
		
		
		/// <summary>
		/// Recreate the custom icons list view.
		/// </summary>
		/// <returns>Index of the previous custom icon, if specified.</returns>
		private void CreateCustomIconList()
		{
			// Retrieves from Keepass IconPickerForm
			ImageList ilCustom = UIUtil.BuildImageList(m_PluginHost.Database.CustomIcons, 16, 16);
			m_lvViewIcon.SmallImageList = ilCustom;
			
			int j = 0;
			m_iconIndexer = new Dictionary<int, PwCustomIcon>();
			foreach(PwCustomIcon pwci in m_PluginHost.Database.CustomIcons)
			{
				ListViewItem lvi = m_lvViewIcon.Items.Add(j.ToString(), j);
				m_iconIndexer.Add(j, pwci);
				
				lvi.SubItems.Add(m_iconCounter.getNbUsageInEntries(pwci).ToString());
				lvi.SubItems.Add(m_iconCounter.getNbUsageInGroups(pwci).ToString());
				int nTotal = m_iconCounter.getNbUsageInEntries(pwci) + m_iconCounter.getNbUsageInGroups(pwci);
				lvi.SubItems.Add( nTotal.ToString());
				lvi.Tag = pwci.Uuid;
				
				++j;
			}

		}
		
		
		private void CleanUpEx()
		{
			// Detach event handlers
			m_lvViewIcon.SmallImageList = null;
			m_iconCounter = null;
		}

		private void OnFormClosed(object sender, FormClosedEventArgs e)
		{
			CleanUpEx();
			GlobalWindowManager.RemoveWindow(this);
		}
		
		
		
		void M_lvViewIconSelectedIndexChanged(object sender, EventArgs e)
		{
			ListView.SelectedListViewItemCollection sItems = m_lvViewIcon.SelectedItems;
			
			m_lvUsedEntries.Items.Clear();
			m_lvUsedGroups.Items.Clear();
			
			if (sItems.Count > 0 ) {
				
				PwCustomIcon readIcon = m_iconIndexer[sItems[0].ImageIndex];
				
				pbo_selectedIcon.Image = resizedImage(readIcon.Image, 32,32);
				
				IEnumerator<PwEntry> myEntryEnumerator = m_iconCounter.getListEntries( readIcon ).GetEnumerator();
				while (myEntryEnumerator.MoveNext()) {
					PwEntry readEntry = myEntryEnumerator.Current;
					ListViewItem lvi = m_lvUsedEntries.Items.Add( readEntry.Strings.ReadSafe( PwDefs.TitleField ) );
					lvi.SubItems.Add( readEntry.Strings.ReadSafe( PwDefs.UserNameField ) );
					lvi.SubItems.Add( readEntry.ParentGroup.GetFullPath(".", false) );
				}
				
				IEnumerator<PwGroup> myGroupEnumerator = m_iconCounter.getListGroups( readIcon ).GetEnumerator();
				while (myGroupEnumerator.MoveNext()) {
					PwGroup readGroup = myGroupEnumerator.Current;
					ListViewItem lvi = m_lvUsedGroups.Items.Add( readGroup.Name );
					lvi.SubItems.Add( readGroup.GetFullPath() );
				}
			}
			else {
				pbo_selectedIcon.Image = null;
			}
		}
		
		private Image resizedImage(Image imgToBeConverted, int nWidth, int nHeight) {
			
				Image imgNew = imgToBeConverted;
				if(imgToBeConverted == null) { Debug.Assert(false); }

				if((imgToBeConverted.Width != nWidth) || (imgToBeConverted.Height != nHeight))
					imgNew = new Bitmap(imgToBeConverted, new Size(nWidth, nHeight));
				
				return imgNew;
		}
		
		private void OnLvViewIconColumnClick(object sender, ColumnClickEventArgs e)
		{	
			if ( e.Column == m_lvwColumnSorter.CurrentSortedColumn )
				{
					// Change sortOrder for this column
					if (m_lvwColumnSorter.CurrentSortOrder == SortOrder.Ascending)
					{
						m_lvwColumnSorter.CurrentSortOrder = SortOrder.None;
					}
					else if (m_lvwColumnSorter.CurrentSortOrder == SortOrder.Descending) {
						m_lvwColumnSorter.CurrentSortOrder = SortOrder.Ascending;
					}
					else
					{ // Set to Default
						m_lvwColumnSorter.CurrentSortOrder = SortOrder.Descending;
					}
					
				}
				else
				{
					// Define sort column.
					m_lvwColumnSorter.CurrentSortedColumn = e.Column;
					m_lvwColumnSorter.CurrentSortOrder = SortOrder.Descending;
				}
				
				// Process Sort
				this.m_lvViewIcon.Sort();
				UpdateColumnSortingIcons();
		}
		
		
		private void UpdateColumnSortingIcons()
		{
			if(UIUtil.SetSortIcon(m_lvViewIcon, m_lvwColumnSorter.CurrentSortedColumn,
				m_lvwColumnSorter.CurrentSortOrder)) return;

			string strAsc = "  \u2191"; // Must have same length
			string strDsc = "  \u2193"; // Must have same length
			if(WinUtil.IsWindows9x || WinUtil.IsWindows2000 || WinUtil.IsWindowsXP ||
				KeePassLib.Native.NativeLib.IsUnix())
			{
				strAsc = @"  ^";
				strDsc = @"  v";
			}
			else if(WinUtil.IsAtLeastWindowsVista)
			{
				strAsc = "  \u25B3";
				strDsc = "  \u25BD";
			}

			foreach(ColumnHeader ch in m_lvViewIcon.Columns)
			{
				string strCur = ch.Text, strNew = null;

				if(strCur.EndsWith(strAsc) || strCur.EndsWith(strDsc))
				{
					strNew = strCur.Substring(0, strCur.Length - strAsc.Length);
					strCur = strNew;
				}

				if((ch.Index == m_lvwColumnSorter.CurrentSortedColumn) &&
					(m_lvwColumnSorter.CurrentSortOrder != SortOrder.None))
				{
					if(m_lvwColumnSorter.CurrentSortOrder == SortOrder.Ascending)
						strNew = strCur + strAsc;
					else if(m_lvwColumnSorter.CurrentSortOrder == SortOrder.Descending)
						strNew = strCur + strDsc;
				}

				if(strNew != null) ch.Text = strNew;
			}
		}
		
	}
}
