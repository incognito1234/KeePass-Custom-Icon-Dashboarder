/*
  LomsonLib - Library to help management of various object types in program 
  developed in c#.
  
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

/*
 * LomsonLib.UI
 * 
 *    Version 1.3 - 2015/04/20
 *      Add Compatibility Manager
 *      Add ScaleControlTableLayoutManager
 * 
 *    Version 1.2.1
 *      Implement some FxCop considerations
 * 
 *    Version 1.2 - 2014/07/07
 *        - Add Contextmenu to check and invert checkboxes of selected items
 *        - Take into account multichecking
 * 
 *    Version 1.1 - 2014/07/04
 *       - Add statistics in status bar
 *       - Add checkBox HeaderStyle
 *       - Add RTF Builder
 *       - Bug Fixes
 * 
 *    Version 1.0 - 2014/06/25
 *       - Initial Version
 * 
 */
 
using System;
using System.Collections;	
using System.Collections.Generic;

using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;

using KeePass.Util;
using KeePass.UI;

using LomsonLib.LomsonDebug;

namespace LomsonLib.UI
{
	/// <summary>
	/// Sorter for List.
	/// </summary>
	public sealed class ListViewLayoutManager : IComparer, IDisposable
	{
		public String Name { get; set; }
		private ListView m_lvi;		
		
		#region Building, Initialization and Dispose
		
		// Initialize
		public ListViewLayoutManager()
		{			
			InitializeComparer();
		}
		
		
		public void ApplyToListView(ListView lvi) {
			
			Debug.Assert( m_lvi == null );
			lvi.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.OnLvViewIconInvertOrderColumnClick);
			lvi.ListViewItemSorter = this;
			
			m_lvi = lvi;
			
			if ( this.AutoWidthColumn ) {
				MakeColumnsAutoSized();
			}
			
			if ( this.CheckAllCheckBox != null ) {
				AttachCheckAllCheckBox();
			}
			
			if (this.SelectMenuToolStripItemsToBeAdded != null ) {
				ApplySelectedMenuItems();
			}
			
			if (this.ManageMultiChecking) {
				InitializeManageMultiChecking();
			}
			
			if (StatisticIsApplicable()) {
				ApplyStatisticMessageUpdater();
			}
			
//			if (this.m_boCheckboxInHeader) {
//				#if DEBUG
//				if (! m_lvi.OwnerDraw) {
//					Debug.Assert(false,"ListViewLayoutManager.ApplyToListView is invoked on a ListView with no OwnerDraw. Checkbox in Header will not been applied");
//				}
//				#endif
//				m_lvi.DrawColumnHeader += new DrawListViewColumnHeaderEventHandler( OnLvColumnHeader_DrawColumnHeader );
//				m_lvi.DrawItem += new DrawListViewItemEventHandler(OnLvColumnHeader_DrawItem);
//				m_lvi.DrawSubItem += new DrawListViewSubItemEventHandler(OnLvColumnHeader_DrawSubItem);
//				
//			}
		}
		
		private bool m_boItemCheckedEnabled;
		public void RemoveCheckedItemEvent() {
			Debug.Assert(m_lvi != null, "RemoveOnItemCheckedEvent::lvi is null");
			//Debug.Assert(!m_boItemCheckedEnabled, "RemoveOnItemCheckedEvent::m_boItemCheckedEnabled is false");
			if (m_boItemCheckedEnabled) {
				if (!ManageMultiChecking) {
					m_lvi.ItemChecked -= OnItemChecked;
				}
				m_boItemCheckedEnabled = false;	
			}
			RemoveMultiCheckingManagement();
		}
		
		public void RestoreCheckedItemEvent() {
			Debug.Assert(m_lvi != null, "AssignOnItemCheckedEvent;;lvi is null");
			//Debug.Assert(m_boItemCheckedEnabled, "RemoveOnItemCheckedEvent::m_boItemCheckedEnabled is true");
			if (!m_boItemCheckedEnabled)  {
				if (!ManageMultiChecking) {
					m_lvi.ItemChecked += OnItemChecked;
				}
				m_boItemCheckedEnabled = true;
			}
			RestoreMultiCheckingManagement();
		}
		
		public void Dispose() {
			DisposeSelectedContextMenu();
		}
		#endregion
		
		#region Check All CheckBox
			
		private CheckBox m_cbCheckAllCheckBox;
		public CheckBox CheckAllCheckBox {
			get {
				return m_cbCheckAllCheckBox;
			}
			set {
				m_cbCheckAllCheckBox = value;
				if ((m_lvi != null) && (m_cbCheckAllCheckBox == null)) {
					DetachCheckAllCheckBox();
				}
				if ((m_lvi != null) && (m_cbCheckAllCheckBox != null )) {
					AttachCheckAllCheckBox();
				}
				//m_cbAllCheck
			}
		}
		
		private ItemCheckEventHandler[] m_eventToDisableWhenCheckStatusChanges = {}; // Null pattern
		private dlgToBeRaiseAfterCheckStatusChanges m_methodToBeRaisedAfterCheckStatusChanges;
		public delegate void dlgToBeRaiseAfterCheckStatusChanges();
		
		/// <summary>
		/// Define behavior of checkbox status before and after update
		/// </summary>
		/// <param name="eventToBeDisabled">Event that will be disabled before checked/unchecked CheckBox. Can be <b>null</b> </param>
		/// <param name="method">Method that will be launched after checking/unchecking the CheckBox.</param>
		public void DefineCheckBoxEventBehavior(ItemCheckEventHandler[] eventToBeDisabled, dlgToBeRaiseAfterCheckStatusChanges method) {
			if (eventToBeDisabled != null) {
				m_eventToDisableWhenCheckStatusChanges = eventToBeDisabled;
			}
			m_methodToBeRaisedAfterCheckStatusChanges = method;
		}
		
		/// <summary>
		/// Update checkall checkbox status
		/// </summary>
		/// <param name="updateAllCheckboxes">is <b>true</b> if checkboxes of item will be updated</param>
		private void UpdateCheckAllCheckBox(bool updateCheckboxesItem) {
			if (m_lvi == null) { Debug.Assert(false); throw new InvalidOperationException("UpdateSelectAllCheckbox::SelectAllCheckBox is null"); }
			
			if ( CheckAllCheckBox == null) return;
			
			int  i = 0;
			bool allChecked   = true;
			bool allNotChecked = true;
			while ( (i < m_lvi.Items.Count)
			       &&  ( allChecked || allNotChecked ) ) {
				allChecked    &= m_lvi.Items[i].Checked;
				allNotChecked &= !m_lvi.Items[i].Checked;
				i++;
			}
			
			if ( !updateCheckboxesItem) {
				DetachCheckAllCheckBox();
			}
			
			if (allChecked) {
				CheckAllCheckBox.CheckState = CheckState.Checked;
			}
			else if (allNotChecked) {
				CheckAllCheckBox.CheckState = CheckState.Unchecked;
			}
			else {
				CheckAllCheckBox.CheckState = CheckState.Indeterminate;
			}
			if ( !updateCheckboxesItem)
				AttachCheckAllCheckBox();
		}
		
		
		/// <summary>
		/// Update CheckState of individual element from CheckState of Checkall checkbox
		/// </summary>
		/// <param name="simulation">is <b>true</b>, event handle not be removed/added and after method will not be launched</param>
		public void UpdateCheckedStateOfItemsFromCheckAllCheckBox(bool simulation) {
			if (m_lvi == null) { Debug.Assert(false); throw new InvalidOperationException("UpdateSelectAllCheckbox::SelectAllCheckBox is null"); }
			
			if ( CheckAllCheckBox == null) return;
			
			// Disable event before updating everything
			if (!simulation) {
				foreach (ItemCheckEventHandler eh in m_eventToDisableWhenCheckStatusChanges) {
					m_lvi.ItemCheck -= eh;
				}
			}
			if (ManageMultiChecking) {
				m_lvi.ItemCheck -= MultiCheckOnItemCheck;
			}
			
			// Update check status
			if ( CheckAllCheckBox.CheckState == CheckState.Checked ) {
				RemoveCheckedItemEvent();
				foreach (ListViewItem lvi in m_lvi.Items) {
					lvi.Checked = true;
				}
				RestoreCheckedItemEvent();
				UpdateStatistics();
			}
			else if ( CheckAllCheckBox.CheckState == CheckState.Unchecked ) {
				RemoveCheckedItemEvent();
				foreach (ListViewItem lvi in m_lvi.Items) {
					lvi.Checked = false;
				}
				RestoreCheckedItemEvent();
				UpdateStatistics();
			} else { Debug.Assert(false);} // should not occur 
			
			// Enable event before updating everything
			if (!simulation) {
				foreach (ItemCheckEventHandler eh in m_eventToDisableWhenCheckStatusChanges) {
					m_lvi.ItemCheck += eh;
				}
			}
			if (ManageMultiChecking) {
				m_lvi.ItemCheck -= MultiCheckOnItemCheck;
				m_lvi.ItemCheck += MultiCheckOnItemCheck;
			}
			
			// Run after methods
			if ((m_methodToBeRaisedAfterCheckStatusChanges != null) && (!simulation)) {
				m_methodToBeRaisedAfterCheckStatusChanges();
			}
			
		}
		
		/// <summary>
		/// If layout manager is attached to a list, attach "all check" checkbox. Otherwise do noting.
		/// </summary>
		/// <returns><c>true</c> is checkbox was attached. Else <c>false</c></returns>
		private bool m_boCheckAllCheckBoxIsAttached;
		public bool AttachCheckAllCheckBox()
		{
			if ( (m_lvi != null) && ( m_cbCheckAllCheckBox != null ) ) {
				
				if (!m_boItemCheckedEnabled) {
					RestoreCheckedItemEvent();
				}
				if (!m_boCheckAllCheckBoxIsAttached) {
					m_cbCheckAllCheckBox.CheckStateChanged += OnCheckStateChange;
					m_boCheckAllCheckBoxIsAttached = true;
				}
				return true;
			}
			//else {
				return false;
			
		}
		
		/// <summary>
		/// If layout manager is attached to a list, attach "all check" checkbox. Otherwise do noting.
		/// </summary>
		/// <returns><c>true</c> is checkbox was detached. Else <c>false</c></returns>
		public bool DetachCheckAllCheckBox()
		{
			if ( (m_lvi != null) && ( m_cbCheckAllCheckBox != null ) ) {			
				RemoveCheckedItemEvent();
				
				m_cbCheckAllCheckBox.CheckStateChanged -= OnCheckStateChange;
				m_boCheckAllCheckBoxIsAttached = false;
				
				return true;
			}
			// else {
			return false;
		}
		
		private void OnCheckStateChange( Object sender, EventArgs e)
		{
			UpdateCheckedStateOfItemsFromCheckAllCheckBox(false);
		}
		
		#endregion
		
		#region Check Selected Menu Item
		private ICollection<ToolStripItem> SelectMenuToolStripItemsToBeAdded { get; set; }
		private bool m_SelectedmenuItemAlreadyAssigned;
		
		/// <summary>
		/// Add menu items Check/Uncheck checkboxes and Invert Checkboxes to contextual menu of Listview.
		/// </summary>
		/// <param name="menuItemCheckUncheck">Menu item text for Check/Unselected feature.
		/// <c>null</c> if not needed</param>
		/// <param name="menuItemInvertChecked">Menu item text for Invert checkboxes feature.
		/// <c>null</c> if not needed</param>
		/// <returns></returns>
		public void AddCheckSelectedContextMenu(String menuItemCheckUncheck, String menuItemInvertCheckBoxes)
		{
			if ( (menuItemCheckUncheck == null) && (menuItemInvertCheckBoxes == null ) ) { return; }
			
			SelectMenuToolStripItemsToBeAdded = new List<ToolStripItem>();
			
			if (menuItemCheckUncheck != null) {
				ToolStripItem tsi1 = new ToolStripMenuItem(
					menuItemCheckUncheck, null, new System.EventHandler(OnCheckUncheckboxesClick));
				SelectMenuToolStripItemsToBeAdded.Add(tsi1);
			}
			if (menuItemInvertCheckBoxes != null) {
				ToolStripItem tsi2 = new ToolStripMenuItem(menuItemInvertCheckBoxes,
				                                           null, new System.EventHandler(OnInvertCheckboxesClick));
				SelectMenuToolStripItemsToBeAdded.Add(tsi2);
			}		
			
		}
			
		public void ApplySelectedMenuItems() {
			Debug.Assert(m_lvi != null);
			
			if (m_SelectedmenuItemAlreadyAssigned) {
				Debug.Assert(false, "Menu Item checkboxes has already been assigned for listView");
				return;
			}
			
			if (SelectMenuToolStripItemsToBeAdded != null) {
				ContextMenuStrip cms;
				if (m_lvi.ContextMenuStrip != null) {
					cms = m_lvi.ContextMenuStrip;
				}
				else {
					cms = new ContextMenuStrip();
					m_lvi.ContextMenuStrip = cms;
				}
				foreach (ToolStripItem tsi in SelectMenuToolStripItemsToBeAdded) {
					cms.Items.Add(tsi);
				}
			}
			
			m_SelectedmenuItemAlreadyAssigned = true;
			
		}
		
		// Event Handler
        private void OnCheckUncheckboxesClick(object sender, EventArgs e)
        {
        	if (m_lvi.SelectedItems.Count <= 0) return;
        	
        	bool atLeastOneUnchecked = false;
			int i = 0;
			while (   (i < m_lvi.SelectedItems.Count)
			       && ( !atLeastOneUnchecked ) ) {
				atLeastOneUnchecked = !m_lvi.SelectedItems[i].Checked;
				i++;
			}
			
			if (atLeastOneUnchecked) {
				// Check all
				foreach( ListViewItem lvi in m_lvi.SelectedItems ) {
					lvi.Checked = true;
				}
			}
			else {
				// Uncheck all
				foreach( ListViewItem lvi in m_lvi.SelectedItems ) {
					lvi.Checked = false;
				}
			}
        }
		
        // Event Handler
        private void OnInvertCheckboxesClick(object sender, EventArgs e)
        {
        	if (m_lvi.SelectedItems.Count <= 0) return;
        	
    		foreach( ListViewItem lvi in m_lvi.SelectedItems ) {
				lvi.Checked = !lvi.Checked;
			}
		
        }
        
        private void DisposeSelectedContextMenu() {
        	if (SelectMenuToolStripItemsToBeAdded != null) {
        		foreach (ToolStripMenuItem tsmi in SelectMenuToolStripItemsToBeAdded) {
        			tsmi.Dispose();
        		}
        	}
        }
		#endregion
		
		#region Multi Checking Checkboxes
		public delegate void dlgMultiCheckingCheckBoxes( IList<ListViewItem> lst);
		
		private dlgMultiCheckingCheckBoxes m_launchOnItemChecked;
		private bool ManageMultiChecking {get; set; }
		
		private List<ListViewItem> m_itemsAboutToBeChanged;
		
		private void InitializeManageMultiChecking() {
			Debug.Assert(m_lvi != null);
			if (ManageMultiChecking) {
				m_lvi.ItemCheck -= MultiCheckOnItemCheck;
				m_lvi.ItemCheck += MultiCheckOnItemCheck;
			}
		}
		
		private void RemoveMultiCheckingManagement() {
			if (ManageMultiChecking) {
				m_lvi.ItemCheck -= MultiCheckOnItemCheck;
			}
		}
		
		private void RestoreMultiCheckingManagement() {
			if (ManageMultiChecking) {
				m_lvi.ItemCheck -= MultiCheckOnItemCheck;
				m_lvi.ItemCheck += MultiCheckOnItemCheck;
			}
		}
		
		/// <summary>
		/// If multichecking management is enabled and a checkbox status is changed for
		///  several items, the event launchOnItemChecked will be launched after
		///  item will be Checked or Unchecked.
		/// When multichecking management is enabled, it is not recommended to subscribe
		///  to event "ItemChecked" without using ListViewLayoutManager.
		/// </summary>
		public void DefineMultiCheckingBehavior( dlgMultiCheckingCheckBoxes launchOnItemChecked ) {
			m_launchOnItemChecked = launchOnItemChecked;
		}
		
		public void EnableMultiCheckingControl() {
			ManageMultiChecking = true;
		}
		
		public void DisableMultiCheckingControl() {
			ManageMultiChecking = false;
		}
		
		private bool m_multiCheckingIsStarted;
		private int  m_countMultiChecking;
		
		private void MultiCheckOnItemCheck(object sender, ItemCheckEventArgs a) {
		
			if ( (m_lvi.SelectedItems.Count > 1) &&
			    (m_lvi.SelectedIndices.Contains(a.Index))) {
				
				// MultiCheck
				
				bool newCheckedValue = a.NewValue == CheckState.Checked;
				
				if ( ! m_multiCheckingIsStarted ) {
					
					m_multiCheckingIsStarted = true;
					
					// Initialize multichecking
					m_itemsAboutToBeChanged = new List<ListViewItem>();
						
					// Count number of "OnItemCheck" to be launched until it will be re-enable
					m_countMultiChecking = 0;
					foreach (ListViewItem lvi in m_lvi.SelectedItems) {
						if (lvi.Checked != newCheckedValue) {
							m_countMultiChecking++;
							m_itemsAboutToBeChanged.Add(lvi);
						}
						
					}
					
				}
				
				m_countMultiChecking--;
				
				if (m_countMultiChecking <= 0 ) {
					RemoveCheckedItemEvent();
					m_lvi.ItemChecked += ItemCheckedMultiCheckedEnd;
				}
				
				
			}
			else {
				m_lvi.ItemChecked += ItemCheckedOneCheckedEnd;
			}
		}
		
		// Event Handler
		private void ItemCheckedMultiCheckedEnd(object sender, ItemCheckedEventArgs e) {
			m_lvi.ItemChecked -= ItemCheckedMultiCheckedEnd;
			
			m_launchOnItemChecked(m_itemsAboutToBeChanged);
			
			// Restore events
			RestoreCheckedItemEvent();
			OnItemChecked(sender, e);
			m_multiCheckingIsStarted = false;
			
		}
		
		private void ItemCheckedOneCheckedEnd(object sender, ItemCheckedEventArgs e) {
			m_lvi.ItemChecked -= ItemCheckedOneCheckedEnd;
			RemoveCheckedItemEvent();
			List<ListViewItem> lstWithNewStatus = new List<ListViewItem>();

			lstWithNewStatus.Add( e.Item );
			
			m_launchOnItemChecked( lstWithNewStatus );
			
			RestoreCheckedItemEvent();
			OnItemChecked(sender, e);
		}
		
		#endregion
		
		#region Common Events
		private void OnItemChecked( Object sender, ItemCheckedEventArgs e)
		{
			UpdateCheckAllCheckBox(false);
			UpdateStatistics();
		}
		
		private void OnSelectedIndexChanged(Object sender, EventArgs e)
		{
			UpdateStatistics();
		}
		
		#endregion
		
		#region Comparer and Sorter
		
		private bool                                     m_SortIsSuspended; // Use for suspending sort
		private SortOrder                                m_lastSortOrder; 
		private ISwappableStringComparer                 m_CurrentComparer;
		private List<ISwappableComparer<ListViewItem>>   m_DefaultComparers;		
		private Dictionary<int,ISwappableStringComparer> m_ColumnComparers; 		

		
		private void InitializeComparer() {
			m_CurrentSortedColumn = 0;
			CurrentSortOrder    = SortOrder.None;
			m_ColumnComparers = new Dictionary<int,ISwappableStringComparer>();
			m_DefaultComparers = new List<ISwappableComparer<ListViewItem>>();
			m_SortIsSuspended = false;
		}
		
		/// <summary>
		/// Current Sort Order. If null, default sort is applied
		/// </summary>
		private SortOrder m_CurrentSortOrder;
		public SortOrder  CurrentSortOrder {
			
			get {
				return m_CurrentSortOrder;
			}
			
			set {
				if (!m_SortIsSuspended) {
					if (value == SortOrder.Ascending) {
						Debug.Assert( m_ColumnComparers.ContainsKey( CurrentSortedColumn ) );
						m_ColumnComparers[CurrentSortedColumn].RevertSwapToDefault();
						m_CurrentComparer = m_ColumnComparers[CurrentSortedColumn];
					}
					else if (value == SortOrder.Descending) {
						Debug.Assert( m_ColumnComparers.ContainsKey( CurrentSortedColumn ) );
						m_ColumnComparers[CurrentSortedColumn].RevertSwapToDefault();
						m_ColumnComparers[CurrentSortedColumn].Swap();
						m_CurrentComparer = m_ColumnComparers[CurrentSortedColumn];
					}
					else { // value = SortOrder.None
						m_CurrentComparer = null;
					}
					
					m_CurrentSortOrder = value;
				}
				else {
					Debug.Assert(false, "SortOrder is changed whereas Sort is suspended");
				}
			}
		}
		
		private int m_CurrentSortedColumn;
		public  int CurrentSortedColumn {
			get {
				return m_CurrentSortedColumn;
			}
			set {
				Debug.Assert( m_ColumnComparers.ContainsKey( value ), "Change sorted column while compared does not exist" );
				m_CurrentSortedColumn = value; 
				m_CurrentComparer = m_ColumnComparers[value];
			}
		}
		
		public void SuspendSort() {
			Debug.Assert(m_DefaultComparers != null, "SuspendSort::Sort is suspended whereas Ordercolumn is not set");
			Debug.Assert(!m_SortIsSuspended, "SuspendSort::Listview is already suspended");
			
			m_lastSortOrder = CurrentSortOrder;
			CurrentSortOrder = SortOrder.None;
			m_SortIsSuspended = true; // must be after assignment of CurrentSortOrder
			
		}
		
		public void ResumeSort() {
			Debug.Assert(m_SortIsSuspended, "ResumeSort::Resume sort whereas Suspend sort has never been launched");
			
			if (!m_SortIsSuspended) return;
			
			m_SortIsSuspended = false; // must be before assignment of CurrentSortOrder
			CurrentSortOrder = m_lastSortOrder;			
		}
		
		public void AddColumnComparer(int nColumn, ISwappableStringComparer comp)
		{
			Debug.Assert( !m_ColumnComparers.ContainsKey( nColumn ) );
			if (m_ColumnComparers.ContainsKey( nColumn )) throw new InvalidOperationException("addColumn::Column comparer already exists");
			
			m_ColumnComparers.Add(nColumn,comp);
		}
		
		// Initialize Default
		public void AddDefaultSortedColumn( int nColumn, bool defaultSwapped )
		{
			Debug.Assert( m_ColumnComparers.ContainsKey( nColumn ) );
			
			OrdererColumnComparer occ = 
				new OrdererColumnComparer(nColumn, defaultSwapped, m_ColumnComparers[ nColumn ] );
			
			m_DefaultComparers.Add( occ );
		}
		
		public void AddDefaultComparer( ISwappableComparer<ListViewItem> comparer ) {
			m_DefaultComparers.Add(comparer);
		}
	
		// Compare
		public int Compare(object x, object y)
		{
			ListViewItem lvX = x as ListViewItem;
			ListViewItem lvY = y as ListViewItem;
			
			Debug.Assert( (lvX != null) && (lvY != null) );
			
			if (CurrentSortOrder == SortOrder.None) {
				if (m_DefaultComparers.Count > 0)
					return DefaultCompare( lvX, lvY);
				else
					return 0;
			}
			else {
				String str1 = lvX.SubItems[m_CurrentSortedColumn].Text;
				String str2 = lvY.SubItems[m_CurrentSortedColumn].Text;
				
				return m_CurrentComparer.Compare(str1, str2);
			}
		}
		
		private int DefaultCompare(ListViewItem lvi1, ListViewItem lvi2)
		{
			Debug.Assert( m_DefaultComparers.Count > 0);
			
			ISwappableComparer<ListViewItem> readComparer;
			int result = 0;
			int iKey = 0;
			while ( (iKey < m_DefaultComparers.Count ) && (result == 0 ) ) {
				readComparer = m_DefaultComparers[iKey];
				
				result = readComparer.Compare( lvi1, lvi2);
				iKey++;
					
			}
			
			return result;
		}
		
		private class OrdererColumnComparer: BaseSwappableComparer<ListViewItem> {
			
			private int ColumnNumber { get; set; }
			private ISwappableStringComparer Comparer{get; set; }
			
			public OrdererColumnComparer(int nColumn, bool defaultSwapped, ISwappableStringComparer issc):base(defaultSwapped) {
				ColumnNumber = nColumn;
				Comparer = issc;
				Comparer.RevertSwapToDefault();
				if (defaultSwapped) Comparer.Swap();
			}
			
			public override int Compare(ListViewItem lvi1, ListViewItem lvi2){
				
				Comparer.RevertSwapToDefault();
				
				String str1 = lvi1.SubItems[ColumnNumber].Text;
				String str2 = lvi2.SubItems[ColumnNumber].Text;
				return Comparer.Compare( str1, str2);

			}
		}
		
		private void OnLvViewIconInvertOrderColumnClick(object sender, ColumnClickEventArgs e)
		{	
			if ( e.Column == CurrentSortedColumn )
				{
					// Change sortOrder for this column
					if (CurrentSortOrder == SortOrder.Ascending)
					{
						CurrentSortOrder = SortOrder.None;
					}
					else if (CurrentSortOrder == SortOrder.Descending) {
						CurrentSortOrder = SortOrder.Ascending;
					}
					else
					{ // Set to Default
						CurrentSortOrder = SortOrder.Descending;
					}
					
				}
				else
				{
					// Define sort column.
					CurrentSortedColumn = e.Column;
					CurrentSortOrder = SortOrder.Descending;
				}
				
				// Process Sort
				this.m_lvi.Sort();
				UpdateColumnSortingIcons();
		}
		
		
		private void UpdateColumnSortingIcons()
		{
			if(UIUtil.SetSortIcon(m_lvi, CurrentSortedColumn,
				CurrentSortOrder)) return;

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

			foreach(ColumnHeader ch in m_lvi.Columns)
			{
				string strCur = ch.Text, strNew = null;

				if(strCur.EndsWith(strAsc) || strCur.EndsWith(strDsc))
				{
					strNew = strCur.Substring(0, strCur.Length - strAsc.Length);
					strCur = strNew;
				}

				if((ch.Index == CurrentSortedColumn) &&
					(CurrentSortOrder != SortOrder.None))
				{
					if(CurrentSortOrder == SortOrder.Ascending)
						strNew = strCur + strAsc;
					else if(CurrentSortOrder == SortOrder.Descending)
						strNew = strCur + strDsc;
				}

				if(strNew != null) ch.Text = strNew;
			}
		}

		
		#endregion
		
		#region AutoSizing
		
		private bool m_boAutoWidthColumn;
		public bool AutoWidthColumn {
			get {
				return m_boAutoWidthColumn;
			}
			set {
				if ( m_boAutoWidthColumn != value ) {
					if (m_lvi != null) {
						if ( value ) {
							MakeColumnsAutoSized();
						}
						else {
							MakeColumnsNotAutoSized();
						}
					}
						
				}
				m_boAutoWidthColumn = value;
			}
		}
	    // AutoSize feature
		private bool Resizing;
	 
		private void ListView_SizeChanged(object sender, EventArgs e)
		{
			//
			// From URL:
			//   http://nickstips.wordpress.com/2010/11/10/c-listview-dynamically-sizing-columns-to-fill-whole-control/
			// ( Width of column is used in place of Tag)
			//
			
			Debug.Assert( m_lvi != null );
			
		    // Don't allow overlapping of SizeChanged calls
		    if (!Resizing)
		    {
		        // Set the resizing flag
		        Resizing = true;
		 
	            float totalColumnWidth = 0;
	 
	            // Get the sum of all column tags
	            for (int i = 0; i < m_lvi.Columns.Count; i++)
	            	totalColumnWidth += Convert.ToInt32(m_lvi.Columns[i].Width);
	 
	            // Calculate the percentage of space each column should
	            // occupy in reference to the other columns and then set the
	            // width of the column to that percentage of the visible space.
	            for (int i = 0; i < m_lvi.Columns.Count; i++)
	            {
	                float colPercentage = (Convert.ToInt32(m_lvi.Columns[i].Width) / totalColumnWidth);
	                m_lvi.Columns[i].Width = (int)(colPercentage * m_lvi.ClientRectangle.Width);            
	            }
		        
		    }
		   
		    // Clear the resizing flag
		    Resizing = false;
		}
		
		private void MakeColumnsAutoSized() {
			Debug.Assert( m_lvi != null);
			m_lvi.SizeChanged += new EventHandler(ListView_SizeChanged);
		}
		
		private void MakeColumnsNotAutoSized() {
			Debug.Assert( m_lvi != null);
			m_lvi.SizeChanged -= new EventHandler(ListView_SizeChanged);
		}
		
		#endregion
		
		#region Update Statistics message
		
		/// <summary>
		/// This delegate will be launched each time:
		/// <list type="bullet">
		///  <item>
		///     <description>an item will be checked/unchecked</description>
		///  </item>
		///  <item>
		///     <description>an item will selected/unselected</description>
		///  </item>
		/// </list>
        ///  %1 will be replaced by number of Items.
        ///  %2 will be replaced by number of Selected Items
        ///  %3 will be replace by number of Checked Items
		/// </summary>
		public delegate void dlgStatisticMessageUpdater(String newText);
		
		private dlgStatisticMessageUpdater m_dlgStatisticMessageUpdater;
		private bool m_boLaunchWhenCheckItemsChanges;
		private bool m_boLaunchWhenSelectedItemsChanges;
			
		private String m_strTemplate;
			
		// Automatically launch.
		// Can be launched if number of selected items has been changed
		public void UpdateStatistics()
		{
			if ( ! StatisticIsApplicable()) return;
			
			// Compile text
			String text = m_strTemplate;
			text = text.Replace("%1", m_lvi.Items.Count.ToString(NumberFormatInfo.InvariantInfo));
			text = text.Replace("%2", m_lvi.SelectedItems.Count.ToString(NumberFormatInfo.InvariantInfo));
			text = text.Replace("%3", m_lvi.CheckedItems.Count.ToString(NumberFormatInfo.InvariantInfo));
			
			m_dlgStatisticMessageUpdater(text);
		}
		
		/// <summary>
		/// Assign an delegate that will be launched to update statistics message
		/// </summary>
		/// <param name="statAdapter"></param>
		/// <param name="launchWhenCheckItemsChanges"></param>
		/// <param name="launchWhenSelected_ItemsChanges"></param>
		/// <param name="strTemplate"></param>
		public void AssignStatisticMessageUpdater( dlgStatisticMessageUpdater statMessageAdapter,
		  bool launchWhenCheckItemsChanges,
		  bool launchWhenSelectedItemsChanges,
		  String strTemplate ) {
			
			m_boLaunchWhenSelectedItemsChanges = launchWhenSelectedItemsChanges;
			m_boLaunchWhenCheckItemsChanges = launchWhenCheckItemsChanges;

			m_strTemplate = (String)(strTemplate.Clone());
			m_dlgStatisticMessageUpdater = statMessageAdapter;
			
			if (m_lvi != null)
				ApplyStatisticMessageUpdater();
			
		}
		
		public bool StatisticIsApplicable() {
			return (m_dlgStatisticMessageUpdater != null ) && 
				(m_strTemplate != null );
		}
		
		/// <summary>
		/// Apply statistic adapter to embedded list
		/// </summary>
		private void ApplyStatisticMessageUpdater() {
			if(m_lvi == null) { Debug.Assert(false); throw new InvalidOperationException("AssignStatisticAdapter::lvi is null");}
			
			if (m_boLaunchWhenCheckItemsChanges) {
				RestoreCheckedItemEvent();
			}
			
			if (m_boLaunchWhenSelectedItemsChanges) {
				m_lvi.SelectedIndexChanged -= OnSelectedIndexChanged; // To ensure that it is not already attached
				m_lvi.SelectedIndexChanged += OnSelectedIndexChanged;
			}
		}
		
		#endregion
		
		#region Try - Checkbox in header
		
//		private bool m_boCheckboxInHeader = false;
//		public bool CheckboxInHeader {
//			get { return m_boCheckboxInHeader; }
//			set { m_boCheckboxInHeader = value; }
//		}
		// CheckBox in listWith
//		private static Int64 start = 0; // UNDONE Checkbox in HEader
//	
//		private bool currentDrawing = false;
//		private void OnLvColumnHeader_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e) {
//			// From http://stackoverflow.com/questions/1778600/listview-header-check-box
//			//
//			
//			Debug.Print("Begin DrawColumnHeader - " + start);
//			start++;
//			
//			Debug.Assert(m_lvi != null);
//			
//			if ((e.ColumnIndex == 0)) {
//				if (currentDrawing) return;
//				currentDrawing = true;
//				
//				CheckBox cck = new CheckBox();
//	            cck.Text = "";  cck.Visible = true;
//	            
//	            m_lvi.SuspendLayout();
//	            e.DrawBackground();
//	            UpdateColumnSortingIcons();
//	            cck.UseVisualStyleBackColor = true;
//	            cck.Location = new Point(3, 0);
//	            
//	            cck.SetBounds(
//	            	e.Bounds.X,
//	            	e.Bounds.Y,
//	            	cck.GetPreferredSize(new Size(e.Bounds.Width, e.Bounds.Height)).Width,
//	            	cck.GetPreferredSize(new Size(e.Bounds.Width, e.Bounds.Height)).Height);
//	            
//	            cck.Size = new Size(
//	            	(cck.GetPreferredSize(
//	            		new Size(
//	            			e.Bounds.Width - 1,
//	            			e.Bounds.Height
//	            		)
//	            		).Width +1
//	            	),
//	            	e.Bounds.Height-2);
//	            
//	            
//		            m_lvi.Controls.Add(cck);
//		            cck.Show();
//		            cck.BringToFront();
//		            
//		            e.DrawText((TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.LeftAndRightPadding | TextFormatFlags.GlyphOverhangPadding));
//				
//		            cck.Click += new EventHandler(Bink);
//		            
//	            
//	
//	            m_lvi.ResumeLayout(true);
//	            currentDrawing = false;
//	
//	        }
//	        else  {
//	            e.DrawDefault = true;
//	        }
//			
//			
//
//		}
//
//	    private void OnLvColumnHeader_DrawItem(object sender, DrawListViewItemEventArgs e)  {
//	        e.DrawDefault = true;
//	    }
//	
//	    private void OnLvColumnHeader_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)  {
//	        e.DrawDefault = true;
//	    }
//	
//	    private void Bink(object sender, System.EventArgs e) {
//			Debug.Assert( m_lvi != null);
//			
//	        CheckBox test = sender as CheckBox;
//	
//	        for (int i=0;i < m_lvi.Items.Count; i++) {
//	            m_lvi.Items[i].Checked = test.Checked;
//	        }
//	    }
		 #endregion

	    
	}
	
}

