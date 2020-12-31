<%@ Control Language="c#" AutoEventWireup="True" Codebehind="PremiumMaintenanceControl.ascx.cs" Inherits="QSPFulfillment.MarketingMgt.Control.PremiumMaintenanceControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<table class="CSTable" id="Table2" cellspacing="0" cellpadding="2">
	<tbody>
		<tr>
			<td align="center">
				<table id="Table3">
					<tbody>
						<tr>
							<td colspan="2"></td>
						</tr>
						<tr>
							<td><br>
								<asp:label id="Label1" cssclass="csPlainText" runat="server">Premium Code *</asp:label></td>
							<td><br>
								<cc1:textboxreq id="tbxPremiumCode" runat="server" errormsgrequired="The field Premium Code is mandatory."
									required="True"></cc1:textboxreq></td>
						</tr>
						<tr>
							<td><asp:label id="Label3" cssclass="csPlainText" runat="server">Year *</asp:label></td>
							<td><cc1:dropdownlistreq id="ddlYear" runat="server" errormsgrequired="Please select a year." initialvalue="0"
									required="True"></cc1:dropdownlistreq></td>
						</tr>
						<tr>
							<td><asp:label id="Label8" cssclass="csPlainText" runat="server">Season *</asp:label></td>
							<td><cc1:dropdownlistreq id="ddlSeason" runat="server" errormsgrequired="Please select a season." required="True"></cc1:dropdownlistreq></td>
						</tr>
						<tr>
							<td><asp:label id="Label2" cssclass="csPlainText" runat="server">Is Active *</asp:label></td>
							<td><cc1:checkboxsearch id="chkIsActive" runat="server" parametername="IsActive" contenttype="int"></cc1:checkboxsearch></td>
						</tr>
						<tr>
							<td style="HEIGHT: 21px"><asp:label id="Label4" cssclass="csPlainText" runat="server">English Description</asp:label></td>
							<td style="HEIGHT: 21px"><cc1:textboxsearch id="tbxEnglishName" runat="server" parametername="EnglishName" contenttype="string"
									maxlength="100" columns="60"></cc1:textboxsearch></td>
						</tr>
						<tr>
							<td><asp:label id="Label9" runat="server" cssclass="csPlainText"> French Description</asp:label></td>
							<td>
								<cc1:textboxsearch id="tbxFrenchName" runat="server" parametername="FrenchName" contenttype="string"
									maxlength="100" columns="60"></cc1:textboxsearch></td>
						</tr>
						<tr>
							<td style="HEIGHT: 34px" colspan="2"></td>
						</tr>
						<tr>
							<td align="right" colspan="2"><asp:button id="btnSubmit" runat="server" text="Submit" cssclass="boxlook" onclick="btnSubmit_Click"></asp:button>
								<asp:button id="btnCancel" runat="server" text="Cancel" causesvalidation="False" cssclass="boxlook" onclick="btnCancel_Click"></asp:button></td>
						</tr>
					</tbody>
				</table>
			</td>
		</tr>
	</tbody>
</table>
</TABLE></TR></TBODY></TABLE></TR></TBODY></TABLE>
