<%@ Page language="c#" debug="true" Codebehind="ProgramSales.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.Sales.ProgramSales" %>
<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<html>
	<head>
		<title>Fulfillment System</title>
		<link href="../Includes/QSPFulfillment.css" type="text/css" rel="stylesheet">
	</head>
	<body leftMargin="0" topMargin="0" marginwidth="0" marginheight="0">
		<form id="Form1" method="post" runat="server">
		<%if( Request.QueryString["bExcel"] != "1" && (int)Session["ReportsOnly"] != 1){%>
		<!--#include file="../Includes/Menu.inc"-->
		<%}%>
			<center>
				<h3><font face="Verdana" color="#2f4f88">Sales By Alpha Code</font></h3>
			</center>
			<p></p>
			<center><asp:Label id="ProductLabel" CssClass="ClearTextBoxB" runat="server" /></center>
			<p></p>
			<table cellSpacing="0" cellPadding="0" width="80%" align="center" border="0">
				<tr>
					<td align="center">
						<b><font face="Verdana" size="2" color="#2f4f88">Season</font></b>&nbsp;
						<asp:DropDownList 
									ID="MagSeason" 
									Runat=server 
									CssClass ="boxlookW"
									DataSource='<%# GetMagSeason() %>'
									DataTextField="Season"
									DataValueField="Season"
									SelectedIndex='<%# GetSelectedMagSeason(strThisSeason) %>' />
						<b><font face="Verdana" size="2" color="#2f4f88">Year</font></b>&nbsp;
						<asp:DropDownList 
									ID="MagYear"
									Runat=server 
									CssClass ="boxlookW"
									DataSource='<%# GetFiscalYear() %>'
									DataTextField="MYear"
									DataValueField="MYear"
									SelectedIndex='<%# GetSelectedFiscalYear(dYear) %>' />
						&nbsp;&nbsp;
						<asp:LinkButton ID="Search" Runat="server" CssClass="boxlook" OnClick="ButtonClick" Text="<font face='Verdana' color='#2f4f88'>Go</font>" />
					</td>
				</tr>
			</table>
			<table cellSpacing="0" cellPadding="2" width="80%" align="center" border="0">
				<tr>
					<td><asp:datagrid OnItemDataBound="ProgramSalesDG_ItemDataBound" FooterStyle-BackColor="#2F4F88" FooterStyle-ForeColor="White" FooterStyle-Font-Bold="True" FooterStyle-Font-Names="Verdana" ShowFooter="True" align="center" id="ProgramSalesDG" Width="80%" runat="server" HeaderStyle-BackColor="#2f4f88" Font-Size="8pt" Font-Name="Verdana" CellSpacing="0" CellPadding="2" GridLines="Both" BorderWidth="1" BorderColor="black" AutoGenerateColumns="False" DataKeyField="Alpha_Code" BackColor="#bfbfbf" HeaderStyle-Font-Bold="True" HeaderStyle-CssClass="Font9BoldWhiteV">
							<Columns>
								<asp:TemplateColumn HeaderText="Alpha Code" HeaderStyle-Wrap="False">
									<ItemTemplate>
										<asp:HyperLink runat="server" Font-Bold="True" ForeColor="#2f4f88" Font-Underline="True" Text='<%# DataBinder.Eval(Container.DataItem, "Alpha_Code") %>' NavigateUrl='<%# "DailySalesByAlpha.aspx?RequestID=" + Server.UrlEncode((string)DataBinder.Eval(Container.DataItem, "Alpha_Code")) +"&Name="+Server.UrlEncode((string)Request.QueryString["Name"])%>'  ID="Hyperlink1" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="Program" HeaderText="Program">
									<ItemStyle HorizontalAlign="Left" />
									<FooterStyle HorizontalAlign="Left" />
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Units" HeaderText="Units">
									<ItemStyle HorizontalAlign="Right" />
									<FooterStyle HorizontalAlign="Right" />
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Amount" HeaderText="Sales">
									<ItemStyle HorizontalAlign="Right" />
									<FooterStyle HorizontalAlign="Right" />
								</asp:BoundColumn>
								<asp:BoundColumn DataField="AvgSubPrice" HeaderText="Avg Sub Price">
									<ItemStyle HorizontalAlign="Right" />
									<FooterStyle HorizontalAlign="Right" />
								</asp:BoundColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
				<tr height="4">
					<td>&nbsp;</td>
				</tr>
			</table>
			<p></p>
			<center>
			<%if( Request.QueryString["bExcel"] != "1" && (int)Session["ReportsOnly"] != 1){%>
				<A class="ClearTextBox" id="A1" href="/Reports/Reports.aspx"><IMG alt="Back to Reports" src="../Images/L_arrow.gif" border="0">
					Back to Reports</A>
			<%}%>
			</center>
		</form>
		<script language="C#" runat="server">
string connString = ConfigurationSettings.AppSettings["DSN"];
SqlConnection conn = new SqlConnection();
DataSet ds = new DataSet();
string strThisSeason;
int dMonth;
string dYear;
string strPCode;
int TotalUnits;
Decimal TotalSales;

  public DataTable GetFiscalYear()
  {
	SqlDataAdapter SqlDa = new SqlDataAdapter("SELECT MYear FROM Years ORDER BY MYear DESC", connString);
	SqlDa.Fill(ds, "Years");
	return ds.Tables["Years"];
  }
  
  int GetSelectedFiscalYear(string strFYear )
  {
    //Loop through each row in the DataSet
	DataTable dt = ds.Tables["Years"];
	for(int iLoop = 0; iLoop <= dt.Rows.Count - 1; iLoop++)
	{
		if (strFYear == dt.Rows[iLoop]["MYear"].ToString() )
		return iLoop;
	}
		//If there is no match, return 0
		return 0;
  }
  
  public DataTable GetMagSeason()
  {
    SqlDataAdapter SqlDa = new SqlDataAdapter("SELECT Season FROM Seasons ORDER BY Season", connString);
	SqlDa.Fill(ds, "Seasons");
	return ds.Tables["Seasons"];
  }
  
  int GetSelectedMagSeason(string strThisSeason )
  {
    //Loop through each row in the DataSet
	DataTable dt = ds.Tables["Seasons"];
	for(int iLoop = 0; iLoop <= dt.Rows.Count - 1; iLoop++)
	{
		if (strThisSeason == dt.Rows[iLoop]["Season"].ToString() )
		return iLoop;
	}
		//If there is no match, return 0
		return 0;
  }
  
  void Page_Load()
  {
	conn.ConnectionString = connString;
	
    DateTime aDate = DateTime.Now;
	dMonth = Convert.ToInt32(aDate.Month); 
	dYear  = Convert.ToString(aDate.Year); 
	strThisSeason = ((dMonth>=1 && dMonth<=6) ? strThisSeason = "SPRING" : strThisSeason = "FALL");	
	strPCode = Request.QueryString["RequestID"];
	ProductLabel.Text = "Title: <font color=red>" + Request.QueryString["Name"] + "&nbsp;&nbsp;&nbsp;  </font>UMC: <font color=red>" +strPCode+"</font>";
    
	
    if (!IsPostBack) 
	{
	    string sql  = @"SELECT MYear FROM Years ORDER BY MYear DESC";
		string sql2 = @"SELECT Season FROM Seasons ORDER BY Season";
		
		SqlCommand cmd  = new SqlCommand(sql,  conn);
		SqlCommand cmd2 = new SqlCommand(sql2, conn);
		
		conn.Open();
		SqlDataReader reader = cmd.ExecuteReader();

		// bind the readers to the DropDownLists
		MagYear.DataSource = reader;
		MagYear.DataBind();

		// close the reader 
		reader.Close();

		SqlDataReader reader2 = cmd2.ExecuteReader(); 

		MagSeason.DataSource = reader2;
		MagSeason.DataBind();

		// close the second reader
		reader2.Close();

		conn.Close();
		
	  BindGrid();
    } 
  }
  
  void BindGrid()
  {
  	conn.Open();
	SqlCommand cmd = new SqlCommand("GetSalesByAlphaCode", conn);
	cmd.CommandTimeout = 240;

	SqlDataAdapter da = new SqlDataAdapter();
	da.SelectCommand = cmd;

	da.SelectCommand.CommandType = CommandType.StoredProcedure;
	
	da.SelectCommand.Parameters.Add(new SqlParameter("@ProductCode", SqlDbType.VarChar, 10));
	da.SelectCommand.Parameters["@ProductCode"].Value = strPCode;
	
	da.SelectCommand.Parameters.Add(new SqlParameter("@MagYear", SqlDbType.Int, 4));
	da.SelectCommand.Parameters["@MagYear"].Value = Int32.Parse(MagYear.SelectedItem.Value.Trim());

	da.SelectCommand.Parameters.Add(new SqlParameter("@MagSeason", SqlDbType.VarChar, 6));
	da.SelectCommand.Parameters["@MagSeason"].Value = (string)MagSeason.SelectedItem.Value.Trim();
	
	//fill the dataset and retrieve the default view
    da.Fill(ds, "ProgramSalesReport");
    DataView dv = ds.Tables["ProgramSalesReport"].DefaultView;
               
    // bind the reader to the DataList
    ProgramSalesDG.DataSource = dv;
    ProgramSalesDG.DataBind();
      
    // close the connection
    if (conn != null) 
	{
		if (conn.State == ConnectionState.Open) 
			{conn.Close();}
		conn.Dispose();
	}
    
    	if (cmd != null) 
	{ cmd.Dispose(); }
	
	if (da != null) 
	{ da.Dispose();  }
  }
  	
  void ButtonClick(Object sender, EventArgs e) 
  {
	BindGrid();
  }
  
  void ProgramSalesDG_ItemDataBound(Object sender, DataGridItemEventArgs e)
  {
		if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
		{
			CalcTotalUnits( e.Item.Cells[2].Text );
			e.Item.Cells[2].Text = string.Format("{0:#,###}", Convert.ToDouble(e.Item.Cells[2].Text));
			CalcTotalSales( e.Item.Cells[3].Text );
			e.Item.Cells[3].Text = string.Format("{0:c}", Convert.ToDouble(e.Item.Cells[3].Text));
			e.Item.Cells[4].Text = string.Format("{0:c}", Convert.ToDouble(e.Item.Cells[4].Text));
		}
		else if(e.Item.ItemType == ListItemType.Footer )
		{
			e.Item.Cells[1].Text="Totals";
			e.Item.Cells[2].Text = string.Format("{0:#,###}", TotalUnits);
			e.Item.Cells[3].Text= string.Format("{0:c}", TotalSales);
			if(TotalUnits>0)
				e.Item.Cells[4].Text= string.Format("{0:c}", (TotalSales / TotalUnits));
			else
				e.Item.Cells[4].Text="0";
		
		} 
  }
  
  void CalcTotalUnits (string TotalSubs)
  {
	TotalUnits += Int32.Parse(TotalSubs);
  }
  
  void CalcTotalSales (string TtlSales)
  {
	TotalSales += Decimal.Parse(TtlSales);
  }
    
    
    
		</script>
	</body>
</html>
