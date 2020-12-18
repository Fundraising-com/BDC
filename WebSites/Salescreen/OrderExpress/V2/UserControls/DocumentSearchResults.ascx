<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DocumentSearchResults.ascx.cs"
    Inherits="QSP.OrderExpress.Web.V2.UserControls.DocumentSearchResults" %>
<table cellpadding="0" cellspacing="0" class="Box" width="100%">
    <tr>
        <td>
            <asp:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" OnSorting="GridView1_Sorting"
                AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" EmptyDataText="No records were found"
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
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("QSPDocumentId", "~/V2/Forms/DocumentView.aspx?DocumentId={0}") %>'
                                Target="_blank">
                                <asp:Image ID="imageDetail" runat="server" ImageUrl="~/Images/BtnDetail.gif" />
                            </asp:HyperLink>
                        </ItemTemplate>
                        <HeaderStyle Width="20px" HorizontalAlign="Center"></HeaderStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="QSPDocumentId" HeaderText="Document id" ReadOnly="True" SortExpression="QSPDocumentId" />
                    <asp:BoundField DataField="TypeName" HeaderText="Type" ReadOnly="True" SortExpression="DocumentTypeName" />
                    <asp:BoundField DataField="QSPAccountId" HeaderText="QSP acct id" ReadOnly="True" SortExpression="QSPAccountId" />
                    <asp:BoundField DataField="EDSAccountId" HeaderText="EDS acct id" ReadOnly="True" SortExpression="EDSAccountId" />
                    <asp:BoundField DataField="AccountName" HeaderText="Account name" ReadOnly="True" SortExpression="AccountName" />
                    <asp:BoundField DataField="ExemptionNumber" HeaderText="Exemption #" ReadOnly="True" SortExpression="ExemptionNumber" />
                    <asp:BoundField DataField="DisplayExemptionStartDate" HeaderText="Start" ReadOnly="True" SortExpression="ExemptionStartDate" />
                    <asp:BoundField DataField="DisplayExemptionEndDate" HeaderText="End" ReadOnly="True" SortExpression="ExemptionEndDate" />
                    <asp:CheckBoxField DataField="IsApproved" HeaderText="Approved" ReadOnly="True" SortExpression="IsApproved" />
                    <asp:BoundField DataField="DisplayApprovedByName" HeaderText="Approved by" ReadOnly="True" SortExpression="ApprovedByLastName" />
                    <asp:BoundField DataField="DisplayApprovedDate" HeaderText="Approved at" ReadOnly="True" SortExpression="ApprovedDate" />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>
<asp:Label ID="lblTotalRows" runat="server" Text="" CssClass="Text_Small"></asp:Label>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" EnableViewState="False"
    TypeName="QSP.OrderExpress.Web.V2.UserControls.DocumentGridHelper" SelectCountMethod="GetCount"
    SelectMethod="GetData" SortParameterName="sort" OnSelecting="ObjectDataSource1_Selecting"
    OnSelected="ObjectDataSource1_Selected">
    <SelectParameters>
        <asp:Parameter Name="parameter" Type="Object" />
    </SelectParameters>
</asp:ObjectDataSource>
