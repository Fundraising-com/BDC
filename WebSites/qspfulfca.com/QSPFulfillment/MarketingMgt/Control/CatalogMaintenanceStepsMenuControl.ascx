<%@ Control Language="c#" AutoEventWireup="True" Codebehind="CatalogMaintenanceStepsMenuControl.ascx.cs" Inherits="QSPFulfillment.MarketingMgt.Control.CatalogMaintenanceStepsMenuControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="CatalogMaintenanceOneStepMenuControl" Src="CatalogMaintenanceOneStepMenuControl.ascx" %>
<div style="width: 175px;">
	<uc1:catalogMaintenanceOneStepMenuControl id="ctrlCatalogMaintenanceOneStepMenuControlSelectCatalog" runat="server" steptitle="Select Catalog" showseparator="true"></uc1:catalogmaintenanceonestepmenucontrol>
	<uc1:CatalogMaintenanceOneStepMenuControl id="ctrlCatalogMaintenanceOneStepMenuControlCatalogInformations" runat="server" StepTitle="Catalog Informations" ShowSeparator="true"></uc1:CatalogMaintenanceOneStepMenuControl>
	<uc1:CatalogMaintenanceOneStepMenuControl id="ctrlCatalogMaintenanceOneStepMenuControlCatalogSections" runat="server" StepTitle="Catalog Sections" ShowSeparator="true"></uc1:CatalogMaintenanceOneStepMenuControl>
	<uc1:CatalogMaintenanceOneStepMenuControl id="ctrlCatalogMaintenanceOneStepMenuControlIncludeProducts" runat="server" StepTitle="Include Products" ShowSeparator="false"></uc1:CatalogMaintenanceOneStepMenuControl>
</div>
