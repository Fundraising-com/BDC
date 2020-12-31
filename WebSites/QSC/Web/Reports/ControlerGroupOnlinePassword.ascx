<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ControlerGroupOnlinePassword.ascx.cs" Inherits="QSPFulfillment.Reports.ControlerGroupOnlinePassword" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="DateEntry" Src="../Common/DateEntry.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CommonWeb" Assembly="QSPFulfillment" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<div style="TEXT-ALIGN: center">
	<table id="Table1" cellspacing="0" cellpadding="0" width="500" border="0">
		<tr>
			<td><asp:label id="lblTitle" cssclass="CSPageTitle" runat="server">Group Online Passwords</asp:label></td>
		</tr>
	</table>
	<br>
	<table cellspacing="0" cellpadding="1" width="500" bgcolor="#000000" border="0">
		<tr>
			<td>
				<table id="Table1ss" cellspacing="0" cellpadding="2" width="100%" bgcolor="#ffffff" border="0">
					<tr>
						<td width="250"><asp:label id="Label1" cssclass="csPlainText" runat="server">Field Manager</asp:label></td>
						<td><cc1:dropdownlistsearch id="ddlFieldManager" runat="server" parametername="sFMID" width="229px" contenttype="string"></cc1:dropdownlistsearch><asp:label id="lblFieldManager" cssclass="csPlainText" runat="server" visible="False"></asp:label></td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
	<br>
	<table cellspacing="0" cellpadding="2" width="500" bgcolor="#ffffff" border="0">
		<tr>
			<td align="center"><asp:button id="btnPreview" runat="server" text="Preview"></asp:button></td>
		</tr>
	</table>
	<cc2:rsgeneration id="rsGenerationGroupOnlinePassword" runat="server" reportname="GroupOnlinePassword" mode="PopUp"></cc2:rsgeneration>
</div>
