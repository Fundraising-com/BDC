<%@ Control Language="vb" EnableViewState = False AutoEventWireup="false" Codebehind="CartList.ascx.vb" Inherits="StoreFront.StoreFront.CartList" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<table border="0" cellpadding="0" cellspacing="0" width="100%">
	<tr>
		<td class="RightColumn" colspan="3">&nbsp;</td>
	</tr>
	<tr>
		<td colspan="3" bgcolor="#000000" height="1"><img src="images/clear.gif" width="1" height="1"></td>
	</tr>
	<tr>
		<td class="RightColumn" colspan="3">&nbsp;</td>
	</tr>
	<tr>
		<td colspan="3" class="RightColumn">&nbsp;<b>Shopping Cart:</b></td>
	</tr>
	<tr>
		<td colspan="3" class="RightColumn">&nbsp;<asp:Label id="lblCount" runat="server"></asp:Label>&nbsp;<asp:Label id="lblItem" runat="server"></asp:Label>&nbsp;In 
			Cart</td>
	</tr>
	<tr>
		<td colspan="3" class="RightColumn">&nbsp;Total:
			<asp:Label id="lblTotal" runat="server"></asp:Label></td>
	</tr>
	<tr>
		<td colspan="3" class="RightColumn">&nbsp;</td>
	</tr>
	<tr>
		<td colspan="3" bgcolor="#000000" height="1"><img src="images/clear.gif" width="1" height="1"></td>
	</tr>
	<tr>
		<td colspan="3" class="RightColumn">&nbsp;</td>
	</tr>
</table>
