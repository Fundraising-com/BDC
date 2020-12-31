using System;
using QSPFulfillment.DataAccess.Business;
using QSPFulfillment.DataAccess.Common.TableDef;
using System.Data;
using QSPFulfillment.DataAccess.Data;
using QSPFulfillment.DataAccess.Common;
using System.ComponentModel;

namespace QSPFulfillment.CustomerService.action
{
	
	/// <summary>
	/// Summary description for ControlerAction.
	/// </summary>
	/// 
	
	public class CustomerServiceActionControl: CustomerServiceControl
	{
		protected bool IsSuccess = false;
		private IncidentActionTable Table;
		private bool bCloseAfterAction = true;
		protected QSPFulfillment.DataAccess.Data.ConnectionProvider connProvider;

		
		private void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
				SetValueElement();
			
		
		}
		#region Web Form Designer generated code
		protected override void OnInit(EventArgs e)
		{		
			InitializeComponent();
			this.Page.ConfirmClicked += new EventHandler(Page_ConfirmClicked);
		
			base.OnInit(e);							
		}
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Page.Load += new EventHandler(Page_Load);
			
			
		}
		#endregion
		
		public  new CustomerServiceActionPage Page
		{
			get
			{				
				return (CustomerServiceActionPage)base.Page;
			}
			set
			{
			
				base.Page = value;
			}
		}

		protected virtual bool RaisesDoAction 
		{
			get 
			{
				return true;
			}
		}

		protected void AddScriptRelaodClose()
		{
			
			if(!this.Page.IsStartupScriptRegistered("ConfirmCloseReload"))
			{
				this.Page.RegisterStartupScript("ConfirmCloseReload","<script language=\"javascript\"> window.opener.pleasewait(); window.opener.RefreshAction("+(int)this.Page.GetAction()+"); self.close(); </script>");//location.reload(1) 
			}
			
			
		}
		
		protected virtual void SetValueElement()
		{
			
			this.Page.Message = "";
		
			
		}
		protected bool InsertIncidentAction()
		{
			
			Table = new IncidentActionTable();
			SetValueIncidentAction();
			this.Page.BusIncidentAction.Insert(Table);

			return true;
			
		}


		private void SetValueIncidentAction()
		{	
			DataRow row = Table.NewRow();
			row[IncidentActionTable.FLD_ACTIONINSTANCE] = Convert.ToInt32(this.Page.GetAction()); 
			row[IncidentActionTable.FLD_INCIDENTINSTANCE] = this.Page.IncidentID;
			row[IncidentActionTable.FLD_USERIDCREATED] = this.Page.UserID;
			row[IncidentActionTable.FLD_COMMENTS] = this.Page.Comment;
			
			Table.Rows.Add(row);
		}
		protected virtual void DoAction()
		{
			
		}
		private void Page_ConfirmClicked(object sender, System.EventArgs e)
		{
			if(RaisesDoAction)
			{
				try
				{					
					DoAction();

					InsertIncidentAction();
				}
				catch(ExceptionFulf ex)
				{
					this.Page.SetPageError(ex);
					bCloseAfterAction = false;
				}
						
				if(bCloseAfterAction)
				{
					AddScriptRelaodClose();
				}
			}
		}
		protected bool CloseAfterAction
		{
			get{return bCloseAfterAction;}
			set{ bCloseAfterAction= value;}
		}
		protected virtual int CustomerRemitHistoryInstance
		{
			get
			{
				return 0;
			}
		}
	}
}
