<%@ Control Language="c#" AutoEventWireup="True" Codebehind="Phone.ascx.cs" Inherits="QSPFulfillment.CommonWeb.UC.Phone" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<asp:textbox id="tb_PHONE" Runat="server" Columns="15" MaxLength="15" />
<asp:RequiredFieldValidator     id="rq_phone"  runat="server" ControlToValidate="tb_PHONE" Display="Dynamic" Text="*" />
<asp:RegularExpressionValidator id="reg_phone" runat="server" ControlToValidate="tb_PHONE" Display="Dynamic" Text="*" ValidationExpression="\(?\s*\d{3}\s*[\)\.\-]?\s*\d{3}\s*[\-\.]?\s*\d{4}" />
<asp:label id="txt_phone" Runat="server" ForeColor="blue" Font-Size="x-small" Text="(Please enter numbers only)" >
(numbers only)</asp:label>
