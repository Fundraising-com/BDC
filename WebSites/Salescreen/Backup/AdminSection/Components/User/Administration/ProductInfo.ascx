<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ProductInfo.ascx.cs" Inherits="AdminSection.Components.User.Administration.ProductInfo" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc1" Namespace="efundraising.Web.UI.InputControls" Assembly="efundraising.Web.UI.InputControls" %>
<TABLE class="NormalText" id="Table3" style="WIDTH: 360px; HEIGHT: 144px" width="360" border="0">
	<TR>
		<TD style="WIDTH: 113px; HEIGHT: 21px">Product ID</TD>
		<TD style="WIDTH: 15px; HEIGHT: 21px"></TD>
		<TD style="HEIGHT: 21px">
			<asp:label id="ProductIDLabel" Font-Bold="True" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 113px; HEIGHT: 13px">Name</TD>
		<TD style="WIDTH: 15px; HEIGHT: 13px">
			<asp:Image id="ExistsImage" runat="server" ToolTip="This Package Name Already Exists!" ImageUrl="../../../UserResources/Images/exclam.gif"
				Visible="False"></asp:Image></TD>
		<TD style="HEIGHT: 13px">
			<cc1:stringtextbox id="ProductNameTextBox" runat="server" Width="200px" Columns="30"></cc1:stringtextbox></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 113px; HEIGHT: 13px">Product Code</TD>
		<TD style="WIDTH: 15px; HEIGHT: 13px"></TD>
		<TD style="HEIGHT: 13px">
			<cc1:stringtextbox id="ProductCodeTextBox" runat="server" Width="184px" Columns="30"></cc1:stringtextbox></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 113px; HEIGHT: 17px">Parent Product</TD>
		<TD style="WIDTH: 15px; HEIGHT: 17px"></TD>
		<TD style="HEIGHT: 17px">
			<asp:dropdownlist id="ParentProductDropDownList" runat="server" Width="200px"></asp:dropdownlist></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 113px; HEIGHT: 13px">Raising Potential</TD>
		<TD style="WIDTH: 15px; HEIGHT: 13px"></TD>
		<TD style="HEIGHT: 13px">
			<cc1:decimaltextbox id="RaisingPotentialTextBox" runat="server" Nullable="True"></cc1:decimaltextbox></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 113px; HEIGHT: 1px">Enabled</TD>
		<TD style="WIDTH: 15px; HEIGHT: 1px"></TD>
		<TD style="HEIGHT: 1px">
			<asp:dropdownlist id="ProductEnabledDropDownList" Width="80px" Runat="server"></asp:dropdownlist></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 113px; HEIGHT: 8px">Is Inner</TD>
		<TD style="WIDTH: 15px; HEIGHT: 8px"></TD>
		<TD style="HEIGHT: 8px">
			<asp:dropdownlist id="IsInnerDropDownList" Width="80px" Runat="server"></asp:dropdownlist></TD>
	</TR>
</TABLE>
