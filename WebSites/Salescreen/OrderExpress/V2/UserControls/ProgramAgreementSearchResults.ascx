﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProgramAgreementSearchResults.ascx.cs"
    Inherits="QSP.OrderExpress.Web.V2.UserControls.ProgramAgreementSearchResults" %>
<table cellpadding="0" cellspacing="0" class="Box" width="100%">
    <tr>
        <td>
            <asp:GridView ID="GridView1" runat="server" OnSorting="GridView1_Sorting"
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
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("ProgramAgreementId", "~/V2/Forms/ProgramAgreementView.aspx?ProgramAgreementId={0}") %>'
                                Target="_blank">
                                <asp:Image ID="imageDetail" runat="server" ImageUrl="~/Images/BtnDetail.gif" />
                            </asp:HyperLink>
                        </ItemTemplate>
                        <HeaderStyle Width="20px" HorizontalAlign="Center"></HeaderStyle>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="lblStatusRead" runat="server" BackColor='<%# System.Drawing.Color.FromName(Eval("DisplayColorCode").ToString()) %>'
                                BorderWidth="1px" BorderStyle="Solid" BorderColor="Black">
								&nbsp;&nbsp;
                            </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="10px" HorizontalAlign="Center"></HeaderStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="StatusShortDescription" HeaderText="Status" ReadOnly="True"
                        SortExpression="StatusShortDescription" />
                    <asp:BoundField DataField="ProgramAgreementId" HeaderText="QSP PA id" ReadOnly="True"
                        SortExpression="ProgramAgreementId" />
                    <asp:BoundField DataField="EDSProgramAgreementId" HeaderText="EDS PA id" ReadOnly="True"
                        SortExpression="EDSProgramAgreementId" />
                    <asp:BoundField DataField="AccountId" HeaderText="QSP acct id" ReadOnly="True" SortExpression="AccountId" />
                    <asp:BoundField DataField="EDSAccountId" HeaderText="EDS acct id" ReadOnly="True"
                        SortExpression="EDSAccountId" />
                    <asp:BoundField DataField="AccountName" HeaderText="Account name" ReadOnly="True"
                        SortExpression="AccountName" />
                    <asp:BoundField DataField="FmId" HeaderText="FSM id" ReadOnly="True" SortExpression="FsmId" />
                    <asp:BoundField DataField="DisplayFsmName" HeaderText="FSM name" ReadOnly="True"
                        SortExpression="FsmLastName" />
                    <asp:BoundField DataField="ProgramTypeName" HeaderText="Program type" ReadOnly="True"
                        SortExpression="ProgramTypeName" Visible="false" />
                    <asp:BoundField DataField="ProgramName" HeaderText="Program" ReadOnly="True" SortExpression="ProgramName"
                        Visible="false" />
                    <asp:BoundField DataField="FormName" HeaderText="Form" ReadOnly="True" SortExpression="FormName" />
                    <asp:BoundField DataField="City" HeaderText="City" ReadOnly="True" SortExpression="AddressCity" />
                    <asp:BoundField DataField="DisplaySubdivisionCode" HeaderText="ST" ReadOnly="True"
                        SortExpression="AddressSubdivisionCode" />
                    <asp:BoundField DataField="Zip" HeaderText="Zip" ReadOnly="True" SortExpression="AddressZip" />
                    <asp:BoundField DataField="DisplayCreatorName" HeaderText="Created by" ReadOnly="True"
                        SortExpression="CreateUserLastName" />
                    <asp:BoundField DataField="DisplayCreateDate" HeaderText="Created at" ReadOnly="True"
                        SortExpression="CreateDate" />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>
<asp:Label ID="lblTotalRows" runat="server" Text="" CssClass="Text_Small"></asp:Label>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" EnableViewState="False"
    TypeName="QSP.OrderExpress.Web.V2.UserControls.ProgramAgreementGridHelper" SelectCountMethod="GetCount"
    SelectMethod="GetData" SortParameterName="sort" OnSelecting="ObjectDataSource1_Selecting"
    OnSelected="ObjectDataSource1_Selected">
    <SelectParameters>
        <asp:Parameter Name="parameter" Type="Object" />
    </SelectParameters>
</asp:ObjectDataSource>
