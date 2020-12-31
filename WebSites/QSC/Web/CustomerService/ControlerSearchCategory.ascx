<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ControlerSearchCategory.ascx.cs" Inherits="QSPFulfillment.CustomerService.ControlerSearchCategory" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table>
	<tr>
		<td>
			<asp:Label id="Label1" runat="server" CssClass="csPlainText" Font-Bold="True">Subscription Category:</asp:Label>
		</td>
	</tr>
	<tr>
		<td>
			<cc1:DropDownListSearch runat="server" id="ddlSearchCategory" ParameterName="SearchCategory">
				<asp:ListItem Value="0" Selected="True">Approved Magazines</asp:ListItem>
				<asp:ListItem Value="1">Non Approved Magazines</asp:ListItem>
				<asp:ListItem Value="2">Other Products</asp:ListItem>
			</cc1:DropDownListSearch>
		</td>
	</tr>
</table>
