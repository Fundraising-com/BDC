namespace QSPFulfillment.CustomerService
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Business;
	using QSPFulfillment.DataAccess;

	/// <summary>
	///		Summary description for ControlerProblemCode.
	/// </summary>
	/// 
	

	public class ControlerProblemCode : CustomerServiceControlDataGrid
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox TextBox2;
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox TextBox3;
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		protected System.Web.UI.WebControls.LinkButton lbtnInsert;
		protected System.Web.UI.WebControls.LinkButton LinkButton3;
		protected System.Web.UI.WebControls.LinkButton LinkButton2;
		protected ProblemCodeTable Table = new ProblemCodeTable();
		protected QSPFulfillment.CustomerService.DataGridObject dtgMain;
		
		protected Label lblMessage;

		#region event page
		private void Page_Load(object sender, System.EventArgs e)
		{
			
			IsSelectOnly = GetIsSelectOnly();
		}
		
		private void ControlerProblemCode_PreRender(object sender, EventArgs e)
		{


			if(!IsPostBack)
				DataBind();

			if(IsSelectOnly && !IsPostBack)
			{
				this.dtgMain.Columns[2].Visible = false;
				this.dtgMain.ShowFooter = false;
				this.dtgMain.Columns[3].Visible = true;
						
			}
			else if(!IsSelectOnly)
			{
				this.dtgMain.ShowFooter = true;
			}
			
				
				
			
			

		}

		#endregion
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			
			InitializeComponent();
			DataSource = Table;
			base.OnInit(e,this.dtgMain,lblMessage);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);
			this.PreRender += new System.EventHandler(this.ControlerProblemCode_PreRender);

		}
		#endregion
		#region datagrid
		private void dtgMain_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			this.dtgMain.EditItemIndex = -1;
			this.dtgMain.DataBind();
		}
		#endregion
		#region override base
		protected override void Insert(DataGridCommandEventArgs e)
		{
			DataRow row = Table.NewRow();
			GetValueInsert(e,row);
			Table.Rows.Add(row);
			this.Page.BusProblemCode.Insert(Table);
			NewIDInserted = Convert.ToInt32(row[ProblemCodeTable.FLD_INSTANCE]);
		}
		protected override void Update(DataGridCommandEventArgs e)
		{
			this.Page.BusProblemCode.SelectOne(Table,GetProblemCodeID(e));
			
			if(Table.Rows.Count != 0)
			{
				GetValueUpdate(e,Table.Rows[0]);
				this.Page.BusProblemCode.Update(Table);
			}
			
			
		}
		protected override void LoadData()
		{
			DataSource.Clear();
			if(List == null )
			{
				this.Page.BusProblemCode.SelectAll(DataSource);
			}
			else
			{
				if(List.Count == 0)
				{
					this.Page.BusProblemCode.SelectAll(DataSource);
				}
				else
				{
					this.Page.BusProblemCode.SelectSearch(DataSource,List);
				}
			}
		}
		#endregion
		#region Get or set
		private int GetProblemCodeID(DataGridCommandEventArgs e)
		{
			return Convert.ToInt32(e.CommandArgument);
		}
		private void GetValueUpdate(DataGridCommandEventArgs e,DataRow Row)
		{
			Insert(Row,ProblemCodeTable.FLD_DESCRIPTION,GetDescription(e.Item));	
		}
		private void GetValueInsert(DataGridCommandEventArgs e,DataRow Row)
		{
			
			Insert(Row,ProblemCodeTable.FLD_DESCRIPTION,GetDescriptionInsert(e.Item));
			Row[ProblemCodeTable.FLD_INSTANCE] =0;
		
				
		}
		private string GetDescription(DataGridItem Item)
		{
			try
			{
				return ((TextBox)Item.FindControl("tbxDescriptionUpdate")).Text;
			}
			catch
			{
				return "";
			}
		}
		private string GetDescriptionInsert(DataGridItem Item)
		{
			try
			{
				return ((TextBox)Item.FindControl("tbxDescriptionInsert")).Text;
	
			}
			catch
			{
				return "";
			}
	}
		private int GetInstanceInsert(DataGridItem Item)
		{
			try
			{
			return Convert.ToInt32(((TextBox)Item.FindControl("tbxInstanceInsert")).Text);
			}
			catch
			{
				return -1;
			}
		}
		private bool GetIsSelectOnly()
		{
			if(Request.QueryString["ID"] != null)
				return Convert.ToBoolean(Request.QueryString["ID"]);
			else
				return true;
		}

		# endregion
		
	
		

		
	}
}
