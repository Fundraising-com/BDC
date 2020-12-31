<%@ Register  TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Register TagPrefix="uc1" TagName="DateEntry" Src="../Common/DateEntry.ascx" %>
<%@ Page language="c#" Codebehind="TimeStaffOrderReport.aspx.cs" AutoEventWireup="false" Inherits="QSPFulfillment.Reports.TimeStaffOrderReport" %>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CommonWeb" Assembly="QSPFulfillment"  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>TimeStaffOrderReport</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body topmargin="0" leftmargin="0" marginwidth="0" border="0" rightmargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!-- #include file="../Includes/Menu.inc" -->
			<asp:label id="lblReportName" style="Z-INDEX: 101; LEFT: 48px; POSITION: absolute; TOP: 32px"
				runat="server" width="376px" font-names="Verdana" font-size="Large"> Time's Staff Order Report</asp:label>
			<TABLE id="Table4" style="Z-INDEX: 104; LEFT: 48px; WIDTH: 784px; POSITION: absolute; TOP: 224px; HEIGHT: 24px"
				cellSpacing="1" cellPadding="1" width="784" border="0">
				<TR>
					<TD style="WIDTH: 293px"></TD>
					<TD>
						<asp:label id="lblErrorMessage" runat="server" width="304px" font-names="Verdana" font-size="XX-Small"
							font-bold="True" forecolor="Red" height="8px"></asp:label></TD>
				</TR>
			</TABLE>
			<TABLE id="Table3" style="Z-INDEX: 103; LEFT: 48px; WIDTH: 784px; POSITION: absolute; TOP: 80px; HEIGHT: 32px"
				cellSpacing="1" cellPadding="1" width="784" bgColor="#dcdcdc" border="0">
				<TR>
					<TD>
						<asp:label id="lblReportDetail" runat="server" width="152px" font-names="Verdana" font-size="X-Small"
							font-bold="True">Report Detail</asp:label></TD>
				</TR>
			</TABLE>
			<TABLE id="Table1" style="Z-INDEX: 102; LEFT: 48px; WIDTH: 784px; POSITION: absolute; TOP: 112px; HEIGHT: 117px"
				cellSpacing="1" cellPadding="1" width="784" border="0">
				<TR>
					<TD>
						<asp:label id="lblDateFrom" runat="server" font-names="Verdana" font-size="X-Small" font-bold="True">Order Date From</asp:label></TD>
					<TD style="WIDTH: 177px">
						<uc1:DateEntry id="ucDateFrom" runat="server"></uc1:DateEntry></TD>
					<TD>
						<asp:label id="lblDateTo" runat="server" font-names="Verdana" font-size="X-Small" font-bold="True">Order Date To</asp:label></TD>
					<TD>
						<uc1:DateEntry id="ucDateTo" runat="server"></uc1:DateEntry></TD>
				</TR>
				<TR>
					<TD>
						<asp:label id="lblAccount" runat="server" font-names="Verdana" font-size="X-Small" font-bold="True">Account Id</asp:label></TD>
					<TD style="WIDTH: 177px">
						<asp:TextBox id="tbAccountId" runat="server"></asp:TextBox></TD>
					<TD>
						<asp:label id="lblCamapaign" runat="server" font-names="Verdana" font-size="X-Small" font-bold="True">Campaign</asp:label></TD>
					<TD>
						<asp:TextBox id="tbCampaignId" runat="server"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD>
						<asp:label id="lblSortBy" runat="server" font-names="Verdana" font-size="X-Small" font-bold="True">Sort by</asp:label></TD>
					<TD style="WIDTH: 177px">
						<asp:DropDownList id="ddlSortBy" runat="server" Width="152px">
							<asp:ListItem Value="FM" Selected="True">FM ID</asp:ListItem>
							<asp:ListItem Value="ACCOUNT">Account ID</asp:ListItem>
							<asp:ListItem Value="ACCOUNTNAME">Account Name</asp:ListItem>
						</asp:DropDownList></TD>
					<TD></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD style="WIDTH: 177px"></TD>
					<TD>
						<asp:Button id="PrintButton" runat="server" Text="Print / Preview" Width="110px"></asp:Button></TD>
					<TD></TD>
				</TR>
			</TABLE>
			<cc2:rsgeneration id="rsGenerationTimeStaffOrderReport" runat="server" reportname="TimeStaffOrderReport"
				Width="392px"></cc2:rsgeneration>
		</form>
	</body>
</HTML>
