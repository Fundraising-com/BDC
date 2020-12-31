<%@ Control Language="c#" AutoEventWireup="True" Codebehind="AddressViewerControl.ascx.cs" Inherits="QSPFulfillment.AcctMgt.Control.AddressViewerControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table>
	<tr>
		<td><asp:label id="lblAddressType" font-bold="True" cssclass="csPlainText" runat="server"></asp:label></td>
	</tr>
	<tr>
		<td><asp:label id="lblAddressLine1" cssclass="csPlainText" runat="server"></asp:label></td>
	</tr>
	<tr>
		<td id="rowAddressLine2" runat="server"><asp:label id="lblAddressLine2" cssclass="csPlainText" runat="server"></asp:label></td>
	</tr>
	<tr>
		<td><asp:label id="lblCity" cssclass="csPlainText" runat="server"></asp:label></td>
	</tr>
	<tr>
		<td><asp:label id="lblProvince" cssclass="csPlainText" runat="server"></asp:label></td>
	</tr>
	<tr>
		<td><asp:label id="lblPostalCode" cssclass="csPlainText" runat="server"></asp:label></td>
	</tr>
	<TR>
		<TD>
			<asp:label id="lblCountryCode" runat="server" cssclass="csPlainText" Width="88px"></asp:label></TD>
	</TR>
</table>
