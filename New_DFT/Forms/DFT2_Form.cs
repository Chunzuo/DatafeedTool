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
using Newtonsoft.Json;
using DevExpress.XtraLayout;
using DevExpress.XtraCharts;
using System.Xml;
using System.Xml.Linq;

namespace New_DFT.Forms
{
    public partial class DFT2_Form : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        List<DatafeedResult> mQueryResult = null;
        List<CellInfo> mChangedCells = new List<CellInfo>();
        List<DatafeedHistoryResult> mHistoryResult = null;

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

            gridControl.Visible = false;
            chartControl.Visible = false;

            // Initialize DatafeedTool History
            bei_startDate.EditValue = DateTime.Today;
            bei_endDate.EditValue = DateTime.Today;
            for (int i = 1; i <= 12; i++)
            {
                (bei_startTime.Edit as RepositoryItemComboBox).Items.Add(i + " :00 AM");
                (bei_endTime.Edit as RepositoryItemComboBox).Items.Add(i + " :00 AM");
            }
            for (int i = 1; i <= 12; i++)
            {
                (bei_startTime.Edit as RepositoryItemComboBox).Items.Add(i + " :00 PM");
                (bei_endTime.Edit as RepositoryItemComboBox).Items.Add(i + " :00 PM");
            }
            bei_startTime.EditValue = (bei_startTime.Edit as RepositoryItemComboBox).Items[0];
            bei_endTime.EditValue = (bei_endTime.Edit as RepositoryItemComboBox).Items[23];
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

        /**
         * Enable Buttons if database selected.
         * */
        private void repositoryItemComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label_database.Caption = bei_database.EditValue.ToString();

            btn_runQuery.Enabled = true;
            btn_writeChanges.Enabled = true;
            btn_save.Enabled = true;
            btn_replace.Enabled = true;
            btn_history.Enabled = true;
        }

        private void btn_runQuery_ItemClick(object sender, ItemClickEventArgs e)
        {
            gridControl.Visible = true;
            chartControl.Visible = false;

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

                        FormatGridView();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /**
         * Initialize GridView format
         * */
        void FormatGridView()
        {
            gridView.Columns[0].OptionsColumn.AllowEdit = false;
            gridView.Columns[1].OptionsColumn.AllowEdit = false;
            gridView.Columns[2].OptionsColumn.AllowEdit = false;

            gridView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            gridView.OptionsSelection.MultiSelect = true;

            gridView.Columns[21].Visible = false;
        }

        private void btn_writeChanges_ItemClick(object sender, ItemClickEventArgs e)
        {
            bool isError = false;
            if (gridControl.DataSource == null)
            {
                Global.ShowMessageBox("Warning", "Run query first.");
                return;
            }
            int[] selectedRows = gridView.GetSelectedRows();
            if (selectedRows.Length < 1)
            {
                Global.ShowMessageBox("Warning", "No row is selected.");
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
            
            Global.ShowMessageBox("Message", "Successfully ran query.");
            mChangedCells = new List<CellInfo>();
            gridView.RefreshData();
        }

        /**
         * Get Query for Update XML_configuration
         * */
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
            //result += string.Format(@"SET @configuration_xml.modify(''replace value of(/*:DataFeed/*:Transporter/*:ArcherWebServiceTransportActivity/@Uri)[1]with (""{0}"")'')", data.TransportURL);
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
            //result += string.Format(@"SET @configuration_xml.modify(''replace value of(/*:DataFeed/*:Transporter/*:UNCDataFeedTransportActivity/@Uri)[1]with (""{0}"")'')", data.TransportPath);

            result += UpdateConnectionStringQuery(data.ConnectionString);
            result += UpdateTransportURLQuery(data.TransportURL);
            result += UpdateTransportPathQuery(data.TransportPath);



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

        /**
         * Query for Update ConnectionString field of Xml_Configuration
         * */
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

        /**
         * Query for Update TransportURL field of xml_configuration
         * */
        string UpdateTransportURLQuery(string transportURL)
        {
            string query = @"
DECLARE @transport_url nvarchar(max) = @configuration_xml.value(''(/*:DataFeed/*:Transporter/*:ArcherWebServiceTransportActivity/@Uri)[1]'', ''nvarchar(max)'')

if (@transport_url is null)
	begin
		DECLARE @is_transporter_1 int = @configuration_xml.exist(''/*:DataFeed/*:Transporter'')
		if (@is_transporter_1 > 0)
			begin
				DECLARE @is_activity int = @configuration_xml.exist(''/*:DataFeed/*:Transporter/*:ArcherWebServiceTransportActivity'')
				if (@is_activity > 0)
					begin
						SET @configuration_xml.modify(''
						insert attribute Uri {""" + transportURL + @"""}";
            query += string.Format(@"
into(/*:DataFeed/*:Transporter/*:ArcherWebServiceTransportActivity[@Uri])[1]
						'')
					end
				else
					SET @configuration_xml.modify(''
					insert <ArcherWebServiceTransportActivity Uri=""{0}""></ArcherWebServiceTransportActivity>

                    as first

                    into(/*:DataFeed/*:Transporter)[1]
					'')
			end
		else
			DECLARE @transporter_xml_1 XML = ''
			<Transporter>
				<ArcherWebServiceTransportActivity Uri=""{0}"">
				</ArcherWebServiceTransportActivity>
			</Transporter>''

			SET @configuration_xml.modify(''
			insert sql:variable(""@transporter_xml_1"")
			into (/*:DataFeed)[1]
			'')
	end
else
	SET @configuration_xml.modify(''replace value of(/*:DataFeed/*:Transporter/*:ArcherWebServiceTransportActivity/@Uri)[1]with (""{0}"")'')	
", transportURL);
            return query;
        }

        /**
         * Get query for update TransportPath in xml_configuration
         * */
        string UpdateTransportPathQuery(string transportPath)
        {
            string query = @"
DECLARE @transport_path nvarchar(max) = @configuration_xml.value(''(/*:DataFeed/*:Transporter/*:UNCDataFeedTransportActivity/@Uri)[1]'', ''nvarchar(max)'')

if (@transport_path is null)
	begin
		DECLARE @is_transporter_2 int = @configuration_xml.exist(''/*:DataFeed/*:Transporter'')
		if (@is_transporter_2 > 0)
			begin
				DECLARE @is_activity_1 int = @configuration_xml.exist(''/*:DataFeed/*:Transporter/*:UNCDataFeedTransportActivity'')
				if (@is_activity_1 > 0)
					begin
						SET @configuration_xml.modify(''
						insert attribute Uri {""" + transportPath + @"""}";
            query += string.Format(@"
into(/*:DataFeed/*:Transporter/*:UNCDataFeedTransportActivity[@Uri])[1]
						'')
					end
				else
					SET @configuration_xml.modify(''
					insert <UNCDataFeedTransportActivity Uri=""{0}""></UNCDataFeedTransportActivity>
as first
                    into(/*:DataFeed/*:Transporter)[1]
					'')

			end
		else
			DECLARE @transporter_xml_2 XML = ''
			<Transporter>
				<UNCDataFeedTransportActivity Uri=""{0}"">
				</UNCDataFeedTransportActivity>
			</Transporter>''

			SET @configuration_xml.modify(''
			insert sql:variable(""@transporter_xml_2"")
			into (/*:DataFeed)[1]
			'')
	end
else
	SET @configuration_xml.modify(''replace value of(/*:DataFeed/*:Transporter/*:UNCDataFeedTransportActivity/@Uri)[1]with (""{0}"")'')	
", transportPath);
            return query;
        }

        /**
         * Save Datafeed Query result in a file.
         * */
        private void btn_save_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridControl.DataSource == null)
            {
                Global.ShowMessageBox("Warning", "Run query first.");
                return;
            }

            int[] selectedRows = gridView.GetSelectedRows();
            if (selectedRows.Length < 1)
            {
                Global.ShowMessageBox("Warning", "No row is selected.");
                return;
            }

            List<DatafeedResult> datafeedResults = (List<DatafeedResult>)gridControl.DataSource;

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save datafeed result";
            saveFileDialog.DefaultExt = "json";
            saveFileDialog.Filter = "*.json|All files(*.*)";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string json = "[\n";

                foreach (int index in selectedRows)
                {
                    var item = datafeedResults[index];

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

        /**
         * Load Datafeed data from Save file.
         * */
        private void btn_load_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Open datafeed result";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string json = File.ReadAllText(openFileDialog.FileName);
                try
                {
                    List<DatafeedResult> datafeedResults = JsonConvert.DeserializeObject<List<DatafeedResult>>(json);
                    gridControl.DataSource = datafeedResults;
                    FormatGridView();
                    gridControl.Visible = true;
                    chartControl.Visible = false;
                    MessageBox.Show("Successfully Loaded.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private string SearchKeyword = "";
        private string ReplaceKeyword = "";
        private DatafeedCheckStatus CheckStatus = new DatafeedCheckStatus();
        private void btn_replace_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridControl.DataSource == null)
            {
                Global.ShowMessageBox("Warning", "Run query first.");
                return;
            }
            
            FindReplaceForm findReplaceForm = new FindReplaceForm();
            findReplaceForm.FindText = SearchKeyword;
            findReplaceForm.ReplaceText = ReplaceKeyword;
            findReplaceForm.CheckStatus = CheckStatus;
            findReplaceForm.ShowDialog();

            SearchKeyword = findReplaceForm.FindText;
            ReplaceKeyword = findReplaceForm.ReplaceText;
            CheckStatus = findReplaceForm.CheckStatus;
            if (findReplaceForm.Mode == 1)
            {
                FindResult();
            }
            else if (findReplaceForm.Mode == 2)
            {
                ReplaceResult();
                gridView.RefreshData();
            }
        }

        /**
         * Find datafeed results.
        */
        private void FindResult()
        {
            List<DatafeedResult> findResult = new List<DatafeedResult>();

            foreach (DatafeedResult item in mQueryResult)
            {
                item.CheckStatus = CheckStatus;
                if (item.Find(SearchKeyword))
                {
                    findResult.Add(item);
                }
            }
            gridControl.DataSource = findResult;
            if (findResult.Count < 1)
            {
                Global.ShowMessageBox("Message", "There're no match result.");
            }
        }

        /**
         *  Replace datafeed results.
         * */
         private void ReplaceResult()
        {
            List<DatafeedResult> replaceResult = new List<DatafeedResult>();
            int replaceCount = 0;
            foreach (DatafeedResult item in mQueryResult)
            {
                item.CheckStatus = CheckStatus;
                replaceCount += item.Replace(SearchKeyword, ReplaceKeyword);
            }
            if (replaceCount == 0)
            {
                Global.ShowMessageBox("Message", "There're no match result.");
            }
            else
            {
                Global.ShowMessageBox("Message", string.Format("Successfully replaced {0} values", replaceCount));
            }
        }

        private void btn_history_ItemClick(object sender, ItemClickEventArgs e)
        {
            chartControl.Visible = true;
            gridControl.Visible = false;

            chartControl.Series.Clear();
            mHistoryResult = new List<DatafeedHistoryResult>();

            SplashScreenManager.ShowForm(this, typeof(WaitForm), true, true, false);

            DateTime start_date = DateTime.Parse(bei_startDate.EditValue.ToString());
            DateTime end_date = DateTime.Parse(bei_endDate.EditValue.ToString());

            string strStartDate = start_date.Year + "/" + start_date.Month + "/" + start_date.Day + " " + bei_startTime.EditValue.ToString();
            string strEndDate = end_date.Year + "/" + end_date.Month + "/" + end_date.Day + " " + bei_endTime.EditValue.ToString();

            string query = string.Format("USE {0};", bei_database.EditValue.ToString());
            query += string.Format(@"
SELECT start_time,end_time, B.datafeed_name, A.target_records_created, A.target_records_updated, A.target_records_deleted, A.target_records_failed, A.target_records_set_value, A.subform_records_created, A.subform_records_updated, A.subform_records_failed, A.child_records_created, A.child_records_updated, A.child_records_failed, C.datafeed_status_name, B.configuration_xml
FROM tblDataFeedHistory A
LEFT JOIN tblDatafeed B ON A.datafeed_id = B.datafeed_id
LEFT JOIN tblDataFeedHistoryStatus C ON A.status_id = C.datafeed_status_id
where start_time >= '{0}'AND end_time < '{1}'", strStartDate, strEndDate);

            Series series = new Series("DatafeedHistory", ViewType.Gantt);
            series.ValueScaleType = ScaleType.DateTime;
            File.WriteAllText("query.txt", query);
            using (SqlCommand cmd = new SqlCommand(query, Global.SQL_CONNECTION))
            {
                using (IDataReader dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        DateTime startTime = DateTime.Parse(dr[0].ToString());
                        DateTime endTime = DateTime.Parse(dr[1].ToString());
                        if (dr[2].ToString() == "") continue;
                        
                        SeriesPoint seriesPoint = new SeriesPoint(dr[2].ToString(), startTime, endTime);
                        int status = int.Parse(dr[3].ToString());
                        if (status == 1)
                        {
                            seriesPoint.Color = Color.LightBlue;
                        }
                        else if (status == 2)
                        {
                            seriesPoint.Color = Color.Green;
                        }
                        else if (status == 3)
                        {
                            seriesPoint.Color = Color.Red;
                        }
                        else if (status == 4)
                        {
                            seriesPoint.Color = Color.Yellow;
                        }
                        else if (status == 5)
                        {
                            seriesPoint.Color = Color.LightGray;
                        }
                        else if (status == 6)
                        {
                            seriesPoint.Color = Color.Black;
                        }
                        else if (status == 7)
                        {
                            seriesPoint.Color = Color.Purple;
                        }
                        DatafeedHistoryResult datafeedHistoryResult = new DatafeedHistoryResult();
                        datafeedHistoryResult.StartTime = dr[0].ToString();
                        datafeedHistoryResult.EndTime = dr[1].ToString();
                        datafeedHistoryResult.DatafeedName = dr[2].ToString();
                        datafeedHistoryResult.TargetRecordsCreated = dr[3].ToString();
                        datafeedHistoryResult.TargetRecordsUpdated = dr[4].ToString();
                        datafeedHistoryResult.TargetRecordsDeleted = dr[5].ToString();
                        datafeedHistoryResult.TargetRecordsFailed = dr[6].ToString();
                        datafeedHistoryResult.TargetRecordsSetValue = dr[7].ToString();
                        datafeedHistoryResult.SubFormRecordsCreated = dr[8].ToString();
                        datafeedHistoryResult.SubFormRecordsUpdated = dr[9].ToString();
                        datafeedHistoryResult.SubFormRecordsFailed = dr[10].ToString();
                        datafeedHistoryResult.ChildRecordsCreated = dr[11].ToString();
                        datafeedHistoryResult.ChildRecordsUpdated = dr[12].ToString();
                        datafeedHistoryResult.ChildRecordsFailed = dr[13].ToString();
                        datafeedHistoryResult.StatusName = dr[14].ToString();
                        mHistoryResult.Add(datafeedHistoryResult);

                        series.Points.Add(seriesPoint);
                    }

                }
            }
            chartControl.Series.Add(series);
            SplashScreenManager.CloseForm(false);
        }

        private void chartControl_CustomDrawCrosshair(object sender, CustomDrawCrosshairEventArgs e)
        {
            foreach (var group in e.CrosshairElementGroups)
            {
                foreach (var element in group.CrosshairElements)
                {
                    //string datafeedName = element.LabelElement.Text.Split(' ')[0];
                    string datafeedName = element.LabelElement.Text.Split('(')[0].Substring(0, element.LabelElement.Text.Split('(')[0].Length - 1);
                    string runTime = element.AxisLabelElement.AxisValue.ToString();
                    DatafeedHistoryResult history = FindResultByDatafeedName(datafeedName, runTime);
                    //string hintText =  datafeedName + "\n";
                    if (history == null) continue;
                    string hintText = string.Format("{0} ({1})\n", datafeedName, history.StatusName);
                    hintText += string.Format("{0} ~ {1}\n", history.StartTime, history.EndTime);
                    hintText += string.Format("Target records created: {0}\n", history.TargetRecordsCreated);
                    hintText += string.Format("Target records updated: {0}\n", history.TargetRecordsUpdated);
                    hintText += string.Format("Target records deleted: {0}\n", history.TargetRecordsDeleted);
                    hintText += string.Format("Target records failed: {0}\n", history.TargetRecordsFailed);
                    hintText += string.Format("Target records set value: {0}\n", history.TargetRecordsSetValue);
                    hintText += string.Format("Subform records created: {0}\n", history.SubFormRecordsCreated);
                    hintText += string.Format("Subform records updated: {0}\n", history.SubFormRecordsUpdated);
                    hintText += string.Format("Subform records failed: {0}\n", history.SubFormRecordsFailed);
                    hintText += string.Format("Child records created: {0}\n", history.ChildRecordsCreated);
                    hintText += string.Format("Child records updated: {0}\n", history.ChildRecordsUpdated);
                    hintText += string.Format("Child records failed: {0}\n", history.ChildRecordsFailed);
                    element.LabelElement.Text = hintText;
                }
            }
        }

        private DatafeedHistoryResult FindResultByDatafeedName (string datafeedName, string runTime)
        {
            foreach (var history in mHistoryResult)
            {
                if (history.DatafeedName == datafeedName && (history.StartTime == runTime || history.EndTime == runTime)) return history;
            }
            return null;
        }

        public static XmlDocument ParseXml(String xml)
        {
            XDocument d = XDocument.Parse(xml);
            d.Root.Descendants().Attributes().Where(x => x.IsNamespaceDeclaration).Remove();

            foreach (var elem in d.Descendants())
                elem.Name = elem.Name.LocalName;

            var xmlDocument = new XmlDocument();
            xmlDocument.Load(d.CreateReader());

            return xmlDocument;
        }
    }
}