using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using System.Threading;
using DevExpress.XtraSplashScreen;
using New_DFT.Models;

namespace New_DFT.Forms
{
    public partial class LoginForm : DevExpress.XtraEditors.XtraForm
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            cb_auth.SelectedIndex = 0;
            CheckAuthMode();
            LoadLocalDB();
        }

        private void CheckAuthMode()
        {
            if (cb_auth.SelectedIndex == 0)
            {
                // Windows Auth Mode
                label_username.ForeColor = Color.Gray;
                label_password.ForeColor = Color.Gray;
                tb_username.Enabled = false;
                tb_password.Enabled = false;
            }
            else
            {
                // SQL Server Auth mode
                label_username.ForeColor = Color.Black;
                label_password.ForeColor = Color.Black;
                tb_username.Enabled = true;
                tb_password.Enabled = true;
            }
        }

        private void cb_auth_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckAuthMode();
        }

        private void btn_connect_Click(object sender, EventArgs e)
        {
            if (tb_server.Text == "")
            {
                MessageBox.Show("Please input server name");
                return;
            }
            if (cb_auth.SelectedIndex == 1)
            {
                if (tb_username.Text == "")
                {
                    MessageBox.Show("Please input user name");
                    return;
                }
                if (tb_password.Text == "")
                {
                    MessageBox.Show("Please input password");
                    return;
                }
            }
            SplashScreenManager.ShowForm(this, typeof(WaitForm), true, true, false);

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = tb_server.Text;
            if (cb_auth.SelectedIndex == 0)
            {
                builder.IntegratedSecurity = true;
            }
            else
            {
                builder.UserID = tb_username.Text;
                builder.Password = tb_password.Text;
            }
            Global.SQL_CONNECTION = new SqlConnection(builder.ConnectionString);
            Global.SERVER_NAME = tb_server.Text;
            try
            {
                Global.SQL_CONNECTION.Open();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            SaveLocalDBInfo();
            SplashScreenManager.CloseForm(false);
            Close();
        }

        private void SaveLocalDBInfo()
        {
            LocalDB localDB = new LocalDB();
            localDB.ServerName = tb_server.Text;
            localDB.Save();
        }

        private void LoadLocalDB()
        {
            LocalDB localDB = LocalDB.Load();
            if (localDB != null)
            {
                tb_server.Text = localDB.ServerName;
            }
        }
    }
}