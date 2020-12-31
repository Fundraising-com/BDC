<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ControlerCampaignsForProductReplacement.ascx.cs" Inherits="QSPFulfillment.CustomerService.ControlerCampaignsForProductReplacement" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CustomerService" Assembly="QSPFulfillment" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<br>
<div id="divSearch">
	<table id="Table3" cellspacing="0" cellpadding="0" bgcolor="#cecece" border="0">
		<tr>
			<td>
				<table id="Table4" cellspacing="1" cellpadding="2">
					<tr>
						<td valign="top" height="20"><asp:label id="lblTitle2" cssclass="CSTitle" runat="server">Search</asp:label></td>
					</tr>
					<tr bgcolor="#ffffff">
						<td valign="top">
							<table id="Table1" cellspacing="0" cellpadding="2" border="0">
								<tbody>
									<tr>
										<td valign="bottom" width="70"><asp:label id="lblGroupID" cssclass="CSPlainText" runat="server">Group ID</asp:label><br>
											<cc1:textboxinteger id="tbxGroupID" runat="server" columns="15" maxlength="6"></cc1:textboxinteger></td>
										<td valign="bottom" width="105"><asp:label id="lblGroupName" cssclass="CSPlainText" runat="server">Group Name</asp:label><br>
											<asp:textbox id="tbxGroupName" runat="server" columns="30" maxlength="50"></asp:textbox></td>
										<td valign="bottom" width="70" colspan="2"><asp:label id="lblCampaignID" cssclass="CSPlainText" runat="server" width="112px">Campaign ID</asp:label><br>
											<cc1:textboxinteger id="tbxCampaignID" runat="server" columns="6" maxlength="7"></cc1:textboxinteger></td>
										<td>&nbsp;</td>
									</tr>
									<tr>
										<td valign="bottom" width="70"><asp:label id="lblFMID" cssclass="CSPlainText" runat="server">FM ID</asp:label><br>
											<asp:textbox id="tbxFMID" runat="server" columns="15" maxlength="6"></asp:textbox></td>
										<td valign="bottom" width="70"><asp:label id="lblFMLastName" cssclass="CSPlainText" runat="server" width="112px">FM Last Name</asp:label>
											<asp:textbox id="tbxFMLastName" runat="server" columns="30" maxlength="50"></asp:textbox></td>
										<td valign="bottom" width="70"><asp:label id="lblFMFirstName" cssclass="CSPlainText" runat="server" width="112px">FM First Name</asp:label>
											<asp:textbox id="tbxFMFirstName" runat="server" columns="30" maxlength="50"></asp:textbox></td>
										<td>&nbsp;</td>
									</tr>
									<tr>
										<td valign="bottom" width="70"><asp:label id="lblCity" cssclass="CSPlainText" runat="server">City</asp:label><br>
											<asp:textbox id="tbxCity" runat="server" columns="15" maxlength="50"></asp:textbox></td>
										<td valign="bottom" width="105"><asp:label id="lblProvince" cssclass="CSPlainText" runat="server">Province</asp:label><br>
											<asp:textbox id="tbxProvince" runat="server" columns="30" maxlength="2"></asp:textbox></td>
										<td valign="bottom" width="70"><asp:label id="lblPostalCode" cssclass="CSPlainText" runat="server" width="112px">Postal Code</asp:label><br>
											<asp:textbox id="tbxPostalCode" runat="server" columns="6" maxlength="6"></asp:textbox></td>
										<td valign="bottom" align="right">
											<table id="Table5" cellspacing="0" cellpadding="0" border="0">
												<tr>
													<td align="center"><asp:button id="btnSearch" runat="server" text="Search"></asp:button>&nbsp;&nbsp;&nbsp;</td>
													<td align="center"><asp:button id="btnReset" runat="server" text="Reset"></asp:button></td>
												</tr>
											</table>
										</td>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
	</TD></TR></TBODY></TABLE>
</div>
<br>
<asp:label id="lblMessage" runat="server"></asp:label><cc2:datagridobject id="dtgMain" runat="server" width="100%" autogeneratecolumns="False" borderwidth="1px"
	borderstyle="None" bordercolor="#CCCCCC" allowpagging="true" backcolor="White" cellpadding="3" searchmode="0" allowpaging="True"><selecteditemstyle backcolor="#008A8C" forecolor="White" font-bold="True"></selecteditemstyle>
	<itemstyle cssclass="CSSearchResult" forecolor="#000066"></itemstyle>
	<headerstyle cssclass="CSSearchResult" backcolor="#006699" forecolor="White" font-bold="True"></headerstyle>
	<footerstyle cssclass="CSSearchResult" backcolor="White" forecolor="#000066"></footerstyle>
	<columns>
		<asp:templatecolumn headertext="Group ID">
			<itemtemplate>
				<asp:Label id="lblGroupID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.GroupID") %>'>
				</asp:Label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Group Name">
			<itemtemplate>
				<asp:Label id="lblGroupName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.GroupName") %>'>
				</asp:Label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Campaign ID">
			<itemtemplate>
				<asp:Label id="lblCampaignID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CampaignID") %>'>
				</asp:Label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Programs">
			<itemtemplate>
				<asp:Label id="lblPrograms" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Programs") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Start Date">
			<itemtemplate>
				<asp:Label id=lblStartDate runat="server" Text='<%# ((DateTime) DataBinder.Eval(Container, "DataItem.StartDate")).ToString("MM-dd-yyyy") %>'>
				</asp:Label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="End Date">
			<itemtemplate>
				<asp:Label id=lblEndDate runat="server" Text='<%# ((DateTime) DataBinder.Eval(Container, "DataItem.EndDate")).ToString("MM-dd-yyyy") %>'>
				</asp:Label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="FM ID">
			<itemtemplate>
				<asp:Label id=lblFMID runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FMID") %>'>
				</asp:Label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="FM Last Name">
			<itemtemplate>
				<asp:Label id=lblFMLastName runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.lastname") %>'>
				</asp:Label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="FM First Name">
			<itemtemplate>
				<asp:Label id=lblFMFirstName Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.firstname") %>'>
				</asp:Label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="City">
			<itemtemplate>
				<asp:Label id="lblCity" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.city") %>'>
				</asp:Label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Province">
			<itemtemplate>
				<asp:Label id="lblProvince" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.stateprovince") %>'>
				</asp:Label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Postal Code">
			<itemtemplate>
				<asp:Label id="lblPostalCode" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.postal_code") %>'>
				</asp:Label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="">
			<itemtemplate>
				<asp:linkbutton id="LinkButton1" runat="server" commandname="Select" causesvalidation="True">Select</asp:linkbutton>
			</itemtemplate>
		</asp:templatecolumn>
	</columns>
	<pagerstyle cssclass="CSPager" backcolor="White" forecolor="#000066" mode="NumericPages" horizontalalign="Left"></pagerstyle>
</cc2:datagridobject>
