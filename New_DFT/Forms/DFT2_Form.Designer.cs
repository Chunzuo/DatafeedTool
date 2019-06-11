namespace New_DFT.Forms
{
    partial class DFT2_Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DFT2_Form));
            this.repositoryItemHypertextLabel1 = new DevExpress.XtraEditors.Repository.RepositoryItemHypertextLabel();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btn_runQuery = new DevExpress.XtraBars.BarButtonItem();
            this.btn_writeChanges = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.label_database = new DevExpress.XtraBars.BarStaticItem();
            this.label_serverName = new DevExpress.XtraBars.BarStaticItem();
            this.barListItem1 = new DevExpress.XtraBars.BarListItem();
            this.barDockingMenuItem1 = new DevExpress.XtraBars.BarDockingMenuItem();
            this.bei_database = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.barEditItem2 = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.barHeaderItem1 = new DevExpress.XtraBars.BarHeaderItem();
            this.barStaticItem3 = new DevExpress.XtraBars.BarStaticItem();
            this.barHeaderItem2 = new DevExpress.XtraBars.BarHeaderItem();
            this.barStaticItem4 = new DevExpress.XtraBars.BarStaticItem();
            this.barHeaderItem3 = new DevExpress.XtraBars.BarHeaderItem();
            this.barEditItem3 = new DevExpress.XtraBars.BarEditItem();
            this.barEditItem4 = new DevExpress.XtraBars.BarEditItem();
            this.barLinkContainerItem1 = new DevExpress.XtraBars.BarLinkContainerItem();
            this.btn_save = new DevExpress.XtraBars.BarButtonItem();
            this.btn_load = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.repositoryItemCheckedComboBoxEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.ribbonPage2 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHypertextLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckedComboBoxEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            this.SuspendLayout();
            // 
            // repositoryItemHypertextLabel1
            // 
            this.repositoryItemHypertextLabel1.Name = "repositoryItemHypertextLabel1";
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.btn_runQuery,
            this.btn_writeChanges,
            this.barButtonItem3,
            this.barButtonItem4,
            this.label_database,
            this.label_serverName,
            this.barListItem1,
            this.barDockingMenuItem1,
            this.bei_database,
            this.barEditItem2,
            this.barHeaderItem1,
            this.barStaticItem3,
            this.barHeaderItem2,
            this.barStaticItem4,
            this.barHeaderItem3,
            this.barEditItem3,
            this.barEditItem4,
            this.barLinkContainerItem1,
            this.btn_save,
            this.btn_load});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 23;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonControl1.QuickToolbarItemLinks.Add(this.label_serverName);
            this.ribbonControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckedComboBoxEdit1,
            this.repositoryItemDateEdit1,
            this.repositoryItemComboBox1});
            this.ribbonControl1.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.ShowToolbarCustomizeItem = false;
            this.ribbonControl1.Size = new System.Drawing.Size(1013, 146);
            this.ribbonControl1.StatusBar = this.ribbonStatusBar1;
            this.ribbonControl1.Toolbar.ShowCustomizeItem = false;
            this.ribbonControl1.TransparentEditorsMode = DevExpress.Utils.DefaultBoolean.True;
            // 
            // btn_runQuery
            // 
            this.btn_runQuery.Caption = "Run query";
            this.btn_runQuery.Enabled = false;
            this.btn_runQuery.Id = 1;
            this.btn_runQuery.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_runQuery.ImageOptions.Image")));
            this.btn_runQuery.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btn_runQuery.ImageOptions.LargeImage")));
            this.btn_runQuery.Name = "btn_runQuery";
            this.btn_runQuery.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_runQuery_ItemClick);
            // 
            // btn_writeChanges
            // 
            this.btn_writeChanges.Caption = "Write Changes";
            this.btn_writeChanges.Enabled = false;
            this.btn_writeChanges.Id = 2;
            this.btn_writeChanges.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_writeChanges.ImageOptions.Image")));
            this.btn_writeChanges.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btn_writeChanges.ImageOptions.LargeImage")));
            this.btn_writeChanges.Name = "btn_writeChanges";
            this.btn_writeChanges.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_writeChanges_ItemClick);
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "ABC";
            this.barButtonItem3.Id = 4;
            this.barButtonItem3.Name = "barButtonItem3";
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Caption = "barButtonItem4";
            this.barButtonItem4.Id = 6;
            this.barButtonItem4.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem4.ImageOptions.Image")));
            this.barButtonItem4.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem4.ImageOptions.LargeImage")));
            this.barButtonItem4.Name = "barButtonItem4";
            // 
            // label_database
            // 
            this.label_database.Caption = "No database selected";
            this.label_database.Id = 7;
            this.label_database.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("label_database.ImageOptions.Image")));
            this.label_database.Name = "label_database";
            // 
            // label_serverName
            // 
            this.label_serverName.Caption = "Localhost";
            this.label_serverName.Id = 8;
            this.label_serverName.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("label_serverName.ImageOptions.LargeImage")));
            this.label_serverName.ItemInMenuAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label_serverName.ItemInMenuAppearance.Normal.Options.UseFont = true;
            this.label_serverName.Name = "label_serverName";
            // 
            // barListItem1
            // 
            this.barListItem1.Caption = "barListItem1";
            this.barListItem1.Id = 9;
            this.barListItem1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barListItem1.ImageOptions.Image")));
            this.barListItem1.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barListItem1.ImageOptions.LargeImage")));
            this.barListItem1.ItemIndex = 0;
            this.barListItem1.Name = "barListItem1";
            this.barListItem1.Strings.AddRange(new object[] {
            "Instance1",
            "Instance2"});
            // 
            // barDockingMenuItem1
            // 
            this.barDockingMenuItem1.Caption = "barDockingMenuItem1";
            this.barDockingMenuItem1.Id = 10;
            this.barDockingMenuItem1.Name = "barDockingMenuItem1";
            // 
            // bei_database
            // 
            this.bei_database.Edit = this.repositoryItemComboBox1;
            this.bei_database.EditWidth = 150;
            this.bei_database.Hint = "Select database";
            this.bei_database.Id = 11;
            this.bei_database.Name = "bei_database";
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            this.repositoryItemComboBox1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.repositoryItemComboBox1.SelectedIndexChanged += new System.EventHandler(this.repositoryItemComboBox1_SelectedIndexChanged);
            // 
            // barEditItem2
            // 
            this.barEditItem2.Caption = "Database:";
            this.barEditItem2.Edit = this.repositoryItemDateEdit1;
            this.barEditItem2.EditWidth = 150;
            this.barEditItem2.Id = 12;
            this.barEditItem2.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barEditItem2.ImageOptions.Image")));
            this.barEditItem2.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barEditItem2.ImageOptions.LargeImage")));
            this.barEditItem2.Name = "barEditItem2";
            // 
            // repositoryItemDateEdit1
            // 
            this.repositoryItemDateEdit1.AutoHeight = false;
            this.repositoryItemDateEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit1.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit1.Name = "repositoryItemDateEdit1";
            this.repositoryItemDateEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // barHeaderItem1
            // 
            this.barHeaderItem1.Caption = "Localhost";
            this.barHeaderItem1.Id = 13;
            this.barHeaderItem1.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barHeaderItem1.ImageOptions.LargeImage")));
            this.barHeaderItem1.Name = "barHeaderItem1";
            // 
            // barStaticItem3
            // 
            this.barStaticItem3.Caption = "Localhost";
            this.barStaticItem3.Id = 14;
            this.barStaticItem3.Name = "barStaticItem3";
            // 
            // barHeaderItem2
            // 
            this.barHeaderItem2.Caption = "barHeaderItem2";
            this.barHeaderItem2.Id = 15;
            this.barHeaderItem2.Name = "barHeaderItem2";
            // 
            // barStaticItem4
            // 
            this.barStaticItem4.Caption = "barStaticItem4";
            this.barStaticItem4.Id = 16;
            this.barStaticItem4.Name = "barStaticItem4";
            // 
            // barHeaderItem3
            // 
            this.barHeaderItem3.Appearance.BackColor = System.Drawing.Color.Black;
            this.barHeaderItem3.Appearance.Font = new System.Drawing.Font("Tahoma", 14F);
            this.barHeaderItem3.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.barHeaderItem3.Appearance.Options.UseBackColor = true;
            this.barHeaderItem3.Appearance.Options.UseFont = true;
            this.barHeaderItem3.Appearance.Options.UseForeColor = true;
            this.barHeaderItem3.Caption = "Localhost";
            this.barHeaderItem3.Id = 17;
            this.barHeaderItem3.Name = "barHeaderItem3";
            // 
            // barEditItem3
            // 
            this.barEditItem3.Caption = "barEditItem3";
            this.barEditItem3.Edit = this.repositoryItemHypertextLabel1;
            this.barEditItem3.Id = 18;
            this.barEditItem3.Name = "barEditItem3";
            // 
            // barEditItem4
            // 
            this.barEditItem4.Caption = "barEditItem4";
            this.barEditItem4.Edit = this.repositoryItemComboBox1;
            this.barEditItem4.Id = 19;
            this.barEditItem4.Name = "barEditItem4";
            // 
            // barLinkContainerItem1
            // 
            this.barLinkContainerItem1.Caption = "barLinkContainerItem1";
            this.barLinkContainerItem1.Id = 20;
            this.barLinkContainerItem1.Name = "barLinkContainerItem1";
            // 
            // btn_save
            // 
            this.btn_save.Caption = "Save";
            this.btn_save.Enabled = false;
            this.btn_save.Id = 21;
            this.btn_save.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btn_save.ImageOptions.LargeImage")));
            this.btn_save.Name = "btn_save";
            // 
            // btn_load
            // 
            this.btn_load.Caption = "Load";
            this.btn_load.Hint = "Load Saved Datafeed Result";
            this.btn_load.Id = 22;
            this.btn_load.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btn_load.ImageOptions.LargeImage")));
            this.btn_load.Name = "btn_load";
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1,
            this.ribbonPageGroup2});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Home";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("ribbonPageGroup1.ImageOptions.Image")));
            this.ribbonPageGroup1.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("ribbonPageGroup1.ImageOptions.SvgImage")));
            this.ribbonPageGroup1.ItemLinks.Add(this.bei_database);
            this.ribbonPageGroup1.ItemLinks.Add(this.btn_runQuery);
            this.ribbonPageGroup1.ItemLinks.Add(this.btn_writeChanges);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.ShowCaptionButton = false;
            this.ribbonPageGroup1.Text = "Database";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.btn_save);
            this.ribbonPageGroup2.ItemLinks.Add(this.btn_load);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.ShowCaptionButton = false;
            this.ribbonPageGroup2.Text = "Actions";
            // 
            // repositoryItemCheckedComboBoxEdit1
            // 
            this.repositoryItemCheckedComboBoxEdit1.AutoHeight = false;
            this.repositoryItemCheckedComboBoxEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemCheckedComboBoxEdit1.Name = "repositoryItemCheckedComboBoxEdit1";
            this.repositoryItemCheckedComboBoxEdit1.SelectAllItemVisible = false;
            // 
            // ribbonStatusBar1
            // 
            this.ribbonStatusBar1.ItemLinks.Add(this.label_database);
            this.ribbonStatusBar1.Location = new System.Drawing.Point(0, 684);
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Ribbon = this.ribbonControl1;
            this.ribbonStatusBar1.Size = new System.Drawing.Size(1013, 21);
            // 
            // ribbonPage2
            // 
            this.ribbonPage2.Name = "ribbonPage2";
            this.ribbonPage2.Text = "ribbonPage2";
            // 
            // gridControl
            // 
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.Location = new System.Drawing.Point(0, 146);
            this.gridControl.MainView = this.gridView;
            this.gridControl.MenuManager = this.ribbonControl1;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(1013, 538);
            this.gridControl.TabIndex = 2;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.GridControl = this.gridControl;
            this.gridView.Name = "gridView";
            this.gridView.OptionsView.ColumnAutoWidth = false;
            this.gridView.OptionsView.ShowGroupPanel = false;
            this.gridView.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView_RowCellStyle);
            this.gridView.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.gridView_PopupMenuShowing);
            this.gridView.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView_CellValueChanged);
            // 
            // DFT2_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1013, 705);
            this.Controls.Add(this.gridControl);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Controls.Add(this.ribbonControl1);
            this.Name = "DFT2_Form";
            this.Ribbon = this.ribbonControl1;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StatusBar = this.ribbonStatusBar1;
            this.Text = "Datafeed Tool - 2.0";
            this.Load += new System.EventHandler(this.DFT2_Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHypertextLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckedComboBoxEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage2;
        private DevExpress.XtraBars.BarButtonItem btn_runQuery;
        private DevExpress.XtraBars.BarButtonItem btn_writeChanges;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraBars.BarStaticItem label_database;
        private DevExpress.XtraBars.BarStaticItem label_serverName;
        private DevExpress.XtraBars.BarListItem barListItem1;
        private DevExpress.XtraBars.BarDockingMenuItem barDockingMenuItem1;
        private DevExpress.XtraBars.BarEditItem bei_database;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit repositoryItemCheckedComboBoxEdit1;
        private DevExpress.XtraBars.BarEditItem barEditItem2;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit1;
        private DevExpress.XtraBars.BarHeaderItem barHeaderItem1;
        private DevExpress.XtraBars.BarStaticItem barStaticItem3;
        private DevExpress.XtraBars.BarHeaderItem barHeaderItem2;
        private DevExpress.XtraBars.BarStaticItem barStaticItem4;
        private DevExpress.XtraBars.BarHeaderItem barHeaderItem3;
        private DevExpress.XtraBars.BarEditItem barEditItem3;
        private DevExpress.XtraBars.BarEditItem barEditItem4;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraBars.BarLinkContainerItem barLinkContainerItem1;
        private DevExpress.XtraBars.BarButtonItem btn_save;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraEditors.Repository.RepositoryItemHypertextLabel repositoryItemHypertextLabel1;
        private DevExpress.XtraBars.BarButtonItem btn_load;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
    }
}