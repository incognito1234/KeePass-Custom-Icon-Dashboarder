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

/*
 * LomsonLib.UI
 * 
 * Version 1.0 - 2014/06/25
 *    - Initial Version
 * 
 */
 
using System;
using System.Collections;	
using System.Collections.Generic;

using System.Windows.Forms;
using System.Diagnostics;

using KeePass.Util;
using KeePass.UI;

namespace LomsonLib.UI
{
	/// <summary>
	/// Sorter for List.
	/// </summary>
	public class ListViewLayoutManager : IComparer
	{
		private List<OrderedColumn>  m_DefaultOrderedColumn;
		
		private Dictionary<int,ISwappableStringComparer> m_ColumnComparers; 
		
		private ListView m_lvi;
		
		private ISwappableStringComparer m_CurrentComparer;
		
		/// <summary>
		/// Current Sort Order. If null, default sort is applied
		/// </summary>
		private SortOrder m_CurrentSortOrder;
		public SortOrder  CurrentSortOrder {
			
			get {
				return m_CurrentSortOrder;
			}
			
			set {
				if (value == SortOrder.Ascending) {
					Debug.Assert( m_ColumnComparers.ContainsKey( CurrentSortedColumn ) );
					m_ColumnComparers[CurrentSortedColumn].revertSwapToDefault();
					m_CurrentComparer = m_ColumnComparers[CurrentSortedColumn];
				}
				else if (value == SortOrder.Descending) {
					Debug.Assert( m_ColumnComparers.ContainsKey( CurrentSortedColumn ) );
					m_ColumnComparers[CurrentSortedColumn].revertSwapToDefault();
					m_ColumnComparers[CurrentSortedColumn].swap();
					m_CurrentComparer = m_ColumnComparers[CurrentSortedColumn];
				}
				else { // value = SortOrder.None
					m_CurrentComparer = null;
				}
				
				m_CurrentSortOrder = value;
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
		
		private bool m_boAutoWidthColumn = false;
		public bool AutoWidthColumn {
			get {
				return m_boAutoWidthColumn;
			}
			set {
				if ( m_boAutoWidthColumn != value ) {
					if (m_lvi != null) {
						if ( value = true ) {
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
		
		
		// Initialize
		public ListViewLayoutManager()
		{
			m_lvi = null;
			m_CurrentSortedColumn = 0;
			CurrentSortOrder    = SortOrder.None;
			m_boAutoWidthColumn = false;
			
			m_ColumnComparers = new Dictionary<int,ISwappableStringComparer>();
			m_DefaultOrderedColumn = new List<OrderedColumn>();
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
			OrderedColumn toBeAdded = new OrderedColumn();
			toBeAdded.ColumnNumber = nColumn;
			toBeAdded.DefaultSwapped    = defaultSwapped;
			
			m_DefaultOrderedColumn.Add( toBeAdded );
		}
	
		// Compare
		public int Compare(object x, object y)
		{
			ListViewItem lvX = x as ListViewItem;
			ListViewItem lvY = y as ListViewItem;
			
			Debug.Assert( (lvX != null) && (lvY != null) );
			
			if (CurrentSortOrder == SortOrder.None) {
				return DefaultCompare( lvX, lvY);
			}
			else {
				String str1 = lvX.SubItems[m_CurrentSortedColumn].Text;
				String str2 = lvY.SubItems[m_CurrentSortedColumn].Text;
				
				return m_CurrentComparer.compare(str1, str2);
			}
		}
		
		private int DefaultCompare(ListViewItem lvi1, ListViewItem lvi2)
		{
			Debug.Assert( m_DefaultOrderedColumn.Count > 0);
			
			String str1;
			String str2;
			
			int readNColumn;
			ISwappableStringComparer readComparer;
			int result = 0;
			int iKey = 0;
			while ( (iKey < m_DefaultOrderedColumn.Count ) && (result == 0 ) ) {
				readNColumn =  m_DefaultOrderedColumn[iKey].ColumnNumber;
				
				readComparer = m_ColumnComparers[iKey];
				readComparer.revertSwapToDefault();
				if ( m_DefaultOrderedColumn[iKey].DefaultSwapped ) {
					m_ColumnComparers[iKey].swap();
				}
				
				str1 = lvi1.SubItems[readNColumn].Text;
				str2 = lvi2.SubItems[readNColumn].Text;
				result = readComparer.compare( str1, str2);
				iKey++;
					
			}
			
			return result;
		}
		
		private void OnLvViewIconColumnClick(object sender, ColumnClickEventArgs e)
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
		
		
		public void ApplyToListView(ListView lvi) {
			Debug.Assert( m_lvi == null );
			lvi.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.OnLvViewIconColumnClick);
			lvi.ListViewItemSorter = this;
			
			m_lvi = lvi;
			if ( this.AutoWidthColumn ) {
				MakeColumnsAutoSized();
			}
			
		}
		
	    // AutoSize feature
		private bool Resizing = false;
	 
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
		 

		private class OrderedColumn {
			public int  ColumnNumber { get; set; }
			public bool DefaultSwapped { get; set; }
		}
	    
	}
	
}

