<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DocumentSearch.ascx.cs"
    Inherits="QSP.OrderExpress.Web.V2.UserControls.DocumentSearch" %>
<table width="100%">
    <tr>
        <td colspan="4">
            <span class="Text_Title">Search options</span>
        </td>
    </tr>
    <tr>
        <td width="15%">
            <span class="Text_Important">Search in</span>
        </td>
        <td width="35%">
            <asp:DropDownList ID="ddlField" runat="server" Width="200px">
            </asp:DropDownList>
        </td> 
        <td width="15%">
            <span class="Text_Important">Containing</span>
        </td>
        <td width="35%">
            <asp:TextBox ID="txtContaining" runat="server" Width="200px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <hr width="100%" class="Separator" />
        </td>
    </tr>
    <tr>
        <td>
            <span class="Text_Important">Search name</span>
        </td>
        <td colspan="3">
            <span class="Text_Important">Beginning with:</span>
        </td>
    </tr>
    <tr>
        <td>
            <span class="Text_Important">&nbsp;</span>
        </td>
        <td colspan="3">
            <asp:Repeater ID="BeginWithRepeater" runat="server" OnItemCommand="BeginWithRepeater_ItemCommand">
                <ItemTemplate>
                    <asp:LinkButton ID="lbBeginWithButton" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "Text") %>
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:Repeater>
            <br />
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <hr width="100%" class="Separator" />
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <span class="Text_Title">Filter options</span>
        </td>
    </tr>
    <tr>
        <td>
            <span class="Text_Important">Document type</span>
        </td>
        <td>
            <asp:DropDownList ID="ddlDocumentType" runat="server" Width="200px">
            </asp:DropDownList>
        </td>
        <td>
            <span class="Text_Important">Status</span>
        </td>
        <td>
            <asp:DropDownList ID="ddlStatus" runat="server" Width="200px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <hr width="100%" class="Separator" />
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <span class="Text_Note_Title">Note: </span><span class="Text_Note_Text">All criteria
                is considered when searching documents.</span>
        </td>
        <td>
            <asp:Button ID="btnSearch" runat="server" Text="Search documents" OnClick="btnSearch_Click"
                CssClass="LinkButton" Width="200px" />
        </td>
    </tr>
</table>