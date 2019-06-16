using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New_DFT.Models
{
    class DatafeedResult
    {
        //public bool Selected { get; set; }
        public string DatafeedID { get; set; }
        public string Guid { get; set; }
        public string DatafeedName { get; set; }
        public string SecurityUsername { get; set; }
        public string SecurityPassword { get; set; }
        public string TransportUsername { get; set; }
        public string TransportPassword { get; set; }
        public string TransportURL { get; set; }
        public string TransportInstance { get; set; }
        public string SecurityDomain { get; set; }
        public string TransportDomain { get; set; }
        public string ProxyOption { get; set; }
        public string ProxyName { get; set; }
        public string ProxyPort { get; set; }
        public string ProxyDomain { get; set; }
        public string ProxyUserName { get; set; }
        public string ProxyPassword { get; set; }
        public string TransportPath { get; set; }
        public string ConnectionString { get; set; }
        public string SQLQueryUsername { get; set; }
        public string SQLQueryPassword { get; set; }
        public DatafeedCheckStatus CheckStatus { get; set; }

        /**
         * Check if current DatafeedResult contains "Keyword"
         * @params: keyword: Search keyword
         * @return: True: if it contains, False: if it doesn't contain
         * */
        public bool Find(string keyword)
        {
            if ((DatafeedID.Contains(keyword) && CheckStatus.Check_DatafeedID) ||
                (Guid.Contains(keyword) && CheckStatus.Check_Guid) ||
                (DatafeedName.Contains(keyword) && CheckStatus.Check_DatafeedName) ||
                (SecurityUsername.Contains(keyword) && CheckStatus.Check_SecurityUsername) ||
                (SecurityPassword.Contains(keyword) && CheckStatus.Check_SecurityPassword) ||
                (TransportUsername.Contains(keyword) && CheckStatus.Check_TransportUsername) ||
                (TransportPassword.Contains(keyword) && CheckStatus.Check_TransportPassword) ||
                (TransportURL.Contains(keyword) && CheckStatus.Check_TransportURL) ||
                (TransportInstance.Contains(keyword) && CheckStatus.Check_TransportInstance) ||
                (SecurityDomain.Contains(keyword) && CheckStatus.Check_SecurityDomain) ||
                (TransportDomain.Contains(keyword) && CheckStatus.Check_TransportDomain) ||
                (ProxyOption.Contains(keyword) && CheckStatus.Check_ProxyOption) ||
                (ProxyName.Contains(keyword) && CheckStatus.Check_ProxyName) ||
                (ProxyPort.Contains(keyword) && CheckStatus.Check_ProxyPort) ||
                (ProxyDomain.Contains(keyword) && CheckStatus.Check_ProxyDomain) ||
                (ProxyUserName.Contains(keyword) && CheckStatus.Check_ProxyUserName) ||
                (ProxyPassword.Contains(keyword) && CheckStatus.Check_ProxyPassword) ||
                (TransportPath.Contains(keyword) && CheckStatus.Check_TransportPath) ||
                (ConnectionString.Contains(keyword) && CheckStatus.Check_ConnectionString) ||
                (SQLQueryUsername.Contains(keyword) && CheckStatus.Check_SQLQueryUsername) ||
                (SQLQueryPassword.Contains(keyword) && CheckStatus.Check_SQLQueryPassword)
                )
            {
                return true;
            }
            return false;
        }
        /**
         *  Replace DatafeedResult with keyword.
         *  @params: keyword: Search Keyword, replaceText: Text for Replace
         *  @return: Replace keyword count
         * */
        public int Replace(string keyword, string replaceText)
        {
            int replaceCount = 0;
            if (DatafeedID.Contains(keyword) && CheckStatus.Check_DatafeedID)
            {
                DatafeedID = DatafeedID.Replace(keyword, replaceText);
                replaceCount += 1;
            }
            if (Guid.Contains(keyword) && CheckStatus.Check_Guid)
            {
                Guid = Guid.Replace(keyword, replaceText);
                replaceCount += 1;
            }
            if (DatafeedName.Contains(keyword) && CheckStatus.Check_DatafeedName)
            {
                DatafeedName = DatafeedName.Replace(keyword, replaceText);
                replaceCount += 1;
            }
            if (SecurityUsername.Contains(keyword) && CheckStatus.Check_SecurityUsername)
            {
                SecurityUsername = SecurityUsername.Replace(keyword, replaceText);
                replaceCount += 1;
            }
            if (SecurityPassword.Contains(keyword) && CheckStatus.Check_SecurityPassword)
            {
                SecurityPassword = SecurityPassword.Replace(keyword, replaceText);
                replaceCount += 1;
            }
            if (TransportUsername.Contains(keyword) && CheckStatus.Check_TransportUsername)
            {
                TransportUsername = TransportUsername.Replace(keyword, replaceText);
                replaceCount += 1;
            }
            if (TransportPassword.Contains(keyword) && CheckStatus.Check_TransportPassword)
            {
                TransportPassword = TransportPassword.Replace(keyword, replaceText);
                replaceCount += 1;
            }
            if (TransportURL.Contains(keyword) && CheckStatus.Check_TransportURL)
            {
                TransportURL = TransportURL.Replace(keyword, replaceText);
                replaceCount += 1;
            }
            if (TransportInstance.Contains(keyword) && CheckStatus.Check_TransportInstance)
            {
                TransportInstance = TransportInstance.Replace(keyword, replaceText);
                replaceCount += 1;
            }
            if (SecurityDomain.Contains(keyword) && CheckStatus.Check_SecurityDomain)
            {
                SecurityDomain = SecurityDomain.Replace(keyword, replaceText);
                replaceCount += 1;
            }
            if (TransportDomain.Contains(keyword) && CheckStatus.Check_TransportDomain)
            {
                TransportDomain = TransportDomain.Replace(keyword, replaceText);
                replaceCount += 1;
            }
            if (ProxyOption.Contains(keyword) && CheckStatus.Check_ProxyOption)
            {
                ProxyOption = ProxyOption.Replace(keyword, replaceText);
                replaceCount += 1;
            }
            if (ProxyName.Contains(keyword) && CheckStatus.Check_ProxyName)
            {
                ProxyName = ProxyName.Replace(keyword, replaceText);
                replaceCount += 1;
            }
            if (ProxyPort.Contains(keyword) && CheckStatus.Check_ProxyPort)
            {
                ProxyPort = ProxyPort.Replace(keyword, replaceText);
                replaceCount += 1;
            }
            if (ProxyDomain.Contains(keyword) && CheckStatus.Check_ProxyDomain)
            {
                ProxyDomain = ProxyDomain.Replace(keyword, replaceText);
                replaceCount += 1;
            }
            if (ProxyUserName.Contains(keyword) && CheckStatus.Check_ProxyUserName)
            {
                ProxyUserName = ProxyUserName.Replace(keyword, replaceText);
                replaceCount += 1;
            }
            if (ProxyPassword.Contains(keyword) && CheckStatus.Check_ProxyPassword)
            {
                ProxyPassword = ProxyPassword.Replace(keyword, replaceText);
                replaceCount += 1;
            }
            if (TransportPath.Contains(keyword) && CheckStatus.Check_TransportPath)
            {
                TransportPath = TransportPath.Replace(keyword, replaceText);
                replaceCount += 1;
            }
            if (ConnectionString.Contains(keyword) && CheckStatus.Check_ConnectionString)
            {
                ConnectionString = ConnectionString.Replace(keyword, replaceText);
                replaceCount += 1;
            }
            if (SQLQueryUsername.Contains(keyword) && CheckStatus.Check_SQLQueryUsername)
            {
                SQLQueryUsername = SQLQueryUsername.Replace(keyword, replaceText);
                replaceCount += 1;
            }
            if (SQLQueryPassword.Contains(keyword) && CheckStatus.Check_SQLQueryPassword)
            {
                SQLQueryPassword = SQLQueryPassword.Replace(keyword, replaceText);
                replaceCount += 1;
            }

            return replaceCount;
        }
    }
}
