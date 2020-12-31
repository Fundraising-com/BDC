<%@ Control Language="c#" AutoEventWireup="True" Codebehind="AccountListControl.ascx.cs" Inherits="QSPFulfillment.AcctMgt.Control.AccountListControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="DatePicker" Src="../../Common/DateEntry.ascx" %>

<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="dbwc" Namespace="DBauer.Web.UI.WebControls" Assembly="DBauer.Web.UI.WebControls.HierarGrid" %>

<script type='text/javascript'>
function GenerateFS_CallBack(response)
{
alert(response.value);
		window.Refresh();

}

function GenerateFS()
{
	var eleID = document.getElementById('ctrlAccountListControl_tbxCampaignID');
	var eleStart = document.getElementById('ctrlAccountListControl_dteFromDeliveryDate_tb_DATE');
	var eleEnd = document.getElementById('ctrlAccountListControl_dteToDeliveryDate_tb_DATE');
	var eleProvince = document.getElementById('ctrlAccountListControl_ddlGroupProvince');
	var eleFM = document.getElementById('ctrlAccountListControl_ddlFieldManager');
	var eleHid = document.getElementById('ctrlAccountListControl_hidDataBind');

	
	if(window.confirm("Are you sure you want to Generate Field Supplies?  "))
	{
		if(eleID.value.length == 0)
		{
			window.pleasewait();
			eleHid.value="1";
			AccountListControl.GenerateFieldSupply(0,eleProvince.value, eleStart.value,eleEnd.value,eleFM.value,GenerateFS_CallBack)
		}
		else
		{
			window.pleasewait();
			eleHid.value="1";
			AccountListControl.GenerateFieldSupply(eleID.value,eleProvince.value,eleStart.value,eleEnd.value, eleFM.value,GenerateFS_CallBack)
		}
	}
}

</script>
<input type="hidden" value="0" id="hidDataBind" runat="server" NAME="hidDataBind"/>
<div id="divSearch" name="divSearch">
	<table id="Table3" cellspacing="0" cellpadding="0" width="100%" bgcolor="#cecece" border="0">
		<tr>
			<td>
				<table class="CSTable" id="Table4" cellspacing="1" cellpadding="2" width="100%">
					<tr>
						<td valign="top" height="20"><asp:label id="lblTitle2" runat="server" cssclass="CSTitle">Search</asp:label></td>
					</tr>
					<tr bgcolor="#ffffff">
						<td valign="top">
							<table id="Table1" cellspacing="0" cellpadding="2" width="100%" border="0">
								<tr>
									<td style="WIDTH: 33%" valign="bottom">
										<asp:label id="Label5" runat="server" cssclass="CSPlainText">Group&nbsp;ID</asp:label><br>
										<cc1:textboxinteger id="tbxGroupID" runat="server" maxlength="25" errormsgregexp="The field Group ID has to be a number."></cc1:textboxinteger>
									</td>
									<td style="WIDTH: 33%" valign="bottom"><asp:label id="Label2" runat="server" cssclass="CSPlainText">Group&nbsp;Name</asp:label><br>
										<cc1:textbox id="tbxGroupName" runat="server" maxlength="50"></cc1:textbox></td>
									<td style="WIDTH: 34%" valign="bottom">
										<asp:label id="Label7" runat="server" cssclass="CSPlainText">Campaign&nbsp;ID</asp:label><br>
										<cc1:textboxinteger id="tbxCampaignID" runat="server" maxlength="25" errormsgregexp="The field Campaign ID has to be a number."></cc1:textboxinteger>
									</td>
									<td style="WIDTH: 34%" valign="bottom">
										<asp:label id="Label9" runat="server" cssclass="CSPlainText">FS Shipment&nbsp;From</asp:label>
		
										<uc1:datepicker id="dteFromDeliveryDate" runat="server" columns="10"></uc1:datepicker>
									
									</td>
									<td  valign="bottom">
										<asp:label id="Label11" runat="server" cssclass="CSPlainText">Supply  Status</asp:label>
										<br>
										<cc1:dropdownlistreq id="ddlSupplyStatus" runat="server">
										
										</cc1:dropdownlistreq>
									</td>
								</tr>
								<tr>
									<td style="WIDTH: 33%" valign="bottom">
										<asp:label id="Label4" runat="server" cssclass="CSPlainText"> City</asp:label><br>
										<asp:textbox id="tbxGroupCity" runat="server" maxlength="50"></asp:textbox>
									</td>
									<td style="WIDTH: 33%" valign="bottom">
										<asp:label id="Label3s" runat="server" cssclass="CSPlainText">Province</asp:label><br>
										<cc1:dropdownlistprovince id="ddlGroupProvince" runat="server" code="CA"></cc1:dropdownlistprovince>
									</td>
									<td style="WIDTH: 34%" valign="bottom">
										<asp:label id="Label6" runat="server" cssclass="CSPlainText">Postal&nbsp;Code</asp:label><br>
										<cc1:postalcode id="tbxGroupPostalCode" runat="server"></cc1:postalcode>
									</td>
									<td style="WIDTH: 34%" valign="bottom">
										<asp:label id="Label10" runat="server" cssclass="CSPlainText">FS Shipment&nbsp;To</asp:label><br>
											<uc1:datepicker id="dteToDeliveryDate" runat="server" columns="10"></uc1:datepicker>
									</td>
									<td style="WIDTH: 34%" valign="bottom"></td>
								</tr>
								<tr>
									<td style="WIDTH: 33%" valign="bottom">
										<asp:label id="Label1s" runat="server" cssclass="CSPlainText">Field&nbsp;Manager</asp:label><br>
										<cc1:dropdownlistreq id="ddlFieldManager" runat="server"></cc1:dropdownlistreq>
									</td>
									<td style="WIDTH: 33%" valign="bottom">
										<asp:label id="Label1" runat="server" cssclass="csPlainText">Fiscal&nbsp;Year</asp:label><br>
										<cc1:dropdownlistreq id="ddlFiscalYear" runat="server" initialvalue="0"></cc1:dropdownlistreq>
									</td>
									<td style="WIDTH: 33%" valign="bottom">
										<asp:label id="Label8" runat="server" cssclass="csPlainText">Campaign Count</asp:label><br>
										<asp:label id="CampaignCount" runat="server" cssclass="csPlainText">****</asp:label>
									</td>
									<td style="WIDTH: 34%" valign="bottom">
										<table id="Table2" cellspacing="0" cellpadding="0" border="0">
											<tr>
												<td><asp:button id="btnSearch" runat="server" cssclass="boxlook" text="Search" onclick="btnSearch_Click"></asp:button>&nbsp;&nbsp;&nbsp;
												</td>
												<td align="center"><input class="boxlook" onclick="Reset('divSearch')" type="button" value="Reset">
												</td>
												
												

												
											</tr>
										</table>
									</td>
									<td align="center" valign="bottom" id="GenerateFSBtn" runat="server"><input class="boxlook" onclick="GenerateFS()" type="button" value="Generate FS">
												</td>
								</tr>
								
							</table>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
</div>
<br>
<br>
<br>
<dbwc:hierargrid id="dtgMain" runat="server" cssclass="CSSearchResult" templatecachingbase="Tablename"
	loadcontrolmode="UserControl" templatedatamode="Table" rowexpanded="DBauer.Web.UI.WebControls.RowStates"
	autogeneratecolumns="False" allowpaging="True" bordercolor="#999999" borderstyle="None" borderwidth="1px"
	width="100%" backcolor="White" cellpadding="3" gridlines="Vertical" AllowSorting="true">
	<selecteditemstyle backcolor="#008A8C" forecolor="White" font-bold="True"></selecteditemstyle>
	<alternatingitemstyle backcolor="#DCDCDC"></alternatingitemstyle>
	<itemstyle backcolor="#EEEEEE" forecolor="Black"></itemstyle>
	<headerstyle cssclass="CSSearchResult" backcolor="#000084" forecolor="White" font-bold="True"></headerstyle>
	<footerstyle cssclass="CSSearchResult" backcolor="#CCCCCC" forecolor="Black"></footerstyle>
	<pagerstyle cssclass="CSPager" backcolor="#999999" forecolor="Black" mode="NumericPages" horizontalalign="Center"></pagerstyle>
	<columns>
		<asp:templatecolumn headertext="Group ID" SortExpression="ID">
			<itemtemplate>
				<ASP:LABEL id=lblGroupID runat="server" text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Name" SortExpression="Name">
			<itemtemplate>
				<ASP:LABEL id=lblName runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Name") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="City" SortExpression="City">
			<itemtemplate>
				<ASP:LABEL id=lblCity runat="server" text='<%# DataBinder.Eval(Container, "DataItem.City") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Province" SortExpression="State">
			<itemtemplate>
				<asp:label id="lblProvince" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.State") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Contact" SortExpression="Sponsor">
			<itemtemplate>
				<ASP:LABEL id=lblContact runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Sponsor") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Phone" SortExpression="Phone">
			<itemtemplate>
				<ASP:LABEL id=lblPhone runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Phone") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn>
			<itemtemplate>
				<a id="hypNewCampaign" runat="server" href='<%# "../CampaignMaintenance.aspx?CampaignID=0&AccountID=" + DataBinder.Eval(Container, "DataItem.ID") %>'>New CA</a>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn>
			<ItemTemplate>
				<a href="AccountMaintenance.aspx?AccountID=<%# DataBinder.Eval(Container, "DataItem.ID") %>">Edit</a>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn Visible="False">
			<itemtemplate>
				<a href="<%#"javascript: window.opener.document.getElementById('" + this.ParentControlName + "').value = " + DataBinder.Eval(Container, "DataItem.ID") + "; self.close();" %>">Select</a>
			</itemtemplate>
		</asp:templatecolumn>
	</columns>
	<pagerstyle mode="NumericPages"></pagerstyle>
</dbwc:hierargrid>
<br>
<div style="text-align: right;">
	<input id="btnCancel" runat="server" type="button" class="boxlook" value="Cancel" onclick="javascript: self.close();" />
</div>