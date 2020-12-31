<%@ Register TagPrefix="uc1" TagName="InfoSearchMagazine" Src="InfoSearchMagazine.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="InfoSearchOrder.ascx.cs" Inherits="QSPFulfillment.CustomerService.InfoSearchOrder" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="InfoSearchFM" Src="InfoSearchFM.ascx" %>
<%@ Register TagPrefix="uc1" TagName="DateEntry" Src="../Common/DateEntry.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" border="0">
	<TR>
		<TD>
			<asp:Label id="Label10" runat="server" CssClass="csPlainText" Font-Bold="True">Field Manager</asp:Label></TD>
	</TR>
	<TR>
		<TD><uc1:infosearchfm id="ctrlInfoSearchFM" runat="server"></uc1:infosearchfm></TD>
	</TR>
	<TR>
		<TD>
			<asp:Label id="Label11" runat="server" CssClass="csPlainText" Font-Bold="True">Order</asp:Label></TD>
	</TR>
	<TR>
		<TD>
			<P><asp:label id="Label2" runat="server" CssClass="csPlainText">From</asp:label></P>
		</TD>
	</TR>
	<TR>
		<TD><uc1:dateentry id="ctrlDateEntryFrom" runat="server" ParameterName="FromDate"></uc1:dateentry></TD>
	</TR>
	<TR>
		<TD><asp:label id="Label3" runat="server" CssClass="csPlainText">TO</asp:label></TD>
	</TR>
	<TR>
		<TD><uc1:dateentry id="ctrlDateEntryTo" runat="server" ParameterName="ToDate"></uc1:dateentry></TD>
	</TR>
	<TR>
		<TD><asp:label id="Label4" runat="server" CssClass="csPlainText">Campaign ID</asp:label></TD>
	</TR>
	<TR>
		<TD><cc1:textboxsearch id="tbxCampaignID" runat="server" ParameterName="CampaignID"></cc1:textboxsearch>
			<asp:RangeValidator id="RangeValidator5" runat="server" ErrorMessage="Campaign ID must be between 1 and 2147483647."
				Type="Integer" ControlToValidate="tbxCampaignID" MaximumValue="2147483647" MinimumValue="1">*</asp:RangeValidator></TD>
	</TR>
	<TR>
		<TD><asp:label id="Label5" runat="server" CssClass="csPlainText">Order ID</asp:label></TD>
	</TR>
	<TR>
		<TD><cc1:textboxsearch id="tbxOrderID" runat="server" ParameterName="OrderID"></cc1:textboxsearch>
			<asp:RangeValidator id="RangeValidator1" runat="server" ErrorMessage="Order ID must be between 1 and 2147483647."
				Type="Integer" ControlToValidate="tbxOrderID" MaximumValue="2147483647" MinimumValue="1">*</asp:RangeValidator></TD>
	</TR>
	<TR>
		<TD><asp:label id="Label6" runat="server" CssClass="csPlainText">Order Type</asp:label></TD>
	</TR>
	<TR>
		<TD><cc1:dropdownlistsearch id="ddlOrderType" runat="server" ParameterName="OrderTypeID" onload="ddlOrderType_Load"></cc1:dropdownlistsearch></TD>
	</TR>
	<TR>
		<TD><asp:label id="Label7" runat="server" CssClass="csPlainText">Staff Order</asp:label></TD>
	</TR>
	<TR>
		<TD><cc1:dropdownlistsearch id="DropDownList2" runat="server" ParameterName="StaffOrder">
				<asp:ListItem Selected="True"></asp:ListItem>
				<asp:ListItem Value="0">No</asp:ListItem>
				<asp:ListItem Value="1">Yes</asp:ListItem>
			</cc1:dropdownlistsearch></TD>
	</TR>
	<TR>
		<TD><asp:label id="Label8" runat="server" CssClass="csPlainText">Qualifier Name</asp:label></TD>
	</TR>
	<TR>
		<TD>
			<cc1:DropDownListSearch id="ddlQualifierName" runat="server" ParameterName="QualifierID" onload="ddlQualifierName_Load"></cc1:DropDownListSearch></TD>
	</TR>
	<TR>
		<TD><asp:label id="Label9" runat="server" CssClass="csPlainText">Order Status</asp:label></TD>
	</TR>
	<TR>
		<TD><cc1:dropdownlistsearch id="ddlOrderStatus" runat="server" ParameterName="OrderStatusID" onload="ddlOrderStatus_Load"></cc1:dropdownlistsearch></TD>
	</TR>
	<TR>
		<TD>
			<asp:label id="Label15" CssClass="csPlainText" runat="server">Product Line</asp:label></TD>
	</TR>
	<TR>
		<TD>
			<asp:DropDownList id="ddlProductType" runat="server" onload="ddlProductType_Load"></asp:DropDownList></TD>
	</TR>
	<TR>
		<TD>
			<asp:Label id="Label12" runat="server" CssClass="csPlainText" Font-Bold="True">Show Product with Code</asp:Label></TD>
	</TR>
	<TR>
		<TD>
			<asp:TextBox id="tbxProductCode" runat="server"></asp:TextBox><asp:HyperLink id="hypFindProductCode" Runat="server" ImageUrl="images/find.gif" NavigateUrl="javascript:void(0);"></asp:HyperLink></TD>
	</TR>
</TABLE>
