<%@ Register TagPrefix="UC" TagName="StateProvince" Src="../Common/StateProvince.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="PostalAddressDisabled.ascx.cs" Inherits="QSPFulfillment.CommonWeb.UC.PostalAddressDisabled" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table cellpadding="0" cellspacing="0" border="0">
	<tr>
		<td>
			<asp:Label id="street1" runat="server" CssClass="csPlainText"></asp:Label>
		</td>
	</tr>
	<tr>
		<td>
			<asp:Label id="street2" runat="server" CssClass="csPlainText"></asp:Label>
		</td>
	</tr>
	<tr>
		<td>
			<asp:Label id="city" runat="server" CssClass="csPlainText"></asp:Label><asp:Label Runat="server" id="lblcoma" Text=", " Visible="false"></asp:Label>
			<asp:Label id="lblStateProvince" runat="server" CssClass="csPlainText"></asp:Label>
		</td>
	</tr>
	<tr>
		<td>
			<table border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td>
						<asp:Label id="PostalCode" runat="server" CssClass="csPlainText"></asp:Label>
					</td>
					<td><asp:Label id="PostalCode4" runat="server" CssClass="csPlainText"></asp:Label>
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td>
			<asp:Label id="lblCountry" runat="server" CssClass="csPlainText"></asp:Label>
		</td>
	</tr>
    <tr>
		<td>
			<asp:Label id="lblEmail" runat="server" CssClass="csPlainText"></asp:Label>
		</td>
	</tr>
	<tr>
		<td>
		</td>
	</tr>
</table>
