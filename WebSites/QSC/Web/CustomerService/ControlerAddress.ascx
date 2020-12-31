<%@ Register TagPrefix="uc1" TagName="PostalAddressDisabled" Src="../Common/PostalAddressDisabled.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ControlerAddress.ascx.cs" Inherits="QSPFulfillment.CustomerService.ControlerAddress" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="ControlerAddressHistory" Src="ControlerAddressHistory.ascx" %>
<TABLE id="Table1">
	<TR>
		<TD>
			<asp:CheckBox Enabled="false" id="cbxValideAddress" runat="server" Text="Is Valid Address" Visible="False"></asp:CheckBox>
			<asp:CheckBox Enabled="false" id="cbxInfoMissing" runat="server" Text="Info Missing" Visible="False"></asp:CheckBox></TD>
	</TR>
	<TR>
		<TD>
			<asp:Label id="lblName" runat="server" CssClass="CSPlainText"></asp:Label>
		</TD>
	</TR>
	<TR>
		<TD>
			<uc1:PostalAddressDisabled id="ctrlPostalAddress" runat="server"></uc1:PostalAddressDisabled></TD>
	</TR>
	<TR runat="server" id="rowPhone">
		<TD>
			<asp:Label id="lblPhone" runat="server" CssClass="CSPlainText"></asp:Label></TD>
	</TR>
</TABLE>
