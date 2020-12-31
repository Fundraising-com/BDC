<%@ Control Language="c#" AutoEventWireup="True" Codebehind="InfoSearchFM.ascx.cs" Inherits="QSPFulfillment.CustomerService.InfoSearchFM" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD>
			<asp:Label id="lblID" runat="server" CssClass="csPlainText"> ID</asp:Label></TD>
	</TR>
	<TR>
		<TD style="HEIGHT: 23px">
			<cc1:TextBoxSearch ParameterName="FMID" id="tbxID" runat="server"></cc1:TextBoxSearch></TD>
	</TR>
	<TR>
		<TD>
			<asp:Label id="lblLastName" runat="server" CssClass="csPlainText"> Last Name</asp:Label></TD>
	</TR>
	<TR>
		<TD>
			<cc1:TextBoxSearch ParameterName="FMLastName" id="tbxLastName" runat="server"></cc1:TextBoxSearch></TD>
	</TR>
	<TR>
		<TD>
			<asp:Label id="lblFirstName" runat="server" CssClass="csPlainText"> First Name</asp:Label></TD>
	</TR>
	<TR>
		<TD>
			<cc1:TextBoxSearch ParameterName="FMFirstName" id="tbxFirstName" runat="server"></cc1:TextBoxSearch></TD>
	</TR>
</TABLE>
