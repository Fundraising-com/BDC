<%@ Page language="c#" Codebehind="Shipment.aspx.cs" AutoEventWireup="false" Inherits="QSPFulfillment.OrderMgt.Shipment" %>
<%@ Register  TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.OrderMgt" Assembly="QSPFulfillment" %>
<%@ Register TagPrefix="UC" TagName="ShipmentGroup" Src="UC/ShipmentGroup.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BatchList</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="../Includes/QSPFulfillment.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body topmargin="0">
		<center>
			<form id="Form1" method="post" runat="server">
				<!-- #include virtual="/Qspfulfillment/Includes/Menu.inc" -->
				<p><strong><font size="3">Orders Ready for Shipping</font></strong></p>
				<center><asp:button id="Button2" runat="server" text="Process Shipment" cssclass="Button4"></asp:button>
					<table class="boxlook" cellspacing="0" cellpadding="0" width="100%" border="0">
						<tr>
							<td style="HEIGHT: 13px" bgcolor="#ffffcc"><strong><font face="Verdana" size="2">Process 
										Information</font></strong></td>
						</tr>
						<tr>
							<td bgcolor="#ffffff" style="HEIGHT: 133px">
								<table cellspacing="0" cellpadding="0" width="100%" border="0">
									<tr>
										<td style="HEIGHT: 19px" valign="top"><strong><font size="2"><font face="Verdana">Operator 
														Name:</font> </font></strong>
										</td>
										<td style="HEIGHT: 19px" valign="top" align="left">&nbsp;
											<asp:label id="lblUsername" runat="server" font-size="XX-Small"></asp:label></td>
										<td style="HEIGHT: 19px" valign="top"><font face="Verdana" size="2"><strong>Date Shipped:</strong></font>
										</td>
										<td style="HEIGHT: 19px" valign="top">&nbsp;
											<asp:textbox id="tbDateShipped" runat="server" cssclass="TextBox" width="151px"></asp:textbox></td>
									</tr>
									<tr>
										<td style="HEIGHT: 17px" valign="top"><font size="2"><strong><font face="Verdana">Carrier:</font>
												</strong></font>
										</td>
										<td style="HEIGHT: 17px" valign="top" align="left" colspan="1" rowspan="1">&nbsp;
											<asp:dropdownlist id="ddCarrier" runat="server" font-size="XX-Small" datatextfield="Description" datavaluefield="Instance"></asp:dropdownlist></td>
										<td style="HEIGHT: 17px" valign="top"><font face="Verdana" size="2"><strong>Weight:</strong></font>
										</td>
										<td style="HEIGHT: 17px" valign="top">&nbsp;
											<asp:textbox id="tbWeight" runat="server" cssclass="TextBox" width="75px"></asp:textbox>&nbsp;<asp:dropdownlist id="ddWeightUnit" runat="server" font-size="XX-Small">
												<asp:listitem value="LBS">LBS</asp:listitem>
												<asp:listitem value="KG">KG</asp:listitem>
											</asp:dropdownlist></td>
									</tr>
									<tr>
										<td valign="top"><font size="2"><strong><font face="Verdana">Waybill #:</font> </strong>
											</font>
										</td>
										<td valign="top" align="left" colspan="1" rowspan="1">&nbsp;
											<asp:textbox id="tbWaybill" runat="server" cssclass="TextBox"></asp:textbox></td>
										<td valign="top"></td>
										<td valign="top">&nbsp;
											<asp:textbox id="tbExpectedDeliveryDate" runat="server" cssclass="TextBox" visible="False"></asp:textbox></td>
									</tr>
									<tr>
										<td valign="top"><font size="2"><strong><font face="Verdana">Total Cartons Shipped:</font> </strong>
											</font>
										</td>
										<td valign="top" align="left">&nbsp;
											<asp:textbox id="tbCartonsShipped" runat="server" cssclass="TextBox" width="72px"></asp:textbox></td>
										<td valign="top"><font face="Verdana" size="2"><strong># of Skids:</strong></font>
										</td>
										<td valign="top">&nbsp;
											<asp:textbox id="tbSkids" runat="server" cssclass="TextBox" width="72px"></asp:textbox></td>
									</tr>
									<tr>
										<td valign="top"><strong><font size="2"><font face="Verdana">Note:</font> </font></strong>
										</td>
										<td valign="top" align="left" colspan="3"><strong><font size="2">&nbsp;&nbsp;
													<asp:textbox id="tbNote" runat="server" cssclass="TextBox" width="540px"></asp:textbox></font></strong></td>
									</tr>
								</table>
								<p>
									<asp:label id="lblMessage" runat="server" font-size="X-Small" font-names="Verdana" forecolor="#00C000"
										font-bold="True"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<strong><font size="2">&nbsp;&nbsp;</font></strong></p>
							</td>
						</tr>
						<tr>
							<td style="HEIGHT: 96px">
								<table cellspacing="0" cellpadding="0" width="944" border="0" style="WIDTH: 944px; HEIGHT: 72px"
									height="72">
									<tr>
										<td style="WIDTH: 216px; HEIGHT: 29px">
											<p><strong><font face="Verdana" size="2">Search Order Ids</font></strong></p>
										</td>
										<td style="HEIGHT: 29px" align="left">
											<p>&nbsp;&nbsp;
												<asp:textbox id="OrderIDs" runat="server" cssclass="TextBox" width="376px"></asp:textbox>&nbsp;&nbsp;&nbsp;<asp:button id="SearchBtn" runat="server" text="Search" cssclass="Button4"></asp:button>&nbsp;<font size="2"><strong>
														&nbsp;&nbsp;&nbsp;</strong></font>&nbsp;&nbsp;&nbsp;
											</p>
										</td>
										<td style="HEIGHT: 29px" align="right"><FONT size="2"><STRONG>Print Status &nbsp;</STRONG></FONT>
											<asp:dropdownlist id="ddPrint" runat="server" font-size="XX-Small" width="92px">
												<asp:listitem value="-1">All</asp:listitem>
												<asp:listitem value="1">Printed</asp:listitem>
												<asp:listitem value="0">Unprinted</asp:listitem>
											</asp:dropdownlist></td>
									</tr>
									<TR>
										<TD style="WIDTH: 216px; HEIGHT: 27px">
											<P><STRONG><FONT face="Verdana" size="2">Catalogue <STRONG><FONT face="Verdana" size="2">Item</FONT></STRONG>
														&nbsp;Codes</FONT></STRONG></P>
										</TD>
										<TD style="HEIGHT: 27px" align="left" colSpan="1" rowSpan="1">&nbsp;&nbsp;
											<asp:textbox id="tbItemDesc" runat="server" cssclass="TextBox" width="488px"></asp:textbox></TD>
										<TD style="HEIGHT: 27px" align="left">&nbsp;
											<asp:CheckBox id="chkbBackOrderOnly" runat="server" Text="BackOrder Items Only?" Font-Names="Verdana"
												Font-Bold="True" Width="179px" Font-Size="9pt"></asp:CheckBox></TD>
										<TD style="HEIGHT: 27px" align="left"></TD>
										<TD style="HEIGHT: 27px" align="left"></TD>
										<TD style="HEIGHT: 27px" align="left"></TD>
									</TR>
                                    <tr>
                                        <TD style="WIDTH: 216px; HEIGHT: 27px">
											<P><STRONG><FONT face="Verdana" size="2">Shipment Group</FONT></STRONG></P>
										</TD>
                                        <td>
                                            <UC:ShipmentGroup id="ucShipmentGroup" runat="server" cssclass="boxlookW" AllShipmentGroupsOption="true"></UC:ShipmentGroup>
                                        </td>
                                    </tr>
								</table>
								<p>&nbsp;</p>
							</td>
						</tr>
						<tr>
							<td bgcolor="#ffffcc">
								<table cellspacing="0" cellpadding="0" width="100%" border="0">
									<tr>
										<td><strong><font face="Verdana" size="2">Order Information</font></strong></td>
										<td align="right"><strong><font size="2">Show:</font></strong>
											<asp:dropdownlist id="ddTop" runat="server" font-size="XX-Small" autopostback="True">
												<asp:listitem value="15">15</asp:listitem>
												<asp:listitem value="30" selected="True">30</asp:listitem>
												<asp:listitem value="10000">ALL</asp:listitem>
												<asp:listitem value="1">1</asp:listitem>
												<asp:listitem value="2">2</asp:listitem>
											</asp:dropdownlist></td>
									</tr>
								</table>
							</td>
						</tr>
						<tr>
							<td bgcolor="#ffffff"><asp:datagrid id="DataGrid1" runat="server" width="100%" autogeneratecolumns="False" borderstyle="Solid"
									bordercolor="Black" borderwidth="1px">
									<alternatingitemstyle backcolor="WhiteSmoke"></alternatingitemstyle>
									<itemstyle font-size="XX-Small"></itemstyle>
									<headerstyle font-size="XX-Small" font-names="Verdana" font-bold="True" backcolor="#FFFFCC"></headerstyle>
									<columns>
										<asp:templatecolumn headertext="Order ID">
											<headerstyle font-bold="True" horizontalalign="Center" verticalalign="Bottom"></headerstyle>
											<itemstyle horizontalalign="Center"></itemstyle>
											<itemtemplate>
												<a href='/QSPFulfillment/OrderMgt/BatchViewer.aspx?BatchOrderId=<%# DataBinder.Eval(Container.DataItem,"OrderId")%>' target=_blank>
													<%# DataBinder.Eval(Container.DataItem,"OrderId")%>
													<input type=hidden name="HOrderId" id="HOrderId" value='<%# DataBinder.Eval(Container.DataItem,"OrderId")%>' runat=server>
													<input type=hidden name="OrderHeader" id="OrderHeader" value='<%# DataBinder.Eval(Container.DataItem,"CustomerOrderHeaderInstance")%>' runat=server>
													<input type=hidden name="TransID" id="TransID" value='<%# DataBinder.Eval(Container.DataItem,"TransID")%>' runat=server>
												</a>
											</itemtemplate>
										</asp:templatecolumn>
										<asp:templatecolumn headertext="Ship To GroupId">
											<headerstyle horizontalalign="Center" verticalalign="Bottom"></headerstyle>
											<itemstyle horizontalalign="Center"></itemstyle>
											<itemtemplate>
												<%# DataBinder.Eval(Container.DataItem,"ShipToGroupId")%>
											</itemtemplate>
										</asp:templatecolumn>
										<asp:templatecolumn headertext="For: ">
											<headerstyle horizontalalign="Center" verticalalign="Bottom"></headerstyle>
											<itemstyle horizontalalign="Left"></itemstyle>
											<itemtemplate>
												<%# DataBinder.Eval(Container.DataItem,"For")%>
											</itemtemplate>
										</asp:templatecolumn>
										<asp:templatecolumn headertext="Ship To FMID">
											<headerstyle horizontalalign="Center" verticalalign="Bottom"></headerstyle>
											<itemstyle horizontalalign="Center"></itemstyle>
											<itemtemplate>
												<%# DataBinder.Eval(Container.DataItem,"ShipToFMId")%>
											</itemtemplate>
										</asp:templatecolumn>
										<asp:templatecolumn headertext="CampaignId">
											<headerstyle horizontalalign="Center" verticalalign="Bottom"></headerstyle>
											<itemstyle horizontalalign="Center"></itemstyle>
											<itemtemplate>
												<%# DataBinder.Eval(Container.DataItem,"CampaignId")%>
											</itemtemplate>
										</asp:templatecolumn>
										<asp:templatecolumn headertext="Shipment Group">
											<headerstyle horizontalalign="Center" verticalalign="Bottom"></headerstyle>
											<itemstyle horizontalalign="Center"></itemstyle>
											<itemtemplate>
                                                <input type=hidden name="ShipmentGroupID" id="ShipmentGroupID" value='<%# DataBinder.Eval(Container.DataItem,"ShipmentGroupID")%>' runat=server>
												<%# DataBinder.Eval(Container.DataItem,"ShipmentGroupName")%>
											</itemtemplate>
										</asp:templatecolumn>
										<asp:templatecolumn headertext="Date Printed">
											<headerstyle horizontalalign="Center" verticalalign="Bottom"></headerstyle>
											<itemstyle horizontalalign="Center"></itemstyle>
											<itemtemplate>
												<%# DataBinder.Eval(Container.DataItem,"DatePrinted")%>
											</itemtemplate>
										</asp:templatecolumn>
										<asp:templatecolumn headertext="FS Ship Date">
											<headerstyle horizontalalign="Center" verticalalign="Bottom"></headerstyle>
											<itemstyle horizontalalign="Center"></itemstyle>
											<itemtemplate>
												<%# DataBinder.Eval(Container.DataItem,"SuppliesDeliveryDate")%>
											</itemtemplate>
										</asp:templatecolumn>
										<asp:templatecolumn headertext="Order Ship Date">
											<headerstyle horizontalalign="Center" verticalalign="Bottom"></headerstyle>
											<itemstyle horizontalalign="Center"></itemstyle>
											<itemtemplate>
												<%# DataBinder.Eval(Container.DataItem,"OrderShippingDate")%>
											</itemtemplate>
										</asp:templatecolumn>
										<asp:templatecolumn headertext="Print Packing Slip">
											<headerstyle verticalalign="Bottom" horizontalalign="Center"></headerstyle>
											<itemstyle horizontalalign="Center"></itemstyle>
											<itemtemplate>
												<cc2:PackingSlipLinkButton id="hylPackingSlipLinkButton" runat="server" CausesValidation="false" CommandName="PrintPackingSlip" OrderID='<%# DataBinder.Eval(Container.DataItem, "OrderID") %>' BatchID='<%# DataBinder.Eval(Container.DataItem, "BatchID") %>' BatchDate='<%# DataBinder.Eval(Container.DataItem, "BatchDate") %>'>Print</cc2:PackingSlipLinkButton>
											</itemtemplate>
										</asp:templatecolumn>
										<asp:templatecolumn headertext="Select&lt;BR&gt;For&lt;BR&gt;Shipping">
											<headerstyle horizontalalign="Center" verticalalign="Bottom"></headerstyle>
											<itemstyle horizontalalign="Center"></itemstyle>
											<itemtemplate>
												<asp:checkbox id="cbShip" runat="server" font-size="xx-small"></asp:checkbox>
											</itemtemplate>
										</asp:templatecolumn>
										<asp:templatecolumn headertext="Back Order">
											<headerstyle horizontalalign="Center" verticalalign="Bottom"></headerstyle>
											<itemstyle horizontalalign="Center"></itemstyle>
											<itemtemplate>
												<%# DataBinder.Eval(Container.DataItem,"IsSplit")%>
											</itemtemplate>
										</asp:templatecolumn>
										<asp:templatecolumn>
											<headerstyle horizontalalign="Center" verticalalign="Bottom"></headerstyle>
											<itemstyle horizontalalign="Center"></itemstyle>
											<itemtemplate>
												<a href='/QSPFulfillment/OrderMgt/ShipmentEditItems.aspx?BatchOrderId=<%# DataBinder.Eval(Container.DataItem,"OrderId")%>&ShipmentGroupID=<%# DataBinder.Eval(Container.DataItem,"ShipmentGroupID")%>' target=EditItem>
													<img src="../Images/edit.gif" border="0"></a>
											</itemtemplate>
										</asp:templatecolumn>
									</columns>
								</asp:datagrid><br>
							</td>
						</tr>
					</table>
					<br>
					<center><asp:button id="Button1" runat="server" text="Process Shipment" cssclass="Button4"></asp:button>
			</form>
		</center>
		</CENTER>
		<center></center>
		</CENTER>
	</body>
</HTML>
