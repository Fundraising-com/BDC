<%@ Control Language="c#" AutoEventWireup="false" Codebehind="DefaultProductSearchControl.ascx.cs" Inherits="QSPFulfillment.MarketingMgt.Control.DefaultProductSearchControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CustomerService" Assembly="QSPFulfillment" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="uc1" TagName="ControlerConfirmationPage" Src="../../CustomerService/ControlerConfirmationPage.ascx" %>
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
									<td width="70" valign="bottom"><asp:label id="Label1s" runat="server" cssclass="CSPlainText">Product Code</asp:label><br>
										<asp:textbox id="tbxTitleCode" runat="server" width="70"></asp:textbox></td>
									<td valign="bottom">
										<asp:label id="Label1" runat="server" cssclass="csPlainText">Year</asp:label>
										<br>
										<cc1:dropdownlistinteger id="ddlYear" runat="server"></cc1:dropdownlistinteger>
									</td>
									<td valign="bottom">
										<asp:label id="Label4" runat="server" cssclass="csPlainText">Season</asp:label><br>
										<asp:dropdownlist id="ddlSeason" runat="server"></asp:dropdownlist>
									</td>
									<td width="216" valign="bottom" style="WIDTH: 216px"><asp:label id="Label3s" runat="server" cssclass="CSPlainText">Title</asp:label><br>
										<asp:textbox id="tbxTitle" runat="server" columns="30"></asp:textbox></td>
									<td style="WIDTH: 216px" valign="bottom" width="216">
										<asp:label id="Label5" runat="server" cssclass="CSPlainText">Product Status</asp:label>
										<br>
										<cc1:dropdownlistinteger id="ddlProductStatus" runat="server" initialvalue="0"></cc1:dropdownlistinteger></td>
									<td valign="bottom">
										<asp:label id="Label8" runat="server" cssclass="CSPlainText">Product Type</asp:label>
										<br>
										<cc1:dropdownlistinteger id="ddlProductType" runat="server" autopostback="True" initialvalue="0"></cc1:dropdownlistinteger></td>
									<td valign="bottom">
										<br>
										<table cellspacing="0" cellpadding="0" border="0">
											<tr>
												<td style="PADDING-LEFT: 15px" align="center">
													<asp:button id="btnSearch" runat="server" text="Search" cssclass="boxlook"></asp:button>&nbsp;&nbsp;&nbsp;</td>
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
	bordercolor="#CCCCCC" allowpagging="true" backcolor="White" cellpadding="3" width="100%" searchmode="0" allowpaging="True" pagesize="15">
	<pagerstyle horizontalalign="Left" forecolor="#000066" backcolor="White" cssclass="CSPager"
		mode="NumericPages"></pagerstyle>
	<footerstyle forecolor="#000066" cssclass="CSSearchResult" backcolor="White"></footerstyle>
	<selecteditemstyle font-bold="True" forecolor="White" backcolor="#008A8C"></selecteditemstyle>
	<itemstyle forecolor="#000066" cssclass="CSSearchResult"></itemstyle>
	<headerstyle font-bold="True" forecolor="White" cssclass="CSSearchResult" backcolor="#006699"></headerstyle>
	<columns>
		<asp:templatecolumn visible="False">
			<itemtemplate>
				<asp:label id="lblProductInstance" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Product_Instance") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Product Code">
			<itemtemplate>
				<asp:Label id=lblProductCode runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Product_Code") %>'>
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
		<asp:templatecolumn visible="False">
			<itemtemplate>
				<asp:label id="lblProductStatus" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.ProductStatus") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Product Status">
			<itemtemplate>
				<asp:label id="lblProductStatusDescription" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.ProductStatusDescription") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Name">
			<itemtemplate>
				<asp:Label id=lblTitle runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Product_Sort_Name") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn visible="False" headertext="ProductType">
			<itemtemplate>
				<asp:label id="lblProductType" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.ProductType") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Language">
			<itemtemplate>
				<asp:Label id="lblLanguage" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Language") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Oracle Code">
			<itemtemplate>
				<asp:label id="lblOracleCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.OracleCode") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn>
			<itemtemplate>
				<asp:linkbutton id="LinkButton3" runat="server" commandname="Delete" causesvalidation="True">Delete</asp:linkbutton>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn>
			<itemtemplate>
				<asp:linkbutton id="Linkbutton1" runat="server" commandname="Select" causesvalidation="True">Select</asp:linkbutton>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn>
			<itemtemplate>
				<asp:linkbutton id="LinkButton2" runat="server" commandname="Edit" causesvalidation="True">Edit</asp:linkbutton>
			</itemtemplate>
		</asp:templatecolumn>
	</columns>
</cc2:datagridobject>
<uc1:controlerconfirmationpage id="ctrlControlerConfirmationPageDelete" runat="server"></uc1:controlerconfirmationpage>