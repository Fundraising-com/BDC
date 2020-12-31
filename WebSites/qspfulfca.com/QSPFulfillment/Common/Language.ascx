<%@ Control Language="c#" AutoEventWireup="True" Codebehind="Language.ascx.cs" Inherits="QSPFulfillment.CommonWeb.UC.Language" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<asp:DropDownList ID=DDL Runat=server>
	<asp:ListItem Value="en" Text="English" Selected=True />
	<asp:ListItem Value="fr" Text="French" />
</asp:DropDownList>
<asp:RequiredFieldValidator id="rq_Language" runat="server" ControlToValidate="DDL" Display="Dynamic" ErrorMessage="<br />Please select a language" />