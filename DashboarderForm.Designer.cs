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
namespace CustomIconDashboarderPlugin
{
	partial class DashboarderForm 
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.m_lvViewIcon = new KeePass.UI.CustomListViewEx();
			this.lbl_title = new System.Windows.Forms.Label();
			this.m_lvUsedEntries = new KeePass.UI.CustomListViewEx();
			this.m_lvUsedGroups = new KeePass.UI.CustomListViewEx();
			this.lbl_usedinentries = new System.Windows.Forms.Label();
			this.lbl_usedInGroups = new System.Windows.Forms.Label();
			this.pbo_selectedIcon = new System.Windows.Forms.PictureBox();
			this.lbl_selectedIcon = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pbo_selectedIcon)).BeginInit();
			this.SuspendLayout();
			// 
			// m_lvViewIcon
			// 
			this.m_lvViewIcon.AllowColumnReorder = true;
			this.m_lvViewIcon.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.m_lvViewIcon.HideSelection = false;
			this.m_lvViewIcon.Location = new System.Drawing.Point(12, 35);
			this.m_lvViewIcon.MultiSelect = false;
			this.m_lvViewIcon.Name = "m_lvViewIcon";
			this.m_lvViewIcon.Size = new System.Drawing.Size(225, 406);
			this.m_lvViewIcon.TabIndex = 0;
			this.m_lvViewIcon.UseCompatibleStateImageBehavior = false;
			this.m_lvViewIcon.View = System.Windows.Forms.View.Details;
			this.m_lvViewIcon.SelectedIndexChanged += new System.EventHandler(this.M_lvViewIconSelectedIndexChanged);
			// 
			// lbl_title
			// 
			this.lbl_title.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_title.Location = new System.Drawing.Point(12, 9);
			this.lbl_title.Name = "lbl_title";
			this.lbl_title.Size = new System.Drawing.Size(155, 23);
			this.lbl_title.TabIndex = 1;
			this.lbl_title.Text = "Custom Icons Statistics";
			// 
			// m_lvUsedEntries
			// 
			this.m_lvUsedEntries.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.m_lvUsedEntries.Location = new System.Drawing.Point(263, 106);
			this.m_lvUsedEntries.Name = "m_lvUsedEntries";
			this.m_lvUsedEntries.Size = new System.Drawing.Size(382, 150);
			this.m_lvUsedEntries.TabIndex = 2;
			this.m_lvUsedEntries.UseCompatibleStateImageBehavior = false;
			this.m_lvUsedEntries.View = System.Windows.Forms.View.Details;
			// 
			// m_lvUsedGroups
			// 
			this.m_lvUsedGroups.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.m_lvUsedGroups.Location = new System.Drawing.Point(263, 291);
			this.m_lvUsedGroups.Name = "m_lvUsedGroups";
			this.m_lvUsedGroups.Size = new System.Drawing.Size(382, 150);
			this.m_lvUsedGroups.TabIndex = 3;
			this.m_lvUsedGroups.UseCompatibleStateImageBehavior = false;
			this.m_lvUsedGroups.View = System.Windows.Forms.View.Details;
			// 
			// lbl_usedinentries
			// 
			this.lbl_usedinentries.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_usedinentries.Location = new System.Drawing.Point(255, 80);
			this.lbl_usedinentries.Name = "lbl_usedinentries";
			this.lbl_usedinentries.Size = new System.Drawing.Size(100, 23);
			this.lbl_usedinentries.TabIndex = 4;
			this.lbl_usedinentries.Text = "Used in entries";
			// 
			// lbl_usedInGroups
			// 
			this.lbl_usedInGroups.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_usedInGroups.Location = new System.Drawing.Point(255, 268);
			this.lbl_usedInGroups.Name = "lbl_usedInGroups";
			this.lbl_usedInGroups.Size = new System.Drawing.Size(100, 20);
			this.lbl_usedInGroups.TabIndex = 5;
			this.lbl_usedInGroups.Text = "Used in groups";
			// 
			// pbo_selectedIcon
			// 
			this.pbo_selectedIcon.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pbo_selectedIcon.InitialImage = null;
			this.pbo_selectedIcon.Location = new System.Drawing.Point(371, 9);
			this.pbo_selectedIcon.Name = "pbo_selectedIcon";
			this.pbo_selectedIcon.Size = new System.Drawing.Size(36, 36);
			this.pbo_selectedIcon.TabIndex = 6;
			this.pbo_selectedIcon.TabStop = false;
			// 
			// lbl_selectedIcon
			// 
			this.lbl_selectedIcon.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_selectedIcon.Location = new System.Drawing.Point(255, 25);
			this.lbl_selectedIcon.Name = "lbl_selectedIcon";
			this.lbl_selectedIcon.Size = new System.Drawing.Size(110, 23);
			this.lbl_selectedIcon.TabIndex = 7;
			this.lbl_selectedIcon.Text = "Selected icon : ";
			// 
			// DashboarderForm
			// 
			this.ClientSize = new System.Drawing.Size(655, 453);
			this.Controls.Add(this.pbo_selectedIcon);
			this.Controls.Add(this.lbl_selectedIcon);
			this.Controls.Add(this.m_lvUsedEntries);
			this.Controls.Add(this.lbl_usedInGroups);
			this.Controls.Add(this.lbl_usedinentries);
			this.Controls.Add(this.m_lvUsedGroups);
			this.Controls.Add(this.lbl_title);
			this.Controls.Add(this.m_lvViewIcon);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = global::CustomIconDashboarderPlugin.Resource.KeePass;
			this.Name = "DashboarderForm";
			this.Text = "Custom Icon Dashboard";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnFormClosed);
			this.Load += new System.EventHandler(this.DashboarderLoad);
			((System.ComponentModel.ISupportInitialize)(this.pbo_selectedIcon)).EndInit();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Label lbl_selectedIcon;
		private System.Windows.Forms.PictureBox pbo_selectedIcon;
		
		private System.Windows.Forms.Label lbl_usedInGroups;
		private System.Windows.Forms.Label lbl_usedinentries;
		private KeePass.UI.CustomListViewEx m_lvUsedGroups;
		private KeePass.UI.CustomListViewEx m_lvUsedEntries;
		private System.Windows.Forms.Label lbl_title;
		private KeePass.UI.CustomListViewEx m_lvViewIcon;

		
		void DashboarderLoad(object sender, System.EventArgs e)
		{
			this.OnFormLoad(sender, e);
		}
		
	
		
	}
}
