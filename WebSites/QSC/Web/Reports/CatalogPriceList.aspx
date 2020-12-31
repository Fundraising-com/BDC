<%@ Page language="c#" Codebehind="CatalogPriceList.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.Reports.CatalogPriceList" %>
<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Catalog Price List</title>
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
						<asp:Label Font-Size="Large" Runat="server" id="lblHead">Catalog Price List</asp:Label>
					</td>
				</tr>
				<tr>
					<td>Catalogue Code:</td>
					<td>
						<asp:DropDownList ID="ddlCatalogueCode" Runat="server" Width=300>
						</asp:DropDownList>
					</td>
				</tr>
				<tr>
					<td>Sort By:</td>
					<td>
						<asp:DropDownList ID="ddlSortBy" Runat="server" Width=300>
						</asp:DropDownList>
					</td>
				</tr>
				<tr>
					<td>Language:</td>
					<td>
						<asp:DropDownList ID="ddlLanguage" Runat="server" Width=300>
						</asp:DropDownList>
					</td>
				</tr>
				<tr>
					<td>Price To Display:</td>
					<td>
						<asp:DropDownList ID="ddlPriceToDisplay" Runat="server" Width=300>
						</asp:DropDownList>
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
