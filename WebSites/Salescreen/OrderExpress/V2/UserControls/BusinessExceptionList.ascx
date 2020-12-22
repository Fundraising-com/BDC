<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BusinessExceptionList.ascx.cs"
    Inherits="QSP.OrderExpress.Web.V2.UserControls.BusinessExceptionList" %>
<table cellpadding="0" cellspacing="0" class="Box" width="100%">
    <tr>
        <td>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" EmptyDataText="No records were found"
                Width="100%" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" BackColor="White">
                <HeaderStyle Wrap="False" CssClass="GridHeaderItemStyle" Font-Size="12px" ForeColor="White">
                </HeaderStyle>
                <RowStyle CssClass="GridItemStyle"></RowStyle>
                <AlternatingRowStyle CssClass="GridItemStyle_Alternative"></AlternatingRowStyle>
                <FooterStyle CssClass="GridFooterItemStyle" Font-Size="10px"></FooterStyle>
                <PagerStyle CssClass="GridPagerItemStyle"></PagerStyle>
                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                    NextPageText="Next" PreviousPageText="Previous" />
                <Columns>
                    <asp:BoundField DataField="Message" HeaderText="Message" HeaderStyle-ForeColor="White" ReadOnly="True" />
                    <asp:BoundField DataField="FeesAmount" HeaderText="Fee" HeaderStyle-ForeColor="White" ReadOnly="True" Visible="false"/>
                    <asp:BoundField DataField="ApprovedForDisplay" HeaderText="Approved" HeaderStyle-ForeColor="White" ReadOnly="True" Visible="false"/>
                    <asp:BoundField DataField="ApprovedByForDisplay" HeaderText="Approved by" HeaderStyle-ForeColor="White" ReadOnly="True" Visible="false"/>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>
<asp:Label ID="lblTotalRows" runat="server" Text="" CssClass="Text_Small"></asp:Label>