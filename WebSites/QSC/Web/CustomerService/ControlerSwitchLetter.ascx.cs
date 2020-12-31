namespace QSPFulfillment.CustomerService
{
	using System;
	using System.Text;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Business;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Common;
	using QSPFulfillment.CommonWeb;

	/// <summary>
	///		Summary description for ControlerSwitchLetter.
	/// </summary>
	public class ControlerSwitchLetter : CustomerServiceControlDataGrid
	{
		private const int REPORT_TIMEOUT = 600000; // This report can take time: 10 minutes

		protected DataGridObject dtgMain;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected QSPFulfillment.CommonWeb.RSGeneration rsGenerationSwitchLetter;
		private SwitchLetterBatchTable Table;

		private void Page_Load(object sender, System.EventArgs e)
		{
			
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e,this.dtgMain,lblMessage);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.dtgMain.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgMain_ItemCommand);
			this.dtgMain.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dtgMain_ItemDataBound);
			this.List = null;
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void dtgMain_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			StringBuilder sb = new StringBuilder();
			System.Web.UI.WebControls.LinkButton oLink;
			System.Web.UI.WebControls.LinkButton mLink;
			oLink = (System.Web.UI.WebControls.LinkButton)e.Item.FindControl("hylReprint");
			mLink = (System.Web.UI.WebControls.LinkButton)e.Item.FindControl("hylMark");

			try
			{
				if(e.CommandName== "ResetSWL")
				{
					this.Page.BusSwitchLetterBatch.CancelSwitchLetterBatch(Convert.ToInt32(e.CommandArgument),this.Page.UserID);
					this.DataBind();
				}
				else if(e.CommandName== "ReprintSWL")
				{

					if(oLink.Text == "Close")
					{
					   this.Page.BusSwitchLetterBatch.UpdateSwitchLetterStatus(Convert.ToInt32(e.CommandArgument),1,9999);
					   this.DataBind();
					}
					else
					{
						GenerateReport(Convert.ToInt32(e.CommandArgument));
					}

				}

				else if(e.CommandName== "Mark")
				{
					if(mLink.Text == "Printed")
					{
						this.Page.BusSwitchLetterBatch.UpdateSwitchLetterStatus(Convert.ToInt32(e.CommandArgument),9999,1);
						this.DataBind();
					}	 
				}
			}
			catch(ExceptionFulf ex)
			{
				this.Page.SetPageError(ex);
			}
		}

		protected override void LoadData()
		{
			DataSource = Table = new SwitchLetterBatchTable();
			
			this.Page.BusSwitchLetterBatch.SelectAll(DataSource);
		}

		private int GetInstance(DataGridItem Item)
		{
			return Convert.ToInt32(((Label)Item.FindControl("Instance")).Text);
		}

		private void GenerateReport(int switchLetterBatchID) 
		{
			ParameterValueCollection parameterValues = new ParameterValueCollection();
			ParameterValue parameterValue;
				
			parameterValue = new ParameterValue();
			parameterValue.Name = "iRemitBatchID";
			parameterValue.Value = "0";
			parameterValues.Add(parameterValue);

			parameterValue = new ParameterValue();
			parameterValue.Name = "dFrom";
			parameterValue.Value = "01/01/1955";
			parameterValues.Add(parameterValue);
				
			parameterValue = new ParameterValue();
			parameterValue.Name = "dTo";
			parameterValue.Value = "01/01/1955";
			parameterValues.Add(parameterValue);
				
			parameterValue = new ParameterValue();
			parameterValue.Name = "sTitleCode";
			parameterValue.Value = "0";
			parameterValues.Add(parameterValue);
				
			parameterValue = new ParameterValue();
			parameterValue.Name = "sReport";
			parameterValue.Value = "pr_SwitchLetterSelectReport";
			parameterValues.Add(parameterValue);
				
			parameterValue = new ParameterValue();
			parameterValue.Name = "iSwitchLetterBatchID";
			parameterValue.Value = switchLetterBatchID.ToString();
			parameterValues.Add(parameterValue);

			parameterValue = new ParameterValue();
			parameterValue.Name = "iCustomerOrderHeaderInstance";
			parameterValue.Value = "0";
			parameterValues.Add(parameterValue);
				
			parameterValue = new ParameterValue();
			parameterValue.Name = "iTransID";
			parameterValue.Value = "0";
			parameterValues.Add(parameterValue);
				
			try 
			{
				rsGenerationSwitchLetter.Generate(parameterValues, REPORT_TIMEOUT);
			} 
			catch(Exception ex)
			{
				QSPFulfillment.DataAccess.Common.ApplicationError.ManageError(ex);

				this.Page.MessageManager.Add(QSPFulfillment.DataAccess.Common.Message.ERRMSG_SYSTEM_VAR_0);
				this.Page.MessageManager.PrepareErrorMessage();
				throw new ExceptionFulf(this.Page.MessageManager);
			}
		}

		private void dtgMain_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			System.Web.UI.WebControls.LinkButton oLink;
			oLink = (System.Web.UI.WebControls.LinkButton)e.Item.FindControl("hylReprint");

			System.Web.UI.WebControls.LinkButton mLink;
			mLink = (System.Web.UI.WebControls.LinkButton)e.Item.FindControl("hylMark");

			string IsLocked = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "IsLocked"));
			string IsPrinted = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "IsPrinted"));
			
			if (IsLocked == "Open")
			{
				oLink.Text = "Close";
				mLink.Text = "";
			}

			if (IsPrinted == "Yes")
			{
				mLink.Text = "";
			}
		
		}



	
	}
}
