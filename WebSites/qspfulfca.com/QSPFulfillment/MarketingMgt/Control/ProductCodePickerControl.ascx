<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ProductCodePickerControl.ascx.cs" Inherits="QSPFulfillment.MarketingMgt.Control.ProductCodePickerControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<cc1:textboxreq id="tbxProductCode" runat="server" columns="4" maxlength="4" errormsgrequired="The field Product Code is mandatory."
	required="True"></cc1:textboxreq>
<asp:hyperlink id="hylPicker" runat="server" imageurl="../images/find.gif" navigateurl=""></asp:hyperlink>
