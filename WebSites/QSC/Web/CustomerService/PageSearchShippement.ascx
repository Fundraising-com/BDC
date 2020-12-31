<%@ Control Language="c#" AutoEventWireup="True" Codebehind="PageSearchShippement.ascx.cs" Inherits="QSPFulfillment.CustomerService.PageSearchShippement" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="InfoSearchShippement" Src="InfoSearchShippement.ascx" %>
<div id="PageSearchShipReset">
	<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
		<TR>
			<TD>
				<br>
					<uc1:InfoSearchShippement id="ctrlInfoSearchShippement" runat="server"></uc1:InfoSearchShippement>
			</TD>
		</TR>
		<TR align="center" valign="bottom">
			<TD>
				<!--<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0" height="41">
					<tr>
						<td colspan=2><img src="images/cecece.gif" height="1" width="195"></td>
					</tr><TR>
					
						<TD align="center" valign="center">
							<asp:Button id="btnSearch" runat="server" Text="Search"></asp:Button></TD>
						<TD align="center" valign="center">
							<INPUT type="button" value="Reset" onclick="Reset('PageSearchShipReset')"></TD>
					</TR>
				</TABLE>-->
			</TD>
		</TR>
	</TABLE>
</div>
