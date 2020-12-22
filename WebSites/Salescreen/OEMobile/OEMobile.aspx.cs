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

public partial class OEMobile : System.Web.UI.MobileControls.MobilePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Command1_Click(object sender, EventArgs e)
    {
        ValidateUser();
    }
    private void ValidateUser()
    {
        string connectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString() ;
        SqlConnection myConnection = new SqlConnection(connectionString);

        try
        {
            if (txtPassword.Text.Trim().Length > 0 && txtUserName.Text.Trim().Length > 0)
            {
                string query = "pr_oemobile_login";

                SqlCommand myCommand = new SqlCommand();
                myCommand.CommandText = query;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Connection = myConnection;
                myCommand.Parameters.Add(new SqlParameter("@username", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, txtUserName.Text.Trim()));
                myCommand.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, txtPassword.Text.Trim()));

                myConnection.Open();

                SqlDataAdapter sda = new SqlDataAdapter(myCommand);
                DataTable dtbl = new DataTable();
                sda.Fill(dtbl);

                myConnection.Close();

                if (dtbl.Rows.Count > 1 || dtbl.Rows.Count == 0)
                {
                    lblError.Text = "Error : User invalid";
                    lblError.Visible = true;
                }
                else
                {
                    this.Session["fm_id"] = dtbl.Rows[0]["fm_id"].ToString();
                    this.Session["user_id"] = dtbl.Rows[0]["user_id"].ToString();
                    this.Session["first_name"] = dtbl.Rows[0]["first_name"].ToString();
                    this.Session["last_name"] = dtbl.Rows[0]["last_name"].ToString();
                    this.Session["role_id"] = dtbl.Rows[0]["role_id"].ToString();

                    this.RedirectToMobilePage("OEMobileInfo.aspx");
                }

            }
            else
            {
                lblError.Text = "User Name or Password is invalid";
                lblError.Visible = true;
            }
        }
        catch(Exception ex)
        {
            lblError.Text = "Enable to execute the requested command ( "+ex.Message+" )";
            lblError.Visible = true;
        }
        finally
        {
            if (myConnection.State != ConnectionState.Closed)
                myConnection.Close();
        }

    }
}
