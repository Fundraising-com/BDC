<%@ Control Language="c#" AutoEventWireup="True" Codebehind="DataHeaderAR.ascx.cs" Inherits="CRMWeb.UserControls.DataHeaderAR" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE id="Table4" style="WIDTH: 472px; HEIGHT: 64px" cellSpacing="2" cellPadding="0" width="472"
	border="0">
	<TR>
		<TD style="WIDTH: 61px; HEIGHT: 18px" vAlign="top">
			<asp:Label id="Label1" Font-Size="9pt" Font-Bold="True" ForeColor="#FFFEC7" Width="56px" runat="server">Sale ID</asp:Label>
		</TD>
		<TD style="WIDTH: 114px; HEIGHT: 18px" vAlign="top">
			<asp:Label id="lblSalesID" runat="server" Width="72px" ForeColor="#FFFEC7" Font-Bold="True"
				Font-Size="9pt">Label</asp:Label>
		</TD>
		<TD style="WIDTH: 116px; HEIGHT: 18px" vAlign="top" align="right">
			<asp:Label id="Label4" Font-Size="9pt" Font-Bold="True" ForeColor="#FFFEC7" Width="55px" runat="server">Client #</asp:Label>
		</TD>
		<TD style="HEIGHT: 18px" vAlign="top">
			<asp:Label id="lblClientNo" ForeColor="#FFFEC7" Width="96px" runat="server" Font-Bold="True"
				Font-Size="9pt">Label</asp:Label>
		</TD>
	</TR>
	<TR>
		<TD style="WIDTH: 61px" vAlign="top">
			<asp:Label id="Label2" Font-Size="9pt" Font-Bold="True" ForeColor="#FFFEC7" Width="56px" runat="server">Person</asp:Label>
		</TD>
		<TD colSpan="2" style="WIDTH: 232px" vAlign="top">
			<asp:Label id="lblPerson" ForeColor="#FFFEC7" Width="160px" runat="server" Font-Bold="True"
				Font-Size="9pt">Label</asp:Label></TD>
		<TD>
		</TD>
	</TR>
	<TR>
		<TD style="WIDTH: 61px" vAlign="top">
			<asp:Label id="Label3" Font-Size="9pt" Font-Bold="True" ForeColor="#FFFEC7" Width="40px" runat="server">Group</asp:Label>
		</TD>
		<TD colSpan="2" style="WIDTH: 232px" vAlign="middle">
			<asp:Label id="lblOrg" ForeColor="#FFFEC7" Width="164px" runat="server" Font-Bold="True" Font-Size="9pt">Label</asp:Label></TD>
		<TD></TD>
	</TR>
</TABLE>
