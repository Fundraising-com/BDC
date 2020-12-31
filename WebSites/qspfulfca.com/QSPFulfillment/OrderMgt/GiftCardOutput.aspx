<%@ Page language="c#" Codebehind="GiftCardOutput.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.OrderMgt.GiftCardOutput" %>
<%@ Register TagPrefix="uc1" TagName="SearchModule" Src="SearchModule.ascx" %>
<%@ Register TagPrefix="dbwc" Namespace="DBauer.Web.UI.WebControls" Assembly="DBauer.Web.UI.WebControls.HierarGrid" %>
<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
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
    <h3><font face="Verdana" color="#2f4f88">Gift Card Information</font></h3>

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
							<uc1:SearchModule id="ctrlSearchModule" runat="server"></uc1:SearchModule></TD></TR>
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
					
					
							<p><DBWC:HierarGrid id="higMainM" runat="server" RowExpanded="DBauer.Web.UI.WebControls.RowStates" TemplateDataMode="Table"
								LoadControlMode="UserControl" TemplateCachingBase="Tablename"  Width="100%" AutoGenerateColumns="False"  AllowPaging="True"
								BorderStyle="None" BorderWidth="1px"
		BackColor="White" CellPadding="3" GridLines="Vertical">
		<AlternatingItemStyle BackColor="#DCDCDC" cssClass="CSSearchResult"></AlternatingItemStyle>
		<ItemStyle ForeColor="Black" BackColor="#EEEEEE" cssClass="CSSearchResult"></ItemStyle>
		<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"  cssClass="CSSearchResult"></HeaderStyle>
		<FooterStyle ForeColor="Black"  cssClass="CSSearchResult" BackColor="#CCCCCC"></FooterStyle>
		<PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="#999999" Mode="NumericPages" cssClass="CSPager"></PagerStyle>


<Columns>
<asp:BoundColumn DataField="Date" HeaderText="Date" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
<asp:BoundColumn DataField="ID" HeaderText="ID"></asp:BoundColumn>
<asp:BoundColumn DataField="FileName" HeaderText="File Name"></asp:BoundColumn>
<asp:BoundColumn DataField="TotalRemitBatches" HeaderText="Total Remit Batches">
<ItemStyle HorizontalAlign="Right">
</ItemStyle>
</asp:BoundColumn>
</Columns>
							</DBWC:HierarGrid>
							</P>
							</TD>
						</TR>
					</TABLE>
							
			<!-- #Include File="../Includes/Footer.inc" -->
		</form>
	</body>
</HTML>
