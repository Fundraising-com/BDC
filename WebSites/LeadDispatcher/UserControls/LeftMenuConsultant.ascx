<%@ Control Language="c#" AutoEventWireup="True" Codebehind="LeftMenuConsultant.ascx.cs" Inherits="CRMWeb.UserControls.LeftMenuConsultant" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE id="Table1" style="WIDTH: 104px; HEIGHT: 416px" cellSpacing="0" cellPadding="0"
	width="104" border="0">
	<TR>
		<TD style="WIDTH: 302px; HEIGHT: 416px" vAlign="top">&nbsp;&nbsp;&nbsp;
		</TD>
		<TD style="WIDTH: 302px; HEIGHT: 416px" vAlign="top">
			<TABLE id="Table2" style="WIDTH: 84px; HEIGHT: 336px" cellSpacing="0" cellPadding="0" width="84"
				border="0">
				<TR>
					<TD>
						<asp:imagebutton id="ImageButton1" runat="server" ImageUrl="../images/Untitled-1 copy.gif"></asp:imagebutton></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD>
						<asp:imagebutton id="ImageButton2" runat="server" ImageUrl="../images/mycrm_leftmenu.gif"></asp:imagebutton></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 80px">
						<asp:imagebutton id="ImageButton3" runat="server" ImageUrl="../images/reports_leftmenu.gif"></asp:imagebutton></TD>
					<TD style="HEIGHT: 80px"></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 85px">
						<asp:ImageButton id="cmdLogOff" runat="server" ImageUrl="../images/logOff_leftmenu.gif"></asp:ImageButton></TD>
					<TD style="HEIGHT: 85px"></TD>
				</TR>
			</TABLE>
			&nbsp;
		</TD>
		<TD vAlign="top" style="HEIGHT: 416px">&nbsp;
			<asp:Image id="Image1" runat="server" ImageUrl="../images/line.gif" Height="401px"></asp:Image></TD>
	</TR>
</TABLE>
