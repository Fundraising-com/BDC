<%@ Page Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true"
    Inherits="QSP.OrderExpress.Web.Default" CodeBehind="Default.aspx.cs" %>
<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl.DataGridControl" Assembly="QSP.WebControl" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContentHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentHolder" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td valign="top" align="left">
                <table cellspacing="0" cellpadding="0" width="90%" border="0">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblUserName" runat="server" CssClass="PageTitleLabel">Gary Maxwell</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblPageTitle" runat="server" CssClass="PageTitleLabel"> Welcome to Order Express !</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="100" height="5px">
                        </td>
                        <td>
                            <table cellpadding="4" width="100%">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblInstruction" runat="server" CssClass="DirectionLabel" Font-Bold="True">
																Dynamic Instructions
                                        </asp:Label>
                                        <br />
                                        <br />
                                        <!-- Put general messages here -->
                                        <!-- This is the TEST Server -->
                                    </td>
                                </tr>
                                <tr id="trOrganization" runat="server" visible="false">
                                    <td>
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr>
                                                <td valign="middle" align="center">
                                                    <asp:ImageButton ID="imgOrganization" runat="server" AlternateText="Click here to go to Organization List !"
                                                        ImageUrl="~/images/icon/icon_organization.gif" Width="40px" PostBackUrl="~/V2/Forms/OrganizationSearch.aspx">
                                                    </asp:ImageButton>
                                                </td>
                                                <td>
                                                    &nbsp;&nbsp;
                                                </td>
                                                <td>
                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label5" runat="server" CssClass="StandardLabel">
																					Organization:
                                                                </asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label6" runat="server" CssClass="DescLabelDefault">
																						The organization is mainly represent by a School.  But that can be also other kinds of organizatons like Boy Scout, Charity group, etc.
																						If you want to consult the list of organization, click on the image on the left. 
                                                                </asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                    </td>
                                </tr>
                                <tr id="trAccount" runat="server" visible="false">
                                    <td>
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr>
                                                <td valign="middle" align="center">
                                                    <asp:ImageButton ID="imgAccount" runat="server" AlternateText="Click here to go to Account List !"
                                                        ImageUrl="~/images/icon/icon_account.gif" Width="40px" PostBackUrl="~/V2/Forms/AccountSearch.aspx">
                                                    </asp:ImageButton>
                                                </td>
                                                <td>
                                                    &nbsp;&nbsp;
                                                </td>
                                                <td valign="bottom">
                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label7" runat="server" CssClass="StandardLabel">
																					Account:
                                                                </asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label8" runat="server" CssClass="DescLabelDefault">
																						The account represents the trade agreement between the Organization and a fundraising program of QSP. is mainly represent by a School.  
																						The main program offer are Chocolate, Food, Gift, etc.
																						If you want to consult the list of account, click on the image on the left. 
                                                                </asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <br>
                                    </td>
                                </tr>
                                <tr id="trOrder" runat="server" visible="false">
                                    <td>
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr>
                                                <td valign="middle" align="center">
                                                    <asp:ImageButton ID="imgOrder" runat="server" AlternateText="Click here to go to Order List !"
                                                        ImageUrl="~/images/icon/icon_order.gif" Width="40px" PostBackUrl="~/V2/Forms/OrderSearch.aspx">
                                                    </asp:ImageButton>
                                                </td>
                                                <td>
                                                    &nbsp;&nbsp;
                                                </td>
                                                <td valign="bottom">
                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label9" runat="server" CssClass="StandardLabel">
																					Order:
                                                                </asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label10" runat="server" CssClass="DescLabelDefault">
																						The order is made through an account.  When adding or modifying an order, different 
																						business rules are apply depending on the program associate to the account.
																						If you want to consult the list of order for different program or form, click on the image on the left. 
                                                                </asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblQCAPOrderDetail" runat="server" CssClass="StandardLabel">QCAP Orders:</asp:Label>
                                        <asp:GridView ID="dtgOrderDetail" runat="server" ShowFooter="False" AllowSorting="True"
                                            DataKeyField="Id" AutoGenerateColumns="False" GridLines="Vertical"
                                            CellPadding="3" CssClass="GridStyle" BorderColor="#999999" 
                                            AllowPaging="True" PageSize="5"
                                            onsorting="dtgOrderDetail_Sorting" 
                                            onpageindexchanging="dtgOrderDetail_PageIndexChanging" 
                                            onselectedindexchanged="dtgOrderDetail_SelectedIndexChanged" 
                                            onrowdeleting="dtgOrderDetail_RowDeleting">
                                            <PagerStyle CssClass="PagerItemStyle"></PagerStyle>
                                            <PagerSettings Mode="Numeric"/>
                                            <AlternatingRowStyle CssClass="AlternatingItemStyle"></AlternatingRowStyle>
                                            <RowStyle CssClass="ItemStyle"></RowStyle>
                                            <HeaderStyle Wrap="False" CssClass="HeaderItemStyle" ForeColor="White"></HeaderStyle>
                                            <Columns>
                                                <asp:TemplateField>
						                            <ItemTemplate>
							                            <asp:imagebutton id="imgBtnDetail" runat="server" ImageUrl="~/images/BtnAddOrder.gif" CommandName="Select"
								                            CausesValidation="False"></asp:imagebutton>
						                            </ItemTemplate>
					                            </asp:TemplateField>
                                                <asp:TemplateField SortExpression="Id" HeaderText="Temp<br/>Order&nbsp;ID">
                                                    <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTempOrderId" CssClass="DescLabel" Text='<%# DataBinder.Eval(Container, "DataItem.Id") %>'
                                                            runat="server">
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCampaignId" Text='<%# DataBinder.Eval(Container, "DataItem.CampaignId") %>'
                                                            runat="server">
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFormId" Text='<%# DataBinder.Eval(Container, "DataItem.FormId") %>'
                                                            runat="server">
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="AccountId" HeaderText="QSP<br/>Acct&nbsp;ID">
                                                    <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAccountId" CssClass="DescLabel" Text='<%# DataBinder.Eval(Container, "DataItem.AccountId") %>'
                                                            runat="server">
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="AccountName" HeaderText="Account&nbsp;Name">
                                                    <HeaderStyle HorizontalAlign="Center" Width="250px"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAccountName" CssClass="DescLabel" Text='<%# DataBinder.Eval(Container, "DataItem.AccountName").ToString().Trim().Replace(" ", "&nbsp;")  %>'
                                                            runat="server">
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="FormName" HeaderText="Form&nbsp;Name">
                                                    <HeaderStyle HorizontalAlign="Center" Width="250px"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFormName" runat="server" CssClass="DescLabel" Text='<%# DataBinder.Eval(Container, "DataItem.FormName").ToString().Trim().Replace(" ", "&nbsp;") %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="OrderDate" HeaderText="Order&nbsp;Date">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderDate" runat="server" CssClass="DescLabel" Text='<%# DataBinder.Eval(Container, "DataItem.OrderDate") %>'>
                                                        </asp:Label>&nbsp;
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right" Width="70px" Wrap="False"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderTotal" runat="server" CssClass="DescLabel" Text='<%# DataBinder.Eval(Container, "DataItem.OrderTotal", "{0:C}") %>'>
                                                        </asp:Label>&nbsp;
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
						                            <ItemTemplate>
							                            <asp:imagebutton id="imgBtnCancel" runat="server" ImageUrl="~/images/BtnCancel.gif" CommandName="Delete"
								                            CausesValidation="False" OnClientClick="return confirm('Are you sure you want to delete this order?')"></asp:imagebutton>
						                            </ItemTemplate>
					                            </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr>
                                                <td valign="middle" align="center">
                                                    <table cellpadding="3" width="100%">
                                                        <tr>
                                                            <td align="left">
                                                                <asp:LinkButton ID="LnkBtnStatusMsg" runat="server" CssClass="StandardLabel" Font-Bold="True"
                                                                    ForeColor="#993300" OnClick="LnkBtnStatusMsg_Click"> Status Message</asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td width="9">
                                                    <img src="images/spacer.gif">
                                                </td>
                                                <td valign="middle" align="center" width="200">
                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                        <tr>
                                                            <td align="left">
                                                                <asp:Label ID="lblLstVisit" runat="server" CssClass="StandardLabel">
																					Last Visit Date:
                                                                </asp:Label><asp:Label ID="lblLstVisitDate" runat="server" CssClass="DescLabelDefault">First Time</asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <asp:Label ID="lblToday" runat="server" CssClass="StandardLabel">
																					Today:
                                                                </asp:Label><asp:Label ID="lblTodayName" runat="server" CssClass="DescLabelDefault"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="3" style="height: 18px">
                            <asp:Label ID="lblMessage" runat="server" CssClass="eRewardsError" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
            <td valign="top" align="right">
                <br />
                <br />
            </td>
        </tr>
    </table>
</asp:Content>
