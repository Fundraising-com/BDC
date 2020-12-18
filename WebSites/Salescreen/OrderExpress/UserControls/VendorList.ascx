<%@ Register TagPrefix="uc1" TagName="SearchModule" Src="~/UserControls/SearchModule.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl.DataGridControl" Assembly="QSP.WebControl" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.VendorList" Codebehind="VendorList.ascx.cs" %>
<table id="Table5" cellspacing="0" cellpadding="0" align="left" border="0">
    <tr>
        <td>
            <table cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td valign="top">
                        <table id="Table2" cellspacing="0" cellpadding="0" width="80%" border="0">
                            <tr>
                                <td>
                                    <uc1:SearchModule ID="QSPFormSearchModule" MaxLengthValidate="0" runat="server"></uc1:SearchModule>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table id="tblFilter" cellspacing="0" cellpadding="0" width="100%" border="0" runat="server">
                                        <tr>
                                            <td>
                                                <hr width="100%" color="#666666" size="1">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Label ID="lblHeaderFilter" runat="server" CssClass="StandardLabel">
																Filter 
																By:
                                                            </asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                <tr>
                                                                    <td>
                                                                        <table cellspacing="0" cellpadding="0" border="0">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="lblState" runat="server" CssClass="ModuleSearchText">
																						State&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                    </asp:Label></td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlState" runat="server" CssClass="boxes">
                                                                                    </asp:DropDownList></td>
                                                                            </tr>
                                                                        </table>
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
                                                <hr width="100%" color="#666666" size="1">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblNote" runat="server" CssClass="FilterNoteTitle">Note&nbsp;:</asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblNoteDesc" runat="server" CssClass="FilterNoteDesc">All criteria is considered when refreshing the list.</asp:Label></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td valign="top" align="right">
                    </td>
                </tr>
            </table>
            <br>
            <table cellspacing="0" cellpadding="0" width="400" border="0">
                <tr>
                    <td align="left" colspan="2">
                        <asp:ImageButton ID="imgbtnAddVendor" runat="server" AlternateText="Click here to Add new Vendor !"
                            ImageUrl="~/images/btnAdd.gif"></asp:ImageButton><br>
                        <br>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lblCurrentIndex" runat="server" CssClass="CurrentPageIndexLabel">Page 1 of 1</asp:Label></td>
                    <td align="right">
                        <asp:Label ID="Labelsss4" runat="server" CssClass="FilterNoteDesc">Click on Column Headings to Resort Data.</asp:Label></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td width="100%">
            <!--DataGrid  -->
            <cc2:SortedDataGrid ID="dtgVendor" runat="server" SearchMode="0" ShowFooter="True"
                DataSource="<%# DVVendor %>" AutoGenerateColumns="False" BorderColor="#CCCCCC"
                BorderStyle="None" BorderWidth="1px" BackColor="White" CellPadding="3" AllowSorting="True"
                AllowPaging="True" Width="100%" PageSize="30" Font-Size="10pt">
                <PagerStyle CssClass="PagerItemStyle" Mode="NumericPages"></PagerStyle>
                <AlternatingItemStyle CssClass="AlternatingItemStyle_off"></AlternatingItemStyle>
                <SelectedItemStyle CssClass="SelectedItemStyle"></SelectedItemStyle>
                <FooterStyle CssClass="FooterItemStyle" Font-Size="10px"></FooterStyle>
                <ItemStyle CssClass="ItemStyle_off" Font-Size="12px"></ItemStyle>
                <HeaderStyle Wrap="False" CssClass="HeaderItemStyle" Font-Size="11px" ForeColor="White">
                </HeaderStyle>
                <Columns>
                    <asp:TemplateColumn>
                        <itemtemplate>
							<asp:imagebutton id="imgBtnDetail" runat="server" ImageUrl="~/images/BtnDetail.gif" CommandName="Create"
								CausesValidation="False"></asp:imagebutton>
						</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn SortExpression="vendor_id" HeaderText="Vendor&nbsp;ID">
                        <itemstyle horizontalalign="Right"></itemstyle>
                        <itemtemplate>
							<asp:Label id="Label8" Width="50px" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.vendor_id")%>'>
							</asp:Label>
						</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn SortExpression="vendor_name" HeaderText="Vendor&nbsp;Name">
                        <itemtemplate>
							<asp:HyperLink id="hypLnkName"  Width="250px" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.vendor_name") %>' ForeColor="#336699" NavigateUrl="javascript:void(0);">
							</asp:HyperLink>
						</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="city" SortExpression="city" ReadOnly="True" HeaderText="City">
                        <itemstyle wrap="False" width="130px"></itemstyle>
                    </asp:BoundColumn>
                    <asp:TemplateColumn SortExpression="Zip" HeaderText="Zip">
                        <itemtemplate>
							<asp:Label id=Label2  Width="100px" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Zip") %>'>
							</asp:Label>
						</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="subdivision_name_1" SortExpression="subdivision_name_1"
                        ReadOnly="True" HeaderText="State"></asp:BoundColumn>
                </Columns>
            </cc2:SortedDataGrid>
        </td>
    </tr>
    <tr>
        <td>
            <table cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td>
                        <br>
                        <asp:Label ID="lblTotal" runat="server" CssClass="TotalListLabel">
							Number of Vendor(s):
                        </asp:Label></td>
                </tr>
            </table>
        </td>
    </tr>
</table>
