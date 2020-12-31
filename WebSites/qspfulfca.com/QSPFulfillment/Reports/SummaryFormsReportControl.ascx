<%@ Control Language="c#" AutoEventWireup="True" Codebehind="SummaryFormsReportControl.ascx.cs" Inherits="QSPFulfillment.Reports.SummaryFormsReportControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="DateEntry" Src="../Common/DateEntry.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<div style="TEXT-ALIGN: center">
	<table id="Table1" cellspacing="0" cellpadding="0" width="500" border="0">
		<tr>
			<td><asp:label id="lblTitle" runat="server" cssclass="CSPageTitle">Summary Forms Report</asp:label></td>
		</tr>
	</table>
	<br>
	<table cellspacing="0" cellpadding="1" width="500" bgcolor="#000000" border="0">
		<tr>
			<td>
				<table id="Table1ss" cellspacing="0" cellpadding="2" width="100%" bgcolor="#ffffff" border="0">
					<tr>
						<td></td>
						<td width="250">
							<asp:label id="Label1" cssclass="csPlainText" runat="server">Campaign ID</asp:label></td>
						<td>
							<cc1:textboxinteger id="tbxCampaignID" runat="server" errormsgregexp="The field Campaign ID has to be a number."></cc1:textboxinteger>
						</td>
					</tr>
					<tr>
						<td></td>
						<td width="250">
							<asp:label id="Label2" cssclass="csPlainText" runat="server">Group ID</asp:label></td>
						<td>
							<cc1:textboxinteger id="tbxAccountID" runat="server" errormsgregexp="The field Group ID has to be a number."></cc1:textboxinteger></td>
					</tr>
					<tr>
						<td>&nbsp;</td>
						<td width="250">
							<asp:label id="lblFMId" runat="server" cssclass="csPlainText">FM ID</asp:label></td>
						<td><cc1:dropdownlistreq id="ddlFieldManager" runat="server" width="229px" initialtext="Please select..."></cc1:dropdownlistreq><br>
							<asp:label id="lblFieldManager" runat="server" cssclass="csPlainText" visible="False"></asp:label>
						</td>
					</tr>
					<tr>
						<td></td>
						<td><asp:label id="lblDateFrom" runat="server" cssclass="csPlainText">Date From (Campaign Start Date)</asp:label></td>
						<td><uc1:dateentry id="dteStartDate" runat="server"></uc1:dateentry></td>
					</tr>
					<tr>
						<td></td>
						<td><asp:label id="lblDateTo" runat="server" cssclass="csPlainText">Date To (Campaign End Date)</asp:label></td>
						<td><uc1:dateentry id="dteEndDate" runat="server"></uc1:dateentry></td>
					</tr>
					<tr>
						<td></td>
						<td>
							<asp:label id="Label3" cssclass="csPlainText" runat="server">Approved Status Date From</asp:label></td>
						<td>
							<uc1:dateentry id="dteApprovedStatusDateFrom" runat="server"></uc1:dateentry></td>
					</tr>
					<tr>
						<td></td>
						<td>
							<asp:label id="Label4" cssclass="csPlainText" runat="server">Approved Status Date To</asp:label></td>
						<td>
							<uc1:dateentry id="dteApprovedStatusDateTo" runat="server"></uc1:dateentry></td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
	<br>
	<table cellspacing="0" cellpadding="2" width="500" bgcolor="#ffffff" border="0">
		<tr>
			<td align="center"><asp:button id="btnPreview" runat="server" text="Preview" cssclass="boxlook" onclick="btnPreview_Click"></asp:button></td>
		</tr>
	</table>
</div>
