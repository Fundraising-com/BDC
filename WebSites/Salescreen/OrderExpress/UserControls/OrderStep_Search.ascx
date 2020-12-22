<%@ Register TagPrefix="uc1" TagName="AccountList_AddOrder" Src="AccountList_AddOrder.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.OrderStep_Search" Codebehind="OrderStep_Search.ascx.cs" %>

<table id="Table1222" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td><asp:label id="Label5" runat="server" CssClass="StandardLabel" Visible="False">
				1 - Select the account for which you want to order :
				<br>
				<br>
			</asp:label>
		</td>
	</tr>
	<tr>
		<td align="left" colSpan="6"> <!--Section Body --><uc1:AccountList_AddOrder id="AccountList_AddOrderStep" runat="server"></uc1:AccountList_AddOrder></td>
	</tr>
	<tr>
		<td align="center"><br>
			<asp:hyperlink id="hypLnkNewAccount" runat="server" CssClass="StandardLabel" ForeColor="#993300"
				NavigateUrl="javascript:void(0);">Click here if you cannot find your account in this list.</asp:hyperlink><br>
		</td>
	</tr>
</table>
