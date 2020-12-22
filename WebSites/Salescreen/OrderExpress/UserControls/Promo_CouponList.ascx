<%@ Reference Control="Promo_CouponDetailInfo.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.Promo_CouponList" Codebehind="Promo_CouponList.ascx.cs" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl.DataGridControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="uc1" TagName="SearchModule" Src="~/UserControls/SearchModule.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ToolBar" Src="~/UserControls/ToolBar.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<TABLE id="Table5" cellSpacing="0" cellPadding="0" align="left" border="0">
	<TR>
		<TD>
			<TABLE id="Table6" cellSpacing="0" cellPadding="0" width="20%" border="0">
				<TR>
					<TD><uc1:searchmodule id="QSPFormSearchModule" runat="server"></uc1:searchmodule></TD>
				</TR>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td>
									<hr width="100%" color="#666666" SIZE="1">
								</td>
							</tr>
							<TR>
								<TD>
									<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
										<TR>
											<TD colSpan="2"><asp:label id="Label6" CssClass="StandardLabel" Runat="server">
													Filter&nbsp;By:
												</asp:label></TD>
										</TR>
										<TR>
											<td>
												<table cellSpacing="0" cellPadding="0" width="100%" border="0">
													<tr>
														<td><asp:label id="Label5" runat="server" CssClass="ModuleSearchText">
																Subdivision&nbsp;:
															</asp:label></td>
														<TD noWrap><asp:dropdownlist id="ddlSubdivision" runat="server"></asp:dropdownlist></TD>
														<td>&nbsp;
														</td>
														<td><asp:label id="LblIsNational" runat="server" CssClass="ModuleSearchText">
																Coupon&nbsp;Type:
															</asp:label></td>
														<TD noWrap><asp:dropdownlist id="ddlNational" runat="server">
																<asp:ListItem Selected="True" Value="-1">--SELECT--</asp:ListItem>
																<asp:ListItem Value="0">Not National</asp:ListItem>
																<asp:ListItem Value="1">National</asp:ListItem>
															</asp:dropdownlist></TD>
													</tr>
													<tr>
														<td><asp:label id="Label10" runat="server" CssClass="ModuleSearchText">
																Received&nbsp;Status:
															</asp:label></td>
														<TD noWrap><asp:dropdownlist id="ddlReceivedStatus" runat="server">
																<asp:ListItem Selected="True" Value="-1">--SELECT--</asp:ListItem>
																<asp:ListItem Value="0">Not Received</asp:ListItem>
																<asp:ListItem Value="1">Received</asp:ListItem>
															</asp:dropdownlist></TD>
														<td>&nbsp;
														</td>
														<TD id="tdFilterFMReportedTo" align="right" colSpan="2" runat="server">
															<table cellSpacing="0" cellPadding="0" border="0">
																<tr>
																	<td><asp:label id="Label3" runat="server" CssClass="ModuleSearchText">
																			&nbsp;All&nbsp;FM&nbsp;Reported&nbsp;To&nbsp;:&nbsp;
																		</asp:label></td>
																	<td><asp:checkbox id="chkReportedTo" runat="server" CssClass="boxes"></asp:checkbox></td>
																</tr>
															</table>
														</TD>
													</tr>
													<tr>
														<TD ><asp:label id="Label7" runat="server" CssClass="ModuleSearchText">
																Expiration&nbsp;Date&nbsp;Between:
															</asp:label></TD>
														<td  colspan=2>
															<table cellSpacing="0" cellPadding="0" border="0">
																<tr>
																	<TD><asp:textbox id="txtStartDate" runat="server" Font-Size="9px" Height="14px" Font-Names="Verdana, Arial, Tahoma" Width="100px"></asp:textbox>
																	<ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtStartDate"
                                                        Mask="99/99/9999" MessageValidatorTip="false" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" CultureName="en-US" />
																	</TD>
																	<td><asp:hyperlink id="hypLnkStartDate" runat="server" ToolTip="Click here to select the start date from a popup calendar !"
																			ImageUrl="~/images/Calendar.gif" NavigateUrl="javascript:void(0);">HyperLink</asp:hyperlink>&nbsp;
																	</td>
																	<td><asp:requiredfieldvalidator id="reqFldVal_StartDate" runat="server" CssClass="LabelError" ControlToValidate="txtStartDate"
																			ErrorMessage="The Start Date is required.">*</asp:requiredfieldvalidator><asp:comparevalidator id="compVal_StartDate" runat="server" CssClass="LabelError" ControlToValidate="txtStartDate"
																			ErrorMessage="The Start Date is invalid." Type="Date" Operator="DataTypeCheck">*</asp:comparevalidator><asp:comparevalidator id="Comparevalidator1" runat="server" CssClass="LabelError" ControlToValidate="txtStartDate"
																			ErrorMessage="The Start Date must be less or equal than the End Date." Type="Date" Operator="LessThanEqual" ControlToCompare="txtEndDate">*</asp:comparevalidator></td>
																
																	<TD><asp:textbox id="txtEndDate" runat="server" Font-Size="9px" Height="14px" Font-Names="Verdana, Arial, Tahoma" Width="100px"></asp:textbox>
																	<ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtEndDate"
                                                        Mask="99/99/9999" MessageValidatorTip="false" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" CultureName="en-US" />
																	</TD>
																	<td><asp:hyperlink id="hypLnkEndDate" runat="server" ToolTip="Click here to select the end date from a popup calendar !"
																			ImageUrl="~/images/Calendar.gif" NavigateUrl="javascript:void(0);">HyperLink</asp:hyperlink>&nbsp;
																	</td>
																	<td><asp:requiredfieldvalidator id="reqFldVal_EndDate" runat="server" CssClass="LabelError" ControlToValidate="txtEndDate"
																			ErrorMessage="The End Date is required.">*</asp:requiredfieldvalidator><asp:comparevalidator id="compVal_EndDate" runat="server" CssClass="LabelError" ControlToValidate="txtEndDate"
																			ErrorMessage="The Start Date is invalid." Type="Date" Operator="DataTypeCheck">*</asp:comparevalidator></td>
																</tr>
															</table>
														</td>
														
														<td colspan="2"></td>
													</tr>
												</table>
											</td>
										</TR>
									</TABLE>
								</TD>
							</TR>
							<tr>
								<td>
									<hr width="100%" color="#666666" SIZE="1">
								</td>
							</tr>
						</TABLE>
					</TD>
				</TR>
				<tr>
					<td>
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td><asp:label id="lblNote" runat="server" CssClass="FilterNoteTitle">Note&nbsp;:</asp:label></td>
								<td><asp:label id="lblNoteDesc" runat="server" CssClass="FilterNoteDesc">All criteria is considered when refreshing the list.</asp:label></td>
							</tr>
						</table>
						<br>
					</td>
				</tr>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD><br>
			<asp:hyperlink id="hypLnkAddNew" runat="server" NavigateUrl="javascript:void(0);" ImageUrl="~/images/BtnAdd.gif"></asp:hyperlink></TD>
	</TR>
	<tr>
		<td><BR>
			<TABLE cellSpacing="0" cellPadding="0" width="400" border="0">
				<TR>
					<TD align="left"><asp:label id="lblCurrentIndex" runat="server" CssClass="CurrentPageIndexLabel">Page 1 of 1</asp:label></TD>
					<td align="right"><asp:label id="Labelsss4" runat="server" CssClass="FilterNoteDesc">Click on Column Headings to Resort Data.</asp:label></td>
				</TR>
			</TABLE>
		</td>
	</tr>
	<TR>
		<TD><!--DataGrid  --><cc2:sorteddatagrid id=dtgPromo_Coupon runat="server" Font-Size="10pt" ShowFooter="True" AutoGenerateColumns="False" DataSource="<%# DVPromo_Coupon %>" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" BackColor="White" CellPadding="3" AllowSorting="True" AllowPaging="True" PageSize="10" width="700px" SearchMode="0">
				<SelectedItemStyle CssClass="SelectedItemStyle"></SelectedItemStyle>
				<AlternatingItemStyle CssClass="AlternatingItemStyle_off"></AlternatingItemStyle>
				<ItemStyle CssClass="ItemStyle_off"></ItemStyle>
				<HeaderStyle Wrap="False" ForeColor="White" CssClass="HeaderItemStyle"></HeaderStyle>
				<FooterStyle CssClass="FooterItemStyle"></FooterStyle>
				<Columns>
					<asp:TemplateColumn>
						<ItemTemplate>
							<ASP:IMAGEBUTTON id="imgBtnDetail" runat="server" ImageUrl="~/images/BtnDetail.gif"></ASP:IMAGEBUTTON>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="promotion_id" HeaderText="Contract&nbsp;No">
						<ItemTemplate>
							<asp:Label id="Contract" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.promotion_id") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="vendor_name" HeaderText="Vendor">
						<ItemTemplate>
							<asp:Label id="Vendor_ID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.vendor_name")%>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="fm_id" HeaderText="FM&#160;ID">
						<ItemTemplate>
							<asp:Label id="lblFM_ID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.fm_id") %>' />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Coupon&nbsp;Offer">
						<ItemTemplate>
							<asp:Label id="lblCouponOffer" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.offer") %>' />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Logo">
						<ItemTemplate>
							<ASP:IMAGEBUTTON id="imgBtnLogo" runat="server" ImageUrl="" Width="100" Height="100"></ASP:IMAGEBUTTON>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
				<PagerStyle CssClass="PagerItemStyle" Mode="NumericPages"></PagerStyle>
			</cc2:sorteddatagrid></TD>
	</TR>
	<TR>
		<TD style="height: 38px">
			<TABLE cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<td><br>
						<asp:label id="lblTotal" runat="server" CssClass="TotalListLabel">
							Number of Coupon Agreement(s):
						</asp:label></td>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>
