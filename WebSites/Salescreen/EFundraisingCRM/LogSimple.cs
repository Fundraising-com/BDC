using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Configuration;

namespace efundraising.EFundraisingCRM
{
    public class LogSimple
    {

        public static void LogInfo(string info)
        {
            try
            {
                string path = efundraising.Configuration.ApplicationSettings.GetConfig()["Common.Log.Info", "LogSource"].ToString();

                path = path.Replace("%yyyy%", DateTime.Now.Year.ToString());
                path = path.Replace("%MM%", DateTime.Now.Month.ToString());
                path = path.Replace("%dd%", DateTime.Now.Day.ToString());
                StreamWriter sw = new StreamWriter(path, true);
                sw.Write("<Info>");
                sw.Write(info);
                sw.Write("</Info>");
                sw.Flush();
                sw.Close();
            }
            catch (Exception x)
            {
                string error = x.Message;
            }
            
        }
        
        //newer --must send the xml tags
        public static void Log(string info, int syncId)
        {
            try
            {
                string path = ConfigurationManager.AppSettings["Log.Info"].ToString();

                path = path.Replace("%yyyy%", DateTime.Now.Year.ToString());
                path = path.Replace("%MM%", DateTime.Now.Month.ToString());
                path = path.Replace("%dd%", DateTime.Now.Day.ToString());
                path = path.Replace("%syncId%", syncId.ToString()); 
                StreamWriter sw = new StreamWriter(path, true);
                sw.Write(info);
                sw.Flush();
                sw.Close();
            }
            catch (Exception x)
            {
                string error = x.Message;
            }

        }

    }
}
