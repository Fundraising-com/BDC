<%@ Reference Control="OrganizationDetail.ascx" %>
<%@ Register TagPrefix="uc1" TagName="OrganizationInfo" Src="OrganizationInfo.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.OrganizationDetailInfo" Codebehind="OrganizationDetailInfo.ascx.cs" %>

<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<td>
			<uc1:OrganizationInfo id="OrganizationInfo_Org" runat="server"></uc1:OrganizationInfo>
			<br>
		</td>
	</TR>
	<tr>
		<td align="center">
			<table border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td>
						<asp:ImageButton id="imgBtnEdit" runat="server" CausesValidation="False" ImageUrl="~/images/btnEdit.gif"
							AlternateText="Edit"></asp:ImageButton>
					</td>
					<td>
						&nbsp;&nbsp;
					</td>
					<td>
						<asp:HyperLink id="hypLnkClose" runat="server" ImageUrl="~/images/btnClose.gif" NavigateUrl="javascript:window.close();">Cancel</asp:HyperLink>
					</td>
				</tr>
			</table>
		</td>
	</tr>
</TABLE>
