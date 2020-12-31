<%@ Page language="c#" Codebehind="RemitBatchSummary.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.OrderMgt.RemitBatchSummary" %>
<%@ Register TagPrefix="mbcbb" Namespace="MetaBuilders.WebControls" Assembly="MetaBuilders.WebControls.ComboBox" %>
<%@ Register TagPrefix="XXX" TagName="CodeDetail" Src="../Common/CodeDetailDropDown.ascx" %>
<%@ Register  TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>RemitBatchSummary</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
  </HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<cc1:Menu id="Menu1" runat="Server" BackColor="LightGray" BorderColor="Black" Layout="Horizontal"
				Cursor="Pointer" BorderWidth="2px" GridLines="Both">
				<SelectedMenuItemStyle BorderColor="#FF8000" BackColor="#FFC080"></SelectedMenuItemStyle>
			</cc1:Menu>
			<XXX:CodeDetail id="Code" runat="server"></XXX:CodeDetail>
		</form>
	</body>
</HTML>
