<%@ Control Language="c#" AutoEventWireup="false" Codebehind="AddressHygiene.ascx.cs" Inherits="GA.BDC.WEB.ScratchcardWeb.Components.User.AddressHygiene.AddressHygiene" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<asp:datagrid id="SuggestionListDataGrid" AllowSorting="True" runat="server" AllowPaging="True"
 borderwidth="1px" AutoGenerateColumns="False" Font-Size="X-Small" Width="620px">
 <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
 <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#006699"></HeaderStyle>
 <FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
 <Columns>
  <asp:BoundColumn DataField="Address1" SortExpression="Address1" HeaderText="Address1"></asp:BoundColumn>
  <asp:TemplateColumn Visible="False" SortExpression="Address1" HeaderText="Address1">
   <ItemTemplate>
    <asp:TextBox id="Address1TextBox" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Address1") %>'></asp:TextBox>
   </ItemTemplate>
  </asp:TemplateColumn>
  <asp:BoundColumn DataField="Address2" SortExpression="Address2" HeaderText="Address2"></asp:BoundColumn>
  <asp:TemplateColumn Visible="False" SortExpression="Address2" HeaderText="Address2">
   <ItemTemplate>
    <asp:TextBox id="Address2TextBox" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Address2") %>'></asp:TextBox>
   </ItemTemplate>
  </asp:TemplateColumn>
  <asp:BoundColumn DataField="City" SortExpression="City" HeaderText="City"></asp:BoundColumn>
  <asp:BoundColumn DataField="County" SortExpression="County" HeaderText="County"></asp:BoundColumn>
  <asp:BoundColumn DataField="Region" SortExpression="Region" HeaderText="Region"></asp:BoundColumn>
  <asp:BoundColumn DataField="PostCode" SortExpression="PostCode" HeaderText="PostCode"></asp:BoundColumn>
  <asp:BoundColumn DataField="PostCode2" SortExpression="PostCode2" HeaderText="PostCode2"></asp:BoundColumn>
  <asp:BoundColumn DataField="Country" SortExpression="Country" HeaderText="Country"></asp:BoundColumn>
  <asp:ButtonColumn Text="Select" CommandName="Select"></asp:ButtonColumn>
 </Columns>
 <PagerStyle Mode="NumericPages"></PagerStyle>
</asp:datagrid>
