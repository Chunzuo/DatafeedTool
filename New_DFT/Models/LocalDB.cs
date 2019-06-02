using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace New_DFT.Models
{
    class LocalDB
    {
        public string ServerName { get; set; }
        public LocalDB()
        {

        }
        public void Save()
        {
            File.WriteAllText("settings.json", JsonConvert.SerializeObject(this));
        }

        public static LocalDB Load()
        {
            try
            {
                LocalDB localDB = JsonConvert.DeserializeObject<LocalDB>(File.ReadAllText("settings.json"));
                return localDB;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
