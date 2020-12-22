<%@ Register TagPrefix="uc1" TagName="AccountList_Add" Src="AccountList_Add.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.ProgramAgreementStep_Search" Codebehind="ProgramAgreementStep_Search.ascx.cs" %>

<table id="Table1222" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td><asp:label id="Label5" runat="server" CssClass="StandardLabel" Visible="False">
				1 - Select the account for which you want to enter a program agreement :
				<br>
				<br>
			</asp:label>
		</td>
	</tr>
	<tr>
		<td align="left" colSpan="6"> <!--Section Body --><uc1:AccountList_Add AddButton_ImageUrl="~/images/btnAddPA.gif" id="AccountList_AddProgStep" runat="server"></uc1:AccountList_Add></td>
	</tr>
	<tr>
		<td align="center"><br>
			<asp:hyperlink id="hypLnkNewAccount" runat="server" CssClass="StandardLabel" ForeColor="#993300"
				NavigateUrl="javascript:void(0);">Click here if you cannot find your account in this list.</asp:hyperlink><br>
		</td>
	</tr>
</table>
