<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="uc1" TagName="MainSearchResult" Src="MainSearchResult.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ControlerCurrentInfo" Src="ControlerCurrentInfo.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="Main.ascx.cs" Inherits="QSPFulfillment.CustomerService.Main" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="MainShipmentInfo" Src="MainShipmentInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MainSubscriptionInfo" Src="MainSubscriptionInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MainAddressInfo" Src="MainAddressInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MainHistoryInfo" Src="MainHistoryInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MainIncidentActionInfo" Src="MainIncidentActionInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MainOrderInfo" Src="MainOrderInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MainPaymentInfo" Src="MainPaymentInfo.ascx" %>
<TABLE id="Table1" cellSpacing="0" width="100%" cellPadding="0" border="0">
	<TR>
		<TD vAlign="top" align="left" background="images/tab_bg.gif">
			<iewc:tabstrip id="tbsMainPage" runat="server" TargetID="mupMainPage" Height="32px" SelectedIndex="-1">
				<iewc:Tab DefaultImageUrl="images/_searchresult_off_disabled.gif" SelectedImageUrl="images/_searchresult_on.gif"
					ID="iewcSearchResult" Enabled="False"></iewc:Tab>
				<iewc:Tab DefaultImageUrl="images/_addressinfo_off_disabled.gif" SelectedImageUrl="images/_addressinfo_on.gif"
					ID="iewcAddressInformation" Enabled="False"></iewc:Tab>
				<iewc:Tab DefaultImageUrl="images/_subscriptioninfo_off_disabled.gif" SelectedImageUrl="images/_subscriptioninfo_on.gif"
					ID="iewcSubscriptionInfo" Enabled="False"></iewc:Tab>
				<iewc:Tab DefaultImageUrl="images/_orderinfo_off_disabled.gif" SelectedImageUrl="images/_orderinfo_on.gif"
					ID="iewcOrderInformation" Enabled="False"></iewc:Tab>
				<iewc:Tab DefaultImageUrl="images/_history_off_disabled.gif" SelectedImageUrl="images/_history_on.gif"
					ID="iewcHistory" Enabled="False"></iewc:Tab>
				<iewc:Tab DefaultImageUrl="images/_creditcardinfo_off_disabled.gif" SelectedImageUrl="images/_creditcardinfo_on.gif"
					ID="iewcPaymentInformation" Enabled="False"></iewc:Tab>
				<iewc:Tab DefaultImageUrl="images/_shipmentinfo_off_disabled.gif" SelectedImageUrl="images/_shipmentinfo_on.gif"
					ID="iewcShipment" Enabled="False"></iewc:Tab>
			</iewc:tabstrip>
		</TD>
		<td background="images/tab_bg.gif">
			<asp:HyperLink id="hypPrint" runat="server" ImageUrl="images/print.gif" NavigateUrl="javascript: void(0);" onclick="javascript: Open('PagePrintInfo.aspx?IsNewWindow=true&PageSwitchStateID=' + CustomerServicePage.SavePageSwitchStateFromClient(document.forms[0].elements['__VIEWSTATE'].value).value);"></asp:HyperLink>
		</td>
	</TR>
	<tr>
		<td colspan="2"><iewc:multipage id="mupMainPage" runat="server">
				<iewc:pageview id="pavMainSearchResult">
					<uc1:MainSearchResult id="ctrlMainSearchResult" runat="server"></uc1:MainSearchResult>
				</iewc:pageview>
				<iewc:pageview id="pavMainAddressInfo">
					<uc1:MainAddressInfo id="ctrlMainAddressInfo" runat="server"></uc1:MainAddressInfo>
				</iewc:pageview>
				<iewc:pageview id="pavMainSubscriptionInfo">
					<uc1:MainSubscriptionInfo id="ctrlMainSubscriptionInfo" runat="server"></uc1:MainSubscriptionInfo>
				</iewc:pageview>
				<iewc:pageview id="pavMainOrderInfo">
					<uc1:MainOrderInfo id="ctrlMainOrderInfo" runat="server"></uc1:MainOrderInfo>
				</iewc:pageview>
				<iewc:pageview id="pavMainHistoryInfo">
					<uc1:MainHistoryInfo id="ctrlMainHistoryInfo" runat="server"></uc1:MainHistoryInfo>
				</iewc:pageview>
				<iewc:pageview id="pavPaymentInformation">
					<uc1:MainPaymentInfo id="ctrlMainPaymentInfo" runat="server"></uc1:MainPaymentInfo>
				</iewc:pageview>
				<iewc:pageview id="pavMainShipmentInfo">
					<uc1:MainShipmentInfo id="ctrlMainShipmentInfo" runat="server"></uc1:MainShipmentInfo>
				</iewc:pageview>
			</iewc:multipage></td>
	</tr>
</TABLE>
