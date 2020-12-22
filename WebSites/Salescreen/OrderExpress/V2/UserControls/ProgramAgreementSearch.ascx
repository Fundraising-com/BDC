<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProgramAgreementSearch.ascx.cs"
    Inherits="QSP.OrderExpress.Web.V2.UserControls.ProgramAgreementSearch" %>
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
            <span class="Text_Important">Search account name</span>
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
            <span class="Text_Important">Qsp program type</span>
        </td>
        <td>
            <asp:DropDownList ID="ddlProgramType" runat="server" Width="200px">
            </asp:DropDownList>
        </td>
        <td>
            <span class="Text_Important">Qsp program</span>
        </td>
        <td>
            <asp:DropDownList ID="ddlProgram" runat="server" Width="200px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <span class="Text_Important">Program agreement</span>
        </td>
        <td>
            <asp:DropDownList ID="ddlForm" runat="server" Width="200px">
            </asp:DropDownList>
        </td>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
            <span class="Text_Important">Status category</span>
        </td>
        <td>
            <asp:DropDownList ID="ddlStatusCategory" runat="server" Width="200px">
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
            <span class="Text_Important">FSM id</span>
        </td>
        <td>
            <asp:TextBox ID="txtFSMId" runat="server" Width="200px"></asp:TextBox>
        </td>
        <td>
            <span class="Text_Important">FSM name</span>
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
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
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
                is considered when searching program agreements.</span>
        </td>
        <td>
            <asp:Button ID="btnSearch" runat="server" Text="Search program agreements" OnClick="btnSearch_Click"
                CssClass="LinkButton" Width="200px" />
        </td>
    </tr>
</table>