<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Register TagPrefix="uc1" TagName="SearchModule" Src="SearchModule.ascx" %>
<%@ Page language="c#" Codebehind="TotalRemitBatch.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.OrderMgt.TotalRemitBatch" %>
<%@ Register TagPrefix="dbwc" Namespace="DBauer.Web.UI.WebControls" Assembly="DBauer.Web.UI.WebControls.HierarGrid" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>Orders</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Includes/QSPFulfillment.css" type="text/css" rel="stylesheet">
  </HEAD>
	<body topmargin=0 bottommargin=0 leftmargin=0 rightmargin=0>
		<form id="Orders" method="post" runat="server">
			<!-- #include file="../Includes/Menu.inc" -->
			<center>
					<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="95%" border="0">
  <TR>
    <TD>
    <br>
    <h3><font face="Verdana" color="#2f4f88">Remit Batches</font></h3>

    <TABLE id="Table3" cellspacing="0" width="100%" cellpadding="0" bgcolor="#cecece" border="0">
										<TR>
											<TD>
												<TABLE id="Table4" height="100%" width="100%" cellspacing="1" cellpadding="2">
													<TR>
														<TD valign="top" height="20">
															<font class="CSTitle">Search</font>
														</TD>
													</TR>
													<TR bgcolor="#ffffff">
														<TD valign="top">
															<uc1:SearchModule id=ctrlSearchModule runat="server"></uc1:SearchModule>
														</TD>
													</TR>
												</TABLE>
											</TD>
										</TR>
									</TABLE>
									<br>
</TD></TR>
						<TR>
							
							<TD>
								<P><DBWC:HIERARGRID id="higMainM" runat="server"
										 Width="100%" TemplateCachingBase="Tablename" LoadControlMode="UserControl"
										TemplateDataMode="Table" RowExpanded="DBauer.Web.UI.WebControls.RowStates" AutoGenerateColumns="False"
										AllowPaging="True" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
		BackColor="White" CellPadding="3" GridLines="Vertical">
<PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="#999999" CssClass="CSPager" Mode="NumericPages">
</PagerStyle>

<AlternatingItemStyle CssClass="CSSearchResult" BackColor="Gainsboro">
</AlternatingItemStyle>

<FooterStyle ForeColor="Black" CssClass="CSSearchResult" BackColor="#CCCCCC">
</FooterStyle>

<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C">
</SelectedItemStyle>

<ItemStyle ForeColor="Black" CssClass="CSSearchResult" BackColor="#EEEEEE">
</ItemStyle>

<HeaderStyle Font-Bold="True" ForeColor="White" CssClass="CSSearchResult" BackColor="#000084">
</HeaderStyle>

<Columns>
<asp:BoundColumn DataField="Date" ReadOnly="True" HeaderText="Date" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
<asp:BoundColumn DataField="RemitTotal" ReadOnly="True" HeaderText="Remit Total">
<ItemStyle HorizontalAlign="Right">
</ItemStyle>
</asp:BoundColumn>
<asp:BoundColumn DataField="TotalBasePrice" ReadOnly="True" HeaderText="Total Base Price" DataFormatString="{0:c}">
<ItemStyle HorizontalAlign="Right">
</ItemStyle>
</asp:BoundColumn>
<asp:BoundColumn DataField="TotalCatalogPrice" ReadOnly="True" HeaderText="Total Catalog Price" DataFormatString="{0:c}" >
<ItemStyle HorizontalAlign="Right">
</ItemStyle>
</asp:BoundColumn>
<asp:BoundColumn DataField="TotalItemPrice" HeaderText="Total Item Price" DataFormatString="{0:c}" >
<ItemStyle HorizontalAlign="Right">
</ItemStyle>
</asp:BoundColumn>
<asp:BoundColumn DataField="TotalUnits" HeaderText="Total Units">
<ItemStyle HorizontalAlign="Right">
</ItemStyle>
</asp:BoundColumn>
</Columns>
									</DBWC:HIERARGRID></P>
							</TD>
						</TR>
					</TABLE>
			
			<!-- #Include File="../Includes/Footer.inc" -->
			</center></form>
	</body>
</HTML>
