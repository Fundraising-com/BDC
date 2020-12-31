<%@ Control Language="c#" AutoEventWireup="True" Codebehind="BatchType.ascx.cs" Inherits="QSPFulfillment.OrderMgt.UC.BatchType" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<asp:DropDownList ID="ddlBatchType" runat="server" />
<asp:RequiredFieldValidator id="rq_BatchType"  runat="server" ControlToValidate="ddlBatchType" Display="Dynamic" Text="*" />