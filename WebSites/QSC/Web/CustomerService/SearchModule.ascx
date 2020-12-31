<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="SearchModule.ascx.cs" Inherits="QSPFulfillment.CustomerService.searchmodule" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="PageSearchCreditCard" Src="PageSearchCreditCard.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PageSearchSubscription" Src="PageSearchSubscription.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PageSearchShippement" Src="PageSearchShippement.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PageSearchGroupOrder" Src="PageSearchGroupOrder.ascx" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="190" border="0">
	<TR>
		<TD>
		
		<DIV id="subs_title" style="BACKGROUND-COLOR: #eeeeee"><A href="javascript:show('subs_content')"><IMG src="images/searchSubscription.gif" border="0"></A></DIV>
			<DIV id="subs_content" style="DISPLAY: block; OVERFLOW: auto;"><uc1:PageSearchSubscription id="ctrlPageSearchSubscription" runat="server"></uc1:PageSearchSubscription></DIV>
			<DIV id="group_title" style="BACKGROUND-COLOR: #cecece"><A href="javascript:show('group_content')"><IMG src="images/searchGroupOrder.gif" border="0"></A></DIV>
			<DIV id="group_content" style="DISPLAY: none; OVERFLOW: auto;"><uc1:PageSearchGroupOrder id="ctrlPageSearchGroupOrder" runat="server"></uc1:PageSearchGroupOrder></DIV>
			<DIV id="ship_title" style="BACKGROUND-COLOR: #eeeeee"><A href="javascript:show('ship_content')"><IMG src="images/searchShipment.gif" border="0"></A></DIV>
			<DIV id="ship_content" style="DISPLAY: none; OVERFLOW: auto;"><uc1:PageSearchShippement id="ctrlPageSearchShippement" runat="server"></uc1:PageSearchShippement></DIV>
			<DIV id="creditcard_title" style="BACKGROUND-COLOR: #eeeeee"><A href="javascript:show('creditcard_content')"><IMG src="images/searchcreditcard.gif" border="0"></A></DIV>
			<DIV id="creditcard_content" style="DISPLAY: none; OVERFLOW: auto;"><uc1:PageSearchCreditCard id="ctrlPageSearchCreditCard" runat="server"></uc1:PageSearchCreditCard></DIV>
			<DIV id="subs_search" style="DISPLAY: block;">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" height="41" border="0">
					
					<TR>
						<TD align="center" valign="center">
							<input type="submit" name="ctrlSearchModule:ctrlPageSearchSubscription:btnSearch" value="Search" onclick="if (typeof(Page_ClientValidate) == 'function') Page_ClientValidate(); " language="javascript" id="ctrlSearchModule_ctrlPageSearchSubscription_btnSearch" /></TD>
						<TD align="center" valign="center">
							<INPUT type="button" value="Reset" onclick="Reset('PageSearchSubReset')"></TD>
					</TR>
			</TABLE>
			</DIV>
			<DIV id="group_search" style="DISPLAY: none;">
			<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0" height="41">
				
					<TR>
						<TD align="center" valign="center">
							<input type="submit" name="ctrlSearchModule:ctrlPageSearchGroupOrder:btnSearch" value="Search" onclick="if (typeof(Page_ClientValidate) == 'function') Page_ClientValidate(); " language="javascript" id="ctrlSearchModule_ctrlPageSearchGroupOrder_btnSearch" /></TD>
						<TD align="center" valign="center">
							<INPUT type="button" value="Reset" onclick="Reset('SearchGroupOrderReset')"></TD>
					</TR>
				</TABLE>
			</DIV>
			
			<div id="ship_search" style="DISPLAY: none;">
			<TABLE id="Tablex" cellSpacing="0" cellPadding="0" width="100%" border="0" height="41">
					<TR>
					
						<TD align="center" valign="center">
							<input type="submit" name="ctrlSearchModule:ctrlPageSearchShippement:btnSearch" value="Search" onclick="if (typeof(Page_ClientValidate) == 'function') Page_ClientValidate(); " language="javascript" id="ctrlSearchModule_ctrlPageSearchShippement_btnSearch" /></TD>
						<TD align="center" valign="center">
							<INPUT type="button" value="Reset" onclick="Reset('PageSearchShipReset')"></TD>
					</TR>
				</TABLE>
			</div>
			<div id="creditcard_search" style="DISPLAY: none;">
			<TABLE id="Table22" cellSpacing="0" cellPadding="0" width="100%" border="0" height="41">
				
					<TR>
						<TD align="center" valign="center">
							<input type="submit" name="ctrlSearchModule:ctrlPageSearchCreditCard:btnSearch" value="Search" onclick="if (typeof(Page_ClientValidate) == 'function') Page_ClientValidate(); " language="javascript" id="ctrlSearchModule_ctrlPageSearchCreditCard_btnSearch" /></TD>
						<TD align="center" valign="center">
							<INPUT type="button" value="Reset" onclick="Reset('PageSearchCreditCard')"></TD>
					</TR>
				</TABLE>
			</div>
		</TD>
	</TR>
</TABLE>
<asp:ValidationSummary id="ValidationSummary1" runat="server" ShowSummary="False" ShowMessageBox="True"></asp:ValidationSummary>
