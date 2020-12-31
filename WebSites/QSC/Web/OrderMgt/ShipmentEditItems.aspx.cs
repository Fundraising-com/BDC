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
using QSPFulfillment.CommonWeb;

namespace QSPFulfillment.OrderMgt
{
	/// <summary>
	/// Summary description for ShipmentEditItems.
	/// </summary>
	public partial class ShipmentEditItems : QSPFulfillment.CommonWeb.QSPPage
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if (!IsPostBack) 
			{
				lblOrderIdHidden.Text = Request.QueryString["BatchOrderId"].ToString();
                lblShipmentGroupIDHidden.Text = Request.QueryString["ShipmentGroupID"].ToString();
                PopulateBatchInfo();
				PopulateDG();
			}


		}

		private void PopulateBatchInfo()
		{
			Business.Shipment oShipment = new Business.Shipment();
			DataTable oTable = oShipment.GetShipmentInfoByOrderID(Convert.ToInt32(lblOrderIdHidden.Text));
			
			lblOrderId.Text = oTable.Rows[0]["OrderId"].ToString();
			lblFor.Text = oTable.Rows[0]["For"].ToString();
			lblShipToGroupId.Text = oTable.Rows[0]["ShipToGroupId"].ToString();
			lblFMId.Text = oTable.Rows[0]["ShipToFMId"].ToString();
			lblCampaignId.Text = oTable.Rows[0]["CampaignId"].ToString();

		}

		private void PopulateDG() 
		{
            int? shipmentGroupID;
            if (Request.QueryString["ShipmentGroupID"] == "")
                shipmentGroupID = null;
            else
                shipmentGroupID = Convert.ToInt32(Request.QueryString["ShipmentGroupID"].ToString());

            Business.Shipment oShipment = new Business.Shipment();
			DataTable oTable = oShipment.GetShipmentVariationInfo(Convert.ToInt32(lblOrderIdHidden.Text), Session.SessionID.ToString(), shipmentGroupID);

			DataGrid1.DataSource = oTable;
			DataGrid1.DataBind();
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.DataGrid1.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_ItemCommand);
			this.DataGrid1.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemDataBound);

		}
		#endregion

		private void DataGrid1_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || 
				e.Item.ItemType == ListItemType.AlternatingItem)
			{
				if (Convert.ToBoolean(((DataRowView)e.Item.DataItem).Row.ItemArray[13]) == true)
				{
					TextBox oTextBox = new TextBox();
					oTextBox = (TextBox)e.Item.FindControl("tbQuantityShipped");
					oTextBox.Text = ((DataRowView)e.Item.DataItem).Row.ItemArray[6].ToString();
					oTextBox.Visible = true;

					oTextBox = (TextBox)e.Item.FindControl("tbReplacementQty");
					oTextBox.Text = ((DataRowView)e.Item.DataItem).Row.ItemArray[7].ToString();
					oTextBox.Visible = true;

                    oTextBox = (TextBox)e.Item.FindControl("tbReplacementItem");
                    oTextBox.Text = ((DataRowView)e.Item.DataItem).Row.ItemArray[8].ToString();
                    oTextBox.Visible = true;

                    oTextBox = (TextBox)e.Item.FindControl("tbComment");
					oTextBox.Text = ((DataRowView)e.Item.DataItem).Row.ItemArray[10].ToString();
					oTextBox.Visible = true;

					oTextBox = (TextBox)e.Item.FindControl("tbCustomerComment");
					oTextBox.Text = ((DataRowView)e.Item.DataItem).Row.ItemArray[11].ToString();
					oTextBox.Visible = true;

					Label oLabel = new Label();
					oLabel = (Label)e.Item.FindControl("HComment");
					oLabel.Text = ((DataRowView)e.Item.DataItem).Row.ItemArray[10].ToString();
					oLabel.Visible = false;

					oLabel = (Label)e.Item.FindControl("HCustomerComment");
					oLabel.Text = ((DataRowView)e.Item.DataItem).Row.ItemArray[11].ToString();
					oLabel.Visible = false;

					oLabel = (Label)e.Item.FindControl("lblQuantityShipped");
					oLabel.Visible = false;
					
					oLabel = (Label)e.Item.FindControl("lblReplacementQty");
					oLabel.Visible = false;

                    oLabel = (Label)e.Item.FindControl("lblReplacementItem");
                    oLabel.Visible = false;

                    CheckBox oCheckBox = new CheckBox();
					oCheckBox = (CheckBox)e.Item.FindControl("cbShipItem");
					if (Convert.ToBoolean(((DataRowView)e.Item.DataItem).Row.ItemArray[9])) 
					{
						oCheckBox.Checked = true;
					}
					else
					{
						oCheckBox.Checked = false;
					}
					oCheckBox.Enabled = true;

					Button oButton = new Button();
					oButton = (Button)e.Item.FindControl("btnSplit");
					oButton.Attributes.Add("onClick", "javascript:return confirm('Are you sure you want to split this item? This is not reversible and will take effect immediately. You MUST click SAVE CHANGES before executing a split or you may lose changes you have made.')");

				}
				else
				{
					TextBox oTextBox = new TextBox();
					oTextBox = (TextBox)e.Item.FindControl("tbReplacementQty");
					oTextBox.Visible = false;

                    oTextBox = (TextBox)e.Item.FindControl("tbReplacementItem");
                    oTextBox.Visible = false;

                    oTextBox = (TextBox)e.Item.FindControl("tbQuantityShipped");
					oTextBox.Visible = false; 

					oTextBox = (TextBox)e.Item.FindControl("tbComment");
					oTextBox.Visible = false;

                    oTextBox = (TextBox)e.Item.FindControl("tbCustomerComment");
                    oTextBox.Visible = false;

                    oTextBox = (TextBox)e.Item.FindControl("tbSplitQuantity");
                    oTextBox.Visible = false;

                    Label oLabel = new Label();
					oLabel = (Label)e.Item.FindControl("lblReplacementQty");
					oLabel.Text = ((DataRowView)e.Item.DataItem).Row.ItemArray[7].ToString();
					oLabel.Visible = true;

                    oLabel = (Label)e.Item.FindControl("lblReplacementItem");
                    oLabel.Text = ((DataRowView)e.Item.DataItem).Row.ItemArray[8].ToString();
                    oLabel.Visible = true;

                    oLabel = (Label)e.Item.FindControl("lblQuantityShipped");
					oLabel.Text = ((DataRowView)e.Item.DataItem).Row.ItemArray[6].ToString();
					oLabel.Visible = true;

					oLabel = (Label)e.Item.FindControl("lblComment");
					oLabel.Text = ((DataRowView)e.Item.DataItem).Row.ItemArray[10].ToString();
					oLabel.Visible = true;
					oLabel = (Label)e.Item.FindControl("HComment");
					oLabel.Text = ((DataRowView)e.Item.DataItem).Row.ItemArray[10].ToString();
					oLabel.Visible = false;

					oLabel = (Label)e.Item.FindControl("lblCustomerComment");
					oLabel.Text = ((DataRowView)e.Item.DataItem).Row.ItemArray[11].ToString();
					oLabel.Visible = true;
					oLabel = (Label)e.Item.FindControl("HCustomerComment");
					oLabel.Text = ((DataRowView)e.Item.DataItem).Row.ItemArray[11].ToString();
					oLabel.Visible = false;

					CheckBox oCheckBox = new CheckBox();
					oCheckBox = (CheckBox)e.Item.FindControl("cbShipItem");
					if (Convert.ToBoolean(((DataRowView)e.Item.DataItem).Row.ItemArray[9])) 
					{
						oCheckBox.Checked = true;
					}
					else
					{
						oCheckBox.Checked = false;
					}
					oCheckBox.Enabled = false;

					Button oButton = new Button();
					oButton = (Button)e.Item.FindControl("btnSplit");
					oButton.Attributes.Add("onClick", "javascript:return confirm('Are you sure you want to split this item? This is not reversible and will take effect immediately. You MUST click SAVE CHANGES before executing a split or you may lose changes you have made.')");
                    oButton.Visible = false;
				}
			}
		}

		protected void Button1_Click(object sender, System.EventArgs e)
		{
			Business.Shipment oShipment = new Business.Shipment();

            // Let's Clear out all Temp Shipment Variations
            //oShipment.DeleteShipmentVariations(Session.SessionID.ToString());

            foreach (DataGridItem dgItem in DataGrid1.Items)
			{
				// Get all Hidden Values To Compare
				HtmlInputHidden oHidden = new HtmlInputHidden();
				oHidden = (HtmlInputHidden)dgItem.FindControl("HIsEditable");
				
				if (Convert.ToBoolean(oHidden.Value)) 
				{

					oHidden = (HtmlInputHidden)dgItem.FindControl("HIsFromThisSession");
					bool bIsFromThisSession = Convert.ToBoolean(oHidden.Value);

					bool bRowChanged = false;

					// Compare each item that is editable to see if it has been changed
					oHidden = (HtmlInputHidden)dgItem.FindControl("HOriginalQuantityShipped");
					int lOriginalQuantityShipped = Convert.ToInt32(oHidden.Value);
					
					TextBox oTextBox = new TextBox();
					oTextBox = (TextBox)dgItem.FindControl("tbQuantityShipped");
					int lQuantityShipped = Convert.ToInt32(oTextBox.Text);

					if (lOriginalQuantityShipped != lQuantityShipped) 
					{
						bRowChanged = true;
					}


					oHidden = (HtmlInputHidden)dgItem.FindControl("HOriginalQuantityReplaced");
					int lOriginalQuantityReplaced = Convert.ToInt32(oHidden.Value);
					
					oTextBox = (TextBox)dgItem.FindControl("tbReplacementQty");
					int lQuantityReplaced = Convert.ToInt32(oTextBox.Text);

					if (lOriginalQuantityReplaced != lQuantityReplaced) 
					{
						bRowChanged = true;
					}


					oHidden = (HtmlInputHidden)dgItem.FindControl("HReplacementItemId");
					int lOriginalReplacementItemId = Convert.ToInt32(oHidden.Value);
					
					int lReplacementItem = 0;
					oTextBox = (TextBox)dgItem.FindControl("tbReplacementItem");
					if (oTextBox.Text != "") 
					{
						lReplacementItem = Convert.ToInt32(oTextBox.Text);
					}

					if (lOriginalReplacementItemId != lReplacementItem) 
					{
						bRowChanged = true;
					}



					oHidden = (HtmlInputHidden)dgItem.FindControl("HShipTF");
					bool bOriginalShipTF = Convert.ToBoolean(oHidden.Value);

					CheckBox oCheckBox = new CheckBox();
					oCheckBox = (CheckBox)dgItem.FindControl("cbShipItem");
					bool bShipTF = oCheckBox.Checked;

					if (bOriginalShipTF != bShipTF) 
					{
						bRowChanged = true;
					}

					Label oLabel = new Label();
					oLabel = (Label)dgItem.FindControl("HComment");
					string sOriginalComment = Convert.ToString(oHidden.Value);
					
					oTextBox = (TextBox)dgItem.FindControl("tbComment");
					string sComment = Convert.ToString(oTextBox.Text);

					if (Convert.ToBoolean(sOriginalComment.CompareTo(sComment)) == false) 
					{
						bRowChanged = true;
					}


					oLabel = (Label)dgItem.FindControl("HCustomerComment");
					string sOriginalCustomerComment = Convert.ToString(oHidden.Value);
					
					oTextBox = (TextBox)dgItem.FindControl("tbCustomerComment");
					string sCustomerComment = Convert.ToString(oTextBox.Text);

					if (Convert.ToBoolean(sOriginalCustomerComment.CompareTo(sCustomerComment)) == false) 
					{
						bRowChanged = true;
					}


					if (bRowChanged == true || bIsFromThisSession == true) 
					{

						
						string sModifiedBy = QSPPage.aUserProfile.UserName;

						oHidden = (HtmlInputHidden)dgItem.FindControl("HCOHInstance");
						int lCustomerOrderHeaderInstance = Convert.ToInt32(oHidden.Value);
					
						oHidden = (HtmlInputHidden)dgItem.FindControl("HTransId");
						int lTransId = Convert.ToInt32(oHidden.Value);
					
						
						oShipment.InsertShipmentVariation(
								Session.SessionID.ToString()
								, lCustomerOrderHeaderInstance
								, lTransId
								, lQuantityShipped
								, lQuantityReplaced
								, lReplacementItem
								, bShipTF
								, sComment
								, sCustomerComment
								, sModifiedBy
							);
					}

				}
			}
		}

		private void DataGrid1_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
		
			if (e.CommandName == "SplitCOD")
			{
				
				HtmlInputHidden oHidden = new HtmlInputHidden();
				oHidden = (HtmlInputHidden)e.Item.FindControl("HQtyOrdered");
				Int32 lQuantityOrdered = Convert.ToInt32(oHidden.Value);

				oHidden = (HtmlInputHidden)e.Item.FindControl("HCOHInstance");				
				Int32 lCustomerOrderHeaderInstance = Convert.ToInt32(oHidden.Value);

				oHidden = (HtmlInputHidden)e.Item.FindControl("HTransId");				
				Int32 lTransId = Convert.ToInt32(oHidden.Value);

				TextBox oTextBox = new TextBox();
				oTextBox = (TextBox)e.Item.FindControl("tbSplitQuantity");
				Int32 lSplitQuantity = Convert.ToInt32(oTextBox.Text);

				string sModifiedBy = QSPPage.aUserProfile.UserName;

				if (lQuantityOrdered > lSplitQuantity) 
				{
					Business.Shipment oShipment = new Business.Shipment();
					oShipment.SplitCOD(lCustomerOrderHeaderInstance, lTransId, lSplitQuantity, sModifiedBy);
					lblMessage.ForeColor = System.Drawing.Color.Green;
					lblMessage.Text = "Detail record split successfully.";
					PopulateDG();
				}
				else
				{
					//insert error handling here
					lblMessage.ForeColor = System.Drawing.Color.Red;
					lblMessage.Text = "Invalid split quantity.";
				}
			}


		}
	}
}
