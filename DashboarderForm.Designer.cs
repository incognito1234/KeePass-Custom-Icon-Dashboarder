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
			this.pbo_selectedIcon128 = new System.Windows.Forms.PictureBox();
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
			this.lbl_originalSize = new System.Windows.Forms.Label();
			this.lbl_selectedIcon = new System.Windows.Forms.Label();
			this.lbl_128x128 = new System.Windows.Forms.Label();
			this.lbl_64x64 = new System.Windows.Forms.Label();
			this.lbl_32x32 = new System.Windows.Forms.Label();
			this.lbl_16x16 = new System.Windows.Forms.Label();
			this.scltp_icons = new LomsonLib.UI.ScaleControlTableLayoutPanel();
			this.pbo_selectedIcon16 = new System.Windows.Forms.PictureBox();
			this.pbo_selectedIcon32 = new System.Windows.Forms.PictureBox();
			this.pbo_selectedIcon64 = new System.Windows.Forms.PictureBox();
			this.spc_right = new System.Windows.Forms.SplitContainer();
			this.tlp_usedEntries = new System.Windows.Forms.TableLayoutPanel();
			this.tlp_usedGroups = new System.Windows.Forms.TableLayoutPanel();
			this.bto_OK = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pbo_selectedIcon128)).BeginInit();
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
			this.scltp_icons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbo_selectedIcon16)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbo_selectedIcon32)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbo_selectedIcon64)).BeginInit();
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
			this.m_lvUsedEntries.Size = new System.Drawing.Size(403, 67);
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
			this.m_lvUsedGroups.Size = new System.Drawing.Size(403, 82);
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
			// pbo_selectedIcon128
			// 
			this.pbo_selectedIcon128.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.pbo_selectedIcon128.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.pbo_selectedIcon128.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pbo_selectedIcon128.InitialImage = null;
			this.pbo_selectedIcon128.Location = new System.Drawing.Point(46, 11);
			this.pbo_selectedIcon128.Name = "pbo_selectedIcon128";
			this.pbo_selectedIcon128.Size = new System.Drawing.Size(132, 132);
			this.pbo_selectedIcon128.TabIndex = 6;
			this.pbo_selectedIcon128.TabStop = false;
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
			this.tlp_right.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlp_right.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlp_right.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tlp_right.Size = new System.Drawing.Size(456, 499);
			this.tlp_right.TabIndex = 0;
			// 
			// tlp_upperRight
			// 
			this.tlp_upperRight.AutoSize = true;
			this.tlp_upperRight.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tlp_upperRight.ColumnCount = 4;
			this.tlp_upperRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tlp_upperRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tlp_upperRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.tlp_upperRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.tlp_upperRight.Controls.Add(this.lbl_originalSize, 1, 0);
			this.tlp_upperRight.Controls.Add(this.lbl_selectedIcon, 0, 0);
			this.tlp_upperRight.Controls.Add(this.lbl_128x128, 0, 2);
			this.tlp_upperRight.Controls.Add(this.lbl_64x64, 1, 2);
			this.tlp_upperRight.Controls.Add(this.lbl_32x32, 2, 2);
			this.tlp_upperRight.Controls.Add(this.lbl_16x16, 3, 2);
			this.tlp_upperRight.Controls.Add(this.scltp_icons, 0, 1);
			this.tlp_upperRight.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlp_upperRight.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.AddColumns;
			this.tlp_upperRight.Location = new System.Drawing.Point(3, 3);
			this.tlp_upperRight.Name = "tlp_upperRight";
			this.tlp_upperRight.RowCount = 3;
			this.tlp_upperRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tlp_upperRight.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlp_upperRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tlp_upperRight.Size = new System.Drawing.Size(450, 194);
			this.tlp_upperRight.TabIndex = 1;
			// 
			// lbl_originalSize
			// 
			this.lbl_originalSize.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.tlp_upperRight.SetColumnSpan(this.lbl_originalSize, 3);
			this.lbl_originalSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_originalSize.Location = new System.Drawing.Point(228, 0);
			this.lbl_originalSize.Margin = new System.Windows.Forms.Padding(3, 0, 30, 0);
			this.lbl_originalSize.Name = "lbl_originalSize";
			this.lbl_originalSize.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lbl_originalSize.Size = new System.Drawing.Size(192, 20);
			this.lbl_originalSize.TabIndex = 16;
			this.lbl_originalSize.Text = "Original Size :";
			this.lbl_originalSize.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lbl_selectedIcon
			// 
			this.lbl_selectedIcon.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_selectedIcon.Location = new System.Drawing.Point(3, 0);
			this.lbl_selectedIcon.Name = "lbl_selectedIcon";
			this.lbl_selectedIcon.Size = new System.Drawing.Size(100, 20);
			this.lbl_selectedIcon.TabIndex = 17;
			this.lbl_selectedIcon.Text = "Selected Icon";
			// 
			// lbl_128x128
			// 
			this.lbl_128x128.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.lbl_128x128.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_128x128.Location = new System.Drawing.Point(3, 174);
			this.lbl_128x128.Name = "lbl_128x128";
			this.lbl_128x128.Size = new System.Drawing.Size(219, 20);
			this.lbl_128x128.TabIndex = 10;
			this.lbl_128x128.Text = "128x128";
			this.lbl_128x128.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// lbl_64x64
			// 
			this.lbl_64x64.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.lbl_64x64.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_64x64.Location = new System.Drawing.Point(228, 174);
			this.lbl_64x64.Name = "lbl_64x64";
			this.lbl_64x64.Size = new System.Drawing.Size(106, 20);
			this.lbl_64x64.TabIndex = 13;
			this.lbl_64x64.Text = "64x64";
			this.lbl_64x64.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// lbl_32x32
			// 
			this.lbl_32x32.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.lbl_32x32.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_32x32.Location = new System.Drawing.Point(340, 174);
			this.lbl_32x32.Name = "lbl_32x32";
			this.lbl_32x32.Size = new System.Drawing.Size(50, 20);
			this.lbl_32x32.TabIndex = 9;
			this.lbl_32x32.Text = "32x32";
			this.lbl_32x32.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.lbl_32x32.UseCompatibleTextRendering = true;
			// 
			// lbl_16x16
			// 
			this.lbl_16x16.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.lbl_16x16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_16x16.Location = new System.Drawing.Point(396, 174);
			this.lbl_16x16.Name = "lbl_16x16";
			this.lbl_16x16.Size = new System.Drawing.Size(51, 20);
			this.lbl_16x16.TabIndex = 15;
			this.lbl_16x16.Text = "16x16";
			this.lbl_16x16.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// scltp_icons
			// 
			this.scltp_icons.ColumnCount = 4;
			this.tlp_upperRight.SetColumnSpan(this.scltp_icons, 4);
			this.scltp_icons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.scltp_icons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.scltp_icons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.scltp_icons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.scltp_icons.Controls.Add(this.pbo_selectedIcon16, 3, 0);
			this.scltp_icons.Controls.Add(this.pbo_selectedIcon128, 0, 0);
			this.scltp_icons.Controls.Add(this.pbo_selectedIcon32, 2, 0);
			this.scltp_icons.Controls.Add(this.pbo_selectedIcon64, 1, 0);
			this.scltp_icons.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scltp_icons.Location = new System.Drawing.Point(0, 20);
			this.scltp_icons.Margin = new System.Windows.Forms.Padding(0);
			this.scltp_icons.Name = "scltp_icons";
			this.scltp_icons.RowCount = 1;
			this.scltp_icons.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.scltp_icons.ScaleHeight = false;
			this.scltp_icons.ScaleWidth = true;
			this.scltp_icons.Size = new System.Drawing.Size(450, 154);
			this.scltp_icons.TabIndex = 18;
			// 
			// pbo_selectedIcon16
			// 
			this.pbo_selectedIcon16.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.pbo_selectedIcon16.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.pbo_selectedIcon16.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pbo_selectedIcon16.Location = new System.Drawing.Point(411, 67);
			this.pbo_selectedIcon16.Name = "pbo_selectedIcon16";
			this.pbo_selectedIcon16.Size = new System.Drawing.Size(20, 20);
			this.pbo_selectedIcon16.TabIndex = 14;
			this.pbo_selectedIcon16.TabStop = false;
			// 
			// pbo_selectedIcon32
			// 
			this.pbo_selectedIcon32.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.pbo_selectedIcon32.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.pbo_selectedIcon32.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pbo_selectedIcon32.Location = new System.Drawing.Point(347, 59);
			this.pbo_selectedIcon32.Name = "pbo_selectedIcon32";
			this.pbo_selectedIcon32.Size = new System.Drawing.Size(36, 36);
			this.pbo_selectedIcon32.TabIndex = 11;
			this.pbo_selectedIcon32.TabStop = false;
			// 
			// pbo_selectedIcon64
			// 
			this.pbo_selectedIcon64.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.pbo_selectedIcon64.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.pbo_selectedIcon64.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pbo_selectedIcon64.Location = new System.Drawing.Point(247, 43);
			this.pbo_selectedIcon64.Name = "pbo_selectedIcon64";
			this.pbo_selectedIcon64.Size = new System.Drawing.Size(68, 68);
			this.pbo_selectedIcon64.TabIndex = 12;
			this.pbo_selectedIcon64.TabStop = false;
			// 
			// spc_right
			// 
			this.spc_right.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.spc_right.Dock = System.Windows.Forms.DockStyle.Fill;
			this.spc_right.Location = new System.Drawing.Point(3, 203);
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
			this.spc_right.Size = new System.Drawing.Size(450, 263);
			this.spc_right.SplitterDistance = 122;
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
			this.tlp_usedEntries.Size = new System.Drawing.Size(446, 118);
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
			this.tlp_usedGroups.Size = new System.Drawing.Size(446, 133);
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
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(876, 503);
			this.Controls.Add(this.spc_mainSplitter);
			this.Icon = global::CustomIconDashboarderPlugin.Resource.KeePass;
			this.Name = "DashboarderForm";
			this.Text = "Custom Icon Dashboard";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnFormClosed);
			this.Load += new System.EventHandler(this.DashboarderLoad);
			((System.ComponentModel.ISupportInitialize)(this.pbo_selectedIcon128)).EndInit();
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
			this.scltp_icons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbo_selectedIcon16)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbo_selectedIcon32)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbo_selectedIcon64)).EndInit();
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
		private System.Windows.Forms.PictureBox pbo_selectedIcon128;
		
		private System.Windows.Forms.Label lbl_usedInGroups;
		private System.Windows.Forms.Label lbl_usedinentries;
		private KeePass.UI.CustomListViewEx m_lvUsedGroups;
		private KeePass.UI.CustomListViewEx m_lvUsedEntries;
		private System.Windows.Forms.Label lbl_title;
		private KeePass.UI.CustomListViewEx m_lvViewIcon;
		private System.Windows.Forms.Label lbl_32x32;
		private System.Windows.Forms.PictureBox pbo_selectedIcon32;
		private System.Windows.Forms.Label lbl_128x128;
		private System.Windows.Forms.PictureBox pbo_selectedIcon64;
		private System.Windows.Forms.Label lbl_64x64;
		private System.Windows.Forms.PictureBox pbo_selectedIcon16;
		private System.Windows.Forms.Label lbl_16x16;
		private System.Windows.Forms.Label lbl_originalSize;
		private System.Windows.Forms.Label lbl_selectedIcon;
		private LomsonLib.UI.ScaleControlTableLayoutPanel scltp_icons;

		
		void DashboarderLoad(object sender, System.EventArgs e)
		{
			this.OnFormLoad(sender, e);
		}
		
	
		
	}
}
