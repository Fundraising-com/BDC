<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.CreditApplicationDetailInfo" Codebehind="CreditApplicationDetailInfo.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="CreditApplicationInfo" Src="CreditApplicationInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="Header.ascx" %>

<table border="0" cellpadding="0" cellspacing="0" width=100%>
	<tr>
		<td>
			<uc1:CreditApplicationInfo id="CreditAppInfo" runat="server"></uc1:CreditApplicationInfo>
			<br>
		</td>
	</tr>
	<tr>
		<td align="center">
			<table border="0" cellpadding="0" cellspacing="0" width="400">
				<tr>
					<td align="center">
						<asp:ImageButton id="imgBtnEdit" runat="server" CausesValidation="False" ImageUrl="~/images/btnEdit.gif"
							AlternateText="Edit"></asp:ImageButton>
					</td>
					<td>
						&nbsp;&nbsp;
					</td>
					<td align="center">
						<asp:HyperLink id="hypLnkClose" runat="server" ImageUrl="~/images/btnClose.gif" NavigateUrl="javascript:window.close();">Cancel</asp:HyperLink>
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table>
