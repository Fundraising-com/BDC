<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CommonWeb" Assembly="QSPFulfillment"  %>
<%@ Page language="c#" Codebehind="SearchMagItemsSummaryReport.aspx.cs" AutoEventWireup="false" Inherits="QSPFulfillment.Finance.SearchMagItemsSummaryReport" %>
<html>
	<head>
		<title>CA Fulfill System - Magazine Items Summary Report</title>
		<link rel="stylesheet" href="../Includes/MagSysStyle.css" type="text/css">
	</head>
	<body topmargin="0" leftmargin="0">
		<form method="post" runat="server" id="StatementForm">
			<!-- #include file="../Includes/Menu.inc" -->
			<br>
			<center><h3><font face="Verdana" color="#2f4f88">Magazine Items Summary Report</font></h3>
			</center>
			<p></p>
			<table border="0" cellspacing="0" cellpadding="0" width="100%" align="center">
				<tr>
					<td align="center">
						<b><font face="Verdana" size="2" color="#2f4f88">Search</font></b>&nbsp;
						<asp:textbox id="Search" runat="server" cssclass="boxlook" />
						&nbsp;<b><font face="Verdana" size="2" color="#2f4f88">By</font></b>&nbsp;
						<asp:dropdownlist id="ddlStatus" cssclass="boxlookW" runat="server">
							<asp:listitem text="Order ID" value="OrderID" />
							<asp:listitem text="Campaign ID" value="CampaignID" />
						</asp:dropdownlist>
					</td>
					<td>
						<b><font face="Verdana" size="2" color="#2f4f88">From:</font></b>&nbsp;
						<asp:textbox width="90" maxlength="11" id="FromDate" runat="server" cssclass="boxlook" />&nbsp;
						<b><font face="Verdana" size="2" color="#2f4f88">To:</font></b>&nbsp;
						<asp:textbox width="90" maxlength="11" id="ToDate" runat="server" cssclass="boxlook" />&nbsp; 
						&nbsp;&nbsp;<asp:linkbutton causesvalidation="False" id="BtnSearch" runat="server" cssclass="boxlook" onclick="SearchButtonClick"
							text="<font face='Verdana' color='#2f4f88'> Go </font>" />
					</td>
					<td>
						<asp:label runat="server" id="lblSummary" cssclass="ClearTextBoxG" />
					</td>
				</tr>
			</table>
			<table border="0" cellspacing="0" cellpadding="2" width="100%" align="center">
				<tr>
					<td>
						<asp:datagrid onitemdatabound="MagItemsSummaryReportDG_ItemDataBound" id="MagItemsSummaryReportDG"
							headerstyle-font-bold="True" allowsorting="true" onsortcommand="MagItemsSummaryReportDG_Sort"
							allowpaging="True" pagesize="10" pagerstyle-position="Bottom" pagerstyle-mode="NumericPages"
							pagerstyle-horizontalalign="Center" pagerstyle-pagebuttoncount="20" pagerstyle-width="100%"
							pagerstyle-backcolor="#2f4f88" pagerstyle-forecolor="white" onpageindexchanged="MagItemsSummaryReportDG_Page"
							backcolor="#CCCCCC" runat="server" datakeyfield="OrderId" autogeneratecolumns="False" width="100%"
							bordercolor="black" borderwidth="1" gridlines="Both" cellpadding="2" cellspacing="0" font-name="Verdana"
							font-size="8pt" headerstyle-backcolor="#2f4f88">
							<columns>
								<asp:templatecolumn headerstyle-verticalalign="Top" itemstyle-verticalalign="Top" headertext="Order ID"
									sortexpression="OrderID" headerstyle-wrap="False">
									<itemtemplate>
										<asp:Literal ID="ltOrderID" Visible=false Text='<%# DataBinder.Eval(Container.DataItem, "OrderID") %>' Runat="Server" />
										<asp:Literal ID="ltCampaignID" Visible=false Text='<%# DataBinder.Eval(Container.DataItem, "CampaignID") %>' Runat="Server" />
										<asp:Literal ID="ltLang" Visible=false Text='<%# DataBinder.Eval(Container.DataItem, "Lang") %>' Runat="Server" />
										<cc2:RSGenerationLinkButton id="rsGenerationMagazineItemsSummary" runat="server" CausesValidation="false" font-underline="True" forecolor="RoyalBlue" />
									</itemtemplate>
								</asp:templatecolumn>
								<asp:templatecolumn headerstyle-verticalalign="Top" itemstyle-verticalalign="Top" headertext="Campaign ID"
									sortexpression="CampaignID" headerstyle-wrap="False">
									<itemtemplate>
										<%# DataBinder.Eval(Container.DataItem, "CampaignID") %>
									</itemtemplate>
								</asp:templatecolumn>
								<asp:templatecolumn headerstyle-verticalalign="Top" itemstyle-verticalalign="Top" headertext="Account Name"
									sortexpression="Name" headerstyle-wrap="False">
									<itemtemplate>
										<%# DataBinder.Eval(Container.DataItem, "Name") %>
									</itemtemplate>
								</asp:templatecolumn>
								<asp:templatecolumn headerstyle-verticalalign="Top" itemstyle-verticalalign="Top" headertext="FMID"
									sortexpression="FMID" headerstyle-wrap="False">
									<itemtemplate>
										<%# DataBinder.Eval(Container.DataItem, "FMID") %>
									</itemtemplate>
								</asp:templatecolumn>
								<asp:templatecolumn headerstyle-verticalalign="Top" itemstyle-verticalalign="Top" headertext="FM Name"
									sortexpression="LastName" headerstyle-wrap="False">
									<itemtemplate>
										<%# DataBinder.Eval(Container.DataItem, "LastName") %>
									</itemtemplate>
								</asp:templatecolumn>
							</columns>
						</asp:datagrid>
					</td>
				</tr>
			</table>
			<br>
			<asp:label runat="server" id="LabelMsg" cssclass="ClearTextBoxR" />
			<center></center>
		</form>
		<!-- #Include File="../Includes/Footer.inc" -->
	</body>
</html>
