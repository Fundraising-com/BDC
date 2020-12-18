<%@ Control Language="c#" AutoEventWireup="false" Inherits="QSP.OrderExpress.Web.AddressHygiene" Codebehind="AddressHygiene.ascx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<input type="hidden" id="WidthHidden" runat="server" value="0" />
<input type="hidden" id="HeightHidden" runat="server" value="0" />
<script type="text/javascript">
    var primaryNumberInitialValue = "";
    var secondaryNumberInitialValue = "";

    function <%= this.ClientID %>_AddressHygieneCancel()
    {
        $find("<%= this.ClientID %>_AddressHygieneModalBehavior").hide();
    }
    
    function <%= this.ClientID %>_AddressHygieneSkip()
    {
        $find("<%= this.ClientID %>_AddressHygieneModalBehavior").hide();
        <%= Page.ClientScript.GetPostBackEventReference(AddressHygienePanelSkipButton, String.Empty) %>
    }
    
    function <%= this.ClientID %>_AddressHygieneConfirm()
    {
        var userValue = "";
        var isValid = true;
        
        <% if(OutputAddress != null && OutputAddress.SuggestionList.Length != 0)
        { %>
        if(primaryNumberInitialValue != "")
        {
            userValue = document.getElementById("<%= HygienedAddressDetailsView.Rows[HygienedAddressDetailsView.PageIndex].FindControl("PrimaryNumberTextBox").ClientID %>").value;
            
            isValid = (userValue != "" && userValue != primaryNumberInitialValue);
        }
        else if(secondaryNumberInitialValue != "")
        {
            userValue = document.getElementById("<%= HygienedAddressDetailsView.Rows[HygienedAddressDetailsView.PageIndex].FindControl("UnitNumberTextBox").ClientID %>").value;
            
            isValid = (userValue != "" && userValue != secondaryNumberInitialValue);
        }
        <% } %>
        
        if(isValid)
        {
            $find("<%= this.ClientID %>_AddressHygieneModalBehavior").hide();
            <%= Page.ClientScript.GetPostBackEventReference(AddressHygienePanelConfirmButton, String.Empty) %>
        }
        else
        {
            document.getElementById("<%= AddressHygieneInstructionLabel.ClientID %>").innerHTML = "<%= Resources.AddressHygiene.IncompleteSuggestionListItemInstructionLabel %>";
        }
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

<asp:Panel ID="AddressHygienePanel" runat="server" Style="display: none" BackImageUrl="~/images/KidsJumpingYellow.gif"
    CssClass="modalPopUp" DefaultButton="AddressHygienePanelConfirmButton">
    <asp:Panel ID="AddressHygieneTitlePanel" runat="server" CssClass="modalPopUpTitleBackground">
        <asp:Label ID="PopUpTitleLabel" runat="server" Text="Order Express - Warning" CssClass="FormTitleLabel" Style="white-space: nowrap;"></asp:Label>
    </asp:Panel>
    <table cellpadding="0" cellspacing="0" style="margin-left: 12px; margin-right: 12px;
        margin-top: 10px; margin-bottom: 7px; width: 650px;">
        <tr>
            <td>
                <table cellpadding="10" cellspacing="0">
                    <tr>
                        <td valign="top">
                            <asp:Image ID="WarningImage" runat="server" Height="67px" ImageUrl="~/images/warning.gif" Width="75px" /></td>
                        <td valign="middle">
                            <asp:Label ID="AddressHygieneInstructionLabel" runat="server" CssClass="CommentBoxStandardLabel"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="padding-top: 10px; overflow-y: auto;">
                    <asp:DetailsView ID="HygienedAddressDetailsView" runat="server" CellPadding="0" GridLines="None" AutoGenerateRows="false">
                        <RowStyle Wrap="False" />
                        <Fields>
                            <asp:TemplateField HeaderText="Address Line 1">
                                <ItemTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0">
                                        <tr valign="bottom">
                                            <td>
                                                <asp:TextBox ID="PrimaryNumberTextBox" runat="server" CssClass="OrderAddressDescLabel" style="display: none; margin-right: 5px;" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Address1Label" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Address1") %>' Style="font-size: 8pt; font-family:Verdana,Arial;"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <ItemStyle Wrap="False" />
                                <HeaderStyle CssClass="OrderAddressStandardLabel" Width="125px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Address Line 2">
                                <ItemTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0">
                                        <tr valign="bottom">
                                            <td>
                                                <asp:Label ID="Address2Label" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Address2") %>' Style="font-size: 8pt; font-family:Verdana,Arial;"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="UnitNumberTextBox" runat="server" CssClass="OrderAddressDescLabel" style="display: none; margin-left: 5px;" Width="50px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <HeaderStyle CssClass="OrderAddressStandardLabel" Width="125px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="City">
                                <ItemTemplate>
                                    <asp:Label ID="CityLabel" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.City") %>' Style="font-size: 8pt; font-family:Verdana,Arial;"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="OrderAddressStandardLabel" Width="125px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="County">
                                <ItemTemplate>
                                    <asp:Label ID="CountyLabel" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.County") %>' Style="font-size: 8pt; font-family:Verdana,Arial;"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="OrderAddressStandardLabel" Width="125px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="State">
                                <ItemTemplate>
                                    <asp:Label ID="RegionLabel" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Region") %>' Style="font-size: 8pt; font-family:Verdana,Arial;"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="OrderAddressStandardLabel" Width="125px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Zip">
                                <ItemTemplate>
                                    <asp:Label ID="ZipLabel" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PostCode").ToString() + (DataBinder.Eval(Container, "DataItem.PostCode2") != String.Empty ? "-" + DataBinder.Eval(Container, "DataItem.PostCode2").ToString() : String.Empty) %>' Style="font-size: 8pt; font-family:Verdana,Arial;"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="OrderAddressStandardLabel" Width="125px" />
                            </asp:TemplateField>
                        </Fields>
                    </asp:DetailsView>
                    <br />
                    <asp:GridView id="SuggestionListGridView" AllowSorting="True" runat="server" ShowFooter="True" AutoGenerateColumns="False" CssClass="GridStyle" BorderColor="#CCCCCC" CellPadding="3" Width="100%" AllowPaging="true" PageSize="8">
                        <PagerStyle CssClass="PagerItemStyle" />
                        <PagerSettings Mode="Numeric" />
                        <AlternatingRowStyle CssClass="AlternatingItemStyle" Wrap="False" />
                        <FooterStyle CssClass="FooterItemStyle" Font-Size="10px" Wrap="False"></FooterStyle>
                        <SelectedRowStyle CssClass="SelectedItemStyle" Wrap="False" />
                        <RowStyle CssClass="ItemStyle" Font-Size="11px" Wrap="False" />
                        <HeaderStyle Wrap="False" CssClass="HeaderItemStyle" forecolor="White"></HeaderStyle>
	                    <Columns>
	                        <asp:TemplateField>
	                            <HeaderStyle Width="1%"></HeaderStyle>
				                <ItemTemplate>
				                    <asp:Label ID="SelectionLabel" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Selection") %>' Style="display: none;"></asp:Label>
				                    <input type="image" id="SelectButton" runat="server" src="~/images/BtnSelect.gif" causesvalidation="false" />
				                </ItemTemplate>
			                </asp:TemplateField>
				            <asp:TemplateField>
					            <ItemTemplate>
					                <input type="image" id="DetailButton" runat="server" src="~/images/BtnDetail.gif" causesvalidation="false" />
					            </ItemTemplate>
				            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Address 1" SortExpression="Address1" HeaderStyle-ForeColor="White" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:Label ID="Address1Label" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Address1") %>'></asp:Label>
                                    <asp:Label ID="PrimaryNumberLowLabel" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PrimaryAddressComponents.PrimaryNumberLow") %>' Style="display: none;"></asp:Label>
                                    <asp:Label ID="PrimaryNumberHighLabel" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PrimaryAddressComponents.PrimaryNumberHigh") %>' Style="display: none;"></asp:Label>
                                    <asp:Label ID="PrimaryPrefixLabel" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PrimaryAddressComponents.PrimaryPrefix") %>' Style="display: none;"></asp:Label>
                                    <asp:Label ID="PrimaryNameLabel" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PrimaryAddressComponents.PrimaryName") %>' Style="display: none;"></asp:Label>
                                    <asp:Label ID="PrimaryTypeLabel" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PrimaryAddressComponents.PrimaryType") %>' Style="display: none;"></asp:Label>
                                    <asp:Label ID="PrimaryPostfixLabel" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PrimaryAddressComponents.PrimaryPostfix") %>' Style="display: none;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
		                    <asp:TemplateField HeaderText="Address 2" SortExpression="Address2" HeaderStyle-ForeColor="White" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:Label ID="Address2Label" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Address2") %>'></asp:Label>
                                    <asp:Label ID="UnitDescriptionLabel" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SecondaryAddressComponents.UnitDescription") %>' Style="display: none;"></asp:Label>
                                    <asp:Label ID="UnitNumberLowLabel" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SecondaryAddressComponents.UnitNumberLow") %>' Style="display: none;"></asp:Label>
                                    <asp:Label ID="UnitNumberHighLabel" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SecondaryAddressComponents.UnitNumberHigh") %>' Style="display: none;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
		                    <asp:TemplateField HeaderText="City" SortExpression="City" HeaderStyle-ForeColor="White" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:Label ID="CityLabel" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.City") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
		                    <asp:TemplateField HeaderText="County" SortExpression="County" HeaderStyle-ForeColor="White" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:Label ID="CountyLabel" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.County") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
		                    <asp:TemplateField HeaderText="State" SortExpression="Region" HeaderStyle-ForeColor="White" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:Label ID="RegionLabel" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Region") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
		                    <asp:TemplateField HeaderText="Zip" SortExpression="PostCode" HeaderStyle-ForeColor="White" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:Label ID="ZipLabel" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PostCode").ToString() + (DataBinder.Eval(Container, "DataItem.PostCode2") != String.Empty ? "-" + DataBinder.Eval(Container, "DataItem.PostCode2").ToString() : String.Empty) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
	                    </Columns>
                    </asp:GridView>
                
                <input type="hidden" id="SelectedAddressIDHidden" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="padding-top: 12px; text-align: right;">
                <table cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td>
                            <asp:ImageButton ID="AddressHygienePanelCancelButton" runat="server" ImageUrl="~/images/btnCancel.gif" />
                        </td>
                        <td>
                            <asp:ImageButton ID="AddressHygienePanelSkipButton" runat="server" ImageUrl="~/images/btnSkip.gif" />
                        </td>
                        <td>
                            <asp:ImageButton ID="AddressHygienePanelConfirmButton" runat="server" ImageUrl="~/images/btnConfirm.gif" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="AddressHygieneModalPopupExtender" runat="server"
    TargetControlID="AddressHygienePanel" PopupControlID="AddressHygienePanel"
    PopupDragHandleControlID="AddressHygieneTitlePanel" CancelControlID="AddressHygienePanelCancelButton"
    OkControlID="AddressHygienePanelConfirmButton" DropShadow="true" BackgroundCssClass="modalBackground" Drag="true" />
