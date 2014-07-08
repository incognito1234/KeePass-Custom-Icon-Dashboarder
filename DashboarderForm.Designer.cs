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
			this.spc_mainSplitter = new System.Windows.Forms.SplitContainer();
			this.tlp_topright = new System.Windows.Forms.TableLayoutPanel();
			this.tst_viewIcons = new System.Windows.Forms.ToolStrip();
			this.tsl_nbIcons = new System.Windows.Forms.ToolStripLabel();
			this.tlp_bottomLeft = new System.Windows.Forms.TableLayoutPanel();
			this.gbo_actions = new System.Windows.Forms.GroupBox();
			this.flp_buttons = new System.Windows.Forms.FlowLayoutPanel();
			this.btn_removeIcons = new System.Windows.Forms.Button();
			this.btn_ModifyIcon = new System.Windows.Forms.Button();
			this.cb_allIconsSelection = new System.Windows.Forms.CheckBox();
			this.tlp_right = new System.Windows.Forms.TableLayoutPanel();
			this.tlp_upperRight = new System.Windows.Forms.TableLayoutPanel();
			this.spc_right = new System.Windows.Forms.SplitContainer();
			this.tlp_usedEntries = new System.Windows.Forms.TableLayoutPanel();
			this.tlp_usedGroups = new System.Windows.Forms.TableLayoutPanel();
			this.bto_OK = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pbo_selectedIcon)).BeginInit();
			this.spc_mainSplitter.Panel1.SuspendLayout();
			this.spc_mainSplitter.Panel2.SuspendLayout();
			this.spc_mainSplitter.SuspendLayout();
			this.tlp_topright.SuspendLayout();
			this.tst_viewIcons.SuspendLayout();
			this.tlp_bottomLeft.SuspendLayout();
			this.gbo_actions.SuspendLayout();
			this.flp_buttons.SuspendLayout();
			this.tlp_right.SuspendLayout();
			this.tlp_upperRight.SuspendLayout();
			this.spc_right.Panel1.SuspendLayout();
			this.spc_right.Panel2.SuspendLayout();
			this.spc_right.SuspendLayout();
			this.tlp_usedEntries.SuspendLayout();
			this.tlp_usedGroups.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_lvViewIcon
			// 
			this.m_lvViewIcon.AllowColumnReorder = true;
			this.m_lvViewIcon.CheckBoxes = true;
			this.m_lvViewIcon.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_lvViewIcon.FullRowSelect = true;
			this.m_lvViewIcon.HideSelection = false;
			this.m_lvViewIcon.Location = new System.Drawing.Point(15, 28);
			this.m_lvViewIcon.Margin = new System.Windows.Forms.Padding(15, 3, 15, 2);
			this.m_lvViewIcon.Name = "m_lvViewIcon";
			this.m_lvViewIcon.Size = new System.Drawing.Size(378, 389);
			this.m_lvViewIcon.TabIndex = 0;
			this.m_lvViewIcon.UseCompatibleStateImageBehavior = false;
			this.m_lvViewIcon.View = System.Windows.Forms.View.Details;
			this.m_lvViewIcon.SelectedIndexChanged += new System.EventHandler(this.OnLvViewIconSelectedIndexChanged);
			// 
			// lbl_title
			// 
			this.lbl_title.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbl_title.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_title.Location = new System.Drawing.Point(3, 0);
			this.lbl_title.Name = "lbl_title";
			this.lbl_title.Size = new System.Drawing.Size(402, 25);
			this.lbl_title.TabIndex = 1;
			this.lbl_title.Text = "Custom Icon Statistics";
			this.lbl_title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// m_lvUsedEntries
			// 
			this.m_lvUsedEntries.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_lvUsedEntries.Location = new System.Drawing.Point(28, 36);
			this.m_lvUsedEntries.Margin = new System.Windows.Forms.Padding(3, 15, 15, 15);
			this.m_lvUsedEntries.Name = "m_lvUsedEntries";
			this.m_lvUsedEntries.Size = new System.Drawing.Size(403, 142);
			this.m_lvUsedEntries.TabIndex = 2;
			this.m_lvUsedEntries.UseCompatibleStateImageBehavior = false;
			this.m_lvUsedEntries.View = System.Windows.Forms.View.Details;
			// 
			// m_lvUsedGroups
			// 
			this.m_lvUsedGroups.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_lvUsedGroups.Location = new System.Drawing.Point(28, 36);
			this.m_lvUsedGroups.Margin = new System.Windows.Forms.Padding(3, 15, 15, 15);
			this.m_lvUsedGroups.Name = "m_lvUsedGroups";
			this.m_lvUsedGroups.Size = new System.Drawing.Size(403, 159);
			this.m_lvUsedGroups.TabIndex = 3;
			this.m_lvUsedGroups.UseCompatibleStateImageBehavior = false;
			this.m_lvUsedGroups.View = System.Windows.Forms.View.Details;
			// 
			// lbl_usedinentries
			// 
			this.lbl_usedinentries.AutoSize = true;
			this.tlp_usedEntries.SetColumnSpan(this.lbl_usedinentries, 2);
			this.lbl_usedinentries.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbl_usedinentries.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_usedinentries.Location = new System.Drawing.Point(3, 0);
			this.lbl_usedinentries.Name = "lbl_usedinentries";
			this.lbl_usedinentries.Size = new System.Drawing.Size(440, 21);
			this.lbl_usedinentries.TabIndex = 4;
			this.lbl_usedinentries.Text = "Used by entries";
			this.lbl_usedinentries.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lbl_usedInGroups
			// 
			this.tlp_usedGroups.SetColumnSpan(this.lbl_usedInGroups, 2);
			this.lbl_usedInGroups.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbl_usedInGroups.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_usedInGroups.Location = new System.Drawing.Point(3, 0);
			this.lbl_usedInGroups.Name = "lbl_usedInGroups";
			this.lbl_usedInGroups.Size = new System.Drawing.Size(440, 21);
			this.lbl_usedInGroups.TabIndex = 5;
			this.lbl_usedInGroups.Text = "Used by groups";
			this.lbl_usedInGroups.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pbo_selectedIcon
			// 
			this.pbo_selectedIcon.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pbo_selectedIcon.InitialImage = null;
			this.pbo_selectedIcon.Location = new System.Drawing.Point(228, 3);
			this.pbo_selectedIcon.Name = "pbo_selectedIcon";
			this.pbo_selectedIcon.Size = new System.Drawing.Size(36, 36);
			this.pbo_selectedIcon.TabIndex = 6;
			this.pbo_selectedIcon.TabStop = false;
			// 
			// lbl_selectedIcon
			// 
			this.lbl_selectedIcon.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbl_selectedIcon.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_selectedIcon.Location = new System.Drawing.Point(3, 0);
			this.lbl_selectedIcon.Name = "lbl_selectedIcon";
			this.lbl_selectedIcon.Size = new System.Drawing.Size(219, 42);
			this.lbl_selectedIcon.TabIndex = 7;
			this.lbl_selectedIcon.Text = "Selected icon : ";
			this.lbl_selectedIcon.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// spc_mainSplitter
			// 
			this.spc_mainSplitter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.spc_mainSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.spc_mainSplitter.Location = new System.Drawing.Point(0, 0);
			this.spc_mainSplitter.Name = "spc_mainSplitter";
			// 
			// spc_mainSplitter.Panel1
			// 
			this.spc_mainSplitter.Panel1.Controls.Add(this.tlp_topright);
			this.spc_mainSplitter.Panel1MinSize = 110;
			// 
			// spc_mainSplitter.Panel2
			// 
			this.spc_mainSplitter.Panel2.Controls.Add(this.tlp_right);
			this.spc_mainSplitter.Panel2MinSize = 110;
			this.spc_mainSplitter.Size = new System.Drawing.Size(876, 503);
			this.spc_mainSplitter.SplitterDistance = 412;
			this.spc_mainSplitter.TabIndex = 8;
			// 
			// tlp_topright
			// 
			this.tlp_topright.ColumnCount = 1;
			this.tlp_topright.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlp_topright.Controls.Add(this.m_lvViewIcon, 0, 1);
			this.tlp_topright.Controls.Add(this.lbl_title, 0, 0);
			this.tlp_topright.Controls.Add(this.tst_viewIcons, 0, 3);
			this.tlp_topright.Controls.Add(this.tlp_bottomLeft, 0, 2);
			this.tlp_topright.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlp_topright.Location = new System.Drawing.Point(0, 0);
			this.tlp_topright.Name = "tlp_topright";
			this.tlp_topright.RowCount = 4;
			this.tlp_topright.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tlp_topright.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlp_topright.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
			this.tlp_topright.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tlp_topright.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tlp_topright.Size = new System.Drawing.Size(408, 499);
			this.tlp_topright.TabIndex = 0;
			// 
			// tst_viewIcons
			// 
			this.tst_viewIcons.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tst_viewIcons.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.tsl_nbIcons});
			this.tst_viewIcons.Location = new System.Drawing.Point(0, 479);
			this.tst_viewIcons.Name = "tst_viewIcons";
			this.tst_viewIcons.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.tst_viewIcons.Size = new System.Drawing.Size(408, 20);
			this.tst_viewIcons.TabIndex = 3;
			this.tst_viewIcons.Text = "toolStrip1";
			// 
			// tsl_nbIcons
			// 
			this.tsl_nbIcons.Name = "tsl_nbIcons";
			this.tsl_nbIcons.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.tsl_nbIcons.Size = new System.Drawing.Size(52, 17);
			this.tsl_nbIcons.Text = "nb Icons";
			// 
			// tlp_bottomLeft
			// 
			this.tlp_bottomLeft.ColumnCount = 2;
			this.tlp_bottomLeft.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tlp_bottomLeft.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tlp_bottomLeft.Controls.Add(this.gbo_actions, 1, 0);
			this.tlp_bottomLeft.Controls.Add(this.cb_allIconsSelection, 0, 0);
			this.tlp_bottomLeft.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlp_bottomLeft.Location = new System.Drawing.Point(3, 422);
			this.tlp_bottomLeft.Name = "tlp_bottomLeft";
			this.tlp_bottomLeft.RowCount = 1;
			this.tlp_bottomLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tlp_bottomLeft.Size = new System.Drawing.Size(402, 54);
			this.tlp_bottomLeft.TabIndex = 5;
			// 
			// gbo_actions
			// 
			this.gbo_actions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.gbo_actions.Controls.Add(this.flp_buttons);
			this.gbo_actions.Location = new System.Drawing.Point(223, 0);
			this.gbo_actions.Margin = new System.Windows.Forms.Padding(0);
			this.gbo_actions.MinimumSize = new System.Drawing.Size(175, 50);
			this.gbo_actions.Name = "gbo_actions";
			this.gbo_actions.Size = new System.Drawing.Size(179, 51);
			this.gbo_actions.TabIndex = 4;
			this.gbo_actions.TabStop = false;
			this.gbo_actions.Text = "&Actions on Checked Icons";
			// 
			// flp_buttons
			// 
			this.flp_buttons.Controls.Add(this.btn_removeIcons);
			this.flp_buttons.Controls.Add(this.btn_ModifyIcon);
			this.flp_buttons.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flp_buttons.Location = new System.Drawing.Point(3, 16);
			this.flp_buttons.Margin = new System.Windows.Forms.Padding(0);
			this.flp_buttons.Name = "flp_buttons";
			this.flp_buttons.Size = new System.Drawing.Size(173, 32);
			this.flp_buttons.TabIndex = 3;
			// 
			// btn_removeIcons
			// 
			this.btn_removeIcons.Location = new System.Drawing.Point(3, 3);
			this.btn_removeIcons.Name = "btn_removeIcons";
			this.btn_removeIcons.Size = new System.Drawing.Size(78, 23);
			this.btn_removeIcons.TabIndex = 1;
			this.btn_removeIcons.Text = "&Remove";
			this.btn_removeIcons.UseVisualStyleBackColor = true;
			this.btn_removeIcons.Click += new System.EventHandler(this.OnRemoveIconsClick);
			// 
			// btn_ModifyIcon
			// 
			this.btn_ModifyIcon.Location = new System.Drawing.Point(87, 3);
			this.btn_ModifyIcon.Name = "btn_ModifyIcon";
			this.btn_ModifyIcon.Size = new System.Drawing.Size(72, 23);
			this.btn_ModifyIcon.TabIndex = 0;
			this.btn_ModifyIcon.Text = "&Modify";
			this.btn_ModifyIcon.UseVisualStyleBackColor = true;
			this.btn_ModifyIcon.Click += new System.EventHandler(this.OnModifyIconClick);
			// 
			// cb_allIconsSelection
			// 
			this.cb_allIconsSelection.Location = new System.Drawing.Point(15, 0);
			this.cb_allIconsSelection.Margin = new System.Windows.Forms.Padding(15, 0, 0, 0);
			this.cb_allIconsSelection.Name = "cb_allIconsSelection";
			this.cb_allIconsSelection.Size = new System.Drawing.Size(125, 25);
			this.cb_allIconsSelection.TabIndex = 2;
			this.cb_allIconsSelection.Text = "&Check/Uncheck All";
			this.cb_allIconsSelection.UseVisualStyleBackColor = true;
			// 
			// tlp_right
			// 
			this.tlp_right.ColumnCount = 1;
			this.tlp_right.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlp_right.Controls.Add(this.tlp_upperRight, 0, 0);
			this.tlp_right.Controls.Add(this.spc_right, 0, 1);
			this.tlp_right.Controls.Add(this.bto_OK, 0, 2);
			this.tlp_right.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlp_right.Location = new System.Drawing.Point(0, 0);
			this.tlp_right.Name = "tlp_right";
			this.tlp_right.RowCount = 3;
			this.tlp_right.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
			this.tlp_right.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlp_right.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tlp_right.Size = new System.Drawing.Size(456, 499);
			this.tlp_right.TabIndex = 0;
			// 
			// tlp_upperRight
			// 
			this.tlp_upperRight.AutoSize = true;
			this.tlp_upperRight.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tlp_upperRight.ColumnCount = 2;
			this.tlp_upperRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tlp_upperRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tlp_upperRight.Controls.Add(this.lbl_selectedIcon, 0, 0);
			this.tlp_upperRight.Controls.Add(this.pbo_selectedIcon, 1, 0);
			this.tlp_upperRight.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlp_upperRight.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.AddColumns;
			this.tlp_upperRight.Location = new System.Drawing.Point(3, 3);
			this.tlp_upperRight.Name = "tlp_upperRight";
			this.tlp_upperRight.RowCount = 1;
			this.tlp_upperRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tlp_upperRight.Size = new System.Drawing.Size(450, 42);
			this.tlp_upperRight.TabIndex = 1;
			// 
			// spc_right
			// 
			this.spc_right.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.spc_right.Dock = System.Windows.Forms.DockStyle.Fill;
			this.spc_right.Location = new System.Drawing.Point(3, 51);
			this.spc_right.Name = "spc_right";
			this.spc_right.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// spc_right.Panel1
			// 
			this.spc_right.Panel1.Controls.Add(this.tlp_usedEntries);
			// 
			// spc_right.Panel2
			// 
			this.spc_right.Panel2.Controls.Add(this.tlp_usedGroups);
			this.spc_right.Size = new System.Drawing.Size(450, 415);
			this.spc_right.SplitterDistance = 197;
			this.spc_right.TabIndex = 2;
			// 
			// tlp_usedEntries
			// 
			this.tlp_usedEntries.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tlp_usedEntries.ColumnCount = 2;
			this.tlp_usedEntries.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tlp_usedEntries.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlp_usedEntries.Controls.Add(this.lbl_usedinentries, 0, 0);
			this.tlp_usedEntries.Controls.Add(this.m_lvUsedEntries, 1, 1);
			this.tlp_usedEntries.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlp_usedEntries.Location = new System.Drawing.Point(0, 0);
			this.tlp_usedEntries.Name = "tlp_usedEntries";
			this.tlp_usedEntries.RowCount = 2;
			this.tlp_usedEntries.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
			this.tlp_usedEntries.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tlp_usedEntries.Size = new System.Drawing.Size(446, 193);
			this.tlp_usedEntries.TabIndex = 0;
			// 
			// tlp_usedGroups
			// 
			this.tlp_usedGroups.ColumnCount = 2;
			this.tlp_usedGroups.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tlp_usedGroups.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlp_usedGroups.Controls.Add(this.m_lvUsedGroups, 1, 1);
			this.tlp_usedGroups.Controls.Add(this.lbl_usedInGroups, 0, 0);
			this.tlp_usedGroups.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlp_usedGroups.Location = new System.Drawing.Point(0, 0);
			this.tlp_usedGroups.Name = "tlp_usedGroups";
			this.tlp_usedGroups.RowCount = 2;
			this.tlp_usedGroups.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
			this.tlp_usedGroups.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlp_usedGroups.Size = new System.Drawing.Size(446, 210);
			this.tlp_usedGroups.TabIndex = 0;
			// 
			// bto_OK
			// 
			this.bto_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.bto_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.bto_OK.Location = new System.Drawing.Point(351, 472);
			this.bto_OK.Margin = new System.Windows.Forms.Padding(3, 3, 30, 3);
			this.bto_OK.Name = "bto_OK";
			this.bto_OK.Size = new System.Drawing.Size(75, 23);
			this.bto_OK.TabIndex = 6;
			this.bto_OK.Text = "&Close";
			this.bto_OK.UseVisualStyleBackColor = true;
			// 
			// DashboarderForm
			// 
			this.ClientSize = new System.Drawing.Size(876, 503);
			this.Controls.Add(this.spc_mainSplitter);
			this.Icon = global::CustomIconDashboarderPlugin.Resource.KeePass;
			this.Name = "DashboarderForm";
			this.Text = "Custom Icon Dashboard";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnFormClosed);
			this.Load += new System.EventHandler(this.DashboarderLoad);
			((System.ComponentModel.ISupportInitialize)(this.pbo_selectedIcon)).EndInit();
			this.spc_mainSplitter.Panel1.ResumeLayout(false);
			this.spc_mainSplitter.Panel2.ResumeLayout(false);
			this.spc_mainSplitter.ResumeLayout(false);
			this.tlp_topright.ResumeLayout(false);
			this.tlp_topright.PerformLayout();
			this.tst_viewIcons.ResumeLayout(false);
			this.tst_viewIcons.PerformLayout();
			this.tlp_bottomLeft.ResumeLayout(false);
			this.gbo_actions.ResumeLayout(false);
			this.flp_buttons.ResumeLayout(false);
			this.tlp_right.ResumeLayout(false);
			this.tlp_right.PerformLayout();
			this.tlp_upperRight.ResumeLayout(false);
			this.spc_right.Panel1.ResumeLayout(false);
			this.spc_right.Panel2.ResumeLayout(false);
			this.spc_right.ResumeLayout(false);
			this.tlp_usedEntries.ResumeLayout(false);
			this.tlp_usedEntries.PerformLayout();
			this.tlp_usedGroups.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.TableLayoutPanel tlp_bottomLeft;
		private System.Windows.Forms.ToolStripLabel tsl_nbIcons;
		private System.Windows.Forms.ToolStrip tst_viewIcons;
		private System.Windows.Forms.TableLayoutPanel tlp_topright;
		private System.Windows.Forms.GroupBox gbo_actions;
		private System.Windows.Forms.Button btn_removeIcons;
		private System.Windows.Forms.Button btn_ModifyIcon;
		private System.Windows.Forms.FlowLayoutPanel flp_buttons;
		private System.Windows.Forms.CheckBox cb_allIconsSelection;
		private System.Windows.Forms.SplitContainer spc_right;
		private System.Windows.Forms.TableLayoutPanel tlp_usedGroups;
		private System.Windows.Forms.Button bto_OK;
		private System.Windows.Forms.TableLayoutPanel tlp_upperRight;
		private System.Windows.Forms.TableLayoutPanel tlp_right;
		private System.Windows.Forms.TableLayoutPanel tlp_usedEntries;
		private System.Windows.Forms.SplitContainer spc_mainSplitter;
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
