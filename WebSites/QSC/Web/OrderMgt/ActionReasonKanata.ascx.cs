namespace QSPFulfillment.OrderMgt
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Common;
	using QSPFulfillment.CustomerService;

	/// <summary>
	///		Summary description for ControlerActionReason.
	/// </summary>
	public class ActionReason : QSPFulfillment.CustomerService.CustomerServiceControl
	{
		protected System.Web.UI.WebControls.Panel Panel1;
		protected System.Web.UI.WebControls.Label lblCommunication;
		protected System.Web.UI.WebControls.DropDownList ddlCommunicationChanel;
		protected System.Web.UI.WebControls.Label lblProblemCode;
		protected System.Web.UI.WebControls.TextBox tbxProblemCode;
		protected System.Web.UI.WebControls.Label lblProblemDescription;
		protected System.Web.UI.WebControls.Label lblComment;
		protected System.Web.UI.WebControls.TextBox tbxComment;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox tbxReferToIncident;
		protected System.Web.UI.WebControls.HyperLink hypFindIncident;
		protected System.Web.UI.WebControls.Label lblCommunicationSource;
		protected System.Web.UI.WebControls.DropDownList ddlCommunicationSource;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Button btnSave;
		protected System.Web.UI.WebControls.HyperLink hypFindProblemCode;
		protected System.Web.UI.WebControls.DropDownList ddlStatus;
		protected System.Web.UI.WebControls.RangeValidator RangeValidator5;
		protected System.Web.UI.WebControls.RangeValidator RangeValidator1;
		protected System.Web.UI.WebControls.Button btnNew;
		private IncidentTable Table = new IncidentTable();


		private void Page_Load(object sender, System.EventArgs e)
		{
			Ajax.Utility.RegisterTypeForAjax(typeof(CustomerServicePage));

			if(ddlCommunicationChanel.Items.Count == 0)
			{
				DataBind();
			}
		}

		private void Page_PreRender(object sender, EventArgs e)
		{
			/*msif(this.Page.ResultSelected && !(this.Page.ResultType == SearchMultiPage.Shipment))
			{
				Enabled = true;
				AddJavaScriptHyp();
			}
			else
			{
				Enabled = false;
				RemoveJavaScript();
				
			}
			if(!IsPostBack)
				SetValue();
			
		*/	
		}
		public override void DataBind()
		{
			CommunicationChanel_Load();
			CommunicationSource_Load();
			Status_Load();
			base.DataBind ();
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			//msthis.Page.SelectResultClicked +=new SelectResultEventHandler(Page_SelectResultClicked);
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
			this.Load += new System.EventHandler(this.Page_Load);
			this.PreRender += new System.EventHandler(this.Page_PreRender);

		}
		#endregion

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			/*mstry
			{
				DataRow row; 
						
				if(this.Page.IncidentID == -1)
				{
					row= Table.NewRow();
				
					Table.Rows.Add(row);
					GetValueIncident(Table.Rows[0]);
					this.Page.BusIncident.Insert(Table);	
				}
				else
				{
					this.Page.BusIncident.SelectOne(Table,this.Page.IncidentID);
					GetValueIncident(Table.Rows[0]);
					this.Page.BusIncident.Update(Table);
				}
				this.Page.IncidentID =(int)Table.Rows[0][IncidentTable.FLD_INCIDENTINSTANCE];
				this.Page.ActionReasonEntered = true;
			}
			catch(QSPFulfillment.DataAccess.Common.ExceptionFulf ex)
			{
				this.Page.SetPageError(ex);
				Table.Clear();
			}
			*/
		}

		public bool Enabled
		{
			get
			{
				return this.ddlStatus.Enabled;
			}
			set
			{
				this.tbxComment.Enabled = value;
				this.tbxProblemCode.Enabled = value;
				this.tbxReferToIncident.Enabled = value;
				this.ddlCommunicationChanel.Enabled = value;
				this.ddlCommunicationSource.Enabled = value;
				this.ddlStatus.Enabled = value;
				this.btnSave.Enabled = value;
				this.hypFindIncident.Enabled = value;
				this.hypFindIncident.DataBind();
				this.hypFindProblemCode.Enabled =  value;
				this.hypFindProblemCode.DataBind();
				this.btnNew.Enabled = value;
				
			}
		}

		private void AddJavaScriptHyp()
		{
			
				this.hypFindProblemCode.Attributes.Add("onclick","javascript:Open('ProblemCode.aspx?IsNewWindow=true&ID=true');");
				this.hypFindIncident.Attributes.Add("onclick","javascript:var pageSwitchStateID = CustomerServicePage.SavePageSwitchStateFromClient(document.forms[0].elements[\"__VIEWSTATE\"].value).value; Open('findincident.aspx?IsNewWindow=true&ID=true&PageSwitchStateID=' + pageSwitchStateID);");
		}
		private void RemoveJavaScript()
		{
			this.hypFindProblemCode.Attributes.Clear();
			this.hypFindIncident.Attributes.Clear();
		}
		#region ddl load
		private void CommunicationChanel_Load()
		{
			
			if(ddlCommunicationChanel.Items.Count == 0)
			{
				DataTable Table = new DataTable();
				this.Page.BusCommunicationChannel.SelectAll(Table);
				//DataRow dtrow = Table.NewRow();
				//dtrow[IncidentStatusTable.FLD_DESCRIPTION]= "Select";
				//Table.Rows.InsertAt(dtrow,0);
				foreach(DataRow row in Table.Rows)
				{
					ddlCommunicationChanel.Items.Add(new ListItem(row[IncidentStatusTable.FLD_DESCRIPTION].ToString(),row[IncidentStatusTable.FLD_INSTANCE].ToString()));
				}
				//this.ddlCommunicationChanel.DataBind();
			}
		}

		private void CommunicationSource_Load()
		{
			if(ddlCommunicationSource.Items.Count == 0)
			{
				DataTable Table = new DataTable();
				//CommunicationSourceBusiness busComSource = new CommunicationSourceBusiness();
				this.Page.BusCommunicationSource.SelectAll(Table);
				//DataRow dtrow = Table.NewRow();
				//dtrow[IncidentStatusTable.FLD_DESCRIPTION]= "Select";
				//Table.Rows.InsertAt(dtrow,0);
				foreach(DataRow row in Table.Rows)
				{
					ddlCommunicationSource.Items.Add(new ListItem(row[IncidentStatusTable.FLD_DESCRIPTION].ToString(),row[IncidentStatusTable.FLD_INSTANCE].ToString()));
				}
				//this.ddlCommunicationSource.DataBind();
			}
		}

		private void Status_Load()
		{
			if(ddlStatus.Items.Count == 0)
			{
				DataTable Table = new DataTable();
				//IncidentStatusBusiness busIncidentStatus = new IncidentStatusBusiness();
				this.Page.BusIncidentStatus.SelectAll(Table);
				//DataRow dtrow = Table.NewRow();
				//dtrow[IncidentStatusTable.FLD_DESCRIPTION]= "Select";
				//Table.Rows.InsertAt(dtrow,0);
				
				
				foreach(DataRow row in Table.Rows)
				{
					ddlStatus.Items.Add(new ListItem(row[IncidentStatusTable.FLD_DESCRIPTION].ToString(),row[IncidentStatusTable.FLD_INSTANCE].ToString()));
				}
						
				//this.ddlStatus.DataBind();
			}
		}

		#endregion
		#region get or set
		/*msprivate void GetValueIncident(DataRow Row)
		{
			try 
			{
				GetCommunicationChannel(Row);
				GetCommunicationSource(Row);
				Row[IncidentTable.FLD_CUSTOMERORDERHEADERINSTANCE] = this.Page.OrderInfo.CustomerOrderHeaderInstance;
				GetProblemCode(Row);
				GetReferToIncident(Row);
				GetStatusInstance(Row);
				Row[IncidentTable.FLD_TRANSID] = this.Page.OrderInfo.TransID;
				Row[IncidentTable.FLD_USERIDCREATED]  = this.Page.UserID;
				Row[IncidentTable.FLD_COMMENTS] = GetComment();
				Row[IncidentTable.FLD_USERIDCREATED]= this.Page.UserID;
			} 
			catch (System.NullReferenceException ex) 
			{
				bool hasKey = false;

				foreach(string key in Session.Keys) 
				{
					if(key == "CurrentInfoSession") 
					{
						hasKey = true;
					}
				}

				if(hasKey) 
				{
					ex.Source += " Has the session key.";
				} 
				else 
				{
					ex.Source += " Does not have the session key.";
				}

				throw ex;
			}
		}*/

		private int GetCommunicationChannel()
		{
			try
			{
				return Convert.ToInt32(this.ddlCommunicationChanel.SelectedItem.Value);
			}
			catch
			{
				return 0;	
			}
		}
		private int GetCommunicationSource()
		{
			try
			{
				return Convert.ToInt32(this.ddlCommunicationSource.SelectedItem.Value);
			}
			catch
			{
				return 0;
			}
		}
		private Int64 GetProblemCode()
		{
			try
			{
				return Convert.ToInt64(this.tbxProblemCode.Text);
			}
			catch
			{
				return 0;
			}
		}
		private int GetReferToIncident()
		{
			try
			{
				if(this.tbxReferToIncident.Text == String.Empty)
					return 0;

				return Convert.ToInt32(this.tbxReferToIncident.Text);
			}
			catch
			{
				return 0;
			}
		}
		private int GetStatusInstance()
		{
			try
			{
				return Convert.ToInt32(this.ddlStatus.SelectedItem.Value);
			}
			catch
			{
				return 0;
			}
		}
		private string GetComment()
		{
            return this.tbxComment.Text;
		}
		private void SetValue()
		{
			
			if(Table.Rows.Count !=0)
			{
				DataRow row = Table.Rows[0];
				if(row[IncidentTable.FLD_INCIDENTINSTANCE]!= null)
				{
					
					this.ddlStatus.Items.FindByValue(row[IncidentTable.FLD_STATUSINSTANCE].ToString()).Selected = true;
					this.ddlCommunicationChanel.Items.FindByValue(row[IncidentTable.FLD_COMMUNICATIONCHANNELINSTANCE].ToString()).Selected = true;
					this.ddlCommunicationSource.Items.FindByValue(row[IncidentTable.FLD_COMMUNICATIONSOURCEINSTANCE].ToString()).Selected = true;
					this.tbxComment.Text = row[IncidentTable.FLD_COMMENTS].ToString();
					this.tbxProblemCode.Text = row[IncidentTable.FLD_PROBLEMCODEINSTANCE].ToString();
					this.tbxReferToIncident.Text = row[IncidentTable.FLD_REFERTOINCIDENTINSTANCE].ToString();
								
				}
			}
		}
		private void GetReferToIncident(DataRow Row)
		{
			int id = GetReferToIncident();
			if(id == 0)
				Row[IncidentTable.FLD_REFERTOINCIDENTINSTANCE] = DBNull.Value;
			else
				Row[IncidentTable.FLD_REFERTOINCIDENTINSTANCE] = id;
		}
		private void GetProblemCode(DataRow Row)
		{
			Int64 id = GetProblemCode();

			if(id == 0)
				Row[IncidentTable.FLD_PROBLEMCODEINSTANCE] = DBNull.Value;
			else
				Row[IncidentTable.FLD_PROBLEMCODEINSTANCE] = id;

		}
		private void GetCommunicationChannel(DataRow Row)
		{
			int id = GetCommunicationChannel();

			if(id == 0)
				Row[IncidentTable.FLD_COMMUNICATIONCHANNELINSTANCE] = DBNull.Value;
			else
				Row[IncidentTable.FLD_COMMUNICATIONCHANNELINSTANCE] = id;
		}
		private void GetCommunicationSource(DataRow Row)
		{
			int id = GetCommunicationSource();

			if(id == 0)
				Row[IncidentTable.FLD_COMMUNICATIONSOURCEINSTANCE] = DBNull.Value;
			else
				Row[IncidentTable.FLD_COMMUNICATIONSOURCEINSTANCE] = id;
		}
		private void GetStatusInstance(DataRow Row)
		{
			int id = GetStatusInstance();

			if(id == 0)
				Row[IncidentTable.FLD_STATUSINSTANCE] = DBNull.Value;
			else
				Row[IncidentTable.FLD_STATUSINSTANCE] = id;
		}
		#endregion
		private void SetDefaultValue()
		{
			this.ddlCommunicationChanel.SelectedIndex =-1;
			this.ddlCommunicationSource.SelectedIndex = -1;
			this.ddlStatus.SelectedIndex = -1;
			this.tbxReferToIncident.Text = "";
			this.tbxProblemCode.Text = "";
			this.tbxComment.Text = "";

		}

		/*msprivate void Page_SelectResultClicked(object sender, SelectResultClickedArgs e)
		{
			SetDefaultValue();
		}*/

		private void btnNew_Click(object sender, System.EventArgs e)
		{
			//msthis.Page.FireEventSelect(new SelectResultClickedArgs(this.Page.OrderInfo,false));
		}
	}
}
