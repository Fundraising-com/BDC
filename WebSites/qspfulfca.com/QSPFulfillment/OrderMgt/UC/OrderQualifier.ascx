<%@ Control Language="c#" AutoEventWireup="True" Codebehind="OrderQualifier.ascx.cs" Inherits="QSPFulfillment.OrderMgt.UC.OrderQualifier" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<asp:DropDownList ID="ddlOrderQualifier" runat="server" />
<asp:RequiredFieldValidator id="rq_OrderQualifier"  runat="server" ControlToValidate="ddlOrderQualifier" Display="Dynamic" Text="*" />