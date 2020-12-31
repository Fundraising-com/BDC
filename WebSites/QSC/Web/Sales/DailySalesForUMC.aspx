<%@ Page language="c#" debug="true" Codebehind="DailySalesForUMC.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.Sales.DailySalesForUMC" %>
<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="System.Data" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Fulfillment System</title>
		<link href="../Includes/QSPFulfillment.css" type="text/css" rel="stylesheet">
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</head>
	<body leftMargin="0" topMargin="0" marginwidth="0" marginheight="0">
		<form id="DailySalesForUMC" method="post" runat="server">
		<!--#include file="../Includes/Menu.inc"-->
			<center>
				<h3><font face="Verdana" color="#2f4f88">Daily Sales By UMC Code</font></h3>
			</center>
			<p></p>
			<center><asp:Label id="ProductLabel" CssClass="ClearTextBoxB" runat="server" /></center>
			<p></p>
			<TABLE width="70%" border="0">
				<TR>
					<TD>
						<asp:DataGrid Width="60%" OnItemDataBound="UMCSalesDG_ItemDataBound" id="UMCSalesDG" runat="server" HorizontalAlign="Right" ShowFooter="True" AutoGenerateColumns="False" BorderColor="Black" BorderWidth="1px">
							<ItemStyle Font-Size="Smaller" Font-Names="Verdana" Font-Bold="True" BackColor="Silver"></ItemStyle>
							<HeaderStyle Font-Size="Smaller" Font-Names="Verdana" Font-Bold="True" ForeColor="White" BackColor="#2F4F88"></HeaderStyle>
							<FooterStyle Font-Size="Smaller" Font-Names="Verdana" Font-Bold="True" ForeColor="White" BackColor="#2F4F88"></FooterStyle>
							<Columns>
								<asp:BoundColumn DataField="Day" HeaderText="Day" />
								<asp:BoundColumn DataField="Subs" HeaderText="Subs">
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<FooterStyle HorizontalAlign="Right"></FooterStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Amount" HeaderText="Amount">
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<FooterStyle HorizontalAlign="Right"></FooterStyle>
								</asp:BoundColumn>
							</Columns>
						</asp:DataGrid>
					</TD>
				</TR>
			</TABLE>
			<p></p>
			<center>
				<A class="ClearTextBox" id="A1" href="/Sales/Sales.aspx"><IMG alt="Back to Sales" src="../Images/L_arrow.gif" border="0">
					Back to Sales</A>
			</center>
		</form>
		<script language="C#" runat="server">	

string connString = ConfigurationSettings.AppSettings["DSN"];
SqlConnection conn = new SqlConnection();
DataSet ds = new DataSet();
string strPCode;
string strDate;
int TotalUnits;
Decimal TotalSales;
int SubsCur = 0;
 
				 
void Page_Load()
{	
	conn.ConnectionString = connString;
	strPCode = (string)Request.QueryString["RequestID"];
	strDate = (string)Request.QueryString["WeekDate"];
	
	ProductLabel.Text = "Title: <font color=red>" + Request.QueryString["Name"] + "&nbsp;&nbsp;&nbsp;  </font>UMC: <font color=red>" +strPCode+"</font>";
		
	if (!IsPostBack) 
	{
		BindGrid();
	} 
}
  
void BindGrid()
{
	conn.Open();

	SqlDataAdapter da = new SqlDataAdapter("GetDailySalesByUMC",conn);

	da.SelectCommand.CommandType = CommandType.StoredProcedure;

	da.SelectCommand.Parameters.Add(new SqlParameter("@ProductCode", SqlDbType.VarChar,10));
	da.SelectCommand.Parameters["@ProductCode"].Value = strPCode;

	da.SelectCommand.Parameters.Add(new SqlParameter("@WeekStart", SqlDbType.DateTime));
	da.SelectCommand.Parameters["@WeekStart"].Value = Convert.ToDateTime(strDate);

	//fill the dataset and retrieve the default view
	da.Fill(ds, "DailyUMCSales");
	DataView dv = ds.Tables["DailyUMCSales"].DefaultView;

	UMCSalesDG.DataSource = dv;
	UMCSalesDG.DataBind();

	conn.Close();			
}//BindGrid()
					
		
void CalcTotalUnits (string TotalSubs)
{
	TotalUnits += Int32.Parse(TotalSubs);
}
  
void CalcTotalSales (string TtlSales)
{
	TotalSales += Decimal.Parse(TtlSales);
}

void UMCSalesDG_ItemDataBound(Object sender, DataGridItemEventArgs e)

{
	if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
	{
		e.Item.Cells[0].Text = string.Format("{0:MM/dd/yy}", Convert.ToDateTime(e.Item.Cells[0].Text));
		CalcTotalUnits( e.Item.Cells[1].Text );
		e.Item.Cells[1].Text = string.Format("{0:#,###}", Convert.ToDouble(e.Item.Cells[1].Text));
		CalcTotalSales ( e.Item.Cells[2].Text );
		e.Item.Cells[2].Text = string.Format("{0:c}", Convert.ToDouble(e.Item.Cells[2].Text));
		
	}
	else if(e.Item.ItemType == ListItemType.Footer )
	{
		e.Item.Cells[0].Text="Totals";
		e.Item.Cells[1].Text = string.Format("{0:#,###}", TotalUnits);
		e.Item.Cells[2].Text= string.Format("{0:c}", TotalSales);
		
	} 
}


		</script>
	</body>
</html>
