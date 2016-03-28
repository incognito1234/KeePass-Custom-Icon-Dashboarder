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
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;

using KeePass.UI;
using KeePass.Plugins;
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
		private Dictionary<PwUuid, IconChooser> m_iconChooserIndexer;

		private ListViewLayoutManager  m_lvIconsColumnSorter;
		private ListViewLayoutManager  m_lvGroupsColumnSorter;
		private ListViewLayoutManager  m_lvEntriesColumnSorter;
		private ListViewLayoutManager  m_lvDownloadResultColumnSorter;
		private ListViewLayoutManager  m_lvAllEntriesColumnSorter;
		
		#region Initialization & Dispose
		public DashboarderForm(IPluginHost pluginHost)
		{
			
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			m_PluginHost = pluginHost;
			this.Icon = m_PluginHost.MainWindow.Icon;
			
		}
		
		private void OnFormLoad(object sender, EventArgs e)
		{
			Debug.Assert(m_PluginHost != null); if(m_PluginHost == null) throw new InvalidOperationException();
			
			GlobalWindowManager.AddWindow(this);
			BestIconFinder.InitClass();
			InitEx();
			cbo_iconActionSelector.SelectedIndex = 0;
			// Comment to debug
			//this.tco_right.TabPages.Remove(tpa_Debug);
		}
		
		private void OnFormDispose()
		{
			
		}
		
		private void InitEx()
		{
			m_iconChooserIndexer = new Dictionary<PwUuid, IconChooser>();
			BuildCustomIconListView();
			BuildEntriesListViews();
			BuildUsageListViews();
			ResetDashboard();
		}
		
		private void ResetDashboard() {
			m_iconCounter = new IconStatsHandler();
			m_iconCounter.Initialize( m_PluginHost.Database);
			
			ResetAllIconDashboard();
			m_lvUsedEntries.Items.Clear();
			m_lvUsedGroups.Items.Clear();
		}
		
		private void CleanUpEx()
		{
			// Detach event handlers
			m_lvViewIcon.SmallImageList = null;
			m_lvAllEntries.SmallImageList = null;
			m_iconCounter = null;
		}

		private void OnFormClosed(object sender, FormClosedEventArgs e)
		{
			CleanUpEx();
			GlobalWindowManager.RemoveWindow(this);
		}
		
		#endregion
		
		#region ListView icon action
		private void BuildCustomIconListView()
		{
			// List View Icon
			m_lvViewIcon.Columns.Add( Resource.hdr_icon, 50 );
			m_lvViewIcon.Columns.Add( Resource.hdr_currentSize, 50, HorizontalAlignment.Center );
			m_lvViewIcon.Columns.Add( Resource.hdr_newSize, 50, HorizontalAlignment.Center );
			m_lvViewIcon.Columns.Add( Resource.hdr_nEntry, 50, HorizontalAlignment.Center);
			m_lvViewIcon.Columns.Add( Resource.hdr_nGroup, 50, HorizontalAlignment.Center);
			m_lvViewIcon.Columns.Add( Resource.hdr_nTotal, 50, HorizontalAlignment.Center);
			m_lvViewIcon.Columns.Add( Resource.hdr_nURL, 50, HorizontalAlignment.Center );
			
			m_lvIconsColumnSorter = new ListViewLayoutManager();
			
			m_lvIconsColumnSorter.AddColumnComparer(0, new IntegerAsStringComparer(false));
			m_lvIconsColumnSorter.AddColumnComparer(1, new SizeComparer(false));
			m_lvIconsColumnSorter.AddColumnComparer(2, new SizeComparer(false));
			m_lvIconsColumnSorter.AddColumnComparer(3, new IntegerAsStringComparer(false));
			m_lvIconsColumnSorter.AddColumnComparer(4, new IntegerAsStringComparer(false));
			m_lvIconsColumnSorter.AddColumnComparer(5, new IntegerAsStringComparer(false));
			m_lvIconsColumnSorter.AddColumnComparer(6, new IntegerAsStringComparer(false));
			m_lvIconsColumnSorter.AddDefaultSortedColumn(0,false);

			m_lvIconsColumnSorter.AutoWidthColumn = true;
			m_lvIconsColumnSorter.CheckAllCheckBox = cb_allIconsSelection;
			
			ListViewLayoutManager.dlgStatisticMessageUpdater  ehStats = delegate(String msg) {
				//tsl_nbIcons.Text = msg;
			};
			m_lvIconsColumnSorter.AssignStatisticMessageUpdater(ehStats, true, false, "%3 of %1 checked");
			
			ListViewLayoutManager.dlgMultiCheckingCheckBoxes ehMulti = delegate(IList<ListViewItem> lst) {
				//Disable button if no elements is checked
				if (m_lvViewIcon.CheckedItems.Count == 0 ) {
					btn_performIconAction.Enabled = false;					
				}
				else {
					btn_performIconAction.Enabled = cbo_iconActionSelector.SelectedIndex != 0;
				}
			};
			m_lvIconsColumnSorter.DefineMultiCheckingBehavior(ehMulti);
			m_lvIconsColumnSorter.EnableMultiCheckingControl();
			
			m_lvIconsColumnSorter.ApplyToListView( this.m_lvViewIcon );
			
			ehMulti(null); // Disable buttons
		}
		
		private void BuildUsageListViews() {
			// List View Used Entries
			m_lvUsedEntries.Columns.Add( Resource.hdr_titleEntry, 85 );
			m_lvUsedEntries.Columns.Add( Resource.hdr_userName, 85);
			m_lvUsedEntries.Columns.Add( Resource.hdr_url, 140);
			m_lvUsedEntries.Columns.Add( Resource.hdr_groupName, 140);
			
			m_lvEntriesColumnSorter = new ListViewLayoutManager();
			m_lvEntriesColumnSorter.AddColumnComparer(0, new LomsonLib.UI.StringComparer(false,true) );
			m_lvEntriesColumnSorter.AddColumnComparer(1, new LomsonLib.UI.StringComparer(false,true) );
			m_lvEntriesColumnSorter.AddColumnComparer(2, new LomsonLib.UI.StringComparer(false,true) );
			m_lvEntriesColumnSorter.AddColumnComparer(3, new LomsonLib.UI.StringComparer(false,true) );
			
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
		
			// List DownloadResult
			m_lvDownloadResult.Columns.Add("id", 40);
			m_lvDownloadResult.Columns.Add("Size", 40);
			m_lvDownloadResult.Columns.Add("URL", 150, HorizontalAlignment.Left );
		
			m_lvDownloadResultColumnSorter = new ListViewLayoutManager();
			m_lvDownloadResultColumnSorter.AddColumnComparer(0, new IntegerAsStringComparer(false) );
			m_lvDownloadResultColumnSorter.AddColumnComparer(1, new SizeComparer(false) );
			m_lvDownloadResultColumnSorter.AddColumnComparer(2, new LomsonLib.UI.StringComparer(false,true) );
			
			m_lvDownloadResultColumnSorter.AutoWidthColumn = true;
			m_lvDownloadResultColumnSorter.AddDefaultSortedColumn(0,false);
			m_lvDownloadResultColumnSorter.ApplyToListView(m_lvDownloadResult);	
		}
		
		
		private void BuildEntriesListViews() {
			// List View Icon
			m_lvAllEntries.Columns.Add( Resource.hdr_idEntry, 150 );
			m_lvAllEntries.Columns.Add( Resource.hdr_currentSize, 50, HorizontalAlignment.Center );
			m_lvAllEntries.Columns.Add( Resource.hdr_newSize, 50, HorizontalAlignment.Center );
			m_lvAllEntries.Columns.Add( Resource.hdr_group, 50, HorizontalAlignment.Center);
			m_lvAllEntries.Columns.Add( Resource.hdr_url, 150, HorizontalAlignment.Left);
			
			m_lvAllEntriesColumnSorter = new ListViewLayoutManager();
			
			m_lvAllEntriesColumnSorter.AddColumnComparer(0, new LomsonLib.UI.StringComparer(false, true));
			m_lvAllEntriesColumnSorter.AddColumnComparer(1, new SizeComparer(false));
			m_lvAllEntriesColumnSorter.AddColumnComparer(2, new SizeComparer(false));
			m_lvAllEntriesColumnSorter.AddColumnComparer(3, new LomsonLib.UI.StringComparer(false, true));
			m_lvAllEntriesColumnSorter.AddColumnComparer(4, new LomsonLib.UI.StringComparer(false, true));

			m_lvAllEntriesColumnSorter.AddDefaultSortedColumn(0,false);

			m_lvAllEntriesColumnSorter.AutoWidthColumn = true;
			m_lvAllEntriesColumnSorter.CheckAllCheckBox = cb_allEntriesSelection;
			
			ListViewLayoutManager.dlgMultiCheckingCheckBoxes ehMulti = delegate(IList<ListViewItem> lst) {
				//Disable button if no elements is checked
				if (m_lvAllEntries.CheckedItems.Count == 0 ) {
					btn_performEntryAction.Enabled = false;					
				}
				else {
					btn_performEntryAction.Enabled = cbo_entryActionSelector.SelectedIndex != 0;
				}
			};
			m_lvAllEntriesColumnSorter.DefineMultiCheckingBehavior(ehMulti);
			m_lvAllEntriesColumnSorter.EnableMultiCheckingControl();
			
			m_lvAllEntriesColumnSorter.ApplyToListView( this.m_lvAllEntries );
			
			ehMulti(null); // Disable buttons
			
		}
			
		/// <summary>
		/// Recreate the custom icons list view.
		/// </summary>
		/// <returns>Index of the previous custom icon, if specified.</returns>
		private void CreateCustomIconList()
		{
			int cx = CompatibilityManager.ScaleIntX(16);
			int cy = CompatibilityManager.ScaleIntX(16);
			ImageList ilCustoms =
				UIUtil.BuildImageList(m_PluginHost.Database.CustomIcons, cx, cy);
			
			// Retrieves from Keepass IconPickerForm
			m_lvViewIcon.SmallImageList = ilCustoms;
			
			int j = 0;
			m_iconIndexer = new Dictionary<int, PwCustomIcon>();
			
			m_lvViewIcon.BeginUpdate();
			
			foreach(PwCustomIcon pwci in m_PluginHost.Database.CustomIcons)
			{
				var lvi = new ListViewItem(j.ToString(NumberFormatInfo.InvariantInfo), j);
				Image originalImage = CompatibilityManager.GetOriginalImage(pwci);
				
				m_iconIndexer.Add(j, pwci);
				// Current Size
				lvi.SubItems.Add(originalImage.Width.ToString(NumberFormatInfo.InvariantInfo)
				                + " x " +
				               originalImage.Height.ToString(NumberFormatInfo.InvariantInfo));
				lvi.SubItems.Add("");
				lvi.SubItems.Add(m_iconCounter.GetNbUsageInEntries(pwci).ToString(NumberFormatInfo.InvariantInfo));
				lvi.SubItems.Add(m_iconCounter.GetNbUsageInGroups(pwci).ToString(NumberFormatInfo.InvariantInfo));
				int nTotal = m_iconCounter.GetNbUsageInEntries(pwci) + m_iconCounter.GetNbUsageInGroups(pwci);
				lvi.SubItems.Add( nTotal.ToString(NumberFormatInfo.InvariantInfo));
				lvi.SubItems.Add(m_iconCounter.GetNbUrlsInEntries(pwci).ToString(NumberFormatInfo.InvariantInfo));
				UpdateBestIconFinderResultForIconLvi(lvi);
				
				lvi.Tag = pwci.Uuid;
				m_lvViewIcon.Items.Add(lvi);
				++j;
			}
			
			m_lvViewIcon.EndUpdate();
			m_lvIconsColumnSorter.UpdateStatistics();
		}
		
		
		private void CreateAllEntriesList()
		{
			m_lvAllEntries.BeginUpdate();
			m_lvAllEntries.SmallImageList = m_PluginHost.MainWindow.ClientIcons;
		
			int j=0;
			foreach(PwEntry pe in m_PluginHost.Database.RootGroup.GetEntries(true))
			{
				var lvi = new ListViewItem(pe.Strings.ReadSafe(PwDefs.TitleField));
				
				// Current Size
				lvi.SubItems.Add("0 x 0");
				lvi.SubItems.Add("0 x 0");
				
				lvi.SubItems.Add(pe.ParentGroup.GetFullPath(".",false));
				lvi.SubItems.Add(pe.Strings.ReadSafe(PwDefs.UrlField));
				if(pe.CustomIconUuid.Equals(PwUuid.Zero))
					lvi.ImageIndex = (int)pe.IconId;
				else
					lvi.ImageIndex = (int)PwIcon.Count +
						 m_PluginHost.Database.GetCustomIconIndex(pe.CustomIconUuid);
				
				//lvi.Tag = pe;
				m_lvAllEntries.Items.Add(lvi);
				j++;
			}
			m_lvAllEntries.EndUpdate();
		}
		
		
		
		private void ResetAllIconDashboard() {
			m_lvViewIcon.Items.Clear();
			m_iconCounter = new IconStatsHandler();
			m_iconCounter.Initialize( m_PluginHost.Database);
			CreateCustomIconList();
			CreateAllEntriesList();
			m_lvIconsColumnSorter.UpdateCheckAllCheckBox(false);
			m_lvEntriesColumnSorter.UpdateCheckAllCheckBox(false);
		}
		
		private ListViewItem oldSelectedLvi = null;
		void OnLvViewIconSelectedIndexChanged(object sender, EventArgs e)
		{
			if (m_lvViewIcon.SelectedItems.Count > 0) {
				if (m_lvViewIcon.SelectedItems[0] == oldSelectedLvi) {
					return;
				}
				//else
				oldSelectedLvi = m_lvViewIcon.SelectedItems[0];
			}
			else {
				oldSelectedLvi = null;
			}
			
			UpdateIconPaneFromSelectedIcon();
			UpdateDownloadResultPaneFromSelectedIcon();
			
			//if (m_lvViewIcon.SelectedItems.Count > 0) 

			
				
		}
		
		void OnModifyIconClick(object sender, EventArgs e)
		{
			PwCustomIcon firstIcon = m_iconIndexer[
				m_lvViewIcon.CheckedItems[0].ImageIndex];
			
			var ipf = new IconPickerForm();
			var lvEntriesOfMainForm = (ListView)GetControlFromForm( m_PluginHost.MainWindow, "m_lvEntries");
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
				NotifyDatabaseModificationAndUpdateMainForm();
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
			return cntrls.Length == 0 ? null : cntrls[0];
	    }
		

		
		void OnRemoveIconsClick(object sender, EventArgs e)
		{
			ListView.CheckedListViewItemCollection lvsiChecked = m_lvViewIcon.CheckedItems;
			var vUuidsToDelete = new List<PwUuid>();

			foreach(ListViewItem lvi in lvsiChecked)
			{
				PwUuid uuidIcon = m_iconIndexer[lvi.ImageIndex].Uuid;
				vUuidsToDelete.Add(uuidIcon);
			}

			m_PluginHost.Database.DeleteCustomIcons(vUuidsToDelete);

			if(vUuidsToDelete.Count > 0)
			{
				ResetDashboard();
				NotifyDatabaseModificationAndUpdateMainForm();
			}
			
		}
		
		void OnDownloadIconClick(object sender, EventArgs e)
		{
			ListView.CheckedListViewItemCollection lvsiChecked = m_lvViewIcon.CheckedItems;
			foreach(ListViewItem lvi in lvsiChecked)
			{
				if (UpdateBestIconFinderAndLviFromIconLvi( lvi )) {
				    System.Threading.Thread.Sleep(500);
				}
			   lvi.EnsureVisible();
			   m_lvViewIcon.Refresh();
			}
			
			UpdateDownloadResultPaneFromSelectedIcon();
		
		}
		
		/// <summary>
		/// Update bestIconFinder and size in List View Item
		/// if icon has not already been downloaded
		/// </summary>
		/// <param name="lvi"></param>
		/// <returns>true if an update has occured. false else</returns>
		private bool UpdateBestIconFinderAndLviFromIconLvi(ListViewItem iconLvi) {
			PwCustomIcon readIcon = m_iconIndexer[iconLvi.ImageIndex];
			if ( !m_iconChooserIndexer.ContainsKey( readIcon.Uuid ) ) {
				var lstUris = new List<Uri>();
				lstUris.AddRange( m_iconCounter.GetListUris( readIcon ) );
				
				// Retrieve images
				BestIconFinder bif;
				if (lstUris.Count > 0) {
					Uri uri1 = lstUris[0];
					bif = new BestIconFinder( uri1 );
					bif.FindBestIcon();
				}
				else {
					bif = new BestIconFinder();
				}
				   
				var ich = new IconChooser(bif);
			    m_iconChooserIndexer.Add( readIcon.Uuid, ich);
			    UpdateBestIconFinderResultForIconLvi( iconLvi);
				return true;
			}
			return false;
		}
		
		private void UpdateBestIconFinderResultForIconLvi( ListViewItem lvi) {
			IconChooser ich = null;
			PwCustomIcon readIcon = m_iconIndexer[lvi.ImageIndex];
			if (m_iconChooserIndexer.ContainsKey(readIcon.Uuid)) {
				ich = m_iconChooserIndexer[readIcon.Uuid];
			}
			else {
				lvi.SubItems[2].Text = "";
				return;
			}
			if (ich.Bif.Result.ResultCode == FinderResult.RESULT_NO_URL) {
				lvi.SubItems[2].Text = Resource.val_nourl;
			}
			else if (ich.Bif.BestImage != null) {
		    	lvi.SubItems[2].Text = ich.Bif.BestImage.Width.ToString(NumberFormatInfo.InvariantInfo)
                + " x " +
               ich.Bif.BestImage.Height.ToString(NumberFormatInfo.InvariantInfo);
		    }
	    	else {
		    	lvi.SubItems[2].Text = Resource.val_na;
	    	}
		}
		
		void UpdateIconPaneFromSelectedIcon()
		{
			ListView.SelectedListViewItemCollection sItems = m_lvViewIcon.SelectedItems;
			
			m_lvUsedEntries.Items.Clear();
			m_lvUsedGroups.Items.Clear();
	 
			if (sItems.Count > 0 ) {
				
				PwCustomIcon readIcon = m_iconIndexer[sItems[0].ImageIndex];
				
				Image originalImage = CompatibilityManager.GetOriginalImage(readIcon);
				
				pbo_selectedIcon128.BackgroundImage = 
					CompatibilityManager.ResizedImage(originalImage, 128, 128);
				pbo_selectedIcon64.BackgroundImage = 
					CompatibilityManager.ResizedImage(originalImage, 64, 64);
				pbo_selectedIcon32.BackgroundImage =
					CompatibilityManager.ResizedImage(originalImage, 32, 32);					
				pbo_selectedIcon16.BackgroundImage = 
					CompatibilityManager.ResizedImage(originalImage, 16, 16);
				lbl_originalSize.Text =
					"Original Size : " +
					originalImage.Width +
					" x " +
					originalImage.Height;
				IEnumerator<PwEntry> myEntryEnumerator = m_iconCounter.GetListEntries( readIcon ).GetEnumerator();
				
				// Update entry and group list
				// It is necessary to add all subitems to listView in a single oneshot.
				// On the other case, a runtime exception occurs when the listView is sorted
				// and the sort condition take into account one of the nth column, n > 1
				while (myEntryEnumerator.MoveNext()) {
					PwEntry readEntry = myEntryEnumerator.Current;
					
					var lvi = new ListViewItem( readEntry.Strings.ReadSafe( PwDefs.TitleField ) );
					lvi.SubItems.Add( readEntry.Strings.ReadSafe( PwDefs.UserNameField ) );
					lvi.SubItems.Add( readEntry.Strings.ReadSafe( PwDefs.UrlField ) );
					lvi.SubItems.Add( readEntry.ParentGroup.GetFullPath(".", false) );
					m_lvUsedEntries.Items.Add(lvi);
				}
				
				IEnumerator<PwGroup> myGroupEnumerator = m_iconCounter.GetListGroups( readIcon ).GetEnumerator();
				while (myGroupEnumerator.MoveNext()) {
					PwGroup readGroup = myGroupEnumerator.Current;
					var lvi = new ListViewItem( readGroup.Name );
					lvi.SubItems.Add( readGroup.GetFullPath() );
					m_lvUsedGroups.Items.Add( lvi );
				}
			}
			else {
				pbo_selectedIcon128.Image = null;
			}	
		}
		
		void UpdateDownloadResultPaneFromSelectedIcon() 
		{
			ListView.SelectedListViewItemCollection sItems = m_lvViewIcon.SelectedItems;
			if (sItems.Count > 0) {
				ListViewItem lvi = sItems[0];
				
				var pu = (PwUuid)lvi.Tag;
				bool cleanImage = false;
				
				if (m_iconChooserIndexer.ContainsKey(pu)) {
					IconChooser ich =  m_iconChooserIndexer[pu];
					Image img = ich.ChoosenIcon;
					BestIconFinder bif = ich.Bif;
				    
					if (img != null) {
						pbo_downloadedIcon128.BackgroundImage = 
							CompatibilityManager.ResizedImage(img, 128, 128);
						pbo_downloadedIcon64.BackgroundImage = 
							CompatibilityManager.ResizedImage(img, 64, 64);
						pbo_downloadedIcon32.BackgroundImage =
							CompatibilityManager.ResizedImage(img, 32, 32);					
						pbo_downloadedIcon16.BackgroundImage = 
							CompatibilityManager.ResizedImage(img, 16, 16);
						lbl_newSize.Text =
							"New Size : " +
							img.Width +
							" x " +
							img.Height;
						
						
						m_lvDownloadResult.Items.Clear();
						IEnumerator<ImageInfo> enumImageInfo = bif.ListImageInfo.GetEnumerator();
						m_lvDownloadResult.LargeImageList = new ImageList();
						m_lvDownloadResult.SmallImageList = new ImageList();
						m_lvDownloadResult.LargeImageList.ImageSize = new Size(32, 32);
						m_lvDownloadResult.LargeImageList.ColorDepth = ColorDepth.Depth32Bit;
						m_lvDownloadResult.SmallImageList.ImageSize = new Size(32, 32);
						m_lvDownloadResult.SmallImageList.ColorDepth = ColorDepth.Depth32Bit;
						
						int ii = 0;
						while (enumImageInfo.MoveNext()) {
							ImageInfo myImageInfo = enumImageInfo.Current;
							
							var lviImageInfo = 
								new ListViewItem( (ii+1).ToString(NumberFormatInfo.InvariantInfo )
								                 );
							lviImageInfo.SubItems.Add( myImageInfo.ImgData.Width.ToString(NumberFormatInfo.InvariantInfo)
								              + "x" 
								              + myImageInfo.ImgData.Height.ToString(NumberFormatInfo.InvariantInfo));
							lviImageInfo.SubItems.Add( myImageInfo.Url );
							lviImageInfo.ImageIndex = ii;
							lviImageInfo.Tag = ii;
							if (  ich.ChoosenIndex == ii )
								lviImageInfo.Selected = true;
							
							
							Image smallImage = CompatibilityManager.ResizedImage(
								myImageInfo.ImgData,
								32, 32);
							m_lvDownloadResult.LargeImageList.Images.Add( smallImage );
							m_lvDownloadResult.SmallImageList.Images.Add( smallImage );
							m_lvDownloadResult.Items.Add(lviImageInfo);
						
							ii++;						
						}
						
						//m_lvDownloadResult.Items[ich.ChoosenIndex].Selected |= 
						//	bif.ListImageInfo.Count > 0;
					}
					else {
						cleanImage = true;
						m_lvDownloadResult.Items.Clear();
					}
				     rtb_details.Text=bif.Details;
			
					}
					else {
						cleanImage = true;
					}
				
					if (cleanImage) {
						pbo_downloadedIcon128.BackgroundImage = null;
						pbo_downloadedIcon64.BackgroundImage = null;
						pbo_downloadedIcon32.BackgroundImage = null;
						pbo_downloadedIcon16.BackgroundImage = null;
						m_lvDownloadResult.Items.Clear();
						lbl_newSize.Text = "New Size :";
						
					}
			}
		}
		
		void OnPickIconClick(object sender, EventArgs e)
		{
			ListView.CheckedListViewItemCollection lvsiChecked = m_lvViewIcon.CheckedItems;
						
			foreach(ListViewItem lvi in lvsiChecked) {
				
				UpdateBestIconFinderAndLviFromIconLvi( lvi );
				PwCustomIcon readIcon = m_iconIndexer[lvi.ImageIndex];
				
				Debug.Assert(m_iconChooserIndexer.ContainsKey( readIcon.Uuid ));
				
				BestIconFinder bif = m_iconChooserIndexer[readIcon.Uuid].Bif;
				if (bif.BestImage != null) {
					PwCustomIcon newIcon = UpdateCustomIconFromImage(
						readIcon, bif.BestImage, m_PluginHost.Database);
					if ((newIcon != null ) && (!m_iconChooserIndexer.ContainsKey(newIcon.Uuid)) ) {
						m_iconChooserIndexer.Add(newIcon.Uuid, new IconChooser(bif));
						UpdateBestIconFinderResultForIconLvi(lvi);
					}
					NotifyDatabaseModificationAndUpdateMainForm();
				}
			}
			ResetDashboard();
		}
		
		/// <summary>
		/// Replace all reference to icon with image.
		/// All entries and groups attached to refIcon will be attached to a custom icon that looks like img.
		/// </summary>
		/// <returns>PwCustomIcon if new icon has been created. null else.</returns>
		private PwCustomIcon UpdateCustomIconFromImage( PwCustomIcon refIcon, Image img, PwDatabase kdb) {
			PwCustomIcon targetIcon = GetCustomIconFromImageAndUpdateKdbIfNecessary( img, kdb);
			
			if (targetIcon == null) return null;
			
			ICollection<PwEntry> listEntries = m_iconCounter.GetListEntries( refIcon );
			IEnumerator<PwEntry> myEntryEnumerator = listEntries.GetEnumerator();
			while (myEntryEnumerator.MoveNext()) {
				PwEntry readEntry = myEntryEnumerator.Current;
				readEntry.CustomIconUuid = targetIcon.Uuid;
				readEntry.Touch(true);
			}
			
			ICollection<PwGroup> listGroups = m_iconCounter.GetListGroups( refIcon );
			IEnumerator<PwGroup> myGroupEnumerator = listGroups.GetEnumerator();
			while (myGroupEnumerator.MoveNext()) {
				PwGroup readGroup = myGroupEnumerator.Current;
				readGroup.CustomIconUuid = targetIcon.Uuid;
			}
			
			if ( (listEntries.Count > 0) || (listGroups.Count > 0) ) {
				kdb.UINeedsIconUpdate = true;
				kdb.Modified = true;
			}
			
			return targetIcon;
			
		}
		
		/// <summary>
		/// Get Custom Icon from image
		/// </summary>
		/// <param name="img">Image to be compared</param>
		/// <param name="kdb">Keepass database where custom icons are based.
		/// This database will be updated if customIcons is missing.</param>
		/// <returns>Custom Icon in database. null if an error occurs</returns>
		private PwCustomIcon GetCustomIconFromImageAndUpdateKdbIfNecessary( Image img, PwDatabase kdb) {
			// From IconPickerForm.OnBtnCustomAdd - Keepass v2.29
		 	var ms = new MemoryStream();
			const int wMax = 128;
			const int hMax = 128;
		 	try {
   				 if((img.Width <= wMax) && (img.Height <= hMax))
					img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
				else
				{
					Image imgSc = CompatibilityManager.ResizedImage(img, wMax, hMax);
					imgSc.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
					imgSc.Dispose();
				}
				
				byte[] msByteArray = ms.ToArray();
	            foreach (PwCustomIcon item in kdb.CustomIcons)
	            {
	                // re-use existing custom icon if it's already in the database
	                // (This will probably fail if database is used on 
	                // both 32 bit and 64 bit machines - not sure why...)
	                if (KeePassLib.Utility.MemUtil.ArraysEqual(msByteArray, item.ImageDataPng))
	                {
	                	return item;
	                }
	            }
	
	            // Create a new custom icon for use with this entry
	            var pwci = new PwCustomIcon(new PwUuid(true),
	                ms.ToArray());
	            kdb.CustomIcons.Add(pwci);
	            ms.Close();
				return pwci;
		 	}
		 	catch (Exception e) {
		 		MessageBox.Show("Error while processing custom icon" + System.Environment.NewLine
		 		                + e.Message+ System.Environment.NewLine
		 		                + e.StackTrace, "Error");
		 		ms.Close();
				return null;
		 	}
			
		}	
		
		
		void OnPerformIconActionClick(object sender, EventArgs e)
		{
			switch (cbo_iconActionSelector.SelectedIndex) {
				case 1:
					OnModifyIconClick(sender,e);
					break;
				case 2:
					OnRemoveIconsClick(sender, e);
					break;
				case 3:
					OnDownloadIconClick(sender, e);
					break;
				case 4:
					OnPickIconClick(sender, e);
					break;
				default:
					MessageBox.Show("Pbm");
					Debug.Assert(false);
					break;
			}
		}
		
		void OnIconActionSelectorSelectedIndexChanged(object sender, EventArgs e)
		{
			btn_performIconAction.Enabled = cbo_iconActionSelector.SelectedIndex != 0;
		}
		
		
		#endregion
		
		#region ListView entries action
		void OnLvAllEntriesSelectedIndexChanged(object sender, EventArgs e)
		{
		
			ListView.SelectedListViewItemCollection sItems = m_lvAllEntries.SelectedItems;
			
			m_lvUsedEntries.Items.Clear();
			m_lvUsedGroups.Items.Clear();
	 
			if ((sItems.Count > 0 ) &&
			    (sItems[0].ImageIndex > ((int)PwIcon.Count)) ) {
				PwCustomIcon readIcon;
				readIcon = tco_lists.SelectedIndex == 0 ?
					m_iconIndexer[sItems[0].ImageIndex] : 
					m_iconIndexer[sItems[0].ImageIndex - (int)PwIcon.Count];
				
				Image originalImage = CompatibilityManager.GetOriginalImage(readIcon);
				
				pbo_selectedIcon128.BackgroundImage = 
					CompatibilityManager.ResizedImage(originalImage, 128, 128);
				pbo_selectedIcon64.BackgroundImage = 
					CompatibilityManager.ResizedImage(originalImage, 64, 64);
				pbo_selectedIcon32.BackgroundImage =
					CompatibilityManager.ResizedImage(originalImage, 32, 32);					
				pbo_selectedIcon16.BackgroundImage = 
					CompatibilityManager.ResizedImage(originalImage, 16, 16);
				lbl_originalSize.Text =
					"Original Size : " +
					originalImage.Width +
					" x " +
					originalImage.Height;
				IEnumerator<PwEntry> myEntryEnumerator = m_iconCounter.GetListEntries( readIcon ).GetEnumerator();
				
				// Update entry and group list
				// It is necessary to add all subitems to listView in a single oneshot.
				// On the other case, a runtime exception occurs when the listView is sorted
				// and the sort condition take into account one of the nth column, n > 1
				while (myEntryEnumerator.MoveNext()) {
					PwEntry readEntry = myEntryEnumerator.Current;
					
					var lvi = new ListViewItem( readEntry.Strings.ReadSafe( PwDefs.TitleField ) );
					lvi.SubItems.Add( readEntry.Strings.ReadSafe( PwDefs.UserNameField ) );
					lvi.SubItems.Add( readEntry.Strings.ReadSafe( PwDefs.UrlField ) );
					lvi.SubItems.Add( readEntry.ParentGroup.GetFullPath(".", false) );
					m_lvUsedEntries.Items.Add(lvi);
				}
				
				IEnumerator<PwGroup> myGroupEnumerator = m_iconCounter.GetListGroups( readIcon ).GetEnumerator();
				while (myGroupEnumerator.MoveNext()) {
					PwGroup readGroup = myGroupEnumerator.Current;
					var lvi = new ListViewItem( readGroup.Name );
					lvi.SubItems.Add( readGroup.GetFullPath() );
					m_lvUsedGroups.Items.Add( lvi );
				}
			}
			else {
				pbo_selectedIcon128.BackgroundImage = null;
				pbo_selectedIcon64.BackgroundImage = null;
				pbo_selectedIcon32.BackgroundImage = null;
				pbo_selectedIcon16.BackgroundImage = null;
			}	
		
		}
		
		#endregion
		
		#region Common methods
		private void NotifyDatabaseModificationAndUpdateMainForm() {
			m_PluginHost.Database.Modified = true;
			m_PluginHost.MainWindow.UpdateUI(true, null, false, null, true, null, true);
		}
		
		void OnDetailsCheckedChanged(object sender, EventArgs e)
		{
			m_lvDownloadResult.View = rbu_details.Checked ? 
				View.Details : View.LargeIcon;
		}
		
		void OnDownloadResultSelectedIndexChanged(object sender, EventArgs e)
		{
			if (m_lvViewIcon.SelectedItems.Count <= 0) 
				return;
			
			ListViewItem lvi = m_lvViewIcon.SelectedItems[0];
			var iconUuid = lvi.Tag as PwUuid;
			Debug.Assert(iconUuid != null);
			IconChooser ich = m_iconChooserIndexer[iconUuid];
			if (m_lvDownloadResult.SelectedItems.Count > 0) 
				ich.ChoosenIndex = (Int32)m_lvDownloadResult.SelectedItems[0].Tag;
			else
				ich.ChoosenIndex = 0;
		}
		
		
		#endregion
		
	}
	
	/// <summary>
	/// Class to compare size stored as width x height
	/// compare only Width
	/// </summary>
	public class SizeComparer:BaseSwappableStringComparer
	{
		private enum OperandType { str, num, size };
		
		
		public SizeComparer( bool defaultSwapped )
			:base( defaultSwapped) {
			
		}
		
		public override int Compare( String obj1, String obj2) {
			
			string cmpStr1 = GetString1( obj1, obj2);
			string cmpStr2 = GetString2( obj1, obj2);
			
			var op1 = new Operand( cmpStr1);
			var op2 = new Operand( cmpStr2);
			
			if ( (op1.Ot == OperandType.size)
			    && (op2.Ot == OperandType.size) ) {
				int sum1 = op1.LeftInt + op1.RightInt;
				int sum2 = op2.LeftInt + op2.RightInt;
				return sum1.CompareTo(sum2);
			}
			
			if (op1.Ot == OperandType.size) { return 1;  }
			if (op2.Ot == OperandType.size) { return -1; }
			
			if ( (op1.Ot == OperandType.num)
			    && (op2.Ot == OperandType.num) ) {
				return op1.LeftInt.CompareTo(op2.LeftInt);
			}
			
			if (op1.Ot == OperandType.num) { return 1;  }
			if (op2.Ot == OperandType.num) { return -1; }
			
			// op1.Ot == OperandType.str
			// op2.Ot == OperandType.str
			return string.Compare(op1.InitialString, op2.InitialString, StringComparison.CurrentCulture);
		}
		
		public int Compare2( String obj1, String obj2) {
			
			string cmpStr1 = GetString1( obj1, obj2);
			string cmpStr2 = GetString2( obj1, obj2);
			
			string[] cmpStrInts1 = cmpStr1.Split('x');
			string[] cmpStrInts2 = cmpStr2.Split('x');
			
			string cmpStrInt1_1 = cmpStrInts1[0].Trim();
			string cmpStrInt2_1 = cmpStrInts2[0].Trim();
			
			// One string is empty or starts with x
			if (string.IsNullOrEmpty(cmpStrInt2_1)) {
				return 1;
			} else if (string.IsNullOrEmpty(cmpStrInt1_1)) {
				return -1;
			}
			
			// One string does not have right operator
			if (cmpStrInts2.Length <= 1) { return 1; }
			string cmpStrInt2_2 = cmpStrInts2[1].Trim();
			if (cmpStrInts1.Length <= 1) { return -1; }
			string cmpStrInt1_2 = cmpStrInts1[1].Trim();
			
			
			// Parse left operator. If not parsable, not parsable string is the first
			bool parseOK;
			int iComp2_1;	parseOK = int.TryParse(cmpStrInt2_1, out iComp2_1);
			if (!parseOK) {	return 1; }
			int iComp1_1;   parseOK = int.TryParse(cmpStrInt1_1, out iComp1_1);
			if (!parseOK) {	return -1; }
			
			// Parse right operator.
			int iComp2_2;	parseOK = int.TryParse(cmpStrInt2_2, out iComp2_2);
			if (!parseOK) {	return 1; }
			int iComp1_2;   parseOK = int.TryParse(cmpStrInt1_2, out iComp1_2);
			if (!parseOK) {	return -1; }
			int sum1 = iComp1_1 + iComp1_2;
			int sum2 = iComp2_1 + iComp2_2;
			if ( sum1 > sum2 ) { return 1; }
			if ( sum1 < sum2 ) { return -1; }
			
			return 0; // sums are equals
		}
		
		public static string basicTest() {
			var sc = new SizeComparer(false);
			string result = "";
			string nl = System.Environment.NewLine;
			result += "  str1        str2         expected   effective" + nl;
			result += nl;
			result += " Nominal tests" + nl;
			result += " 10 x 10       10 x 10         0        " + sc.Compare("10 x 10", "10 x 10") + nl;
			result += " 20 x 20       10 x 10         1        " + sc.Compare("20 x 20", "10 x 10") + nl;
			result += " 10 x 10       20 x 20        -1        " + sc.Compare("10 x 10", "10  x 20") + nl;
			result += " 10 x 10       100 x 100      -1        " + sc.Compare("10 x 10", "100 x 100") + nl;
			result += nl;
			result += " Asymmetry" + nl;
			result += " 109 x 10      101 x 9         0        " + sc.Compare("109 x 01", "101 x 9") + nl;
			result += " 10 x 10       10 x 11        -1        " + sc.Compare("10 x 10", "10 x 11") + nl;
			result += nl;
			result += " Only Numbers" + nl;
			result += " 10            20             -1        " + sc.Compare("10", "20") + nl;
			result += " 20            10              1        " + sc.Compare("20", "10") + nl;
			result += " 01            100            -1        " + sc.Compare("01", "100") + nl;
			result += " 100           01              1        " + sc.Compare("100", "01") + nl;
			result += nl;
			result += " Exceptional" + nl;
			result += " 10 x 10       10   x 10       0        " + sc.Compare("10 x 10", "10   x 10") + nl;
			result += " 10 x 10       10              1        " + sc.Compare("10 x 10", "10") + nl;
			result += " 10            10 x 10        -1        " + sc.Compare("10", "10 x 10") + nl;
			result += nl;
			result += " Compare with string" + nl;
			result += " abcd          10 x 10        -1        " + sc.Compare("abcd", "10 x 10")  + nl;
			result += " 10 x 10       abcd            1        " + sc.Compare("10 x 10", "abcd") + nl;
			result += " abcd          defg           -1        " + sc.Compare("abcd", "defg") + nl;
			result += " defg          abcd            1        " + sc.Compare("defg", "abcd") + nl;
			result += " abxcd         defg           -1        " + sc.Compare("abxcd", "defg") + nl;
			result += " defg          abxcd           1        " + sc.Compare("defg", "abxcd") + nl;
			result += " dexfg         abxcd           1        " + sc.Compare("dexfg", "abxcd") + nl;
			return result;
		}
		
		private class Operand {
			
			// Properties
			public String InitialString {get; private set;}
			public OperandType Ot {get; private set; }
			public int LeftInt {get; private set;}
			public int RightInt {get; private set;}
			
			
			public Operand(string str) {
				InitialString = str.Trim();
				computeType();
			}
			
			private void computeType() {
				
				if (string.IsNullOrEmpty( this.InitialString )) {
					Ot = OperandType.str;
					return;
				}
				
				string[] strs = this.InitialString.Split('x');
				
				if (strs.Length > 2) {
					Ot = OperandType.str;
					return;
				}
				
				if (strs.Length == 1) {
					int intResult;
					bool parseOK = int.TryParse( strs[0], out intResult );
					if (parseOK) {
						Ot = OperandType.num;
						this.LeftInt = intResult;
						return;
					}
					else {
						Ot = OperandType.str;
						return;
					}
				}
				
				if (strs.Length == 2) {
					int intResult1;
					bool parseOK1 = int.TryParse( strs[0], out intResult1 );
					int intResult2;
					bool parseOK2 = int.TryParse( strs[1], out intResult2 );
					if (parseOK1 && parseOK2) {
						this.LeftInt  = intResult1;
						this.RightInt = intResult2;
						Ot = OperandType.size;
					}
					else {
						Ot = OperandType.str;
					}
				}
			}
		}
			
		
	}
}
