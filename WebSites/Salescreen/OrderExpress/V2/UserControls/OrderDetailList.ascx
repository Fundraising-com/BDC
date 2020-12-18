<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderDetailList.ascx.cs"
    Inherits="QSP.OrderExpress.Web.V2.UserControls.OrderDetailList" %>
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
                    <asp:BoundField DataField="Number" HeaderText="Number" HeaderStyle-ForeColor="White"
                        ReadOnly="True">
                        <HeaderStyle ForeColor="White"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="ItemNumber" HeaderText="Code" HeaderStyle-ForeColor="White"
                        ReadOnly="True">
                        <HeaderStyle ForeColor="White"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="ItemName" HeaderText="Name" HeaderStyle-ForeColor="White"
                        ReadOnly="True">
                        <HeaderStyle ForeColor="White"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="UnitsPerCase" HeaderText="Units / case" HeaderStyle-ForeColor="White"
                        ReadOnly="True">
                        <HeaderStyle ForeColor="White"></HeaderStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Pro code cases">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("OrderedProCodeCases") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <%# GetProCodeTotal().ToString()%>
                        </FooterTemplate>
                        <HeaderStyle ForeColor="White" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ordered cases">
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("OrderedCases") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <%# GetCaseTotal().ToString()%>
                        </FooterTemplate>
                        <HeaderStyle ForeColor="White" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="CasePrice" HeaderText="Case price" HeaderStyle-ForeColor="White"
                        ReadOnly="True" DataFormatString="{0:C}">
                        <HeaderStyle ForeColor="White"></HeaderStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Total">
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("Total", "{0:C}") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <%# GetAmountTotal().ToString("C")%>
                        </FooterTemplate>
                        <HeaderStyle ForeColor="White" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>
<asp:Label ID="lblTotalRows" runat="server" Text="" CssClass="Text_Small"></asp:Label>