<%@ Register TagPrefix="uc1" TagName="SearchModule" Src="~/UserControls/SearchModule.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl.DataGridControl" Assembly="QSP.WebControl" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.OrderStatusChangeList" Codebehind="OrderStatusChangeList.ascx.cs" %>
<br>
<br>
<table id="Table5" cellspacing="0" cellpadding="0" border="0">
    <tr>
        <td class="SectionPageTitleInfo">
            <asp:Label ID="Label4" runat="server">
				Order History
            </asp:Label></td>
    </tr>
    <tr>
        <td>
            <!--DataGrid  -->
            <br>
            <cc2:SortedDataGrid ID="dtgOrder" runat="server" Font-Size="8pt" Width="600px" ShowFooter="True"
                DataSource="<%# DVOrderStatusChanges %>" AutoGenerateColumns="False" BorderColor="#CCCCCC"
                BorderStyle="None" BorderWidth="1px" BackColor="White" CellPadding="3" AllowSorting="True"
                PageSize="30" SearchMode="0">
                <PagerStyle CssClass="PagerItemStyle" Mode="NumericPages"></PagerStyle>
                <AlternatingItemStyle CssClass="AlternatingItemStyle_off" Wrap="False"></AlternatingItemStyle>
                <FooterStyle CssClass="FooterItemStyle" Font-Size="10px" Wrap="False"></FooterStyle>
                <SelectedItemStyle CssClass="SelectedItemStyle" Wrap="False"></SelectedItemStyle>
                <ItemStyle CssClass="ItemStyle_off" Font-Size="11px" Wrap="False"></ItemStyle>
                <HeaderStyle Wrap="False" CssClass="HeaderItemStyle" ForeColor="White"></HeaderStyle>
                <Columns>
                    <asp:TemplateColumn>
                        <headerstyle horizontalalign="Center" width="10px"></headerstyle>
                        <itemtemplate>
							<asp:Label id="lblStatusRead" runat="server" Width="5px" BackColor='<%# System.Drawing.Color.FromName(DataBinder.Eval(Container, "DataItem.Color_Code").ToString()) %>' Height="5px" BorderWidth="1px" BorderStyle="Solid" BorderColor="Black">
								&nbsp;&nbsp;
							</asp:Label>
						</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="order_status_short_description" SortExpression="order_status_short_description"
                        ReadOnly="True" HeaderText="Status&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;">
                        <itemstyle width="100px" wrap="false"></itemstyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="order_id" SortExpression="order_id" ReadOnly="True" HeaderText="QSP&nbsp;Order&nbsp;ID">
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="fulf_order_id" SortExpression="fulf_order_id" ReadOnly="True"
                        HeaderText="EDS&nbsp;Order&nbsp;#"></asp:BoundColumn>
                    <asp:TemplateColumn SortExpression="create_date" HeaderText="Created&nbsp;At">
                        <itemtemplate>
							<asp:Label id="Label1" runat="server" Width="140px" Text='<%# DataBinder.Eval(Container, "DataItem.create_date", "{0:MM/dd/yyyy HH:mm:ss}") %>'>
							</asp:Label>
						</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn SortExpression="create_last_name, create_first_name" HeaderText="Created&#160;By">
                        <itemtemplate>
							<asp:Label id="Label11" runat="server" Width="110px" Text='<%# DataBinder.Eval(Container, "DataItem.create_last_name") + " " + DataBinder.Eval(Container, "DataItem.create_first_name") %>'>
							</asp:Label>
						</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn SortExpression="update_date" HeaderText="Updated&nbsp;At">
                        <itemtemplate>
							<asp:Label id="Label12" runat="server" width="140px" Text='<%# DataBinder.Eval(Container, "DataItem.update_date", "{0:MM/dd/yyyy HH:mm:ss}") %>'>
							</asp:Label>
						</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn SortExpression="update_last_name, update_first_name" HeaderText="Updated&#160;By">
                        <itemtemplate>
							<asp:Label id="Label13" runat="server" Width="100px" Text='<%# DataBinder.Eval(Container, "DataItem.update_last_name") + " " + DataBinder.Eval(Container, "DataItem.update_first_name") %>'>
							</asp:Label>
						</itemtemplate>
                    </asp:TemplateColumn>
                </Columns>
            </cc2:SortedDataGrid>
        </td>
    </tr>
    <tr>
        <td>
            <table cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td colspan="2">
                        <br>
                        <asp:Label ID="lblTotal" runat="server" CssClass="TotalListLabel">
							Number of Order Change(s):
                        </asp:Label></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center">
            <br>
            <br>
            <asp:HyperLink ID="hypLnkClose" runat="server" ImageUrl="~/images/btnClose.gif" NavigateUrl="javascript:window.close();">Cancel</asp:HyperLink>
        </td>
    </tr>
</table>
