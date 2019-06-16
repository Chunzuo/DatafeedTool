namespace New_DFT.Forms
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.ribbonPage2 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btn_close = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.tb_server = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.cb_auth = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label_username = new DevExpress.XtraEditors.LabelControl();
            this.tb_username = new DevExpress.XtraEditors.TextEdit();
            this.label_password = new DevExpress.XtraEditors.LabelControl();
            this.tb_password = new DevExpress.XtraEditors.TextEdit();
            this.btn_connect = new DevExpress.XtraEditors.SimpleButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tb_server.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_auth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_username.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_password.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ribbonPage2
            // 
            this.ribbonPage2.Name = "ribbonPage2";
            this.ribbonPage2.Text = "ribbonPage2";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(127, 69);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(168, 25);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Connect to Server";
            // 
            // btn_close
            // 
            this.btn_close.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.btn_close.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_close.ImageOptions.Image")));
            this.btn_close.Location = new System.Drawing.Point(347, 4);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(34, 29);
            this.btn_close.TabIndex = 5;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(21, 144);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(100, 19);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Server name: ";
            // 
            // tb_server
            // 
            this.tb_server.Location = new System.Drawing.Point(127, 141);
            this.tb_server.Name = "tb_server";
            this.tb_server.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_server.Properties.Appearance.Options.UseFont = true;
            this.tb_server.Size = new System.Drawing.Size(245, 26);
            this.tb_server.TabIndex = 1;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(8, 198);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(113, 19);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "Authentication: ";
            // 
            // cb_auth
            // 
            this.cb_auth.Location = new System.Drawing.Point(127, 195);
            this.cb_auth.Name = "cb_auth";
            this.cb_auth.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_auth.Properties.Appearance.Options.UseFont = true;
            this.cb_auth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cb_auth.Properties.Items.AddRange(new object[] {
            "Windows Authentication",
            "SQL Server Authentication"});
            this.cb_auth.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cb_auth.Size = new System.Drawing.Size(245, 26);
            this.cb_auth.TabIndex = 2;
            this.cb_auth.SelectedIndexChanged += new System.EventHandler(this.cb_auth_SelectedIndexChanged);
            // 
            // label_username
            // 
            this.label_username.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_username.Appearance.Options.UseFont = true;
            this.label_username.Location = new System.Drawing.Point(34, 243);
            this.label_username.Name = "label_username";
            this.label_username.Size = new System.Drawing.Size(87, 19);
            this.label_username.TabIndex = 2;
            this.label_username.Text = "User name: ";
            // 
            // tb_username
            // 
            this.tb_username.Location = new System.Drawing.Point(127, 240);
            this.tb_username.Name = "tb_username";
            this.tb_username.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_username.Properties.Appearance.Options.UseFont = true;
            this.tb_username.Size = new System.Drawing.Size(245, 26);
            this.tb_username.TabIndex = 3;
            // 
            // label_password
            // 
            this.label_password.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_password.Appearance.Options.UseFont = true;
            this.label_password.Location = new System.Drawing.Point(43, 275);
            this.label_password.Name = "label_password";
            this.label_password.Size = new System.Drawing.Size(78, 19);
            this.label_password.TabIndex = 2;
            this.label_password.Text = "Password: ";
            // 
            // tb_password
            // 
            this.tb_password.Location = new System.Drawing.Point(127, 272);
            this.tb_password.Name = "tb_password";
            this.tb_password.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_password.Properties.Appearance.Options.UseFont = true;
            this.tb_password.Properties.UseSystemPasswordChar = true;
            this.tb_password.Size = new System.Drawing.Size(245, 26);
            this.tb_password.TabIndex = 4;
            // 
            // btn_connect
            // 
            this.btn_connect.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_connect.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btn_connect.Appearance.Options.UseFont = true;
            this.btn_connect.Appearance.Options.UseForeColor = true;
            this.btn_connect.Location = new System.Drawing.Point(137, 357);
            this.btn_connect.Name = "btn_connect";
            this.btn_connect.Size = new System.Drawing.Size(145, 33);
            this.btn_connect.TabIndex = 8;
            this.btn_connect.Text = "Connect";
            this.btn_connect.Click += new System.EventHandler(this.btn_connect_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btn_close);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(393, 39);
            this.panel1.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(4, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 19);
            this.label1.TabIndex = 6;
            this.label1.Text = "Datafeed Tool - 2.0.4";
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 446);
            this.Controls.Add(this.btn_connect);
            this.Controls.Add(this.cb_auth);
            this.Controls.Add(this.tb_password);
            this.Controls.Add(this.tb_username);
            this.Controls.Add(this.tb_server);
            this.Controls.Add(this.label_password);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.label_username);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LoginForm";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tb_server.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_auth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_username.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_password.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btn_close;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit tb_server;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.ComboBoxEdit cb_auth;
        private DevExpress.XtraEditors.LabelControl label_username;
        private DevExpress.XtraEditors.TextEdit tb_username;
        private DevExpress.XtraEditors.LabelControl label_password;
        private DevExpress.XtraEditors.TextEdit tb_password;
        private DevExpress.XtraEditors.SimpleButton btn_connect;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
    }
}