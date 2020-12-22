<%@ Import Namespace="csr.CSRBusinessRule"%>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="AddEditUserRoles.ascx.vb" Inherits="StoreFront.StoreFront.AddEditUserRoles" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td class="contenttableheader" width="1"><IMG src="../images/clear.gif"></td>
		<td class="contenttableheader" width="100%">&nbsp;<asp:label id="lblHeading" Runat="server"></asp:label></td>
		<td class="contenttableheader" width="1"><IMG src="../images/clear.gif"></td>
	</tr>
	<tr>
		<td class="contenttable" width="1"><IMG src="../images/clear.gif"></td>
		<td class="content" width="100%">&nbsp;Role Name:&nbsp;&nbsp;<ASP:TEXTBOX id="txtname" Runat="server"></ASP:TEXTBOX>&nbsp;<FONT color="#ff0000">*</FONT><FONT color="#ff0000"></FONT></td>
		<td class="contenttable" width="1"></td>
	</tr>
	<tr>
		<td class="contenttable" width="100%" colSpan="5"><IMG src="../images/clear.gif"></td>
	</tr>
</table>
<br>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TBODY>
		<tr>
			<td class="contenttableheader" width="1"><IMG src="../images/clear.gif" width="1"></td>
			<td class="contenttableheader" width="30%">&nbsp;Store Management</td>
			<td class="contenttableheader" width="30%">Marketing &amp; Promotions</td>
			<td class="contenttableheader" width="40%">Store Inventory</td>
			<td class="contenttableheader" width="1"><IMG src="../images/clear.gif" width="1"></td>
		</tr>
		<tr>
			<td class="contenttableheader" width="1"><IMG src="../images/clear.gif" width="1"></td>
			<td class="content" vAlign="top" width="30%">
				<asp:checkboxlist id="chkManagement" Runat="server" RepeatColumns="1" Width="100%" CssClass="content"></asp:checkboxlist>
				<table class="content" border="0" style="width:100%;">
					<tr>
						<td><asp:CheckBox id="chkManageCSROptions" runat="server" Text="Manage CSR Options"></asp:CheckBox></td>
					</tr>
				</table>
			</td>
			<td class="content" vAlign="top" width="30%"><asp:checkboxlist id="chkMarkPromo" Runat="server" RepeatColumns="1" Width="100%" CssClass="content"></asp:checkboxlist></td>
			<td class="content" vAlign="top" width="40%"><asp:checkbox id="chkImportProd" runat="server" Text="Import products"></asp:checkbox><br>
				&nbsp;Products<br>
				<asp:panel id="pnlProduct" Runat="server">&nbsp;&nbsp;&nbsp; 
<asp:CheckBox id="chkProdDelete" runat="server" Text="Delete"></asp:CheckBox><BR>&nbsp;&nbsp;&nbsp; 
<asp:CheckBox id="chkProdAddNew" runat="server" Text="Add New"></asp:CheckBox>&nbsp;(requires General)<BR>&nbsp;&nbsp;&nbsp; 
<asp:CheckBox id="chkProdGeneral" runat="server" Text="General"></asp:CheckBox><BR>&nbsp;&nbsp;&nbsp; 
<asp:CheckBox id="chkProdDetails" runat="server" Text="Details"></asp:CheckBox><BR>&nbsp;&nbsp;&nbsp; 
<asp:CheckBox id="chkProdCategories" runat="server" Text="Categories"></asp:CheckBox><BR>&nbsp;&nbsp;&nbsp; 
<asp:CheckBox id="chkProdAttributes" runat="server" Text="Attributes"></asp:CheckBox><BR>&nbsp;&nbsp;&nbsp; 
<asp:CheckBox id="chkProdImages" runat="server" Text="Image Control"></asp:CheckBox><BR>&nbsp;&nbsp;&nbsp; 
<asp:CheckBox id="chkFulfillment" runat="server" Text="Fulfillment"></asp:CheckBox><BR>&nbsp;&nbsp;&nbsp; 
<asp:checkbox id="chkInventory" runat="server" Text="Inventory"></asp:checkbox><BR>&nbsp;&nbsp;&nbsp; 
<asp:checkbox id="chkDiscounts" runat="server" Text="Discounts"></asp:checkbox><BR>&nbsp;&nbsp;&nbsp; 
<asp:checkbox id="chkMarketing" runat="server" Text="Marketing"></asp:checkbox><br>&nbsp;&nbsp;&nbsp; 
<asp:checkbox id="chkBundleComponents" runat="server" Text="Bundle Components"></asp:checkbox><BR>&nbsp;&nbsp;&nbsp;
<asp:checkbox id="chkCustomerDefinedRules" runat="server" Text="Customer Defined Rules"></asp:checkbox></asp:panel>
				<asp:checkbox id="chkAttributes" Runat="server" Text="Attributes"></asp:checkbox><br>
				<asp:checkbox id="chkCategories" runat="server" Text="Categories"></asp:checkbox><BR>
				<asp:checkbox id="chkManufacturers" runat="server" Text="Manufacturers"></asp:checkbox><BR>
				<asp:checkbox id="chkVendors" runat="server" Text="Vendors"></asp:checkbox><BR>
				<asp:checkbox id="chkSearchFilters" runat="server" Text="Search Result Filters"></asp:checkbox></td>
			<td class="contenttableheader" width="1"><IMG src="../images/clear.gif" width="1"></td>
		</tr>
		<tr>
			<td class="contenttable" width="100%" colSpan="5"><IMG src="../images/clear.gif"></td>
		</tr>
	</TBODY></table>
<br>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td class="contenttableheader" width="1"><IMG src="../images/clear.gif" width="1"></td>
		<td class="contenttableheader" width="30%">&nbsp;Store Design</td>
		<td class="contenttableheader" width="70%" colSpan="2">Store Settings</td>
		<td class="contenttableheader" width="1"><IMG src="../images/clear.gif" width="1"></td>
	</tr>
	<tr>
		<td class="contenttable" width="1"><IMG src="../images/clear.gif" width="1"></td>
		<td class="content" vAlign="top" width="30%"><asp:checkboxlist id="chkDesign" Runat="server" RepeatColumns="1" Width="100%" CssClass="content"></asp:checkboxlist></td>
		<td class="content" width="70%"><asp:checkbox id="chkGeneral" runat="server" Text="General"></asp:checkbox><br>
			<asp:checkbox id="chkOnlineChat" runat="server" Text="Online Chat"></asp:checkbox><br>
			<asp:checkbox id="chkEmail" runat="server" Text="E-Mail"></asp:checkbox><br>
			<asp:checkbox id="chkShipping" runat="server" Text="Shipping"></asp:checkbox><BR>
			&nbsp;Payments
			<asp:panel id="pnlpayment" Runat="server">&nbsp;&nbsp;&nbsp; 
<asp:CheckBox id="chkPaymentMethods" runat="server" Text="Payment Methods"></asp:CheckBox><BR>&nbsp;&nbsp;&nbsp; 
<asp:CheckBox id="chkOnlineProcessing" runat="server" Text="Online Processing"></asp:CheckBox><BR>&nbsp;&nbsp;&nbsp; 
<asp:CheckBox id="ChkEncryption" runat="server" Text="Encryption"></asp:CheckBox><BR>&nbsp;&nbsp;&nbsp; 
<asp:CheckBox id="chkPayPal" runat="server" Text="Pay Pal"></asp:CheckBox></asp:panel><asp:checkbox id="chkLocalization" runat="server" Text="Localization"></asp:checkbox><br>
			<asp:checkbox id="chkTax" runat="server" Text="Tax"></asp:checkbox><BR>
			<asp:checkbox id="chkWebServices" runat="server" Text="StoreFront Connector "></asp:checkbox><br>
			<asp:checkbox id="chkMappedURLs" runat="server" Text="Mapped URLs"></asp:checkbox></td>
		<td class="contenttable" width="1"><IMG src="../images/clear.gif" width="1"></td>
	</tr>
	<tr>
		<td class="contenttable" width="1" style="height: 19px"><IMG src="../images/clear.gif"></td>
		<td class="contenttable" width="100%" colSpan="2" style="height: 19px"></td>
		<td class="contenttable" width="1" style="height: 19px"><IMG src="../images/clear.gif"></td>
	</tr>
</table>
<div align="right"><br>
	<INPUT id="hdnTitle" type="hidden" name="hdnTitle" runat="server">
	<asp:linkbutton id="cmdCancel" Runat="server">
		<asp:image id="Img2" Runat="server" ImageUrl="../images/cancel.jpg"></asp:image>
	</asp:linkbutton>&nbsp;&nbsp;
	<asp:linkbutton id="cmdSave" Runat="server">
		<asp:image id="img1" Runat="server" ImageUrl="../images/save.jpg"></asp:image>
	</asp:linkbutton></div>
