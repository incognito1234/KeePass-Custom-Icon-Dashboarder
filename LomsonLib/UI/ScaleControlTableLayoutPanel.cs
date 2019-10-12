/*
  LomsonLib - Library to help management of various object types in program 
  developed in c#.
  
 Copyright (C) 2014 Jareth Lomson <jareth.lomson@gmail.com>
 
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
using System.Drawing;
using System.Windows.Forms;

/*
 * LomsonLib.UI
 * 
 *     Version 1.0
 *      See ListViewLayoutManager.cs for release notes
 * 
 */
 

namespace LomsonLib.UI
{
	/// <summary>
	/// TableLayoutPanel with autoScaling that can be switched off
	/// </summary>
	public class ScaleControlTableLayoutPanel: TableLayoutPanel
	{
		private readonly bool my_ScaleChildren;
		protected override bool ScaleChildren {
			get {
				return my_ScaleChildren;
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		public bool ScaleWidth {
			get; set;
		}
		
		public bool ScaleHeight {
			get; set;
		}
		
		protected override Rectangle GetScaledBounds(
			Rectangle bounds,
			SizeF factor,
			BoundsSpecified specified) {
			int newWidth =
				ScaleWidth ? Convert.ToInt32(bounds.Width * factor.Width):bounds.Width;
			int newHeight =
				ScaleHeight ? Convert.ToInt32(bounds.Height * factor.Height):bounds.Height;
		
			return new Rectangle(bounds.X, bounds.Y, newWidth, newHeight);
		}
		
		/// <summary>
		/// New ScaleControlTableLayoutPanel with :<br/>
		/// <list type="bullet">
		///   <item><description>ScaleChildren that returns false</description></item>
		///   <item><description>Automatic scale of width</description></item>
		///   <item><description>Automatic scale of height</description></item>
		/// </list>
		/// </summary>
		public ScaleControlTableLayoutPanel()
		{
			my_ScaleChildren = false;
			ScaleWidth  = true;
			ScaleHeight = true;
		}
		
		public ScaleControlTableLayoutPanel(bool myScaleChildren)
		{
			my_ScaleChildren = myScaleChildren;
		}
	}
}
