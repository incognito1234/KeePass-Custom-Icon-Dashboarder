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
			this.OnFormDispose();
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
			this.m_lvUsedEntries = new KeePass.UI.CustomListViewEx();
			this.m_lvUsedGroups = new KeePass.UI.CustomListViewEx();
			this.lbl_usedinentries = new System.Windows.Forms.Label();
			this.lbl_usedInGroups = new System.Windows.Forms.Label();
			this.pbo_selectedIcon128 = new System.Windows.Forms.PictureBox();
			this.spc_mainSplitter = new System.Windows.Forms.SplitContainer();
			this.tlp_topright = new System.Windows.Forms.TableLayoutPanel();
			this.tco_lists = new System.Windows.Forms.TabControl();
			this.tpa_listAllIcons = new System.Windows.Forms.TabPage();
			this.tlp_allIcons = new System.Windows.Forms.TableLayoutPanel();
			this.cb_allIconsSelection = new System.Windows.Forms.CheckBox();
			this.flp_iconActions = new System.Windows.Forms.FlowLayoutPanel();
			this.cbo_iconActionSelector = new System.Windows.Forms.ComboBox();
			this.btn_performIconAction = new System.Windows.Forms.Button();
			this.tpa_listAllEntries = new System.Windows.Forms.TabPage();
			this.tlp_allEntries = new System.Windows.Forms.TableLayoutPanel();
			this.flp_entryAction = new System.Windows.Forms.FlowLayoutPanel();
			this.cbo_entryActionSelector = new System.Windows.Forms.ComboBox();
			this.btn_performEntryAction = new System.Windows.Forms.Button();
			this.m_lvAllEntries = new System.Windows.Forms.ListView();
			this.cb_allEntriesSelection = new System.Windows.Forms.CheckBox();
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
			this.bto_OK = new System.Windows.Forms.Button();
			this.tco_right = new System.Windows.Forms.TabControl();
			this.tpa_IconUsage = new System.Windows.Forms.TabPage();
			this.spc_right = new System.Windows.Forms.SplitContainer();
			this.tlp_usedEntries = new System.Windows.Forms.TableLayoutPanel();
			this.tlp_usedGroups = new System.Windows.Forms.TableLayoutPanel();
			this.tpa_DownloadResult = new System.Windows.Forms.TabPage();
			this.tlp_downloadResult = new System.Windows.Forms.TableLayoutPanel();
			this.scltp_downloadResult = new LomsonLib.UI.ScaleControlTableLayoutPanel();
			this.pbo_downloadedIcon128 = new System.Windows.Forms.PictureBox();
			this.pbo_downloadedIcon64 = new System.Windows.Forms.PictureBox();
			this.pbo_downloadedIcon16 = new System.Windows.Forms.PictureBox();
			this.pbo_downloadedIcon32 = new System.Windows.Forms.PictureBox();
			this.lbl_newSize = new System.Windows.Forms.Label();
			this.m_lvDownloadResult = new System.Windows.Forms.ListView();
			this.lbl_alldownloadedicons = new System.Windows.Forms.Label();
			this.rbu_icons = new System.Windows.Forms.RadioButton();
			this.rbu_details = new System.Windows.Forms.RadioButton();
			this.tpa_Debug = new System.Windows.Forms.TabPage();
			this.rtb_details = new System.Windows.Forms.RichTextBox();
			this.tlp_main = new System.Windows.Forms.TableLayoutPanel();
			((System.ComponentModel.ISupportInitialize)(this.pbo_selectedIcon128)).BeginInit();
			this.spc_mainSplitter.Panel1.SuspendLayout();
			this.spc_mainSplitter.Panel2.SuspendLayout();
			this.spc_mainSplitter.SuspendLayout();
			this.tlp_topright.SuspendLayout();
			this.tco_lists.SuspendLayout();
			this.tpa_listAllIcons.SuspendLayout();
			this.tlp_allIcons.SuspendLayout();
			this.flp_iconActions.SuspendLayout();
			this.tpa_listAllEntries.SuspendLayout();
			this.tlp_allEntries.SuspendLayout();
			this.flp_entryAction.SuspendLayout();
			this.tlp_right.SuspendLayout();
			this.tlp_upperRight.SuspendLayout();
			this.scltp_icons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbo_selectedIcon16)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbo_selectedIcon32)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbo_selectedIcon64)).BeginInit();
			this.tco_right.SuspendLayout();
			this.tpa_IconUsage.SuspendLayout();
			this.spc_right.Panel1.SuspendLayout();
			this.spc_right.Panel2.SuspendLayout();
			this.spc_right.SuspendLayout();
			this.tlp_usedEntries.SuspendLayout();
			this.tlp_usedGroups.SuspendLayout();
			this.tpa_DownloadResult.SuspendLayout();
			this.tlp_downloadResult.SuspendLayout();
			this.scltp_downloadResult.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbo_downloadedIcon128)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbo_downloadedIcon64)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbo_downloadedIcon16)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbo_downloadedIcon32)).BeginInit();
			this.tpa_Debug.SuspendLayout();
			this.tlp_main.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_lvViewIcon
			// 
			this.m_lvViewIcon.AllowColumnReorder = true;
			this.m_lvViewIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.m_lvViewIcon.CheckBoxes = true;
			this.m_lvViewIcon.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_lvViewIcon.FullRowSelect = true;
			this.m_lvViewIcon.GridLines = true;
			this.m_lvViewIcon.HideSelection = false;
			this.m_lvViewIcon.Location = new System.Drawing.Point(0, 0);
			this.m_lvViewIcon.Margin = new System.Windows.Forms.Padding(0);
			this.m_lvViewIcon.Name = "m_lvViewIcon";
			this.m_lvViewIcon.Size = new System.Drawing.Size(316, 464);
			this.m_lvViewIcon.TabIndex = 0;
			this.m_lvViewIcon.UseCompatibleStateImageBehavior = false;
			this.m_lvViewIcon.View = System.Windows.Forms.View.Details;
			this.m_lvViewIcon.SelectedIndexChanged += new System.EventHandler(this.OnLvViewIconSelectedIndexChanged);
			// 
			// m_lvUsedEntries
			// 
			this.m_lvUsedEntries.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_lvUsedEntries.Location = new System.Drawing.Point(28, 36);
			this.m_lvUsedEntries.Margin = new System.Windows.Forms.Padding(3, 15, 15, 15);
			this.m_lvUsedEntries.Name = "m_lvUsedEntries";
			this.m_lvUsedEntries.Size = new System.Drawing.Size(465, 80);
			this.m_lvUsedEntries.TabIndex = 0;
			this.m_lvUsedEntries.UseCompatibleStateImageBehavior = false;
			this.m_lvUsedEntries.View = System.Windows.Forms.View.Details;
			// 
			// m_lvUsedGroups
			// 
			this.m_lvUsedGroups.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_lvUsedGroups.Location = new System.Drawing.Point(28, 36);
			this.m_lvUsedGroups.Margin = new System.Windows.Forms.Padding(3, 15, 15, 15);
			this.m_lvUsedGroups.Name = "m_lvUsedGroups";
			this.m_lvUsedGroups.Size = new System.Drawing.Size(465, 102);
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
			this.lbl_usedinentries.Size = new System.Drawing.Size(502, 21);
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
			this.lbl_usedInGroups.Size = new System.Drawing.Size(502, 21);
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
			this.pbo_selectedIcon128.Location = new System.Drawing.Point(67, 11);
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
			this.spc_mainSplitter.Margin = new System.Windows.Forms.Padding(0);
			this.spc_mainSplitter.Name = "spc_mainSplitter";
			// 
			// spc_mainSplitter.Panel1
			// 
			this.spc_mainSplitter.Panel1.Controls.Add(this.tlp_topright);
			this.spc_mainSplitter.Panel1MinSize = 300;
			// 
			// spc_mainSplitter.Panel2
			// 
			this.spc_mainSplitter.Panel2.Controls.Add(this.tlp_right);
			this.spc_mainSplitter.Panel2MinSize = 110;
			this.spc_mainSplitter.Size = new System.Drawing.Size(876, 557);
			this.spc_mainSplitter.SplitterDistance = 328;
			this.spc_mainSplitter.TabIndex = 8;
			// 
			// tlp_topright
			// 
			this.tlp_topright.BackColor = System.Drawing.SystemColors.Control;
			this.tlp_topright.ColumnCount = 1;
			this.tlp_topright.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlp_topright.Controls.Add(this.tco_lists, 0, 0);
			this.tlp_topright.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlp_topright.Location = new System.Drawing.Point(0, 0);
			this.tlp_topright.Name = "tlp_topright";
			this.tlp_topright.RowCount = 1;
			this.tlp_topright.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlp_topright.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 553F));
			this.tlp_topright.Size = new System.Drawing.Size(324, 553);
			this.tlp_topright.TabIndex = 0;
			// 
			// tco_lists
			// 
			this.tco_lists.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
			this.tco_lists.Controls.Add(this.tpa_listAllIcons);
			this.tco_lists.Controls.Add(this.tpa_listAllEntries);
			this.tco_lists.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tco_lists.Location = new System.Drawing.Point(0, 0);
			this.tco_lists.Margin = new System.Windows.Forms.Padding(0);
			this.tco_lists.Name = "tco_lists";
			this.tco_lists.SelectedIndex = 0;
			this.tco_lists.Size = new System.Drawing.Size(324, 553);
			this.tco_lists.TabIndex = 3;
			// 
			// tpa_listAllIcons
			// 
			this.tpa_listAllIcons.BackColor = System.Drawing.SystemColors.Control;
			this.tpa_listAllIcons.Controls.Add(this.tlp_allIcons);
			this.tpa_listAllIcons.Location = new System.Drawing.Point(4, 25);
			this.tpa_listAllIcons.Margin = new System.Windows.Forms.Padding(0);
			this.tpa_listAllIcons.Name = "tpa_listAllIcons";
			this.tpa_listAllIcons.Size = new System.Drawing.Size(316, 524);
			this.tpa_listAllIcons.TabIndex = 0;
			this.tpa_listAllIcons.Text = "All Icons";
			// 
			// tlp_allIcons
			// 
			this.tlp_allIcons.ColumnCount = 1;
			this.tlp_allIcons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlp_allIcons.Controls.Add(this.m_lvViewIcon, 0, 0);
			this.tlp_allIcons.Controls.Add(this.cb_allIconsSelection, 0, 1);
			this.tlp_allIcons.Controls.Add(this.flp_iconActions, 0, 2);
			this.tlp_allIcons.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlp_allIcons.Location = new System.Drawing.Point(0, 0);
			this.tlp_allIcons.Name = "tlp_allIcons";
			this.tlp_allIcons.RowCount = 3;
			this.tlp_allIcons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlp_allIcons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tlp_allIcons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tlp_allIcons.Size = new System.Drawing.Size(316, 524);
			this.tlp_allIcons.TabIndex = 0;
			// 
			// cb_allIconsSelection
			// 
			this.cb_allIconsSelection.Location = new System.Drawing.Point(3, 464);
			this.cb_allIconsSelection.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.cb_allIconsSelection.Name = "cb_allIconsSelection";
			this.cb_allIconsSelection.Size = new System.Drawing.Size(129, 29);
			this.cb_allIconsSelection.TabIndex = 2;
			this.cb_allIconsSelection.Text = "&Check/Uncheck All";
			this.cb_allIconsSelection.UseVisualStyleBackColor = true;
			// 
			// flp_iconActions
			// 
			this.flp_iconActions.Controls.Add(this.cbo_iconActionSelector);
			this.flp_iconActions.Controls.Add(this.btn_performIconAction);
			this.flp_iconActions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flp_iconActions.Location = new System.Drawing.Point(3, 497);
			this.flp_iconActions.Name = "flp_iconActions";
			this.flp_iconActions.Size = new System.Drawing.Size(310, 24);
			this.flp_iconActions.TabIndex = 3;
			// 
			// cbo_iconActionSelector
			// 
			this.cbo_iconActionSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo_iconActionSelector.FormattingEnabled = true;
			this.cbo_iconActionSelector.Items.AddRange(new object[] {
			"-- Choose an action --",
			"Modify",
			"Remove",
			"Download Best Icon",
			"Pick Best Icon"});
			this.cbo_iconActionSelector.Location = new System.Drawing.Point(3, 3);
			this.cbo_iconActionSelector.Name = "cbo_iconActionSelector";
			this.cbo_iconActionSelector.Size = new System.Drawing.Size(140, 21);
			this.cbo_iconActionSelector.TabIndex = 3;
			this.cbo_iconActionSelector.SelectedIndexChanged += new System.EventHandler(this.OnIconActionSelectorSelectedIndexChanged);
			// 
			// btn_performIconAction
			// 
			this.btn_performIconAction.Location = new System.Drawing.Point(149, 3);
			this.btn_performIconAction.Name = "btn_performIconAction";
			this.btn_performIconAction.Size = new System.Drawing.Size(61, 21);
			this.btn_performIconAction.TabIndex = 4;
			this.btn_performIconAction.Text = "Go";
			this.btn_performIconAction.UseVisualStyleBackColor = true;
			this.btn_performIconAction.Click += new System.EventHandler(this.OnPerformIconActionClick);
			// 
			// tpa_listAllEntries
			// 
			this.tpa_listAllEntries.Controls.Add(this.tlp_allEntries);
			this.tpa_listAllEntries.Location = new System.Drawing.Point(4, 25);
			this.tpa_listAllEntries.Name = "tpa_listAllEntries";
			this.tpa_listAllEntries.Padding = new System.Windows.Forms.Padding(3);
			this.tpa_listAllEntries.Size = new System.Drawing.Size(316, 524);
			this.tpa_listAllEntries.TabIndex = 1;
			this.tpa_listAllEntries.Text = "All Entries";
			this.tpa_listAllEntries.UseVisualStyleBackColor = true;
			// 
			// tlp_allEntries
			// 
			this.tlp_allEntries.ColumnCount = 1;
			this.tlp_allEntries.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlp_allEntries.Controls.Add(this.flp_entryAction, 0, 2);
			this.tlp_allEntries.Controls.Add(this.m_lvAllEntries, 0, 0);
			this.tlp_allEntries.Controls.Add(this.cb_allEntriesSelection, 0, 1);
			this.tlp_allEntries.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlp_allEntries.Location = new System.Drawing.Point(3, 3);
			this.tlp_allEntries.Name = "tlp_allEntries";
			this.tlp_allEntries.RowCount = 3;
			this.tlp_allEntries.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlp_allEntries.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tlp_allEntries.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tlp_allEntries.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tlp_allEntries.Size = new System.Drawing.Size(310, 518);
			this.tlp_allEntries.TabIndex = 0;
			// 
			// flp_entryAction
			// 
			this.flp_entryAction.Controls.Add(this.cbo_entryActionSelector);
			this.flp_entryAction.Controls.Add(this.btn_performEntryAction);
			this.flp_entryAction.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flp_entryAction.Location = new System.Drawing.Point(3, 491);
			this.flp_entryAction.Name = "flp_entryAction";
			this.flp_entryAction.Size = new System.Drawing.Size(304, 24);
			this.flp_entryAction.TabIndex = 4;
			// 
			// cbo_entryActionSelector
			// 
			this.cbo_entryActionSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo_entryActionSelector.FormattingEnabled = true;
			this.cbo_entryActionSelector.Items.AddRange(new object[] {
			"-- Choose an action --",
			"Modify",
			"Remove",
			"Download Best Icon",
			"Pick Best Icon"});
			this.cbo_entryActionSelector.Location = new System.Drawing.Point(3, 3);
			this.cbo_entryActionSelector.Name = "cbo_entryActionSelector";
			this.cbo_entryActionSelector.Size = new System.Drawing.Size(140, 21);
			this.cbo_entryActionSelector.TabIndex = 3;
			// 
			// btn_performEntryAction
			// 
			this.btn_performEntryAction.Location = new System.Drawing.Point(149, 3);
			this.btn_performEntryAction.Name = "btn_performEntryAction";
			this.btn_performEntryAction.Size = new System.Drawing.Size(61, 21);
			this.btn_performEntryAction.TabIndex = 4;
			this.btn_performEntryAction.Text = "Go";
			this.btn_performEntryAction.UseVisualStyleBackColor = true;
			// 
			// m_lvAllEntries
			// 
			this.m_lvAllEntries.AllowColumnReorder = true;
			this.m_lvAllEntries.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.m_lvAllEntries.CheckBoxes = true;
			this.m_lvAllEntries.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_lvAllEntries.FullRowSelect = true;
			this.m_lvAllEntries.GridLines = true;
			this.m_lvAllEntries.HideSelection = false;
			this.m_lvAllEntries.Location = new System.Drawing.Point(0, 0);
			this.m_lvAllEntries.Margin = new System.Windows.Forms.Padding(0);
			this.m_lvAllEntries.Name = "m_lvAllEntries";
			this.m_lvAllEntries.Size = new System.Drawing.Size(310, 458);
			this.m_lvAllEntries.TabIndex = 0;
			this.m_lvAllEntries.UseCompatibleStateImageBehavior = false;
			this.m_lvAllEntries.View = System.Windows.Forms.View.Details;
			this.m_lvAllEntries.SelectedIndexChanged += new System.EventHandler(this.OnLvAllEntriesSelectedIndexChanged);
			// 
			// cb_allEntriesSelection
			// 
			this.cb_allEntriesSelection.Location = new System.Drawing.Point(3, 461);
			this.cb_allEntriesSelection.Name = "cb_allEntriesSelection";
			this.cb_allEntriesSelection.Size = new System.Drawing.Size(129, 24);
			this.cb_allEntriesSelection.TabIndex = 1;
			this.cb_allEntriesSelection.Text = "&Check/Uncheck All";
			this.cb_allEntriesSelection.UseVisualStyleBackColor = true;
			// 
			// tlp_right
			// 
			this.tlp_right.ColumnCount = 1;
			this.tlp_right.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlp_right.Controls.Add(this.tlp_upperRight, 0, 0);
			this.tlp_right.Controls.Add(this.bto_OK, 0, 2);
			this.tlp_right.Controls.Add(this.tco_right, 0, 1);
			this.tlp_right.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlp_right.Location = new System.Drawing.Point(0, 0);
			this.tlp_right.Name = "tlp_right";
			this.tlp_right.RowCount = 3;
			this.tlp_right.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlp_right.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlp_right.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
			this.tlp_right.Size = new System.Drawing.Size(540, 553);
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
			this.tlp_upperRight.Size = new System.Drawing.Size(534, 194);
			this.tlp_upperRight.TabIndex = 1;
			// 
			// lbl_originalSize
			// 
			this.lbl_originalSize.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.tlp_upperRight.SetColumnSpan(this.lbl_originalSize, 3);
			this.lbl_originalSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_originalSize.Location = new System.Drawing.Point(270, 0);
			this.lbl_originalSize.Margin = new System.Windows.Forms.Padding(3, 0, 30, 0);
			this.lbl_originalSize.Name = "lbl_originalSize";
			this.lbl_originalSize.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lbl_originalSize.Size = new System.Drawing.Size(234, 20);
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
			this.lbl_128x128.Size = new System.Drawing.Size(261, 20);
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
			this.lbl_64x64.Location = new System.Drawing.Point(270, 174);
			this.lbl_64x64.Name = "lbl_64x64";
			this.lbl_64x64.Size = new System.Drawing.Size(127, 20);
			this.lbl_64x64.TabIndex = 0;
			this.lbl_64x64.Text = "64x64";
			this.lbl_64x64.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// lbl_32x32
			// 
			this.lbl_32x32.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.lbl_32x32.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_32x32.Location = new System.Drawing.Point(403, 174);
			this.lbl_32x32.Name = "lbl_32x32";
			this.lbl_32x32.Size = new System.Drawing.Size(60, 20);
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
			this.lbl_16x16.Location = new System.Drawing.Point(469, 174);
			this.lbl_16x16.Name = "lbl_16x16";
			this.lbl_16x16.Size = new System.Drawing.Size(62, 20);
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
			this.scltp_icons.Size = new System.Drawing.Size(534, 154);
			this.scltp_icons.TabIndex = 18;
			// 
			// pbo_selectedIcon16
			// 
			this.pbo_selectedIcon16.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.pbo_selectedIcon16.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.pbo_selectedIcon16.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pbo_selectedIcon16.Location = new System.Drawing.Point(490, 67);
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
			this.pbo_selectedIcon32.Location = new System.Drawing.Point(415, 59);
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
			this.pbo_selectedIcon64.Location = new System.Drawing.Point(299, 43);
			this.pbo_selectedIcon64.Name = "pbo_selectedIcon64";
			this.pbo_selectedIcon64.Size = new System.Drawing.Size(68, 68);
			this.pbo_selectedIcon64.TabIndex = 12;
			this.pbo_selectedIcon64.TabStop = false;
			// 
			// bto_OK
			// 
			this.bto_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.bto_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.bto_OK.Location = new System.Drawing.Point(435, 529);
			this.bto_OK.Margin = new System.Windows.Forms.Padding(3, 3, 30, 3);
			this.bto_OK.Name = "bto_OK";
			this.bto_OK.Size = new System.Drawing.Size(75, 21);
			this.bto_OK.TabIndex = 6;
			this.bto_OK.Text = "&Close";
			this.bto_OK.UseVisualStyleBackColor = true;
			// 
			// tco_right
			// 
			this.tco_right.Controls.Add(this.tpa_IconUsage);
			this.tco_right.Controls.Add(this.tpa_DownloadResult);
			this.tco_right.Controls.Add(this.tpa_Debug);
			this.tco_right.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tco_right.Location = new System.Drawing.Point(15, 203);
			this.tco_right.Margin = new System.Windows.Forms.Padding(15, 3, 3, 3);
			this.tco_right.Multiline = true;
			this.tco_right.Name = "tco_right";
			this.tco_right.SelectedIndex = 0;
			this.tco_right.Size = new System.Drawing.Size(522, 320);
			this.tco_right.TabIndex = 5;
			// 
			// tpa_IconUsage
			// 
			this.tpa_IconUsage.BackColor = System.Drawing.SystemColors.Control;
			this.tpa_IconUsage.Controls.Add(this.spc_right);
			this.tpa_IconUsage.Location = new System.Drawing.Point(4, 22);
			this.tpa_IconUsage.Name = "tpa_IconUsage";
			this.tpa_IconUsage.Padding = new System.Windows.Forms.Padding(3);
			this.tpa_IconUsage.Size = new System.Drawing.Size(514, 294);
			this.tpa_IconUsage.TabIndex = 0;
			this.tpa_IconUsage.Text = "Icon Usage";
			// 
			// spc_right
			// 
			this.spc_right.Dock = System.Windows.Forms.DockStyle.Fill;
			this.spc_right.Location = new System.Drawing.Point(3, 3);
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
			this.spc_right.Size = new System.Drawing.Size(508, 288);
			this.spc_right.SplitterDistance = 131;
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
			this.tlp_usedEntries.Size = new System.Drawing.Size(508, 131);
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
			this.tlp_usedGroups.Size = new System.Drawing.Size(508, 153);
			this.tlp_usedGroups.TabIndex = 0;
			// 
			// tpa_DownloadResult
			// 
			this.tpa_DownloadResult.BackColor = System.Drawing.SystemColors.Control;
			this.tpa_DownloadResult.Controls.Add(this.tlp_downloadResult);
			this.tpa_DownloadResult.Location = new System.Drawing.Point(4, 22);
			this.tpa_DownloadResult.Name = "tpa_DownloadResult";
			this.tpa_DownloadResult.Padding = new System.Windows.Forms.Padding(3);
			this.tpa_DownloadResult.Size = new System.Drawing.Size(514, 294);
			this.tpa_DownloadResult.TabIndex = 1;
			this.tpa_DownloadResult.Text = "Download Result";
			// 
			// tlp_downloadResult
			// 
			this.tlp_downloadResult.ColumnCount = 4;
			this.tlp_downloadResult.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tlp_downloadResult.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tlp_downloadResult.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.tlp_downloadResult.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.tlp_downloadResult.Controls.Add(this.scltp_downloadResult, 0, 1);
			this.tlp_downloadResult.Controls.Add(this.lbl_newSize, 1, 0);
			this.tlp_downloadResult.Controls.Add(this.m_lvDownloadResult, 0, 4);
			this.tlp_downloadResult.Controls.Add(this.lbl_alldownloadedicons, 0, 3);
			this.tlp_downloadResult.Controls.Add(this.rbu_icons, 1, 3);
			this.tlp_downloadResult.Controls.Add(this.rbu_details, 2, 3);
			this.tlp_downloadResult.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlp_downloadResult.Location = new System.Drawing.Point(3, 3);
			this.tlp_downloadResult.Name = "tlp_downloadResult";
			this.tlp_downloadResult.RowCount = 5;
			this.tlp_downloadResult.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
			this.tlp_downloadResult.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlp_downloadResult.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tlp_downloadResult.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tlp_downloadResult.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlp_downloadResult.Size = new System.Drawing.Size(508, 288);
			this.tlp_downloadResult.TabIndex = 0;
			// 
			// scltp_downloadResult
			// 
			this.scltp_downloadResult.ColumnCount = 4;
			this.tlp_downloadResult.SetColumnSpan(this.scltp_downloadResult, 4);
			this.scltp_downloadResult.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.25126F));
			this.scltp_downloadResult.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.12563F));
			this.scltp_downloadResult.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.31156F));
			this.scltp_downloadResult.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.31156F));
			this.scltp_downloadResult.Controls.Add(this.pbo_downloadedIcon128, 0, 0);
			this.scltp_downloadResult.Controls.Add(this.pbo_downloadedIcon64, 1, 0);
			this.scltp_downloadResult.Controls.Add(this.pbo_downloadedIcon16, 3, 0);
			this.scltp_downloadResult.Controls.Add(this.pbo_downloadedIcon32, 2, 0);
			this.scltp_downloadResult.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scltp_downloadResult.Location = new System.Drawing.Point(0, 15);
			this.scltp_downloadResult.Margin = new System.Windows.Forms.Padding(0);
			this.scltp_downloadResult.Name = "scltp_downloadResult";
			this.scltp_downloadResult.RowCount = 1;
			this.scltp_downloadResult.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.scltp_downloadResult.ScaleHeight = false;
			this.scltp_downloadResult.ScaleWidth = true;
			this.scltp_downloadResult.Size = new System.Drawing.Size(508, 154);
			this.scltp_downloadResult.TabIndex = 0;
			// 
			// pbo_downloadedIcon128
			// 
			this.pbo_downloadedIcon128.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.pbo_downloadedIcon128.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.pbo_downloadedIcon128.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pbo_downloadedIcon128.InitialImage = null;
			this.pbo_downloadedIcon128.Location = new System.Drawing.Point(61, 11);
			this.pbo_downloadedIcon128.Name = "pbo_downloadedIcon128";
			this.pbo_downloadedIcon128.Size = new System.Drawing.Size(132, 132);
			this.pbo_downloadedIcon128.TabIndex = 0;
			this.pbo_downloadedIcon128.TabStop = false;
			// 
			// pbo_downloadedIcon64
			// 
			this.pbo_downloadedIcon64.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.pbo_downloadedIcon64.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.pbo_downloadedIcon64.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pbo_downloadedIcon64.InitialImage = null;
			this.pbo_downloadedIcon64.Location = new System.Drawing.Point(284, 43);
			this.pbo_downloadedIcon64.Name = "pbo_downloadedIcon64";
			this.pbo_downloadedIcon64.Size = new System.Drawing.Size(68, 68);
			this.pbo_downloadedIcon64.TabIndex = 1;
			this.pbo_downloadedIcon64.TabStop = false;
			// 
			// pbo_downloadedIcon16
			// 
			this.pbo_downloadedIcon16.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.pbo_downloadedIcon16.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.pbo_downloadedIcon16.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pbo_downloadedIcon16.InitialImage = null;
			this.pbo_downloadedIcon16.Location = new System.Drawing.Point(466, 67);
			this.pbo_downloadedIcon16.Name = "pbo_downloadedIcon16";
			this.pbo_downloadedIcon16.Size = new System.Drawing.Size(20, 20);
			this.pbo_downloadedIcon16.TabIndex = 3;
			this.pbo_downloadedIcon16.TabStop = false;
			// 
			// pbo_downloadedIcon32
			// 
			this.pbo_downloadedIcon32.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.pbo_downloadedIcon32.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.pbo_downloadedIcon32.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pbo_downloadedIcon32.InitialImage = null;
			this.pbo_downloadedIcon32.Location = new System.Drawing.Point(396, 60);
			this.pbo_downloadedIcon32.Name = "pbo_downloadedIcon32";
			this.pbo_downloadedIcon32.Size = new System.Drawing.Size(34, 34);
			this.pbo_downloadedIcon32.TabIndex = 2;
			this.pbo_downloadedIcon32.TabStop = false;
			// 
			// lbl_newSize
			// 
			this.lbl_newSize.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.tlp_downloadResult.SetColumnSpan(this.lbl_newSize, 3);
			this.lbl_newSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_newSize.Location = new System.Drawing.Point(257, 0);
			this.lbl_newSize.Margin = new System.Windows.Forms.Padding(3, 0, 30, 0);
			this.lbl_newSize.Name = "lbl_newSize";
			this.lbl_newSize.Size = new System.Drawing.Size(221, 15);
			this.lbl_newSize.TabIndex = 4;
			this.lbl_newSize.Text = "New Size :";
			this.lbl_newSize.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// m_lvDownloadResult
			// 
			this.tlp_downloadResult.SetColumnSpan(this.m_lvDownloadResult, 4);
			this.m_lvDownloadResult.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_lvDownloadResult.FullRowSelect = true;
			this.m_lvDownloadResult.Location = new System.Drawing.Point(3, 212);
			this.m_lvDownloadResult.Name = "m_lvDownloadResult";
			this.m_lvDownloadResult.Size = new System.Drawing.Size(502, 73);
			this.m_lvDownloadResult.TabIndex = 5;
			this.m_lvDownloadResult.UseCompatibleStateImageBehavior = false;
			// 
			// lbl_alldownloadedicons
			// 
			this.lbl_alldownloadedicons.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbl_alldownloadedicons.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_alldownloadedicons.Location = new System.Drawing.Point(3, 189);
			this.lbl_alldownloadedicons.Name = "lbl_alldownloadedicons";
			this.lbl_alldownloadedicons.Size = new System.Drawing.Size(248, 20);
			this.lbl_alldownloadedicons.TabIndex = 6;
			this.lbl_alldownloadedicons.Text = "All downloaded icons";
			// 
			// rbu_icons
			// 
			this.rbu_icons.Checked = true;
			this.rbu_icons.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rbu_icons.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rbu_icons.Location = new System.Drawing.Point(254, 189);
			this.rbu_icons.Margin = new System.Windows.Forms.Padding(0);
			this.rbu_icons.Name = "rbu_icons";
			this.rbu_icons.Size = new System.Drawing.Size(127, 20);
			this.rbu_icons.TabIndex = 7;
			this.rbu_icons.TabStop = true;
			this.rbu_icons.Text = "Icons only";
			this.rbu_icons.UseVisualStyleBackColor = true;
			// 
			// rbu_details
			// 
			this.tlp_downloadResult.SetColumnSpan(this.rbu_details, 2);
			this.rbu_details.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rbu_details.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rbu_details.Location = new System.Drawing.Point(381, 189);
			this.rbu_details.Margin = new System.Windows.Forms.Padding(0);
			this.rbu_details.Name = "rbu_details";
			this.rbu_details.Size = new System.Drawing.Size(127, 20);
			this.rbu_details.TabIndex = 8;
			this.rbu_details.Text = "Icons + Details";
			this.rbu_details.UseVisualStyleBackColor = true;
			this.rbu_details.CheckedChanged += new System.EventHandler(this.OnDetailsCheckedChanged);
			// 
			// tpa_Debug
			// 
			this.tpa_Debug.Controls.Add(this.rtb_details);
			this.tpa_Debug.Location = new System.Drawing.Point(4, 22);
			this.tpa_Debug.Name = "tpa_Debug";
			this.tpa_Debug.Padding = new System.Windows.Forms.Padding(3);
			this.tpa_Debug.Size = new System.Drawing.Size(514, 294);
			this.tpa_Debug.TabIndex = 2;
			this.tpa_Debug.Text = "Debug";
			this.tpa_Debug.UseVisualStyleBackColor = true;
			// 
			// rtb_details
			// 
			this.rtb_details.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtb_details.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rtb_details.Location = new System.Drawing.Point(3, 3);
			this.rtb_details.Name = "rtb_details";
			this.rtb_details.Size = new System.Drawing.Size(508, 288);
			this.rtb_details.TabIndex = 0;
			this.rtb_details.Text = "";
			// 
			// tlp_main
			// 
			this.tlp_main.ColumnCount = 1;
			this.tlp_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlp_main.Controls.Add(this.spc_mainSplitter, 0, 0);
			this.tlp_main.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlp_main.Location = new System.Drawing.Point(0, 0);
			this.tlp_main.Name = "tlp_main";
			this.tlp_main.RowCount = 2;
			this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
			this.tlp_main.Size = new System.Drawing.Size(876, 562);
			this.tlp_main.TabIndex = 9;
			// 
			// DashboarderForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(876, 562);
			this.Controls.Add(this.tlp_main);
			this.Name = "DashboarderForm";
			this.Text = "Custom Icon Dashboard";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnFormClosed);
			this.Load += new System.EventHandler(this.DashboarderLoad);
			((System.ComponentModel.ISupportInitialize)(this.pbo_selectedIcon128)).EndInit();
			this.spc_mainSplitter.Panel1.ResumeLayout(false);
			this.spc_mainSplitter.Panel2.ResumeLayout(false);
			this.spc_mainSplitter.ResumeLayout(false);
			this.tlp_topright.ResumeLayout(false);
			this.tco_lists.ResumeLayout(false);
			this.tpa_listAllIcons.ResumeLayout(false);
			this.tlp_allIcons.ResumeLayout(false);
			this.flp_iconActions.ResumeLayout(false);
			this.tpa_listAllEntries.ResumeLayout(false);
			this.tlp_allEntries.ResumeLayout(false);
			this.flp_entryAction.ResumeLayout(false);
			this.tlp_right.ResumeLayout(false);
			this.tlp_right.PerformLayout();
			this.tlp_upperRight.ResumeLayout(false);
			this.scltp_icons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbo_selectedIcon16)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbo_selectedIcon32)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbo_selectedIcon64)).EndInit();
			this.tco_right.ResumeLayout(false);
			this.tpa_IconUsage.ResumeLayout(false);
			this.spc_right.Panel1.ResumeLayout(false);
			this.spc_right.Panel2.ResumeLayout(false);
			this.spc_right.ResumeLayout(false);
			this.tlp_usedEntries.ResumeLayout(false);
			this.tlp_usedEntries.PerformLayout();
			this.tlp_usedGroups.ResumeLayout(false);
			this.tpa_DownloadResult.ResumeLayout(false);
			this.tlp_downloadResult.ResumeLayout(false);
			this.scltp_downloadResult.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbo_downloadedIcon128)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbo_downloadedIcon64)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbo_downloadedIcon16)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbo_downloadedIcon32)).EndInit();
			this.tpa_Debug.ResumeLayout(false);
			this.tlp_main.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		private System.Windows.Forms.TableLayoutPanel tlp_topright;
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
		private System.Windows.Forms.TabControl tco_right;
		private System.Windows.Forms.TabPage tpa_IconUsage;
		private System.Windows.Forms.TabPage tpa_DownloadResult;
		private System.Windows.Forms.TableLayoutPanel tlp_downloadResult;
		private LomsonLib.UI.ScaleControlTableLayoutPanel scltp_downloadResult;
		private System.Windows.Forms.PictureBox pbo_downloadedIcon128;
		private System.Windows.Forms.PictureBox pbo_downloadedIcon64;
		private System.Windows.Forms.PictureBox pbo_downloadedIcon32;
		private System.Windows.Forms.PictureBox pbo_downloadedIcon16;
		private System.Windows.Forms.Label lbl_newSize;
		private System.Windows.Forms.TableLayoutPanel tlp_main;
		private System.Windows.Forms.RichTextBox rtb_details;
		private System.Windows.Forms.TabPage tpa_Debug;
		private System.Windows.Forms.ListView m_lvDownloadResult;
		private System.Windows.Forms.Label lbl_alldownloadedicons;
		private System.Windows.Forms.RadioButton rbu_icons;
		private System.Windows.Forms.RadioButton rbu_details;
		private System.Windows.Forms.TabControl tco_lists;
		private System.Windows.Forms.TabPage tpa_listAllIcons;
		private System.Windows.Forms.TableLayoutPanel tlp_allIcons;
		private System.Windows.Forms.TabPage tpa_listAllEntries;
		private System.Windows.Forms.ComboBox cbo_iconActionSelector;
		private System.Windows.Forms.FlowLayoutPanel flp_iconActions;
		private System.Windows.Forms.Button btn_performIconAction;
		private System.Windows.Forms.TableLayoutPanel tlp_allEntries;
		private System.Windows.Forms.ListView m_lvAllEntries;
		private System.Windows.Forms.CheckBox cb_allEntriesSelection;
		private System.Windows.Forms.FlowLayoutPanel flp_entryAction;
		private System.Windows.Forms.ComboBox cbo_entryActionSelector;
		private System.Windows.Forms.Button btn_performEntryAction;

		
		void DashboarderLoad(object sender, System.EventArgs e)
		{
			this.OnFormLoad(sender, e);
		}
		
	
		
	}
}
