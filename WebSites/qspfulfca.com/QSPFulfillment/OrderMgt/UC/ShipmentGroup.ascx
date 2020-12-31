<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ShipmentGroup.ascx.cs" Inherits="QSPFulfillment.OrderMgt.UC.ShipmentGroup" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<asp:DropDownList ID="ddlShipmentGroup" runat="server" />
<asp:RequiredFieldValidator id="rq_ShipmentGroup"  runat="server" ControlToValidate="ddlShipmentGroup" Display="Dynamic" Text="*" />