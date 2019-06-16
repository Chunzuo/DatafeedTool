using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace New_DFT
{
    public class Global
    {
        public static SqlConnection SQL_CONNECTION;
        public static string SERVER_NAME;

        /**
         * XtraMessageBox dialog for Global usage.
         * */
        public static void ShowMessageBox(string caption, string content)
        {
            XtraMessageBoxArgs args = new XtraMessageBoxArgs();
            args.Caption = caption;
            args.Text = content;
            args.Buttons = new DialogResult[] { DialogResult.OK };
            XtraMessageBox.Show(args).ToString();
        }
    }
}
