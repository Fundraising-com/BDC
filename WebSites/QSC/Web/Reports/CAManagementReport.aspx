<%@ Register TagPrefix="uc1" TagName="FieldManagerDDL" Src="../Common/FieldManagerDDL.ascx" %>
<%@ Page language="c#" Codebehind="CAManagementReport.aspx.cs" AutoEventWireup="false" Inherits="QSPFulfillment.Reports.CAManagementReport" %>
<%@ Register TagPrefix="uc1" TagName="DivisionManagerDDL" Src="../Common/DivisionManagerDDL.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ProgramsDDL" Src="../Common/ProgramsDDL.ascx" %>
<%@ Register TagPrefix="uc1" TagName="DateEntry" Src="../Common/DateEntry.ascx" %>
<%@ Register  TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Register  TagPrefix="cc2" Namespace="QSPFulfillment.CommonWeb" Assembly="QSPFulfillment"  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CAManagementReport</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body ms_positioning="GridLayout" topmargin="0" leftmargin="0" marginwidth="0" marginheight="0"
		border="0" rightmargin="0">
		<form id="Form1" method="post" runat="server">
			<!-- #include file="../Includes/Menu.inc" -->
			<table id="Table3" style="Z-INDEX: 102; LEFT: 48px; WIDTH: 864px; POSITION: absolute; TOP: 80px; HEIGHT: 24px"
				cellspacing="1" cellpadding="1" width="864" bgcolor="gainsboro" border="0">
				<tr>
					<td>
						<asp:label id="Label2" runat="server" font-bold="True" font-size="X-Small" font-names="Verdana"
							width="232px" height="8px">Report Detail</asp:label></td>
				</tr>
			</table>
			<table id="Table4" style="Z-INDEX: 105; LEFT: 48px; WIDTH: 864px; POSITION: absolute; TOP: 256px; HEIGHT: 24px"
				cellspacing="1" cellpadding="1" width="864" border="0">
				<tr>
					<td style="WIDTH: 376px" valign="top" align="center"></td>
					<td valign="top" align="left">
						<asp:label id="lblErrorMessage" runat="server" width="288px" font-names="Verdana" font-size="XX-Small"
							font-bold="True" forecolor="Red" height="16px">Please Correct the error</asp:label></td>
				</tr>
			</table>
			<asp:label id="lblDeadOrderReport" style="Z-INDEX: 104; LEFT: 48px; POSITION: absolute; TOP: 32px"
				runat="server" font-size="Large" font-names="Verdana" width="288px">CA Management Report</asp:label>
			<table id="Table2" style="Z-INDEX: 101; LEFT: 48px; WIDTH: 864px; POSITION: absolute; TOP: 104px; HEIGHT: 152px"
				cellspacing="1" cellpadding="1" width="864" border="0">
				<tr>
					<td><asp:label id="lblCAStartDate" runat="server" width="104px" font-names="Verdana" font-size="X-Small"
							font-bold="True">CA Start Date</asp:label></td>
					<td><uc1:dateentry id="CAStartDateFrom" runat="server"></uc1:dateentry></td>
					<td><asp:label id="lblCAStartDateTo" runat="server" width="96px" font-names="Verdana" font-size="X-Small"
							font-bold="True" forecolor="#404040" enabled="False">CA End Date</asp:label></td>
					<td><uc1:dateentry id="CAStartDateTo" runat="server"></uc1:dateentry></td>
				</tr>
				<tr>
					<td style="HEIGHT: 28px"><asp:label id="lblFM" runat="server" width="104px" font-names="Verdana" font-size="X-Small"
							font-bold="True" forecolor="#404040">Field Manager</asp:label></td>
					<td style="HEIGHT: 28px" valign="middle"><uc1:fieldmanagerddl id="ucFMddl1" runat="server"></uc1:fieldmanagerddl>
						<asp:label id="lblLoggedFMId" runat="server"></asp:label></td>
					<td style="HEIGHT: 28px"><asp:label id="lblProgram" runat="server" width="96px" font-names="Verdana" font-size="X-Small"
							font-bold="True" forecolor="#404040">Program</asp:label></td>
					<td style="HEIGHT: 28px"><uc1:programsddl id="ucProgramsDDL1" runat="server"></uc1:programsddl></td>
				</tr>
				<tr>
					<td><asp:label id="lblCAApprovedFrom" runat="server" font-names="Verdana" font-size="X-Small" font-bold="True"
							forecolor="#404040">CA Approved From</asp:label></td>
					<td><uc1:dateentry id="CAApproveDateFrom" runat="server"></uc1:dateentry></td>
					<td><asp:label id="lblCAApprovedTo" runat="server" width="123px" font-names="Verdana" font-size="X-Small"
							font-bold="True" forecolor="#404040">CA Approved To</asp:label></td>
					<td><uc1:dateentry id="CAApproveDateTo" runat="server"></uc1:dateentry></td>
				</tr>
				<tr>
					<td><asp:label id="lblCAStatus" runat="server" font-names="Verdana" font-size="X-Small" font-bold="True"
							forecolor="#404040">CA Status</asp:label></td>
					<td><asp:dropdownlist id="ddlCAStatus" runat="server" width="161px" datatextfield="description" datavaluefield="instance"
							autopostback="True"></asp:dropdownlist></td>
					<td><asp:label id="lblSortBy" runat="server" width="96px" font-names="Verdana" font-size="X-Small"
							font-bold="True" forecolor="#404040">Sort By</asp:label></td>
					<td><asp:dropdownlist id="ddlSortBy" runat="server" width="128px" autopostback="True">
							<asp:ListItem Value="FM">FM</asp:ListItem>
							<asp:ListItem Value="ACCOUNT">Account</asp:ListItem>
							<asp:ListItem Value="START DATE">CA Start Date</asp:ListItem>
							<asp:ListItem Value="END DATE">CA End Date</asp:ListItem>
							<asp:ListItem Value="ORDER ID">Order Id</asp:ListItem>
							<asp:ListItem Value="DELIVERY DATE">FS Delivery Date</asp:ListItem>
							<asp:ListItem Value="STAFF ORDER">Staff Order</asp:ListItem>
							<asp:ListItem Value="RENEWED CA">Renewed CA</asp:ListItem>
							<asp:ListItem Value="ONLINE ONLY">Online Only</asp:ListItem>
						</asp:dropdownlist></td>
				</tr>
				<tr>
					<td></td>
					<td align="right"></td>
					<td><asp:button id="PrintButton" runat="server" width="152px" text="Print / Preview"></asp:button></td>
					<td></td>
				</tr>
			</table>
			<cc2:rsgeneration id="rsGenerationCAManagementReport" runat="server" reportname="CAManagementReport"></cc2:rsgeneration>
		</form>
	</body>
</HTML>
