<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.Promo_LogoDetailInfo" Codebehind="Promo_LogoDetailInfo.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="PROMO_LOGOInfo" Src="PROMO_LOGOInfo.ascx" %>

<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD><asp:label id="lblError" Runat="server" ForeColor="Red" Font-Bold="True"></asp:label><br>
			<uc1:PROMO_LOGOInfo id="ctrlPromo_logoInfo" runat="server"></uc1:Promo_logoInfo>
			<br>
		</TD>
	</TR>
	<TR>
		<td align="center">
			<table id="Table1" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td align="center"><asp:imagebutton id="imgBtnEdit" runat="server" AlternateText="Edit" ImageUrl="~/images/btnEdit.gif"
							CausesValidation="False"></asp:imagebutton></td>
					<td>&nbsp;&nbsp;
					</td>
					<td align="center">
						<asp:HyperLink id="hypLnkClose" runat="server" ImageUrl="~/images/btnClose.gif" NavigateUrl="javascript:window.close();">Cancel</asp:HyperLink></td>
				</tr>
			</table>
		</td>
	</TR>
</TABLE>
