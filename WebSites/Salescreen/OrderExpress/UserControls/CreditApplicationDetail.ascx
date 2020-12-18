<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.CreditApplicationDetail" Codebehind="CreditApplicationDetail.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="CreditApplicationForm" Src="CreditApplicationForm.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="Header.ascx" %>

<table border="0" cellpadding="0" cellspacing="0" width=100%>
	<tr>
		<td>
			<uc1:CreditApplicationForm id="CreditAppForm" runat="server"></uc1:CreditApplicationForm>
			<br>
		</td>
	</tr>
	<tr>
		<td align="center">
			<table border="0" cellpadding="0" cellspacing="0" width="400">
				<tr>
					<td align="center">
						<asp:ImageButton id="imgBtnSave" runat="server" CausesValidation="False" ImageUrl="~/images/btnSave.gif"
							AlternateText="Save"></asp:ImageButton>
					</td>
					<td align="center">
						<asp:HyperLink id="hypLnkCancel" runat="server" ImageUrl="~/images/btnCancel.gif" NavigateUrl="javascript:window.close();">Cancel</asp:HyperLink>
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table>
