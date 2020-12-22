<%@ Control Language="c#" AutoEventWireup="True" Codebehind="Status.ascx.cs" Inherits="EFundraisingCRMWeb.Components.User.Sales.Status" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Reference VirtualPath="~/Components/User/Sales/SaleInfoList.ascx" %>


<table cellSpacing="0" cellPadding="1" width="280" style="WIDTH: 280px; HEIGHT: 96px">
	<tr vAlign="top" height="20">
		<td class="NormalText" width="121" style="WIDTH: 121px">Sale Status</td>
		<TD vAlign="top" style="WIDTH: 29px"></TD>
		<td vAlign="top"><asp:DropDownList ID="saleStatusDropDownList" CssClass="NormalText" Runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList></td>
	</tr>
	<tr vAlign="top" height="20">
		<td class="NormalText" style="WIDTH: 121px">
			Production Status
		</td>
		<TD vAlign="top" style="WIDTH: 29px"></TD>
		<td vAlign="top"><asp:DropDownList ID="productStatusDropdownlist" CssClass="NormalText" Runat="server"></asp:DropDownList></td>
	</tr>
	<tr vAlign="top" height="20">
		<td class="NormalText" style="WIDTH: 121px">
			AR Status
		</td>
		<TD vAlign="top" style="WIDTH: 29px"></TD>
		<td vAlign="top"><asp:DropDownList ID="arStatusDropdownlist" CssClass="NormalText" Runat="server"></asp:DropDownList></td>
	</tr>
	<tr vAlign="top" height="20">
		<td class="NormalText" style="WIDTH: 121px">
			Credit Status
		</td>
		<TD vAlign="top" style="WIDTH: 29px"></TD>
		<td vAlign="top"><asp:DropDownList ID="creditStatusDropdownlist" CssClass="NormalText" Runat="server"></asp:DropDownList></td>
	</tr>
</table>
