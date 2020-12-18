<%@ Control Language="C#" AutoEventWireup="true" Inherits="QSP.OrderExpress.Web.UserControls.MatchingAccountList" Codebehind="MatchingAccountList.ascx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<input type="hidden" id="WidthHidden" runat="server" value="0" />
<input type="hidden" id="HeightHidden" runat="server" value="0" />
<script type="text/javascript">
    function MatchingAccountsCancel()
    {
        $find("MatchingAccountsModalBehavior").hide();
        window.location = '<%= ResolveUrl("~/V2/Forms/AccountSearch.aspx") %>';
    }
    
    function MatchingAccountsConfirm()
    {
        $find("MatchingAccountsModalBehavior").hide();
        document.getElementById("<%= MatchingAccountsConfirmedIndicator.ClientID %>").value = "True";
        <%= Page.ClientScript.GetPostBackEventReference(MatchingAccountsPanelConfirmButton, String.Empty) %>
    }
    
    // The following snippet works around a problem where FloatingBehavior
    // doesn't allow drops outside the "content area" of the page - where "content
    // area" is a little unusual for our sample web pages due to their use of CSS
    // for layout.
    function setBodyHeightToContentHeight() {
        document.body.style.height = Math.max(document.documentElement.scrollHeight, document.body.scrollHeight) - 221 + "px";

        document.getElementById("<%= WidthHidden.ClientID %>").value = document.documentElement.clientWidth - 20;
        document.getElementById("<%= HeightHidden.ClientID %>").value = document.documentElement.clientHeight - 20;
    }

    $addHandler(window, "load", setBodyHeightToContentHeight);
</script>

<asp:Panel ID="MatchingAccountsPanel" runat="server" Style="display: none" BackImageUrl="~/images/KidsJumpingYellow.gif"
    CssClass="modalPopUp" DefaultButton="MatchingAccountsPanelCancelButton">
    <asp:Panel ID="MatchingAccountsTitlePanel" runat="server" CssClass="modalPopUpTitleBackground">
        <asp:Label ID="PopUpTitleLabel" runat="server" Text="Order Express - Warning" CssClass="FormTitleLabel" Style="white-space: nowrap;"></asp:Label>
    </asp:Panel>
    <table cellpadding="0" cellspacing="0" style="margin-left: 12px; margin-right: 12px;
        margin-top: 10px; margin-bottom: 7px;">
        <tr>
            <td>
                <table cellpadding="10" cellspacing="0">
                    <tr>
                        <td valign="top">
                            <asp:Image ID="WarningImage" runat="server" Height="67px" ImageUrl="~/images/warning.gif" Width="75px" /></td>
                        <td valign="middle">
                            <asp:Label ID="MatchingAccountsInstructionLabel" runat="server" CssClass="CommentBoxStandardLabel">According to our records, this Account Information is similar to an existing account[s] in our database.<br /><br />Carefully review the Account Information again to avoid duplication.  If this account is noted below, click on 'Cancel' button to abort the creation of this account.  If it is NOT a duplication, click on "Confirm' button to continue.</asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="padding-top: 10px; overflow-y: auto;">
                <asp:GridView ID="MatchingAccountsGridView" runat="server" ShowFooter="True" AutoGenerateColumns="False" CssClass="GridStyle" BorderColor="#CCCCCC" CellPadding="3" AllowSorting="False" Width="100%" AllowPaging="true" PageSize="8">
                    <PagerStyle CssClass="PagerItemStyle" />
                    <PagerSettings Mode="Numeric" />
                    <AlternatingRowStyle CssClass="AlternatingItemStyle" Wrap="False" />
                    <FooterStyle CssClass="FooterItemStyle" Font-Size="10px" Wrap="False"></FooterStyle>
                    <SelectedRowStyle CssClass="SelectedItemStyle" Wrap="False" />
                    <RowStyle CssClass="ItemStyle" Font-Size="11px" Wrap="False" />
                    <HeaderStyle Wrap="False" CssClass="HeaderItemStyle" forecolor="White"></HeaderStyle>
                    <Columns>
                        <asp:TemplateField HeaderText="Match Probability" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="MatchProbabilityLabel" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MatchScore").ToString() + "%" %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                            <HeaderStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="QSP Account ID" HeaderStyle-ForeColor="White">
                            <ItemTemplate>
                                <asp:Label ID="AccountID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AccountId") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                            <HeaderStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Account Name" HeaderStyle-ForeColor="White">
                            <ItemTemplate>
                                <asp:Label ID="AccountNameLabel" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Name") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                            <HeaderStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Address 1" HeaderStyle-ForeColor="White">
                            <ItemTemplate>
                                <asp:Label ID="Address1Label" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Address.Address1") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                            <HeaderStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Address 2" HeaderStyle-ForeColor="White">
                            <ItemTemplate>
                                <asp:Label ID="Address2Label" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Address.Address2") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                            <HeaderStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="City" HeaderStyle-ForeColor="White">
                            <ItemTemplate>
                                <asp:Label ID="CityLabel" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Address.City") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                            <HeaderStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="County" HeaderStyle-ForeColor="White">
                            <ItemTemplate>
                                <asp:Label ID="CountyLabel" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Address.County") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                            <HeaderStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="State" HeaderStyle-ForeColor="White">
                            <ItemTemplate>
                                <asp:Label ID="StateLabel" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Address.Region") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                            <HeaderStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Zip" HeaderStyle-ForeColor="White">
                            <ItemTemplate>
                                <asp:Label ID="ZipLabel" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Address.PostCode").ToString() + (DataBinder.Eval(Container, "DataItem.Address.PostCode2") != String.Empty ? "-" + DataBinder.Eval(Container, "DataItem.Address.PostCode2").ToString() : String.Empty) %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                            <HeaderStyle Wrap="False" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="padding-top: 12px; text-align: right;">
                <asp:ImageButton ID="MatchingAccountsPanelCancelButton" runat="server" ImageUrl="~/images/btnCancel.gif" />
                <asp:ImageButton ID="MatchingAccountsPanelConfirmButton" runat="server" ImageUrl="~/images/btnConfirm.gif" />
            </td>
        </tr>
    </table>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MatchingAccountsModalPopupExtender" runat="server"
    TargetControlID="MatchingAccountsPanel" PopupControlID="MatchingAccountsPanel"
    PopupDragHandleControlID="MatchingAccountsTitlePanel" CancelControlID="MatchingAccountsPanelCancelButton"
    OkControlID="MatchingAccountsPanelConfirmButton" OnOkScript="MatchingAccountsConfirm();"
    OnCancelScript="MatchingAccountsCancel();" DropShadow="true" BackgroundCssClass="modalBackground"
    BehaviorID="MatchingAccountsModalBehavior" />
<input type="hidden" id="MatchingAccountsConfirmedIndicator" runat="server" value="False" />
