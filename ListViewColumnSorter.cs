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
using System.Collections;	
using System.Windows.Forms;
using System.Diagnostics;

namespace CustomIconDashboarderPlugin
{
	/// <summary>
	/// Description of ListViewColumnSorter.
	/// </summary>
	public class ListViewColumnSorter : IComparer
	{
		private int ColumnToSort;
		private SortOrder OrderOfSort;
	
		public ListViewColumnSorter()
		{
			ColumnToSort = 0;
			OrderOfSort = SortOrder.None;
		}
	
		public int Compare(object x, object y)
		{
			int compareResult;
			ListViewItem listviewX, listviewY;
	
			listviewX = (ListViewItem)x;
			listviewY = (ListViewItem)y;
			
			bool parseOK;
			int iViewX;
			parseOK = int.TryParse(listviewX.SubItems[ColumnToSort].Text, out iViewX);
			Debug.Assert( parseOK ); if (!parseOK) throw new InvalidOperationException("unable to parse");
			int iViewY;
			parseOK = int.TryParse(listviewY.SubItems[ColumnToSort].Text, out iViewY);
			Debug.Assert( parseOK ); if (!parseOK) throw new InvalidOperationException("unable to parse");
			
			if (iViewX > iViewY) {
				compareResult = 1;
			}
			else {
				compareResult = -1;
			}
				
			if (OrderOfSort == SortOrder.Ascending)	{
				return compareResult;
			}
			else if (OrderOfSort == SortOrder.Descending) {
				return (-compareResult);
			}
			else {
				return 0;
			}
		}
	
		public int SortColumn {
			set	{ ColumnToSort = value;	}
			get	{ return ColumnToSort;	}
		}
	
		public SortOrder Order {
			set	{ OrderOfSort = value;	}
			get	{ return OrderOfSort;	}
		}
	    
	}
}

