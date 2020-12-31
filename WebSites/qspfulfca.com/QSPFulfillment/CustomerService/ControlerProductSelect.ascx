<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ControlerProductSelect.ascx.cs" Inherits="QSPFulfillment.CustomerService.ControlerProductSelect" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CustomerService" Assembly="QSPFulfillment" %>
<br>
<div id="divSearch" runat="server">
	<table id="Table3" width="100%" cellspacing="0" cellpadding="0" bgcolor="#cecece" border="0">
		<tr>
			<td>
				<table id="Table4" width="100%" cellspacing="1" cellpadding="2">
					<tr>
						<td valign="top" height="20"><asp:label runat="server" id="lblTitle2" cssclass="CSTitle">Search</asp:label>
						</td>
					</tr>
					<tr bgcolor="#ffffff">
						<td valign="top">
							<table id="Table1" cellspacing="0" cellpadding="2" border="0" width="100%">
								<tr>
									<td width="70"><asp:label id="Label1s" runat="server" cssclass="CSPlainText">Title Code</asp:label><br>
										<asp:textbox id="tbxTitleCode" runat="server" width="70"></asp:textbox></td>
									<td width="300"><asp:label id="Label3s" runat="server" cssclass="CSPlainText">Title</asp:label><br>
										<asp:textbox id="tbxTitle" runat="server" width="300"></asp:textbox></td>
									<td width="60" valign="top">
										<asp:label id="Label2" cssclass="CSPlainText" runat="server">Term</asp:label><br>
										<table id="Table2" cellspacing="0" cellpadding="0" border="0">
											<tr>
												<td>
													<asp:textbox id="tbxTerm" runat="server" width="40"></asp:textbox></td>
												<td style="WIDTH: 10px">
													<asp:rangevalidator id="RangeValidator5" runat="server" errormessage="Term must be between 1 and 48."
														type="Integer" controltovalidate="tbxTerm" maximumvalue="48" minimumvalue="1">*</asp:rangevalidator>
												</td>
											</tr>
										</table>
									</td>
									<td valign="middle">
										<table cellspacing="0" cellpadding="0" border="0">
											<tr>
												<td align="center"><asp:button id="btnSearch" runat="server" text="Search"></asp:button>&nbsp;&nbsp;&nbsp;</td>
												<td align="center"><asp:button id="btnReset" runat="server" text="Reset"></asp:button></td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
	<br>
</div>
<asp:label id="lblMessage" runat="server"></asp:label>
<cc2:datagridobject id="dtgMain" runat="server" allowpaging="True" searchmode="0" width="97%" cellpadding="3"
	backcolor="White" bordercolor="#CCCCCC" borderstyle="None" borderwidth="1px"
	autogeneratecolumns="False" OnItemDataBound="dtgMain_ItemDataBound">
	<selecteditemstyle font-bold="True" forecolor="White" backcolor="#008A8C"></selecteditemstyle>
	<itemstyle forecolor="#000066" cssclass="CSSearchResult"></itemstyle>
	<headerstyle font-bold="True" forecolor="White" cssclass="CSSearchResult" backcolor="#006699"></headerstyle>
	<footerstyle forecolor="#000066" cssclass="CSSearchResult" backcolor="White"></footerstyle>
	<columns>
		<asp:templatecolumn visible="False">
			<itemtemplate>
				<asp:checkbox id="chkSelect" runat="server"></asp:checkbox>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn visible="False">
			<itemtemplate>
				<asp:Label id=lblMagInstance runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MagPrice_instance") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Title Code">
			<itemtemplate>
				<asp:Label id=lblProductCode runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Product_code") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Title">
			<itemtemplate>
				<asp:Label id=lblTitle runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Product_sort_name") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Term">
			<itemtemplate>
				<asp:Label id=lblTerm runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Term") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn visible="False" headertext="Quantity">
			<itemtemplate>
				<cc1:textboxinteger id="tbxQuantity" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Quantity") %>' errormsgregexp="The field Quantity has to be a number." required="True" errormsgrequired="The field Quantity is mandatory." columns="4">
				</cc1:textboxinteger>
				<asp:rangevalidator id="ravQuantity" runat="server" controltovalidate="tbxQuantity" display="Dynamic" errormessage="The Quantity has to be more than 0." type="Integer" minimumvalue="0" maximumvalue="2147483647">*</asp:rangevalidator>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Catalog Price">
			<itemtemplate>
				<asp:Label id=lblPrice runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Price") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Language">
			<itemtemplate>
				<asp:Label id="lblLang" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Lang") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Catalog Name">
			<itemtemplate>
				<asp:Label id="lblCatalogName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Catalog_Name") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn visible="False" headertext="ProductType">
			<itemtemplate>
				<asp:Label ID="lblProductType" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ProductType") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn visible="False" headertext="Price">
			<itemtemplate>
				<cc1:textboxfloat id="tbxPrice" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EnterredPrice") %>' errormsgregexp="The field Price has to be a number." maxlength="25" required="True" errormsgrequired="The field Price is mandatory." columns="6">
				</cc1:textboxfloat>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn visible="False" headertext="Price Override Reason">
			<itemtemplate>
				<cc1:dropdownlistreq id="ddlPriceOverrideReason" runat="server" initialtext="Please select..." initialvalue="0" required="True" errormsgrequired="Please select an override reason."></cc1:dropdownlistreq>
			</itemtemplate>
		</asp:templatecolumn>
        <asp:templatecolumn visible="False" headertext="Product Replacement Reason">
			<itemtemplate>
				<cc1:dropdownlistreq id="ddlProductReplacementReason" ClientIDMode="Static" runat="server" initialtext="Please select..." initialvalue="0" required="True" errormsgrequired="Please select a replacement reason."></cc1:dropdownlistreq>
			</itemtemplate>
		</asp:templatecolumn>
	</columns>
	<pagerstyle horizontalalign="Left" forecolor="#000066" backcolor="White" cssclass="CSPager"
		mode="NumericPages"></pagerstyle>
</cc2:datagridobject>
