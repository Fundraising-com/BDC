<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MethodNotificationList.ascx.cs"
    Inherits="QSP.OrderExpress.Web.V2.UserControls.MethodNotificationList" %>
<table cellpadding="0" cellspacing="0" class="Box" width="100%">
    <tr>
        <td>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" EmptyDataText="No records were found"
                Width="100%" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" BackColor="White"
                OnRowDataBound="GridView1_RowDataBound">
                <HeaderStyle Wrap="False" CssClass="GridHeaderItemStyle" Font-Size="12px" ForeColor="White">
                </HeaderStyle>
                <RowStyle CssClass="GridItemStyle"></RowStyle>
                <AlternatingRowStyle CssClass="GridItemStyle_Alternative"></AlternatingRowStyle>
                <FooterStyle CssClass="GridFooterItemStyle" Font-Size="10px"></FooterStyle>
                <PagerStyle CssClass="GridPagerItemStyle"></PagerStyle>
                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                    NextPageText="Next" PreviousPageText="Previous" />
                <Columns>
                    <asp:TemplateField HeaderText="Type">
                        <ItemTemplate>
                            <asp:Label ID="columnType" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle ForeColor="White" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="Message" HeaderText="Message" HeaderStyle-ForeColor="White"
                        ReadOnly="True">
                        <HeaderStyle ForeColor="White"></HeaderStyle>
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>
<asp:Label ID="lblTotalRows" runat="server" Text="" CssClass="Text_Small"></asp:Label>
