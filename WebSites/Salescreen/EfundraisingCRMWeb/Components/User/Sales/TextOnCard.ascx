<%@ Control Language="c#" AutoEventWireup="True" Codebehind="TextOnCard.ascx.cs" Inherits="EFundraisingCRMWeb.Components.User.Sales.TextOnCard" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE id="Table1" style="WIDTH: 472px; HEIGHT: 32px" width="472" border="0">
	<TR>
		<TD style="WIDTH: 56px"><asp:label id="productLabel" CssClass="FrameTitleColor" Width="88px" runat="server">Text On</asp:label></TD>
		<TD style="WIDTH: 277px"><asp:textbox id="textTextBox" Width="272px" runat="server" Font-Bold="True" BackColor="#FFFFC0"></asp:textbox></TD>
		<TD><asp:button id="saveButton" runat="server" Text="Confirm" Width="76px" onclick="saveButton_Click"></asp:button><asp:textbox id="scratchBookIDTextBox" Width="8px" runat="server" Visible="False"></asp:textbox></TD>
	</TR>
</TABLE>
