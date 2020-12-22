<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.PostalAddressForm" Codebehind="PostalAddressForm.ascx.cs" %>

<table id="tblFormDetailTitle" cellSpacing="0" cellPadding="0" width="550px" border="0">
	<TR>
		<TD>
			<asp:ImageButton id="imgBtnAddNew" runat="server" ImageUrl="~/images/BtnAdd.gif" CommandName="Add"
				CausesValidation="False"></asp:ImageButton>
		</TD>
	</TR>
	<tr>
		<td><asp:datalist id=dtLstAddress runat="server" DataKeyField="postal_address_entity_id" width="500px" DataSource="<%# DVAddress %>">
				<SeparatorTemplate>
					<hr width="100%" size="1px" color="#003366">
				</SeparatorTemplate>
				<ItemTemplate>
					<TABLE cellSpacing="0" cellPadding="3" width="500px" border="0">
						<TR id="htmlTblRowTitleAddress" runat="server">
							<td colspan="2">
								<table border="0" cellpadding="0" cellspacing="0">
									<tr>
										<TD>
											<asp:label id="lblTitleItemNo" CssClass="StandardLabel" runat="server">Shipping Address&nbsp;#&nbsp;</asp:label></TD>
										<TD>
											<asp:Label id="lblItemNo" runat="server" CssClass="DescLabel" Text='<%# (Convert.ToInt32(DataBinder.Eval(Container, "ItemIndex")) + 1) %>'>
											</asp:Label>
										</TD>
									</tr>
								</table>
							</td>
						</TR>
						<TR id="htmlTblRowTypeAddress" runat="server">
							<TD>
								<asp:label id="Label1" CssClass="StandardLabel" runat="server">Type&nbsp;:&nbsp;</asp:label></TD>
							<TD>
								<table border="0" cellpadding="0" cellspacing="0">
									<tr>
										<td>
											<asp:DropDownList id="ddlType" runat="server" Width="300px" DataSource="<%# tblTypeAddress %>" DataTextField="postal_address_type_name" DataValueField="postal_address_type_id" SelectedIndex='<%# getSelectedIndex(tblTypeAddress,Convert.ToString(DataBinder.Eval(Container, "DataItem.postal_address_type_id"))) %>'>
											</asp:DropDownList>
										</td>
										<td>
											<asp:RequiredFieldValidator id="ReqFldVal_Type" CssClass="LabelError" runat="server" ErrorMessage="The State is required"
												ControlToValidate="ddlType">*</asp:RequiredFieldValidator>
										</td>
									</tr>
								</table>
							</TD>
						</TR>
						<TR>
							<TD>
								<asp:label id="lblFirstName" CssClass="StandardLabel" runat="server">First&nbsp;Name&nbsp;:&nbsp;</asp:label></TD>
							<TD>
								<table border="0" cellpadding="0" cellspacing="0">
									<tr>
										<td>
											<asp:TextBox id="txtFirstName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.First_Name") %>' Width="350px">
											</asp:TextBox>
										</td>
										<td>
											<asp:RequiredFieldValidator id="ReqFldVal_FName" CssClass="LabelError" runat="server" ErrorMessage="The Contact First Name is required"
												ControlToValidate="txtFirstName">*</asp:RequiredFieldValidator>
										</td>
									</tr>
								</table>
							</TD>
						</TR>
						<TR>
							<TD>
								<asp:label id="lblLastName" CssClass="StandardLabel" runat="server">Last&nbsp;Name&nbsp;:&nbsp;</asp:label></TD>
							<TD>
								<table border="0" cellpadding="0" cellspacing="0">
									<tr>
										<td>
											<asp:TextBox id="txtLastName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Last_Name") %>' Width="350px">
											</asp:TextBox>
										</td>
										<td>
											<asp:RequiredFieldValidator id="ReqFldVal_LName" CssClass="LabelError" runat="server" ErrorMessage="The Contact Last Name is required"
												ControlToValidate="txtLastName">*</asp:RequiredFieldValidator>
										</td>
									</tr>
								</table>
							</TD>
						</TR>
						<TR>
							<TD vAlign="top">
								<asp:label id="lblAddressLine1" CssClass="StandardLabel" runat="server">Address&nbsp;Line&nbsp;1&nbsp;:&nbsp;</asp:label></TD>
							<TD>
								<table border="0" cellpadding="0" cellspacing="0">
									<tr>
										<td>
											<asp:TextBox id="txtAddressLine1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Address1") %>' Width="350px">
											</asp:TextBox>
										</td>
										<td>
											<asp:RequiredFieldValidator id="ReqFldVal_AddrL1" CssClass="LabelError" runat="server" ErrorMessage="The Address Line 1 is required"
												ControlToValidate="txtAddressLine1">*</asp:RequiredFieldValidator>
										</td>
									</tr>
								</table>
							</TD>
						</TR>
						<TR>
							<TD vAlign="top">
								<asp:label id="lblAddressLine2" CssClass="StandardLabel" runat="server">Address&nbsp;Line&nbsp;2&nbsp;:&nbsp;</asp:label></TD>
							<TD>
								<asp:TextBox id="txtAddressLine2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Address2") %>' Width="350px">
								</asp:TextBox>
							</TD>
						</TR>
						<TR>
							<TD>
								<asp:label id="lblCity" CssClass="StandardLabel" runat="server">City&nbsp;:&nbsp;</asp:label>
							</TD>
							<TD>
								<table border="0" cellpadding="0" cellspacing="0">
									<tr>
										<td>
											<table border="0" cellpadding="0" cellspacing="0">
												<tr>
													<td>
														<asp:TextBox id="txtCity" Text='<%# DataBinder.Eval(Container, "DataItem.City") %>' runat="server" Width="170px">
														</asp:TextBox>
													</td>
													<td>
														<asp:RequiredFieldValidator id="ReqFldVal_City" CssClass="LabelError" runat="server" ErrorMessage="The City is required"
															ControlToValidate="txtCity">*</asp:RequiredFieldValidator>
													</td>
												</tr>
											</table>
										</td>
										<TD>
											<asp:label id="lblCounty" CssClass="StandardLabel" runat="server">&nbsp;County&nbsp;:&nbsp;</asp:label></TD>
										<TD>
											<asp:TextBox id="txtCounty" Text='<%# DataBinder.Eval(Container, "DataItem.County") %>' runat="server" Width="100px">
											</asp:TextBox>
										</TD>
									</tr>
								</table>
							</TD>
						</TR>
						<TR>
							<TD>
								<asp:label id="lblState" CssClass="StandardLabel" runat="server">State&nbsp;:&nbsp;</asp:label>
							</TD>
							<TD vAlign="top">
								<table border="0" cellpadding="0" cellspacing="0">
									<tr>
										<td>
											<table border="0" cellpadding="0" cellspacing="0">
												<tr>
													<td>
														<asp:DropDownList id="ddlState" runat="server" Width="140px" DataSource="<%# tblState %>" DataTextField="subdivision_name_1" DataValueField="subdivision_code" SelectedIndex='<%# getSelectedIndex(tblState,Convert.ToString(DataBinder.Eval(Container, "DataItem.subdivision_code"))) %>'>
														</asp:DropDownList>
													</td>
													<td>
														<asp:RequiredFieldValidator id="ReqFldVal_State" CssClass="LabelError" runat="server" ErrorMessage="The State is required"
															ControlToValidate="ddlState">*</asp:RequiredFieldValidator>
													</td>
												</tr>
											</table>
										</td>
										<td>
											<asp:label id="lblZip" CssClass="StandardLabel" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Zip&nbsp;Code&nbsp;:&nbsp;</asp:label></td>
										<TD>
											<table border="0" cellpadding="0" cellspacing="0">
												<tr>
													<td>
														<asp:TextBox id="txtZip" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Zip") %>' Width="100px">
														</asp:TextBox>
													<td>
														<asp:RequiredFieldValidator id="ReqFldVal_Zip" CssClass="LabelError" runat="server" ErrorMessage="The Zip Code is required"
															ControlToValidate="txtZip">*</asp:RequiredFieldValidator>
													</td>
												</tr>
											</table>
										</TD>
									</tr>
								</table>
							</TD>
						</TR>
						<tr>
							<td colspan="3">
								<asp:ImageButton id="imgBtnDelete" runat="server" ImageUrl="~/images/BtnDelete.gif" CommandName="Delete"
									CausesValidation="False"></asp:ImageButton>
							</td>
						</tr>
					</TABLE>
				</ItemTemplate>
			</asp:datalist>
		</td>
	</tr>
	<tr>
		<td align="center"></td>
	</tr>
</table>
