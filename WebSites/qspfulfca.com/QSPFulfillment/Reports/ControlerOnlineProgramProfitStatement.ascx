<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ControlerOnlineProgramProfitStatement.ascx.cs" Inherits="QSPFulfillment.Reports.ControlerOnlineProgramProfitStatement" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="DateEntry" Src="../Common/DateEntry.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CommonWeb" Assembly="QSPFulfillment" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<div style="TEXT-ALIGN: center">
	<table id="Table1" cellspacing="0" cellpadding="0" width="500" border="0">
		<tr>
			<td><asp:label id="lblTitle" cssclass="CSPageTitle" runat="server">OnLine Program Profit Statement Report</asp:label></td>
		</tr>
	</table>
</div>
<div style="TEXT-ALIGN: center">&nbsp;</div>
<div style="TEXT-ALIGN: center">&nbsp;</div>
<div style="TEXT-ALIGN: center"><br>
</div>
<div style="TEXT-ALIGN: center">
	<table cellspacing="0" cellpadding="1" width="500" bgcolor="#000000" border="0">
		<tr>
			<td>
				<table id="Table1ss" cellspacing="0" cellpadding="10" width="100%" bgcolor="#ffffff" border="0">
					<tr>
						<td width="250"><asp:label id="lblGenerateBy" cssclass="csPlainText" runat="server">Generate by:</asp:label></td>
						<td><cc1:radiobuttonsearch id="rbsGenerateBy" runat="server" parametername="Over100" contenttype="int" cssclass="csPlainText"
								text="All Over 100$" checked="True" groupname="GenerateBy"></cc1:radiobuttonsearch><br>
							<asp:radiobutton id="rbtAll" runat="server" cssclass="csPlainText" text="All" groupname="GenerateBy"></asp:radiobutton>
							<table cellpadding="0" cellspacing="0" border="0">
								<tr>
									<td>
										<asp:radiobutton id="rbtCampaignID" runat="server" cssclass="csPlainText" text="Campaign ID" groupname="GenerateBy"></asp:radiobutton>
									</td>
									<td style="PADDING-LEFT: 15px">
										<cc1:textboxsearch id="tbxCampaignID" runat="server" parametername="CampaignID" contenttype="int" maxlength="7"
											columns="7"></cc1:textboxsearch>
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td width="250">
							<asp:label id="Label1" cssclass="csPlainText" runat="server">Field Manager</asp:label></td>
						<td>
							<cc1:dropdownlistsearch id="ddlFieldManager" runat="server" contenttype="string" parametername="FMID" width="229px"></cc1:dropdownlistsearch>
							<asp:label id="lblFieldManager" runat="server" cssclass="csPlainText" visible="False"></asp:label></td>
					</tr>
					<tr>
						<td><asp:label id="lblDateFrom" runat="server" cssclass="csPlainText">Date From</asp:label></td>
						<td><uc1:dateentry id="ctrlDateEntryFrom" runat="server" parametername="DateFrom" contenttype="DateTime"></uc1:dateentry></td>
					</tr>
					<tr>
						<td><asp:label id="lblDateTo" runat="server" cssclass="csPlainText">Date To</asp:label></td>
						<td><uc1:dateentry id="ctrlDateEntryTo" runat="server" parametername="DateTo" contenttype="DateTime"></uc1:dateentry></td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
</div>
<div style="TEXT-ALIGN: center"><br>
</div>
<div style="TEXT-ALIGN: center">
	<table cellspacing="0" cellpadding="2" width="500" bgcolor="#ffffff" border="0">
		<tr>
			<td align="center"><asp:button id="btnPreview" runat="server" text="Preview"></asp:button></td>
		</tr>
	</table>
</div>
<cc2:rsgeneration id="rsGenerationOnlineProgramProfitStatement" runat="server" reportname="OnlineProgramProfitStatement" mode="PopUp"></cc2:rsgeneration>
