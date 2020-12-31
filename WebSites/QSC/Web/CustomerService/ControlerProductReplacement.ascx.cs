namespace QSPFulfillment.CustomerService
{
	using System;
	using System.Collections;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Common;
	using QSPFulfillment.DataAccess.Common.ActionObject;

	/// <summary>
	///		Summary description for ControlerProductReplacement.
	/// </summary>
	public partial class ControlerProductReplacement : CustomerServiceControl
	{

		private Batch currentBatch;

		public event EventHandler NextStudentClicked;
		public event OrderHeaderEventHandler EditProductsClicked;
		public event EventHandler ConfirmedClicked;

		protected void ControlerProductReplacement_Init(object sender, EventArgs e)
		{
			try 
			{
				ControlerOrderHeaderForProductReplacement ctrlControlerOrderHeaderForProductReplacement;

				foreach(string ID in OrderHeaderControlIDCollection) 
				{
					ctrlControlerOrderHeaderForProductReplacement = (ControlerOrderHeaderForProductReplacement) LoadControl("ControlerOrderHeaderForProductReplacement.ascx");
					ctrlControlerOrderHeaderForProductReplacement.ID = ID;
					this.plhOrderHeaders.Controls.Add(ctrlControlerOrderHeaderForProductReplacement);
				
					ctrlControlerOrderHeaderForProductReplacement.EditProductsClicked += new OrderHeaderEventHandler(ctrlControlerOrderHeaderForProductReplacement_EditProductsClicked);
					ctrlControlerOrderHeaderForProductReplacement.RemoveOrderClicked += new OrderHeaderEventHandler(ctrlControlerOrderHeaderForProductReplacement_RemoveOrderClicked);
				}
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			try 
			{
				UpdateBatchInformations();
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
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
			this.Init += new System.EventHandler(this.ControlerProductReplacement_Init);

		}
		#endregion

		protected void btnNextStudent_Click(object sender, System.EventArgs e)
		{
			try 
			{
				if(NextStudentClicked != null) 
				{
					NextStudentClicked(this, EventArgs.Empty);
				}
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		private void ctrlControlerOrderHeaderForProductReplacement_EditProductsClicked(object sender, OrderHeader orderHeader)
		{
			try 
			{
				if(EditProductsClicked != null) 
				{
					EditProductsClicked(sender, orderHeader);
				}
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		private void ctrlControlerOrderHeaderForProductReplacement_RemoveOrderClicked(object sender, OrderHeader orderHeader)
		{
			try 
			{
				RemoveOrderHeader(orderHeader);
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		protected void btnConfirm_Click(object sender, System.EventArgs e)
		{
			try 
			{
				if(ConfirmedClicked != null) 
				{
					ConfirmedClicked(this, EventArgs.Empty);
				}
			}
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		private ArrayList OrderHeaderControlIDCollection 
		{
			get 
			{
				if(Session[this.ClientID + "OrderHeaderControlIDCollection"] == null)
					Session[this.ClientID + "OrderHeaderControlIDCollection"] = new ArrayList();

				return (ArrayList) Session[this.ClientID + "OrderHeaderControlIDCollection"];
			}
		}

		public Batch CurrentBatch
		{
			get 
			{
				return currentBatch;
			}
			set 
			{
				currentBatch = value;
			}
		}

		#region Fields

		private string Comment 
		{
			get 
			{
				return this.tbxComment.Text;
			}
			set 
			{
				this.tbxComment.Text = value;
			}
		}

		#endregion

		public override void DataBind()
		{
			ControlerOrderHeaderForProductReplacement ctrlControlerOrderHeaderForProductReplacement;

			this.OrderHeaderControlIDCollection.Clear();
			this.plhOrderHeaders.Controls.Clear();

			if(this.CurrentBatch != null) 
			{
				for(int i = 0; i < CurrentBatch.OrderHeaders.Count; i++)
				{
					ctrlControlerOrderHeaderForProductReplacement = (ControlerOrderHeaderForProductReplacement) LoadControl("ControlerOrderHeaderForProductReplacement.ascx");
					this.plhOrderHeaders.Controls.Add(ctrlControlerOrderHeaderForProductReplacement);
					ctrlControlerOrderHeaderForProductReplacement.ID = "ctrlControlerOrderHeaderForProductReplacement" + i.ToString();
					ctrlControlerOrderHeaderForProductReplacement.CurrentOrderHeader = CurrentBatch.OrderHeaders[i];
					
					ctrlControlerOrderHeaderForProductReplacement.DataBind();

					ctrlControlerOrderHeaderForProductReplacement.EditProductsClicked += new OrderHeaderEventHandler(ctrlControlerOrderHeaderForProductReplacement_EditProductsClicked);
					this.OrderHeaderControlIDCollection.Add(ctrlControlerOrderHeaderForProductReplacement.ID);
				}
			}
		}

		private void UpdateBatchInformations() 
		{
			ControlerOrderHeaderForProductReplacement ctrlControlerOrderHeaderForProductReplacement;
			int i = 0;

			if(this.CurrentBatch != null) 
			{
				foreach(System.Web.UI.Control control in this.plhOrderHeaders.Controls) 
				{
					ctrlControlerOrderHeaderForProductReplacement = control as ControlerOrderHeaderForProductReplacement;

					if(ctrlControlerOrderHeaderForProductReplacement != null) 
					{
						ctrlControlerOrderHeaderForProductReplacement.CurrentOrderHeader = CurrentBatch.OrderHeaders[i];
						ctrlControlerOrderHeaderForProductReplacement.UpdateOrderHeaderInformations();

						i++;
					}
				}

				CurrentBatch.Comment = Comment;
			}
		}

		private void RemoveOrderHeader(OrderHeader orderHeader) 
		{
			if(this.CurrentBatch.OrderHeaders.Contains(orderHeader))
			{
				this.CurrentBatch.OrderHeaders.Remove(orderHeader);

				if(this.CurrentBatch.OrderHeaders.Count > 0) 
				{
					DataBind();
				} 
				else 
				{
					if(NextStudentClicked != null) 
					{
						NextStudentClicked(this, EventArgs.Empty);
					}
				}
			}
		}
	}
}
