<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="QSP.OrderExpress.Web.UserControls.OrderChargeList2" Codebehind="OrderChargeList2.ascx.cs" %>

<asp:DataGrid ID="dtgOrderChargeList" runat="server" ShowFooter="True" AllowSorting="True"
    AutoGenerateColumns="False" GridLines="Vertical" CellPadding="3" 
    CssClass="GridStyle" BorderColor="#999999" width="100%" onitemcreated="dtgOrderChargeList_ItemCreated">
    <PagerStyle CssClass="PagerItemStyle" Mode="NumericPages"></PagerStyle>
    <AlternatingItemStyle CssClass="AlternatingItemStyle"></AlternatingItemStyle>
    <FooterStyle CssClass="FooterItemStyle"></FooterStyle>
    <SelectedItemStyle CssClass="SelectedItemStyle"></SelectedItemStyle>
    <ItemStyle CssClass="ItemStyle"></ItemStyle>
    <HeaderStyle Wrap="False" CssClass="HeaderItemStyle" ForeColor="White"></HeaderStyle>
    <Columns>
        <asp:BoundColumn DataField="ChargeId" HeaderText="Charge&nbsp;ID" ItemStyle-Width="50px"
            Visible="false"></asp:BoundColumn>
        <asp:BoundColumn DataField="ChargeName" HeaderText="Charge&nbsp;Name" ItemStyle-Wrap="false">
        </asp:BoundColumn>
        <asp:BoundColumn DataField="ChargeToName" HeaderText="Charge&nbsp;To" ItemStyle-Width="50px">
        </asp:BoundColumn>
        <asp:BoundColumn DataField="Amount" HeaderText="Amount" DataFormatString="{0:C}"
            ItemStyle-Width="50px"></asp:BoundColumn>
        <asp:BoundColumn DataField="Comment" HeaderText="Comment" ItemStyle-Width="100px"
            Visible="false"></asp:BoundColumn>
        <asp:TemplateColumn HeaderText="Created&nbsp;At" ItemStyle-Width="100px">
            <ItemTemplate>
                <asp:Label ID="Label1" runat="server" Width="140px" Text='<%# DataBinder.Eval(Container, "DataItem.CreateDate", "{0:MM/dd/yyyy HH:mm:ss}") %>'>
                </asp:Label>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="Created&#160;By" ItemStyle-Width="50px">
            <ItemTemplate>
                <asp:Label ID="Label11" runat="server" Width="110px" Text='<%# DataBinder.Eval(Container, "DataItem.CreateUserLastName") + " " + DataBinder.Eval(Container, "DataItem.CreateUserFirstName") %>'>
                </asp:Label>
            </ItemTemplate>
        </asp:TemplateColumn>
    </Columns>
</asp:DataGrid>
