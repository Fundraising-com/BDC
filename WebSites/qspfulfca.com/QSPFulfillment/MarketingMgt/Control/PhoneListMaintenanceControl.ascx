<%@ Control Language="c#" AutoEventWireup="True" Codebehind="PhoneListMaintenanceControl.ascx.cs" Inherits="QSPFulfillment.MarketingMgt.Control.PhoneListMaintenanceControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table id="Table4" cellspacing="1" cellpadding="2" width="100%">
	<tr>
		<td valign="top">
			<table width="100%">
				<tr>
					<td style="WIDTH: 25%">
						<asp:label id="lblTypeTitle" runat="server" cssclass="csPlainText">Type</asp:label>
					</td>
					<td style="WIDTH: 25%">
						<asp:label id="lblNumberTitle" runat="server" cssclass="csPlainText">Number</asp:label>
					</td>
					<td style="WIDTH: 25%">
						<asp:label id="lblBestTimeToCallTitle" runat="server" cssclass="csPlainText">Best Time To Call</asp:label>
					</td>
					<td style="WIDTH: 25%">
						&nbsp;
					</td>
				</tr>
				<asp:placeholder id="plhPhoneList" runat="server"></asp:placeholder>
			</table>
			<br>
			<div style="TEXT-ALIGN: left;">
				<asp:button id="btnAddNew" runat="server" text="Add a new Phone Number" cssclass="boxlook" causesvalidation="False" onclick="btnAddNew_Click"></asp:button>
			</div>
		</td>
	</tr>
</table>
