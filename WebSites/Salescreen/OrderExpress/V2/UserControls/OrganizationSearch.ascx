<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrganizationSearch.ascx.cs"
    Inherits="QSP.OrderExpress.Web.V2.UserControls.OrganizationSearch" %>
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
            <asp:Repeater ID="BeginWithRepeater" runat="server" 
                onitemcommand="BeginWithRepeater_ItemCommand">
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
            <span class="Text_Important">Organization type</span>
        </td>
        <td>
            <asp:DropDownList ID="ddlType" runat="server" Width="200px">
            </asp:DropDownList>
        </td>
        <td>
            <span class="Text_Important">State</span>
        </td>
        <td>
            <asp:DropDownList ID="ddlState" runat="server" Width="200px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr id="trFSMFilter" runat="server">
        <td>
            <span class="Text_Important">FSM Id</span>
        </td>
        <td>
            <asp:TextBox ID="txtFSMId" runat="server" Width="200px"></asp:TextBox>
        </td>
        <td>
            <span class="Text_Important">FSM Name</span>
        </td>
        <td>
            <asp:TextBox ID="txtFSMName" runat="server" Width="200px"></asp:TextBox>
        </td>
    </tr>
    <tr id="trFSMHierarchy" runat="server">
        <td>
            <span class="Text_Important">Display options</span>
        </td>
        <td>
            <asp:DropDownList ID="ddlFSMHierarchy" runat="server" Width="200px">
            </asp:DropDownList>
        </td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td colspan="4">
            <hr width="100%" class="Separator" />
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <span class="Text_Note_Title">Note: </span><span class="Text_Note_Text">All criteria
                is considered when searching organizations.</span>
        </td>
        <td>
            <asp:Button ID="btnSearch" runat="server" Text="Search organizations" onclick="btnSearch_Click" CssClass="LinkButton" Width="200px" />
        </td>
    </tr>
</table>
