<%@ Control Language="c#" AutoEventWireup="True" Codebehind="StepCatalogSectionsControl.ascx.cs" Inherits="QSPFulfillment.MarketingMgt.Control.StepCatalogSectionsControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="uc1" TagName="CatalogSectionMaintenanceControl" Src="CatalogSectionMaintenanceControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CatalogSectionSearchControl" Src="CatalogSectionSearchControl.ascx" %>
<uc1:catalogsectionsearchcontrol id="ctrlCatalogSectionSearchControl" runat="server"></uc1:catalogsectionsearchcontrol>
<br>
<asp:button id="btnCreate" runat="server" text="Create Section" cssclass="boxlook" onclick="btnCreate_Click"></asp:button>
