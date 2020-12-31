<%@ Register TagPrefix="uc1" TagName="PostalAddress" Src="../Common/PostalAddress.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ContolerGroupTo.ascx.cs" Inherits="QSPFulfillment.CustomerService.ContolerGroupTo" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD>
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<TD>
						<asp:Label id="Label1" runat="server">ID</asp:Label></TD>
					<TD>
						<asp:Label id="lblID" runat="server">Label</asp:Label></TD>
				</TR>
				<TR>
					<TD>
						<asp:Label id="Label2" runat="server">Name</asp:Label></TD>
					<TD>
						<asp:Label id="lblName" runat="server">Label</asp:Label>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD>
			<uc1:PostalAddress id="PostalAddress1" runat="server"></uc1:PostalAddress></TD>
	</TR>
</TABLE>
