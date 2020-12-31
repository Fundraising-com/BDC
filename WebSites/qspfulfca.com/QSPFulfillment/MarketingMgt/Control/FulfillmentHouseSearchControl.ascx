<%@ Control Language="c#" AutoEventWireup="false" Codebehind="FulfillmentHouseSearchControl.ascx.cs" Inherits="QSPFulfillment.MarketingMgt.Control.FulfillmentHouseSearchControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CustomerService" Assembly="QSPFulfillment" %>
<br>
<div id="Div1" runat="server">
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
									<td width="70" valign="bottom"><asp:label id="Label1s" runat="server" cssclass="CSPlainText"> Name</asp:label><br>
										<asp:textbox id="tbxName" runat="server" columns="30"></asp:textbox></td>
									<td valign="bottom">
										<asp:label id="Label1" runat="server" cssclass="csPlainText">Status</asp:label>
										<br>
										<asp:dropdownlist id="ddlStatus" runat="server"></asp:dropdownlist>
									</td>
									<td width="205" valign="bottom" style="WIDTH: 205px"><asp:label id="Label3s" runat="server" cssclass="CSPlainText">City</asp:label><br>
										<asp:textbox id="tbxCity" runat="server" columns="30"></asp:textbox></td>
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
<div id="divSearch" runat="server">
	<asp:label id="lblMessage" runat="server"></asp:label>
	<cc2:datagridobject id="dtgMain" runat="server" allowpaging="True" searchmode="0" width="100%" cellpadding="3"
		backcolor="White" allowpagging="true" bordercolor="#CCCCCC" borderstyle="None" borderwidth="1px"
		autogeneratecolumns="False">
		<selecteditemstyle font-bold="True" forecolor="White" backcolor="#008A8C"></selecteditemstyle>
		<itemstyle forecolor="#000066" cssclass="CSSearchResult"></itemstyle>
		<headerstyle font-bold="True" forecolor="White" cssclass="CSSearchResult" backcolor="#006699"></headerstyle>
		<footerstyle forecolor="#000066" cssclass="CSSearchResult" backcolor="White"></footerstyle>
		<columns>
			<asp:templatecolumn visible="False">
				<itemtemplate>
					<asp:Label id="lblFulfillmentHouseNumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Ful_Nbr") %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn headertext="Name">
				<itemtemplate>
					<asp:Label id="lblName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Ful_Name") %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn headertext="Status">
				<itemtemplate>
					<asp:Label id="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Ful_Status") %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn headertext="Address1" visible="false">
				<itemtemplate>
					<asp:Label id="lblAddress1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Ful_Addr_1") %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn headertext="Address2" visible="false">
				<itemtemplate>
					<asp:Label id="lblAddress2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Ful_Addr_2") %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn headertext="City">
				<itemtemplate>
					<asp:Label id="lblCity" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Ful_City") %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn headertext="State / Province" visible="false">
				<itemtemplate>
					<asp:Label id="lblStateProvince" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Ful_State") %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn headertext="Zip / PostalCode" visible="false">
				<itemtemplate>
					<asp:Label id="lblZip" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Ful_Zip") %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn visible="false">
				<ItemTemplate>
					<asp:label id="lblCountry" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CountryCode") %>'></asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn visible="false">
				<itemtemplate>
					<asp:label id="lblInterfaceMediaID" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.InterfaceMediaID") %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn headertext="Interface Media">
				<itemtemplate>
					<asp:Label id="lblInterfaceMedia" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.InterfaceMediaDescription") %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn visible="False">
				<itemtemplate>
					<asp:label id="lblInterfaceLayoutID" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.InterfaceLayoutID") %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn headertext="Interface Layout">
				<itemtemplate>
					<asp:Label id="lblInterfaceLayout" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.InterfaceLayoutDescription") %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn visible="False">
				<itemtemplate>
					<asp:label id="lblTransmissionMethodID" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.TransmissionMethodID") %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn headertext="Transmission Method">
				<itemtemplate>
					<asp:Label id="lblTransmissionMethod" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TransmissionMethodDescription") %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn headertext="Hard Copy">
				<itemtemplate>
					<asp:Label id="lblHardCopy" runat="server" Text='<%# Convert.ToBoolean(DataBinder.Eval(Container, "DataItem.HardCopy")) ? "True" : "False" %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn visible="false">
				<itemtemplate>
					<asp:label id="lblQSPAgencyCode" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.QSPAgencyCode") %>'></asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn visible="false">
				<itemtemplate>
					<asp:label id="lblIsEffortKeyRequired" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.IsEffortKeyRequired") %>'></asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn visible="False">
				<itemtemplate>
					<asp:label id="lblPayGroupLookUpCode" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.PayGroupLookUpCode") %>'></asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn visible="true" headertext="">
				<itemtemplate>
					<asp:linkbutton id="LinkButton1" runat="server" commandname="Select" causesvalidation="True">Edit</asp:linkbutton>
				</itemtemplate>
			</asp:templatecolumn>
		</columns>
		<pagerstyle horizontalalign="Left" forecolor="#000066" backcolor="White" cssclass="CSPager"
			mode="NumericPages"></pagerstyle>
	</cc2:datagridobject>
</div>
