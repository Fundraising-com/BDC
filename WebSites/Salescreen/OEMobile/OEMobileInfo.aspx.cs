using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.Mobile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.MobileControls;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;

public partial class OEMobileInfo : System.Web.UI.MobileControls.MobilePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            SetupGreetings();
            SetupFMSelector();
            SetupAccountSelector();
            //SetupOrderList();
        }
    }

    private string FirstName
    {
        get 
        {
            string fname = "";
            if (Session["first_name"] != null)
                fname = Session["first_name"].ToString();
            return fname;
        }
    }
    private string LastName
    {
        get
        {
            string lname = "";
            if (Session["last_name"] != null)
                lname = Session["last_name"].ToString();
            return lname;
        }
    }
    private string FMID
    {
        get
        {
            string fmid = "";
            if (Session["fm_id"] != null)
                fmid = Session["fm_id"].ToString();
            return fmid;
        }
    }
    private string UserID
    {
        get
        {
            string user_id = "";
            if (Session["user_id"] != null)
                user_id = Session["user_id"].ToString();
            return user_id;
        }
    }
    private int RoleID
    {
        get
        {
            int role_id = 0;
            if (Session["role_id"] != null)
                role_id = Convert.ToInt32(Session["role_id"].ToString());
            return role_id;
        }
    }

    private string SelectedFM
    {
        get 
        {

            if (this.RoleID > 1)
                return ddlFM.Selection.Value;
            else
                return FMID;
        }
    }

    private string SelectedAccount
    {
        get
        {
            if (ddlAccount.Selection != null)
                return ddlAccount.Selection.Value;
            else
                return "0";
        }
    }

    private void SetupGreetings()
    {
        if ((FirstName.Trim() != "") & (LastName.Trim() != ""))
        { 
            lblGreetings.Text = "Hi " + FirstName + " " + LastName;
        }
    }

    private void SetupFMSelector()
    {
        if (this.RoleID > 1)
        {
            ddlFM.Visible = true;
            
            SqlCommand myCommand = new SqlCommand();
            myCommand.CommandText = "pr_oemobile_getFM";
            myCommand.CommandType = CommandType.StoredProcedure;

            DataTable dtbl = ExecuteQuery(myCommand);
            ddlFM.DataSource = dtbl;
            ddlFM.DataTextField = "info";
            ddlFM.DataValueField = "fm_id";
            ddlFM.DataBind();

            ddlFM.Items.Insert(0,new MobileListItem("Select an FM","0"));
            ddlFM.Items[0].Selected = true;
            
        }
        else 
        {
            ddlFM.Visible = false;
        }
    }

    private void SetupAccountSelector()
    {
        if (SelectedFM != "0")
        {
            SqlCommand myCommand = new SqlCommand();
            myCommand.CommandText = "pr_oemobile_getAccount";
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add(new SqlParameter("@fm_id", SqlDbType.VarChar, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, SelectedFM));

            DataTable dtbl = ExecuteQuery(myCommand);
            ddlAccount.DataSource = dtbl;
            ddlAccount.DataTextField = "account_info";
            ddlAccount.DataValueField = "account_id";
            ddlAccount.DataBind();

            ddlAccount.Items.Insert(0, new MobileListItem("Select an Account", "0"));
            ddlAccount.Items[0].Selected = true;


            if(ddlAccount.Items.Count == 1)
                cmdShowOrder.Visible = false;
            else
                cmdShowOrder.Visible = true;
            //wait to look if there is existing orders before showing the list
            lblOrderTitle.Visible = false;
            lstOrder.Visible = false;
            
           
        }
        else
        {
            lblAction.Text = "Please select an FM";
        }
    }

    private void SetupOrderList()
    {
        if (SelectedAccount != "0")
        {
            SqlCommand myCommand = new SqlCommand();
            myCommand.CommandText = "pr_oemobile_getOrder";
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add(new SqlParameter("@account_id", SqlDbType.Int, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Convert.ToInt32(this.SelectedAccount)));

            DataTable dtbl = ExecuteQuery(myCommand);
            //Campaign_id, Order_id, order_status_info
            lstOrder.Items.Clear();
            if (dtbl.Rows.Count > 0)
            {

                lblOrderTitle.Visible = true;
                lstOrder.Visible = true;

                foreach (DataRow r in dtbl.Rows)
                {
                    lstOrder.Items.Add(r[0].ToString() + " , " + r[1].ToString() + " : " + r[2].ToString());
                }
            }
            else
            {
                lblOrderTitle.Visible = false;
                lstOrder.Visible = true;

                lstOrder.Items.Add("No Order For This Account");
            }
         
        }
        else 
        {
            lblAction.Text = "Please select an Account";
        }
    }

    private DataTable ExecuteQuery(SqlCommand myCommand)
    { 
        string connectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();

        SqlConnection myConnection = new SqlConnection(connectionString);

        try
        {
            
                
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Connection = myConnection;
               
                myConnection.Open();

                SqlDataAdapter sda = new SqlDataAdapter(myCommand);
                DataTable dtbl = new DataTable();
                sda.Fill(dtbl);

                myConnection.Close();

                return dtbl;
        }
        catch(Exception ex)
        {
            lblError.Text = "Enable to execute the requested command ("+ex.Message+")";
            lblError.Visible = true;
            return new DataTable();
        }
        finally
        {
            if (myConnection.State != ConnectionState.Closed)
                myConnection.Close();

        }

    
    }
   
    protected void cmdShowAccount_Click(object sender, EventArgs e)
    {
        SetupAccountSelector();
    }
    protected void cmdShowOrder_Click(object sender, EventArgs e)
    {
        SetupOrderList();
    }
}
