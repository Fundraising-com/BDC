<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StatusHistoryList.ascx.cs"
    Inherits="QSP.OrderExpress.Web.V2.UserControls.StatusHistoryList" %>
<table cellpadding="0" cellspacing="0" class="Box" width="100%">
    <tr>
        <td>
            <asp:GridView ID="GridView1" runat="server" 
                AutoGenerateColumns="False" EmptyDataText="No records were found"
                Width="100%" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" BackColor="White">
                <HeaderStyle Wrap="False" CssClass="GridHeaderItemStyle" Font-Size="12px" ForeColor="White"></HeaderStyle>
                <RowStyle CssClass="GridItemStyle"></RowStyle>
                <AlternatingRowStyle CssClass="GridItemStyle_Alternative"></AlternatingRowStyle>
                <FooterStyle CssClass="GridFooterItemStyle" Font-Size="10px"></FooterStyle>
                <PagerStyle CssClass="GridPagerItemStyle"></PagerStyle>
                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" NextPageText="Next" PreviousPageText="Previous" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="lblStatusRead" runat="server" BackColor='<%# System.Drawing.Color.FromName(Eval("DisplayColorCode").ToString()) %>'
                                BorderWidth="1px" BorderStyle="Solid" BorderColor="Black">
								&nbsp;&nbsp;
                            </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="10px" HorizontalAlign="Center"></HeaderStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="StatusShortDescription" HeaderText="Status" HeaderStyle-ForeColor="White" ReadOnly="True" />
                    <asp:BoundField DataField="Reason" HeaderText="Reason" HeaderStyle-ForeColor="White" ReadOnly="True" />
                    <asp:BoundField DataField="DisplayCreateDate" HeaderText="Updated on" HeaderStyle-ForeColor="White" ReadOnly="True" />
                    <asp:BoundField DataField="DisplayCreatorName" HeaderText="Updated by" HeaderStyle-ForeColor="White" ReadOnly="True" />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>
<asp:Label ID="lblTotalRows" runat="server" Text="" CssClass="Text_Small"></asp:Label>