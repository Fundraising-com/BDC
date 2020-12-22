<%@ Register TagPrefix="uc1" TagName="OrderSupplyList" Src="OrderSupplyList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AddressControlForm" Src="AddressControlForm.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.ProgramAgreementSupplyForm" Codebehind="ProgramAgreementSupplyForm.ascx.cs" %>


<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td align="left"> <!--Section Body -->
			<table id="Table4" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td><uc1:ordersupplylist id="SupplyList" runat="server"></uc1:ordersupplylist></td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td align="left" style="height: 50px"><br>
			<asp:label id="lblTitle" runat="server"></asp:label></td>
	</tr>
	<tr id="trValSum" runat="server" visible="false">
		<td><asp:validationsummary id="ValSum" runat="server" HeaderText="Correct the following error to proceed."
				CssClass="LabelError"></asp:validationsummary></td>
	</tr>
	<tr>
		<td align="left"> <!--Section Body -->
			<table id="Table4wdd" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td vAlign="top">
						<table id="Table533" cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td><asp:label id="lblTitleSupplyShipping" runat="server" CssClass="StandardLabel">Supply Shipping Information :
									</asp:label>&nbsp;&nbsp;&nbsp;
									<br>
								</td>
								<td vAlign="top"></td>
							</tr>
						</table>
						<table id="Table4dd" cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<td vAlign="top"><asp:label id="Label19" runat="server" CssClass="StandardLabel">
										Ship&nbsp;To&nbsp;:&nbsp;
									</asp:label>
								</td>
								<td></td>
								<td width="100%">
									<table id="Tablew3" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td vAlign="top"><asp:radiobuttonlist id="radBtnLstShipTo" runat="server" CssClass="DescLabel" RepeatDirection="Vertical"
													CellSpacing="0" CellPadding="0" AutoPostBack="True" onselectedindexchanged="radBtnLstShipTo_SelectedIndexChanged">
													<asp:ListItem Value="1">FSM</asp:ListItem>
													<asp:ListItem Value="2" Selected="True">Account (Ship To Address)</asp:ListItem>
													<asp:ListItem Value="3">Enter New Address</asp:ListItem>
												</asp:radiobuttonlist></td>
											<td>&nbsp;
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<table id="Table43dd" cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<td vAlign="top"><asp:label id="Label31" CssClass="StandardLabel" runat="server">
										Order Date :
									</asp:label></td>
								<td>&nbsp;
								</td>
								<td><asp:label id="lblOrderDate" CssClass="DescInfoLabel" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<td><asp:label id="lblLabelDeliveryDate" runat="server" CssClass="StandardLabel">Requested&nbsp;Shipment&nbsp;Date:<span class="RequiredSymbolLabel">*</span>&nbsp;
									</asp:label></td>
								<td>&nbsp;</td>
								<td>
									<table id="Table3" cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<TD>
    											    <!--<asp:textbox id="txtDeliveryDate_old" runat="server" AutoPostBack="False" Font-Size="9px" Height="20px" Font-Names="Verdana, Arial, Tahoma" Width="100px"></asp:textbox>-->
    											    <asp:textbox id="txtDeliveryDate" runat="server" AutoPostBack="False" Width="100px"></asp:textbox>
											    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtDeliveryDate" Mask="99/99/9999" MessageValidatorTip="false" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" CultureName="en-US" />
											</TD>
											<td>
											    <asp:hyperlink id="hypLnkDeliveryDate" runat="server" NavigateUrl="javascript:void(0);" ImageUrl="~/images/Calendar.gif" AlternateText="Click here to select the requested shipment date from a popup calendar !"></asp:hyperlink>&nbsp;
											</td>
											<td>
											    &nbsp;
											</td>
											<td>
											    <asp:requiredfieldvalidator id="reqFldVal_DeliveryDate" runat="server" CssClass="LabelError" ErrorMessage="The Requested Shipment Date is required." ControlToValidate="txtDeliveryDate">*</asp:requiredfieldvalidator>
											</td>
											<td>
											    <asp:comparevalidator id="compVal_DeliveryDate" runat="server" CssClass="LabelError" ErrorMessage="The Requested Shipment Date is invalid" ControlToValidate="txtDeliveryDate" Operator="DataTypeCheck" Type="Date">*</asp:comparevalidator>
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<td>
									<asp:label id="Label12" runat="server" CssClass="StandardLabel" Width="155px">Requested&nbsp;Lead-Time:&nbsp;</asp:label>
								</td>
								<td>&nbsp;</td>
								<td>
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td>
											    <asp:label id="lblDayLeadTime" runat="server" CssClass="DescInfoLabel"> 0
								                </asp:label>
								            </td>
											<td>
											    <asp:label id="Label33" runat="server" CssClass="StandardLabel">&nbsp;&nbsp;Business&nbsp;Day(s)
								                </asp:label>
								            </td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<td colspan="3">
									<table border="0" cellpadding="0" cellspacing="0">
										<tr>
											<td vAlign="top">
												<asp:label id="Label28" runat="server" CssClass="RequiredSymbol">
													*&nbsp;
												</asp:label>
											</td>
											<td>
												<asp:label id="Label30" runat="server" CssClass="RequiredSymbolLabel">
													Required Field
												</asp:label>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
					<td>&nbsp;
					</td>
					<td vAlign="top">
						<table id="tblAddressSupply" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
							<tr class="HeaderItemStyle">
								<td><asp:label id="Label15" runat="server">
										&nbsp;Supply Shipping Address
									</asp:label>
								</td>
							</tr>
							<tr>
								<td><uc1:addresscontrolform id="AddressSupply" LabelOrgNameText="Account&nbsp;Name" runat="server"></uc1:addresscontrolform></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<br>
		</td>
	</tr>
	<tr id="trBusinessMessage" runat="server" visible="false">
		<td align="center"> <!--Section Body -->
			<table id="Table5dd33" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td><asp:label id="lblBusinessMessage" runat="server" CssClass="BizRuleLabel"></asp:label>&nbsp;&nbsp;&nbsp;
						<br>
						<br>
					</td>
					<td vAlign="top"></td>
				</tr>
			</table>
		</td>
	</tr>
</table>
