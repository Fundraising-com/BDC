<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ControlerPrintInfo.ascx.cs" Inherits="QSPFulfillment.CustomerService.ControlerPrintInfo" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="ControlerAddress" Src="ControlerAddress.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ControlerAddressHistory" Src="ControlerAddressHistory.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ControlerSubscriptionStatusHistory" Src="ControlerSubscriptionStatusHistory.ascx" %>
<br>
<div id="divPrint" runat="server">
	<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%" border="0">
		<TR>
			<TD vAlign="top" align="center"><br>
				<TABLE class="CSTable" id="Table3" cellSpacing="0" cellPadding="2" width="90%">
					<TR class="CSTableHeader">
						<TD>
							<asp:Label id="Label5" runat="server" Font-Bold="True">Recipient Address</asp:Label>
						</TD>
					</TR>
					<TR>
						<TD>
							<uc1:controleraddress id="ctrlControlerAddressRecipient" runat="server"></uc1:controleraddress></TD>
					</TR>
				</TABLE>
			</TD>
			<TD vAlign="top" align="center"><br>
				<TABLE class="csTable" id="Table8" cellSpacing="0" cellPadding="2" width="90%" border="0">
					<TR class="csTableHeader">
						<TD vAlign="top">
							<asp:label id="Label7" runat="server" Font-Bold="True">Magazine Information</asp:label></TD>
						<TD></TD>
					</TR>
					<TR>
						<TD class="csTableSubHeader">
							<asp:label id="Label23" runat="server" CssClass="csPlainText">Magazine Title</asp:label></TD>
						<TD class="csTableItems">
							<asp:label id="lblMagazineTitle" runat="server"></asp:label></TD>
					</TR>
					<TR>
						<TD class="csTableSubHeader">
							<asp:label id="Label13" runat="server" CssClass="csPlainText">Title Code</asp:label></TD>
						<TD class="csTableItems">
							<asp:label id="lblTitleCode" runat="server"></asp:label></TD>
					</TR>
					<TR>
						<TD class="csTableSubHeader">
							<asp:label id="Label27" runat="server" CssClass="csPlainText">Magazine Status</asp:label></TD>
						<TD class="csTableItems">
							<asp:label id="lblMagazineStatus" runat="server"></asp:label></TD>
					</TR>
					<TR>
						<TD class="csTableSubHeader">
							<asp:label id="Label46" runat="server" CssClass="csPlainText">Fulfillment House Name</asp:label></TD>
						<TD class="csTableItems">
							<asp:label id="lblFulfillmentHouseName" runat="server"></asp:label></TD>
					</TR>
					<TR>
						<TD class="csTableSubHeader">
							<asp:label id="Label37" runat="server" CssClass="csPlainText">Fullfillment House Contact</asp:label></TD>
						<TD class="csTableItems">
							<asp:label id="lblFullContact" runat="server"></asp:label></TD>
					</TR>
					<TR>
						<TD class="csTableSubHeader">
							<asp:Label id="Label21" runat="server" CssClass="csPlainText">Price Entered</asp:Label></TD>
						<TD class="csTableItems">
							<asp:Label id="lblPriceEntered" runat="server"></asp:Label></TD>
					</TR>
					<TR>
						<TD class="csTableSubHeader">
							<asp:Label id="Label25" runat="server" CssClass="csPlainText">Catalog Price</asp:Label></TD>
						<TD class="csTableItems">
							<asp:Label id="lblCatalogPrice" runat="server"></asp:Label></TD>
					</TR>
					<TR>
						<TD class="csTableSubHeader" style="HEIGHT: 22px">
							<asp:Label id="Label29" runat="server" CssClass="csPlainText">Base Price</asp:Label></TD>
						<TD class="csTableItems" style="HEIGHT: 22px">
							<asp:Label id="lblPrice" runat="server"></asp:Label></TD>
					</TR>
				</TABLE>
			</TD>
		</TR>
		<TR>
			<TD vAlign="top" align="center" colspan="2">
			<br>
				<uc1:ControlerSubscriptionStatusHistory id="ctrlControlerSubscriptionStatusHistory" runat="server"></uc1:ControlerSubscriptionStatusHistory>
			</TD>
		</TR>
	</TABLE>
</div>