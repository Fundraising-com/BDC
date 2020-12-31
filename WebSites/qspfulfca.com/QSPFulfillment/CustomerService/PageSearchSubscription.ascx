<%@ Register TagPrefix="uc1" TagName="InfoSearchSubscription" Src="InfoSearchSubscription.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="PageSearchSubscription.ascx.cs" Inherits="QSPFulfillment.CustomerService.PageSearchSubscription" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<div id="PageSearchSubReset">
	<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
		<TR>
			<TD><uc1:InfoSearchSubscription id="ctrlInfoSearchSubscription" runat="server"></uc1:InfoSearchSubscription></TD>
		</TR>
		<TR vAlign="bottom" align="center">
			<TD>
				<!-- <TABLE id="Table2"  cellSpacing="0" cellPadding="0" width="100%" height="41" border="0">
					<tr>
						<td colspan=2><img src="images/cecece.gif" height="1" width="195"></td>
					</tr>
					<TR>
						<TD align="center" valign="center">
							<asp:Button id="btnSearch" runat="server" Text="Search"></asp:Button></TD>
						<TD align="center" valign="center">
							<INPUT type="button" value="Reset" onclick="Reset('PageSearchSubReset')"></TD>
					</TR>
				</TABLE> -->
			</TD>
		</TR>
	</TABLE>
</div>
