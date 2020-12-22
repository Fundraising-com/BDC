<%@ Page Language="C#" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="System.Configuration" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Uptime</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="UptimeLabel" runat="server" Text=""></asp:Label>
    </div>
    </form>
</body>
</html>

<script runat="server">
    /// <summary>
    /// This Uptime performs simple operations and returns the keyword "success" that is
    /// monitored by an external service.
    /// </summary>
    void Page_Load()
    {
        try
        {
            // Perform any operation that you like to test for uptime here...
            PingSQLServer(efundraising.Configuration.ApplicationSettings.GetConfig()["EFundWeb.SqlConnection.Release", "connectionString"]);

            UptimeLabel.Text = "Success";
        }
        catch (Exception e)
        {
            UptimeLabel.Text = "Failed. " + e.Message;
        }
    }

    /// <summary>
    /// Get date from SQL Server using the provided connection string.
    /// </summary>
    /// <param name="connectionString"></param>
    /// <param name="timeout"></param>
    private void PingSQLServer(string connectionString)
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = connectionString;
        conn.Open();
        SqlCommand cmd = new SqlCommand("SELECT getdate()", conn);
        SqlDataReader reader = cmd.ExecuteReader();
        reader.Read();
        reader.Close();
        conn.Close();
    }
</script>
