<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.BusinessExceptionForm" Codebehind="BusinessExceptionForm.ascx.cs" %>

<table id="tblFormDetailTitle" cellSpacing="0" cellPadding="0" width="550" border="0">
	<TR>
		<TD><asp:imagebutton id="imgBtnAddNew" CausesValidation="False" CommandName="Add" ImageUrl="~/images/BtnAdd.gif"
				runat="server"></asp:imagebutton></TD>
	</TR>
	<tr>
		<td><asp:datalist id=dtLstBizException runat="server" DataSource="<%# DVBizException %>" width="500px" DataKeyField="business_exception_id">
				<SeparatorTemplate>
					<hr width="100%" size="1px" color="#003366">
				</SeparatorTemplate>
				<ItemTemplate>
					<TABLE cellSpacing="0" cellPadding="3" width="500px" border="0">
						<TR>
							<TD>
								<asp:label id="lblName" CssClass="StandardLabel" runat="server">Name&nbsp;:&nbsp;</asp:label></TD>
							<TD>
								<table border="0" cellpadding="0" cellspacing="0">
									<tr>
										<td>
											<asp:TextBox id="txtName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.business_exception_name") %>' Width="350px">
											</asp:TextBox>
										</td>
										<td>
											<asp:RequiredFieldValidator id="ReqFldVal_Name" CssClass="LabelError" runat="server" ErrorMessage="The Name is required"
												ControlToValidate="txtName">*</asp:RequiredFieldValidator>
										</td>
									</tr>
								</table>
							</TD>
						</TR>
						<TR>
							<TD>
								<asp:label id="Label3" CssClass="StandardLabel" runat="server">Type&nbsp;:&nbsp;</asp:label>
							</TD>
							<TD vAlign="top">
								<asp:DropDownList id="ddlExceptionType" runat="server" DataSource="<%# tblExceptionType %>" DataTextField="exception_type_name" DataValueField="exception_type_id" SelectedIndex='<%# getSelectedIndex(tblExceptionType,Convert.ToString(DataBinder.Eval(Container, "DataItem.exception_type_id"))) %>'>
								</asp:DropDownList>
							</TD>
						</TR>
						<TR>
							<TD>
								<asp:label id="Label2" CssClass="StandardLabel" runat="server">Related to&nbsp;:&nbsp;</asp:label>
							</TD>
							<TD vAlign="top">
								<asp:DropDownList id="ddlEntityType" runat="server" DataSource="<%# tblEntityType %>" DataTextField="entity_type_name" DataValueField="entity_type_id" SelectedIndex='<%# getSelectedIndex(tblEntityType,Convert.ToString(DataBinder.Eval(Container, "DataItem.entity_type_id"))) %>'>
								</asp:DropDownList>
							</TD>
						</TR>
						<TR>
							<TD vAlign="top">
								<asp:label id="Label4" CssClass="StandardLabel" runat="server">Warning&nbsp;Message&nbsp;:&nbsp;</asp:label></TD>
							<TD>
								<asp:TextBox MaxLength=2048 id="txtWarningMessage" TextMode="MultiLine" runat="server" Rows="3" CssClass="DescLabel" Text='<%# DataBinder.Eval(Container, "DataItem.Warning_Message") %>' Width="350px">
								</asp:TextBox>
							</TD>
						</TR>
						<TR>
							<TD>
								<asp:label id="Label1" CssClass="StandardLabel" runat="server">Warning&nbsp;Display&nbsp;in&nbsp;:&nbsp;</asp:label>
							</TD>
							<TD vAlign="top">
								<asp:DropDownList id="ddlAppItem" runat="server" DataSource="<%# tblAppItem %>" DataTextField="Name" DataValueField="AppItem_ID" SelectedIndex='<%# getSelectedIndex(tblAppItem,Convert.ToString(DataBinder.Eval(Container, "DataItem.app_item_id"))) %>'>
								</asp:DropDownList>
							</TD>
						</TR>
						<TR>
							<TD vAlign="top">
								<asp:label id="lblMessage" CssClass="StandardLabel" runat="server">Message&nbsp;:&nbsp;</asp:label></TD>
							<TD>
								<asp:TextBox MaxLength=2048 id="txtMessage" TextMode="MultiLine" runat="server" Rows="3" CssClass="DescLabel" Text='<%# DataBinder.Eval(Container, "DataItem.Message") %>' Width="350px">
								</asp:TextBox>
							</TD>
						</TR>
						<TR>
							<TD vAlign="top">
								<asp:label id="lblExpression" CssClass="StandardLabel" runat="server">Expression&nbsp;:&nbsp;</asp:label></TD>
							<TD>
								<asp:TextBox id="txtExpression" TextMode="MultiLine" runat="server" Rows="3" CssClass="DescLabel" Text='<%# DataBinder.Eval(Container, "DataItem.exception_expression") %>' Width="350px">
								</asp:TextBox>
							</TD>
						</TR>
						<TR>
							<TD vAlign="top">
								<asp:label id="lblFeesValueExpression" CssClass="StandardLabel" runat="server">Fees&nbsp;Expression&nbsp;:&nbsp;</asp:label></TD>
							<TD>
								<asp:TextBox id="txtFeesValueExpression" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.fees_value_expression") %>' Width="350px">
								</asp:TextBox>
							</TD>
						</TR>
						<tr>
				            <td>
					            <asp:label id="Label5" CssClass="StandardLabel" runat="server">Section&nbsp;Type:&nbsp;</asp:label>
				            </td>
				            <TD vAlign="top">
								<table border="0" cellpadding="0" cellspacing="0">
									<tr>
				                        <td>
					                        <table border="0" cellpadding="0" cellspacing="0">
						                        <tr>
							                        <td>
								                        <asp:DropDownList id="ddlFormSectionType" runat="server" Width="200px" DataSource='<%# DVFormSectionType %>' DataTextField="form_section_type_name" DataValueField="form_section_type_id" SelectedIndex='<%# getSelectedIndex(DVFormSectionType, Convert.ToString(DataBinder.Eval(Container, "DataItem.form_section_type_id"))) %>'>
								                        </asp:DropDownList>
							                        </td>
							                        <td>
								                        <asp:CompareValidator id="compVal_FormSectionType" CssClass="LabelError" runat="server" ErrorMessage="The Section Type is required"
									                        ControlToValidate="ddlFormSectionType" Type="Integer" ValueToCompare="0" Operator="GreaterThan">*</asp:CompareValidator>
							                        </td>
							                        <td>
					                                    &nbsp;&nbsp;
				                                    </td>
				                                    <td>
					                                    <asp:label id="Label6" CssClass="StandardLabel" runat="server">Section&nbsp;Number:&nbsp;</asp:label>
				                                    </td>
				                                    <td>
					                                    &nbsp;
				                                    </td>
				                                    <td>
					                                    <asp:textbox id="txtSectionNumber" Columns="1" MaxLength=1 CssClass="StandardTextBox" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.form_section_number") %>' ></asp:textbox>
				                                    </td>    
						                        </tr>
					                        </table>
				                        </td>
				                    </TR>
				                  </table>
				               </TD>
			            </tr>
						<tr>
							<td colspan="3">
								<asp:ImageButton id="imgBtnDelete" runat="server" ImageUrl="~/images/BtnDelete.gif" CommandName="Delete"
									CausesValidation="False"></asp:ImageButton>
							</td>
						</tr>
					</TABLE>
				</ItemTemplate>
			</asp:datalist></td>
	</tr>
	<tr>
		<td align="center"></td>
	</tr>
</table>
