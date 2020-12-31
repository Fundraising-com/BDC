<%@ Control Language="c#" AutoEventWireup="True" Codebehind="PhoneListMaintenanceControl.ascx.cs" Inherits="QSPFulfillment.AcctMgt.Control.PhoneListMaintenanceControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table>
	<tr>
		<td>
			<asp:label id="lblTypeTitle" runat="server" cssclass="csPlainText">Type</asp:label>
		</td>
		<td>
			<asp:label id="lblNumberTitle" runat="server" cssclass="csPlainText">Number</asp:label>
		</td>
		<td>
			<asp:label id="lblBestTimeToCallTitle" runat="server" cssclass="csPlainText">Best Time To Call</asp:label>
		</td>
	</tr>
	<asp:placeholder id="plhPhoneList" runat="server"></asp:placeholder>
</table>
<br>
<div style="TEXT-ALIGN: center">
	<asp:button id="btnAddNew" runat="server" text="Add a new Phone Number" cssclass="boxlook" causesvalidation="False" onclick="btnAddNew_Click"></asp:button>
</div>
