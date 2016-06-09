/*
 CustomIconDashboarder - KeePass Plugin to get some information and
  manage custom icons
   
 Copyright (C) 2016 Jareth Lomson <incognito1234@users.sourceforge.net>

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
using LomsonLib.Utility;

namespace CustomIconDashboarderPlugin
{
	
	/// <summary>
	/// Description of Class1.
	/// </summary>
	public class IconStatsHandler
	{
		
		private Dictionary<PwUuid, IconStats> m_dicCustomIconsStats = null;
		private Dictionary<PwIcon, IconStats> m_dicStandardIconsStats = null;
		private bool m_isInitialized;
		
		public IconStatsHandler()
		{
			m_dicCustomIconsStats = new Dictionary<PwUuid, IconStats>();
			m_dicStandardIconsStats = new Dictionary<PwIcon, IconStats>();
		}
		
		public void Initialize( PwDatabase db) {
			
			Debug.Assert( m_dicCustomIconsStats != null ); if (m_dicCustomIconsStats == null) throw new InvalidOperationException();
			Debug.Assert( m_dicStandardIconsStats != null ); if (m_dicStandardIconsStats == null) throw new InvalidOperationException();
			
			
			GroupHandler gh = delegate(PwGroup pg)
			{
				if ( !(pg.CustomIconUuid.Equals(PwUuid.Zero) ) ){
					if (!(m_dicCustomIconsStats.ContainsKey( pg.CustomIconUuid)) ){
						m_dicCustomIconsStats.Add( pg.CustomIconUuid, new IconStats());
					}
					m_dicCustomIconsStats[ pg.CustomIconUuid ].nbInGroups += 1;
					m_dicCustomIconsStats[ pg.CustomIconUuid ].listGroups.Add( pg);
				}
				else {
					if (!(m_dicStandardIconsStats.ContainsKey( pg.IconId)) ){
						m_dicStandardIconsStats.Add( pg.IconId, new IconStats());
					}
					m_dicStandardIconsStats[ pg.IconId ].nbInGroups += 1;
					m_dicStandardIconsStats[ pg.IconId ].listGroups.Add( pg);
				}
								
				return true;
			};
			
			String urlFieldValue;
			Uri siteUri;
			List<Uri> uriList;
			
			EntryHandler eh = delegate(PwEntry pe)
			{
				if (!(pe.CustomIconUuid.Equals(PwUuid.Zero))) {
					
					if (!(m_dicCustomIconsStats.ContainsKey(pe.CustomIconUuid))) {
						m_dicCustomIconsStats.Add(pe.CustomIconUuid, new IconStats());
					}
					m_dicCustomIconsStats[pe.CustomIconUuid].nbInEntries += 1;
					m_dicCustomIconsStats[pe.CustomIconUuid].listEntries.Add(pe);
					uriList = m_dicCustomIconsStats[pe.CustomIconUuid].listUris;
						
				} 
				else {
					if (!(m_dicStandardIconsStats.ContainsKey(pe.IconId))) {
						m_dicStandardIconsStats.Add(pe.IconId, new IconStats());
					}
					m_dicStandardIconsStats[pe.IconId].nbInEntries += 1;
					m_dicStandardIconsStats[pe.IconId].listEntries.Add(pe);
					uriList = m_dicStandardIconsStats[pe.IconId].listUris;
					
				}
					
				// Get Uri
				urlFieldValue = pe.Strings.ReadSafe(PwDefs.UrlField);
				if (!string.IsNullOrEmpty(urlFieldValue)) {
					try {
						urlFieldValue = URLUtility.addUrlHttpPrefix(urlFieldValue);
							
						siteUri = new Uri(urlFieldValue);
						if ((siteUri.Scheme.ToLower() == "http") ||
						     (siteUri.Scheme.ToLower() == "https")) {
							uriList.Add(siteUri);
						}
					} catch (Exception) {};
				}
				
				return true;
			};
			
			gh( db.RootGroup );
			db.RootGroup.TraverseTree(TraversalMethod.PreOrder, gh, eh);
			
			m_isInitialized = true;
		}
		
		
		/// <summary>
		/// Get number of usage of pci in groups for Custom Icon
		/// </summary>
		/// <param name="pci"></param>
		/// <returns>number of usage of pci in groups</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Nb")]
		public int GetNbUsageInGroups(PwCustomIcon pci) {
			Debug.Assert( m_isInitialized);
			
			if ( m_dicCustomIconsStats.ContainsKey( pci.Uuid ) ) {
				return m_dicCustomIconsStats[pci.Uuid].nbInGroups;
				
			}
			else {
				return 0;
			}
		}
		
		/// <summary>
		/// Get number of usage of pci in entries for Custom Icon
		/// </summary>
		/// <param name="pci"></param>
		/// <returns>number of usage of pci in entries</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Nb")]
		public int GetNbUsageInEntries(PwCustomIcon pci) {
			Debug.Assert( m_isInitialized);
			
			if ( m_dicCustomIconsStats.ContainsKey( pci.Uuid ) ) {
				return m_dicCustomIconsStats[pci.Uuid].nbInEntries;
				
			}
			else {
				return 0;
			}
		}
		
		/// <summary>
		/// Get List of entries for Custom Icon
		/// </summary>
		/// <param name="pci"></param>
		/// <returns>List of entries</returns>
		public ICollection<PwEntry> GetListEntriesForPci( PwCustomIcon pci) {
			Debug.Assert( m_isInitialized);
			
			if ( m_dicCustomIconsStats.ContainsKey( pci.Uuid ) ) {
				return m_dicCustomIconsStats[pci.Uuid].listEntries;
				
			}
			else {
				return new List<PwEntry>();
			}
		}
		
		/// <summary>
		/// Get List of entries for Standard Icon
		/// </summary>
		/// <param name="pci"></param>
		/// <returns>List of entries</returns>
		public ICollection<PwEntry> GetListEntriesForStandardIcon( PwIcon stdIcon) {
			Debug.Assert( m_isInitialized);
			
			if ( m_dicStandardIconsStats.ContainsKey( stdIcon ) ) {
				return m_dicStandardIconsStats[stdIcon].listEntries;
				
			}
			else {
				return new List<PwEntry>();
			}
		}
		
		
		// <summary>
		/// Get List of entries for Custom Icon
		/// </summary>
		/// <param name="pci"></param>
		/// <returns>List of entries</returns>
		public ICollection<PwEntry> GetListEntriesForUuid( PwUuid puuid) {
			Debug.Assert( m_isInitialized);
			
			if ( m_dicCustomIconsStats.ContainsKey( puuid ) ) {
				return m_dicCustomIconsStats[puuid].listEntries;
				
			}
			else {
				return new List<PwEntry>();
			}
		}
		
		/// <summary>
		/// Get List of groups for Custom Icon
		/// </summary>
		/// <param name="pci"></param>
		/// <returns>List of groups</returns>
		public ICollection<PwGroup> GetListGroupsForPci( PwCustomIcon pci) {
			Debug.Assert( m_isInitialized);
			
			if ( m_dicCustomIconsStats.ContainsKey( pci.Uuid ) ) {
				return m_dicCustomIconsStats[pci.Uuid].listGroups;
				
			}
			else {
				return new List<PwGroup>();
			}
		}
		
		/// <summary>
		/// Get List of groups for Standard Icon
		/// </summary>
		/// <param name="pci"></param>
		/// <returns>List of groups</returns>
		public ICollection<PwGroup> GetListGroupsForStandardIcon( PwIcon stdIcon) {
			Debug.Assert( m_isInitialized);
			
			if ( m_dicStandardIconsStats.ContainsKey( stdIcon ) ) {
				return m_dicStandardIconsStats[stdIcon].listGroups;
				
			}
			else {
				return new List<PwGroup>();
			}
		}
		
		
		public ICollection<PwGroup> GetListGroupsFromUuid( PwUuid puuid) {
			Debug.Assert( m_isInitialized);
			
			if ( m_dicCustomIconsStats.ContainsKey( puuid ) ) {
				return m_dicCustomIconsStats[puuid].listGroups;
				
			}
			else {
				return new List<PwGroup>();
			}
		}
		
		/// <summary>
		/// Get List of Uris
		/// </summary>
		/// <param name="pci"></param>
		/// <returns>List of groups</return>
		public ICollection<Uri> GetListUris( PwCustomIcon pci) {
			Debug.Assert( m_isInitialized);
			
			if ( m_dicCustomIconsStats.ContainsKey( pci.Uuid ) ) {
				return m_dicCustomIconsStats[pci.Uuid].listUris;
				
			}
			else {
				return new List<Uri>();
			}
		}
		
		/// <summary>
		/// Get number of urls of pci in entries
		/// </summary>
		/// <param name="pci"></param>
		/// <returns>number of urls of pci in entries</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Nb")]
		public int GetNbUrlsInEntries(PwCustomIcon pci) {
			Debug.Assert( m_isInitialized);
			
			if ( m_dicCustomIconsStats.ContainsKey( pci.Uuid ) ) {
				return m_dicCustomIconsStats[pci.Uuid].listUris.Count;
				
			}
			else {
				return 0;
			}
		}
		
		
		private class IconStats {
			
			public int nbInGroups  { get; set ;}
			public int nbInEntries { get; set ;}
			public List<PwEntry> listEntries {get; set;}
			public List<PwGroup> listGroups {get; set; }
			public List<Uri>  listUris {get; set; }
			
			public IconStats () {
				nbInGroups = 0;
				nbInEntries = 0;
				listEntries = new List<PwEntry>();
				listGroups = new List<PwGroup>();
				listUris = new List<Uri>();
			}
		}
	}
	
	
}
