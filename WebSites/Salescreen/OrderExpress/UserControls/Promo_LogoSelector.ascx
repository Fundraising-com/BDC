<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.Promo_LogoSelector" Codebehind="Promo_LogoSelector.ascx.cs" %>
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
				<cc2:sorteddatagrid id=dtgList runat="server" Font-Size="8pt" ShowFooter="True" DataSource="<%# DVPromo_logo %>" AutoGenerateColumns="False" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" BackColor="White" CellPadding="3" AllowSorting="True" AllowPaging="True" PageSize="10">
					<FooterStyle CssClass="FooterItemStyle"></FooterStyle>
					<SelectedItemStyle Font-Bold="True" ForeColor="Black" BackColor="#CCCCCC"></SelectedItemStyle>
					<AlternatingItemStyle CssClass="AlternatingItemStyle_off"></AlternatingItemStyle>
					<ItemStyle CssClass="ItemStyle_off"></ItemStyle>
					<HeaderStyle CssClass="HeaderItemStyle"></HeaderStyle>
					<Columns>
						<asp:TemplateColumn>
							<HeaderStyle Wrap="False" Width="1%"></HeaderStyle>
							<ItemTemplate>
								<asp:ImageButton id="imgBtnSelect" runat="server" ImageUrl="~/images/BtnSelect.gif" CommandName="Select" CausesValidation="False"></asp:ImageButton>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn>
							<ItemStyle Wrap="False" Width="50px"></ItemStyle>
							<ItemTemplate>
								<ASP:IMAGEBUTTON id="imgBtnDetail" runat="server" ImageUrl='' CommandName="Select" CausesValidation="False" Width="50"></ASP:IMAGEBUTTON>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="Promo_logo_id" HeaderText="ID">
							<ItemTemplate>
								<asp:Label id="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Promo_logo_id") %>' />
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="Promo_logo_name" HeaderText="Name">
							<ItemTemplate>
								<asp:Label id="Name" runat="server" Text='<%# ((String)DataBinder.Eval(Container, "DataItem.Promo_logo_name")).Replace(" ","&nbsp;") %>' />
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="Description" HeaderText="Description">
							<ItemTemplate>
								<asp:Label id="Description" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.description") %>' />
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="FM_ID" HeaderText="FM&nbsp;ID">
							<ItemTemplate>
								<center>
									<asp:Label id="lblFM_ID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FM_ID") %>' />
								</center>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="National" visible="true">
							<ItemTemplate>
								<center>
									<asp:CheckBox id="chkNational" Runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container, "DataItem.IsNational")) %>'>
									</asp:CheckBox>
								</center>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Deleted" visible="false">
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
							Number of logo(s):
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
function CloseSelector(id,desc,url)
{
	var lblId = opener.document.getElementById('<%=ParentID%>');
	var lblDesc = opener.document.getElementById('<%=ParentDesc%>');
	var img = opener.document.getElementById('<%=ParentImage%>');
	var tempimg = opener.document.getElementById('<%=ParentTempImage%>');
	
	lblId.value = id;
	lblDesc.value = desc;
	img.src = url;
	tempimg.value = url;
	
	window.close();
}
</script>
