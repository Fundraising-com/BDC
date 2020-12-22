<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="QSP.OrderExpress.Web.UserControls.OrderChargeList" Codebehind="OrderChargeList.ascx.cs" %>

<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl.DataGridControl" Assembly="QSP.WebControl" %>

<cc2:SortedDataGrid 
    ID="dtgOrderChargeList" 
    runat="server" 
    width="100%"
    ShowFooter="True" AllowSorting="True"
    GridLines="Vertical"
    AutoGenerateColumns="False" 
    BorderColor="#999999" 
    CellPadding="3"
    CssClass="GridStyle" 
    PageSize="30" 
    SearchMode="0"
    Criteria="" 
    FilterExpression="" 
    SortExpression="" onitemcreated="dtgOrderChargeList_ItemCreated">
    
    <PagerStyle CssClass="PagerItemStyle" Mode="NumericPages"></PagerStyle>
    <AlternatingItemStyle CssClass="AlternatingItemStyle"></AlternatingItemStyle>
    <FooterStyle CssClass="FooterItemStyle"></FooterStyle>
    <SelectedItemStyle CssClass="SelectedItemStyle"></SelectedItemStyle>
    <ItemStyle CssClass="ItemStyle"></ItemStyle>
    <HeaderStyle Wrap="False" CssClass="HeaderItemStyle" ForeColor="White"></HeaderStyle>
    
    <Columns>
        <asp:BoundColumn DataField="ChargeId" HeaderText="Charge&nbsp;ID" ItemStyle-Width="50px" Visible="false"></asp:BoundColumn>
        <asp:TemplateColumn HeaderText="Charge&nbsp;Name">
            <ItemTemplate>
                <asp:Label runat="server" Text='<%# Eval("ChargeName") %>'></asp:Label>
            </ItemTemplate>
			<FooterTemplate>
				<asp:Label id="Label2" Text="Total:" CssClass="StandardLabel" runat="server"></asp:Label>
			</FooterTemplate>
        </asp:TemplateColumn>
        <asp:BoundColumn DataField="ChargeToName" HeaderText="Charge&nbsp;To" ItemStyle-Width="50px"></asp:BoundColumn>
        <asp:TemplateColumn HeaderText="Amount" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="100px" FooterStyle-HorizontalAlign="Right">
            <ItemTemplate>
                <asp:Label runat="server" Text='<%# Eval("Amount", "{0:C}") %>'></asp:Label>
            </ItemTemplate>
			<FooterTemplate>
				<asp:Label id="lblTotalAmount" Text="" CssClass="StandardLabel" runat="server"></asp:Label>
			</FooterTemplate>
        </asp:TemplateColumn>
        <asp:BoundColumn DataField="Comment" HeaderText="Comment" ItemStyle-Width="100px" Visible="false"></asp:BoundColumn>
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
</cc2:SortedDataGrid>
