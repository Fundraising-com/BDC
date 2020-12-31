<%@ Page language="c#" Codebehind="ThankYouLetter.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.Reports.ThankYouLetter" %>
<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Register TagPrefix="UC" TagName="Date" Src="../Common/DateEntry.ascx" %>
<%@ Register TagPrefix="UC" TagName="FMddl" Src="../Common/FieldManagerDDL.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Thank You Letter</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body leftMargin="0" topMargin="0">
		<form id="Form1" method="post" runat="server">
			<!-- #include file="../Includes/Menu.inc" -->
			<table align="center" style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none">
				<colgroup span="1" align="right">
				</colgroup>
				<colgroup span="1" align="left">
				</colgroup>
				<tr style="PADDING-BOTTOM: 20px">
					<td></td>
					<td>
						<asp:Label Font-Size="Large" Runat="server" id="lblHead">Thank You Letter</asp:Label>
					</td>
				</tr>
				<tr>
					<td>Field Manager:</td>
					<td>
						<UC:FMddl id="ucFMddl" runat="server" />
					</td>
				</tr>
				<tr>
					<td>Campaign Id:</td>
					<td>
						<asp:DropDownList ID="ddlCampaignId" Runat="server" Width=300>
						</asp:DropDownList>
					</td>
				</tr>
				<tr>
					<td>From Approved Status Date:</td>
					<td><UC:Date ID="FromApprovedDate" Runat="server" /></td>
				</tr>
				<tr>
					<td>To Approved Status Date:</td>
					<td><UC:Date ID="ToApprovedDate" Runat="server" /></td>
				</tr>
			</table>
			<p align="center">
				<asp:Button id="pbPreview" Text="Preview" Runat="server" />
				<asp:Button ID="pbPrint" Text="Print" Runat="server" />
			</p>
		</form>
	</body>
</HTML>
