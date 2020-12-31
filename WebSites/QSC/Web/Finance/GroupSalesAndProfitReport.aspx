<%@ Page language="c#" Codebehind="GroupSalesAndProfitReport.aspx.cs" AutoEventWireup="false" Inherits="QSPFulfillment.Finance.GroupSalesAndProfitReport" %>
<%@ Register TagPrefix="uc1" TagName="DateEntry" Src="../Common/DateEntry.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CommonWeb" Assembly="QSPFulfillment"  %>
<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>GroupSalesAndProfitReport</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body leftMargin="0" topMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!-- #include file="../Includes/Menu.inc" --><asp:label id="lblGroupSalesandProfitReport" style="Z-INDEX: 100; LEFT: 48px; POSITION: absolute; TOP: 40px"
				runat="server" width="368px" font-size="Large" font-names="Verdana">Group Sales and Profit  Report</asp:label>
			<TABLE id="Table3" style="Z-INDEX: 104; LEFT: 48px; WIDTH: 704px; POSITION: absolute; TOP: 88px; HEIGHT: 24px"
				cellSpacing="1" cellPadding="1" width="704" bgColor="gainsboro" border="0">
				<TR>
					<TD><asp:label id="Label4" runat="server" width="232px" font-size="X-Small" font-names="Verdana"
							font-bold="True" height="8px">Report Detail</asp:label></TD>
				</TR>
			</TABLE>
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 48px; WIDTH: 704px; POSITION: absolute; TOP: 112px; HEIGHT: 109px"
				cellSpacing="1" cellPadding="1" width="704" border="0">
				<TR>
					<TD style="WIDTH: 107px"><asp:label id="lblDateFrom" runat="server" Font-Size="X-Small" Font-Bold="True" Font-Names="Verdana"
							Width="104px">Date From</asp:label></TD>
					<TD style="WIDTH: 247px"><uc1:dateentry id="ucDateFrom" runat="server"></uc1:dateentry></TD>
					<TD style="WIDTH: 108px"><asp:label id="lblDateTo" runat="server" Font-Size="X-Small" Font-Bold="True" Font-Names="Verdana"
							Width="80px">Date To</asp:label></TD>
					<TD><uc1:dateentry id="ucDateTo" runat="server"></uc1:dateentry></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 107px"><asp:label id="lblAccountId" runat="server" Font-Size="X-Small" Font-Bold="True" Font-Names="verdana">Group ID</asp:label></TD>
					<TD style="WIDTH: 247px"><asp:textbox id="tbAccountId" runat="server"></asp:textbox></TD>
					<TD style="WIDTH: 108px">
						<asp:label id="lblAccountFM" runat="server" Font-Names="verdana" Font-Bold="True" Font-Size="X-Small">FM</asp:label></TD>
					<TD>
						<asp:Label id="lblLoggedFM" runat="server" Width="192px" Font-Names="Verdana" Font-Bold="True"
							Font-Size="X-Small"></asp:Label></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 107px"></TD>
					<TD style="WIDTH: 247px"></TD>
					<TD style="WIDTH: 108px"><asp:button id="PrintButton" runat="server" Text="Print / Preview"></asp:button></TD>
					<TD></TD>
				</TR>
			</TABLE>
			<TABLE id="Table2" style="Z-INDEX: 103; LEFT: 48px; WIDTH: 704px; POSITION: absolute; TOP: 224px; HEIGHT: 27px"
				cellSpacing="1" cellPadding="1" width="704" border="0">
				<TR>
					<TD style="WIDTH: 41px" align="center"></TD>
					<TD align="center"><asp:label id="lblErrorMessage" runat="server" width="251px" font-size="XX-Small" font-names="Verdana"
							font-bold="True" forecolor="Red"></asp:label></TD>
				</TR>
			</TABLE>
			<cc2:rsgeneration id="rsGenerationGroupSalesProfitReport" runat="server" reportname="GroupSalesProfitReport"></cc2:rsgeneration></form>
	</body>
</HTML>
