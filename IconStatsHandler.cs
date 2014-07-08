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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using KeePassLib.Delegates;
using KeePassLib;

namespace CustomIconDashboarderPlugin
{
	
	/// <summary>
	/// Description of Class1.
	/// </summary>
	public class IconStatsHandler
	{
		
		private Dictionary<PwUuid, IconStats> m_dicIconStats = null;
		private bool m_isInitialized;
		
		public IconStatsHandler()
		{
			m_dicIconStats = new Dictionary<PwUuid, IconStats>();
		}
		
		public void Initialize( PwDatabase db) {
			
			Debug.Assert( m_dicIconStats != null ); if (m_dicIconStats == null) throw new InvalidOperationException();
			
			
			GroupHandler gh = delegate(PwGroup pg)
			{
				if ( !(pg.CustomIconUuid.Equals(PwUuid.Zero) ) ){
					if (!(m_dicIconStats.ContainsKey( pg.CustomIconUuid)) ){
						m_dicIconStats.Add( pg.CustomIconUuid, new IconStats());
					}
					m_dicIconStats[ pg.CustomIconUuid ].nbInGroups += 1;
					m_dicIconStats[ pg.CustomIconUuid ].listGroups.Add( pg);
				}
				
				
				return true;
			};
			
			EntryHandler eh = delegate(PwEntry pe)
			{
				if ( !(pe.CustomIconUuid.Equals(PwUuid.Zero))) {
					
					if (!(m_dicIconStats.ContainsKey( pe.CustomIconUuid))) {
						m_dicIconStats.Add( pe.CustomIconUuid, new IconStats());
					}
					m_dicIconStats[ pe.CustomIconUuid ].nbInEntries += 1;
					m_dicIconStats[ pe.CustomIconUuid ].listEntries.Add( pe);
					
				}
				
				return true;
			};
			
			gh( db.RootGroup );
			db.RootGroup.TraverseTree(TraversalMethod.PreOrder, gh, eh);
			
			m_isInitialized = true;
		}

		/// <summary>
		/// Get number of usage of pci in groups
		/// </summary>
		/// <param name="pci"></param>
		/// <returns>number of usage of pci in groups</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Nb")]
		public int GetNbUsageInGroups(PwCustomIcon pci) {
			Debug.Assert( m_isInitialized);
			
			if ( m_dicIconStats.ContainsKey( pci.Uuid ) ) {
				return m_dicIconStats[pci.Uuid].nbInGroups;
				  
			}
			else {
				return 0;
			}
		}
		
		/// <summary>
		/// Get number of usage of pci in entries
		/// </summary>
		/// <param name="pci"></param>
		/// <returns>number of usage of pci in entries</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Nb")]
		public int GetNbUsageInEntries(PwCustomIcon pci) {
			Debug.Assert( m_isInitialized);
			
			if ( m_dicIconStats.ContainsKey( pci.Uuid ) ) {
				return m_dicIconStats[pci.Uuid].nbInEntries;
				  
			}
			else {
				return 0;
			}
		}
		
		/// <summary>
		/// Get List of entries
		/// </summary>
		/// <param name="pci"></param>
		/// <returns>List of entries</returns>
		public ICollection<PwEntry> GetListEntries( PwCustomIcon pci) {
			Debug.Assert( m_isInitialized);
			
			if ( m_dicIconStats.ContainsKey( pci.Uuid ) ) {
				return m_dicIconStats[pci.Uuid].listEntries;
				  
			}
			else {
				return new List<PwEntry>();
			}
		}
		
		// <summary>
		/// Get List of entries
		/// </summary>
		/// <param name="pci"></param>
		/// <returns>List of entries</returns>
		public ICollection<PwEntry> GetListEntriesFromUuid( PwUuid puuid) {
			Debug.Assert( m_isInitialized);
			
			if ( m_dicIconStats.ContainsKey( puuid ) ) {
				return m_dicIconStats[puuid].listEntries;
				  
			}
			else {
				return new List<PwEntry>();
			}
		}
		
		/// <summary>
		/// Get List of groups
		/// </summary>
		/// <param name="pci"></param>
		/// <returns>List of groups</returnGroupss>
		public ICollection<PwGroup> GetListGroups( PwCustomIcon pci) {
			Debug.Assert( m_isInitialized);
			
			if ( m_dicIconStats.ContainsKey( pci.Uuid ) ) {
				return m_dicIconStats[pci.Uuid].listGroups;
				  
			}
			else {
				return new List<PwGroup>();
			}
		}
		
		public ICollection<PwGroup> GetListGroupsFromUuid( PwUuid puuid) {
			Debug.Assert( m_isInitialized);
			
			if ( m_dicIconStats.ContainsKey( puuid ) ) {
				return m_dicIconStats[puuid].listGroups;
				  
			}
			else {
				return new List<PwGroup>();
			}
		}
		
		
		private class IconStats {
		
			public int nbInGroups  { get; set ;}
			public int nbInEntries { get; set ;}
			public List<PwEntry> listEntries {get; set;}
			public List<PwGroup> listGroups {get; set; }
			
			public IconStats () {
				nbInGroups = 0;
				nbInEntries = 0;
				listEntries = new List<PwEntry>();
				listGroups = new List<PwGroup>();
			}
		}
	}
	
	
	
	
		
}
