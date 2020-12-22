<%@ Reference Control="ProductInfo.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.ProductDetailInfo" Codebehind="ProductDetailInfo.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="ProductInfo" Src="ProductInfo.ascx" %>

<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD>
			<uc1:ProductInfo id="ProductInfo" runat="server"></uc1:ProductInfo>
			<br>
		</TD>
	</TR>
	<TR>
		<td align="center">
			<table border="0" cellpadding="0" cellspacing="0">
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
	</TR>
</TABLE>
