<%@ Control Language="c#" AutoEventWireup="True" Codebehind="DataHeaderLead.ascx.cs" Inherits="CRMWeb.UserControls.DataHeaderLead" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<asp:Panel id="Panel1" runat="server" Height="8px" Width="720px">
	<TABLE id="Table4" style="WIDTH: 712px; HEIGHT: 32px" cellSpacing="2" cellPadding="0" width="712"
		border="0">
		<TR>
			<TD style="WIDTH: 141px; HEIGHT: 24px" vAlign="top"></TD>
			<TD style="WIDTH: 50px; HEIGHT: 24px" vAlign="top">
				<asp:Label id="Label1" runat="server" Font-Size="11pt" Font-Bold="True" Width="56px" ForeColor="#FFFEC7">Person:</asp:Label></TD>
			<TD style="WIDTH: 251px; HEIGHT: 24px" vAlign="top">
				<asp:Label id="lblPerson" runat="server" Font-Size="11pt" Font-Bold="True" Width="231px" ForeColor="White"></asp:Label></TD>
			<TD style="WIDTH: 64px; HEIGHT: 24px" vAlign="top" align="left">
				<asp:Label id="Label3" runat="server" Font-Size="11pt" Font-Bold="True" Width="48px" ForeColor="#FFFEC7">Phone:</asp:Label></TD>
			<TD style="HEIGHT: 24px" vAlign="top" align="left">
				<asp:Label id="lblPhone" runat="server" Font-Size="11pt" Font-Bold="True" Width="160px" ForeColor="White"></asp:Label></TD>
		</TR>
		<TR>
			<TD style="WIDTH: 141px" vAlign="middle">
				<asp:Label id="lblUserName" runat="server" Font-Size="9pt" Font-Bold="True" Width="70px" ForeColor="#FFFEC7"
					Visible="False"></asp:Label></TD>
			<TD style="WIDTH: 50px" vAlign="middle">
				<asp:Label id="Label2" runat="server" Font-Size="11pt" Font-Bold="True" Width="41px" ForeColor="#FFFEC7">Group:</asp:Label></TD>
			<TD style="WIDTH: 251px" vAlign="middle">
				<asp:Label id="lblGroup" runat="server" Font-Size="11pt" Font-Bold="True" Width="224px" ForeColor="White"></asp:Label></TD>
			<TD style="WIDTH: 64px">
				<asp:Label id="Label4" runat="server" Font-Size="11pt" Font-Bold="True" Width="63px" ForeColor="#FFFEC7">E-Mail:</asp:Label></TD>
			<TD>
				<asp:Label id="lblEmail" runat="server" Font-Size="11pt" Font-Bold="True" Width="184px" ForeColor="White"></asp:Label></TD>
		</TR>
	</TABLE>
</asp:Panel>
