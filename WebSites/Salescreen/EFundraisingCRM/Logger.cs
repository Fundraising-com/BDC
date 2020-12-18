using System;
using System.Collections.Generic;
using System.Text;

namespace efundraising.EFundraisingCRM
{
    class Logger
    {
        public void LogInfo(string info)
        {
            string path = EFundraisingCRMWeb.Components.Server.ManageSaleScreen.GetValueFromWebConfig("Common.Log.Info", "LogSource");
            path = path.Replace("%yyyy%", DateTime.Now.Year.ToString());
            path = path.Replace("%MM%", DateTime.Now.Month.ToString());
            path = path.Replace("%dd%", DateTime.Now.Day.ToString());
            StreamWriter sw = new StreamWriter(path, true);
            sw.Write("<Info>");
            sw.Write(info);
            sw.Write("</Info>");
            sw.Close();
        }
           
    }
}
