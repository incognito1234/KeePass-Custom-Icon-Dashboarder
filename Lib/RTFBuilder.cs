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
using System.Windows.Forms;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Globalization;

using System.Collections.Generic;

/*
 * LomsonLib.UI
 * 
 *     Version 1.1
 *      See ListViewLayoutManager.cs for release notes
 */
 
namespace LomsonLib.UI
{
	//
	// This code is derived from RichTextBuilder class of KeePass Project v2.26
	// Some feature has been added:
	//     * Can add new Font
	//     * To Be Continued
	//
	public sealed class RTFBuilder
	{
		private StringBuilder m_sb = new StringBuilder();

		private static List<RtfbTag> m_vTags;
		
		private List<OwnTag> m_ownTags = null;
		
		private static Random  m_myRandom;
		private static bool    m_monoWorkaroundBug586901;
		public static void InitializeRTFBuilder(Random r, bool mwab586901) {
			m_myRandom = r;
			m_monoWorkaroundBug586901 = mwab586901;
		}
		
		private sealed class RtfbTag
		{
			public string IdCode { get; private set; }
			public string RtfCode { get; private set; }
			public bool StartTag { get; private set; }
			public FontStyle Style { get; private set; }

			public RtfbTag(string strId, string strRtf, bool bStartTag, FontStyle fs)
			{
				if(string.IsNullOrEmpty(strId)) strId = GenerateRandomIdCode();
				this.IdCode = strId;

				this.RtfCode = strRtf;
				this.StartTag = bStartTag;
				this.Style = fs;
			}

			
		}
		
		private sealed class OwnTag
		{
			public string IdCode { get; private set; }
			public string RtfCode { get; private set; }
			public bool StartTag { get; private set; }
			public string ExternalCode { get; private set;}

			public OwnTag(string strId, string strRtf, bool bStartTag, String externalCode)
			{
				if(string.IsNullOrEmpty(strId)) strId = GenerateRandomIdCode();
				this.IdCode = strId;
				this.RtfCode = strRtf;
				this.StartTag = bStartTag;
				this.ExternalCode = externalCode;
			}

		} 
		
		
		internal static string GenerateRandomIdCode()
			{
				Debug.Assert( m_myRandom != null);
				StringBuilder sb = new StringBuilder(14);
				for(int i = 0; i < 12; ++i)
				{
					int n = m_myRandom.Next(62);
					if(n < 26) sb.Append((char)('A' + n));
					else if(n < 52) sb.Append((char)('a' + (n - 26)));
					else sb.Append((char)('0' + (n - 52)));
				}
				return sb.ToString();
			}

		private Font m_fDefault;
		public Font DefaultFont
		{
			get { return m_fDefault; }
			set { m_fDefault = value; }
		}

		public RTFBuilder()
		{
			EnsureInitializedStatic();
			m_ownTags = new List<OwnTag>();
		}

		private static void EnsureInitializedStatic()
		{
			if(m_vTags != null) return;

			List<RtfbTag> l = new List<RtfbTag>();
			l.Add(new RtfbTag(null, "\\b ", true, FontStyle.Bold));
			l.Add(new RtfbTag(null, "\\b0 ", false, FontStyle.Bold));
			l.Add(new RtfbTag(null, "\\i ", true, FontStyle.Italic));
			l.Add(new RtfbTag(null, "\\i0 ", false, FontStyle.Italic));
			l.Add(new RtfbTag(null, "\\ul ", true, FontStyle.Underline));
			l.Add(new RtfbTag(null, "\\ul0 ", false, FontStyle.Underline));
			l.Add(new RtfbTag(null, "\\strike ", true, FontStyle.Strikeout));
			l.Add(new RtfbTag(null, "\\strike0 ", false, FontStyle.Strikeout));
			m_vTags = l;
		}
		

		public static KeyValuePair<string, string> GetStyleIdCodes(FontStyle fs)
		{
			string strL = null, strR = null;

			foreach(RtfbTag rTag in m_vTags)
			{
				if(rTag.Style == fs)
				{
					if(rTag.StartTag) strL = rTag.IdCode;
					else strR = rTag.IdCode;
				}
			}

			return new KeyValuePair<string, string>(strL, strR);
		}
		
		public KeyValuePair<string, string> GetStyleIdOwnCodes(string externalCode)
		{
			string strL = null, strR = null;

			foreach(OwnTag rTag in m_ownTags)
			{
				if(externalCode == rTag.ExternalCode )
				{
					if(rTag.StartTag) strL = rTag.IdCode;
					else strR = rTag.IdCode;
				}
			}

			return new KeyValuePair<string, string>(strL, strR);
		}
		
		public void AddOwnTag(string externalCode, string rtfStartCode, string rtfStopCode ) 
		{
			m_ownTags.Add(new OwnTag(null, rtfStartCode, true, externalCode ));
			if (!string.IsNullOrEmpty(rtfStopCode) ) {
				m_ownTags.Add(new OwnTag(null, rtfStopCode, false, externalCode ));
			}
		}

		public void Append(string str)
		{
			m_sb.Append(str);
		}

		public void AppendLine()
		{
			m_sb.AppendLine();
		}

		public void AppendLine(string str)
		{
			m_sb.AppendLine(str);
		}

		public void Append(string str, FontStyle fs)
		{
			Append(str, fs, null, null, null, null);
		}

		public void Append(string str, FontStyle fs, string strOuterPrefix,
		                   string strInnerPrefix, string strInnerSuffix, string strOuterSuffix)
		{
			KeyValuePair<string, string> kvpTags = GetStyleIdCodes(fs);

			if(!string.IsNullOrEmpty(strOuterPrefix)) m_sb.Append(strOuterPrefix);

			if(!string.IsNullOrEmpty(kvpTags.Key)) m_sb.Append(kvpTags.Key);
			if(!string.IsNullOrEmpty(strInnerPrefix)) m_sb.Append(strInnerPrefix);
			m_sb.Append(str);
			if(!string.IsNullOrEmpty(strInnerSuffix)) m_sb.Append(strInnerSuffix);
			if(!string.IsNullOrEmpty(kvpTags.Value)) m_sb.Append(kvpTags.Value);

			if(!string.IsNullOrEmpty(strOuterSuffix)) m_sb.Append(strOuterSuffix);
		}
		
		/// <summary>
		/// Append Own tag (only start code)
		/// </summary>
		public void AppendOwnTag(string externalCode)
		{
			KeyValuePair<string, string> kvpTags = GetStyleIdOwnCodes( externalCode );

			if(!string.IsNullOrEmpty(kvpTags.Key)) m_sb.Append(kvpTags.Key);
		}
		
		
		public void AppendOwnTagged(string str, string externalCode, string strOuterPrefix,
		                   string strInnerPrefix, string strInnerSuffix, string strOuterSuffix)
		{
			KeyValuePair<string, string> kvpTags = GetStyleIdOwnCodes( externalCode );

			if(!string.IsNullOrEmpty(strOuterPrefix)) m_sb.Append(strOuterPrefix);

			if(!string.IsNullOrEmpty(kvpTags.Key)) m_sb.Append(kvpTags.Key);
			if(!string.IsNullOrEmpty(strInnerPrefix)) m_sb.Append(strInnerPrefix);
			m_sb.Append(str);
			if(!string.IsNullOrEmpty(strInnerSuffix)) m_sb.Append(strInnerSuffix);
			if(!string.IsNullOrEmpty(kvpTags.Value)) m_sb.Append(kvpTags.Value);

			if(!string.IsNullOrEmpty(strOuterSuffix)) m_sb.Append(strOuterSuffix);
			
		}

		
		public void AppendOwnTaggedLine(string str, string externalCode, string strOuterPrefix,
		                   string strInnerPrefix, string strInnerSuffix, string strOuterSuffix) 
		{
			AppendOwnTagged(str, externalCode,strOuterPrefix, strInnerPrefix, strInnerSuffix, strOuterSuffix);
			m_sb.AppendLine();
		}
		
		public void AppendLine(string str, FontStyle fs, string strOuterPrefix,
		                       string strInnerPrefix, string strInnerSuffix, string strOuterSuffix)
		{
			Append(str, fs, strOuterPrefix, strInnerPrefix, strInnerSuffix, strOuterSuffix);
			m_sb.AppendLine();
		}

		public void AppendLine(string str, FontStyle fs)
		{
			Append(str, fs);
			m_sb.AppendLine();
		}

		private static RichTextBox CreateOpRtb()
		{
			RichTextBox rtbOp = new RichTextBox();
			rtbOp.Visible = false; // Ensure invisibility
			rtbOp.DetectUrls = false;
			rtbOp.HideSelection = true;
			rtbOp.Multiline = true;
			rtbOp.WordWrap = false;

			return rtbOp;
		}

		public void Build(RichTextBox rtb)
		{
			if(rtb == null) throw new ArgumentNullException("rtb");

			RichTextBox rtbOp = CreateOpRtb();
			string strText = m_sb.ToString();

			Dictionary<char, string> dEnc = new Dictionary<char, string>();
			if(m_monoWorkaroundBug586901)
			{
				StringBuilder sbEnc = new StringBuilder();
				for(int i = 0; i < strText.Length; ++i)
				{
					char ch = strText[i];
					if((int)ch <= 255) sbEnc.Append(ch);
					else
					{
						string strCharEnc;
						if(!dEnc.TryGetValue(ch, out strCharEnc))
						{
							strCharEnc = GenerateRandomIdCode();
							dEnc[ch] = strCharEnc;
						}
						sbEnc.Append(strCharEnc);
					}
				}
				strText = sbEnc.ToString();
			}

			rtbOp.Text = strText;
			Debug.Assert(rtbOp.Text == strText); // Test committed

			if(m_fDefault != null)
			{
				rtbOp.Select(0, rtbOp.TextLength);
				rtbOp.SelectionFont = m_fDefault;
			}

			string strRtf = rtbOp.Rtf;
			rtbOp.Dispose();

			foreach(KeyValuePair<char, string> kvpEnc in dEnc)
			{
				strRtf = strRtf.Replace(kvpEnc.Value,
				                        RtfEncodeChar(kvpEnc.Key));
			}
			foreach(RtfbTag rTag in m_vTags)
			{
				strRtf = strRtf.Replace(rTag.IdCode, rTag.RtfCode);
			}
			foreach(OwnTag oTag in m_ownTags)
			{
				strRtf = strRtf.Replace(oTag.IdCode, oTag.RtfCode);
			}

			rtb.Rtf = strRtf;
		}
		
		// From StrUtil of KeePass v2.26
		public static string RtfEncodeChar(char ch)
		{
			// Unicode character values must be encoded using
			// 16-bit numbers (decimal); Unicode values greater
			// than 32767 must be expressed as negative numbers
			short sh = (short)ch;
			return ("\\u" + sh.ToString(NumberFormatInfo.InvariantInfo) + "?");
		}
		
	}
}
