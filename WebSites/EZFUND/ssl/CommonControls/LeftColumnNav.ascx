<%@ Register TagPrefix="uc1" TagName="CartList" Src="../Controls/CartList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CMenubar" Src="../Controls/CMenubar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SimpleSearch" Src="../Controls/SimpleSearch.ascx" %>
<%@ Control Language="vb" EnableViewState = False AutoEventWireup="false" Codebehind="LeftColumnNav.ascx.vb" Inherits="StoreFront.StoreFront.LeftColumnNav" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<table border="0" id="Table1" cellpadding="0" cellspacing="0" width="100%" runat="Server"
	visible="True">
	<tr>
		<td class="LeftColumn">
			<uc1:cmenubar id="CMenubar1" CallPage="left" runat="server"></uc1:cmenubar>
		</td>
	</tr>
</table>
