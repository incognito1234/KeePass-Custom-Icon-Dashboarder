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
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;
using System.Net;

using KeePass.UI;
using KeePass.Plugins;
using KeePass.Forms;

using KeePassLib;

using LomsonLib.UI;
using LomsonLib.Utility;

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
		private Dictionary<PwUuid, BestIconFinder> m_bestIconFindersIndexer;

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
			BestIconFinder.InitClass();
			InitEx();		
		}
		
		private void OnFormDispose()
		{
			BestIconFinder.DisposeClass();
		}
		
		private void InitEx()
		{
			m_iconCounter = new IconStatsHandler();
			m_iconCounter.Initialize( m_PluginHost.Database);
			
			int cx = CompatibilityManager.ScaleIntX(16);
			int cy = CompatibilityManager.ScaleIntX(16);
			m_ilCustoms = UIUtil.BuildImageList(m_PluginHost.Database.CustomIcons, cx, cy);
			m_bestIconFindersIndexer = new Dictionary<PwUuid, BestIconFinder>();
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
			m_lvViewIcon.Columns.Add( Resource.hdr_currentSize, 50, HorizontalAlignment.Center );
			m_lvViewIcon.Columns.Add( Resource.hdr_newSize, 50, HorizontalAlignment.Center );
			m_lvViewIcon.Columns.Add( Resource.hdr_nEntry, 50, HorizontalAlignment.Center);
			m_lvViewIcon.Columns.Add( Resource.hdr_nGroup, 50, HorizontalAlignment.Center);
			m_lvViewIcon.Columns.Add( Resource.hdr_nTotal, 50, HorizontalAlignment.Center);
			m_lvViewIcon.Columns.Add( Resource.hdr_nURL, 50, HorizontalAlignment.Center );
			m_lvIconsColumnSorter = new ListViewLayoutManager();
			
			m_lvIconsColumnSorter.AddColumnComparer(0, new IntegerAsStringComparer(false));
			m_lvIconsColumnSorter.AddColumnComparer(1, new LomsonLib.UI.StringComparer(false,true));
			m_lvIconsColumnSorter.AddColumnComparer(2, new LomsonLib.UI.StringComparer(false,true));
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
				UpdateBestIconFinderResultForLvi(lvi);
				
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
			
			int cx = CompatibilityManager.ScaleIntX(16);
			int cy = CompatibilityManager.ScaleIntX(16);
			m_ilCustoms = UIUtil.BuildImageList(m_PluginHost.Database.CustomIcons, cx, cy);
		
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
			UpdateFormFromSelectedIcon();
			UpdateFormFromBestIconFinder();
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
		
		void OnDownloadClick(object sender, EventArgs e)
		{
			ListView.CheckedListViewItemCollection lvsiChecked = m_lvViewIcon.CheckedItems;
			foreach(ListViewItem lvi in lvsiChecked)
			{
				if (UpdateBestIconFinderAndLviFromListViewItem( lvi )) {
				    System.Threading.Thread.Sleep(500);
				}
			   lvi.EnsureVisible();
			   m_lvViewIcon.Refresh();
			}
			
			UpdateFormFromBestIconFinder();
		
		}
		
		/// <summary>
		/// Update bestIconFinder and size in List View Item
		/// if icon has not already been downloaded
		/// </summary>
		/// <param name="lvi"></param>
		/// <returns>true if an update has occured. false else</returns>
		private bool UpdateBestIconFinderAndLviFromListViewItem(ListViewItem lvi) {
			PwCustomIcon readIcon = m_iconIndexer[lvi.ImageIndex];
			if ( !m_bestIconFindersIndexer.ContainsKey( readIcon.Uuid ) ) {
				List<Uri> lstUris = new List<Uri>();
				lstUris.AddRange( m_iconCounter.GetListUris( readIcon ) );
				
				// Retrieve images
				if (lstUris.Count > 0) {
					Uri uri1 = lstUris[0];
					
					BestIconFinder bif = new BestIconFinder( uri1 );
				    bif.FindBestIcon();
				    
				    m_bestIconFindersIndexer.Add( readIcon.Uuid, bif);
				    UpdateBestIconFinderResultForLvi( lvi);
				}
				return true;
			}
			return false;
		}
		
		private void UpdateBestIconFinderResultForLvi( ListViewItem lvi) {
			BestIconFinder bif = null;
			PwCustomIcon readIcon = m_iconIndexer[lvi.ImageIndex];
			if (m_bestIconFindersIndexer.ContainsKey(readIcon.Uuid)) {
				bif = m_bestIconFindersIndexer[readIcon.Uuid];
			}
			else {
				lvi.SubItems[2].Text = "";
				return;
			}
			if (bif.BestImage != null) {
		    	lvi.SubItems[2].Text = bif.BestImage.Width.ToString(NumberFormatInfo.InvariantInfo)
                + " x " +
               bif.BestImage.Height.ToString(NumberFormatInfo.InvariantInfo);
		    }
	    	else {
		    	lvi.SubItems[2].Text = Resource.val_na;
	    	}
		}
		
		void UpdateFormFromSelectedIcon()
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
				pbo_selectedIcon128.Image = null;
			}	
		}
		
		void UpdateFormFromBestIconFinder() 
		{
			ListView.SelectedListViewItemCollection sItems = m_lvViewIcon.SelectedItems;
			if (sItems.Count > 0) {
				ListViewItem lvi = sItems[0];
				
				PwUuid pu = (PwUuid)lvi.Tag;
				bool cleanImage = false;
				
				if (m_bestIconFindersIndexer.ContainsKey(pu)) {
					BestIconFinder bif = m_bestIconFindersIndexer[pu];
					 
					if (bif.BestImage != null) {
						pbo_downloadedIcon128.BackgroundImage = 
							CompatibilityManager.ResizedImage(bif.BestImage, 128, 128);
						pbo_downloadedIcon64.BackgroundImage = 
							CompatibilityManager.ResizedImage(bif.BestImage, 64, 64);
						pbo_downloadedIcon32.BackgroundImage =
							CompatibilityManager.ResizedImage(bif.BestImage, 32, 32);					
						pbo_downloadedIcon16.BackgroundImage = 
							CompatibilityManager.ResizedImage(bif.BestImage, 16, 16);
						lbl_newSize.Text =
							"New Size : " +
							bif.BestImage.Width +
							" x " +
							bif.BestImage.Height;
					}
					else {
						cleanImage = true;
					}
				    
				}
				else {
					cleanImage = true;
				}
				
				if (cleanImage) {
					pbo_downloadedIcon128.BackgroundImage = null;
					pbo_downloadedIcon64.BackgroundImage = null;
					pbo_downloadedIcon32.BackgroundImage = null;
					pbo_downloadedIcon16.BackgroundImage = null;
					lbl_newSize.Text = "New Size :";
				}
			}
		}
		
		void OnPickClick(object sender, EventArgs e)
		{
			ListView.CheckedListViewItemCollection lvsiChecked = m_lvViewIcon.CheckedItems;
						
			foreach(ListViewItem lvi in lvsiChecked) {
				UpdateBestIconFinderAndLviFromListViewItem( lvi );
				PwCustomIcon readIcon = m_iconIndexer[lvi.ImageIndex];
				
				Debug.Assert(m_bestIconFindersIndexer.ContainsKey( readIcon.Uuid ));
				
				BestIconFinder bif = m_bestIconFindersIndexer[readIcon.Uuid];
				if (bif.BestImage != null) {
					PwCustomIcon newIcon = UpdateCustomIconFromImage( readIcon, bif.BestImage, m_PluginHost.Database);
					if ((newIcon != null ) && (!m_bestIconFindersIndexer.ContainsKey(newIcon.Uuid)) ) {
						m_bestIconFindersIndexer.Add(newIcon.Uuid, bif);
						UpdateBestIconFinderResultForLvi(lvi);
					}
					m_PluginHost.Database.Modified = true;
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
		 	MemoryStream ms = new MemoryStream();
			const int wMax = 128;
			const int hMax = 128;
		 	try {
   				 if((img.Width <= wMax) && (img.Height <= hMax))
					img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
				else
				{
					Image imgSc = KeePassLib.Utility.GfxUtil.ScaleImage(img, wMax, hMax);
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
	            PwCustomIcon pwci = new PwCustomIcon(new PwUuid(true),
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
		
	}
	
}
