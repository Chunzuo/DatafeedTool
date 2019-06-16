namespace New_DFT.Forms
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.accordionControl1 = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.accordionControlElement1 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.item_runquery = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.item_writechanges = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement4 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.item_save = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.item_load = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ribbonPage2 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.check_selectAll = new DevExpress.XtraEditors.CheckEdit();
            this.label_server = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cb_database = new DevExpress.XtraEditors.ComboBoxEdit();
            this.panel_find = new System.Windows.Forms.Panel();
            this.btn_replace = new DevExpress.XtraEditors.SimpleButton();
            this.text_replace = new DevExpress.XtraEditors.TextEdit();
            this.btn_find = new DevExpress.XtraEditors.SimpleButton();
            this.text_find = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.gridview = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.sub_gridview = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.fluentDesignFormContainer1 = new DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormContainer();
            this.fluentDesignFormControl1 = new DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.check_selectAll.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_database.Properties)).BeginInit();
            this.panel_find.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.text_replace.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.text_find.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sub_gridview)).BeginInit();
            this.fluentDesignFormContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fluentDesignFormControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // accordionControl1
            // 
            this.accordionControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.accordionControl1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlElement1,
            this.accordionControlElement4});
            this.accordionControl1.Location = new System.Drawing.Point(0, 31);
            this.accordionControl1.Name = "accordionControl1";
            this.accordionControl1.OptionsMinimizing.NormalWidth = 260;
            this.accordionControl1.ScrollBarMode = DevExpress.XtraBars.Navigation.ScrollBarMode.Touch;
            this.accordionControl1.ShowFilterControl = DevExpress.XtraBars.Navigation.ShowFilterControl.Always;
            this.accordionControl1.Size = new System.Drawing.Size(260, 621);
            this.accordionControl1.TabIndex = 1;
            this.accordionControl1.ViewType = DevExpress.XtraBars.Navigation.AccordionControlViewType.HamburgerMenu;
            // 
            // accordionControlElement1
            // 
            this.accordionControlElement1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.item_runquery,
            this.item_writechanges});
            this.accordionControlElement1.Expanded = true;
            this.accordionControlElement1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("accordionControlElement1.ImageOptions.Image")));
            this.accordionControlElement1.Name = "accordionControlElement1";
            this.accordionControlElement1.Text = "Database";
            // 
            // item_runquery
            // 
            this.item_runquery.Name = "item_runquery";
            this.item_runquery.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.item_runquery.Text = "Query current database";
            this.item_runquery.Click += new System.EventHandler(this.item_runquery_Click);
            // 
            // item_writechanges
            // 
            this.item_writechanges.Name = "item_writechanges";
            this.item_writechanges.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.item_writechanges.Text = "Write Changes";
            this.item_writechanges.Click += new System.EventHandler(this.item_writechanges_Click);
            // 
            // accordionControlElement4
            // 
            this.accordionControlElement4.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.item_save,
            this.item_load});
            this.accordionControlElement4.Expanded = true;
            this.accordionControlElement4.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("accordionControlElement4.ImageOptions.Image")));
            this.accordionControlElement4.Name = "accordionControlElement4";
            this.accordionControlElement4.Text = "File";
            // 
            // item_save
            // 
            this.item_save.Name = "item_save";
            this.item_save.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.item_save.Text = "Save";
            this.item_save.Click += new System.EventHandler(this.item_save_Click);
            // 
            // item_load
            // 
            this.item_load.Name = "item_load";
            this.item_load.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.item_load.Text = "Load";
            this.item_load.Click += new System.EventHandler(this.item_load_Click);
            // 
            // ribbonPage2
            // 
            this.ribbonPage2.Name = "ribbonPage2";
            this.ribbonPage2.Text = "ribbonPage2";
            // 
            // panelControl1
            // 
            this.panelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl1.Controls.Add(this.check_selectAll);
            this.panelControl1.Controls.Add(this.label_server);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.cb_database);
            this.panelControl1.Controls.Add(this.panel_find);
            this.panelControl1.Location = new System.Drawing.Point(6, 6);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(851, 141);
            this.panelControl1.TabIndex = 2;
            // 
            // check_selectAll
            // 
            this.check_selectAll.Location = new System.Drawing.Point(5, 81);
            this.check_selectAll.Name = "check_selectAll";
            this.check_selectAll.Properties.Caption = "Select All";
            this.check_selectAll.Size = new System.Drawing.Size(75, 19);
            this.check_selectAll.TabIndex = 3;
            this.check_selectAll.CheckedChanged += new System.EventHandler(this.check_selectAll_CheckedChanged);
            // 
            // label_server
            // 
            this.label_server.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_server.Appearance.Options.UseFont = true;
            this.label_server.Location = new System.Drawing.Point(119, 13);
            this.label_server.Name = "label_server";
            this.label_server.Size = new System.Drawing.Size(77, 19);
            this.label_server.TabIndex = 2;
            this.label_server.Text = "Localhost";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(5, 13);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(97, 19);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Server Name:";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(32, 46);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(70, 19);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Database:";
            // 
            // cb_database
            // 
            this.cb_database.Location = new System.Drawing.Point(119, 43);
            this.cb_database.Name = "cb_database";
            this.cb_database.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_database.Properties.Appearance.Options.UseFont = true;
            this.cb_database.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cb_database.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cb_database.Size = new System.Drawing.Size(228, 26);
            this.cb_database.TabIndex = 1;
            // 
            // panel_find
            // 
            this.panel_find.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_find.Controls.Add(this.btn_replace);
            this.panel_find.Controls.Add(this.text_replace);
            this.panel_find.Controls.Add(this.btn_find);
            this.panel_find.Controls.Add(this.text_find);
            this.panel_find.Controls.Add(this.labelControl4);
            this.panel_find.Controls.Add(this.labelControl3);
            this.panel_find.Location = new System.Drawing.Point(401, 5);
            this.panel_find.Name = "panel_find";
            this.panel_find.Size = new System.Drawing.Size(445, 82);
            this.panel_find.TabIndex = 7;
            // 
            // btn_replace
            // 
            this.btn_replace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_replace.Location = new System.Drawing.Point(366, 41);
            this.btn_replace.Name = "btn_replace";
            this.btn_replace.Size = new System.Drawing.Size(75, 23);
            this.btn_replace.TabIndex = 6;
            this.btn_replace.Text = "Replace";
            this.btn_replace.Click += new System.EventHandler(this.btn_replace_Click);
            // 
            // text_replace
            // 
            this.text_replace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.text_replace.Location = new System.Drawing.Point(136, 43);
            this.text_replace.Name = "text_replace";
            this.text_replace.Size = new System.Drawing.Size(225, 20);
            this.text_replace.TabIndex = 4;
            // 
            // btn_find
            // 
            this.btn_find.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_find.Location = new System.Drawing.Point(365, 11);
            this.btn_find.Name = "btn_find";
            this.btn_find.Size = new System.Drawing.Size(75, 23);
            this.btn_find.TabIndex = 6;
            this.btn_find.Text = "Find";
            this.btn_find.Click += new System.EventHandler(this.btn_find_Click);
            // 
            // text_find
            // 
            this.text_find.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.text_find.Location = new System.Drawing.Point(136, 12);
            this.text_find.Name = "text_find";
            this.text_find.Size = new System.Drawing.Size(225, 20);
            this.text_find.TabIndex = 4;
            // 
            // labelControl4
            // 
            this.labelControl4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(80, 44);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(50, 16);
            this.labelControl4.TabIndex = 5;
            this.labelControl4.Text = "Replace:";
            // 
            // labelControl3
            // 
            this.labelControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(101, 15);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(29, 16);
            this.labelControl3.TabIndex = 5;
            this.labelControl3.Text = "Find:";
            // 
            // gridview
            // 
            this.gridview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridview.Location = new System.Drawing.Point(6, 169);
            this.gridview.MainView = this.gridView1;
            this.gridview.Name = "gridview";
            this.gridview.Size = new System.Drawing.Size(851, 449);
            this.gridview.TabIndex = 3;
            this.gridview.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1,
            this.sub_gridview});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridview;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // sub_gridview
            // 
            this.sub_gridview.GridControl = this.gridview;
            this.sub_gridview.Name = "sub_gridview";
            this.sub_gridview.OptionsView.ColumnAutoWidth = false;
            this.sub_gridview.OptionsView.ShowGroupPanel = false;
            this.sub_gridview.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.sub_gridview_RowCellStyle);
            this.sub_gridview.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.sub_gridview_PopupMenuShowing);
            this.sub_gridview.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.sub_gridview_CellValueChanged);
            // 
            // fluentDesignFormContainer1
            // 
            this.fluentDesignFormContainer1.Controls.Add(this.gridview);
            this.fluentDesignFormContainer1.Controls.Add(this.panelControl1);
            this.fluentDesignFormContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fluentDesignFormContainer1.Location = new System.Drawing.Point(260, 31);
            this.fluentDesignFormContainer1.Name = "fluentDesignFormContainer1";
            this.fluentDesignFormContainer1.Size = new System.Drawing.Size(860, 621);
            this.fluentDesignFormContainer1.TabIndex = 0;
            // 
            // fluentDesignFormControl1
            // 
            this.fluentDesignFormControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.fluentDesignFormControl1.FluentDesignForm = this;
            this.fluentDesignFormControl1.Location = new System.Drawing.Point(0, 0);
            this.fluentDesignFormControl1.Name = "fluentDesignFormControl1";
            this.fluentDesignFormControl1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fluentDesignFormControl1.Size = new System.Drawing.Size(1120, 31);
            this.fluentDesignFormControl1.TabIndex = 4;
            this.fluentDesignFormControl1.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1120, 652);
            this.ControlContainer = this.fluentDesignFormContainer1;
            this.Controls.Add(this.fluentDesignFormContainer1);
            this.Controls.Add(this.accordionControl1);
            this.Controls.Add(this.fluentDesignFormControl1);
            this.FluentDesignFormControl = this.fluentDesignFormControl1;
            this.Name = "MainForm";
            this.NavigationControl = this.accordionControl1;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Datafeed Tool 2.0";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(255)))));
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.check_selectAll.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_database.Properties)).EndInit();
            this.panel_find.ResumeLayout(false);
            this.panel_find.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.text_replace.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.text_find.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sub_gridview)).EndInit();
            this.fluentDesignFormContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fluentDesignFormControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraBars.Navigation.AccordionControl accordionControl1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement item_runquery;
        private DevExpress.XtraBars.Navigation.AccordionControlElement item_writechanges;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement4;
        private DevExpress.XtraBars.Navigation.AccordionControlElement item_save;
        private DevExpress.XtraBars.Navigation.AccordionControlElement item_load;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.GridControl gridview;
        private DevExpress.XtraGrid.Views.Grid.GridView sub_gridview;
        private DevExpress.XtraEditors.CheckEdit check_selectAll;
        private DevExpress.XtraEditors.LabelControl label_server;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit cb_database;
        private System.Windows.Forms.Panel panel_find;
        private DevExpress.XtraEditors.SimpleButton btn_replace;
        private DevExpress.XtraEditors.TextEdit text_replace;
        private DevExpress.XtraEditors.SimpleButton btn_find;
        private DevExpress.XtraEditors.TextEdit text_find;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormContainer fluentDesignFormContainer1;
        private DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl fluentDesignFormControl1;
    }
}