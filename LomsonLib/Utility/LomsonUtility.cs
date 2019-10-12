/*
 LomsonLib - Library to help management of various object types in program 
  developed in c#.
  
 Copyright (C) 2015 Jareth Lomson <jareth.lomson@gmail.com>
  
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
using System.Text.RegularExpressions;

/*
 * LomsonLib.Utility
 * 
 *     
 */
namespace LomsonLib.Utility
{
	/// <summary>
	/// Utility to manage URL.
	/// </summary>
	public static class URLUtility
	{
		/// <summary>
		/// Extract rootDomain from URL
		/// </summary>
		public static string RootDomain( string url) 
		{
			Match match = Regex.Match(url, "([hH][tT][tT][pP][sS]?:\\/\\/)?"            // remove http[s]?://
			                              +"([^.]+\\.[^.]{1,3}(\\.[^.]{1,3})?)$");
			string domain = match.Groups[2].Success ? match.Groups[2].Value : null;
			
			return domain;
		}
		
		/// <summary>
		/// Get Domain
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public static string UrlDomain( string url)
		{
			try {
				Uri uri = new Uri( addUrlHttpPrefix(url));
				return uri.Host;
			}
			catch ( Exception ) {
				return "";
			}
			
			
			//return "www.google.fr";
		}
		
		/// <summary>
		/// Build absolute URL.
		/// If urlToBeTested is relative, rootUrl is prefixed.
		/// </summary>
		public static string absoluteURL( string urlToBeTested, string rootUrl ) {
			if (string.IsNullOrEmpty(urlToBeTested) ) {
				return urlToBeTested;
			} else {
				
				Uri test = new Uri(new Uri(rootUrl), urlToBeTested);
				
				return test.AbsoluteUri;
			
			}
		}
		
		/// <summary>
		/// Add http:// if http:// or https:// is not found
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public static string addUrlHttpPrefix( string url) {
			if ( (!url.ToLower().StartsWith("http://"))
			      && (!url.ToLower().StartsWith("https://"))) {
					url = "http://" + url;
			}
			
			return url;
		}
		
		/// <summary>
		/// Remove http:// if http:// or https:// 
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public static string removeHttpPrefix( string url) {
			if  (url.ToLower().StartsWith("http://") ) {
				return url.Remove(0,7);
			} else if (url.ToLower().StartsWith("https://")) {
				return url.Remove(0,8);
			} 
			else {
				return url;
			}
		}
		
	}
}
