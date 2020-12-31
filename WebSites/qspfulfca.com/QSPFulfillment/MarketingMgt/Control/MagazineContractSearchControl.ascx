<%@ Control Language="c#" AutoEventWireup="false" Codebehind="MagazineContractSearchControl.ascx.cs" Inherits="QSPFulfillment.MarketingMgt.Control.MagazineContractSearchControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="ControlerConfirmationPage" Src="../../CustomerService/ControlerConfirmationPage.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CustomerService" Assembly="QSPFulfillment" %>
<br>
<div id="divSearch" runat="server">
	<table id="Table3" cellSpacing="0" cellPadding="0" width="100%" bgColor="#cecece" border="0">
		<tr>
			<td>
				<table id="Table4" cellSpacing="1" cellPadding="2" width="100%">
					<tr>
						<td vAlign="top" height="20"><asp:label id="lblTitle2" runat="server" cssclass="CSTitle">Search</asp:label></td>
					</tr>
					<tr bgColor="#ffffff">
						<td vAlign="top">
							<table id="Table1" cellSpacing="0" cellPadding="2" width="100%" border="0">
								<tr>
									<td style="HEIGHT: 64px" vAlign="bottom" width="70"><asp:label id="Label1s" runat="server" cssclass="CSPlainText">Title Code</asp:label><br>
										<asp:textbox id="tbxTitleCode" runat="server" width="70" maxlength="20"></asp:textbox></td>
									<td style="HEIGHT: 64px" vAlign="bottom" width="70"><asp:label id="Label10" runat="server" cssclass="CSPlainText">Remit Code</asp:label><br>
										<asp:textbox id="tbxRemitCode" runat="server" width="70" maxlength="20"></asp:textbox></td>
									<td style="HEIGHT: 64px" vAlign="bottom">
										<div id="divYear" runat="server"><asp:label id="Label1" runat="server" cssclass="csPlainText">Year</asp:label><br>
											<cc1:dropdownlistinteger id="ddlYear" runat="server"></cc1:dropdownlistinteger></div>
									</td>
									<td style="HEIGHT: 64px" vAlign="bottom">
										<div id="divSeason" runat="server"><asp:label id="Label4" runat="server" cssclass="csPlainText">Season</asp:label><br>
											<asp:dropdownlist id="ddlSeason" runat="server"></asp:dropdownlist></div>
									</td>
									<td style="HEIGHT: 64px" vAlign="bottom">
										<div id="divProductStatus" runat="server"><asp:label id="Label9" runat="server" cssclass="csPlainText">Status</asp:label><br>
											<cc1:dropdownlistinteger id="ddlProductStatus" runat="server"></cc1:dropdownlistinteger></div>
									</td>
									<td style="WIDTH: 205px; HEIGHT: 64px" vAlign="bottom" width="205"><asp:label id="Label3s" runat="server" cssclass="CSPlainText">Title</asp:label><br>
										<asp:textbox id="tbxTitle" runat="server" columns="30"></asp:textbox></td>
									<td style="HEIGHT: 64px" vAlign="bottom" width="60"><asp:label id="Label8" runat="server" cssclass="CSPlainText">Product&nbsp;Type</asp:label><cc1:dropdownlistinteger id="ddlProductType" runat="server" initialvalue="0" autopostback="True" errormsgrequired="The field Product Type is required."></cc1:dropdownlistinteger></td>
									<td style="HEIGHT: 64px" vAlign="bottom" width="60"><asp:label id="Label2" runat="server" cssclass="CSPlainText">Term</asp:label><br>
										<table id="Table2" cellSpacing="0" cellPadding="0" border="0">
											<tr>
												<td><cc1:textboxinteger id="tbxTerm" runat="server" width="40" emptyvalue="0"></cc1:textboxinteger></td>
												<td style="WIDTH: 10px"><asp:rangevalidator id="RangeValidator5" runat="server" errormessage="Term must be between 0 and 52."
														type="Integer" controltovalidate="tbxTerm" maximumvalue="52" minimumvalue="0">*</asp:rangevalidator></td>
											</tr>
										</table>
									</td>
									<td style="HEIGHT: 64px" vAlign="bottom" width="70">
										<div id="divCatalogCode" runat="server"><asp:label id="Label6" runat="server" cssclass="CSPlainText">Catalog Code</asp:label><br>
											<cc1:dropdownlistreq id="ddlCatalogCode" runat="server"></cc1:dropdownlistreq></div>
									</td>
									<td style="HEIGHT: 64px" vAlign="bottom">
										<table cellSpacing="0" cellPadding="0" border="0">
											<tr>
												<td style="PADDING-LEFT: 15px" align="center"><asp:button id="btnSearch" runat="server" cssclass="boxlook" text="Search"></asp:button>&nbsp;&nbsp;&nbsp;</td>
												<td align="center"><input class="boxlook" onclick="Reset('divSearch')" type="button" value="Reset"></td>
											</tr>
										</table>
									</td>
								</tr>
								<tr>
									<td vAlign="bottom" width="70" colSpan="5"><asp:label id="Label5" runat="server" cssclass="csPlainText">Publisher</asp:label><br>
										<cc1:dropdownlistinteger id="ddlPublisher" runat="server"></cc1:dropdownlistinteger></td>
									<td vAlign="bottom" colSpan="4"><asp:label id="Label7" runat="server" cssclass="csPlainText">Fulfillment House</asp:label><br>
										<cc1:dropdownlistinteger id="ddlFulfillmentHouse" runat="server"></cc1:dropdownlistinteger></td>
									<td vAlign="bottom"></td>
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
<asp:label id="lblMessage" runat="server"></asp:label><cc2:datagridobject id="dtgMain" runat="server" width="100%" pagesize="20" allowpaging="True" searchmode="0"
	cellpadding="3" backcolor="White" allowpagging="true" bordercolor="#CCCCCC" borderstyle="None" borderwidth="1px" autogeneratecolumns="False">
	<pagerstyle horizontalalign="Left" forecolor="#000066" backcolor="White" cssclass="CSPager"
		mode="NumericPages"></pagerstyle>
	<footerstyle forecolor="#000066" cssclass="CSSearchResult" backcolor="White"></footerstyle>
	<selecteditemstyle font-bold="True" forecolor="White" backcolor="#008A8C"></selecteditemstyle>
	<itemstyle forecolor="#000066" cssclass="CSSearchResult"></itemstyle>
	<headerstyle font-bold="True" forecolor="White" cssclass="CSSearchResult" backcolor="#006699"></headerstyle>
	<columns>
		<asp:templatecolumn>
			<itemtemplate>
				<asp:checkbox id="chkSelect" runat="server"></asp:checkbox>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="New Product Code" visible="false">
			<itemtemplate>
				<asp:TextBox Width="45" id=tbxProductCode runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Product_Code") %>'>
				</asp:TextBox>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Product Code">
			<itemtemplate>
				<asp:Label id="lblProductCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Product_Code") %>'>
				</asp:Label>
			</itemtemplate>
		</asp:templatecolumn> 
		<asp:templatecolumn visible="False">
			<itemtemplate>
				<asp:Label id="lblMagPriceInstanceGST" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MagPrice_InstanceGST") %>'>
				</asp:Label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn visible="False">
			<itemtemplate>
				<asp:label id="lblMagPriceInstanceHST" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.MagPrice_InstanceHST") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn visible="False">
			<itemtemplate>
				<asp:label id="lblProductInstance" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Product_Instance") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Remit Code">
			<itemtemplate>
				<asp:label id="lblRemitCode" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.RemitCode") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Year">
			<itemtemplate>
				<asp:label id="lblYear" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Year") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Season">
			<itemtemplate>
				<asp:label id="lblSeason" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Season") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Status">
			<itemtemplate>
				<asp:label id="lblProductStatus" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Status") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Title">
			<itemtemplate>
				<asp:Label id=lblTitle runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Product_Sort_Name") %>'>
				</asp:Label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Term">
			<itemtemplate>
				<asp:Label id=lblTerm runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Nbr_Of_Issues") %>'>
				</asp:Label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="GST Price">
			<itemtemplate>
				<asp:Label id=lblGSTPrice runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.GSTPrice") %>'>
				</asp:Label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="HST Price">
			<itemtemplate>
				<asp:label id="lblHSTPrice" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.HSTPrice") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Language">
			<itemtemplate>
				<asp:Label id="lblLanguage" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Language") %>'>
				</asp:Label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Catalog Code">
			<itemtemplate>
				<asp:Label ID="lblCatalogCode" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CatalogCode") %>'>
				</asp:Label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Catalog Name">
			<itemtemplate>
				<asp:Label id="lblCatalogName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CatalogName") %>'>
				</asp:Label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn visible="False" headertext="Product Type">
			<itemtemplate>
				<asp:Label ID="lblProductTypeInstance" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ProductTypeInstance") %>'>
				</asp:Label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn visible="False">
			<itemtemplate>
				<asp:label id="lblPublisherID" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.PublisherID") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn visible="False">
			<itemtemplate>
				<asp:label id="lblFulfillmentHouseID" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.FulfillmentHouseID") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn>
			<itemtemplate>
				<asp:linkbutton id="LinkButton1" runat="server" commandname="Select" causesvalidation="False">Edit</asp:linkbutton>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn visible="true" headertext="">
			<itemtemplate>
				<asp:linkbutton causesvalidation="False" runat="server" commandname="DeleteContract" id="Linkbutton2">Delete Contract</asp:linkbutton>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn visible="true" headertext="">
			<itemtemplate>
				<asp:linkbutton causesvalidation="False" runat="server" commandname="ModifyContract" id="Linkbutton3">Modify Contract</asp:linkbutton>
			</itemtemplate>
		</asp:templatecolumn>
	</columns>
</cc2:datagridobject><uc1:controlerconfirmationpage id="ctrlControlerConfirmationPageDelete" runat="server"></uc1:controlerconfirmationpage>
