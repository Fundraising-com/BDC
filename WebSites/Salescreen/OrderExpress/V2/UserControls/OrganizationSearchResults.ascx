<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrganizationSearchResults.ascx.cs"
    Inherits="QSP.OrderExpress.Web.V2.UserControls.OrganizationSearchResults" %>
<table cellpadding="0" cellspacing="0" class="Box" width="100%">
    <tr>
        <td>
            <asp:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" OnSorting="GridView1_Sorting"
                AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" EmptyDataText="No records were found"
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
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("OrganizationId", "~/V2/Forms/OrganizationView.aspx?OrganizationId={0}") %>'
                                Target="_blank">
                                <asp:Image ID="imageDetail" runat="server" ImageUrl="~/Images/BtnDetail.gif" />
                            </asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="OrganizationId" HeaderText="QSP Org Id" ReadOnly="True"
                        SortExpression="OrganizationId" />
                    <asp:BoundField DataField="OrganizationName" HeaderText="Organization name" ReadOnly="True"
                        SortExpression="OrganizationName" />
                    <asp:BoundField DataField="OrganizationTypeName" HeaderText="Type" ReadOnly="True"
                        SortExpression="OrganizationTypeName" />
                    <asp:BoundField DataField="Address1" HeaderText="Address" ReadOnly="True" SortExpression="Address1" />
                    <asp:BoundField DataField="City" HeaderText="City" ReadOnly="True" SortExpression="City" />
                    <asp:BoundField DataField="DisplaySubdivisionCode" HeaderText="State" ReadOnly="True"
                        SortExpression="SubdivisionCode" />
                    <asp:BoundField DataField="Zip" HeaderText="Zip code" ReadOnly="True" SortExpression="Zip" />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>
<asp:Label ID="lblTotalRows" runat="server" Text="" CssClass="Text_Small"></asp:Label>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" EnableViewState="False"
    TypeName="QSP.OrderExpress.Web.V2.UserControls.OrganizationGridHelper" SelectCountMethod="GetCount"
    SelectMethod="GetData" SortParameterName="sort" OnSelecting="ObjectDataSource1_Selecting"
    OnSelected="ObjectDataSource1_Selected">
    <SelectParameters>
        <asp:Parameter Name="parameter" Type="Object" />
    </SelectParameters>
</asp:ObjectDataSource>
