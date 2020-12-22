<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChargeList.ascx.cs"
    Inherits="QSP.OrderExpress.Web.V2.UserControls.ChargeList" %>
<table cellpadding="0" cellspacing="0" class="Box" width="100%">
    <tr>
        <td>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" EmptyDataText="No records were found"
                Width="100%" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" BackColor="White" ShowFooter="true">
                <HeaderStyle Wrap="False" CssClass="GridHeaderItemStyle" Font-Size="12px" ForeColor="White">
                </HeaderStyle>
                <RowStyle CssClass="GridItemStyle"></RowStyle>
                <AlternatingRowStyle CssClass="GridItemStyle_Alternative"></AlternatingRowStyle>
                <FooterStyle CssClass="GridFooterItemStyle" Font-Size="10px"></FooterStyle>
                <PagerStyle CssClass="GridPagerItemStyle"></PagerStyle>
                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                    NextPageText="Next" PreviousPageText="Previous" />
                <Columns>
                    <asp:BoundField DataField="ChargeName" HeaderText="Charge" HeaderStyle-ForeColor="White"
                        ReadOnly="True">
                        <HeaderStyle ForeColor="White"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="ChargeToName" HeaderText="Charge to" HeaderStyle-ForeColor="White"
                        ReadOnly="True">
                        <HeaderStyle ForeColor="White"></HeaderStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Amount">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Amount", "{0:C}") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <%# GetAmountTotal().ToString("C")%>
                        </FooterTemplate>
                        <HeaderStyle ForeColor="White" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="Comment" HeaderText="Comment" HeaderStyle-ForeColor="White"
                        ReadOnly="True">
                        <HeaderStyle ForeColor="White"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="CreateDate" HeaderText="Created at" HeaderStyle-ForeColor="White"
                        ReadOnly="True">
                        <HeaderStyle ForeColor="White"></HeaderStyle>
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>
<asp:Label ID="lblTotalRows" runat="server" Text="" CssClass="Text_Small"></asp:Label>