namespace CRMWeb.UserControls.Lead
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using GA.BDC.Core.EnterpriseComponents;
	using System.Diagnostics;

	/// <summary>
	///		Summary description for PromotionGroups.
	/// </summary>
	public partial class PromotionGroups : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			try{
				// Put user code to initialize the page here
				if (!IsPostBack){
					DataTable dt = DatabaseObjects.GetPromoGroups();
					cboPromoGroup.DataSource = dt;
					cboPromoGroup.DataTextField = "description";
					cboPromoGroup.DataValueField = "promo_group_id";
					cboPromoGroup.DataBind();

			   
					dt = DatabaseObjects.GetPromoTypes();
					cboPromoType.DataSource = dt;
					cboPromoType.DataTextField = "description";
					cboPromoType.DataValueField = "promotion_type_code";
					cboPromoType.DataBind();
					cboPromoType.Items.Insert(0,"---ALL---");


					dt = DatabaseObjects.GetPartners();
					cboPartner.DataSource = dt;
					cboPartner.DataTextField = "partner_name";
					cboPartner.DataValueField = "partner_id";
					cboPartner.DataBind();
					cboPartner.Items.Insert(0,"---ALL---");
				}
			}catch(Exception ex){
				throw new Global.CRMException("",ex,0,"PromotionGroups.Page_Load");
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
			this.cmdAssign.Click += new System.Web.UI.ImageClickEventHandler(this.cmdAssign_Click);
			this.cmdUnassign.Click += new System.Web.UI.ImageClickEventHandler(this.cmdUnassign_Click);
			this.cmdDetails.Click += new System.Web.UI.ImageClickEventHandler(this.cmdFilterUnassigned_Click);
			this.cmdDetailsUnclassified.Click += new System.Web.UI.ImageClickEventHandler(this.Imagebutton1_Click);
			this.cmdDetailsClassified.Click += new System.Web.UI.ImageClickEventHandler(this.cmdDetailsClassified_Click);

		}
		#endregion


		
		private void ListBox2_SelectedIndexChanged(object sender, System.EventArgs e) {
		
			
		}

		protected void cboPromoGroup_SelectedIndexChanged(object sender, System.EventArgs e) {
		   FillPromoGroups(Convert.ToInt32(cboPromoGroup.SelectedValue));
		}

		private void FillPromoGroups(int promoGroupID){
			try{
				DataTable dt = DatabaseObjects.GetPromoByGroups(promoGroupID);
				lblClassified.Text = cboPromoGroup.SelectedItem.Text + " Promotions: " + dt.Rows.Count.ToString();
				lstPromoGroup.DataSource = dt;
				lstPromoGroup.DataTextField = "description";
				lstPromoGroup.DataValueField = "promotion_id";
				lstPromoGroup.DataBind();
			}catch(Exception ex){
				throw new Global.CRMException("",ex,0,"PromotionGroups.FillPromoGroups");
			}
			
		
		}

		private void FillUnclassified(){
			try{

				DataTable dt = DatabaseObjects.GetPromoByGroups(0);
				DataView dv = new DataView(dt);
			
				string filter = "";
				if (cboPromoType.SelectedIndex != 0){
					filter = "promotion_type_code = '" + cboPromoType.SelectedItem.Value + "'";
					if (cboPartner.SelectedIndex != 0){
						filter = filter + " and ";	 
					}
				}
	        
				if (cboPartner.SelectedIndex != 0){
					filter = filter + "partner_id = " + cboPartner.SelectedItem.Value;
				}
			
				dv.RowFilter = filter;
				lblUnclassifed.Text = "Unclassified: " +  dv.Count.ToString();
		
 
				lstUnclassified.DataSource = dv;
				lstUnclassified.DataTextField = "description";
				lstUnclassified.DataValueField = "promotion_id";
				lstUnclassified.DataBind();

			}catch(Exception ex){
				throw new Global.CRMException("",ex,0,"PromotionGroups.FillUnclassified");
			}
			
		}

		private void cmdAssign_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
			try{
				foreach(ListItem li in lstUnclassified.Items) {
					if(li.Selected == true) {
						int id = Convert.ToInt32(li.Value);
						DatabaseObjects.UpdatePromoGroup(Convert.ToInt32(cboPromoGroup.SelectedValue),id);
					}
				}

				Refresh();
			}catch(Exception ex){
				throw new Global.CRMException("",ex,0,"PromotionGroups.cmdAssign_click");
			}
			
			
		}

		public void Refresh(){
			FillPromoGroups(Convert.ToInt32(cboPromoGroup.SelectedValue));
			FillUnclassified();

		}

		private void cmdFilterUnassigned_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
		   FillUnclassified();
		}

		private void cmdUnassign_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
			try{foreach(ListItem li in lstPromoGroup.Items) {
					if(li.Selected == true) {
						int id = Convert.ToInt32(li.Value);
						DatabaseObjects.UpdatePromoGroup(0,id);
					}
				}

				Refresh();
			}catch(Exception ex){
				throw new Global.CRMException("",ex,0,"PromotionGroups.cmdUnassign_click");
			}
		}

		private void lstUnclassified_SelectedIndexChanged(object sender, System.EventArgs e) {
			
			
		}

		private void Imagebutton1_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
		
			try{

				if (lstUnclassified.SelectedIndex > 0){
				
					int id = Convert.ToInt32(lstUnclassified.SelectedItem.Value);
					if (id > 0){   
						DataTable dt = DatabaseObjects.GetPromotionDetail(id);
						lblAdvertiser.Text = Helper.IFNULL_S(dt.Rows[0]["advertiser"],"");
						lblPartner.Text = Helper.IFNULL_S(dt.Rows[0]["partner"],"");
						lblPromo.Text = Helper.IFNULL_S(dt.Rows[0]["promotion"],"");
						lblPromoGroup.Text = Helper.IFNULL_S(dt.Rows[0]["promo_group"],"");
						lblPromoID.Text = Helper.IFNULL_S(dt.Rows[0]["promotion_id"],"");
						lblPromoType.Text = Helper.IFNULL_S(dt.Rows[0]["promo_type"],"");
						lblPromoTypeCode.Text = Helper.IFNULL_S(dt.Rows[0]["promotion_type_code"],"");
						lblScript.Text = Helper.IFNULL_S(dt.Rows[0]["script_name"],"");
					}
		
				}
			}catch(Exception ex){
				throw new Global.CRMException("",ex,0,"PromotionGroups.ListBox2_SelectedIndexChanged");
			}
		  
		
     	}

		private void cmdDetailsClassified_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
			try{
				if (lstPromoGroup.SelectedIndex > 0){
		
					int id = Convert.ToInt32(lstPromoGroup.SelectedItem.Value);
					if (id > 0){   
						DataTable dt = DatabaseObjects.GetPromotionDetail(id);
						lblAdvertiser.Text = Helper.IFNULL_S(dt.Rows[0]["advertiser"],"");
						lblPartner.Text = Helper.IFNULL_S(dt.Rows[0]["partner"],"");
						lblPromo.Text = Helper.IFNULL_S(dt.Rows[0]["promotion"],"");
						lblPromoGroup.Text = Helper.IFNULL_S(dt.Rows[0]["promo_group"],"");
						lblPromoID.Text = Helper.IFNULL_S(dt.Rows[0]["promotion_id"],"");
						lblPromoType.Text = Helper.IFNULL_S(dt.Rows[0]["promo_type"],"");
						lblPromoTypeCode.Text = Helper.IFNULL_S(dt.Rows[0]["promotion_type_code"],"");
						lblScript.Text = Helper.IFNULL_S(dt.Rows[0]["script_name"],"");
					}
				}
			}catch(Exception ex){
				throw new Global.CRMException("",ex,0,"PromotionGroups.ListBox2_SelectedIndexChanged");
			}
		  
		}
		


	}
}
