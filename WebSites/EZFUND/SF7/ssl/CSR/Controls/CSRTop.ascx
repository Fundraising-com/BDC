<%@ Control Language="vb" AutoEventWireup="false" Codebehind="CSRTop.ascx.vb" Inherits="StoreFront.StoreFront.CSRTop" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<TABLE class="contentTable" id="tblAddProduct" width="100%" cellSpacing="0" cellPadding="0"
	border="0">

	<TR>
		
		<TD class="TopSubBanner" align="left" noWrap width="20%">&nbsp;Logged in as: <asp:label id="lblCSRName" cssClass="TopSubBanner" Runat="server" Width="20"/> <asp:LinkButton ID="SignOut" cssClass="TopSubBanner" Runat="server"><u>(Signout)</u></asp:LinkButton></TD>
		<td class="TopSubBanner" align="right" width="80%">
			<TABLE width="100%" cellSpacing="0" cellPadding="1" border="0">
				<TR>
					<td align="right"><asp:LinkButton cssClass="TopSubBanner" ID="ClearForm" Runat="server">&nbsp;<u>Clear 
								Form Contents / New Order</u></asp:LinkButton>&nbsp;</td>
				</TR>
			</TABLE>
		</td>
		
	</TR>

</TABLE>
