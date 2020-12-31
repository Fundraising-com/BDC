<%@ Register TagPrefix="uc1" TagName="DateEntry" Src="../Common/DateEntry.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ProgramsDDL" Src="../Common/ProgramsDDL.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FieldManagerDDL" Src="../Common/FieldManagerDDL.ascx" %>
<%@ Page language="c#" Codebehind="KanataPrizeManagementReport.aspx.cs" AutoEventWireup="false" Inherits="QSPFulfillment.Finance.KanataPrizeManagementReport" %>
<%@ Register  TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CommonWeb" Assembly="QSPFulfillment"  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>KanataPrizeManagementReport</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</head>
	<body leftmargin="0" topmargin="0" rightmargin="0" ms_positioning="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!-- #include file="../Includes/Menu.inc" --><asp:label id="Label1" style="Z-INDEX: 100; LEFT: 48px; POSITION: absolute; TOP: 32px" runat="server"
				width="416px" font-names="Verdana" font-size="Large">Kanata Prize Management Report</asp:label>
			<table id="Table4" style="Z-INDEX: 105; LEFT: 48px; WIDTH: 784px; POSITION: absolute; TOP: 280px; HEIGHT: 24px"
				cellspacing="1" cellpadding="1" width="784" border="0">
				<tr>
					<td style="WIDTH: 340px"></td>
					<td><asp:label id="lblErrorMessage" runat="server" width="304px" font-names="Verdana" font-size="XX-Small"
							font-bold="True" forecolor="Red" height="8px"></asp:label></td>
				</tr>
			</table>
			<table id="Table1" style="Z-INDEX: 101; LEFT: 48px; WIDTH: 784px; POSITION: absolute; TOP: 112px; HEIGHT: 167px"
				cellspacing="1" cellpadding="1" width="784" border="0">
				<tr>
					<td><asp:label id="Label2" runat="server" font-names="Verdana" font-size="X-Small" font-bold="True">Account Id</asp:label></td>
					<td style="WIDTH: 218px"><asp:textbox id="tbAccountId" runat="server" width="128px"></asp:textbox></td>
					<td><asp:label id="Label5" runat="server" font-names="Verdana" font-size="X-Small" font-bold="True">Campaign</asp:label></td>
					<td><asp:textbox id="tbCampaignId" runat="server" width="112px"></asp:textbox></td>
				</tr>
				<tr>
					<td><asp:label id="Label3" runat="server" font-names="Verdana" font-size="X-Small" font-bold="True">Field Manager</asp:label></td>
					<td style="WIDTH: 218px"><uc1:fieldmanagerddl id="ucFMddl2" runat="server"></uc1:fieldmanagerddl></td>
					<td><asp:label id="Label6" runat="server" font-names="Verdana" font-size="X-Small" font-bold="True">Program</asp:label></td>
					<td><uc1:programsddl id="ucProgramsDDL1" runat="server"></uc1:programsddl></td>
				</tr>
				<tr>
					<td><asp:label id="Label4" runat="server" font-names="Verdana" font-size="X-Small" font-bold="True">Order Date From</asp:label></td>
					<td style="WIDTH: 218px"><uc1:dateentry id="ucOrdDateCreatedFrom" runat="server"></uc1:dateentry></td>
					<td><asp:label id="Label7" runat="server" font-names="Verdana" font-size="X-Small" font-bold="True">Order Date To</asp:label></td>
					<td><uc1:dateentry id="ucOrdDateCreatedTo" runat="server"></uc1:dateentry></td>
				</tr>
				<tr>
					<td><asp:label id="Label8" runat="server" font-names="Verdana" font-size="X-Small" font-bold="True">Sort by</asp:label></td>
					<td style="WIDTH: 218px"><asp:dropdownlist id="ddlSortBy" runat="server" width="160px">
							<asp:listitem value="FM">FM ID</asp:listitem>
							<asp:listitem value="ACCOUNT">Account ID</asp:listitem>
							<asp:listitem value="ACCOUNTNAME">Account Name</asp:listitem>
						</asp:dropdownlist></td>
					<td></td>
					<td></td>
				</tr>
				<tr>
					<td></td>
					<td style="WIDTH: 218px"></td>
					<td><asp:button id="PrintButton" runat="server" text="Print /Preview"></asp:button></td>
					<td></td>
				</tr>
			</table>
			<table id="Table3" style="Z-INDEX: 104; LEFT: 48px; WIDTH: 784px; POSITION: absolute; TOP: 80px; HEIGHT: 32px"
				cellspacing="1" cellpadding="1" width="784" bgcolor="#dcdcdc" border="0">
				<tr>
					<td><asp:label id="Label9" runat="server" width="152px" font-names="Verdana" font-size="X-Small"
							font-bold="True">Report Detail</asp:label></td>
				</tr>
			</table>
			<cc2:rsgeneration id="rsGenerationKanataPrizeManagementReport" runat="server" reportname="KanataPrizeManagementReport"></cc2:rsgeneration>
		</form>
	</body>
</html>
