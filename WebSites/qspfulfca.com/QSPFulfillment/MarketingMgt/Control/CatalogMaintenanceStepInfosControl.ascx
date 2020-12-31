<%@ Control Language="c#" AutoEventWireup="True" Codebehind="CatalogMaintenanceStepInfosControl.ascx.cs" Inherits="QSPFulfillment.MarketingMgt.Control.CatalogMaintenanceStepInfosControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<div id="divInfos" runat="server" style="BORDER: whitesmoke 1px solid; PADDING-RIGHT: 15px; PADDING-LEFT: 15px; PADDING-BOTTOM: 10px; WIDTH: 100%; PADDING-TOP: 10px; margin: 10px;">
	<div id="divCatalog" runat="server">
		<asp:label id="Label1" runat="server" cssclass="csPlainText" font-bold="True">Selected Catalog</asp:label>
		<div style="MARGIN-BOTTOM: 5px; TEXT-ALIGN: right">
			<asp:label id="lblCatalogName" runat="server" cssclass="csPlainText"></asp:label>
		</div>
	</div>
	<br>
	<div id="divSection" runat="server">
		<asp:label id="Label3" runat="server" cssclass="csPlainText" font-bold="True">Selected Section</asp:label>
		<div style="MARGIN-BOTTOM: 5px; TEXT-ALIGN: right">
			<asp:label id="lblSectionName" runat="server" cssclass="csPlainText"></asp:label>
		</div>
	</div>
</div>
