<%@ Control Language="c#" AutoEventWireup="True" Codebehind="Email.ascx.cs" Inherits="QSPFulfillment.CommonWeb.UC.Email" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<asp:textbox id="tb_EMAIL" Runat="server" Columns="30" MaxLength="75" />
<asp:RequiredFieldValidator     id="rq_email"  runat="server" ControlToValidate="tb_EMAIL" Display="Dynamic" Text="*" />
<asp:RegularExpressionValidator id="reg_email" runat="server" ControlToValidate="tb_EMAIL" Display="Dynamic" Text="*" ErrorMessage="Please enter a valid email address." ValidationExpression="\S+@\S+\.\S{2,3}" /><br>