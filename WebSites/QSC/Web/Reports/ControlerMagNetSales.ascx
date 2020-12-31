<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ControlerMagNetSales.ascx.cs" Inherits="QSPFulfillment.Reports.ControlerMagNetSales" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="DateEntry" Src="../Common/DateEntry.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CommonWeb" Assembly="QSPFulfillment" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<!--
<script language="javascript">

	function SetTitleCode(Code,Description)
	{
		var tbxTitleCode = document.getElementById('ctrlControlerGenerateSwitchLetter_tbxTitleCode');
		tbxTitleCode.value = Code;
		
		var lblDescription = document.getElementById('ctrlControlerGenerateSwitchLetter_lblCodeDescription');
		lblDescription.innerHTML = Description;
		
		tbxTitleCode.focus();
	
	}

</script>
-->
<div style="TEXT-ALIGN: center">
	<table id="Table1" cellspacing="0" cellpadding="0" width="500" border="0">
		<tr>
			<td><asp:label id="lblTitle" runat="server" cssclass="CSPageTitle"> MagNet Sales Report</asp:label></td>
		</tr>
	</table>
	<br>
	<table cellspacing="0" cellpadding="1" width="500" bgcolor="#000000" border="0">
		<tr>
			<td>
				<table id="Table1ss" cellspacing="0" cellpadding="2" width="100%" bgcolor="#ffffff" border="0">
					<tr>
						<td></td>
						<td><asp:label id="lblDateFrom" runat="server" cssclass="csPlainText">Date From (Campaign Start Date)</asp:label></td>
						<td><uc1:dateentry id="ctrlDateEntryFrom" runat="server" contenttype="DateTime" parametername="DateFrom"></uc1:dateentry></td>
					</tr>
					<tr>
						<td></td>
						<td><asp:label id="lblDateTo" runat="server" cssclass="csPlainText">Date To (Campaign Start Date)</asp:label></td>
						<td><uc1:dateentry id="ctrlDateEntryTo" runat="server" contenttype="DateTime" parametername="DateTo"></uc1:dateentry></td>
					</tr>
					<tr>
						<td></td>
						<td width="250"><asp:label id="Label1" cssclass="csPlainText" runat="server">Field Manager</asp:label></td>
						<td><cc1:dropdownlistsearch id="ddlFieldManager" runat="server" parametername="FMID" width="229px" contenttype="string"></cc1:dropdownlistsearch><asp:label id="lblFieldManager" cssclass="csPlainText" runat="server" visible="False"></asp:label></td>
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
</div>
<cc2:rsgeneration id="rsGenerationMagNetSales" runat="server" reportname="MagNetSales" mode="PopUp"></cc2:rsgeneration>
