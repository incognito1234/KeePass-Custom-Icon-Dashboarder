/*
 CustomIconDashboarder - KeePass Plugin to get some information and 
  manage custom icons

 Copyright (C) 2014 Jareth Lomson <incognito1234@users.sourceforge.net>
 
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
using System.Globalization;

using KeePass.UI;
using KeePass.Plugins;
using KeePass.Util;
using KeePass.Forms;

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
		private IconStatsHandler m_iconCounter;
		private Dictionary<int, PwCustomIcon> m_iconIndexer;
		private ImageList m_ilCustoms;

		private ListViewLayoutManager  m_lvIconsColumnSorter;
		private ListViewLayoutManager  m_lvGroupsColumnSorter;
		private ListViewLayoutManager  m_lvEntriesColumnSorter;
		
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
			
			InitEx();			
		}
		
		private void InitEx()
		{
			m_iconCounter = new IconStatsHandler();
			m_iconCounter.Initialize( m_PluginHost.Database);
			
			m_ilCustoms = UIUtil.BuildImageList(m_PluginHost.Database.CustomIcons, 16, 16);
			BuildCustomListView(m_ilCustoms);
		}
		
		private void BuildCustomListView(ImageList ilCustom)
		{
			Debug.Assert( m_iconCounter != null );
			
			// List View Used Entries
			m_lvUsedEntries.Columns.Add( Resource.hdr_titleEntry, 85 );
			m_lvUsedEntries.Columns.Add( Resource.hdr_userName, 85);
			m_lvUsedEntries.Columns.Add( Resource.hdr_groupName, 190 );
			
			m_lvEntriesColumnSorter = new ListViewLayoutManager();
			m_lvEntriesColumnSorter.AddColumnComparer(0, new LomsonLib.UI.StringComparer(false,true) );
			m_lvEntriesColumnSorter.AddColumnComparer(1, new LomsonLib.UI.StringComparer(false,true) );
			m_lvEntriesColumnSorter.AddColumnComparer(2, new LomsonLib.UI.StringComparer(false,true) );
			
			m_lvEntriesColumnSorter.AddDefaultSortedColumn(2,false);
			m_lvEntriesColumnSorter.AddDefaultSortedColumn(0,false);
			m_lvEntriesColumnSorter.AddDefaultSortedColumn(1,false);
			
			m_lvEntriesColumnSorter.AutoWidthColumn = true;
			
			m_lvEntriesColumnSorter.ApplyToListView( this.m_lvUsedEntries );
			
			// List View Used Group
			m_lvUsedGroups.Columns.Add( Resource.hdr_groupName, 130 );
			m_lvUsedGroups.Columns.Add( Resource.hdr_fullPath, 230 );
			
			m_lvGroupsColumnSorter = new ListViewLayoutManager();
			m_lvGroupsColumnSorter.AddColumnComparer(0, new LomsonLib.UI.StringComparer(false,true) );
			m_lvGroupsColumnSorter.AddColumnComparer(1, new LomsonLib.UI.StringComparer(false,true) );
			m_lvGroupsColumnSorter.AddDefaultSortedColumn(1,false);
			
			m_lvGroupsColumnSorter.AutoWidthColumn = true;
			
			m_lvGroupsColumnSorter.ApplyToListView( this.m_lvUsedGroups);
		
			// List View Icon
			m_lvViewIcon.Columns.Add( Resource.hdr_icon, 50 );
			m_lvViewIcon.Columns.Add( Resource.hdr_nEntry, 50, HorizontalAlignment.Center);
			m_lvViewIcon.Columns.Add( Resource.hdr_nGroup, 50, HorizontalAlignment.Center);
			m_lvViewIcon.Columns.Add( Resource.hdr_nTotal, 50, HorizontalAlignment.Center);
			m_lvIconsColumnSorter = new ListViewLayoutManager();
			
			m_lvIconsColumnSorter.AddColumnComparer(0, new IntegerAsStringComparer(false));
			m_lvIconsColumnSorter.AddColumnComparer(1, new IntegerAsStringComparer(false));
			m_lvIconsColumnSorter.AddColumnComparer(2, new IntegerAsStringComparer(false));
			m_lvIconsColumnSorter.AddColumnComparer(3, new IntegerAsStringComparer(false));
			m_lvIconsColumnSorter.AddDefaultSortedColumn(0,false);

			m_lvIconsColumnSorter.AutoWidthColumn = true;
			m_lvIconsColumnSorter.CheckAllCheckBox = cb_allIconsSelection;
			
			ListViewLayoutManager.dlgStatisticMessageUpdater  ehStats = delegate(String msg) {
				tsl_nbIcons.Text = msg;
			};
			m_lvIconsColumnSorter.AssignStatisticMessageUpdater(ehStats, true, false, "%3 of %1 checked");
			
			ListViewLayoutManager.dlgMultiCheckingCheckBoxes ehMulti = delegate(IList<ListViewItem> lst) {
				//Disable button if no elements is checked
				if (m_lvViewIcon.CheckedItems.Count == 0 ) {
					btn_ModifyIcon.Enabled = false;
					btn_removeIcons.Enabled = false;
				}
				else {
					btn_ModifyIcon.Enabled = true;
					btn_removeIcons.Enabled = true;
				}
			};
			m_lvIconsColumnSorter.DefineMultiCheckingBehavior(ehMulti);
			m_lvIconsColumnSorter.EnableMultiCheckingControl();
			
			m_lvIconsColumnSorter.ApplyToListView( this.m_lvViewIcon );
			ehMulti(null); // Disable buttons
					
			CreateCustomIconList(ilCustom);
			
		}
			
		/// <summary>
		/// Recreate the custom icons list view.
		/// </summary>
		/// <returns>Index of the previous custom icon, if specified.</returns>
		private void CreateCustomIconList(ImageList ilCustom)
		{
			// Retrieves from Keepass IconPickerForm
			m_lvViewIcon.SmallImageList = ilCustom;
			
			int j = 0;
			m_iconIndexer = new Dictionary<int, PwCustomIcon>();
			
			m_lvViewIcon.BeginUpdate();
			
			foreach(PwCustomIcon pwci in m_PluginHost.Database.CustomIcons)
			{
				ListViewItem lvi = new ListViewItem(j.ToString(NumberFormatInfo.InvariantInfo), j);
				m_iconIndexer.Add(j, pwci);
				
				lvi.SubItems.Add(m_iconCounter.GetNbUsageInEntries(pwci).ToString(NumberFormatInfo.InvariantInfo));
				lvi.SubItems.Add(m_iconCounter.GetNbUsageInGroups(pwci).ToString(NumberFormatInfo.InvariantInfo));
				int nTotal = m_iconCounter.GetNbUsageInEntries(pwci) + m_iconCounter.GetNbUsageInGroups(pwci);
				lvi.SubItems.Add( nTotal.ToString(NumberFormatInfo.InvariantInfo));
				lvi.Tag = pwci.Uuid;
				m_lvViewIcon.Items.Add(lvi);
				++j;
			}
			
			m_lvViewIcon.EndUpdate();
			m_lvIconsColumnSorter.UpdateStatistics();
		}
		
		private void ResetDashboard() {
			m_lvViewIcon.Items.Clear();
			m_iconCounter = new IconStatsHandler();
			m_iconCounter.Initialize( m_PluginHost.Database);
			
			m_ilCustoms = UIUtil.BuildImageList(m_PluginHost.Database.CustomIcons, 16, 16);
		
			CreateCustomIconList(m_ilCustoms);
			m_lvUsedEntries.Items.Clear();
			m_lvUsedGroups.Items.Clear();
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
		
		void OnLvViewIconSelectedIndexChanged(object sender, EventArgs e)
		{
			ListView.SelectedListViewItemCollection sItems = m_lvViewIcon.SelectedItems;
			
			m_lvUsedEntries.Items.Clear();
			m_lvUsedGroups.Items.Clear();
			
			if (sItems.Count > 0 ) {
				
				PwCustomIcon readIcon = m_iconIndexer[sItems[0].ImageIndex];
				
				pbo_selectedIcon.Image = ResizedImage(readIcon.Image, 32,32);
				
				IEnumerator<PwEntry> myEntryEnumerator = m_iconCounter.GetListEntries( readIcon ).GetEnumerator();
				
				// Update entry and group list
				// It is necessary to add all subitems to listView in a single oneshot.
				// On the other case, a runtime exception occurs when the listView is sorted
				// and the sort condition take into account one of the nth column, n > 1
				while (myEntryEnumerator.MoveNext()) {
					PwEntry readEntry = myEntryEnumerator.Current;
					
					ListViewItem lvi = new ListViewItem( readEntry.Strings.ReadSafe( PwDefs.TitleField ) );
					lvi.SubItems.Add( readEntry.Strings.ReadSafe( PwDefs.UserNameField ) );
					lvi.SubItems.Add( readEntry.ParentGroup.GetFullPath(".", false) );
					m_lvUsedEntries.Items.Add(lvi);
				}
				
				IEnumerator<PwGroup> myGroupEnumerator = m_iconCounter.GetListGroups( readIcon ).GetEnumerator();
				while (myGroupEnumerator.MoveNext()) {
					PwGroup readGroup = myGroupEnumerator.Current;
					ListViewItem lvi = new ListViewItem( readGroup.Name );
					lvi.SubItems.Add( readGroup.GetFullPath() );
					m_lvUsedGroups.Items.Add( lvi );
				}
			}
			else {
				pbo_selectedIcon.Image = null;
			}
		}
		
		private static Image ResizedImage(Image imgToBeConverted, int nWidth, int nHeight) {
			
				Image imgNew = imgToBeConverted;
				if(imgToBeConverted == null) { Debug.Assert(false); }

				if((imgToBeConverted.Width != nWidth) || (imgToBeConverted.Height != nHeight))
					imgNew = new Bitmap(imgToBeConverted, new Size(nWidth, nHeight));
				
				return imgNew;
		}
		
		void OnModifyIconClick(object sender, EventArgs e)
		{
			PwCustomIcon firstIcon = m_iconIndexer[
				m_lvViewIcon.CheckedItems[0].ImageIndex];
			
			IconPickerForm ipf = new IconPickerForm();
			ListView lvEntriesOfMainForm = (ListView)GetControlFromForm( m_PluginHost.MainWindow, "m_lvEntries");
			ImageList il_allIcons = lvEntriesOfMainForm.SmallImageList; // Standard icons are the "PwIcon.Count"th first items
			ipf.InitEx(
				il_allIcons,
			    (uint)PwIcon.Count,
			    m_PluginHost.Database,
			    0,
				firstIcon.Uuid);

			if(ipf.ShowDialog() == DialogResult.OK) 
			{
				foreach (ListViewItem lvi in m_lvViewIcon.CheckedItems) {
					PwCustomIcon readIcon = m_iconIndexer[lvi.ImageIndex];
					UpdateCustomIconFromUuid( readIcon.Uuid, 
				                         (PwIcon)ipf.ChosenIconId,
				                         ipf.ChosenCustomIconUuid );
				
				}
				ResetDashboard();
				m_PluginHost.Database.UINeedsIconUpdate = true;
				m_PluginHost.Database.Modified = true;
				m_PluginHost.MainWindow.UpdateUI(false, null, true, null, true, null, true);
			}

			UIUtil.DestroyForm(ipf);
		}
		
		
		private void UpdateCustomIconFromUuid(
			PwUuid srcCustomUuid,
			PwIcon dstStandardIcon,
			PwUuid dstCustomUuid ) {
			
				ICollection<PwEntry> myEntriesCollection =
						m_iconCounter.GetListEntriesFromUuid( srcCustomUuid );
				
				ICollection<PwGroup> myGroupsCollection =
						m_iconCounter.GetListGroupsFromUuid( srcCustomUuid );
				
				if(!dstCustomUuid.Equals(PwUuid.Zero)) // Custom icon
				{
					foreach (PwEntry pe in myEntriesCollection) {
						pe.CustomIconUuid = dstCustomUuid;
					}
					foreach (PwGroup pg in myGroupsCollection) {
						pg.CustomIconUuid = dstCustomUuid;
					}				
				}
				else // Standard icon
				{
					foreach (PwEntry pe in myEntriesCollection) {
						pe.IconId = dstStandardIcon;
						pe.CustomIconUuid = PwUuid.Zero;
					}
					foreach (PwGroup pg in myGroupsCollection) {
						pg.IconId = dstStandardIcon;
						pg.CustomIconUuid = PwUuid.Zero;
					}

				}
		}
		
		
		public static Control GetControlFromForm(Control form, String name) {
			Control[] cntrls = form.Controls.Find(name, true);
			if (cntrls.Length == 0)
				return null;
			return cntrls[0];
	    }
		

		
		void OnRemoveIconsClick(object sender, EventArgs e)
		{
			ListView.CheckedListViewItemCollection lvsiChecked = m_lvViewIcon.CheckedItems;
			List<PwUuid> vUuidsToDelete = new List<PwUuid>();

			foreach(ListViewItem lvi in lvsiChecked)
			{
				PwUuid uuidIcon = m_iconIndexer[lvi.ImageIndex].Uuid;
				vUuidsToDelete.Add(uuidIcon);
			}

			m_PluginHost.Database.DeleteCustomIcons(vUuidsToDelete);

			if(vUuidsToDelete.Count > 0)
			{
				m_PluginHost.Database.UINeedsIconUpdate = true;
				m_PluginHost.Database.Modified = true;
				ResetDashboard();
				m_PluginHost.MainWindow.UpdateUI(false, null, true, null, true, null, true);
			}
			
		}
		
		
	}
}
