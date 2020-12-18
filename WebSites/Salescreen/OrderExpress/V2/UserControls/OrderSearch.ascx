<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderSearch.ascx.cs"
    Inherits="QSP.OrderExpress.Web.V2.UserControls.OrderSearch" %>
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
            <span class="Text_Important">Source</span>
        </td>
        <td>
            <asp:DropDownList ID="ddlSource" runat="server" Width="200px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <span class="Text_Important">Order type</span>
        </td>
        <td>
            <asp:DropDownList ID="ddlOrderType" runat="server" Width="200px">
            </asp:DropDownList>
        </td>
        <td>
            <span class="Text_Important">Order form</span>
        </td>
        <td>
            <asp:DropDownList ID="ddlForm" runat="server" Width="200px">
            </asp:DropDownList>
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
    <tr>
        <td>
            <span class="Text_Important">Start date</span>
        </td>
        <td>
            <asp:TextBox ID="tbStartDate" runat="server" Width="200px"></asp:TextBox>
            <ajaxtoolkit:CalendarExtender ID="tbStartDate_CalendarExtender" runat="server" Enabled="True"
                TargetControlID="tbStartDate">
            </ajaxtoolkit:CalendarExtender>
        </td>
        <td>
            <span class="Text_Important">End date</span>
        </td>
        <td>
            <asp:TextBox ID="tbEndDate" runat="server" Width="200px"></asp:TextBox>
            <ajaxtoolkit:CalendarExtender ID="tbEndDate_CalendarExtender" runat="server" Enabled="True"
                TargetControlID="tbEndDate">
            </ajaxtoolkit:CalendarExtender>
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
                is considered when searching orders.</span>
        </td>
        <td>
            <asp:Button ID="btnSearch" runat="server" Text="Search orders" OnClick="btnSearch_Click"
                CssClass="LinkButton" Width="200px" />
        </td>
    </tr>
</table>
