namespace QSPFulfillment.CustomerService
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Common.ActionObject;

	public delegate void OrderHeaderEventHandler(object sender, OrderHeader orderHeader);
	/// <summary>
	///		Summary description for ControlerOrderHeaderForProductReplacement.
	/// </summary>
	public partial class ControlerOrderHeaderForProductReplacement : CustomerServiceControl
	{
		protected QSPFulfillment.CustomerService.ControlerProductSelect ctrlControlerProductSelect;

		private OrderHeader currentOrderHeader;

		public event OrderHeaderEventHandler EditProductsClicked;
		public event OrderHeaderEventHandler RemoveOrderClicked;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}

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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		protected void lnkEditProducts_Click(object sender, System.EventArgs e)
		{
			try 
			{
				if(EditProductsClicked != null) 
				{
					EditProductsClicked(this, this.CurrentOrderHeader);
				}
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		protected void lnkRemoveOrder_Click(object sender, System.EventArgs e)
		{
			try 
			{
				if(RemoveOrderClicked != null) 
				{
					RemoveOrderClicked(this, this.CurrentOrderHeader);
				}
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		public OrderHeader CurrentOrderHeader 
		{
			get 
			{
				return currentOrderHeader;
			}
			set 
			{
				currentOrderHeader = value;
			}
		}

		#region Fields

		private string TeacherFirstName 
		{
			get 
			{
				return this.tbxTeacherFirstName.Text;
			}
			set 
			{
				this.tbxTeacherFirstName.Text = value;
			}
		}

		private string TeacherLastName 
		{
			get 
			{
				return this.tbxTeacherLastName.Text;
			}
			set 
			{
				this.tbxTeacherLastName.Text = value;
			}
		}

		private string StudentFirstName 
		{
			get 
			{
				return this.tbxStudentFirstName.Text;
			}
			set 
			{
				this.tbxStudentFirstName.Text = value;
			}
		}

		private string StudentLastName 
		{
			get 
			{
				return this.tbxStudentLastName.Text;
			}
			set 
			{
				this.tbxStudentLastName.Text = value;
			}
		}

		#endregion

		public override void DataBind()
		{
			if(CurrentOrderHeader != null) 
			{
				SetValue();
				
				this.ctrlControlerProductSelect.DataBind();
			}
		}

		private void SetValue() 
		{
			this.ctrlControlerProductSelect.Items = CurrentOrderHeader.ProductItems;
			TeacherFirstName = CurrentOrderHeader.TeacherFirstName;
			TeacherLastName = CurrentOrderHeader.TeacherLastName;
			StudentFirstName = CurrentOrderHeader.StudentFirstName;
			StudentLastName = CurrentOrderHeader.StudentLastName;
		}

		private void SetValueEmpty() 
		{
			TeacherFirstName = String.Empty;
			TeacherLastName = String.Empty;
			StudentFirstName = String.Empty;
			StudentLastName = String.Empty;
		}

		public void UpdateOrderHeaderInformations() 
		{
			CurrentOrderHeader.ProductItems = this.ctrlControlerProductSelect.Items;
			CurrentOrderHeader.TeacherFirstName = TeacherFirstName;
			CurrentOrderHeader.TeacherLastName = TeacherLastName;
			CurrentOrderHeader.StudentFirstName = StudentFirstName;
			CurrentOrderHeader.StudentLastName = StudentLastName;
		}
	}
}
