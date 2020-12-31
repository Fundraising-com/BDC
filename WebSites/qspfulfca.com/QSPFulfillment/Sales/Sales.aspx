<%@ Page language="c#" debug="true" Codebehind="Sales.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.Sales.Sales_mtc" %>
<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="System.Data" %>
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
				<h3><font face="Verdana" color="#2f4f88">Magazine Sales List</font></h3>
			</center>
			<p></p>
			<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
				<tr>
					<td align="right">
						<b><font face="Verdana" size="2" color="#2f4f88">Search</font></b>&nbsp;
						<asp:TextBox ID="Search" Runat="server" CssClass="boxlook" />
						&nbsp;&nbsp;<b><font face="Verdana" size="2" color="#2f4f88">Search By</font></b>&nbsp;
						<asp:DropDownList id="ddlStatus" CssClass="boxlookW" runat="server">
							<asp:ListItem Text="Title" Value="Title" />
							<asp:ListItem Text="Publisher" Value="Publisher" />
						</asp:DropDownList>
						&nbsp;&nbsp;<asp:LinkButton ID="BtnSearch" Runat="server" CssClass="boxlook" OnClick="SearchButtonClick" Text="<font face='Verdana' color='#2f4f88'>Go</font>" />
					</td>
					<td align="right">
						<asp:LinkButton CssClass="ClearTextBox" id="btnFirst" runat="server" Text="<img border=0 alt='Go to First Page' align=center src=../Images/L_arrow.gif> First Page" CommandArgument="0" OnClick="PagerButtonClick" />
						&nbsp;
						<asp:LinkButton CssClass="ClearTextBox" id="btnLast" runat="server" Text="Last Page <img border=0 alt='Go to Last Page' align=center src=../Images/R_arrow.gif>" CommandArgument="last" OnClick="PagerButtonClick" />&nbsp;&nbsp;&nbsp;
					</td>
				</tr>
			</table>
			<table cellSpacing="0" cellPadding="2" width="100%" align="center" border="0">
				<tr>
					<td><asp:datagrid align="center" id="SalesDG" Width="100%" PageSize="15" AllowPaging="True" runat="server" HeaderStyle-BackColor="#2f4f88" Font-Size="8pt" Font-Name="Verdana" CellSpacing="0" CellPadding="2" GridLines="Both" BorderWidth="1" BorderColor="black" AutoGenerateColumns="False" DataKeyField="Product_Code" BackColor="#bfbfbf" OnPageIndexChanged="SalesDG_Page" PagerStyle-ForeColor="white" PagerStyle-BackColor="#2f4f88" PagerStyle-HorizontalAlign="Right" PagerStyle-Mode="NumericPages" HeaderStyle-Font-Bold="True" HeaderStyle-CssClass="Font9BoldWhiteV">
							<Columns>
								<asp:TemplateColumn HeaderText="UMC" HeaderStyle-Wrap="False">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "Product_Code") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Title/Publisher" HeaderStyle-Wrap="False">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "Product_Name") %>
										<br>
										<%# DataBinder.Eval(Container.DataItem, "Pub_Name") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Contact" HeaderStyle-Wrap="False">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "Contact") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Contact Info" HeaderStyle-Wrap="False">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "PContact_Tel") %>
										<br>
										<asp:HyperLink id=EmailPub runat="server" NavigateUrl='<%# "mailto:" + DataBinder.Eval(Container.DataItem, "PContact_Email")%>' Font-Underline="True" ForeColor="RoyalBlue">
											<%# DataBinder.Eval (Container.DataItem,  "PContact_Email") %>
										</asp:HyperLink>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Program Sales<br>By Season">
									<ItemTemplate>
										<asp:HyperLink id=Hyperlink1 runat="server" Text="Program Sales" NavigateUrl='<%# "../Reports/SalesByUMCReport.aspx?PCode=" + DataBinder.Eval( Container.DataItem,"Product_Code" ) + "&Name=" + Server.UrlEncode((string)DataBinder.Eval(Container.DataItem, "Product_Name")) %>' Font-Underline="True" ForeColor="RoyalBlue" Target="_top">
										</asp:HyperLink>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Alpha Sales<br>By Week">
									<ItemTemplate>
										<asp:HyperLink id=HyperLink2 runat="server" Text="Alpha Sales" NavigateUrl='<%# DataBinder.Eval(Container, "DataItem.Product_code", "ProgramSales.aspx?RequestID={0}") + "&Name=" + Server.UrlEncode((string)DataBinder.Eval(Container.DataItem, "Product_Name"))%>' Font-Underline="True" ForeColor="RoyalBlue" Target="_top">
										</asp:HyperLink>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="UMC Sales<br>By Week">
									<ItemTemplate>
										<asp:HyperLink runat="server" Text="UMC Sales" NavigateUrl='<%# DataBinder.Eval(Container, "DataItem.Product_code", "DailySales.aspx?RequestID={0}") + "&Name=" + Server.UrlEncode((string)DataBinder.Eval(Container.DataItem, "Product_Name")) %>' Font-Underline="True" ForeColor="RoyalBlue" Target="_top" ID="Hyperlink3">
										</asp:HyperLink>
									</ItemTemplate>
								</asp:TemplateColumn>
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
string strSearchField = "";

  void Page_Load()
  {
	conn.ConnectionString = connString;
	
	if(Request.QueryString["Search"] != null)
	{
		strSearchField = Request.QueryString["Search"];
	}
	
    if (!IsPostBack) 
	{
	  BindGrid(strSearchField);
    } 
  }
  
  void BindGrid(string strSearchField)
  {
  	conn.Open();

	SqlCommand cmd = new SqlCommand("GetMagazineSalesReport", conn);
	cmd.CommandTimeout = 240;

	SqlDataAdapter da = new SqlDataAdapter();
	da.SelectCommand = cmd;

	da.SelectCommand.CommandType = CommandType.StoredProcedure;
	
	//fill the dataset and retrieve the default view
    da.Fill(ds, "SalesReport");
    DataView dv = ds.Tables["SalesReport"].DefaultView;
    //Check the search status
    if(ddlStatus.SelectedItem.Value.Trim()=="Title")
		{
			dv.RowFilter = "Product_Name LIKE '" + Regex.Replace(strSearchField,"'","''") + "%'"; 
			dv.Sort = "Product_Name";
		}
	else
		{
			dv.RowFilter = "Pub_Name LIKE '" + Regex.Replace(strSearchField,"'","''") + "%'"; 
			dv.Sort = "Pub_Name";
		}               
    // bind the reader to the DataList
    SalesDG.DataSource = dv;
    SalesDG.DataBind();
      
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
  
     
	void PagerButtonClick(Object sender, EventArgs e) 
	{
		//used by external paging UI
		String arg = ((LinkButton)sender).CommandArgument;

		switch(arg)
		{
			case ("next"):
				if (SalesDG.CurrentPageIndex < (SalesDG.PageCount - 1))
					SalesDG.CurrentPageIndex ++;
				break;
			case ("prev"):
				if (SalesDG.CurrentPageIndex > 0)
					SalesDG.CurrentPageIndex --;
				break;
			case ("last"):
				SalesDG.CurrentPageIndex = (SalesDG.PageCount - 1);
				break;
			default:
				//page number
				SalesDG.CurrentPageIndex = Convert.ToInt32(arg);
				break;
		}
		strSearchField = (string)Search.Text.Trim();
		BindGrid(strSearchField);
	}

    void SalesDG_Page(Object sender, DataGridPageChangedEventArgs e) 
    {
        //used by built-in pager.  CurrentPageIndex already set
        SalesDG.CurrentPageIndex = e.NewPageIndex;
        strSearchField = (string)Search.Text.Trim();
        BindGrid(strSearchField);
    }
	
	void SearchButtonClick(Object sender, EventArgs e) 
	{
		SalesDG.CurrentPageIndex = 0;
		strSearchField = (string)Search.Text.Trim();
        BindGrid(strSearchField);
    }
    
    
		</script>
	</body>
</html>
