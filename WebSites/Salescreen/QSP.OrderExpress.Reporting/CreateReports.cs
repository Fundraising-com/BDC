using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using QSP.OrderExpress.Reporting.ReportingService;

namespace QSP.OrderExpress.Reporting
{
    public class CreateReports
    {
        QSP.OrderExpress.Reporting.Properties.Settings settings = new QSP.OrderExpress.Reporting.Properties.Settings();

        /// <summary>
        /// Gets a ParameterValye array out of a parameter dictionary
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private ParameterValue[] GetParameterArrayFromDictionary(Dictionary<string, string> parameters)
        {
            ParameterValue[] result = new ParameterValue[parameters.Count];

            int i = 0;
            foreach (KeyValuePair<string, string> pair in parameters)
            {
                result[i] = new ParameterValue();

                result[i].Name = pair.Key;
                result[i].Value = pair.Value;

                i++;
            }

            return result;
        }

        /// <summary>
        /// Creates the report
        /// </summary>
        /// <param name="reportName">The report to be created</param>
        /// <param name="parameters">The parameters for the report</param>
        /// <returns>A byte array representing the pds report generated</returns>
        public Byte[] CreatePdfReport(string reportName, Dictionary<string, string> parameters, int reportTimeOut)
        {
            Byte[] result = null;

            RSClient oRS = new RSClient();

            // Set the timeout
            oRS.Timeout = reportTimeOut;

            //for some wack-job reason, RS rejects the first login
            //try catch it to eliminate sillyness
            try
            {
                oRS.LogonUser(settings.RSUID, settings.RSPwd, null);
            }
            catch
            {
                oRS.LogonUser(settings.RSUID, settings.RSPwd, null);
            }

            try
            {
                // Input
                string fullReportName = settings.RSReportFolder + reportName;
                string reportFormat = "PDF";
                ParameterValue[] inputParams = this.GetParameterArrayFromDictionary(parameters);

                // Output 
                string encoding;
                string mimetype;
                ParameterValue[] parametersUsed;
                Warning[] warnings;
                string[] streamids;

                // Get report
                result = oRS.Render(
                    fullReportName
                    , reportFormat
                    , null	/* string HistoryID */
                    , null	/* string DeviceInfo */
                    , inputParams
                    , null	/* DataSourceCredentials[] Credentials */
                    , null	/* string ShowHideToggle */
                    , out encoding
                    , out mimetype
                    , out parametersUsed
                    , out warnings
                    , out streamids);
            }
            catch (Exception ex)
            {
            }

            return result;
        }
    }
}
