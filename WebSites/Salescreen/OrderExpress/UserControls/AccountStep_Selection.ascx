<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.AccountStep_Selection" Codebehind="AccountStep_Selection.ascx.cs" %>
<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl.DataGridControl" Assembly="QSP.WebControl" %>

<table id="Table1222" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td align="left"> <!--Section Body --><br>
			<table id="tblOrgInfoTitle" cellSpacing="0" cellPadding="0" border="0">
				<tr id="trOrgInfoTitle" runat="server">
					<td><asp:label id="Label1" runat="server" CssClass="FormTitleLabel">
							Add&nbsp;New&nbsp;Account&nbsp;to&nbsp;Organization:&nbsp;
						</asp:label></td>
					<td><asp:label id="lblOrgNumber" runat="server" CssClass="FormTitleDescLabel" ForeColor="#993300">
							00000
						</asp:label></td>
					<td>&nbsp;-&nbsp;
					</td>
					<td><asp:label id="lblOrgName" runat="server" CssClass="FormTitleDescLabel" ForeColor="#993300">
							Organization Name
						</asp:label></td>
				</tr>
			</table>
			<br>
		</td>
	</tr>
	<tr>
		<td align="left"> <!--Section Body -->
			
		</td>
	</tr>
	<tr>
		<td><cc2:sorteddatagrid id="dtgForm" runat="server" PageSize="30" AllowPaging="True" AllowSorting="True"
				DataKeyField="form_id" CellPadding="3" CssClass=GridStyle BorderColor="#CCCCCC"
				AutoGenerateColumns="False" ShowFooter="True" onselectedindexchanged="dtgForm_SelectedIndexChanged" OnItemDataBound="dtgForm_ItemDataBound">
				<PagerStyle CssClass="PagerItemStyle" Mode="NumericPages"></PagerStyle>
				<AlternatingItemStyle CssClass="AlternatingItemStyle_off"></AlternatingItemStyle>
				<FooterStyle CssClass="FooterItemStyle"></FooterStyle>
				<SelectedItemStyle CssClass="SelectedItemStyle"></SelectedItemStyle>
				<ItemStyle CssClass="ItemStyle_off"></ItemStyle>
				<HeaderStyle Wrap="False" CssClass="HeaderItemStyle" forecolor="White"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn>
						<ItemTemplate>
							<asp:imagebutton id="imgBtnDetail" runat="server" ImageUrl="~/images/BtnAddAccount.gif" CommandName="Select"
								CausesValidation="False"></asp:imagebutton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="form_id" SortExpression="form_id" ReadOnly="True" HeaderText="ID">
						<ItemStyle ></ItemStyle>
					</asp:BoundColumn>						
					<asp:TemplateColumn SortExpression="form_name" HeaderText="QSP&nbsp;Program">
						<ItemStyle Wrap="False" Width="300px"></ItemStyle>
						<ItemTemplate>
							<ASP:LINKBUTTON id="lnkBtnForm" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.form_name") %>' CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Form_ID") %>' CommandName="Select">
							</ASP:LINKBUTTON>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>
						<ItemTemplate>
							<asp:image id="imgLogo" height=80px runat="server" ImageUrl='<%# "~/"+ DataBinder.Eval(Container, "DataItem.image_url") %>'>
							</asp:image>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</cc2:sorteddatagrid><br>
			<br>
			<br>
			<br>
		</td>
	</tr>
	<TR>
		<TD align="center"><br>
			<TABLE cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<td><asp:imagebutton id="imgBtnBack" runat="server" CausesValidation="False" AlternateText="Click here to go back to the previous STEP"
							ImageUrl="~/images/btnBack.gif"></asp:imagebutton></td>
					<td width="100%">&nbsp;
					</td>
					<td></td>
				</TR>
			</TABLE>
		</TD>
	</TR>
</table>
