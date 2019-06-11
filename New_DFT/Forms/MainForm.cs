using DevExpress.Utils.Menu;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Menu;
using DevExpress.XtraGrid.Views.Grid;
using New_DFT.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace New_DFT.Forms
{
    public partial class MainForm : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        private List<DatafeedResult> mQueryResult;
        private List<CellInfo> mChangedCells;

        public MainForm()
        {
            InitializeComponent();
        }

        class MenuColumnInfo
        {
            public GridColumn Column;
            public MenuColumnInfo(GridColumn column)
            {
                this.Column = column;
            }
        }

        class CellInfo
        {
            public int Row;
            public int Column;
            public CellInfo(int row, int column)
            {
                this.Row = row;
                this.Column = column;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();

            if (Global.SQL_CONNECTION == null)
            {
                Close();
                return;
            }

            label_server.Text = Global.SERVER_NAME;

            // Load Database list
            using (SqlCommand cmd = new SqlCommand("SELECT name FROM sys.databases;", Global.SQL_CONNECTION))
            {
                using (IDataReader dr = cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        cb_database.Properties.Items.Add(dr[0].ToString());
                    }
                }
            }

            // Hide "Select All" checkbox.
            check_selectAll.Visible = false;
            panel_find.Visible = false;
            mChangedCells = new List<CellInfo>();
        }

        private void item_runquery_Click(object sender, EventArgs e)
        {
            if (cb_database.SelectedIndex < 0)
            {
                MessageBox.Show("Please select database.");
                return;
            }
            mQueryResult = new List<DatafeedResult>();
            string sql = string.Format(@"
Use {0};
                            /**********************************************************************************************************/
                            --VARIABLES TO MODIFY
                            DECLARE @datafeedID INT = 0
                            DECLARE @forceWinAuth_username NVARCHAR(MAX) = '' --Leave blank to not force
                            DECLARE @forceWinAuth_password NVARCHAR(MAX) = '' --Leave blank to not force
                            DECLARE @forceArcherTransport_username NVARCHAR(MAX) = '' --Leave blank to not force
                            DECLARE @forceArcherTransport_password NVARCHAR(MAX) = '' --Leave blank to not force
                            DECLARE @forceDBTransport_username NVARCHAR(MAX) = '' --Leave blank to not force
                            DECLARE @forceDBTransport_password NVARCHAR(MAX) = '' --Leave blank to not force
                            DECLARE @forceLongValuesToEmptyString BIT = 1 --enabling blanks the field if it is longer than @MaxCredentialSize
                            DECLARE @ignoreArcherDashBoardFeed BIT = 1 --Max credential size to allow scripting of
                            DECLARE @MaxCredentialSize INT  = 1024 --Max credential size to allow scripting of

                            DECLARE @forceArcherTransport_uri NVARCHAR(MAX) = '' --Leave blank to not force
                            DECLARE @forceArcherTransport_InstanceName NVARCHAR(MAX) = '' --Leave blank to not force

                            DECLARE @forceWinAuth_domain NVARCHAR(MAX) = ''
                            DECLARE @forceArcherTransport_domain NVARCHAR(MAX) = ''
                            DECLARE @forceProxy_Name NVARCHAR(MAX) = ''
                            DECLARE @forceProxy_Port NVARCHAR(MAX) = ''
                            DECLARE @forceProxy_Username NVARCHAR(MAX) = ''
                            DECLARE @forceProxy_Password NVARCHAR(MAX) = ''
                            DECLARE @forceProxy_Domain NVARCHAR(MAX) = ''
                            DECLARE @forceProxy_Option NVARCHAR(MAX) = ''

                            DECLARE @forceConnectionString NVARCHAR(MAX) = ''
                            DECLARE @forceTransportUri NVARCHAR(MAX) = ''

                            IF OBJECT_ID('tempdb..#XMLElements') IS NOT NULL DROP TABLE #XMLElements
                            CREATE TABLE #XMLElements (OptionName NVARCHAR(256), XML_key NVARCHAR(MAX), forceValue NVARCHAR(MAX), optionOrder DECIMAL(5,3))

                            INSERT INTO #XMLElements (OptionName, XML_key, forceValue, optionOrder)
                            VALUES
                                (  N'WinAuth_username' , N'(/*:DataFeed/*:Transporter/*:ArcherWebServiceTransportActivity/@WindowsAuthUserName)[1]', @forceWinAuth_username, 1.1 )
                            , (  N'WinAuth_password' , N'(/*:DataFeed/*:Transporter/*:ArcherWebServiceTransportActivity/@WindowsAuthPassword)[1]', @forceWinAuth_password, 1.2 )
                            , (  N'ArcherTransport_username' , N'(/*:DataFeed/*:Transporter/*:ArcherWebServiceTransportActivity/*:ArcherWebServiceTransportActivity.Credentials/*:NetworkCredentialWrapper/@UserName)[1]', @forceArcherTransport_username, 2.1)
                            , (  N'ArcherTransport_password' , N'(/*:DataFeed/*:Transporter/*:ArcherWebServiceTransportActivity/*:ArcherWebServiceTransportActivity.Credentials/*:NetworkCredentialWrapper/@Password)[1]', @forceArcherTransport_password, 2.2 )
                            , (  N'DBTransport_username' , N'(/*:DataFeed/*:Transporter/*:DBQueryDataFeedTransportActivity/*:DBQueryDataFeedTransportActivity.Credentials/*:NetworkCredentialWrapper/@UserName)[1]', @forceDBTransport_username, 3.1 )
                            , (  N'DBTransport_password' , N'(/*:DataFeed/*:Transporter/*:DBQueryDataFeedTransportActivity/*:DBQueryDataFeedTransportActivity.Credentials/*:NetworkCredentialWrapper/@Password)[1]' , @forceDBTransport_password, 3.2)

                            , (  N'ArcherTransport_uri' , N'(/*:DataFeed/*:Transporter/*:ArcherWebServiceTransportActivity/@Uri)[1]', @forceArcherTransport_uri, 2.3)
                            , (  N'ArcherTransport_InstanceName' , N'(/*:DataFeed/*:Transporter/*:ArcherWebServiceTransportActivity/@InstanceName)[1]', @forceArcherTransport_InstanceName, 2.4)

                            , (  N'WinAuth_domain' , N'(/*:DataFeed/*:Transporter/*:ArcherWebServiceTransportActivity/@WindowsAuthDomain)[1]', @forceWinAuth_domain, 4.1)
                            , (  N'ArcherTransport_domain' , N'(/*:DataFeed/*:Transporter/*:ArcherWebServiceTransportActivity/*:ArcherWebServiceTransportActivity.Credentials/*:NetworkCredentialWrapper/@Domain)[1]', @forceArcherTransport_domain, 4.2)
                            , (  N'Proxy_name' , N'(/*:DataFeed/*:Transporter/*:ArcherWebServiceTransportActivity/@ProxyName)[1]', @forceProxy_Name, 4.3) 
                            , (  N'Proxy_port' , N'(/*:DataFeed/*:Transporter/*:ArcherWebServiceTransportActivity/@ProxyPort)[1]', @forceProxy_Port, 4.4) 
                            , (  N'Proxy_username' , N'(/*:DataFeed/*:Transporter/*:ArcherWebServiceTransportActivity/@ProxyUsername)[1]', @forceProxy_Username, 4.5)
                            , (  N'Proxy_option' , N'(/*:DataFeed/*:Transporter/*:ArcherWebServiceTransportActivity/@ProxyOption)[1]', @forceProxy_Option, 4.6)
                            , (  N'Proxy_password' , N'(/*:DataFeed/*:Transporter/*:ArcherWebServiceTransportActivity/@ProxyPassword)[1]', @forceProxy_Password, 4.7)
                            , (  N'Proxy_domain' , N'(/*:DataFeed/*:Transporter/*:ArcherWebServiceTransportActivity/@ProxyDomain)[1]', @forceProxy_Domain, 4.8)
                            , (  N'ConnectionString', N'(/*:DataFeed/*:Transporter/*:DBQueryDataFeedTransportActivity/*:DBQueryDataFeedTransportActivity.DbQueryInfo/*:DbQueryInfo/@ConnectionString)[1]', @forceConnectionString, 4.9)
                            , (  N'Transport_uri' , N'(/*:DataFeed/*:Transporter/*:UNCDataFeedTransportActivity/@Uri)[1]', @forceTransportUri, 5.1)

                            /**********************************************************************************************************/
                            --CHANGE NOTHING BELOW HERE
                            SET NOCOUNT ON 

                            IF OBJECT_ID('tempdb..#DataFeedOptions') IS NOT NULL DROP TABLE #DataFeedOptions
                            IF OBJECT_ID('tempdb..#ModifyScripts') IS NOT NULL DROP TABLE #ModifyScripts
                            IF OBJECT_ID('tempdb..#agg') IS NOT NULL DROP TABLE #agg

                            DECLARE @maxValueLength INT = 256
                            DECLARE @SQLStmt NVARCHAR(MAX) = ''

                            CREATE TABLE #DataFeedOptions (
                              datafeed_id INT
                            , datafeed_guid UNIQUEIDENTIFIER
                            , datafeed_name NVARCHAR(256)
                            , OptionName NVARCHAR(256)
                            , OptionValue NVARCHAR(MAX)
                            , SetStmt NVARCHAR(MAX) DEFAULT ('')
                            , notes NVARCHAR(MAX) DEFAULT ''
                            )


                            INSERT INTO #DataFeedOptions
                            (  datafeed_id
                             , datafeed_guid
                             , datafeed_name
                             , OptionName
                            )
                            SELECT d.datafeed_id, d.guid, d.datafeed_name, xe.OptionName 
                            FROM dbo.tblDatafeed d
                            CROSS APPLY #XMLElements xe
                            WHERE (d.datafeed_id = @datafeedID OR @datafeedID = 0)
                            AND d.datafeed_type_id = 1
                            AND (d.guid <> 'A5AB2AA4-C563-45E5-A289-85BF8A0D2D82' OR @ignoreArcherDashBoardFeed = 0) 

                            SELECT @SQLStmt = @SQLStmt + '
                            UPDATE dc 
                            SET dc.OptionValue=' + CASE WHEN xe.forceValue <> '' THEN ''''+xe.forceValue+'''' ELSE ' d.configuration_xml.value('''+
                                        XML_key                
                                                        +''', ''nvarchar(max)'') ' END + '
                            FROM dbo.tblDatafeed d
                            JOIN #DataFeedOptions dc ON d.guid = dc.datafeed_guid AND dc.OptionName = '''+xe.OptionName+''''
                            from #XMLElements xe

                            EXEC (@SQLStmt)

                            IF @forceLongValuesToEmptyString = 1
                            BEGIN
                            UPDATE dfc SET dfc.OptionValue = '', dfc.notes = dfc.notes + dfc.OptionName + ' forced to empty string;'  FROM #DataFeedOptions dfc  WHERE DATALENGTH(OptionValue) > @MaxCredentialSize
                            END
                            ELSE 
                            BEGIN
                            UPDATE dfc SET dfc.OptionValue = '', dfc.notes = dfc.notes + dfc.OptionName + ' too big;'  FROM #DataFeedOptions dfc  WHERE DATALENGTH(OptionValue) > @MaxCredentialSize
                            END

                            UPDATE dfc 
                            SET dfc.SetStmt = 
                            'SET @configuration_xml.modify(''replace value of'+xe.XML_key +'with (""'+ dfc.OptionValue+'"")'')'
                             FROM #DataFeedOptions dfc
                             JOIN #XMLElements xe ON xe.OptionName = dfc.OptionName 
                             WHERE dfc.OptionValue IS NOT NULL


                            SELECT DISTINCT dfc.datafeed_id, dfc.datafeed_guid
                             , ISNULL(s.SetStmt, '') SetStmt, ISNULL(n.Notes, '') Notes
                               INTO #agg
                            FROM #DataFeedOptions dfc
                            CROSS APPLY
                            (
                                SELECT s.SetStmt + ' '
                                FROM #DataFeedOptions s
                                where dfc.datafeed_id = s.datafeed_id

                                AND s.SetStmt IS NOT NULL AND s.SetStmt <> ''
                                FOR XML PATH('')
                            ) s(SetStmt)
                            CROSS APPLY
                            (
                                SELECT n.Notes +' '
                                FROM #DataFeedOptions n
                                where dfc.datafeed_id = n.datafeed_id

                                AND n.notes IS NOT NULL AND n.notes <> ''
                                FOR XML PATH('')
                            ) n(Notes)

                            UPDATE a set a.SetStmt = '
                            DECLARE @configuration_xml XML
                            SELECT @configuration_xml = configuration_xml
                            FROM dbo.tblDatafeed d
                            WHERE d.guid = '''+ CAST(a.datafeed_guid AS NVARCHAR(256))+''' '
                            + a.SetStmt
                            + 'UPDATE d SET d.configuration_xml = @configuration_xml
                            FROM dbo.tblDatafeed d
                            WHERE d.guid = '''+ CAST(a.datafeed_guid AS NVARCHAR(256))+'''
                            '
                             FROM #agg a
                            WHERE a.SetStmt <> '' 

                            UPDATE #agg SET SetStmt = 'EXEC (''' + REPLACE(SetStmt,'''','''''') + ''')'

                            DECLARE @cols AS NVARCHAR(MAX),
                                @query  AS NVARCHAR(MAX);

                            SET @cols = STUFF((SELECT distinct ',' + QUOTENAME(c.OptionName) 
                                        FROM #DataFeedOptions c
                                        FOR XML PATH('') , TYPE
                                        ).value('.', 'NVARCHAR(MAX)') 
                                    ,1,1,'')

                            SET @cols = STUFF((SELECT ',' + QUOTENAME(c.OptionName) 
                                        FROM #XMLElements c ORDER BY c.optionOrder
                                        FOR XML PATH('') , TYPE
                                        ).value('.', 'NVARCHAR(MAX)') 
                                    ,1,1,'')

                             set @query = 'SELECT p.datafeed_id, p.datafeed_guid, p.datafeed_name, a.setStmt SQLUpdate, a.notes, ' + @cols + ' from 
                                        (
                                            select datafeed_id
				                            , datafeed_guid
				                            , datafeed_name 
                                                , OptionValue
                                                , OptionName
                                            from #DataFeedOptions
                                        ) x
                                        pivot 
                                        (
                                                max(OptionValue)
                                            for OptionName in (' + @cols + ')
                                        ) p join #agg a on a.datafeed_id = p.datafeed_id'


                            execute(@query)
", cb_database.SelectedItem);

            try
            {
                using (SqlCommand command = new SqlCommand(sql, Global.SQL_CONNECTION))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DatafeedResult result = new DatafeedResult();
                            //result.Selected = false;
                            result.DatafeedID = reader[0].ToString();
                            result.Guid = reader[1].ToString();
                            result.DatafeedName = reader[2].ToString();
                            //result.SqlUpdate = reader[3].ToString();
                            //result.Notes = reader[4].ToString();
                            result.SecurityUsername = reader[5].ToString();
                            result.SecurityPassword = reader[6].ToString();
                            result.TransportUsername = reader[7].ToString();
                            result.TransportPassword = reader[8].ToString();
                            result.TransportURL = reader[9].ToString();
                            result.TransportInstance = reader[10].ToString();
                            result.SQLQueryUsername = reader[11].ToString();
                            result.SQLQueryPassword = reader[12].ToString();
                            result.SecurityDomain = reader[13].ToString();
                            result.TransportDomain = reader[14].ToString();
                            result.ProxyName = reader[15].ToString();
                            result.ProxyPort = reader[16].ToString();
                            result.ProxyUserName = reader[17].ToString();
                            result.ProxyOption = reader[18].ToString();
                            result.ProxyPassword = reader[19].ToString();
                            result.ProxyDomain = reader[20].ToString();
                            result.ConnectionString = reader[21].ToString();
                            result.TransportPath = reader[22].ToString();

                            mQueryResult.Add(result);
                        }
                        gridview.DataSource = mQueryResult;
                        sub_gridview.Columns[0].Caption = " ";
                        sub_gridview.Columns[0].OptionsFilter.AllowFilter = false;
                        sub_gridview.Columns[0].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
                        sub_gridview.Columns[1].OptionsColumn.AllowEdit = false;
                        sub_gridview.Columns[2].OptionsColumn.AllowEdit = false;
                        sub_gridview.Columns[3].OptionsColumn.AllowEdit = false;
                        sub_gridview.BestFitColumns();
                    }
                }
                //panel_selectAll.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            check_selectAll.Visible = true;
            check_selectAll.Checked = false;
            panel_find.Visible = true;
        }

        private void sub_gridview_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.MenuType == GridMenuType.Column)
            {
                GridViewColumnMenu menu = e.Menu as GridViewColumnMenu;
                if (menu.Column.AbsoluteIndex == 0)
                {
                    menu.Items.Clear();
                }
                if (menu.Column.AbsoluteIndex > 2)
                {
                    DXMenuItem item = new DXMenuItem("Clear data", onClickClearColumn);
                    item.Tag = new MenuColumnInfo(menu.Column);
                    menu.Items.Add(item);
                }
            }
        }

        private void onClickClearColumn(object sender, EventArgs e)
        {
            DXMenuItem item = sender as DXMenuItem;
            MenuColumnInfo info = item.Tag as MenuColumnInfo;
            if (info == null) return;

            int columnIndex = info.Column.AbsoluteIndex;
            string columnName = "";

            foreach (DatafeedResult result in mQueryResult)
            {
                if (columnIndex == 3)
                {
                    result.SecurityUsername = "";
                    columnName = "Security Username";
                }
                if (columnIndex == 4)
                {
                    result.SecurityPassword = "";
                    columnName = "Security Password";
                }
                if (columnIndex == 5)
                {
                    result.TransportUsername = "";
                    columnName = "Transport username";
                }
                if (columnIndex == 6)
                {
                    result.TransportPassword = "";
                    columnName = "Transport Password";
                }
                if (columnIndex == 7)
                {
                    result.TransportURL = "";
                    columnName = "Transport URL";
                }
                if (columnIndex == 8)
                {
                    result.TransportInstance = "";
                    columnName = "Transport Instance";
                }
                if (columnIndex == 9)
                {
                    result.SecurityDomain = "";
                    columnName = "Security Domain";
                }
                if (columnIndex == 10)
                {
                    result.TransportDomain = "";
                    columnName = "Transport Domain";
                }
                if (columnIndex == 11)
                {
                    result.ProxyOption = "";
                    columnName = "Proxy Option";
                }
                if (columnIndex == 12)
                {
                    result.ProxyName = "";
                    columnName = "Proxy Name";
                }
                if (columnIndex == 13)
                {
                    result.ProxyPort = "";
                    columnName = "Proxy port";
                }
                if (columnIndex == 14)
                {
                    result.ProxyDomain = "";
                    columnName = "Proxy domain";
                }
                if (columnIndex == 15)
                {
                    result.ProxyUserName = "";
                    columnName = "Proxy username";
                }
                if (columnIndex == 16)
                {
                    result.ProxyPassword = "";
                    columnName = "Proxy password";
                }
                if (columnIndex == 17)
                {
                    result.TransportPath = "";
                    columnName = "Transport Path";
                }
                if (columnIndex == 18)
                {
                    result.ConnectionString = "";
                    columnName = "Connection String";
                }
                if (columnIndex == 19)
                {
                    result.SQLQueryUsername = "";
                    columnName = "SQLQUery username";
                }
                if (columnIndex == 20)
                {
                    result.SQLQueryPassword = "";
                    columnName = "SQLQUery password";
                }
            }

            gridview.DataSource = mQueryResult;
            sub_gridview.RefreshData();

            MessageBox.Show(string.Format("Clear {0} data.", columnName));
        }

        private void item_writechanges_Click(object sender, EventArgs e)
        {
            bool isUpdate = false, isError = false;

            if (cb_database.SelectedIndex < 0)
            {
                MessageBox.Show("Please select database");
                return;
            }
            if (gridview.DataSource == null)
            {
                MessageBox.Show("Run query first.");
                return;
            }

            foreach (DatafeedResult item in (List<DatafeedResult>)gridview.DataSource)
            {
                //if (item.Selected)
                {
                    isUpdate = true;

                    // Get ConfigurationXML and save it.
                    string query = string.Format("USE {0};", cb_database.SelectedItem);
                    query += string.Format("SELECT configuration_xml FROM dbo.tblDatafeed WHERE datafeed_id={0}", item.DatafeedID);
                    using (SqlCommand command = new SqlCommand(query, Global.SQL_CONNECTION))
                    {
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            if (dataReader.Read())
                            {
                                string config_xml = dataReader[0].ToString();
                                if (!Directory.Exists("ConfigurationXmlBackups"))
                                {
                                    Directory.CreateDirectory("ConfigurationXmlBackups");
                                }
                                string filePath = string.Format(@"ConfigurationXmlBackups\{0}.json", item.Guid);
                                File.WriteAllText(filePath, config_xml);
                            }
                        }
                    }

                    string updateQuery = GetUpdateQuery(item);
                    // Save UpdateQuery
                    if (!Directory.Exists("WriteQueryLogs"))
                    {
                        Directory.CreateDirectory("WriteQueryLogs");
                    }
                    DateTime now = DateTime.Now;
                    string fileName = now.Year + "-" + now.Month + "-" + now.Day + "_" + now.Hour + "." + now.Minute;
                    string logPath = string.Format(@"WriteQueryLogs\{0}.txt", fileName);
                    File.WriteAllText(logPath, updateQuery);

                    using (SqlCommand command = new SqlCommand(updateQuery, Global.SQL_CONNECTION))
                    {
                        try
                        {
                            command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            isError = true;
                            MessageBox.Show(ex.Message);
                            break;
                        }
                    }
                }
            }
            if (isError) return;
            if (isUpdate)
            {
                MessageBox.Show("Successfully ran query.");
            }
            else
            {
                MessageBox.Show("No row is selected.");
            }

            mChangedCells = new List<CellInfo>();
            sub_gridview.RefreshData();
        }

        private string GetUpdateQuery(DatafeedResult data)
        {
            string result = string.Format("USE {0};", cb_database.SelectedItem);
            result += @"EXEC ('  
                        DECLARE @configuration_xml XML  
                        SELECT @configuration_xml = configuration_xml  
                        FROM dbo.tblDatafeed d ";

            result += string.Format("WHERE d.guid = ''{0}''", data.Guid);
            result += string.Format(@"SET @configuration_xml.modify(	''replace value of(/*:DataFeed/*:Transporter/*:ArcherWebServiceTransportActivity/@WindowsAuthUserName)[1]with 	(""{0}"")'')", data.SecurityUsername);
            result += string.Format(@"SET @configuration_xml.modify(''replace value of(/*:DataFeed/*:Transporter/*:ArcherWebServiceTransportActivity/@WindowsAuthPassword)[1]with 	(""{0}"")'')", data.SecurityPassword);
            result += string.Format(@"SET @configuration_xml.modify(''replace value of(/*:DataFeed/*:Transporter/*:ArcherWebServiceTransportActivity/*:ArcherWebServiceTransportActivity.Credentials/*:NetworkCredentialWrapper/@UserName)[1]with (""{0}"")'')", data.TransportUsername);
            result += string.Format(@"SET @configuration_xml.modify(''replace value of(/*:DataFeed/*:Transporter/*:ArcherWebServiceTransportActivity/*:ArcherWebServiceTransportActivity.Credentials/*:NetworkCredentialWrapper/@Password)[1]with (""{0}"")'')", data.TransportPassword);
            result += string.Format(@"SET @configuration_xml.modify(''replace value of(/*:DataFeed/*:Transporter/*:ArcherWebServiceTransportActivity/@Uri)[1]with (""{0}"")'')", data.TransportURL);
            result += string.Format(@"SET @configuration_xml.modify(''replace value of(/*:DataFeed/*:Transporter/*:ArcherWebServiceTransportActivity/@InstanceName)[1]with (""{0}"")'')", data.TransportInstance);
            result += string.Format(@"SET @configuration_xml.modify(''replace value of(/*:DataFeed/*:Transporter/*:ArcherWebServiceTransportActivity/@WindowsAuthDomain)[1]with (""{0}"")'')", data.SecurityDomain);
            result += string.Format(@"SET @configuration_xml.modify(''replace value of(/*:DataFeed/*:Transporter/*:ArcherWebServiceTransportActivity/*:ArcherWebServiceTransportActivity.Credentials/*:NetworkCredentialWrapper/@Domain)[1]with (""{0}"")'')", data.TransportDomain);
            result += string.Format(@"SET @configuration_xml.modify(''replace value of(/*:DataFeed/*:Transporter/*:ArcherWebServiceTransportActivity/@ProxyName)[1]with (""{0}"")'')", data.ProxyName);
            result += string.Format(@"SET @configuration_xml.modify(''replace value of(/*:DataFeed/*:Transporter/*:ArcherWebServiceTransportActivity/@ProxyPort)[1]with (""{0}"")'')", data.ProxyPort);
            result += string.Format(@"SET @configuration_xml.modify(''replace value of(/*:DataFeed/*:Transporter/*:ArcherWebServiceTransportActivity/@ProxyUserName)[1]with (""{0}"")'')", data.ProxyUserName);
            result += string.Format(@"SET @configuration_xml.modify(''replace value of(/*:DataFeed/*:Transporter/*:ArcherWebServiceTransportActivity/@ProxyOption)[1]with (""{0}"")'')", data.ProxyOption);
            result += string.Format(@"SET @configuration_xml.modify(''replace value of(/*:DataFeed/*:Transporter/*:ArcherWebServiceTransportActivity/@ProxyPassword)[1]with (""{0}"")'')", data.ProxyPassword);
            result += string.Format(@"SET @configuration_xml.modify(''replace value of(/*:DataFeed/*:Transporter/*:ArcherWebServiceTransportActivity/@ProxyDomain)[1]with (""{0}"")'')", data.ProxyDomain);
            result += string.Format(@"SET @configuration_xml.modify(''replace value of(/*:DataFeed/*:Transporter/*:DBQueryDataFeedTransportActivity/*:DBQueryDataFeedTransportActivity.DbQueryInfo/*:DbQueryInfo/@ConnectionString)[1]with (""{0}"")'')", data.ConnectionString);
            result += string.Format(@"SET @configuration_xml.modify(''replace value of(/*:DataFeed/*:Transporter/*:UNCDataFeedTransportActivity/@Uri)[1]with (""{0}"")'')", data.TransportPath);

            result += "UPDATE d SET d.configuration_xml = @configuration_xml  FROM dbo.tblDatafeed d";
            result += string.Format(" WHERE d.guid = ''{0}''", data.Guid);
            result += "')";

            return result;
        }

        private void item_save_Click(object sender, EventArgs e)
        {
            if (gridview.DataSource == null)
            {
                MessageBox.Show("Run query first.");
                return;
            }
            List<DatafeedResult> selectedRows = new List<DatafeedResult>();
            foreach (DatafeedResult item in (List<DatafeedResult>)gridview.DataSource)
            {
                //if (item.Selected)
                {
                    selectedRows.Add(item);
                }
            }
            if (selectedRows.Count < 1)
            {
                MessageBox.Show("No row is selected!");
                return;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save datafeed result";
            saveFileDialog.DefaultExt = "json";
            saveFileDialog.Filter = "*.json|All files(*.*)";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string json = "[\n";

                foreach (DatafeedResult item in selectedRows)
                {
                    json += @"  {" + "\n";
                    json += @"    ""DatafeedID"": """ + item.DatafeedID + @"""," + "\n";
                    json += @"    ""Guid"": """ + item.Guid + @"""," + "\n";
                    json += @"    ""DatafeedName"": """ + item.DatafeedName + @"""," + "\n";
                    json += @"    ""SecurityUsername"": """ + item.SecurityUsername + @"""," + "\n";
                    json += @"    ""SecurityPassword"": """ + item.SecurityPassword + @"""," + "\n";
                    json += @"    ""TransportUsername"": """ + item.TransportUsername + @"""," + "\n";
                    json += @"    ""TransportPassword"": """ + item.TransportPassword + @"""," + "\n";
                    json += @"    ""TransportURL"": """ + item.TransportURL + @"""," + "\n";
                    json += @"    ""TransportInstance"": """ + item.TransportInstance + @"""," + "\n";
                    json += @"    ""SQLQueryUsername"": """ + item.SQLQueryUsername + @"""," + "\n";
                    json += @"    ""SQLQueryPassword"": """ + item.SQLQueryPassword + @"""," + "\n";
                    json += @"    ""SecurityDomain"": """ + item.SecurityDomain + @"""," + "\n";
                    json += @"    ""TransportDomain"": """ + item.TransportDomain + @"""," + "\n";
                    json += @"    ""ProxyName"": """ + item.ProxyName + @"""," + "\n";
                    json += @"    ""ProxyPort"": """ + item.ProxyPort + @"""," + "\n";
                    json += @"    ""ProxyUserName"": """ + item.ProxyUserName + @"""," + "\n";
                    json += @"    ""ProxyOption"": """ + item.ProxyOption + @"""," + "\n";
                    json += @"    ""ProxyPassword"": """ + item.ProxyPassword + @"""," + "\n";
                    json += @"    ""ProxyDomain"": """ + item.ProxyDomain + @"""," + "\n";
                    json += @"    ""ConnectionString"": """ + item.ConnectionString + @"""," + "\n";
                    json += @"    ""TransportPath"": """ + item.TransportPath + @"""" + "\n";
                    json += "  },\n";
                }
                json += @"]";
                File.WriteAllText(saveFileDialog.FileName, json);
                MessageBox.Show("Successfully saved.");
            }
        }

        private void item_load_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Open datafeed result";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string json = File.ReadAllText(openFileDialog.FileName);
                try
                {
                    List<DatafeedResult> datafeedResults = JsonConvert.DeserializeObject<List<DatafeedResult>>(json);
                    gridview.DataSource = datafeedResults;

                    sub_gridview.Columns[0].Caption = " ";
                    sub_gridview.Columns[0].OptionsFilter.AllowFilter = false;
                    sub_gridview.Columns[0].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
                    sub_gridview.Columns[1].OptionsColumn.AllowEdit = false;
                    sub_gridview.Columns[2].OptionsColumn.AllowEdit = false;
                    sub_gridview.Columns[3].OptionsColumn.AllowEdit = false;
                    sub_gridview.BestFitColumns();

                    check_selectAll.Visible = true;
                    check_selectAll.Checked = false;
                    panel_find.Visible = true;

                    MessageBox.Show("Successfully Loaded.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void check_selectAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DatafeedResult item in (List<DatafeedResult>)gridview.DataSource)
            {
                //item.Selected = check_selectAll.Checked;
            }
            sub_gridview.RefreshData();
        }

        private void btn_find_Click(object sender, EventArgs e)
        {
            List<DatafeedResult> datafeedResults = new List<DatafeedResult>();

            foreach (DatafeedResult item in mQueryResult)
            {
                if (item.DatafeedID.Contains(text_find.Text) ||
                    item.Guid.Contains(text_find.Text) ||
                    item.DatafeedName.Contains(text_find.Text) ||
                    item.SecurityUsername.Contains(text_find.Text) ||
                    item.SecurityPassword.Contains(text_find.Text) ||
                    item.TransportUsername.Contains(text_find.Text) ||
                    item.TransportPassword.Contains(text_find.Text) ||
                    item.TransportURL.Contains(text_find.Text) ||
                    item.TransportInstance.Contains(text_find.Text) ||
                    item.SQLQueryUsername.Contains(text_find.Text) ||
                    item.SQLQueryPassword.Contains(text_find.Text) ||
                    item.SecurityDomain.Contains(text_find.Text) ||
                    item.TransportDomain.Contains(text_find.Text) ||
                    item.ProxyOption.Contains(text_find.Text) ||
                    item.ProxyName.Contains(text_find.Text) ||
                    item.ProxyPort.Contains(text_find.Text) ||
                    item.ProxyDomain.Contains(text_find.Text) ||
                    item.ProxyUserName.Contains(text_find.Text) ||
                    item.ProxyPassword.Contains(text_find.Text) ||
                    item.ConnectionString.Contains(text_find.Text) ||
                    item.TransportPath.Contains(text_find.Text)
                    )
                {
                    datafeedResults.Add(item);
                }
            }
            gridview.DataSource = datafeedResults;
            if (datafeedResults.Count < 1)
            {
                MessageBox.Show("There are no match rows.");
            }
        }

        private void btn_replace_Click(object sender, EventArgs e)
        {
            if (text_find.Text.Length < 1)
            {
                MessageBox.Show("Input search keyword.");
                return;
            }
            if (text_replace.Text.Length < 1)
            {
                MessageBox.Show("Input replace keyword.");
                return;
            }
            List<DatafeedResult> datafeedResults = new List<DatafeedResult>();
            int replaceCount = 0;
            foreach (DatafeedResult item in (List<DatafeedResult>)gridview.DataSource)
            {
                if (item.DatafeedID.Contains(text_find.Text))
                {
                    item.DatafeedID = item.DatafeedID.Replace(text_find.Text, text_replace.Text);
                    replaceCount += 1;
                }
                if (item.Guid.Contains(text_find.Text))
                {
                    item.Guid = item.Guid.Replace(text_find.Text, text_replace.Text);
                    replaceCount += 1;
                }
                if (item.DatafeedName.Contains(text_find.Text))
                {
                    item.DatafeedName = item.DatafeedName.Replace(text_find.Text, text_replace.Text);
                    replaceCount += 1;
                }
                if (item.SecurityUsername.Contains(text_find.Text))
                {
                    item.SecurityUsername = item.SecurityUsername.Replace(text_find.Text, text_replace.Text);
                    replaceCount += 1;
                }
                if (item.SecurityPassword.Contains(text_find.Text))
                {
                    item.SecurityPassword = item.SecurityPassword.Replace(text_find.Text, text_replace.Text);
                    replaceCount += 1;
                }
                if (item.TransportUsername.Contains(text_find.Text))
                {
                    item.TransportUsername = item.TransportUsername.Replace(text_find.Text, text_replace.Text);
                    replaceCount += 1;
                }
                if (item.TransportPassword.Contains(text_find.Text))
                {
                    item.TransportPassword = item.TransportPassword.Replace(text_find.Text, text_replace.Text);
                    replaceCount += 1;
                }
                if (item.TransportURL.Contains(text_find.Text))
                {
                    item.TransportURL = item.TransportURL.Replace(text_find.Text, text_replace.Text);
                    replaceCount += 1;
                }
                if (item.TransportInstance.Contains(text_find.Text))
                {
                    item.TransportInstance = item.TransportInstance.Replace(text_find.Text, text_replace.Text);
                    replaceCount += 1;
                }
                if (item.SQLQueryUsername.Contains(text_find.Text))
                {
                    item.SQLQueryUsername = item.SQLQueryUsername.Replace(text_find.Text, text_replace.Text);
                    replaceCount += 1;
                }
                if (item.SQLQueryPassword.Contains(text_find.Text))
                {
                    item.SQLQueryPassword = item.SQLQueryPassword.Replace(text_find.Text, text_replace.Text);
                    replaceCount += 1;
                }
                if (item.SecurityDomain.Contains(text_find.Text))
                {
                    item.SecurityDomain = item.SecurityDomain.Replace(text_find.Text, text_replace.Text);
                    replaceCount += 1;
                }
                if (item.TransportDomain.Contains(text_find.Text))
                {
                    item.TransportDomain = item.TransportDomain.Replace(text_find.Text, text_replace.Text);
                    replaceCount += 1;
                }
                if (item.ProxyOption.Contains(text_find.Text))
                {
                    item.ProxyOption = item.ProxyOption.Replace(text_find.Text, text_replace.Text);
                    replaceCount += 1;
                }
                if (item.ProxyName.Contains(text_find.Text))
                {
                    item.ProxyName = item.ProxyName.Replace(text_find.Text, text_replace.Text);
                    replaceCount += 1;
                }
                if (item.ProxyPort.Contains(text_find.Text))
                {
                    item.ProxyPort = item.ProxyPort.Replace(text_find.Text, text_replace.Text);
                    replaceCount += 1;
                }
                if (item.ProxyDomain.Contains(text_find.Text))
                {
                    item.ProxyDomain = item.ProxyDomain.Replace(text_find.Text, text_replace.Text);
                    replaceCount += 1;
                }
                if (item.ProxyUserName.Contains(text_find.Text))
                {
                    item.ProxyUserName = item.ProxyUserName.Replace(text_find.Text, text_replace.Text);
                    replaceCount += 1;
                }
                if (item.ProxyPassword.Contains(text_find.Text))
                {
                    item.ProxyPassword = item.ProxyPassword.Replace(text_find.Text, text_replace.Text);
                    replaceCount += 1;
                }
                if (item.ConnectionString.Contains(text_find.Text))
                {
                    item.ConnectionString = item.ConnectionString.Replace(text_find.Text, text_replace.Text);
                    replaceCount += 1;
                }
                if (item.TransportPath.Contains(text_find.Text))
                {
                    item.TransportPath = item.TransportPath.Replace(text_find.Text, text_replace.Text);
                    replaceCount += 1;
                }

                datafeedResults.Add(item);
            }
            gridview.DataSource = datafeedResults;
            MessageBox.Show(string.Format("Successfully Replaced {0} values", replaceCount));
        }

        private void sub_gridview_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            mChangedCells.Add(new CellInfo(e.RowHandle, e.Column.AbsoluteIndex));
        }

        private void sub_gridview_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            foreach (CellInfo cellInfo in mChangedCells)
            {
                if (e.RowHandle == cellInfo.Row && e.Column.AbsoluteIndex == cellInfo.Column)
                {
                    e.Appearance.BackColor = Color.LightPink;

                }
            }
        }
    }
}
