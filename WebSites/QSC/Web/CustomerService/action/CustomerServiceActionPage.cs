using System;
using System.Web.UI.WebControls;
using System.ComponentModel;
using QSPFulfillment.DataAccess.Business;
using QSPFulfillment.DataAccess.Common;
using QSPFulfillment.DataAccess.Common.TableDef;
using System.Data;

namespace QSPFulfillment.CustomerService.action
{
	/// <summary>
	/// Summary description for CustomerServiceActionPage.
	/// </summary>
	public class CustomerServiceActionPage:CustomerServicePage
	{
		private Label lblMessage;
		private Label lblHeader;
		private Button btnConfirmButton;
		private Button btnCancelButton;
		private TextBox tbxComment;
		public event EventHandler ConfirmClicked;
		public event EventHandler CancelClicked;
		private Label lblHeaderTitle;
		private ActionTable Table;
		private Label lblComments;
		protected CustomerServiceActionControl ctrlAction;
		private QSPFulfillment.DataAccess.Business.Action mAction;
		private Label lblAction;
		private bool IsValidAction;

		public CustomerServiceActionPage()
		{
			
		}
		private void Page_Load(object sender, EventArgs e)
		{
			
			
			if(mAction != QSPFulfillment.DataAccess.Business.Action.None && IsValidAction)
			{
				if(!IsPostBack)
				{
					
					SetValueMessage();
				}
			}
			else
			{
				this.tbxComment.Visible = false;
				this.btnConfirmButton.Enabled = false;
				this.lblComments.Visible = false;
				lblHeaderTitle.Visible = false;		
				
			}
		}
        protected void OnPreLoad(object sender, EventArgs e)
        {
            LoadControl();
        }
		#region Web Form Designer generated code
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			//LoadControl();
			this.Load += new System.EventHandler(this.Page_Load);
            this.PreLoad += new System.EventHandler(this.OnPreLoad);
		}
		#endregion	
		protected  void OnInit(EventArgs e,Button Confirm,Button Cancel,Label Message,TextBox Comment,Label Header,Label LabelComments,Label HeaderTitle,Label ControlActionAdd)
		{
			base.OnInit (e);

			this.lblMessage = Message;
			this.btnConfirmButton = Confirm;
			this.btnCancelButton = Cancel;
			this.tbxComment = Comment;
			this.lblHeader = Header;
			
			this.lblComments = LabelComments;
			this.lblHeaderTitle = HeaderTitle;
			this.lblAction = ControlActionAdd;
			InitializeComponent();
		}
		
		public string Message
		{
			get
			{
				
					return this.lblMessage.Text;
				
			}
			set
			{
				this.lblMessage.Text =value;
				

			}
		}
		public string Header
		{
			get
			{
				
				return this.lblHeader.Text;
				
			}
			set
			{
			
				this.lblHeader.Text =value;
				

			}
		}
		
		public Button CancelButton
		{
			get{return this.btnCancelButton;}
		}
		
		public Button ConfirmButton
		{
			get{return this.btnConfirmButton;}
		}
		public TextBox CommentTextBox
		{
			get{return this.tbxComment;}
		}
		public Label CommentsLabel
		{
			get{return this.lblComments;}
		}

		protected void FireEventConfirm(object sender)
		{
			if(ConfirmClicked != null)
			{
				ConfirmClicked(sender,new EventArgs());
			}
		}
		protected void FireEventCancel(object sender)
		{
			if(CancelClicked != null)
			{
				CancelClicked(sender,new EventArgs());
			}
		}

		public QSPFulfillment.DataAccess.Business.Action GetAction()
		{
			if(Context.Request.QueryString["Action"] != null)
			{
				try
				{
					return (QSPFulfillment.DataAccess.Business.Action)Convert.ToInt32(Context.Request.QueryString["Action"]);
				}
				catch
				{
					return QSPFulfillment.DataAccess.Business.Action.None;
				}
			}
			else
			{
				return QSPFulfillment.DataAccess.Business.Action.None;
			}
		}
		
		public string Comment
		{
			get
			{
				if(this.tbxComment.Text.Length <= 0)
				{
					this.tbxComment.Text = GetDefaultComment();
				}				
				return this.tbxComment.Text;
				
			}
			set
			{
				
					this.tbxComment.Text = value;
				
			}
		}
		private void SetValueMessage()
		{
			LoadAction();
			if(Table.Rows.Count !=0)
			{
				this.Message = Table.Rows[0][ActionTable.FLD_MESSAGE].ToString();
			}
		}
		private void LoadAction()
		{
			try
			{
				Table = new ActionTable();
				
				this.BusAction.SelectOne(Table,GetAction());
			
			}
			catch(ExceptionFulf ex)
			{
				this.SetPageError(ex);
			}
		}

		private void LoadControl()
		{
			try
			{
			mAction = GetAction();
			 IsValidAction =this.BusAction.IsValidAction((int)mAction,this.OrderInfo.CustomerOrderHeaderInstance,this.OrderInfo.TransID);

			if(mAction != QSPFulfillment.DataAccess.Business.Action.None && IsValidAction)
			{
				
					ctrlAction = (CustomerServiceActionControl)LoadControl(mAction.ToString()+".ascx");
					this.lblAction.Controls.Add(ctrlAction);
					
				}
				
			
			}
			catch(Exception ex)
			{
				this.lblMessage.Text = "This is not a valid action.";
				this.tbxComment.Visible = false;
				this.btnConfirmButton.Enabled = false;
				this.lblComments.Visible = false;

				throw ex;
			}
		}
		
		private string GetDefaultComment()
		{
			return QSPFulfillment.CommonWeb.QSPPage.aUserProfile.FullName + ", " + DateTime.Now.ToShortDateString();
		}

		
	}
}
