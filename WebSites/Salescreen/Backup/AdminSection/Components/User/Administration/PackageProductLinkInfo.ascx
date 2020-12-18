<%@ Control Language="c#" AutoEventWireup="false" Codebehind="PackageProductLinkInfo.ascx.cs" Inherits="AdminSection.Components.User.Administration.PackageProductLinkInfo" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc1" Namespace="efundraising.Web.UI.InputControls" Assembly="efundraising.Web.UI.InputControls" %>
<P>
	<TABLE class="NormalText" id="Table3" style="WIDTH: 424px; HEIGHT: 108px" width="424" border="0">
		<TR>
			<TD style="WIDTH: 138px">Product
			</TD>
			<TD>
				<asp:dropdownlist id="ProductDropdownlist" runat="server" Width="290px"></asp:dropdownlist></TD>
		</TR>
		<TR>
			<TD style="WIDTH: 138px; HEIGHT: 23px">Package</TD>
			<TD style="HEIGHT: 23px">
				<asp:dropdownlist id="PackageDropDownList" runat="server" Width="290px"></asp:dropdownlist></TD>
		</TR>
		<TR>
			<TD style="WIDTH: 138px; HEIGHT: 24px">Display Order</TD>
			<TD style="HEIGHT: 24px">
				<cc1:IntegerTextBox id="DisplayOrderTextBox" runat="server" Width="120px"></cc1:IntegerTextBox></TD>
		</TR>
		<TR>
			<TD style="WIDTH: 138px; HEIGHT: 1px">Display</TD>
			<TD style="HEIGHT: 1px">
				<asp:dropdownlist id="DisplayDropdownlist" Runat="server"></asp:dropdownlist></TD>
		</TR>
	</TABLE>
</P>
