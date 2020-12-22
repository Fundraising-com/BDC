<%@ Page language="c#" Codebehind="PickDay.aspx.cs" AutoEventWireup="True" Inherits="EFundraisingCRMWeb.Components.User.PickDay1" %>
<head runat="server" id="Header" />
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>PickDay</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../Resources/Css/style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body style="BACKGROUND-IMAGE: url(../images/1x1.gif); BACKGROUND-COLOR: transparent"
		bottomMargin="0" bgColor="#ffffff" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="netCalendar" method="post" runat="server">
			<div style="BORDER-RIGHT: 1px outset; BORDER-TOP: 1px outset; BORDER-LEFT: 1px outset; BORDER-BOTTOM: 1px outset"><span id="cadre">
					<TABLE id="Table1" cellSpacing="2" cellPadding="0" bgColor="#ffffff" border="0">
						<TR>
							<TD><asp:dropdownlist id="MonthDropDownList" runat="server" CssClass="SmallText" AutoPostBack="True" onselectedindexchanged="MonthDropDownList_SelectedIndexChanged"></asp:dropdownlist></TD>
							<TD align="center"><asp:dropdownlist id="YearDropDownList" runat="server" CssClass="SmallText" AutoPostBack="True" onselectedindexchanged="YearDropDownList_SelectedIndexChanged"></asp:dropdownlist></TD>
							<TD align="right"><asp:linkbutton NOVALIDATION CssClass="SmallText" id="TodayButton" runat="server" Text="Today" onclick="TodayButton_Click">[Today]</asp:linkbutton></TD>
						</TR>
						<TR>
							<TD colSpan="3"><asp:calendar id="Calendar1" runat="server" CssClass="SmallText" BorderColor="White" ShowGridLines="True" onselectionchanged="Calendar1_SelectionChanged">
									<todaydaystyle borderstyle="Solid" bordercolor="#64799C"></todaydaystyle>
									<daystyle cssclass="DayStyle"></daystyle>
									<nextprevstyle forecolor="White"></nextprevstyle>
									<dayheaderstyle cssclass="DayHeaderStyle" backcolor="#ABC0E7"></dayheaderstyle>
									<titlestyle font-size="11px" font-bold="True" forecolor="White" backcolor="#64799C"></titlestyle>
									<othermonthdaystyle backcolor="#EFEFEB"></othermonthdaystyle>
								</asp:calendar></TD>
						</TR>
						<TR>
							<TD align="center" colSpan="3">
								<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="120" border="0">
									<TR>
										<!-- <TD align="left"><asp:linkbutton CssClass="SmallText" id="SelectButton" runat="server" Text="Select">[Select]</asp:linkbutton></TD> -->
										<!-- <td align="center"><asp:linkbutton CssClass="SmallText" id="ClearButton" runat="server" text="Clear">[Clear]</asp:linkbutton></td> -->
										<TD align="right"><asp:linkbutton NOVALIDATION CssClass="SmallText" id="CancelButton" runat="server" Text="Cancel" onclick="CancelButton_Click">[Cancel]</asp:linkbutton></TD>
									</TR>
								</TABLE>
							</TD>
						</TR>
					</TABLE>
				</span>
			</div>
		</form>
		<%# GetScript() %>
	</body>
</HTML>
