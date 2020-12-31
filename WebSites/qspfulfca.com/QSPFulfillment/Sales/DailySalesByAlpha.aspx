<%@ Page language="c#" debug="true" Codebehind="DailySalesByAlpha.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.Sales.DailySalesByAlpha" %>
<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
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
		<form id="DailySales" method="post" runat="server">
		<%if( Request.QueryString["bExcel"] != "1" ){%>
		<!--#include file="../Includes/Menu.inc"-->
		<%}%>

			<center>
				<h3><font face="Verdana" color="#2f4f88">Weekly Sales By Alpha Code</font></h3>
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
			<TABLE id="tblWeekly" width="90%" border="0" align="center">
				<TR>
					<TD align="right"><asp:Label id="YearLabel" CssClass="ClearTextBoxB" runat="server" /></TD>
					<TD align="center"><asp:Label id="YearLabel_PY" CssClass="ClearTextBoxB" runat="server" /></TD>
					<TD align="center"><asp:Label id="YearLabel_2Yr" CssClass="ClearTextBoxB" runat="server" /></TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<asp:DataGrid OnItemDataBound="grdCurYr_ItemDataBound" id="grdCurYr" runat="server" HorizontalAlign="Right" ShowFooter="True" AutoGenerateColumns="False" BorderColor="Black" BorderWidth="1px">
							<ItemStyle Font-Size="Smaller" Font-Names="Verdana" Font-Bold="True" BackColor="Silver"></ItemStyle>
							<HeaderStyle Font-Size="Smaller" Font-Names="Verdana" Font-Bold="True" ForeColor="White" BackColor="#2F4F88"></HeaderStyle>
							<FooterStyle Font-Size="Smaller" Font-Names="Verdana" Font-Bold="True" ForeColor="White" BackColor="#2F4F88"></FooterStyle>
							<Columns>
								<asp:BoundColumn DataField="Week" HeaderText="Week"></asp:BoundColumn>
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
					<TD vAlign="top">
						<asp:DataGrid OnItemDataBound="grdPriorYr_ItemDataBound" id="grdPriorYr" runat="server" HorizontalAlign="Center" ShowFooter="True" AutoGenerateColumns="False" BorderColor="Black" BorderWidth="1px" BackColor="#2F4F88">
							<ItemStyle Font-Size="Smaller" Font-Names="Verdana" Font-Bold="True" BackColor="Silver"></ItemStyle>
							<HeaderStyle Font-Size="Smaller" Font-Names="Verdana" Font-Bold="True" ForeColor="White" BackColor="#2F4F88"></HeaderStyle>
							<FooterStyle Font-Size="Smaller" Font-Names="Verdana" Font-Bold="True" ForeColor="White" BackColor="#2F4F88"></FooterStyle>
							<Columns>
								<asp:BoundColumn DataField="Week" HeaderText="Week"></asp:BoundColumn>
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
					<TD vAlign="top">
						<asp:DataGrid OnItemDataBound="grdTwoAgo_ItemDataBound" id="grdTwoAgo" runat="server" HorizontalAlign="Left" ShowFooter="True" AutoGenerateColumns="False" BorderColor="Black" BorderWidth="1px" Font-Bold="True">
							<ItemStyle Font-Size="Smaller" Font-Names="Verdana" BackColor="Silver"></ItemStyle>
							<HeaderStyle Font-Size="Smaller" Font-Names="Verdana" Font-Bold="True" ForeColor="White" BackColor="#2F4F88"></HeaderStyle>
							<FooterStyle Font-Size="Smaller" Font-Names="Verdana" Font-Bold="True" ForeColor="White" BackColor="#2F4F88"></FooterStyle>
							<Columns>
								<asp:BoundColumn DataField="Week" HeaderText="Week"></asp:BoundColumn>
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
				<%if( Request.QueryString["bExcel"] != "1" ){%>
				<A class="ClearTextBox" id="A1" href="/Sales/Sales.aspx"><IMG alt="Back to Sales" src="../Images/L_arrow.gif" border="0">
					Back to Sales</A>
				<%}%>
			</center>
		</form>
		<script language="C#" runat="server">
string connString = ConfigurationSettings.AppSettings["DSN"];
SqlConnection conn = new SqlConnection();
DataSet ds = new DataSet();
string strThisSeason;
int dMonth;
int nYear;
string dYear;
string strPCode;
int TotalUnits, TotalUnits_PY, TotalUnits_2Yr;
Decimal TotalSales, TotalSales_PY, TotalSales_2Yr;
int SubsCur, SubsLastYr, SubDiff = 0;

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
	strPCode = (string)Request.QueryString["RequestID"];
	
	ProductLabel.Text = "Title: <font color=red>" + Request.QueryString["Name"] + "&nbsp;&nbsp;&nbsp;  </font>Alpha Code: <font color=red>" +strPCode+"</font>";
		
	if( Request.QueryString["bExcel"] == "1" )
	{
		Response.ContentType = "application/vnd.ms-excel";
		Response.Charset = ""; //Remove the charset from the Content-Type header.
		Page.EnableViewState = false; //Turn off the view state.

		System.IO.StringWriter tw = new System.IO.StringWriter();
		System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);

		grdCurYr.RenderControl(hw);     //Get the HTML for the control.
		Response.Write(tw.ToString()); //Write the HTML back to the browser.
		Response.Flush();         //End the response.
	} 	
	
	if (!IsPostBack) 
	{
		string sql  = @"SELECT MYear FROM Years ORDER BY MYear DESC";
		string sql2 = @"SELECT Season FROM Seasons ORDER BY Season";
		
		SqlCommand cmd  = new SqlCommand(sql,  conn);
		SqlCommand cmd2 = new SqlCommand(sql2, conn);
		
		conn.Open();
		SqlDataReader readerYr = cmd.ExecuteReader();

		// bind the readers to the DropDownLists
		MagYear.DataSource = readerYr;
		MagYear.DataBind();

		// close the reader 
		readerYr.Close();

		SqlDataReader readerSeason = cmd2.ExecuteReader(); 

		MagSeason.DataSource = readerSeason;
		MagSeason.DataBind();

		// close the second reader
		readerSeason.Close();

		conn.Close();
		nYear = Int32.Parse(MagYear.SelectedItem.Value.Trim());
		
		BindGrid();
	} 
}
  
void BindGrid()
{
	YearLabel.Text		= strThisSeason + "&nbsp;" + Convert.ToString(nYear);
	YearLabel_PY.Text	= strThisSeason + "&nbsp;" + Convert.ToString((nYear-1));
	YearLabel_2Yr.Text	= strThisSeason + "&nbsp;" + Convert.ToString((nYear-2));
	
	conn.Open();

	SqlCommand cmd = new SqlCommand("AlphaSalesReport", conn);
	cmd.CommandTimeout = 240;

	SqlDataAdapter da = new SqlDataAdapter();
	da.SelectCommand = cmd;

	da.SelectCommand.CommandType = CommandType.StoredProcedure;

	da.SelectCommand.Parameters.Add(new SqlParameter("@AlphaCode", SqlDbType.VarChar,6));
	da.SelectCommand.Parameters["@AlphaCode"].Value = strPCode;

	da.SelectCommand.Parameters.Add(new SqlParameter("@MagYear", SqlDbType.Int, 4));
	da.SelectCommand.Parameters["@MagYear"].Value = nYear;

	da.SelectCommand.Parameters.Add(new SqlParameter("@MagSeason", SqlDbType.NVarChar, 6));
	da.SelectCommand.Parameters["@MagSeason"].Value = MagSeason.SelectedItem.Value.Trim();

	//fill the dataset and retrieve the default view
	da.Fill(ds, "AlphaSales");
	DataView dv = ds.Tables["AlphaSales"].DefaultView;

	grdCurYr.DataSource = dv;
	grdCurYr.DataBind();

	SqlCommand cmd2 = new SqlCommand("AlphaSalesReport", conn);
	cmd2.CommandTimeout = 240;

	SqlDataAdapter da2 = new SqlDataAdapter();
	da2.SelectCommand = cmd2;

	da2.SelectCommand.CommandType = CommandType.StoredProcedure;

	da2.SelectCommand.Parameters.Add(new SqlParameter("@AlphaCode", SqlDbType.VarChar,6));
	da2.SelectCommand.Parameters["@AlphaCode"].Value = strPCode;

	da2.SelectCommand.Parameters.Add(new SqlParameter("@MagYear", SqlDbType.Int, 4));
	da2.SelectCommand.Parameters["@MagYear"].Value = (nYear-1);

	da2.SelectCommand.Parameters.Add(new SqlParameter("@MagSeason", SqlDbType.NVarChar, 6));
	da2.SelectCommand.Parameters["@MagSeason"].Value = MagSeason.SelectedItem.Value.Trim();

	//fill the dataset and retrieve the default view
	da2.Fill(ds, "AlphaSales_PY");
	DataView dv2 = ds.Tables["AlphaSales_PY"].DefaultView;
	
	grdPriorYr.DataSource = dv2;
	grdPriorYr.DataBind();

	SqlCommand cmd3 = new SqlCommand("AlphaSalesReport", conn);
	cmd3.CommandTimeout = 240;

	SqlDataAdapter da3 = new SqlDataAdapter();
	da3.SelectCommand = cmd3;

	da3.SelectCommand.CommandType = CommandType.StoredProcedure;

	da3.SelectCommand.Parameters.Add(new SqlParameter("@AlphaCode", SqlDbType.VarChar,6));
	da3.SelectCommand.Parameters["@AlphaCode"].Value = strPCode;

	da3.SelectCommand.Parameters.Add(new SqlParameter("@MagYear", SqlDbType.Int, 4));
	da3.SelectCommand.Parameters["@MagYear"].Value = (nYear-2);

	da3.SelectCommand.Parameters.Add(new SqlParameter("@MagSeason", SqlDbType.NVarChar, 6));
	da3.SelectCommand.Parameters["@MagSeason"].Value = MagSeason.SelectedItem.Value.Trim();

	//fill the dataset and retrieve the default view
	da3.Fill(ds, "AlphaSales_2Yr");
	DataView dv3 = ds.Tables["AlphaSales_2Yr"].DefaultView;
	
	grdTwoAgo.DataSource = dv3;
	grdTwoAgo.DataBind();

	if (conn != null) 
	{
		if (conn.State == ConnectionState.Open) 
			{conn.Close();}
		conn.Dispose();
	}
    
    if (cmd != null) 
	{ cmd.Dispose(); }
	if (cmd2 != null) 
	{ cmd2.Dispose(); }
	if (cmd3 != null) 
	{ cmd3.Dispose(); }
	
	if (da != null) 
	{ da.Dispose();  }		
	if (da2 != null) 
	{ da2.Dispose();  }	
	if (da3 != null) 
	{ da3.Dispose();  }	
	
}//Bind_Grid()				

void ButtonClick(Object sender, EventArgs e) 
{
	strThisSeason = (string)MagSeason.SelectedItem.Value.Trim();
	nYear = Int32.Parse(MagYear.SelectedItem.Value.Trim());
	BindGrid();
}
  		
void CalcTotalUnits (string TotalSubs)
{
	TotalUnits += Int32.Parse(TotalSubs);
}
  
void CalcTotalSales (string TtlSales)
{
	TotalSales += Decimal.Parse(TtlSales);
}

void CalcTotalUnits_PY (string TotalSubs)
{
	TotalUnits_PY += Int32.Parse(TotalSubs);
}
  
void CalcTotalSales_PY (string TtlSales)
{
	TotalSales_PY += Decimal.Parse(TtlSales);
}

void CalcTotalUnits_2Yr (string TotalSubs)
{
	TotalUnits_2Yr += Int32.Parse(TotalSubs);
}
  
void CalcTotalSales_2Yr (string TtlSales)
{
	TotalSales_2Yr += Decimal.Parse(TtlSales);
}
  
void grdCurYr_ItemDataBound(Object sender, DataGridItemEventArgs e)

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


void grdPriorYr_ItemDataBound(Object sender, DataGridItemEventArgs e)

{
	if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
	{
		e.Item.Cells[0].Text = string.Format("{0:MM/dd/yy}", Convert.ToDateTime(e.Item.Cells[0].Text));
		CalcTotalUnits_PY ( e.Item.Cells[1].Text );
		e.Item.Cells[1].Text = string.Format("{0:#,###}", Convert.ToDouble(e.Item.Cells[1].Text));
		CalcTotalSales_PY ( e.Item.Cells[2].Text );
		e.Item.Cells[2].Text = string.Format("{0:c}", Convert.ToDouble(e.Item.Cells[2].Text));

	}
	else if(e.Item.ItemType == ListItemType.Footer )
	{
		e.Item.Cells[1].Text= string.Format("{0:#,###}", TotalUnits_PY);
		e.Item.Cells[2].Text= string.Format("{0:c}", TotalSales_PY);
	} 

}

void grdTwoAgo_ItemDataBound(Object sender, DataGridItemEventArgs e)
{
	if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
	{
		e.Item.Cells[0].Text = string.Format("{0:MM/dd/yy}", Convert.ToDateTime(e.Item.Cells[0].Text));
		CalcTotalUnits_2Yr ( e.Item.Cells[1].Text );
		e.Item.Cells[1].Text = string.Format("{0:#,###}", Convert.ToDouble(e.Item.Cells[1].Text));
		CalcTotalSales_2Yr ( e.Item.Cells[2].Text );
		e.Item.Cells[2].Text = string.Format("{0:c}", Convert.ToDouble(e.Item.Cells[2].Text));

	}
	else if(e.Item.ItemType == ListItemType.Footer )
	{
		e.Item.Cells[1].Text= string.Format("{0:#,###}", TotalUnits_2Yr);
		e.Item.Cells[2].Text= string.Format("{0:c}", TotalSales_2Yr);
	} 

}

		</script>
	</body>
</html>
