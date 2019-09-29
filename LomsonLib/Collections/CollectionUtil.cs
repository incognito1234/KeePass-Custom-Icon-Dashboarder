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
using System.Collections.Generic;
using System.Diagnostics;

using LomsonLib.LomsonDebug;

/*
 * LomsonLib.UI
 * 
 *     Version 1.1
 *      See ListViewLayoutManager.cs for release notes
 */


namespace LomsonLib.Collections
{
	/// <summary>
	/// Description of CollectionUtil.
	/// </summary>
	public class CollectionUtil
	{
		
		/// <summary>
		/// Get union of col1 and col2 with no duplicate entries. Comparison method is the default one;
		/// </summary>
		public static ICollection<T> Union<T>(ICollection<T> col1, ICollection<T> col2) {
			List<T> result = new List<T>();
			
			result.AddRange(col1);
			
			foreach (T readValue in col2) {
				if (!result.Contains(readValue)) {
					result.Add(readValue);
				}
			}
			
			return result;
		}
		
		/// <summary>
		/// Get collection with all element present in col1 and not in col2
		/// </summary>
		public static ICollection<T> OnlyInFirstCollection<T>(ICollection<T> col1, ICollection<T> col2, Equals<T> comparer) {
			
			List<T> result = new List<T>();
			result.AddRange(col1);
			
			foreach( T readValue in col2) {
				T element = GetElementFromCollection( col2, readValue, comparer) ;
				result.Remove(element);
			}
			
			return result;
		}
		
		/// <summary>
		/// Get refElement in col ? default(T) if it does not exist
		/// </summary>
		public static T GetElementFromCollection<T>(ICollection<T> col, T refElement, Equals<T> comparer ) {
			bool exists = false;
			
			IEnumerator<T> enumeratorCol = col.GetEnumerator();
			T readT = default(T);
			while ((enumeratorCol.MoveNext() ) && (!exists)) {
				readT = enumeratorCol.Current;
				exists = comparer(refElement, readT);
			}
			
			if (!exists) {
				return default(T);
			}
			else {
				return readT;
			}
		}
		
		/// <summary>
		/// Get array from ICollection
		/// </summary>
		public static T[] ICollectionToArray<T>(ICollection<T> col) {
			T[] result = new T[col.Count];
			int i = 0;
			foreach (T readValue in col) {
				result[i] = readValue;
				i++;
			}
			return result;
		}
	}
	
		
	public delegate bool Equals<T>(T obj1, T obj2);
}
