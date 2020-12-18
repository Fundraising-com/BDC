<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.OrderStep_Selection" Codebehind="OrderStep_Selection.ascx.cs" %>
<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl.DataGridControl" Assembly="QSP.WebControl" %>

<table id="Table1222" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr id="trCampInfoTitle" runat="server">
		<td align="left"> <!--Section Body --><br>
			<table id="tblCampInfoTitle" cellSpacing="0" cellPadding="0" border="0">
				<tr id="trAccountInfoTitle" runat="server">
					<td><asp:label id="Label1" runat="server" CssClass="FormTitleLabel">
							Account&nbsp;:&nbsp;
						</asp:label></td>
					<td><asp:label id="lblAccountNumber" runat="server" CssClass="FormTitleDescLabel" ForeColor="#993300">
							00000
						</asp:label></td>
					<td>&nbsp;-&nbsp;
					</td>
					<td><asp:label id="lblAccountName" runat="server" CssClass="FormTitleDescLabel" ForeColor="#993300">
							Account Name
						</asp:label></td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td align="left"> <!--Section Body -->
			<table id="Table5" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td><asp:label id="Label3" runat="server" CssClass="StandardLabel" Visible="False">
							3 - Select the Order Form you want to use :
						</asp:label>&nbsp;&nbsp;&nbsp;
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td><cc2:sorteddatagrid id="dtgForm" runat="server" PageSize="30" AllowPaging="True" AllowSorting="True"
				DataKeyField="formid" CellPadding="3" BorderColor="#CCCCCC"
				AutoGenerateColumns="False" ShowFooter="True" CssClass=GridStyle onselectedindexchanged="dtgForm_SelectedIndexChanged" OnItemDataBound="dtgForm_ItemDataBound">
				<PagerStyle CssClass="PagerItemStyle" Mode="NumericPages"></PagerStyle>
				<AlternatingItemStyle CssClass="AlternatingItemStyle_off"></AlternatingItemStyle>
				<FooterStyle CssClass="FooterItemStyle"></FooterStyle>
				<SelectedItemStyle CssClass="SelectedItemStyle"></SelectedItemStyle>
				<ItemStyle CssClass="ItemStyle_off"></ItemStyle>
				<HeaderStyle Wrap="False" CssClass="HeaderItemStyle" forecolor="White"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn>
						<ItemTemplate>
							<asp:imagebutton id="imgBtnDetail" runat="server" ImageUrl="~/images/BtnAddOrder.gif" CommandName="Select"
								CausesValidation="False"></asp:imagebutton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="formid" SortExpression="formid" ReadOnly="True" HeaderText="ID">
						<ItemStyle Wrap="False" Width="100px"></ItemStyle>
					</asp:BoundColumn>
					<asp:TemplateColumn SortExpression="FormName" HeaderText="Order Form">
						<ItemStyle Wrap="False" Width="350px"></ItemStyle>
						<ItemTemplate>
							<asp:linkbutton id="lnkBtnForm" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FormName") %>' CommandArgument='<%# DataBinder.Eval(Container, "DataItem.FormID") %>' CommandName="Select">
							</asp:linkbutton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>
						<ItemTemplate>
							<asp:image id="imgLogo" height=80px  runat="server" ImageUrl='<%# "~/" + DataBinder.Eval(Container, "DataItem.imageurl") %>'>
							</asp:image>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn Visible="False">
					    <ItemTemplate>
					        <asp:Label id="FormIDLabel" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.formid") %>'></asp:Label>
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
