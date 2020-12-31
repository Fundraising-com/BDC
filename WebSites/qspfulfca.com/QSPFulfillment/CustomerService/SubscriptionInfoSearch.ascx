<%@ Control Language="c#" AutoEventWireup="false" Codebehind="SubscriptionInfoSearch.ascx.cs" Inherits="QSPFulfillment.CustomerService.SubscriptionInfoSearch" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD>
			<asp:Label id="lblTitleCode" runat="server">TitleCode</asp:Label></TD>
		<TD>
			<cc1:TextBoxSearch ParameterName="@"  id="tbxTitleCode" runat="server"></cc1:TextBoxSearch></TD>
	</TR>
	<TR>
		<TD style="HEIGHT: 19px">
			<asp:Label id="lbltitle" runat="server">Title</asp:Label></TD>
		<TD style="HEIGHT: 19px">
			<cc1:TextBoxSearch ParameterName="@"  id="TextBox1" runat="server"></cc1:TextBoxSearch></TD>
	</TR>
	<TR>
		<TD style="HEIGHT: 18px">
			<asp:Label id="lblFromDate" runat="server">From Date</asp:Label></TD>
		<TD style="HEIGHT: 18px"></TD>
	</TR>
	<TR>
		<TD style="HEIGHT: 17px">
			<asp:Label id="lblToDate" runat="server">To Date</asp:Label></TD>
		<TD style="HEIGHT: 17px"></TD>
	</TR>
	<TR>
		<TD>
			<asp:Label id="lblInfoMissing" runat="server">Info Missing</asp:Label></TD>
		<TD></TD>
	</TR>
	<TR>
		<TD>
			<asp:Label id="lblChadd" runat="server">Chadd</asp:Label></TD>
		<TD></TD>
	</TR>
	<TR>
		<TD style="HEIGHT: 17px">
			<asp:Label id="lblCancel" runat="server">Cancel</asp:Label></TD>
		<TD style="HEIGHT: 17px"></TD>
	</TR>
	<TR>
		<TD>
			<asp:Label id="lblSwitch" runat="server">Switch</asp:Label></TD>
		<TD></TD>
	</TR>
	<TR>
		<TD>
			<asp:Label id="lblRemitID" runat="server">Remit ID</asp:Label></TD>
		<TD>
			<cc1:TextBoxSearch ParameterName="@"  id="TextBox2" runat="server"></cc1:TextBoxSearch></TD>
	</TR>
	<TR>
		<TD>
			<asp:Label id="lblRemitDate" runat="server">Remit Date</asp:Label></TD>
		<TD></TD>
	</TR>
</TABLE>
