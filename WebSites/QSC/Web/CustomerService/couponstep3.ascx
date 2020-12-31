<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="couponstep3.ascx.cs" Inherits="QSPFulfillment.CustomerService.couponstep3" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<BR>
<TABLE class="CSTable" id="Table1" cellSpacing="0" cellPadding="2" width="100%">
	<TR>
		<TD class="CSTableHeader" colSpan="2">Customer Information</TD>
	</TR>
	<TR>
		<TD>
			<TABLE id="Table3" width="100%">
				<TR>
					<TD width="250">
						<asp:Label id="Label1" runat="server" cssClass="csPlainText">First Name</asp:Label></TD>
					<TD>
						<asp:Label id="lblFirstName" runat="server" CssClass="CSPlainText"></asp:Label></TD>
				</TR>
				<TR>
					<TD>
						<asp:Label id="Label2" runat="server" cssClass="csPlainText">Last Name</asp:Label></TD>
					<TD>
						<asp:Label id="lblLastName" runat="server" CssClass="CSPlainText"></asp:Label></TD>
				</TR>
				<TR>
					<TD>
						<asp:Label id="Label3" runat="server" cssClass="csPlainText"> Address Line 1</asp:Label></TD>
					<TD>
						<asp:Label id="lblStreet1" runat="server" CssClass="CSPlainText"></asp:Label></TD>
				</TR>
				<TR>
					<TD>
						<asp:Label id="Label4" runat="server" cssClass="csPlainText">Address Line 2</asp:Label></TD>
					<TD>
						<asp:Label id="lblStreet2" runat="server" CssClass="CSPlainText"></asp:Label></TD>
				</TR>
				<TR>
					<TD>
						<asp:Label id="Label33" runat="server" cssClass="csPlainText">City</asp:Label></TD>
					<TD>
						<asp:Label id="lblCity" runat="server" CssClass="CSPlainText"></asp:Label></TD>
				</TR>
				<TR>
					<TD>
						<asp:Label id="Label7" runat="server" cssClass="csPlainText">Postal Code</asp:Label></TD>
					<TD>
						<asp:Label id="lblPostalCode" runat="server" CssClass="CSPlainText"></asp:Label></TD>
				</TR>
				<TR>
					<TD>
						<asp:Label id="Label5" runat="server" cssClass="csPlainText">Province</asp:Label></TD>
					<TD>
						<asp:Label id="lblProvince" runat="server" CssClass="CSPlainText"></asp:Label></TD>
				</TR>
				<TR>
					<TD>
						<asp:Label id="Label6" runat="server" cssClass="csPlainText"> Country</asp:Label></TD>
					<TD>
						<asp:Label id="lblCountry" runat="server" CssClass="CSPlainText"></asp:Label></TD>
				</TR>
                <TR>
					<TD>
						<asp:Label id="Label14" runat="server" cssClass="csPlainText"> Email</asp:Label></TD>
					<TD>
						<asp:Label id="lblEmail" runat="server" CssClass="CSPlainText"></asp:Label></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD class="CSTableHeader" colSpan="2">Certificate&nbsp;Information</TD>
	</TR>
	<TR>
		<TD colSpan="2">
			<TABLE id="Table2" width="100%">
				<TR>
					<TD width="250">
						<asp:Label id="Label15" runat="server" cssClass="csPlainText">Certificate Number</asp:Label>&nbsp;</TD>
					<TD>
						<asp:Label id="lblCoupon" runat="server" CssClass="CSPlainText"></asp:Label></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD class="CSTableHeader" colSpan="2">Order&nbsp;Information</TD>
	</TR>
	<TR>
		<TD colSpan="2">
			<TABLE id="Table4" width="100%">
				<TR>
					<TD width="250">
						<asp:Label id="Label12" runat="server" cssClass="csPlainText">Invoice Order</asp:Label>&nbsp;</TD>
					<TD>
						<asp:Label id="lblInvoiceOrder" runat="server" CssClass="CSPlainText"></asp:Label></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD class="CSTableHeader" colSpan="2">Magazine Information</TD>
	</TR>
	<TR>
		<TD>
			<TABLE id="Table1" cellSpacing="0" cellPadding="2" border="0" width="100%">
				<TR>
					<TD width="250">
						<asp:Label id="Label8" runat="server" cssClass="CSPlainText">Title Code</asp:Label></TD>
					<TD>
						<asp:Label id="lblTitleCode" runat="server" cssClass="CSPlainText"></asp:Label></TD>
				<TR>
					<TD>
						<asp:Label id="Label9" runat="server" cssClass="CSPlainText">Magazine Title</asp:Label></TD>
					<TD>
						<asp:Label id="lblMagazineTitle" runat="server" cssClass="CSPlainText"></asp:Label></TD>
				</TR>
				<TR>
					<TD>
						<asp:Label id="Label10" runat="server" cssClass="CSPlainText">Term</asp:Label></TD>
					<TD>
						<asp:Label id="lblTerm" runat="server" cssClass="CSPlainText"></asp:Label></TD>
				</TR>
				<TR>
					<TD>
						<asp:Label id="Label11" runat="server" cssClass="CSPlainText">New or Renewal</asp:Label></TD>
					<TD>
						<asp:Label id="lblNewRenewal" runat="server" CssClass="CSPlainText"></asp:Label></TD>
				</TR>
				<TR>
					<TD>
						<asp:Label id="Label13" runat="server" cssClass="CSPlainText">Catalog Price</asp:Label></TD>
					<TD>
						<asp:Label id="lblCatalogPrice" runat="server" cssClass="CSPlainText"></asp:Label></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD align="center"></TD>
	</TR>
</TABLE>
