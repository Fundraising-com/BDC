<%@ Page language="c#" Codebehind="MagazineDetailedReport.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.Reports.MagazineDetailedReport" %>
<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Magazine Detailed Report</title>
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
					<td colspan="2" align="center">
						<asp:Label Font-Size="Large" Runat="server" id="lblHead">Magazine Subscriber Detailed Report</asp:Label>
					</td>
				</tr>
				<tr>
					<td>Publisher Name:</td>
					<td>
						<asp:DropDownList ID="ddlPubName" Runat="server" Width=300></asp:DropDownList>
					</td>
				</tr>
				<tr>
					<td>Fulfillment House Name:</td>
					<td>
						<asp:DropDownList ID="ddlFulfHouseName" Runat="server" Width=300></asp:DropDownList>
					</td>
				</tr>
				<tr>
					<td>From Remit Batch Number:</td>
					<td>
						<asp:DropDownList ID="ddlFromRemitNum" Runat="server" Width=300></asp:DropDownList>
					</td>
				</tr>
				<tr>
					<td>To Remit Batch Number:</td>
					<td>
						<asp:DropDownList ID="ddlToRemitNum" Runat="server" Width=300></asp:DropDownList>
					</td>
				</tr>
				<tr>
					<td>Interface Media:</td>
					<td>
						<asp:DropDownList ID="ddlInterfaceMedia" Runat="server" Width=300></asp:DropDownList>
					</td>
				</tr>
			</table>
			<p align="center">
				<asp:Button id="pbPreview" Text="Preview" Runat="server" />
				<asp:Button ID="pbPrint" Text="Print" Runat="server" />
			</p>
		</form>
	</body>
</HTML>
