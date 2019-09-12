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
using System.Collections.Generic;
using System.Net;
using System.Drawing;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using LomsonLib.Utility;

namespace CustomIconDashboarderPlugin
{

	/// <summary>
	/// Find the best icon of a web site from its url.
	/// </summary>
	public class BestIconFinder
	{
		#region Static elements
		
		/// <summary>
		/// STATIC Section
		/// </summary>
		
		
		public static void InitClass() {
            //  Enable TLS 1.1 (768) and TLS 1.2 (3072)
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
        }
		
		~BestIconFinder() {
			client.Dispose();
		}

        private const string REQUEST_USER_AGENT = "Mozilla/5.0 (Windows 6.1; rv:27.0) Gecko/20100101 Firefox/27.0";
        private const string REQUEST_HEADER_ACCEPT = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
		private const int REQUEST_TIMEOUT = 10000;
		
		private const string alphabetUC = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		private const string alphabetLC = "abcdefghijklmnopqrstuvwxyz";
		
		private static string[] xPathToTest = {
			"//link[translate(@rel,'" + alphabetUC + "','" + alphabetLC +"')='shortcut icon']",
			"//link[translate(@rel,'" + alphabetUC + "','" + alphabetLC +"')='icon']",
			"//link[translate(@rel,'" + alphabetUC + "','" + alphabetLC +"')='apple-touch-icon']",
			"//link[translate(@rel,'" + alphabetUC + "','" + alphabetLC +"')='apple-touch-icon image_src']",
			"//link[translate(@rel,'" + alphabetUC + "','" + alphabetLC +"')='apple-touch-icon-precomposed']"
		};
		
		private static Dictionary<Uri,Image> imageCache = new Dictionary<Uri, Image>{};
		private static object lckImageCache = new object();
		private static Dictionary<Uri, CachedURIRequestResult> uriRequestCache = new Dictionary<Uri, CachedURIRequestResult>{};
		private static object lckUriRequestCache = new object();
		
		#endregion		
		
		
		public Image BestImage {get; private set; }
		
		public Uri SiteUri {get; private set;} // Uri without path - eg http://www.sample.com
		public Uri OriginalSiteUri {get; private set;} // Original URI with all path - eg http://www.sample.com/folder/subfolder
		public Uri RootUriForIco {get; private set;}
		public FinderResult Result {get; private set; }
		public string Details {get;set; }
		public int NbRequestCacheInvoke {get; private set; }
		private MyWebClient client;

		public List<ImageInfo> ListImageInfo {get; private set;} // All image and URI
		public int IndexOfBestImage {get; private set; }
		
		private MyLogger myLogger;
		private List<Uri> ListUriIcons {get; set;}
		
		public BestIconFinder(Uri siteUri)
		{
			this.OriginalSiteUri = siteUri;
			if ( (siteUri.Scheme.ToLower() == "http") ||
			    (siteUri.Scheme.ToLower() == "https" ) ){
				this.SiteUri = new Uri(siteUri.Scheme + "://" +siteUri.DnsSafeHost);
				this.Result = new FinderResult(FinderResult.RESULT_NO_URL);
			} else {
			    this.SiteUri = null;
			    this.Result = new FinderResult(FinderResult.RESULT_NO_URL);
			}
			this.myLogger = new MyLogger(this);
			this.BestImage = null;
			this.IndexOfBestImage = -1;
			this.ListImageInfo = new List<ImageInfo>();
			this.NbRequestCacheInvoke = 0;

            client = new MyWebClient();
		}
		
		public BestIconFinder()
		{
			this.SiteUri = null;
			this.Result = new FinderResult(FinderResult.RESULT_NO_URL);
			this.myLogger = new MyLogger(this);
			this.BestImage = null;
			this.IndexOfBestImage = -1;
			this.ListImageInfo = new List<ImageInfo>();
			this.NbRequestCacheInvoke = 0;

            client = new MyWebClient();
		}
		
		
		public void FindBestIcon()
		{
			if (this.SiteUri == null) {
				this.myLogger.LogWarn("No URL. Stop Find Best Icon");
				return;
			}
			
			FindBestIconForURI( this.SiteUri);
			
			if (this.BestImage == null) { // try with www
				
				Uri testURI = this.SiteUri;
				if ( ! testURI.Host.StartsWith("www.") ) {
					FindBestIconForURI( new Uri( 
					   this.SiteUri.Scheme + "://www." + this.SiteUri.DnsSafeHost ));
				}
			}
			
			string rootDomain = URLUtility.RootDomain(this.SiteUri.AbsoluteUri);
			
			if (! string.IsNullOrEmpty(rootDomain)) {
				if (this.BestImage == null) { // try with root domain prefix
					FindBestIconForURI( new Uri( this.SiteUri.Scheme + "://" + rootDomain ));
				}
				
				if (this.BestImage == null) { // try with host prefix and www
					FindBestIconForURI( new Uri (this.SiteUri.Scheme + "://www." + rootDomain));
				}
			    }
			
			if (this.RootUriForIco != null) {
				if (this.BestImage == null) { // try with host prefix
					FindBestIconForURI( new Uri( 
					       this.RootUriForIco.Scheme + "://" + this.RootUriForIco.Host) );
				}
				
				if (this.BestImage == null) { // try with host prefix and www
					FindBestIconForURI( new Uri (
						this.RootUriForIco.Scheme + "://www." + this.RootUriForIco.Host));
				}
			}
			
			if (this.BestImage == null) {
				FindBestIconForURI( this.OriginalSiteUri );
			}
			
		}
		
		public void FindBestIconForURI(Uri param_uri)
		{
			// Prepare
			if (param_uri == null) {
				this.myLogger.LogWarn("No url. Stop Find Best Icon");
				return;
			}
	
			Uri testedUri = param_uri;
			
			// Get Links
			GetIconLinksForURI(testedUri);
			
			if (this.Result.ResultCode != FinderResult.RESULT_OK) {
				this.myLogger.LogError("Error while retrieving Icon link for " + 
				                       testedUri.AbsoluteUri);
				return;
			}
			
			// Test Icon URL
			this.myLogger.LogInfo("Test Icon URL");
			this.myLogger.Indent();
			this.BestImage = null;
			foreach ( Uri uriTested in this.ListUriIcons) {
				TestImageUri( uriTested);
			}
			
			// Test default url
			if ( ((this.BestImage != null)
			      && (this.BestImage.Width + this.BestImage.Height < 64))
			      || (this.BestImage == null) ) {
				TestImageUri( new Uri(this.RootUriForIco, "favicon.ico"));
			}
			
			// Try better resolution  (usefull for site like www dot francetvsport dot com
			if ( ((this.BestImage != null)
			      && (this.BestImage.Width + this.BestImage.Height < 64))
			      || (this.BestImage == null) ) {
				TestImageUri( new Uri(this.RootUriForIco, "apple-touch-icon-precomposed.png"));
				TestImageUri( new Uri(this.RootUriForIco, "apple-touch-icon.png"));
			}
			
			this.myLogger.Unindent();
			this.myLogger.LogInfo("End test Icon URI");
			
		}
		
		/// <summary>
		/// Test Image URI and store in Best Image if quality is better 
		/// </summary>
		public void TestImageUri (Uri uriToBeTested)
		{
			this.myLogger.LogDebug("Test " + uriToBeTested.AbsoluteUri);
			byte[] pictureData = null;
			Image downloadedImage = null;
			
			bool imageWasInCache = false;
			
			lock (lckImageCache) {
				if (imageCache.ContainsKey( uriToBeTested ) ) {
					downloadedImage = imageCache[uriToBeTested];
					imageWasInCache = true;
				}
			}
			
			if (!imageWasInCache) {
				try {
					pictureData = client.DownloadData( uriToBeTested);
					pictureData = PreparedIconBytesArrayToBeParsed( pictureData );
						
				   try {
				   	downloadedImage = KeePassLib.Utility.GfxUtil.LoadImage( pictureData );
				   }
				   catch (Exception e) {
				   	this.myLogger.LogError("Error while parsing image " + uriToBeTested.AbsoluteUri + System.Environment.NewLine
				   	                      + e.Message);
				   }
				   
				}
				catch (WebException) {
					this.myLogger.LogError( "Error while getting icon " 
					                       + uriToBeTested.AbsoluteUri);
				}
				catch (NotSupportedException) {
					this.myLogger.LogError( "Not Supported Exception while getting icon " 
					                       + uriToBeTested.AbsoluteUri);
				}
				
				lock (lckImageCache) {
					if (!imageCache.ContainsKey(uriToBeTested)) {
						imageCache.Add(uriToBeTested, downloadedImage);
					}
				}
			}
			
		   if (downloadedImage != null) {
				
				downloadedImage = (Image)downloadedImage.Clone(); 
				  // It is necessary to clone image to avoid concurrent access during post operation.
				  // For example, error should occured if image is resized by several thread in the same time.
	 		    this.myLogger.LogDebug( "Size " + downloadedImage.Width + " x " + downloadedImage.Height );
			    this.ListImageInfo.Add( new ImageInfo( downloadedImage,uriToBeTested.AbsolutePath) );
			    if ( (this.BestImage == null ) ||
			       (CompareImageQuality( downloadedImage, this.BestImage) == 1) ) {
				   	this.BestImage = downloadedImage;
				   	this.IndexOfBestImage = this.ListImageInfo.Count - 1;
				   	this.myLogger.LogInfo("-->stored as the best image");
			    }
		   }
		}
		
		public void GetIconLinks() {
			GetIconLinksForURI(this.SiteUri);
		}
		
		public void GetIconLinksForURI(Uri param_Uri)
		{
			this.myLogger.LogDebug("Start findIconLinks");
			this.myLogger.LogDebug("URL = " + param_Uri.AbsoluteUri);
			
			this.myLogger.Indent();

			Uri testedUri = param_Uri;
			
			// Get HTML
			HtmlAgilityPack.HtmlDocument htmlPage = null;
			Uri fullUri = null;
			
			bool uriInCache = false;
			lock(lckUriRequestCache) {
				if (uriRequestCache.ContainsKey( param_Uri )) {
					CachedURIRequestResult curr = uriRequestCache[param_Uri];
					fullUri = curr.FullUri;
					htmlPage = curr.Html;
					this.NbRequestCacheInvoke++;
					uriInCache = true;
				}
			}
			
			if (!uriInCache) {
				try {
					fullUri = GetURIOfSite(testedUri, out htmlPage);
				}
				catch ( WebException ) {
					this.Result = new FinderResult(FinderResult.RESULT_HTML_NOT_FOUND);
					this.myLogger.Unindent();
					this.myLogger.LogDebug("End findIconLinks");
					return;				
				}
				catch ( Exception ) {
					this.Result = new FinderResult(FinderResult.RESULT_HTML_PARSING);
					this.myLogger.Unindent();
					this.myLogger.LogDebug("End findIconLinks");
					return;
				}
				lock (lckUriRequestCache) {
					if (!uriRequestCache.ContainsKey( param_Uri )) {
						var curr = new CachedURIRequestResult(fullUri, htmlPage);
						uriRequestCache.Add( param_Uri, curr);
					}
				}
			}
		
			if ( htmlPage == null) {
				this.Result = new FinderResult(FinderResult.RESULT_HTML_NOT_FOUND);
				this.myLogger.Unindent();
				this.myLogger.LogDebug("End findIconLinks");
				return;
			}
			this.RootUriForIco = new Uri ( fullUri.Scheme + "://" + fullUri.DnsSafeHost );
			this.myLogger.LogDebug("Root Url = " + this.RootUriForIco.AbsoluteUri);
			
			// Get XPath
			HtmlNode hnn = htmlPage.DocumentNode;
			//this.myLogger.LogDebugIndented(hnn.OuterHtml);
			this.ListUriIcons = new List<Uri>();
			foreach (String currentXpath in xPathToTest) 
			{
				this.myLogger.LogInfo("Testing " + currentXpath);
				this.myLogger.Indent();
				
				HtmlNodeCollection hnc = hnn.SelectNodes(currentXpath);
				if ( (hnc == null) || (hnc.Count == 0) ) {
					this.myLogger.LogInfo("Node not found for xpath " + currentXpath);
				}
				
				else {
					
					foreach (HtmlNode hn in hnc) {
						string relativeUrlPicture = hn.GetAttributeValue("href", "");
						if ( relativeUrlPicture.Length == 0 ) {
							this.myLogger.LogWarn("Zero Length URL found for xpath ");
						}
						else {
							var uriPicture = new Uri(this.RootUriForIco, relativeUrlPicture);
							
							if (!this.ListUriIcons.Exists((Uri myUri) => myUri == uriPicture)) {
								this.ListUriIcons.Add( uriPicture);
							}
							
							this.myLogger.LogDebug("URL = '" + uriPicture.AbsoluteUri + "'");
						}
					}
					
				}
				this.myLogger.Unindent();
			}
			
			this.Result = new FinderResult( FinderResult.RESULT_OK );
			this.myLogger.Unindent();
			this.myLogger.LogDebug("End findIconLinks");
			
		}
		
		
		
		/// <summary>
		/// Compare Quality of two images.
		/// </summary>
		/// <param name="img1"></param>
		/// <param name="img2"></param>
		/// <returns>1 = if img1 better than img2, 0 = equivalent, -1 = worse</returns>
		
		private static int CompareImageQuality(Image img1, Image img2) {
			if ( (img1.Width + img1.Height) > (img2.Width + img2.Height) ) {
				return 1;
			}
			else if ( (img1.Width + img1.Height) == (img2.Width + img2.Height) ) {
				return 0;
			}
			
			return -1;
		}
		
		/// <summary>
        /// Gets a memory stream representing an image from an explicit favicon location.
        /// </summary>
        /// <param name="fullURI">The URI.</param>
        /// <returns></returns>
        private Uri GetURIOfSite(Uri initialURI, out HtmlDocument hdoc) 
        {
        	HtmlWeb hw = new HtmlWeb();
            hw.UserAgent = REQUEST_USER_AGENT;
           
            hdoc = null;
            Uri responseURI = null; 
			
            Uri nextUri = initialURI;
            try
            {
                int counter = 0; // Protection from cyclic redirect 
                do
                {
                    // A cookie container is needed for some sites to work
                    hw.PreRequest += PreRequest_EventHandler;

                    // HtmlWeb.Load will follow 301 and 302 redirects to alternate URIs
                   hdoc = hw.Load(nextUri.AbsoluteUri);
                   responseURI = hw.ResponseUri;

                    // Old school meta refreshes need to parsed
                    nextUri = GetMetaRefreshLink(responseURI, hdoc);
                    counter++;
                } while (nextUri != null && counter < 16); // Sixteen redirects would be more than enough.


            }
            catch (Exception e) {
            	this.myLogger.LogError("error while getting url " + nextUri.AbsoluteUri 
            	                       + " " + e.Message);
            }
            return responseURI;
		}
        
        private Uri GetMetaRefreshLink(Uri uri, HtmlAgilityPack.HtmlDocument hdoc)
        {
            HtmlNodeCollection metas = hdoc.DocumentNode.SelectNodes("/html/head/meta");
            string redirect = null;

            if (metas == null)
            {
                return null;
            }

            for (int i = 0; i < metas.Count; i++)
            {
                HtmlNode node = metas[i];
                try
                {
                    HtmlAttribute httpeq = node.Attributes["http-equiv"];
                    HtmlAttribute content = node.Attributes["content"];
                    if (httpeq.Value.ToLower().Equals("location") || httpeq.Value.ToLower().Equals("refresh"))
                    {
                        if (content.Value.ToLower().Contains("url"))
                        {
                            Match match = Regex.Match(content.Value.ToLower(), @".*?url[\s=]*(\S+)");
                            if (match.Success)
                            {
                                redirect = match.Captures[0].ToString();
                                redirect = match.Groups[1].ToString();
                            }
                        }

                    }
                }
                catch (Exception) { }
            }
            
            if (String.IsNullOrEmpty(redirect))
            {
                return null;
            }

            return new Uri(uri, redirect);
        }
       
        
		/// <summary>
		/// Prepare bytes array representing an icon to be parsed by libraries
		///  (provided by KP and MS)
		/// </summary>
		/// <description>
		/// Pad with zero if length of array is not sufficient according to header.
		/// </description>
		/// <param name="batbp">Byte array to be parsed</param>
		/// <returns></returns>
		/// Inspired by the method KeePassLib.Utility.GsfxUtil.UnpackIco of KeePass v2.30
		private byte[] PreparedIconBytesArrayToBeParsed(byte[] batbp) {
			if (batbp == null) { 
				myLogger.LogWarn("PreparedIconBytesArrayToBeParsed: Byte array to be prepared is Null");
				return batbp;
			}

			const int SizeICONDIR = 6;
			const int SizeICONDIRENTRY = 16;

			if (BitConverter.ToInt32(new byte[] { 1, 2, 3, 4 },
			                         0) != 0x04030201) {
				myLogger.LogWarn("PreparedIconBytesArrayToBeParsed: Not Little Endian detected");
			}

			if (batbp.Length < SizeICONDIR) return null;
			if (BitConverter.ToUInt16(batbp, 0) != 0)	return batbp; // Reserved, 0 if icon
			if (BitConverter.ToUInt16(batbp, 2) != 1)	return batbp; // ICO type, 1 if icon

			int nbIcon = BitConverter.ToUInt16(batbp, 4);
			if (nbIcon < 0) { 
				myLogger.LogWarn("PreparedIconBytesArrayToBeParsed: nb of Icons is not positive");;
			}

			int cbDir = SizeICONDIR + (nbIcon * SizeICONDIRENTRY);
			if (batbp.Length < cbDir)
				return null;

			int iOffset = SizeICONDIR;
			int padLength = 0;
			for (int i = 0; i < nbIcon; ++i) {
				int dataSize = BitConverter.ToInt32(batbp, iOffset + 8);
				int iStartImgData = BitConverter.ToInt32(batbp, iOffset + 12);
				
				if ((iStartImgData + dataSize) > batbp.Length) {
					padLength = iStartImgData + dataSize - batbp.Length;
					
				}
				
				iOffset += SizeICONDIRENTRY;
			}
			
			if (padLength == 0 ) {
				return batbp;
			}
			else {
				myLogger.LogWarn("PreparedIconBytesArrayToBeParsed: Padding file with "
				                 + padLength.ToString() + " leading zero(s)");;
			
				var padArray = new byte[padLength];
				for (int ii = 0; ii < padLength; ii++) {
					padArray[ii] = 0;
				}
				var result = new byte[batbp.Length + padLength];
				Array.Copy(batbp, 0, result, 0, batbp.Length);
				Array.Copy(padArray, 0, result, batbp.Length, padLength);
				
				return result;				
			}
			
		}
        
		private bool PreRequest_EventHandler(HttpWebRequest request)
        {
            request.CookieContainer = new System.Net.CookieContainer();
            request.Accept = REQUEST_HEADER_ACCEPT;
            request.Headers.Add(HttpRequestHeader.AcceptLanguage, "*");
            request.Timeout = REQUEST_TIMEOUT;
            
            return true;
        }
		
		private sealed class MyLogger {
			
			private int nbIndent;
			private String indent;
			private BestIconFinder myBestIf;
			private int logLevel; // ERROR = 1, WARN = 2, INFO = 3, DEBUG = 4
			
			public MyLogger(BestIconFinder paramMyBestIf) {
				this.nbIndent = 0;
				indent = "";
				this.myBestIf = paramMyBestIf;
				this.logLevel = 4;
			}
			
			
			public MyLogger(BestIconFinder paramMyBestIf, int paramLogLevel):this(paramMyBestIf) {
				logLevel = paramLogLevel;
			}
			
			public void Reset() {
				this.nbIndent = 0;
				indent = "";
			}
			
			public void Indent() {
				nbIndent++;
				updateIndent();
			}
			
			public void Unindent() {
				nbIndent--;
				updateIndent();
			}
			
			private void updateIndent() {
				indent = "";
				indent = indent.PadRight(nbIndent * 2, ' ');
			}
			
			public void LogInfo(String what) {
				if ( this.logLevel >= 3)
					myBestIf.Details += indent + what + System.Environment.NewLine;
			}
			
			public void LogDebug(String what) {
				if ( this.logLevel >= 4)
					myBestIf.Details += indent + what + System.Environment.NewLine;
			}
			
			public void LogDebugIndented(String what) {
				if ( this.logLevel >= 4) {
					what = what.Replace("\n", System.Environment.NewLine + indent);
					
					myBestIf.Details += indent + what + System.Environment.NewLine;
				}
			}
			
			public void LogWarn(String what) {
				if ( this.logLevel >= 2)
					myBestIf.Details += indent + what + System.Environment.NewLine;
			}
			
			public void LogError(String what) {
				if ( this.logLevel >= 1)
					myBestIf.Details += indent + "ERROR - " + what + System.Environment.NewLine;
			}
		}
		
		public void Dispose() {
			if (this.BestImage != null) {
				this.BestImage.Dispose();
			}
		}
		
	
//       // Set "UnsafeHeaderParsing" flags. Usefull to retrieve information from several sites
//       // try www dot geni dot com    
//       // to be used with "using System.Reflection;" in header
//       public static bool SetAllowUnsafeHeaderParsing(bool value)
//		{
//		  //Get the assembly that contains the internal class
//		  Assembly aNetAssembly = Assembly.GetAssembly(typeof(System.Net.Configuration.SettingsSection));
//		  if (aNetAssembly != null)
//		  {
//		    //Use the assembly in order to get the internal type for the internal class
//		    Type aSettingsType = aNetAssembly.GetType("System.Net.Configuration.SettingsSectionInternal");
//		    if (aSettingsType != null)
//		    {
//		      //Use the internal static property to get an instance of the internal settings class.
//		      //If the static instance isn't created allready the property will create it for us.
//		      object anInstance = aSettingsType.InvokeMember("Section",
//		        BindingFlags.Static | BindingFlags.GetProperty | BindingFlags.NonPublic, null, null, new object[] { });
//		
//		      if (anInstance != null)
//		      {
//		        //Locate the private bool field that tells the framework is unsafe header parsing should be allowed or not
//		        FieldInfo aUseUnsafeHeaderParsing = aSettingsType.GetField("useUnsafeHeaderParsing", BindingFlags.NonPublic | BindingFlags.Instance);
//		        if (aUseUnsafeHeaderParsing != null)
//		        {
//		          aUseUnsafeHeaderParsing.SetValue(anInstance, value);
//		          return true;
//		        }
//		      }
//		    }
//		  }
//		  return false;
//		} 
//
	
		public class MyWebClient : WebClient
		{
			private readonly CookieContainer m_container = new CookieContainer();
	
			protected override WebRequest GetWebRequest(Uri address)
			{
                  // https://stackoverflow.com/questions/33761919/tls-1-2-in-net-framework-4-0
                  // enhance chance to get favicon by setting some specifics parameters in request
                  // Sample of web site and parameters
                  //    Header Accept  - usefull for www dot akamai dot com
                WebRequest request = base.GetWebRequest(address);
				var webRequest = request as HttpWebRequest;
				if (webRequest != null) {
					webRequest.Accept = REQUEST_HEADER_ACCEPT;
		            webRequest.UserAgent = REQUEST_USER_AGENT;
		            webRequest.Headers.Add(HttpRequestHeader.AcceptLanguage, "*");
		            webRequest.Timeout = REQUEST_TIMEOUT;
					webRequest.CookieContainer = m_container;
                }
				return request;
			}
		}
	
	
		private sealed class CachedURIRequestResult {
			public Uri FullUri {get; private set; }
			public HtmlDocument Html {get; private set; }
			
			public CachedURIRequestResult (Uri fullUri, HtmlDocument html) {
				this.FullUri = fullUri;
				this.Html = html;
			}
		
		}	
		
	}
	
	public sealed class FinderResult {
		
		public const int RESULT_NOT_YET_SEARCH = 0;
		public const int RESULT_OK = 1;
		public const int RESULT_HTML_NOT_FOUND = 2;
		public const int RESULT_HTML_PARSING = 3;
		public const int RESULT_NO_URL = 4;
		
		public int ResultCode {get; private set; }
		public FinderResult (int code) 
		{
			ResultCode = code;
		}
		
		public override string ToString() {
			switch (this.ResultCode) {
				case FinderResult.RESULT_NOT_YET_SEARCH : return "Not Yet Search";
				case RESULT_OK : return "OK";
				case RESULT_HTML_NOT_FOUND : return "Html not found";
				case RESULT_HTML_PARSING : return "Errot Html parsing";
				case RESULT_NO_URL : return "No URL";
				default : return "";
			}
		}
	}

}
