<%@ Register TagPrefix="uc1" TagName="AllAccountsForLead" Src="AllAccountsForLead.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PromotionGroups" Src="Lead/PromotionGroups.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Admin_Menu" Src="Admin_Menu.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Admin_All_Accounts" Src="Admin_All_Accounts.ascx" %>
<%@ Register TagPrefix="uc1" TagName="zzz" Src="zzz.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="Admin_Section.ascx.cs" Inherits="CRMWeb.UserControls.Admin_Section" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="Login" Src="Login.ascx" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="300" border="0">
	<TR>
		<TD></TD>
		<TD><uc1:admin_menu id="Admin_Menu1" runat="server"></uc1:admin_menu></TD>
	</TR>
	<TR>
		<TD>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
		</TD>
		<TD></TD>
	</TR>
	<TR>
		<TD></TD>
		<TD><uc1:promotiongroups id="PromotionGroups1" runat="server"></uc1:promotiongroups>
			<uc1:zzz id="Zzz1" runat="server" Visible="False"></uc1:zzz></TD>
	</TR>
</TABLE>
