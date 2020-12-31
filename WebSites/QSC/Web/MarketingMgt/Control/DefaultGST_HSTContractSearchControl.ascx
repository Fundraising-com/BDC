<%@ Control Language="c#" AutoEventWireup="false" Codebehind="DefaultGST_HSTContractSearchControl.ascx.cs" Inherits="QSPFulfillment.MarketingMgt.Control.DefaultGST_HSTContractSearchControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="ControlerConfirmationPage" Src="../../CustomerService/ControlerConfirmationPage.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CustomerService" Assembly="QSPFulfillment" %>
<br>
<div id="divSearch" runat="server">
	<table id="Table3" cellspacing="0" cellpadding="0" width="100%" bgcolor="#cecece" border="0">
		<tr>
			<td>
				<table id="Table4" cellspacing="1" cellpadding="2" width="100%">
					<tr>
						<td valign="top" height="20"><asp:label id="lblTitle2" cssclass="CSTitle" runat="server">Search</asp:label></td>
					</tr>
					<tr bgcolor="#ffffff">
						<td valign="top">
							<table id="Table1" cellspacing="0" cellpadding="2" width="100%" border="0">
								<tr>
									<td width="70" valign="bottom"><asp:label id="Label1s" runat="server" cssclass="CSPlainText">Product&nbsp;Code</asp:label><br>
										<asp:textbox id="tbxProductCode" runat="server" width="70"></asp:textbox>
									</td>
									<td width="70" valign="bottom"><asp:label id="Label2" runat="server" cssclass="CSPlainText">Oracle&nbsp;Code</asp:label><br>
										<asp:textbox id="tbxOracleCode" runat="server" width="140px"></asp:textbox>
									</td>
									<td valign="bottom">
										<div id="divYear" runat="server">
											<asp:label id="Label1" runat="server" cssclass="csPlainText">Year</asp:label>
											<br>
											<cc1:dropdownlistinteger id="ddlYear" runat="server"></cc1:dropdownlistinteger>
										</div>
									</td>
									<td valign="bottom">
										<div id="divSeason" runat="server">
											<asp:label id="Label4" runat="server" cssclass="csPlainText">Season</asp:label>
											<br>
											<asp:dropdownlist id="ddlSeason" runat="server"></asp:dropdownlist>
										</div>
									</td>
									<td valign="bottom">
										<div id="divProductStatus" runat="server">
											<asp:label id="Label9" runat="server" cssclass="csPlainText">Status</asp:label>
											<br>
											<cc1:dropdownlistinteger id="ddlProductStatus" runat="server"></cc1:dropdownlistinteger>
										</div>
									</td>
									<td width="205" valign="bottom" style="WIDTH: 205px"><asp:label id="Label3s" runat="server" cssclass="CSPlainText">Name</asp:label><br>
										<asp:textbox id="tbxProductName" runat="server" columns="30"></asp:textbox></td>
									<td valign="bottom" width="60">
										<asp:label id="Label8" runat="server" cssclass="CSPlainText">Product&nbsp;Type</asp:label>
										<cc1:dropdownlistinteger id="ddlProductType" runat="server" errormsgrequired="The field Product Type is required."
											autopostback="True" initialvalue="0"></cc1:dropdownlistinteger>
									</td>
									<td width="70" valign="bottom">
										<div id="divCatalogCode" runat="server">
											<asp:label id="Label6" runat="server" cssclass="CSPlainText">Catalog Code</asp:label><br>
											<cc1:dropdownlistreq id="ddlCatalogCode" runat="server"></cc1:dropdownlistreq>
										</div>
									</td>
									<td valign="bottom">
										<table cellspacing="0" cellpadding="0" border="0">
											<tr>
												<td align="center" style="PADDING-LEFT: 15px"><asp:button id="btnSearch" runat="server" text="Search" cssclass="boxlook"></asp:button>&nbsp;&nbsp;&nbsp;</td>
												<td align="center"><input onclick="Reset('divSearch')" type="button" value="Reset" class="boxlook"></td>
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
<asp:label id="lblMessage" runat="server"></asp:label><cc2:datagridobject id="dtgMain" runat="server" autogeneratecolumns="False" borderwidth="1px" borderstyle="None"
	bordercolor="#CCCCCC" allowpagging="true" backcolor="White" cellpadding="3" width="100%" searchmode="0" allowpaging="True" pagesize="20">
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
				<asp:TextBox Width="45" id="tbxProductCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Product_Code") %>'>
				</asp:TextBox>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Product Code">
			<itemtemplate>
				<asp:Label id=lblProductCode runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Product_Code") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn visible="False">
			<itemtemplate>
				<asp:Label id="lblMagPriceInstanceGST" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MagPrice_InstanceGST") %>'>
				</asp:label>
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
		<asp:templatecolumn headertext="Product Name">
			<itemtemplate>
				<asp:Label id=lblTitle runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Product_Sort_Name") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="GST Price">
			<itemtemplate>
				<asp:Label id=lblGSTPrice runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.GSTPrice") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="HST Price">
			<itemtemplate>
				<asp:label id="lblHSTPrice" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.HSTPrice") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Oracle Code">
			<itemtemplate>
				<asp:label id="lblOracleCode" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.OracleCode") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Catalog Code">
			<itemtemplate>
				<asp:Label ID="lblCatalogCode" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CatalogCode") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Catalog Name">
			<itemtemplate>
				<asp:Label id="lblCatalogName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CatalogName") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn visible="False" headertext="Product Type">
			<itemtemplate>
				<asp:Label ID="lblProductTypeInstance" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ProductTypeInstance") %>'>
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
</cc2:datagridobject>
<uc1:controlerconfirmationpage id="ctrlControlerConfirmationPageDelete" runat="server"></uc1:controlerconfirmationpage>
