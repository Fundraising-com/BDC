<%@ Control Language="c#" AutoEventWireup="True" Codebehind="StepSelectCatalogControl.ascx.cs" Inherits="QSPFulfillment.MarketingMgt.Control.StepSelectCatalogControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="CatalogSearchControl" Src="CatalogSearchControl.ascx" %>
<asp:label id="Label1" runat="server" cssclass="csPlainText" font-bold="True">Please select a catalog from the list:</asp:label>
<br>
<uc1:catalogsearchcontrol id="ctrlCatalogSearchControl" runat="server"></uc1:catalogsearchcontrol>
