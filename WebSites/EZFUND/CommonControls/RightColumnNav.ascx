<%@ Register TagPrefix="uc1" TagName="CMenubar" Src="../Controls/CMenubar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CartList" Src="../Controls/CartList.ascx" %>
<%@ Control Language="vb" EnableViewState = False AutoEventWireup="false" Codebehind="RightColumnNav.ascx.vb" Inherits="StoreFront.StoreFront.RightColumnNav" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<%@ Register TagPrefix="uc1" TagName="SimpleSearch" Src="../Controls/SimpleSearch.ascx" %>
<table border="0" id="Table1" cellpadding="0" cellspacing="0" width="100%" runat="Server" visible="True">
	<tr>
		<td class="RightColumn">
			<uc1:CMenubar id="CMenubar1" runat="server"></uc1:CMenubar><br>
			<uc1:SimpleSearch id="SimpleSearch1" runat="server"></uc1:SimpleSearch><br>
			<uc1:CartList id="CartList1" runat="server"></uc1:CartList>
		</td>
	</tr>
</table>
