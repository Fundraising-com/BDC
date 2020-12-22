using System;
using System.Web;
using QSPForm.Common.DataDef;
using System.Data;
using System.Web.UI.WebControls;
using System.Security.Principal;
using System.Web.Security;
using System.Web.SessionState;

namespace QSP.OrderExpress.Web.Code {
    /// <summary>Base page for Web Form pages in QSPForm_Web</summary>
    /// <remarks>
    ///		Inherit from BasePage
    ///		We used this class to manage common functionnality
    ///		for DataGrid by example
    ///	</remarks>
    public class BaseWebForm : BasePage {
        private System.Web.UI.HtmlControls.HtmlInputHidden hidChange;
        protected bool isFormChange = false;
        
        public int DataOperation {
            get {
                if (ViewState["DataOperation"] != null) {
                    return Convert.ToInt32(ViewState["DataOperation"]);
                }
                else {
                    return 0;
                }
            }
            set {
                ViewState["DataOperation"] = value;
            }
        }

        public bool IsFormChange {
            get {
                return isFormChange;
            }
            set {
                isFormChange = value;
                //if ((isFormChange) && (hidChange != null))
                //    hidChange.Value = "1";
                //else
                //    hidChange.Value = "0";
            }
        }

        public System.Web.UI.HtmlControls.HtmlInputHidden HiddenChange {
            get {
                return hidChange;
            }
            set {
                hidChange = value;
            }
        }

        #region oldstuff
        /*
		//Virtual prperty and method to override in child class
		protected virtual void BindForm()
		{
			try
			{
				LoadData();
				//Prepare the DataSource of DropDownList when 
				//databind the grid 
							
			}
			catch(Exception ex)
			{
				SetPageError(ex);
			}
		}
		protected new virtual void LoadData()
		{
		}

		protected virtual void SetFormParameter()
		{
		}


		protected virtual bool Update()
		{
			return true;
		}

		protected virtual bool Delete()
		{
			return true;
		}

		protected override void OnLoad(EventArgs e) 
		{			
			if (hidChange != null)
				IsFormChange = Convert.ToBoolean(Convert.ToInt32(hidChange.Value));
			
			
			base.OnLoad(e);
		}
		
		protected override void OnPreRender(EventArgs e)
		{			
			base.OnPreRender(e);
		}
		
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();			
			base.OnInit(e);			
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			
		}
		#endregion


		
		protected override void OnMenuChange(System.ComponentModel.CancelEventArgs e)
		{				
			
			try
			{	
				if (!e.Cancel)
				{
					bool blnValid = false;		
				
					if (IsFormChange)
					{	
						Page.Validate();
						if (Page.IsValid) 
							blnValid = Update();
						e.Cancel = !blnValid;
					}
				}
			}
			catch(Exception ex)
			{
				SetPageError(ex);
				e.Cancel = true;
			}
			base.OnMenuChange(e);
		}

		protected int GetDropDownListSelectedIndex(string ValueToFound, DataView DV, string ColumnNameToSearch)
		{
			int iIndex =0;
			int iCount =0;
			foreach(DataRowView drwv in DV)
			{
				if(drwv[ColumnNameToSearch].ToString() == ValueToFound)
				{
					iIndex = iCount;
					break;
				}
				iCount++;
			}
			//because of the null value
			return iIndex;
		}
        */
        #endregion

        //Virtual prperty and method to override in child class
        public virtual void BindForm() {
            try {
                LoadData();
                //Prepare the DataSource of DropDownList when 
                //databind the grid 

            }
            catch (Exception ex) {
                SetPageError(ex);
            }
        }

        public new virtual void LoadData() {
        }

        public virtual void SetFormParameter() {
        }

        public virtual bool Update() {
            return true;
        }

        public virtual bool Delete() {
            return true;
        }

        protected override void OnLoad(EventArgs e) {
            if (hidChange != null)
                IsFormChange = Convert.ToBoolean(Convert.ToInt32(hidChange.Value));


            base.OnLoad(e);
        }

        protected override void OnPreRender(EventArgs e) {
            base.OnPreRender(e);
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {

        }
        #endregion
        
        protected override void OnMenuChange(System.ComponentModel.CancelEventArgs e) {
            try {
                if (!e.Cancel) {
                    bool blnValid = false;

                    if (IsFormChange) {
                        Page.Validate();
                        if (Page.IsValid)
                            blnValid = Update();
                        e.Cancel = !blnValid;
                    }
                }
            }
            catch (Exception ex) {
                SetPageError(ex);
                e.Cancel = true;
            }
            base.OnMenuChange(e);
        }

        public int GetDropDownListSelectedIndex(string ValueToFound, DataView DV, string ColumnNameToSearch) {
            int iIndex = 0;
            int iCount = 0;
            foreach (DataRowView drwv in DV) {
                if (drwv[ColumnNameToSearch].ToString() == ValueToFound) {
                    iIndex = iCount;
                    break;
                }
                iCount++;
            }
            //because of the null value
            return iIndex;
        }
    }
}