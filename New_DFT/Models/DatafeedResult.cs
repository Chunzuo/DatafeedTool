using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New_DFT.Models
{
    class DatafeedResult
    {
        public bool Selected { get; set; }
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
    }
}
