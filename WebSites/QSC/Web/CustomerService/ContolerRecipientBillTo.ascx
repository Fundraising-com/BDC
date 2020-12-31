<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ContolerRecipientBillTo.ascx.cs" Inherits="QSPFulfillment.CustomerService.ContolerRecipientBillTo" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="uc1" TagName="PostalAddress" Src="../Common/PostalAddress.ascx" %>
<P>
	<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
		<TR>
			<TD>
				<iewc:tabstrip id="TabStrip1" TabSelectedStyle="background-color:#ffffff;color:#000000" TabHoverStyle="background-color:#777777"
					TabDefaultStyle="background-color:#000000;font-family:verdana;font-weight:bold;font-size:8pt;color:#ffffff;width:79;height:21;text-align:center"
					runat="server" TargetID="mupMain">
					<iewc:Tab Text="Recipient"></iewc:Tab>
					<iewc:Tab Text="Bill To"></iewc:Tab>
					<iewc:tab Text="Ship To"></iewc:tab>
				</iewc:tabstrip></TD>
		</TR>
		<TR>
			<TD>
				<iewc:multipage id="mupMain" runat="server">
					<iewc:pageview id="pavRecipient">
						<uc1:PostalAddress id="ctrlPostalAddressRecipient" runat="server"></uc1:PostalAddress>
					</iewc:pageview>
					<iewc:pageview id="pavBillTo">
						<uc1:PostalAddress id="ctrlPostalAddressBillTo" runat="server"></uc1:PostalAddress>
					</iewc:pageview>
					<iewc:pageview id="pavShipTo">
						<uc1:PostalAddress id="ctrlPostalAddressShipTo" runat="server"></uc1:PostalAddress>
					</iewc:pageview>
				</iewc:multipage></TD>
		</TR>
	</TABLE>
</P>
