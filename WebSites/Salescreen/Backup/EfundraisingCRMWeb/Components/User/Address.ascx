<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Address.ascx.cs" Inherits="EFundraisingCRMWeb.Components.User.Address" %>
<%@ Register src="AddressHygiene/AddressHygiene.ascx" tagname="AddressHygiene" tagprefix="uc1" %>
<TABLE class="NormalText" id="Table8" cellSpacing="1" cellPadding="1" border="0" style="width: 288px">
	<TR>
		<TD style="WIDTH: 1013px">
			<asp:Label id="Label18" Width="104px" runat="server">Address:</asp:Label></TD>
		<TD style="WIDTH: 206px">
			<asp:TextBox id="AddressTextBox" BorderStyle="Solid" BorderWidth="1px" BorderColor="#5C86B0"
				runat="server"></asp:TextBox></TD>
		<TD style="width: 11px">
			<asp:Label id="AddressValidator" runat="server" ForeColor="Red">*</asp:Label></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 1013px; HEIGHT: 24px">
			<asp:Label id="Label16" Width="104px" runat="server">City:</asp:Label></TD>
		<TD style="WIDTH: 206px; HEIGHT: 24px">
			<asp:TextBox id="CityTextBox" BorderStyle="Solid" BorderWidth="1px" BorderColor="#5C86B0" runat="server"></asp:TextBox></TD>
		<TD style="HEIGHT: 24px; width: 11px;">
			<asp:Label id="CityValidator" runat="server" ForeColor="Red">*</asp:Label></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 1013px; HEIGHT: 9px">
			<asp:Label id="Label14" Height="8px" Width="80px" runat="server">State:</asp:Label></TD>
		<TD style="WIDTH: 206px; HEIGHT: 9px">
			<asp:DropDownList id="StateDropDownList" Width="152px" runat="server"></asp:DropDownList></TD>
		<TD style="HEIGHT: 9px; width: 11px;"></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 1013px">
			<asp:Label id="Label22" Height="8px" Width="80px" runat="server">Country:</asp:Label></TD>
		<TD style="WIDTH: 206px">
			<asp:DropDownList id="CountryDropDownList" Width="152px" runat="server" AutoPostBack="true"></asp:DropDownList></TD>
		<TD style="width: 11px"></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 1013px">
			<asp:Label id="Label24" Height="8px" Width="80px" runat="server">Zip:</asp:Label></TD>
		<TD style="WIDTH: 206px">
			<asp:TextBox id="Zip1TextBox" BorderStyle="Solid" BorderWidth="1px" BorderColor="#5C86B0" Width="64px"
				runat="server"></asp:TextBox>-
			<asp:TextBox id="Zip2TextBox" BorderStyle="Solid" BorderWidth="1px" BorderColor="#5C86B0" Width="64px"
				runat="server"></asp:TextBox></TD>
		<TD style="width: 11px">
			<asp:Label id="ZipValidator" runat="server" ForeColor="Red">*</asp:Label></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 1013px">
			<asp:Label id="ZoneLabel" Height="8px" Width="80px" runat="server" Visible="False">Zone:</asp:Label></TD>
		<TD style="WIDTH: 206px">
			<asp:DropDownList id="ZoneDropDownList" Width="152px" runat="server" Visible="False"></asp:DropDownList></TD>
		<TD style="width: 11px"></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 1013px">
			<asp:Label id="AttentionOfLabel" Width="80px" runat="server" Visible="False">Attention of:</asp:Label></TD>
		<TD style="WIDTH: 206px">
			<asp:TextBox id="AttentionOfTextBox" BorderStyle="Solid" BorderWidth="1px" BorderColor="#5C86B0"
				runat="server" Visible="False"></asp:TextBox></TD>
		<TD style="width: 11px">
			<asp:Label id="AttnOfValidator" runat="server" ForeColor="Red">*</asp:Label></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 1013px">
			<asp:Label id="LocationLabel" runat="server" Width="110px" Height="8px" 
                Visible="False">Location Name:</asp:Label></TD>
		<TD style="WIDTH: 206px">
			<asp:TextBox id="LocationTextBox" runat="server" BorderColor="#5C86B0" BorderWidth="1px" BorderStyle="Solid"
				Visible="False"></asp:TextBox></TD>
		<TD style="width: 11px">
			<asp:Label id="LocationValidator" runat="server" ForeColor="Red">*</asp:Label></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 1013px">
			<asp:Label id="WarehouseLabel" runat="server" Width="80px" Height="8px" Visible="False">Warehouse:</asp:Label></TD>
		<TD style="WIDTH: 206px">
			<asp:DropDownList id="WarehouseDropDownList" runat="server" Width="152px" Visible="False" DataValueField="21,22,25,26,27,28,30"></asp:DropDownList></TD>
		<TD style="width: 11px"></TD>
	</TR>
</TABLE>
<asp:Label id="errorLabel" runat="server" ForeColor="Red" Font-Size="10pt">An error  occured for the State</asp:Label>


