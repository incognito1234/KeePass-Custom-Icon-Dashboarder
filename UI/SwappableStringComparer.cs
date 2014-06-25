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
using System.Diagnostics;
using System.Windows.Forms;

/*
 * LomsonLib.UI
 * 
 * Version 1.0 - 2014/06/25
 *     - Initial Version
 * 
 */

namespace LomsonLib.UI
{
	/// <summary>
	/// Description of IListViewItemComparer.
	/// </summary>
	public interface ISwappableStringComparer
	{
		int  compare(String str1, String str2);
		void swap();
		void revertSwapToDefault();
	}
	
	public abstract class BaseSwappableStringComparer:ISwappableStringComparer
	{
		protected bool  m_defaultSwapped;
		protected bool  m_isSwapped;
		
		public BaseSwappableStringComparer( bool defaultSwapped ) {
			m_defaultSwapped = defaultSwapped;
			m_isSwapped = false;
		}
	
		public abstract int compare(string str1, string str2);
	
		
		public void swap() {
			m_isSwapped = !m_isSwapped;
		}
		
		public void revertSwapToDefault() {
			m_isSwapped = m_defaultSwapped;
		}
		
		public bool isSwapped() {
			return ( m_isSwapped ^ m_defaultSwapped );
		}
		
		
		protected String getString1(string str1, string str2) {
			return isSwapped()?str2:str1;
		}
		
		protected String getString2(string str1, string str2) {
			return isSwapped()?str1:str2;
		}
		
	}
	
	public class StringComparer:BaseSwappableStringComparer
	{
		private bool m_ignoreCase;
		
		public StringComparer( bool defaultSwapped, bool ignoreCase )
			:base( defaultSwapped) {
			
			m_ignoreCase = ignoreCase;
		}
		
		public override int compare(string str1, string str2)  {
			
			string cmpStr1 = getString1( str1, str2);
			string cmpStr2 = getString2( str1, str2);
			
			return (String.Compare( cmpStr1, cmpStr2, m_ignoreCase) );
		}
		
	}
	
	public class IntegerAsStringComparer:BaseSwappableStringComparer
	{
		
		public IntegerAsStringComparer( bool defaultSwapped )
			:base( defaultSwapped) {
			
		}
		
		public override int compare( string str1, string str2) {
			int compareResult;
			
			string cmpStr1 = getString1( str1, str2);
			string cmpStr2 = getString2( str1, str2);
			
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
