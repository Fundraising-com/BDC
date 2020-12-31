using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Configuration;   

namespace QSPFulfillment.Admin
{
	///<summary>Update User Profiles and notify relevant Home Office Users.</summary>
	///<remarks>
	///	asp.net: maintainenance and updates: Josh Caesar
	///	asp.net: original author: Jim Corvino - Josh Caesar
	///	asp: original author: unknown, this code had 179 revisions in 
	///	classic asp form from Jan2001 to March2003.
	///</remarks>
	public class Profile : QSPFulfillment.CommonWeb.QSPPage
	{
		#region auto-generated code
		///<summary>Required method for Designer support</summary>
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		///<summary>Required method for Designer support</summary>
		private void InitializeComponent()
		{
			this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion auto-generated code

		protected Repeater		RepeaterAddr;
		protected Button		btnSubmit;
		protected Label			lblMsg;
		protected Label			lblFName;
		protected Label			lblMsg2;
		protected Label			lblMsg3;
		protected Label			lblRequired;
		private string			FMID;
		protected Label			Label1;
		private int				UserId;
		protected PlaceHolder	ph_menuProfileAdmin;
		private int				TimeZone = 0;

		private DAL.ProfileDataAccess aProfileDataAccess;

		public Profile()
		{
			aProfileDataAccess = new DAL.ProfileDataAccess();
		}

		private void Page_Load(object src, System.EventArgs e)
		{
			//first and foremost, dont cache this , sensitive stuff
			Response.Expires = -1;
			Response.AppendHeader("Pragma", "no-cache");
			Response.CacheControl = "no-cache";
			
			//get the UserId
			string	strInstance;
			try   { strInstance = aUserProfile.Instance.ToString(); } 
			catch { strInstance = "There was an error finding your user id instance. Most likely, you timed out."; }
			try   { UserId = Convert.ToInt32(strInstance.Trim()); }
			catch 
			{
				Response.Write("The following instance could not be converted to an integer:<br />");
				Response.Write("<div style='color:red'>" + strInstance + "</div><br />");
				Response.Write("If this problem persists, please contact <a style='text-decoration:none' href='mailto:QSP_IT@rd.com'>QSP_IT@rd.com</a><br />");
				Response.Write("Please log back in at <a href=\"QSPFulfillmentLogin.aspx\">QSP Fulfillment Login</a>");
				Response.End();
			}

			//use admin menu if appropriate
			if( (Session["ProfileAdmin"] != null) && ((bool)Session["ProfileAdmin"] == true) )
			{
				Menu1.DataSource = Server.MapPath("MenuBarProfileAdmin.xml");
				Menu1.DataBind();
			}
			
			string strFname;
			try   { strFname = Session["sFirstName"].ToString(); }
			catch { strFname = ""; }

			//set the greeting label
			lblFName.Text = "Hello " + strFname;
			if((Session["ProfileAdmin"] != null)&&((bool)Session["ProfileAdmin"] == true))
			{
				lblFName.Text = "";
			}
			else if((Session["ChangePassword"]+"") != "")
			{
				lblFName.Text = "Hello " + strFname + ".&nbsp;&nbsp;Your account has indicated that you need to change your password.<br />"
					+ "Type your new password twice into the text boxes provided.<br />";
				//+ "In addition, please make corrections to your profile settings and please provide us with all applicable information.  (Fields with asterisks are required.) &nbsp; Thanks!<br />";
			}
			else
			{
				lblFName.Text = "Hello " + strFname + ".&nbsp;&nbsp;Please make any desired changes to your acccount.<br />";
				//+ "If you are changing your password, type your new password twice into the text boxes provided. &nbsp; (Fields with asterisks are required.) Thanks! <br />";
			}

			FMID = getFMid();
			if (!Page.IsPostBack)
			{
				BindAddrRepeat(UserId);
			}
			
		}
				

		/// <summary>Set a marker for updated fields</summary>
		/// <remarks>If marker isnt set, database wont be updated</remarks>
		/// <param name="src"></param>
		/// <param name="e"></param>
		public void lblMessageChanged(object src, System.EventArgs e)
		{
			lblRequired.Text = "X";
		}


		private void BindAddrRepeat(int id)
		{
			RepeaterAddr.DataSource = aProfileDataAccess.SelectProfile(id);
			RepeaterAddr.DataBind();

			if(RepeaterAddr.Items.Count == 0)
			{
				lblMsg.Text  = "No profile information could be found for you.<br />";
				lblMsg.Text += "Please contact <a href=\"mailto:QSP_IT@qsp.com\">QSP_IT@qsp.com</a><br />";
				RepeaterAddr.Visible = false;
			} 
			else if (RepeaterAddr.Items.Count > 1)
			{
				lblMsg.Text  = "Multiple profiles were found for you.<br />";
				lblMsg.Text += "Please contact <a href=\"mailto:QSP_IT@qsp.com\">QSP_IT@qsp.com</a><br />";
				RepeaterAddr.Visible = false;
			}
			else
			{
				RepeaterAddr.Visible = true;
			}

		}


		private void btnSubmit_Click(object src, System.EventArgs e)
		{
			if(Page.IsValid)
			{
				bool SendToDB;
				
				if (lblRequired.Text != "X")
				{
					lblMsg3.Text ="No values were changed.";
					lblMsg2.Text ="";
					SendToDB = false;
				}
				else				
				{
					SendToDB = true;
					lblMsg3.Text ="";
				}
				
				String  MESSAGE;
				MESSAGE = "";
   
				//since the Page.IsValid now check to make sure 
				//there is no malicious data
				//truncate all fields at their column size
				//length < 1 checks are for mandatory items only
				
				string RFUserName = null, RFPassword = null, RFVerifyPassword = null;
				if( (Session["ProfileAdmin"] == null) || ((bool)Session["ProfileAdmin"] == false) )
				{
					RFUserName = ""; //USERNAME
					if(SendToDB == true) 
					{
						RFUserName = Request.Form["RepeaterAddr:_ctl0:txtusername"].Trim();
						if( RFUserName.Length < 1) 
						{
							SendToDB = false;
							MESSAGE  = "The UserName field is necessary.";
						}
						if( RFUserName.Length > 20 ) 
						{
							RFUserName = RFUserName.Substring(0, 20);
						}
					}
									
					RFPassword = ""; //PASSWORD
					if(SendToDB == true) 
					{
						RFPassword = Request.Form["RepeaterAddr:_ctl0:txtPassword"].Trim();
						if( RFPassword.Length < 1) 
						{
							SendToDB = false;
							MESSAGE  = "The Password field is necessary.";
						}
						if( RFPassword.Length > 20 ) 
						{
							RFPassword = RFPassword.Substring(0, 20);
						}
					}

					RFVerifyPassword = ""; //VERIFYPASSWORD
					if(SendToDB == true) 
					{
						RFVerifyPassword = Request.Form["RepeaterAddr:_ctl0:txtVerifyPassword"].Trim();
						if( RFVerifyPassword.Length < 1) 
						{
							SendToDB = false;
							MESSAGE  = "The Verify Password field is necessary.";
						}
						if( RFVerifyPassword != RFPassword ) 
						{
							SendToDB = false;
							MESSAGE  = "The Password fields don't Match.";
						}
					}
				} //end if Session["ProfileAdmin"] == null || false
				
				string RFFirstName = ""; //FirstName
				if(SendToDB == true) 
				{
					RFFirstName = Request.Form["RepeaterAddr:_ctl0:txtFirstName"].Trim();
				}

				string RFLastName = ""; //LastName
				if(SendToDB == true) 
				{
					RFLastName = Request.Form["RepeaterAddr:_ctl0:txtLastName"].Trim();
					if( RFLastName.Length < 1) 
					{
						SendToDB = false;
						MESSAGE  = "Last Name field is necessary.";

					}

					if( RFLastName.Length > 20 ) 
					{
						RFLastName = RFLastName.Substring(0, 20);
					}
				}

				string RFEmail = ""; //Email
				if(SendToDB == true) 
				{
					RFEmail = Request.Form["RepeaterAddr:_ctl0:txtEmail"].Trim();
				}

				
				string RFSigOther = ""; //Significant Other
				if(SendToDB == true) 
				{
					RFSigOther = Request.Form["RepeaterAddr:_ctl0:txtSigOther"].Trim();
				}

				
				string RFVME = "";		//Voice Mail Ext (ie Meridian)
				if(SendToDB == true) 
				{
					RFVME = Request.Form["RepeaterAddr:_ctl0:txtVoiceMail"].Trim();
				}

				
				string RFHomePhone = "";//HomePhone
				if(SendToDB == true)
				{
					RFHomePhone = CleanPhone(Request.Form["RepeaterAddr:_ctl0:txtHomePhone"].Trim());
				}
				
				string RFWorkPhone = "";//WorkPhone
				if(SendToDB == true)
				{
					RFWorkPhone = CleanPhone(Request.Form["RepeaterAddr:_ctl0:txtWorkPhone"].Trim());
				}

				
				string RFFax = "";		//Fax
				if(SendToDB == true)
				{
					RFFax = CleanPhone(Request.Form["RepeaterAddr:_ctl0:txtFAX"].Trim());
				}

				
				string RFTFP = ""; //Toll Free Phone
				if(SendToDB == true)
				{
					RFTFP = CleanPhone(Request.Form["RepeaterAddr:_ctl0:txtTollFreeNbr"].Trim());
				}

				string RFMobilePhone = ""; //Cell Phone
				if(SendToDB == true)
				{
					RFMobilePhone = CleanPhone(Request.Form["RepeaterAddr:_ctl0:txtMobilePhone"].Trim());
				}

				string RFPager = ""; //Pager
				if(SendToDB == true)
				{
					RFPager = CleanPhone(Request.Form["RepeaterAddr:_ctl0:txtPager"].Trim());
				}

				string RFMailAddr1 = "";//Mailing Address Street 1
				if(SendToDB == true)
				{
					RFMailAddr1 = Request.Form["RepeaterAddr:_ctl0:txtMailAddr1"].Trim();
				}
				string RFMailAddr2 = "";//Mailing Address Street 2
				if(SendToDB == true)
				{
					RFMailAddr2 = Request.Form["RepeaterAddr:_ctl0:txtMailAddr2"].Trim();
				}				
				string RFMailCity = ""; //Mailing Address  City
				if(SendToDB == true)
				{
					RFMailCity = Request.Form["RepeaterAddr:_ctl0:txtMailCity"].Trim();
				}
				string RFMailState = ""; //Mailing Address  State
				if(SendToDB == true)
				{
					RFMailState = Request.Form["RepeaterAddr:_ctl0:txtMailState"].Trim();
				}
				string RFMailPostalCode = ""; //Mailing Address Postal Code
				if(SendToDB == true)
				{
					RFMailPostalCode = Request.Form["RepeaterAddr:_ctl0:txtMailPostalCode"].Trim();
				}

				string RFShipAddr1 = "";//Shipping Address Street 1
				if(SendToDB == true)
				{
					RFShipAddr1 = Request.Form["RepeaterAddr:_ctl0:txtShipAddr1"].Trim();
				}
				string RFShipAddr2 = "";//Shipping Address Street 2
				if(SendToDB == true)
				{
					RFShipAddr2 = Request.Form["RepeaterAddr:_ctl0:txtShipAddr2"].Trim();
				}
				string RFShipCity = "";//Shipping Address City
				if(SendToDB == true)
				{
					RFShipCity = Request.Form["RepeaterAddr:_ctl0:txtShipCity"].Trim();
				}
				string RFShipState = "";//Shipping Address State
				if(SendToDB == true)
				{
					RFShipState = Request.Form["RepeaterAddr:_ctl0:txtShipState"].Trim();
				}
				string RFShipPostalCode = "";//Shipping Address Postal Code
				if(SendToDB == true)
				{
					RFShipPostalCode = Request.Form["RepeaterAddr:_ctl0:txtShipPostalCode"].Trim();
				}

				string RFInvAddr1 = "";//Invoice Address Street 1
				if(SendToDB == true)
				{
					RFInvAddr1 = Request.Form["RepeaterAddr:_ctl0:txtInvAddr1"].Trim();
				}
				string RFInvAddr2 = "";//Invoice Address Street 2
				if(SendToDB == true)
				{
					RFInvAddr2 = Request.Form["RepeaterAddr:_ctl0:txtInvAddr2"].Trim();
				}				
				string RFInvCity = ""; //Invoice Address City
				if(SendToDB == true)
				{
					RFInvCity = Request.Form["RepeaterAddr:_ctl0:txtInvCity"].Trim();
				}
				string RFInvState = ""; //Invoice Address State
				if(SendToDB == true)
				{
					RFInvState = Request.Form["RepeaterAddr:_ctl0:txtInvState"].Trim();
				}
				string RFInvPostalCode = ""; //Invoice Address Postal Code
				if(SendToDB == true)
				{
					RFInvPostalCode = Request.Form["RepeaterAddr:_ctl0:txtInvPostalCode"].Trim();
				}
				string RFInvPhone = ""; //Invoice Phone
				if(SendToDB == true)
				{
					RFInvPhone = CleanPhone(Request.Form["RepeaterAddr:_ctl0:txtInvPhone"].Trim());
				}
				
				int RFDefaultTerm = 0;//Default Terms
				if(SendToDB == true)
				{
					RFDefaultTerm = Convert.ToInt32(Request.Form["RepeaterAddr:_ctl0:txtDefaultTerm"].Trim());
				}

				string RFInvoiceChecks = ""; //Make checks Payable To
				if(SendToDB == true)
				{
					RFInvoiceChecks = Request.Form["RepeaterAddr:_ctl0:txtInvoiceChecks"].Trim();
				}
				
				string RFDefaultMsg1 = "";//DefaultMsg1
				if(SendToDB == true)
				{
					RFDefaultMsg1 = Request.Form["RepeaterAddr:_ctl0:txtDefaultMsg1"].Trim();
				}
				string RFDefaultMsg2 = "";//DefaultMsg2
				if(SendToDB == true)
				{
					RFDefaultMsg2 = Request.Form["RepeaterAddr:_ctl0:txtDefaultMsg2"].Trim();
				}
				string RFDefaultMsg3 = "";//DefaultMsg3
				if(SendToDB == true)
				{
					RFDefaultMsg3 = Request.Form["RepeaterAddr:_ctl0:txtDefaultMsg3"].Trim();
				}

				//directory settings
				short RFTimeZone = 0;//Time Zone
				if(SendToDB == true) 
				{
					RFTimeZone = Convert.ToInt16(Request.Form["RepeaterAddr:_ctl0:TimeZoneDDL"].Trim());
				}

				bool RFdst = true;
				if(SendToDB == true) 
				{
					//RFdst = Convert.ToBoolean(Request.Form["RepeaterAddr:_ctl0:cb_DST"].Trim());
					try
					{
						//checked boxes come through as "on"
						if( Request.Form["RepeaterAddr:_ctl0:cb_DST"].Trim().ToLower() == "on" )
						{
							RFdst = true;
						} 
						else 	
						{
							RFdst = false;
						}
					}
					catch
					{
						//unchecked boxes dont show up in the request object at all
						RFdst = false;
					}
				}

				if( SendToDB == true )
				{
					//Create the required parameters
					string p_UserName = "";
					string p_Password = "";
					if( (Session["ProfileAdmin"] != null) && ((bool)Session["ProfileAdmin"] == true) )
					{
						p_UserName = "";
						p_Password = "";
					}
					else
					{
						p_UserName = RFUserName;
						p_Password = RFPassword;
					}

					int p_ModifiedBy;
					if( (Session["ProfileAdmin"] != null) && ((bool)Session["ProfileAdmin"] == true) )
					{
						p_ModifiedBy = Convert.ToInt32(Session["SalesAdminUserID"]);
					}
					else
					{
						p_ModifiedBy = UserId;
					}

					try
					{
						aProfileDataAccess.SaveProfile(FMID, p_UserName, p_Password,RFLastName,RFInvoiceChecks,RFEmail,
							RFSigOther,RFVME,RFHomePhone,RFWorkPhone,RFFax,RFTFP,RFMobilePhone,RFPager,RFMailAddr1,
							RFMailAddr2,RFMailCity,RFMailState,RFMailPostalCode,RFShipAddr1,RFShipAddr2,RFShipCity,
							RFShipState,RFShipPostalCode,RFInvAddr1,RFInvAddr2,RFInvCity,RFInvState,RFInvPostalCode,
							RFInvPhone,RFDefaultTerm,RFDefaultMsg1,RFDefaultMsg2,RFDefaultMsg3,RFTimeZone,RFdst,
							UserId,p_ModifiedBy);
						lblFName.Text= RFFirstName;
						lblMsg.Text= "Your Updates have been logged."; 
						lblMsg2.Text = "";
						lblRequired.Text = "";
						Session["sEMail"]    = RFEmail;
						Session["sLastName"] = RFLastName;
						Session["sUserName"] = RFUserName;
					}
					catch(SqlException exc)
					{
						Response.Write("<div style=\"font-color: red\">code 6: We are sorry, we are experiencing technical problems ...<br />");
						Response.Write("Message: " + exc.Message+ "<br />");
						Response.Write("Class: " + exc.Class+ "<br />");
						Response.Write("Line Number: " + exc.LineNumber+ "<br />");
						Response.Write("</div>");
					}

					//re-bind the page
					BindAddrRepeat(UserId);
				}
				else if(SendToDB == false) 
				{
					lblFName.Text= Session["sFirstName"].ToString();
					lblMsg.Text= MESSAGE;
					
					lblMsg2.Text = "No update has occurred.";
				}
			} //END _Update
		}

		/// <summary>gets the FMID from the session</summary>
		/// <remarks>Also, logs out users if FMID isnt there</remarks>
		/// <returns>string: FMID</returns>
		private string getFMid()
		{
			if(aUserProfile.FMID != null)
			{
				try
				{
					return aUserProfile.FMID.ToString();
				}
				catch
				{
					Response.Write("There was an error authenticating you for this page<br />");
					Response.Write("Please log back in at <a href=\"QSPFulfillmentLogin.aspx\">QSP Fulfillment Login</a>");
					Response.End();
					return "-999";
				}
			}
			else
			{
				Response.Write("There was an error authenticating you for this page(a FMID can't be found for you)<br />");
				Response.Write("Please log back in at <a href=\"QSPFulfillmentLogin.aspx\">QSP Fulfillment Login</a>");
				Response.End();
				return "-998";
			}
		}

		/// <summary>strips out formatting chars from phone numbers</summary>
		/// <param name="strIN">string: formatted #</param>
		/// <returns>string: stripped #</returns>
		private string CleanPhone(string strIN) 
		{
			string strOUT;
			strOUT = strIN;

			//strip out chars
			strOUT = strOUT.Replace(" ", "");
			strOUT = strOUT.Replace("-", "");
			strOUT = strOUT.Replace("(", "");
			strOUT = strOUT.Replace(")", "");
			strOUT = strOUT.Replace(".", "");
			strOUT = strOUT.Replace("	", "");

			return strOUT;
		}

		///<summary>turn authentication row on or off</summary>
		///<param name="src"></param>
		///<param name="e"></param>
		public void RepeaterAddr_ItemDataBound( Object src, RepeaterItemEventArgs e ) 
		{
			/* turn off required field validators for username and password when 
			 * profile admins use the page
			 */
			if( (Session["ProfileAdmin"] != null) && ((bool)Session["ProfileAdmin"] == true) )
			{
				PlaceHolder plh = (PlaceHolder) e.Item.FindControl("plh_authenticationInfo");
				if(plh != null) { plh.Visible = false; }
				RequiredFieldValidator rq = (RequiredFieldValidator) e.Item.FindControl("rq_username");
				if(rq  != null) { rq.Enabled  = false; }
				rq = (RequiredFieldValidator) e.Item.FindControl("rq_password");
				if(rq  != null) { rq.Enabled  = false; }
				rq = (RequiredFieldValidator) e.Item.FindControl("rq_verifypassword");
				if(rq  != null) { rq.Enabled  = false; }
			}

			/* Setup the timezone dropdown
			 * Cant just use selectedindex = value since they dont match
			 */
			TextBox tb = (TextBox) e.Item.FindControl("TimeZoneTB");
			if (tb != null) { TimeZone = Convert.ToInt16(tb.Text); tb.Visible = false; }
			DropDownList ddl = (DropDownList) e.Item.FindControl("TimeZoneDDL");
			if (ddl != null)
			{
				switch (TimeZone)
				{
					case -4:
					case -5:
					case -6:
					case -7:
					case -8:
					case -9:
					case -10:
						ddl.SelectedIndex = (TimeZone*-1) - 3;
						break;
					default:
						ddl.SelectedIndex = 0;
						break;
				}
			}

		}
	}
}
