<%@ Register TagPrefix="uc4" TagName="ResultHeaderCreditCard" Src="ResultHeaderCreditCard.ascx" %>
<%@ Register TagPrefix="uc3" TagName="ResultSubscription" Src="ResultSubscription.ascx" %>
<%@ Register TagPrefix="uc2" TagName="ResultShipment" Src="ResultShipment.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ResultOrder" Src="ResultOrder.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="MainSearchResult.ascx.cs" Inherits="QSPFulfillment.CustomerService.MainSearchResult" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table width="100%">
	<tr>
		<td>
			<uc1:ResultOrder id="ctrlResultOrder" runat="server"></uc1:ResultOrder>
			<uc3:ResultSubscription id="ctrlResultSubscription" runat="server"></uc3:ResultSubscription>
			<uc2:ResultShipment id="ctrlResultShipment" runat="server" DESIGNTIMEDRAGDROP="6"></uc2:ResultShipment>
			<uc4:ResultHeaderCreditCard id="ctrlResultHeaderCreditCard" runat="server"></uc4:ResultHeaderCreditCard>
		</td>
	</tr>
	<tr>
		<td align="right">
			<asp:Button visible="false" id="btnSelectMultipleSubscription" runat="server" Text="Select"></asp:Button>
		</td>
	</tr>
</table>
