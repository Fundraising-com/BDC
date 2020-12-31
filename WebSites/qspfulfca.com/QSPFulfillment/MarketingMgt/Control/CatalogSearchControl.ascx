<%@ Control Language="c#" AutoEventWireup="false" Codebehind="CatalogSearchControl.ascx.cs" Inherits="QSPFulfillment.MarketingMgt.Control.CatalogSearchControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="uc1" TagName="ControlerConfirmationPage" Src="../../CustomerService/ControlerConfirmationPage.ascx" %>
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
								<tr vAlign="bottom">
									<td width="10%"><asp:label id="Label1s" runat="server" cssclass="CSPlainText">Catalog Code</asp:label><br>
										<asp:textbox id="tbxCatalogCode" runat="server" maxlength="10" width="70"></asp:textbox></td>
									<td width="10%"><asp:label id="Label7" runat="server" cssclass="CSPlainText">Name</asp:label><br>
										<asp:textbox id="tbxCatalogName" runat="server" maxlength="50"></asp:textbox></td>
									<td width="10%"><asp:label id="Label3s" runat="server" cssclass="CSPlainText">Year</asp:label><br>
										<cc1:dropdownlistinteger id="ddlYear" runat="server" initialvalue="0"></cc1:dropdownlistinteger></td>
									<td width="10%"><asp:label id="Label2" runat="server" cssclass="CSPlainText">Season</asp:label><br>
										<asp:dropdownlist id="ddlSeason" runat="server"></asp:dropdownlist></td>
								</tr>
								<tr>
									<td width="10%"><asp:label id="Label4" runat="server" cssclass="CSPlainText">Catalog Type</asp:label><br>
										<cc1:dropdownlistinteger id="ddlType" runat="server" initialvalue="0"></cc1:dropdownlistinteger></td>
									<td width="10%"><asp:label id="Label5" runat="server" cssclass="CSPlainText">Language</asp:label><br>
										<asp:dropdownlist id="ddlLanguage" runat="server">
											<asp:listitem selected="True"></asp:listitem>
											<asp:listitem value="EN">English</asp:listitem>
											<asp:listitem value="FR">French</asp:listitem>
											<asp:listitem value="EN/FR">English/French</asp:listitem>
										</asp:dropdownlist></td>
									<td width="10%"><asp:label id="Label6" runat="server" cssclass="CSPlainText">Status</asp:label><br>
										<cc1:dropdownlistinteger id="ddlStatus" runat="server" initialvalue="0"></cc1:dropdownlistinteger></td>
								</tr>
							</table>
						</td>
						<td style="TEXT-ALIGN: center" width="20%">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td align="center"><asp:button id="btnSearch" runat="server" cssclass="boxlook" text="Search"></asp:button>&nbsp;&nbsp;&nbsp;</td>
									<td align="center"><input class="boxlook" onclick="Reset('divSearch')" type="button" value="Reset"></td>
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
<asp:label id="lblMessage" runat="server"></asp:label><cc2:datagridobject id="dtgMain" runat="server" width="100%" allowpaging="True" searchmode="0" cellpadding="3"
	backcolor="White" allowpagging="true" bordercolor="#CCCCCC" borderstyle="None" borderwidth="1px" autogeneratecolumns="False">
	<selecteditemstyle backcolor="#008A8C" forecolor="White" font-bold="True"></selecteditemstyle>
	<itemstyle cssclass="CSSearchResult" forecolor="#000066"></itemstyle>
	<headerstyle cssclass="CSSearchResult" backcolor="#006699" forecolor="White" font-bold="True"></headerstyle>
	<footerstyle cssclass="CSSearchResult" backcolor="White" forecolor="#000066"></footerstyle>
	<columns>
		<asp:templatecolumn visible="False">
			<itemtemplate>
				<asp:Label id="lblCatalogID" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Program_ID") %>'>
				</asp:Label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Catalog Code">
			<itemtemplate>
				<asp:Label id=lblCatalogCode runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Code") %>'>
				</asp:Label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Name">
			<itemtemplate>
				<asp:Label id=lblName runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Program_Type") %>'>
				</asp:Label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Year">
			<itemtemplate>
				<asp:Label id="lblYear" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Year") %>'>
				</asp:Label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Season">
			<itemtemplate>
				<asp:Label id="lblSeason" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Season") %>'>
				</asp:Label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn visible="false">
			<itemtemplate>
				<asp:label id="lblTypeID" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.SubTypeID") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Type">
			<itemtemplate>
				<asp:Label id=lblType runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SubType") %>'>
				</asp:Label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Language">
			<itemtemplate>
				<asp:Label id=lblLanguage runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Lang") %>'>
				</asp:Label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn visible="false">
			<itemtemplate>
				<asp:label id="lblStatusID" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.StatusID") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Status">
			<itemtemplate>
				<asp:Label id=lblStatus runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Status") %>'>
				</asp:Label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Status" visible="false">
			<itemtemplate>
				<asp:label id="lblIsReplacement" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.IsReplacement") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn visible="true" headertext="">
			<itemtemplate>
				<asp:linkbutton id="lnkDelete" runat="server" commandname="Delete" causesvalidation="True">Delete</asp:linkbutton>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn visible="true" headertext="">
			<itemtemplate>
				<asp:linkbutton id="LinkButton1" runat="server" commandname="Select" causesvalidation="True">Select</asp:linkbutton>
			</itemtemplate>
		</asp:templatecolumn>
	</columns>
	<pagerstyle cssclass="CSPager" backcolor="White" forecolor="#000066" mode="NumericPages" horizontalalign="Left"></pagerstyle>
</cc2:datagridobject>
<uc1:controlerconfirmationpage id="ctrlControlerConfirmationPageDelete" runat="server"></uc1:controlerconfirmationpage>
