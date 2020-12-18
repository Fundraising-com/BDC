<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.Promo_TextSelector" Codebehind="Promo_TextSelector.ascx.cs" %>
<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl.DataGridControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="uc1" TagName="SearchModuleSelector" Src="SearchModuleSelector.ascx" %>

<TABLE id="Table5" cellSpacing="0" cellPadding="0" align="left" border="0">
	<TBODY>
		<TR>
			<TD>
				<TABLE cellSpacing="0" cellPadding="0" border="0">
					<TR>
						<TD><uc1:SearchModuleSelector id="QSPFormSearchModule" runat="server"></uc1:SearchModuleSelector></TD>
					</TR>
				</TABLE>
				<BR>
				<asp:label id="lblCurrentIndex" runat="server" Font-Size="xx-small" CssClass="eRewardsInstr">Page 1 of 1</asp:label></TD>
		</TR>
		<TR>
			<TD><!--DataGrid  -->
				<cc2:sorteddatagrid id=dtgList runat="server" Font-Size="8pt" ShowFooter="True" DataSource="<%# DVPromo_Text %>" AutoGenerateColumns="False" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" BackColor="White" CellPadding="3" AllowSorting="True" AllowPaging="True" PageSize="10">
					<FooterStyle CssClass="FooterItemStyle"></FooterStyle>
					<SelectedItemStyle Font-Bold="True" ForeColor="Black" BackColor="#CCCCCC"></SelectedItemStyle>
					<AlternatingItemStyle CssClass="AlternatingItemStyle_off"></AlternatingItemStyle>
					<ItemStyle CssClass="ItemStyle_off"></ItemStyle>
					<HeaderStyle CssClass="HeaderItemStyle"></HeaderStyle>
					<Columns>
						<asp:TemplateColumn>
							<HeaderStyle Wrap="False" Width="1%"></HeaderStyle>
							<ItemTemplate>
								<asp:ImageButton id="imgBtnSelect" runat="server" ImageUrl="~/images/BtnSelect.gif"
									CausesValidation="False"></asp:ImageButton>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="promo_id" HeaderText="Promo ID" visible="false">
							<ItemTemplate>
								<asp:Label id="lblId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.promo_text_id") %>' />
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="Promo_Text_code" HeaderText="Code">
							<ItemTemplate>
								<asp:Label id="Code" runat="server" Text='<%# ((String)DataBinder.Eval(Container, "DataItem.Promo_Text_code")).Replace(" ","&nbsp;") %>'>
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="Promo_Text_name" HeaderText="Short">
							<ItemTemplate>
								<asp:Label id="Name" runat="server" Text='<%# ((String)DataBinder.Eval(Container, "DataItem.Promo_Text_name")).Replace(" ","&nbsp;") %>'>
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="description" HeaderText="Long">
							<HeaderStyle Width="400px"></HeaderStyle>
							<ItemTemplate>
								<asp:Label id="Description" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.description") %>' />
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="promo_text" HeaderText="Promo Text" visible="false">
							<ItemTemplate>
								<asp:Label id="promo_text" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.promo_text") %>' />
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="field_sales_manager_id" HeaderText="FM&#160;ID">
							<ItemTemplate>
								<center>
									<asp:Label id="lblFM_ID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.field_sales_manager_id") %>' />
								</center>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="National">
							<HeaderStyle Width="10px"></HeaderStyle>
							<ItemTemplate>
								<CENTER>
									<asp:CheckBox id=chkNational Runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container, "DataItem.IsNational")) %>'>
									</asp:CheckBox></CENTER>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn Visible="False" HeaderText="Deleted">
							<HeaderStyle Width="10px"></HeaderStyle>
							<ItemTemplate>
								<asp:CheckBox Runat="server" ID="chkArchived" Checked='<%# DataBinder.Eval(Container, "DataItem.deleted")%>'>
								</asp:CheckBox>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
					<PagerStyle CssClass="PagerItemStyle" Mode="NumericPages"></PagerStyle>
				</cc2:sorteddatagrid>
			</TD>
		</TR>
		<TR>
			<TD>
				<TABLE cellSpacing="0" cellPadding="0" border="0">
					<TR>
						<td><br>
							<asp:label id="lblTotal" runat="server" CssClass="TotalListLabel">
							Number of Text(s):
						</asp:label></td>
					</TR>
				</TABLE>
			</TD>
		</TR>
		<TR>
			<TD align="center">
				<br>
				<TABLE cellSpacing="0" cellPadding="0" border="0">
					<TR>
						<td>
							<asp:ImageButton id="imgBtnOK" Visible="False" runat="server" CausesValidation="False" ImageUrl="~/images/btnOK.gif"
								AlternateText="Click here to confirm your selection"></asp:ImageButton>
						</td>
						<td>
							<asp:HyperLink id="hypLnkCancel" Visible="False" runat="server" ImageUrl="~/images/btnCancel.gif"
								NavigateUrl="javascript:window.close();">Cancel</asp:HyperLink>
						</td>
					</TR>
				</TABLE>
			</TD>
		</TR>
	</TBODY>
</TABLE>

<script>
function CloseSelector(id,desc,text)
{
	
	var lblId = opener.document.getElementById('<%=ParentID%>');
	var lblDesc = opener.document.getElementById('<%=ParentDesc%>');
	var txt = opener.document.getElementById('<%=ParentText%>');
	var temptxt = opener.document.getElementById('<%=ParentTempText%>');
	
	lblId.value = id;
	lblDesc.value = desc;
		
	txt.innerText = text;
	temptxt.value = text;
	
	window.close();
}
</script>
