<%@ Page Language="C#" MasterPageFile="~/Templates/Default.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="QSP.Site.MyFeedback.Admin.Default" Title="Admin Feedback" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <asp:LinkButton ID="RefreshLinkButton" runat="server" OnClick="RefreshLinkButton_Click">Refresh</asp:LinkButton>
    <asp:GridView ID="FeedbackListGridView" runat="server" AllowPaging="True" AllowSorting="True"
        AutoGenerateColumns="False" AutoGenerateEditButton="True"
        BackColor="White" DataSourceID="ObjectDataSource1" Width="100%">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" SortExpression="Id" ConvertEmptyStringToNull="False" />
            <asp:BoundField DataField="CreateDate" HeaderText="CreateDate" SortExpression="CreateDate" ConvertEmptyStringToNull="False" />
            <asp:CheckBoxField DataField="Active" HeaderText="Active" SortExpression="Active" />
            <asp:CheckBoxField DataField="Published" HeaderText="Published" SortExpression="Published" />
            <asp:BoundField DataField="Category" HeaderText="Category" SortExpression="Category" ConvertEmptyStringToNull="False" />
            <asp:BoundField DataField="Message" HeaderText="Message" SortExpression="Message" ConvertEmptyStringToNull="False" />
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" ConvertEmptyStringToNull="False" />
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" ConvertEmptyStringToNull="False" />
            <asp:BoundField DataField="Location" HeaderText="Location" SortExpression="Location" ConvertEmptyStringToNull="False" />
            <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" ConvertEmptyStringToNull="False" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="QSP.Site.MyFeedback.Feedback"
        DeleteMethod="DeleteFeedback" EnablePaging="True" InsertMethod="InsertFeedback"
        SelectMethod="GetFeedbackList" SortParameterName="sortExpression" TypeName="QSP.Site.MyFeedback.Feedback"
        UpdateMethod="UpdateFeedback">
        <SelectParameters>
            <asp:Parameter Name="sortExpression" Type="String" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
