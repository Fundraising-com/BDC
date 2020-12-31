<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ProgramsDDL.ascx.cs" Inherits="QSPFulfillment.CommonWeb.UC.ProgramsDDL" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<asp:DropDownList ID="ddlPrograms" runat="server" />
<asp:RequiredFieldValidator id="rq_Programs"  runat="server" ControlToValidate="ddlPrograms" Display="Dynamic" Text="*" />
