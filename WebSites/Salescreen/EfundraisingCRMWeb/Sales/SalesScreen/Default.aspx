<%@ Page debug="true" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EFundraisingCRMWeb.Sales.SalesScreen.Default" %>
<%@ MasterType virtualpath="~/Site1.master" %>
<%@ Register TagPrefix="uc1" TagName="ProductLookUp" Src="../../Components/User/Package/ProductLookUp.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SaleInfoList" Src="../../Components/User/Sales/SaleInfoList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Address" Src="../../Components/User/Address.ascx" %>

<%@ Register TagPrefix="uc1" TagName="ClientHeader" Src="../../Components/User/ClientHeader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CustomerInformation" Src="../../Components/User/ClientControls/CustomerInformation.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeadSummary" Src="../../Components/User/Lead/LeadSummary.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientRequests" Src="../../Components/User/ClientControls/ClientRequests.ascx" %>

<%@ Register src="../../Components/User/AddressHygiene/AddressHygiene.ascx" tagname="AddressHygiene" tagprefix="uc2" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 	
		<SCRIPT language="javascript">

function showPleaseWait()
{
document.getElementById('PleaseWait').style.display = 'block';
}


		</SCRIPT>
		<TABLE height="100%" cellSpacing="0" cellPadding="0" width="800" border="0">
			<TR>
				<TD class="FrameBorder" width="1" height="1">&nbsp;
				</TD>
				<TD class="FrameBody" align="center">&nbsp;</TD>
				<TD class="FrameBody" align="center" style="width: 910px">
					<uc1:ClientHeader id="ClientHeader1" runat="server"></uc1:ClientHeader></TD>
				<TD class="FrameBorder" width="1" height="1"></TD>
			</TR>
			<TR>
				<TD class="FrameBorder" width="1" height="22"></TD>
				<TD class="FrameBody" align="left" height="22"></TD>
				<TD class="FrameBody" align="left" height="22" style="width: 910px">
					<HR width="100%" color="gainsboro" noShade SIZE="2">
					<asp:Label id="ErrorClientInfoLabel" runat="server" Visible="False" Font-Size="10pt" ForeColor="Red">Correct the invalid fields (*)</asp:Label></TD>
				<TD class="FrameBorder" width="1" height="22"></TD>
			</TR>
			<TR>
				<TD class="FrameBorder" width="1"></TD>
				<TD class="FrameBody"></TD>
				<TD class="FrameBody" style="width: 910px">
					<TABLE height="100%" cellSpacing="0" cellPadding="0">
						<TR>
							<TD vAlign="top" width="70%">
								<TABLE height="100%" cellSpacing="0" cellPadding="0" style="width: 596px">
									<TR>
										<TD style="width: 628px">
											<TABLE height="100%" cellSpacing="0" cellPadding="0" style="width: 612px">
												<TR>
													<TD vAlign="top" height="6" style="width: 644px">
														<asp:Label id="Label4" runat="server" CssClass="FrameTitleColor">Client Information</asp:Label></TD>
												</TR>
												<TR>
													<TD style="width: 644px">
														<uc1:CustomerInformation id="ClientInformation" runat="server"></uc1:CustomerInformation></TD>
												</TR>
											</TABLE>
											<BR>
										</TD>
									</TR>
									<TR>
										<TD style="width: 628px">
											<TABLE id="billingShippingTable" height="100%" cellSpacing="0" cellPadding="0" border="0"
												runat="server">
												<TR>
													<TD vAlign="top" style="width: 281px">
														<asp:Label id="Label2" runat="server" CssClass="FrameTitleColor">Billing Address</asp:Label><BR>
														<TABLE id="Table2" cellSpacing="0" cellPadding="0" border="0">
															<TR>
																<TD>
																	<uc1:Address id="BillingAddress" runat="server"></uc1:Address></TD>
															</TR>
															<TR>
																<TD>
																	<asp:CheckBox class="NormalText" id="SameAsCheckBox" runat="server" AutoPostBack="True" Text="Same for Shipping Address        "
																		TextAlign="Left" oncheckedchanged="SameAsCheckBox_CheckedChanged"></asp:CheckBox></TD>
															</TR>
															<TR>
																<TD align="left">
																	<TABLE id="Table6" cellSpacing="1" cellPadding="1" width="300" border="0">
																		<TR>
																			<TD style="width: 157px">
																				<asp:Label class="NormalText" id="Label6" runat="server">Pick up at Warehouse  </asp:Label></TD>
																			<TD style="width: 3px">
																				<asp:CheckBox id="PickUpCheckBox" runat="server" AutoPostBack="True" Text="                 "
																					TextAlign="Left" oncheckedchanged="PickUpCheckBox_CheckedChanged" Enabled="False"></asp:CheckBox></TD>
																			<TD></TD>
																		</TR>
																	</TABLE>
																	<asp:DropDownList id="WarehouseDropDownList" runat="server" Width="180px" Visible="False" ForeColor="Green"
																		Font-Bold="True" onselectedindexchanged="WarehouseDropDownList_SelectedIndexChanged"></asp:DropDownList></TD>
															</TR>
															<TR>
																<TD>
																	<asp:Label id="WarehouseErrorLabel" runat="server" Visible="False" Font-Size="10pt" ForeColor="Red">Error: Warehouse doesnt exist!</asp:Label>
																	<asp:Label id="AddressHygieneStatusBillLabel0" runat="server" Visible="False" 
                                                                        Font-Size="10pt" ForeColor="Red">address hygiene</asp:Label></TD>
															</TR>
														</TABLE>
													</TD>
													<TD vAlign="top" width="250" runat="server" id="ShipTD">
														<asp:Label id="Label3" runat="server" CssClass="FrameTitleColor">Shipping Address</asp:Label><BR>
														<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="250" border="0" 
                                                            >
															<TR>
																<TD>
																	<uc1:Address id="ShippingAddress" runat="server"></uc1:Address>
                                                                </TD>
															</TR>
															<TR>
																<TD>
																	<asp:Label id="AddressHygieneStatusShipLabel" runat="server" Visible="False" 
                                                                        Font-Size="10pt" ForeColor="Red">address hygiene</asp:Label></TD>
															</TR>
															<TR>
																<TD>
																	<asp:Label id="WarningLabel" runat="server" Visible="False" 
                                                                        Font-Size="10pt" ForeColor="#FF6600" Font-Bold="True">Note that changes to 
                                                                    the Shipping Address will NOT affect already confirmed sales.</asp:Label></TD>
															</TR>
														</TABLE>
													</TD>
												</TR>
											</TABLE>
										</TD>
									</TR>
								</TABLE>
							</TD>
							<TD vAlign="top">
								<TABLE id="leadRequestTable" cellSpacing="1" cellPadding="0" border="0" 
                                    runat="server" style="height: 282px">
									<TR>
										<TD vAlign="top" align="center"><!--HERE -->
											<TABLE height="100%" cellSpacing="0" cellPadding="0">
												<TR vAlign="top">
													<TD vAlign="top" height="45" align="left">
														<TABLE id="Table3" cellSpacing="1" cellPadding="1" border="0">
															<TR>
																<TD align="left">
																	<asp:Label id="Label5" runat="server" CssClass="FrameTitleColor">Lead Summary</asp:Label></TD>
															</TR>
															<TR>
																<TD align="left" style="height: 236px">
																	<uc1:LeadSummary id="LeadSummary1" runat="server"></uc1:LeadSummary></TD>
															</TR>
														</TABLE>
													</TD>
												</TR>
												<TR>
													<TD vAlign="top" height="10"></TD>
												</TR>
												<TR vAlign="top">
													<TD vAlign="top" width="200" align="left">
														<TABLE id="Table4" cellSpacing="1" cellPadding="1" border="0">
															<TR>
																<TD>
																	<asp:Label id="Label7" runat="server" CssClass="FrameTitleColor">Requests</asp:Label></TD>
															</TR>
															<TR>
																<TD>
																	<uc1:ClientRequests id="ClientRequests1" runat="server"></uc1:ClientRequests></TD>
															</TR>
														</TABLE>
													</TD>
												</TR>
											</TABLE> <!--END HERE --></TD>
									</TR>
								</TABLE>
							</TD>
						</TR>
					</TABLE>
				</TD>
				<TD width="1"></TD>
			</TR>
			<TR>
				<TD class="FrameBorder" width="1" height="4"></TD>
				<TD class="FrameBody" height="4"></TD>
				<TD class="FrameBody" height="4" style="width: 910px">
                    <uc2:AddressHygiene ID="AddressHygieneControl" runat="server" />
                </TD>
				<TD class="FrameBorder" width="1" height="4"></TD>
			</TR>
			<TR>
				<TD class="FrameBorder" width="1" height="25"></TD>
				<TD class="FrameBody" height="25"></TD>
				<TD class="FrameBody" height="25" style="width: 910px">
					<TABLE id="Table5" cellSpacing="1" cellPadding="1" width="300" border="0">
						<TR>
							<TD>
								<asp:Label id="Label1" runat="server" CssClass="FrameTitleColor">Sale Info</asp:Label></TD>
						</TR>
						<TR>
							<TD>
								<uc1:SaleInfoList id="SaleInfoList1" runat="server"></uc1:SaleInfoList></TD>
						</TR>
					</TABLE>
				</TD>
				<TD class="FrameBorder" width="1" height="25"></TD>
			</TR>
			<TR>
				<TD height="5"></TD>
				<TD height="5"></TD>
			</TR>
			<TR>
				<TD class="FrameBorder" width="1" height="23"></TD>
				<TD align="right" height="23"></TD>
				<TD align="right" height="23" style="width: 910px">&nbsp;
				</TD>
				<TD class="FrameBorder" width="1" height="23"></TD>
			</TR>
			<TR>
				<TD class="FrameBorder" width="1" height="2"></TD>
				<TD align="right" height="2"></TD>
				<TD align="right" height="2" style="width: 910px">
					<asp:Label id="ErrorLabel" runat="server" Visible="False" Font-Size="10pt" ForeColor="Red"></asp:Label>
					<asp:Button ID="AdminButton" runat="server" onclick="AdminButton_Click" 
                        Text="Admin Section" Visible="False" Width="108px" />
					<asp:Button ID="SyncButton" runat="server" onclick="SyncButton_Click" 
                        Text="Sync Info" Visible="False" Width="95px" />
					<asp:Button ID="newShippingAddressButton" runat="server" onclick="NewAddressButton_Click" 
                        Text="New Shipping Address" Width="143px" />
                    <asp:Button ID="IgnoreButton" runat="server" onclick="IgnoreButton_Click" 
                        Text="Ignore Address Hygiene" Visible="False" Width="165px" />
                    
                    <asp:Button id="saveInfobutton" Text="Save Client" Runat="server" CausesValidation="False" onclick="saveInfobutton_Click"></asp:Button>
					<asp:Button id="newSalesbutton" CssClass="NormaltextBold" Text="Create New sale" Runat="server"
						CausesValidation="False" Enabled="False" onclick="newSalesbutton_Click"></asp:Button></TD>
				<TD class="FrameBorder" width="1" height="2"></TD>
			</TR>
			<TR>
				<TD height="5"></TD>
				<TD height="5"></TD>
			</TR>
		</TABLE>
		</asp:Content>

