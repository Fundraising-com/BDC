<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ControlerSearchProblemCode.ascx.cs" Inherits="QSPFulfillment.CustomerService.ControlerSearchProblemCode" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<div id="divSearchProblemCode">

	<TABLE id="Table1" cellSpacing="0" cellPadding="0" border="0" width="100%">

		<TR>
			<TD valign="top">
				&nbsp;<asp:Label Runat="server" ID="lblDescription" CssClass="CSPlainText">Description</asp:Label><br>
				&nbsp;<cc1:TextBoxSearch id="tbxSearchDescription" runat="server" ParameterName="Description" width="270"></cc1:TextBoxSearch></TD>
			<TD align="center" valign="center">
				<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD align="center">
							&nbsp;&nbsp;&nbsp;<asp:Button id="btnSearch" runat="server" Text="Search" onclick="btnSearch_Click"></asp:Button>&nbsp;&nbsp;&nbsp;
						</TD>
						<TD><INPUT type="button" value="Reset" onclick="Reset('divSearchProblemCode')">&nbsp;&nbsp;&nbsp;</TD>
					</TR>
				</TABLE>
			</TD>
		</TR>
		<tr>
			<td><img src="images/spacer.gif" height="2"></td>
		</tr>
	</TABLE>
	
</div>
