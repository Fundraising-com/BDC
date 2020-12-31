<%@ Control Language="c#" AutoEventWireup="True" Codebehind="OrderStatus.ascx.cs" Inherits="QSPFulfillment.OrderMgt.UC.OrderStatus" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<asp:DropDownList ID="ddlOrderStatus" runat="server" />
<asp:RequiredFieldValidator id="rq_OrderStatus"  runat="server" ControlToValidate="ddlOrderStatus" Display="Dynamic" Text="*" />