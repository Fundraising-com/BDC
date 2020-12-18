<%@ Register TagPrefix="cc1" Namespace="efundraising.Web.UI.InputControls" Assembly="efundraising.Web.UI.InputControls" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="PackageInfo.ascx.cs" Inherits="AdminSection.Components.User.Administration.PackageInfo" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE class="NormalText" id="Table3" style="WIDTH: 400px; HEIGHT: 112px" width="400" border="0">
	<TR>
		<TD style="WIDTH: 101px; HEIGHT: 16px">Package ID</TD>
		<TD style="WIDTH: 18px; HEIGHT: 16px"></TD>
		<TD style="HEIGHT: 16px">
			<asp:label id="PackageIDLabel" runat="server" Font-Bold="True"></asp:label></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 101px; HEIGHT: 13px">Package Name</TD>
		<TD style="WIDTH: 18px; HEIGHT: 13px">
			<asp:Image id="ExistsImage" runat="server" ToolTip="This Package Name Already Exists!" ImageUrl="../../../UserResources/Images/exclam.gif"
				Visible="False"></asp:Image></TD>
		<TD style="HEIGHT: 13px">
			<cc1:stringtextbox id="PackageNameTextBox" runat="server" Width="200px" Columns="30"></cc1:stringtextbox></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 101px; HEIGHT: 4px">Parent Package</TD>
		<TD style="WIDTH: 18px; HEIGHT: 4px"></TD>
		<TD style="HEIGHT: 4px">
			<asp:dropdownlist id="ParentPackageDropDownList" runat="server" Width="200px"></asp:dropdownlist></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 101px; HEIGHT: 2px">Profit %</TD>
		<TD style="WIDTH: 18px; HEIGHT: 2px"></TD>
		<TD style="HEIGHT: 2px">
			<cc1:integertextbox id="ProfitPercentTextBox" runat="server" MaxValue="1000" Nullable="True" Height="23"></cc1:integertextbox></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 101px; HEIGHT: 1px">Enabled</TD>
		<TD style="WIDTH: 18px; HEIGHT: 1px"></TD>
		<TD style="HEIGHT: 1px">
			<asp:dropdownlist id="PackageEnabledDropDownList" Runat="server"></asp:dropdownlist></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 101px; HEIGHT: 28px"></TD>
		<TD style="WIDTH: 18px; HEIGHT: 28px"></TD>
		<TD style="HEIGHT: 28px"></TD>
	</TR>
</TABLE>
