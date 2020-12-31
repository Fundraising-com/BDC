<%@ Control Language="c#" AutoEventWireup="True" Codebehind="PageSearchGroupOrder.ascx.cs" Inherits="QSPFulfillment.CustomerService.PageSearchGroupOrder" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="PageSearchGroup" Src="PageSearchGroup.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PageSearchOrder" Src="PageSearchOrder.ascx" %>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<div id="SearchGroupOrderReset">
	<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0 bgcolor="#FFFFFF" background="images/gr_tab_bg.gif">
		<TR>
			<TD align="center">
				<iewc:TabStrip id="tbsMain"
					runat="server" targetID="mupMain">
					<iewc:Tab Text="" DefaultImageUrl="images/tabaccount_off.gif" SelectedImageUrl="images/tabaccount_on.gif"></iewc:Tab>
					<iewc:Tab Text="" DefaultImageUrl="images/taborder_off.gif" SelectedImageUrl="images/taborder_on.gif"></iewc:Tab>
				</iewc:TabStrip></TD>
		</tr>
		<tr>
			<td><img src="images/spacer.gif" height="10"></td>
		</TR>
	</table>
	<TABLE id="Table" cellSpacing="0" cellPadding="0" width="100%" border="0">
		<TR>
			<TD>
				<iewc:MultiPage id="mupMain" runat="server">
					<iewc:pageview id="pavPageSearchGroup">
						<uc1:PageSearchGroup id="ctrlPageSearchGroup" runat="server"></uc1:PageSearchGroup>
					</iewc:pageview>
					<iewc:pageview id="pavPageSearchOrder">
						<uc1:PageSearchOrder id="ctrlPageSearchOrder" runat="server"></uc1:PageSearchOrder>
					</iewc:pageview>
				</iewc:MultiPage>
			</TD>
		</TR>
	</table>
	<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0"height="40">
		<TR>
			<TD>
				<!--<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0" height="41">
				<tr>
						<td colspan=2><img src="images/cecece.gif" height="1" width="195"></td>
					</tr>
					<TR>
						<TD align="center" valign="center">
							<asp:Button id="btnSearch" runat="server" Text="Search"></asp:Button></TD>
						<TD align="center" valign="center">
							<INPUT type="button" value="Reset" onclick="Reset('SearchGroupOrderReset')"></TD>
					</TR>
				</TABLE>-->
			</TD>
		</TR>
	</TABLE>
</div>
