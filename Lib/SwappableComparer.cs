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
using System;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;

/*
 * LomsonLib.UI
 * 
 *     Version 1.1
 *      See ListViewLayoutManager.cs for release notes
 * 
 */

namespace LomsonLib.UI
{
	/// <summary>
	/// Description of IListViewItemComparer.
	/// </summary>
	public interface ISwappableComparer<T>
	{
		int  Compare(T obj1, T obj2);
		void Swap();
		void RevertSwapToDefault();
	}
	
	public abstract class BaseSwappableComparer<T>: ISwappableComparer<T>
	{
		protected bool  DefaultSwapped  {get; set;}
		protected bool  ManuallySwapped {get; set;}
		
		public abstract int Compare(T obj1, T obj2);
		
		protected BaseSwappableComparer( bool defaultSwapped ) {
			DefaultSwapped = defaultSwapped;
			ManuallySwapped = false;
		}
	
		public void Swap() {
			ManuallySwapped = !ManuallySwapped;
		}
		
		public void RevertSwapToDefault() {
			ManuallySwapped = DefaultSwapped;
		}
		
		public bool IsSwapped() {
			return ( ManuallySwapped ^ DefaultSwapped );
		}
		
	}
	
	public interface ISwappableStringComparer:ISwappableComparer<String>{};
	
	public interface ISwappableListViewItemComparer:ISwappableComparer<ListViewItem>{};
	
	public abstract class BaseSwappableStringComparer:BaseSwappableComparer<String>, ISwappableStringComparer
	{
		protected BaseSwappableStringComparer( bool defaultSwapped ): base(defaultSwapped) {
		}
		
		public abstract override int Compare(String obj1, String obj2);
		
		protected String GetString1(String str1, String str2) {
			return IsSwapped()?str2:str1;
		}
		
		protected String GetString2(String str1, String str2) {
			return IsSwapped()?str1:str2;
		}
		
	}
	
	public class StringComparer:BaseSwappableStringComparer
	{
		private bool m_ignoreCase;
		
		public StringComparer( bool defaultSwapped, bool ignoreCase )
			:base( defaultSwapped) {
			
			m_ignoreCase = ignoreCase;
		}
		
		public override int Compare(String obj1, String obj2)  {
			
			string cmpStr1 = GetString1( obj1, obj2);
			string cmpStr2 = GetString2( obj1, obj2);
			
			return (String.Compare( cmpStr1, cmpStr2, m_ignoreCase) );
		}
		
	}
	
	public class IntegerAsStringComparer:BaseSwappableStringComparer
	{
		
		public IntegerAsStringComparer( bool defaultSwapped )
			:base( defaultSwapped) {
			
		}
		
		public override int Compare( String obj1, String obj2) {
			int compareResult;
			
			string cmpStr1 = GetString1( obj1, obj2);
			string cmpStr2 = GetString2( obj1, obj2);
			
			bool parseOK;
			int iComp1;
			parseOK = int.TryParse(cmpStr1, out iComp1);
			Debug.Assert( parseOK ); if (!parseOK) throw new InvalidOperationException("unable to parse");
			int iComp2;
			parseOK = int.TryParse(cmpStr2, out iComp2);
			Debug.Assert( parseOK ); if (!parseOK) throw new InvalidOperationException("unable to parse");
			
			if (iComp1 > iComp2) {
				compareResult = 1;
			}
			else if (iComp1 < iComp2) {
				compareResult = -1;
			}
			else { // iViewX == iViewY )
				compareResult = 0;
			}
			
			return compareResult;
		}
		
	}
	
}
