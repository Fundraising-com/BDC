using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Business.ReportExecution;
using QSPFulfillment.DataAccess.Business;
using QSPFulfillment.CommonWeb;
using System.IO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Business.Objects;

namespace QSPFulfillment.CustomerService
{
	/// <summary>
	/// Summary description for GenerateOnlineStatements.
	/// </summary>
	public class GenerateCAStatements : CustomerService.CustomerServicePage
	{
		protected System.Web.UI.WebControls.Button btnGenerate;
        protected System.Web.UI.WebControls.TextBox StatementPrintRequestBatchIDTextBox;
		private DataTable Table = new DataTable("CampaignTable");


        const string realtime = "false";

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
		}
		#endregion

		private void btnGenerate_Click(object sender, System.EventArgs e)
		{
			GenerateCampaignStatements();
		}

        private int StatementPrintRequestBatch
        {
            get
            {
                return Convert.ToInt32(StatementPrintRequestBatchIDTextBox.Text);
            }
        }
        
		private void GenerateCampaignStatements() 
		{
			byte[] results;
			FileStream fs;

			LoadData();

			foreach(DataRow row in Table.Rows) 
			{
                string FullFilename = "C:\\Statements\\" + row["Filename"].ToString();
                if (!File.Exists(FullFilename))
                {
                    results = CallReportServicesDirect(row["CampaignID"].ToString(), null, realtime);

                    if (!Directory.Exists("C:\\Statements\\"))
                    {
                        Directory.CreateDirectory("C:\\Statements\\");
                    }

                    fs = new FileStream("C:\\Statements\\" + row["Filename"].ToString(), FileMode.Create);
                    fs.Write(results, 0, results.Length);
                    fs.Close();
                }
			}
		}

		private void LoadData() 
		{
            this.BusSearch.SelectCampaignsForCAStatement(Table, StatementPrintRequestBatch);

            RemoveCampaigns(Table);
            //RemoveFM(Table);
		}

		private byte[] CallReportServicesDirect(string CampaignID, string DateTo, string Realtime)
		{
			RSClient oRS = new RSClient();

            Business.ReportExecution.ParameterValue[] inputParams = new Business.ReportExecution.ParameterValue[3];
            inputParams[0] = new Business.ReportExecution.ParameterValue();
			inputParams[0].Name = "CampaignID";
			inputParams[0].Value = CampaignID;
            inputParams[1] = new Business.ReportExecution.ParameterValue();
			inputParams[1].Name = "DateTo";
            inputParams[1].Value = DateTo;
            inputParams[2] = new Business.ReportExecution.ParameterValue();
			inputParams[2].Name = "Realtime";
            inputParams[2].Value = Realtime;
			
			string reportName = "StatementReportByCampaign";

            return oRS.GenerateReportStream(reportName, "PDF", inputParams, 99999);
        }

        private void RemoveCampaigns(DataTable table)
        {
            List<int> list = new List<int>();

            for (int i = table.Rows.Count-1; i >= 0; i--)
            {
                if (list.Contains(Convert.ToInt32(table.Rows[i]["CampaignID"].ToString())))
                    table.Rows.RemoveAt(i);
            }
        }

        private void RemoveFM(DataTable table)
        {
            List<string> list = new List<string>();

            for (int i = table.Rows.Count - 1; i >= 0; i--)
            {
                if (list.Contains((table.Rows[i]["FMID"].ToString())))
                    table.Rows.RemoveAt(i);
            }
        } 
	}
}
