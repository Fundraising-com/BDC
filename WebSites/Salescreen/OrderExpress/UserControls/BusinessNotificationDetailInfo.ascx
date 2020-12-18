<%@ Register TagPrefix="uc1" TagName="BusinessNotificationInfo" Src="BusinessNotificationInfo.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.BusinessNotificationDetailInfo" Codebehind="BusinessNotificationDetailInfo.ascx.cs" %>

<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<td>
			<uc1:BusinessNotificationInfo id="BusinessNotificationInfo_Usr" runat="server" />
			<br>
		</td>
	</TR>
	<tr>
		<td align="center">
			<table border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td>
						<asp:ImageButton ID="imgBtnDelete" runat="server" ImageUrl="~/images/btnDelete.gif" OnClick="imgBtnDelete_Click" />
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
