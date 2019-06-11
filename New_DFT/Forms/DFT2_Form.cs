using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using System.Data.SqlClient;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraSplashScreen;
using New_DFT.Models;
using System.IO;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Menu;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;

namespace New_DFT.Forms
{
    public partial class DFT2_Form : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        List<DatafeedResult> mQueryResult = null;
        List<CellInfo> mChangedCells = new List<CellInfo>();

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

        class MenuColumnInfo
        {
            public GridColumn Column;
            public MenuColumnInfo(GridColumn column)
            {
                this.Column = column;
            }
        }

        public DFT2_Form()
        {
            InitializeComponent();
        }

        private void DFT2_Form_Load(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();

            if (Global.SQL_CONNECTION == null)
            {
                Close();
                return;
            }

            SplashScreenManager.ShowForm(this, typeof(WaitForm), true, true, false);
            LoadDatabase();
            label_serverName.Caption = Global.SERVER_NAME;
            SplashScreenManager.CloseForm(false);
        }

        void LoadDatabase()
        {
            using (SqlCommand cmd = new SqlCommand("SELECT name FROM sys.databases;", Global.SQL_CONNECTION))
            {
                using (IDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        (bei_database.Edit as RepositoryItemComboBox).Items.Add(dr[0].ToString());
                    }
                }
            }
        }

        private void bei_database_ItemClick(object sender, ItemClickEventArgs e)
        {
            label_database.Caption = bei_database.EditValue.ToString();
        }

        private void repositoryItemComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label_database.Caption = bei_database.EditValue.ToString();

            btn_runQuery.Enabled = true;
            btn_writeChanges.Enabled = true;
        }

        private void btn_runQuery_ItemClick(object sender, ItemClickEventArgs e)
        {
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
", bei_database.EditValue.ToString());

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
                        gridControl.DataSource = mQueryResult;

                        //gridView.Columns[0].Caption = " ";
                        //gridView.Columns[0].OptionsFilter.AllowFilter = false;
                        //gridView.Columns[0].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
                        
                        gridView.Columns[0].OptionsColumn.AllowEdit = false;
                        gridView.Columns[1].OptionsColumn.AllowEdit = false;
                        gridView.Columns[2].OptionsColumn.AllowEdit = false;
                        
                        gridView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                        gridView.OptionsSelection.MultiSelect = true;

                        gridView.BestFitColumns();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_writeChanges_ItemClick(object sender, ItemClickEventArgs e)
        {
            bool isError = false;
            if (gridControl.DataSource == null)
            {
                ShowMessageBox("Warning", "Run query first.");
                return;
            }
            int[] selectedRows = gridView.GetSelectedRows();
            if (selectedRows.Length < 1)
            {
                ShowMessageBox("Warning", "No row is selected.");
                return;
            }

            List<DatafeedResult> datafeedResults = (List <DatafeedResult>) gridControl.DataSource;

            foreach (var index in selectedRows)
            {
                var item = datafeedResults[index];
                // Get ConfigurationXML and save it.
                string query = string.Format("USE {0};", bei_database.EditValue.ToString());
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

            if (isError) return;
            
            ShowMessageBox("Message", "Successfully ran query.");
            mChangedCells = new List<CellInfo>();
            gridView.RefreshData();
        }

        private string GetUpdateQuery(DatafeedResult data)
        {
            string result = string.Format("USE {0};", bei_database.EditValue.ToString());
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
            //result += string.Format(@"SET @configuration_xml.modify(''replace value of(/*:DataFeed/*:Transporter/*:DBQueryDataFeedTransportActivity/*:DBQueryDataFeedTransportActivity.DbQueryInfo/*:DbQueryInfo/@ConnectionString)[1]with (""{0}"")'')", data.ConnectionString);
            result += string.Format(@"SET @configuration_xml.modify(''replace value of(/*:DataFeed/*:Transporter/*:UNCDataFeedTransportActivity/@Uri)[1]with (""{0}"")'')", data.TransportPath);

            result += UpdateConnectionStringQuery(data.ConnectionString);

            result += "UPDATE d SET d.configuration_xml = @configuration_xml  FROM dbo.tblDatafeed d";
            result += string.Format(" WHERE d.guid = ''{0}''", data.Guid);
            result += "')";

            return result;
        }

        private void gridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            mChangedCells.Add(new CellInfo(e.RowHandle, e.Column.AbsoluteIndex));
        }

        private void gridView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            foreach (CellInfo cellInfo in mChangedCells)
            {
                if (cellInfo.Column == 0) continue;
                if (e.RowHandle == cellInfo.Row && e.Column.AbsoluteIndex == cellInfo.Column)
                {
                    e.Appearance.BackColor = Color.LightPink;
                }
            }
        }

        private void gridView_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
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

            gridControl.DataSource = mQueryResult;
            gridView.RefreshData();

            MessageBox.Show(string.Format("Clear {0} data.", columnName));
        }

        void ShowMessageBox(string caption, string content)
        {
            XtraMessageBoxArgs args = new XtraMessageBoxArgs();
            args.Caption = caption;
            args.Text = content;
            args.Buttons = new DialogResult[] { DialogResult.OK };
            //args.Showing += Args_Showing;
            XtraMessageBox.Show(args).ToString();
        }

        string UpdateConnectionStringQuery(string connectionString)
        {
            string query = @"
DECLARE @connection_string nvarchar(max)
SET @connection_string = @configuration_xml.value(''(/*:DataFeed/*:Transporter/*:DBQueryDataFeedTransportActivity/*:DBQueryDataFeedTransportActivity.DbQueryInfo/*:DbQueryInfo/@ConnectionString)[1]'', ''nvarchar(max)'')

if (@connection_string is null)
	begin
	DECLARE @is_transporter int = @configuration_xml.exist(''/*:DataFeed/*:Transporter'')
	if (@is_transporter > 0)
		begin
		DECLARE @is_DBQueryDataFeedTransportActivity int = @configuration_xml.exist(''/*:DataFeed/*:Transporter/*:DBQueryDataFeedTransportActivity'')
		if (@is_DBQueryDataFeedTransportActivity > 0)
			begin
			DECLARE @is_ActivityDbQueryInfo int = @configuration_xml.exist(''/*:DataFeed/*:Transporter/*:DBQueryDataFeedTransportActivity/*:DBQueryDataFeedTransportActivity.DbQueryInfo'')
			if (@is_ActivityDbQueryInfo > 0)
				begin
				DECLARE @is_DbQueryInfo int = @configuration_xml.exist(''/*:DataFeed/*:Transporter/*:DBQueryDataFeedTransportActivity/*:DBQueryDataFeedTransportActivity.DbQueryInfo/*:DbQueryInfo'')
				if (@is_DbQueryInfo > 0)
					begin
					DECLARE @is_connectionString int = @configuration_xml.exist(''/*:DataFeed/*:Transporter/*:DBQueryDataFeedTransportActivity/*:DBQueryDataFeedTransportActivity.DbQueryInfo/*:DbQueryInfo/@ConnectionString'')
					SET @configuration_xml.modify(''
					insert attribute ConnectionString {""" + connectionString + @"""}";

            query += string.Format(@"
                    into(/*:DataFeed/*:Transporter/*:DBQueryDataFeedTransportActivity/*:DBQueryDataFeedTransportActivity.DbQueryInfo/*:DbQueryInfo[@ConnectionString])[1]
					'')
					end
				else
					SET @configuration_xml.modify(''
					insert <DbQueryInfo ConnectionString=""{0}""/>
					as first
					into (/*:DataFeed/*:Transporter/*:DBQueryDataFeedTransportActivity/*:DBQueryDataFeedTransportActivity.DbQueryInfo)[1]
					'')
				end
			else
				DECLARE @activityDbQueryInfo_xml XML = ''
				<DBQueryDataFeedTransportActivity.DbQueryInfo>
					<DbQueryInfo ConnectionString=""{0}""/>
				</DBQueryDataFeedTransportActivity.DbQueryInfo>
				''
				SET @configuration_xml.modify(''
				insert sql:variable(""@activityDbQueryInfo_xml"")
				into (/*:DataFeed/*:Transporter/*:DBQueryDataFeedTransportActivity)[1]
				'')
			end
		else
			DECLARE @dBQueryDataFeedTransportActivity_xml XML = ''
			<DBQueryDataFeedTransportActivity>
				<DBQueryDataFeedTransportActivity.DbQueryInfo>
					<DbQueryInfo ConnectionString=""{0}""/>
				</DBQueryDataFeedTransportActivity.DbQueryInfo>
			</DBQueryDataFeedTransportActivity>
			''

			SET @configuration_xml.modify(''
			insert sql:variable(""@dBQueryDataFeedTransportActivity_xml"")
			into (/*:DataFeed/*:Transporter)[1]
			'')
		end
	else
		DECLARE @transporter_xml XML = ''
		<Transporter>
			<DBQueryDataFeedTransportActivity>
				<DBQueryDataFeedTransportActivity.DbQueryInfo>
					<DbQueryInfo ConnectionString=""{0}""/>
				</DBQueryDataFeedTransportActivity.DbQueryInfo>
			</DBQueryDataFeedTransportActivity>
		</Transporter>''
		
		SET @configuration_xml.modify(''
		insert sql:variable(""@transporter_xml"")
		into (/*:DataFeed)[1]
		'')
	end
else
	SET @configuration_xml.modify(''replace value of(/*:DataFeed/*:Transporter/*:DBQueryDataFeedTransportActivity/*:DBQueryDataFeedTransportActivity.DbQueryInfo/*:DbQueryInfo/@ConnectionString)[1]with (""{0}"")'')
", connectionString);
            return query;
        }
    }
}