<%@ Control Language="vb" AutoEventWireup="false" Codebehind="AdvancedSearch.ascx.vb" Inherits="StoreFront.StoreFront.AdvancedSearch" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<TABLE class="Content" id="Table4" cellSpacing="0" cellPadding="0" border="0">
	<TR>
		<TD class="Headings" align="left" colspan="2" width="582">&nbsp;Advanced Search</TD>
	</TR>
	<TR>
		<TD class="Content" width="177">&nbsp;</TD>
		<TD width="405">&nbsp;</TD>
	</TR>
	<TR id="trKeyword" runat="server">
		<TD class="Content" align="right" width="177">Keyword(s):&nbsp;</TD>
		<TD width="405">
			<asp:textbox id="AdvKeyword" Columns="40" runat="server"></asp:textbox></TD>
	</TR>
	<TR id="trKeyword2" runat="server">
		<TD width="177">&nbsp;</TD>
		<TD class="Content" align="left" width="405">
			<asp:radiobutton id="SimpleKeywordGroup1" runat="server" GroupName="KeywordOption" Text="Any Words" Checked="True"></asp:radiobutton>
			<asp:radiobutton id="SimpleKeywordGroup2" runat="server" GroupName="KeywordOption" Text="Exact Phrase"></asp:radiobutton>
			<asp:radiobutton id="SimpleKeywordGroup3" runat="server" GroupName="KeywordOption" Text="All Words"></asp:radiobutton></TD>
	</TR>
	<TR>
		<TD width="177">&nbsp;</TD>
		<TD width="405">&nbsp;</TD>
	</TR>
	<TR id="trCategory" runat="server">
		<TD class="Content" align="right" width="177">By&nbsp;
			<asp:Label id="lblCategory" runat="server">Category:</asp:Label></TD>
		<TD width="405" height="12">
			<asp:dropdownlist id="AdvCategory" runat="server" DataTextField="Name" DataValueField="ID" DataMember="Categories" Width="237px">
				<asp:ListItem Value="-1" Selected="True">All Categories</asp:ListItem>
			</asp:dropdownlist></TD>
	</TR>
	<tr>
		<TD colSpan="2" width="582">&nbsp;</TD>
	</tr>
	<TR id="trManufacturer" runat="server">
		<TD class="Content" align="right" width="177">By&nbsp;
			<asp:Label id="lblManufacturer" runat="server">Manufacturer:</asp:Label></TD>
		<TD width="405">
			<asp:dropdownlist id="AdvManufacture" runat="server" Width="235px">
				<asp:ListItem Selected="True" Value="-1">All Manufacturers</asp:ListItem>
			</asp:dropdownlist></TD>
	</TR>
	<tr>
		<TD colSpan="2" width="582">&nbsp;</TD>
	</tr>
	<TR id="trVendor" runat="server">
		<TD class="Content" align="right" width="177">By&nbsp;
			<asp:Label id="lblVendor" runat="server">Vendor:</asp:Label></TD>
		<TD width="405">
			<asp:dropdownlist id="AdvVendor" runat="server" Width="234px">
				<asp:ListItem Value="-1" Selected="True">All Vendors</asp:ListItem>
			</asp:dropdownlist></TD>
	</TR>
	<tr>
		<TD colSpan="2" width="582">&nbsp;</TD>
	</tr>
	<TR id="trPriceBetween" runat="server">
		<TD class="Content" align="right" width="177">
			<asp:Label id="lblPrice" runat="server">Price</asp:Label></TD>
		<TD width="405">
			<asp:textbox id="AdvPriceStart" Columns="10" runat="server"></asp:textbox>&nbsp;To&nbsp;
			<asp:textbox id="AdvPriceEnd" Columns="10" runat="server"></asp:textbox>
			<asp:Panel id="pnlSaleOnly" Runat="server">
				<asp:checkbox id="AdvSaleOnly" runat="server" Text="Only Sale Items" CssClass="Content"></asp:checkbox>
			</asp:Panel></TD>
	</TR>
	<tr>
		<TD colSpan="2" width="582">&nbsp;</TD>
	</tr>
	<TR id="trAddedBetween" runat="server">
		<TD class="Content" align="right" width="177">Added to Inventory In:&nbsp;&nbsp;</TD>
		<TD width="405">
			<asp:dropdownlist id="dlInventory" runat="server" Width="234px">
				<asp:ListItem Selected="True" Value="-1">&nbsp;</asp:ListItem>
				<asp:ListItem Value="1">This Month</asp:ListItem>
				<asp:ListItem Value="2">Last Month</asp:ListItem>
				<asp:ListItem Value="3">Last Two Months</asp:ListItem>
				<asp:ListItem Value="4">Last Six Months</asp:ListItem>
			</asp:dropdownlist></TD>
	</TR>
	<tr>
		<TD colSpan="2" width="582"></TD>
	</tr>
	<tr>
		<TD colSpan="2" class="Content" align="right">
			<asp:LinkButton ID="btnAdvSearch" Runat="server">
				<asp:Image BorderWidth="0" ID="imgAdvSearch" Runat="server" AlternateText="Search"></asp:Image>
			</asp:LinkButton>
		</TD>
	</tr>
</TABLE>
