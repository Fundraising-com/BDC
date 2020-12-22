<%@ Control Language="C#" AutoEventWireup="true" Inherits="QSP.OrderExpress.Web.UserControls.Menu"
    CodeBehind="Menu.ascx.cs" %>
<div id="HeaderDiv" runat="server" style="width: 100%; height: 45px; background-image: url(images/Header_PatternNew.gif);
    border-bottom: #f9f0c7 2px solid; overflow: hidden;">
    <asp:Image ID="Image9" ImageAlign="Top" runat="server" ImageUrl="~/images/Header_Left.gif"
        Width="80px"></asp:Image>
    <asp:Image ID="Image10" ImageAlign="Top" runat="server" ImageUrl="~/images/Header_Middle1.gif"
        Width="97px"></asp:Image>
    <asp:Image ID="Image11" ImageAlign="Top" runat="server" ImageUrl="~/images/Header_Middle2.gif"
        Width="133px"></asp:Image>
    <asp:Image ID="Image12" ImageAlign="Top" runat="server" ImageUrl="~/images/Header_Right.gif"
        Style="position: relative; left: 46%;"></asp:Image>
</div>
<div id="HeaderDivPrint" runat="server" style="width: 100%; height: 45px; border-bottom: #f9f0c7 2px solid;
    overflow: hidden;">
    <span class="PageHeader1">QSP Order Express</span>
</div>
<div id="MenuDiv" runat="server" style="height: 20px; background-image: url(images/MenuBar_Pattern.gif);">
    <asp:Menu ID="MenuSuperUser" runat="server" Orientation="Horizontal" BorderWidth="0"
        Font-Bold="true" BackColor="#f3cc60" BorderStyle="solid" ForeColor="#005596"
        Font-Names="Arial, Tahoma" Font-Size="8pt" DynamicMenuStyle-CssClass="Menu" DynamicMenuItemStyle-CssClass="MenuItem"
        StaticMenuItemStyle-VerticalPadding="3" StaticMenuItemStyle-HorizontalPadding="3"
        StaticMenuItemStyle-ItemSpacing="18" DynamicMenuItemStyle-VerticalPadding="3"
        DynamicMenuItemStyle-HorizontalPadding="3" StaticEnableDefaultPopOutImage="false"
        DynamicEnableDefaultPopOutImage="false" DynamicMenuStyle-BackColor="#f3cc60"
        DynamicMenuItemStyle-BackColor="#f3cc60">
        <Items>
            <asp:MenuItem Text="Home" NavigateUrl="~/Default.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Notes" NavigateUrl="~/MyNoteList.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Organizations" NavigateUrl="~/V2/Forms/OrganizationSearch.aspx">
                <asp:MenuItem Text="Search organizations" NavigateUrl="~/V2/Forms/OrganizationSearch.aspx">
                </asp:MenuItem>
            </asp:MenuItem>
            <asp:MenuItem Text="Accounts" NavigateUrl="~/V2/Forms/AccountSearch.aspx">
                <asp:MenuItem Text="Search accounts" NavigateUrl="~/V2/Forms/AccountSearch.aspx">
                </asp:MenuItem>
                <asp:MenuItem Text="Create account" NavigateUrl="~/AccountStep_Search.aspx"></asp:MenuItem>
            </asp:MenuItem>
            <asp:MenuItem Text="Program Agreements" NavigateUrl="~/V2/Forms/ProgramAgreementSearch.aspx">
                <asp:MenuItem Text="Search program agreements" NavigateUrl="~/V2/Forms/ProgramAgreementSearch.aspx">
                </asp:MenuItem>
                <asp:MenuItem Text="Create program agreement" NavigateUrl="~/ProgramAgreementStep_Search.aspx?&amp;ProgType=7">
                </asp:MenuItem>
            </asp:MenuItem>
            <asp:MenuItem Text="Orders" NavigateUrl="~/V2/Forms/OrderSearch.aspx">
                <asp:MenuItem Text="Search orders" NavigateUrl="~/V2/Forms/OrderSearch.aspx"></asp:MenuItem>
                <asp:MenuItem Text="Create order" NavigateUrl="~/OrderStep_Search.aspx?&amp;ProgType=7">
                </asp:MenuItem>
            </asp:MenuItem>
            <asp:MenuItem Text="Other">
                <asp:MenuItem Text="Documents" NavigateUrl="~/V2/Forms/DocumentSearch.aspx"></asp:MenuItem>
                <asp:MenuItem Text="Notes" NavigateUrl="~/NoteList.aspx"></asp:MenuItem>
                <asp:MenuItem Text="Users" NavigateUrl="~/UserList.aspx"></asp:MenuItem>
                <asp:MenuItem Text="Login registry" NavigateUrl="~/RegistryList.aspx"></asp:MenuItem>
                <asp:MenuItem Text="Account transfer" NavigateUrl="~/AccountTransfer.aspx"></asp:MenuItem>
            </asp:MenuItem>
            <asp:MenuItem Text="Help">
                <asp:MenuItem Text="Questions and answers" NavigateUrl="~/Documents/Order Express Q and A 01-30-07.pdf">
                </asp:MenuItem>
                <asp:MenuItem Text="FSM user manual" NavigateUrl="~/Documents/Order Express FSM User Manual - 01-30-07.pdf">
                </asp:MenuItem>
            </asp:MenuItem>
            <asp:MenuItem Text="Logout" NavigateUrl="~/Logout.aspx"></asp:MenuItem>
        </Items>
    </asp:Menu>
    <asp:Menu ID="MenuAdmin" runat="server" Orientation="Horizontal" BorderWidth="0"
        Font-Bold="true" BackColor="#f3cc60" BorderStyle="solid" ForeColor="#005596"
        Font-Names="Arial, Tahoma" Font-Size="8pt" DynamicMenuStyle-CssClass="Menu" DynamicMenuItemStyle-CssClass="MenuItem"
        StaticMenuItemStyle-VerticalPadding="3" StaticMenuItemStyle-HorizontalPadding="3"
        StaticMenuItemStyle-ItemSpacing="18" DynamicMenuItemStyle-VerticalPadding="3"
        DynamicMenuItemStyle-HorizontalPadding="3" StaticEnableDefaultPopOutImage="false"
        DynamicEnableDefaultPopOutImage="false" DynamicMenuStyle-BackColor="#f3cc60"
        DynamicMenuItemStyle-BackColor="#f3cc60">
        <Items>
            <asp:MenuItem Text="Home" NavigateUrl="~/Default.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Notes" NavigateUrl="~/MyNoteList.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Organizations" NavigateUrl="~/V2/Forms/OrganizationSearch.aspx">
                <asp:MenuItem Text="Search organizations" NavigateUrl="~/V2/Forms/OrganizationSearch.aspx">
                </asp:MenuItem>
            </asp:MenuItem>
            <asp:MenuItem Text="Accounts" NavigateUrl="~/V2/Forms/AccountSearch.aspx">
                <asp:MenuItem Text="Search accounts" NavigateUrl="~/V2/Forms/AccountSearch.aspx">
                </asp:MenuItem>
                <asp:MenuItem Text="Create account" NavigateUrl="~/AccountStep_Search.aspx"></asp:MenuItem>
            </asp:MenuItem>
            <asp:MenuItem Text="Program Agreements" NavigateUrl="~/V2/Forms/ProgramAgreementSearch.aspx">
                <asp:MenuItem Text="Search program agreements" NavigateUrl="~/V2/Forms/ProgramAgreementSearch.aspx">
                </asp:MenuItem>
                <asp:MenuItem Text="Create program agreement" NavigateUrl="~/ProgramAgreementStep_Search.aspx?&amp;ProgType=7">
                </asp:MenuItem>
            </asp:MenuItem>
            <asp:MenuItem Text="Orders" NavigateUrl="~/V2/Forms/OrderSearch.aspx">
                <asp:MenuItem Text="Search orders" NavigateUrl="~/V2/Forms/OrderSearch.aspx"></asp:MenuItem>
                <asp:MenuItem Text="Create order" NavigateUrl="~/OrderStep_Search.aspx?&amp;ProgType=7">
                </asp:MenuItem>
            </asp:MenuItem>
            <asp:MenuItem Text="Other">
                <asp:MenuItem Text="Documents" NavigateUrl="~/V2/Forms/DocumentSearch.aspx"></asp:MenuItem>
                <asp:MenuItem Text="Notes" NavigateUrl="~/NoteList.aspx"></asp:MenuItem>
                <asp:MenuItem Text="Users" NavigateUrl="~/UserList.aspx"></asp:MenuItem>
                <asp:MenuItem Text="Login registry" NavigateUrl="~/RegistryList.aspx"></asp:MenuItem>
                <asp:MenuItem Text="Account transfer" NavigateUrl="~/AccountTransfer.aspx"></asp:MenuItem>
            </asp:MenuItem>
            <asp:MenuItem Text="Help">
                <asp:MenuItem Text="Questions and answers" NavigateUrl="~/Documents/Order Express Q and A 01-30-07.pdf">
                </asp:MenuItem>
                <asp:MenuItem Text="FSM user manual" NavigateUrl="~/Documents/Order Express FSM User Manual - 01-30-07.pdf">
                </asp:MenuItem>
            </asp:MenuItem>
            <asp:MenuItem Text="Logout" NavigateUrl="~/Logout.aspx"></asp:MenuItem>
        </Items>
    </asp:Menu>
    <asp:Menu ID="MenuAccountManager" runat="server" Orientation="Horizontal" BorderWidth="0"
        Font-Bold="true" BackColor="#f3cc60" BorderStyle="solid" ForeColor="#005596"
        Font-Names="Arial, Tahoma" Font-Size="8pt" DynamicMenuStyle-CssClass="Menu" DynamicMenuItemStyle-CssClass="MenuItem"
        StaticMenuItemStyle-VerticalPadding="3" StaticMenuItemStyle-HorizontalPadding="3"
        StaticMenuItemStyle-ItemSpacing="18" DynamicMenuItemStyle-VerticalPadding="3"
        DynamicMenuItemStyle-HorizontalPadding="3" StaticEnableDefaultPopOutImage="false"
        DynamicEnableDefaultPopOutImage="false" DynamicMenuStyle-BackColor="#f3cc60"
        DynamicMenuItemStyle-BackColor="#f3cc60">
        <Items>
            <asp:MenuItem Text="Home" NavigateUrl="~/Default.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Notes" NavigateUrl="~/MyNoteList.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Organizations" NavigateUrl="~/V2/Forms/OrganizationSearch.aspx">
                <asp:MenuItem Text="Search organizations" NavigateUrl="~/V2/Forms/OrganizationSearch.aspx">
                </asp:MenuItem>
            </asp:MenuItem>
            <asp:MenuItem Text="Accounts" NavigateUrl="~/V2/Forms/AccountSearch.aspx">
                <asp:MenuItem Text="Search accounts" NavigateUrl="~/V2/Forms/AccountSearch.aspx">
                </asp:MenuItem>
                <asp:MenuItem Text="Create account" NavigateUrl="~/AccountStep_Search.aspx"></asp:MenuItem>
            </asp:MenuItem>
            <asp:MenuItem Text="Program Agreements" NavigateUrl="~/V2/Forms/ProgramAgreementSearch.aspx">
                <asp:MenuItem Text="Search program agreements" NavigateUrl="~/V2/Forms/ProgramAgreementSearch.aspx">
                </asp:MenuItem>
                <asp:MenuItem Text="Create program agreement" NavigateUrl="~/ProgramAgreementStep_Search.aspx?&amp;ProgType=7">
                </asp:MenuItem>
            </asp:MenuItem>
            <asp:MenuItem Text="Orders" NavigateUrl="~/V2/Forms/OrderSearch.aspx">
                <asp:MenuItem Text="Search orders" NavigateUrl="~/V2/Forms/OrderSearch.aspx"></asp:MenuItem>
                <asp:MenuItem Text="Create order" NavigateUrl="~/OrderStep_Search.aspx?&amp;ProgType=7">
                </asp:MenuItem>
            </asp:MenuItem>
            <asp:MenuItem Text="Other">
                <asp:MenuItem Text="Documents" NavigateUrl="~/V2/Forms/DocumentSearch.aspx"></asp:MenuItem>
                <asp:MenuItem Text="Notes" NavigateUrl="~/NoteList.aspx"></asp:MenuItem>
                <asp:MenuItem Text="Account transfer" NavigateUrl="~/AccountTransfer.aspx"></asp:MenuItem>
            </asp:MenuItem>
            <asp:MenuItem Text="Help">
                <asp:MenuItem Text="Questions and answers" NavigateUrl="~/Documents/Order Express Q and A 01-30-07.pdf">
                </asp:MenuItem>
                <asp:MenuItem Text="FSM user manual" NavigateUrl="~/Documents/Order Express FSM User Manual - 01-30-07.pdf">
                </asp:MenuItem>
            </asp:MenuItem>
            <asp:MenuItem Text="Logout" NavigateUrl="~/Logout.aspx"></asp:MenuItem>
        </Items>
    </asp:Menu>
    <asp:Menu ID="MenuFieldSupport" runat="server" Orientation="Horizontal" BorderWidth="0"
        Font-Bold="true" BackColor="#f3cc60" BorderStyle="solid" ForeColor="#005596"
        Font-Names="Arial, Tahoma" Font-Size="8pt" DynamicMenuStyle-CssClass="Menu" DynamicMenuItemStyle-CssClass="MenuItem"
        StaticMenuItemStyle-VerticalPadding="3" StaticMenuItemStyle-HorizontalPadding="3"
        StaticMenuItemStyle-ItemSpacing="18" DynamicMenuItemStyle-VerticalPadding="3"
        DynamicMenuItemStyle-HorizontalPadding="3" StaticEnableDefaultPopOutImage="false"
        DynamicEnableDefaultPopOutImage="false" DynamicMenuStyle-BackColor="#f3cc60"
        DynamicMenuItemStyle-BackColor="#f3cc60">
        <Items>
            <asp:MenuItem Text="Home" NavigateUrl="~/Default.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Notes" NavigateUrl="~/MyNoteList.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Organizations" NavigateUrl="~/V2/Forms/OrganizationSearch.aspx">
                <asp:MenuItem Text="Search organizations" NavigateUrl="~/V2/Forms/OrganizationSearch.aspx">
                </asp:MenuItem>
            </asp:MenuItem>
            <asp:MenuItem Text="Accounts" NavigateUrl="~/V2/Forms/AccountSearch.aspx">
                <asp:MenuItem Text="Search accounts" NavigateUrl="~/V2/Forms/AccountSearch.aspx">
                </asp:MenuItem>
                <asp:MenuItem Text="Create account" NavigateUrl="~/AccountStep_Search.aspx"></asp:MenuItem>
            </asp:MenuItem>
            <asp:MenuItem Text="Program Agreements" NavigateUrl="~/V2/Forms/ProgramAgreementSearch.aspx">
                <asp:MenuItem Text="Search program agreements" NavigateUrl="~/V2/Forms/ProgramAgreementSearch.aspx">
                </asp:MenuItem>
                <asp:MenuItem Text="Create program agreement" NavigateUrl="~/ProgramAgreementStep_Search.aspx?&amp;ProgType=7">
                </asp:MenuItem>
            </asp:MenuItem>
            <asp:MenuItem Text="Orders" NavigateUrl="~/V2/Forms/OrderSearch.aspx">
                <asp:MenuItem Text="Search orders" NavigateUrl="~/V2/Forms/OrderSearch.aspx"></asp:MenuItem>
                <asp:MenuItem Text="Create order" NavigateUrl="~/OrderStep_Search.aspx?&amp;ProgType=7">
                </asp:MenuItem>
            </asp:MenuItem>
            <asp:MenuItem Text="Other">
                <asp:MenuItem Text="Documents" NavigateUrl="~/V2/Forms/DocumentSearch.aspx"></asp:MenuItem>
                <asp:MenuItem Text="Account transfer" NavigateUrl="~/AccountTransfer.aspx"></asp:MenuItem>
            </asp:MenuItem>
            <asp:MenuItem Text="Help">
                <asp:MenuItem Text="Questions and answers" NavigateUrl="~/Documents/Order Express Q and A 01-30-07.pdf">
                </asp:MenuItem>
                <asp:MenuItem Text="FSM user manual" NavigateUrl="~/Documents/Order Express FSM User Manual - 01-30-07.pdf">
                </asp:MenuItem>
            </asp:MenuItem>
            <asp:MenuItem Text="Logout" NavigateUrl="~/Logout.aspx"></asp:MenuItem>
        </Items>
    </asp:Menu>
    <asp:Menu ID="MenuFieldSalesManager" runat="server" Orientation="Horizontal" BorderWidth="0"
        Font-Bold="true" BackColor="#f3cc60" BorderStyle="solid" ForeColor="#005596"
        Font-Names="Arial, Tahoma" Font-Size="8pt" DynamicMenuStyle-CssClass="Menu" DynamicMenuItemStyle-CssClass="MenuItem"
        StaticMenuItemStyle-VerticalPadding="3" StaticMenuItemStyle-HorizontalPadding="3"
        StaticMenuItemStyle-ItemSpacing="18" DynamicMenuItemStyle-VerticalPadding="3"
        DynamicMenuItemStyle-HorizontalPadding="3" StaticEnableDefaultPopOutImage="false"
        DynamicEnableDefaultPopOutImage="false" DynamicMenuStyle-BackColor="#f3cc60"
        DynamicMenuItemStyle-BackColor="#f3cc60">
        <Items>
            <asp:MenuItem Text="Home" NavigateUrl="~/Default.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Notes" NavigateUrl="~/MyNoteList.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Organizations" NavigateUrl="~/V2/Forms/OrganizationSearch.aspx">
                <asp:MenuItem Text="Search organizations" NavigateUrl="~/V2/Forms/OrganizationSearch.aspx">
                </asp:MenuItem>
            </asp:MenuItem>
            <asp:MenuItem Text="Accounts" NavigateUrl="~/V2/Forms/AccountSearch.aspx">
                <asp:MenuItem Text="Search accounts" NavigateUrl="~/V2/Forms/AccountSearch.aspx">
                </asp:MenuItem>
                <asp:MenuItem Text="Create account" NavigateUrl="~/AccountStep_Search.aspx"></asp:MenuItem>
            </asp:MenuItem>
            <asp:MenuItem Text="Program Agreements" NavigateUrl="~/V2/Forms/ProgramAgreementSearch.aspx">
                <asp:MenuItem Text="Search program agreements" NavigateUrl="~/V2/Forms/ProgramAgreementSearch.aspx">
                </asp:MenuItem>
                <asp:MenuItem Text="Create program agreement" NavigateUrl="~/ProgramAgreementStep_Search.aspx?&amp;ProgType=7">
                </asp:MenuItem>
            </asp:MenuItem>
            <asp:MenuItem Text="Orders" NavigateUrl="~/V2/Forms/OrderSearch.aspx">
                <asp:MenuItem Text="Search orders" NavigateUrl="~/V2/Forms/OrderSearch.aspx"></asp:MenuItem>
                <asp:MenuItem Text="Create order" NavigateUrl="~/OrderStep_Search.aspx?&amp;ProgType=7">
                </asp:MenuItem>
            </asp:MenuItem>
            <asp:MenuItem Text="Help">
                <asp:MenuItem Text="Questions and answers" NavigateUrl="~/Documents/Order Express Q and A 01-30-07.pdf">
                </asp:MenuItem>
                <asp:MenuItem Text="FSM user manual" NavigateUrl="~/Documents/Order Express FSM User Manual - 01-30-07.pdf">
                </asp:MenuItem>
            </asp:MenuItem>
            <asp:MenuItem Text="Logout" NavigateUrl="~/Logout.aspx"></asp:MenuItem>
        </Items>
    </asp:Menu>
    <asp:Menu ID="MenuNoRole" runat="server" Orientation="Horizontal" BorderWidth="0"
        Font-Bold="true" BackColor="#f3cc60" BorderStyle="solid" ForeColor="#005596"
        Font-Names="Arial, Tahoma" Font-Size="8pt" DynamicMenuStyle-CssClass="Menu" DynamicMenuItemStyle-CssClass="MenuItem"
        StaticMenuItemStyle-VerticalPadding="3" StaticMenuItemStyle-HorizontalPadding="3"
        StaticMenuItemStyle-ItemSpacing="18" DynamicMenuItemStyle-VerticalPadding="3"
        DynamicMenuItemStyle-HorizontalPadding="3" StaticEnableDefaultPopOutImage="false"
        DynamicEnableDefaultPopOutImage="false" DynamicMenuStyle-BackColor="#f3cc60"
        DynamicMenuItemStyle-BackColor="#f3cc60">
        <Items>
            <asp:MenuItem Text="Login" NavigateUrl="~/Login.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Help">
                <asp:MenuItem Text="Questions and answers" NavigateUrl="~/Documents/Order Express Q and A 01-30-07.pdf">
                </asp:MenuItem>
                <asp:MenuItem Text="FSM user manual" NavigateUrl="~/Documents/Order Express FSM User Manual - 01-30-07.pdf">
                </asp:MenuItem>
            </asp:MenuItem>
        </Items>
    </asp:Menu>
</div>
