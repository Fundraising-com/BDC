<%@ Control Language="c#" AutoEventWireup="True" Codebehind="CatalogMaintenanceStepsContainerControl.ascx.cs" Inherits="QSPFulfillment.MarketingMgt.Control.CatalogMaintenanceStepsContainerControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="CompletedCatalogControl" Src="CompletedCatalogControl.ascx" %>
<asp:label id="lblStepTitle" runat="server" font-size="12pt" font-bold="True" forecolor="#2F4F88"
	font-names="Verdana,Arial"></asp:label>
<br>
<br>
<uc1:completedcatalogcontrol id="ctrlCompletedCatalogControl" runat="server"></uc1:completedcatalogcontrol>
<div style="PADDING-TOP: 55px">
	<asp:label id="lblStep" runat="server"></asp:label>
</div>
