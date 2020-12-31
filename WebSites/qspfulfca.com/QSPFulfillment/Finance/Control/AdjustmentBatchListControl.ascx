<%@ Register TagPrefix="uc1" TagName="DateEntry" Src="../../Common/DateEntry.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ControlerConfirmationPage" Src="../../CustomerService/ControlerConfirmationPage.ascx" %>
<%@ Register TagPrefix="dbwc" Namespace="DBauer.Web.UI.WebControls" Assembly="DBauer.Web.UI.WebControls.HierarGrid" %>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.Finance.Control" Assembly="QSPFulfillment" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="AdjustmentBatchListControl.ascx.cs" Inherits="QSPFulfillment.Finance.Control.AdjustmentBatchListControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
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
									<td><asp:label id="Label5" runat="server" cssclass="CSPlainText">Adjustment&nbsp;Batch&nbsp;ID</asp:label><br>
										<cc1:textboxinteger id="tbxAdjustmentBatchID" runat="server" maxlength="25" errormsgregexp="The field Adjustment Batch ID has to be a number."
											EmptyValue="0"></cc1:textboxinteger></td>
									<td><asp:label id="Label2" runat="server" cssclass="CSPlainText">Adjustment&nbsp;Type</asp:label><br>
										<cc2:adjustmenttypedropdownlist id="ddlAdjustmentType" runat="server" enableviewstate="False" initialtext="Please select..."
											initialvalue="0"></cc2:adjustmenttypedropdownlist></td>
									<td><asp:label id="Label7" runat="server" cssclass="CSPlainText">Status</asp:label><br>
										<cc1:dropdownlistinteger id="ddlStatus" runat="server" initialtext="Please select..." initialvalue="0"></cc1:dropdownlistinteger></td>
								</tr>
								<tr>
									<td><asp:label id="Label4" runat="server" cssclass="CSPlainText">Date&nbsp;From</asp:label><br>
										<uc1:dateentry id="dteDateFrom" runat="server" emptyvalue="1995-01-01"></uc1:dateentry></td>
									<td><asp:label id="Label3s" runat="server" cssclass="CSPlainText">Date&nbsp;To</asp:label><br>
										<uc1:dateentry id="dteDateTo" runat="server" emptyvalue="1995-01-01"></uc1:dateentry></td>
									<td style="WIDTH: 34%" valign="bottom">
										<table id="Table2" cellspacing="0" cellpadding="0" border="0">
											<tr>
												<td><asp:button id="btnSearch" runat="server" cssclass="boxlook" text="Search"></asp:button>&nbsp;&nbsp;&nbsp;
												</td>
												<td align="center"><input class="boxlook" onclick="Reset('divSearch')" type="button" value="Reset">
												</td>
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
</div>
<br>
<br>
<br>
<dbwc:hierargrid id="dtgMain" runat="server" cssclass="CSSearchResult" allowsorting="true" templatecachingbase="Tablename"
	loadcontrolmode="UserControl" templatedatamode="Table" rowexpanded="DBauer.Web.UI.WebControls.RowStates"
	autogeneratecolumns="False" allowpaging="True" bordercolor="#999999" borderstyle="None" borderwidth="1px"
	width="100%" backcolor="White" cellpadding="3" gridlines="Vertical">
	<selecteditemstyle backcolor="#008A8C" forecolor="White" font-bold="True"></selecteditemstyle>
	<alternatingitemstyle backcolor="#DCDCDC"></alternatingitemstyle>
	<itemstyle backcolor="#EEEEEE" forecolor="Black"></itemstyle>
	<headerstyle cssclass="CSSearchResult" backcolor="#000084" forecolor="White" font-bold="True"></headerstyle>
	<footerstyle cssclass="CSSearchResult" backcolor="#CCCCCC" forecolor="Black"></footerstyle>
	<pagerstyle cssclass="CSPager" backcolor="#999999" forecolor="Black" mode="NumericPages" horizontalalign="Center"></pagerstyle>
	<columns>
		<asp:templatecolumn headertext="ID" sortexpression="ID">
			<itemtemplate>
				<ASP:LABEL id="lblAdjustmentBatchID" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
				</ASP:LABEL>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn visible="false">
			<itemtemplate>
				<asp:label id="lblAdjustmentTypeID" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.AdjustmentTypeID") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Type" sortexpression="AdjustmentTypeName">
			<itemtemplate>
				<ASP:LABEL id="lblAdjustmentTypeName" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.AdjustmentTypeName") %>'>
				</ASP:LABEL>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn visible="false">
			<itemtemplate>
				<asp:label id="lblStatusInstance" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Status") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Status" sortexpression="StatusDescription">
			<itemtemplate>
				<ASP:LABEL id="lblStatusDescription" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.StatusDescription") %>'>
				</ASP:LABEL>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="From Date" sortexpression="DateFrom">
			<itemtemplate>
				<asp:label id="lblDateFrom" runat="server" text='<%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.DateFrom")).ToString("MM/dd/yyyy") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="To Date" sortexpression="DateTo">
			<itemtemplate>
				<asp:label id="lblDateTo" runat="server" text='<%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.DateTo")).ToString("MM/dd/yyyy") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Creation Date" sortexpression="CreateDate">
			<itemtemplate>
				<asp:label id="lblCreateDate" runat="server" text='<%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.CreateDate")).ToString("MM/dd/yyyy") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Creation User" sortexpression="CreateUserName">
			<itemtemplate>
				<ASP:LABEL id="lblCreateUserName" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.CreateUserName") %>'>
				</ASP:LABEL>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Change Date" sortexpression="ChangeDate">
			<itemtemplate>
				<ASP:LABEL id="lblChangeDate" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.ChangeDate") != System.DBNull.Value ? Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.ChangeDate")).ToString("MM/dd/yyyy") : String.Empty %>'>
				</ASP:LABEL>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Change User" sortexpression="ChangeUserName">
			<itemtemplate>
				<asp:label id="lblChangeUserName" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.ChangeUserName") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn>
			<itemtemplate>
				<asp:linkbutton id="btnDeleteBatch" runat="server" text="Delete" commandname="DeleteBatch" commandargument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' visible='<%# DataBinder.Eval(Container, "DataItem.IsDeletable") %>'>
				</asp:linkbutton>
				<asp:linkbutton id="btnRestoreBatch" runat="server" text="Restore" commandname="RestoreBatch" commandargument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' visible='<%# DataBinder.Eval(Container, "DataItem.IsRestorable") %>'>
				</asp:linkbutton>
			</itemtemplate>
		</asp:templatecolumn>
	</columns>
	<pagerstyle mode="NumericPages"></pagerstyle>
</dbwc:hierargrid>
<uc1:controlerconfirmationpage id="ctrlControlerConfirmationPage" runat="server" Message="Are you sure you want to delete this Adjustment Batch?"></uc1:controlerconfirmationpage>
