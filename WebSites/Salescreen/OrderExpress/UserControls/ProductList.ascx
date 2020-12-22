<%@ Reference Control="ProductDetailInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SearchModule" Src="~/UserControls/SearchModule.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl.DataGridControl" Assembly="QSP.WebControl" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.ProductList" Codebehind="ProductList.ascx.cs" %>
<table id="Table5" cellspacing="0" cellpadding="0" align="left" border="0">
    <tr>
        <td>
            <br>
            <table cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td valign="top">
                        <table id="Table2" cellspacing="0" cellpadding="0" width="80%" border="0">
                            <tr>
                                <td>
                                    <uc1:SearchModule ID="QSPFormSearchModule" runat="server" MaxLengthValidate="0"></uc1:SearchModule>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table bordercolor="#cccccc" id="tblFilter" cellspacing="1" cellpadding="0" width="100%"
                                        border="0" runat="server">
                                        <tr>
                                            <td>
                                                <hr width="100%" color="#666666" size="1">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Label ID="Label6" runat="server" CssClass="StandardLabel">
													Filter&nbsp;By:
                                                            </asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label1" runat="server" CssClass="ModuleSearchText">
																			Product&nbsp;Type&nbsp;:
                                                                        </asp:Label>
                                                                    </td>
                                                                    <td nowrap>
                                                                        <asp:DropDownList ID="ddlProductType" runat="server" CssClass="boxes">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="LblVendor" runat="server" CssClass="ModuleSearchText">
																			Vendor :
                                                                        </asp:Label>
                                                                    </td>
                                                                    <td nowrap>
                                                                        <asp:DropDownList ID="ddlVendor" runat="server" CssClass="boxes">
                                                                        </asp:DropDownList></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <hr width="100%" color="#666666" size="1">
                                                        </td>
                                                    </tr>
                                                </table>
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
                                                <br>
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
                <tr>
                    <td colspan="3">
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <br>
            <asp:HyperLink ID="hypLnkAddNew" runat="server" NavigateUrl="javascript:void(0);"
                ImageUrl="~/images/BtnAdd.gif" Visible="false"></asp:HyperLink>
        </td>
    </tr>
    <tr>
        <td>
            <br>
            <table cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td align="left">
                        <asp:Label ID="lblCurrentIndex" runat="server" CssClass="CurrentPageIndexLabel">Page 1 of 1</asp:Label></td>
                    <td align="right">
                        <asp:Label ID="Labelsss4" runat="server" CssClass="FilterNoteDesc">&nbsp;:&nbsp;Click on Column Headings to Resort Data.</asp:Label></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td width="100%">
            <!--DataGrid  -->
            <cc2:SortedDataGrid ID="dtgProduct" runat="server" Font-Size="10pt" ShowFooter="True"
                AutoGenerateColumns="False" DataSource="<%# DVProduct %>" BorderColor="#CCCCCC"
                BorderStyle="None" BorderWidth="1px" BackColor="White" CellPadding="3" AllowSorting="True"
                AllowPaging="True" PageSize="30" Width="700px">
                <PagerStyle CssClass="PagerItemStyle" Mode="NumericPages"></PagerStyle>
                <AlternatingItemStyle CssClass="AlternatingItemStyle_off"></AlternatingItemStyle>
                <FooterStyle CssClass="FooterItemStyle" Font-Size="10px"></FooterStyle>
                <SelectedItemStyle CssClass="SelectedItemStyle"></SelectedItemStyle>
                <HeaderStyle Wrap="False" CssClass="HeaderItemStyle" Font-Size="12px" ForeColor="White">
                </HeaderStyle>
                <ItemStyle CssClass="ItemStyle_off" Font-Size="12px" Wrap="False"></ItemStyle>
                <Columns>
                    <asp:TemplateColumn>
                        <itemtemplate>
							<ASP:IMAGEBUTTON id="imgBtnDetail" runat="server" ImageUrl="~/images/BtnDetail.gif" CommandName="Select" CausesValidation="False"></ASP:IMAGEBUTTON>
						</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="product_id" SortExpression="product_id" ReadOnly="True"
                        HeaderText="ID"></asp:BoundColumn>
                    <asp:BoundColumn DataField="product_code" SortExpression="product_code" ReadOnly="True"
                        HeaderText="Code&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;">
                        <itemstyle wrap="False" width="100px"></itemstyle>
                    </asp:BoundColumn>
                    <asp:TemplateColumn SortExpression="product_name" HeaderText="Name">
                        <itemtemplate>
							<ASP:LINKBUTTON id=lnkBtnProduct runat="server" Width="300px" CommandName="Select" CausesValidation="False" Text='<%# DataBinder.Eval(Container, "DataItem.product_name") %>'>
							</ASP:LINKBUTTON>
						</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn SortExpression="product_type_name" HeaderText="Type">
                        <itemtemplate>
							<asp:Label id=lblProductType runat="server"  Width="100px" Text='<%# DataBinder.Eval(Container, "DataItem.product_type_name") %>'>
							</asp:Label>
						</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn SortExpression="vendor_name" HeaderText="Vendor">
                        <itemtemplate>
							<asp:Label id=lblVendor runat="server" Width="150px" Text='<%# DataBinder.Eval(Container, "DataItem.vendor_name") %>'>
							</asp:Label>
						</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn SortExpression="nb_units" HeaderText="Nb.&nbsp;Units">
                        <itemstyle wrap="False" horizontalalign="Right"></itemstyle>
                        <itemtemplate>
							<asp:Label id="Label3" runat="server"  Width="70px" Text='<%# DataBinder.Eval(Container, "DataItem.nb_units") %>&nbsp;'>
							</asp:Label>
						</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn SortExpression="price" HeaderText="Price">
                        <itemstyle wrap="False" horizontalalign="Right"></itemstyle>
                        <itemtemplate>
							<asp:Label id="Label2" runat="server" Width="70px" Text='<%# DataBinder.Eval(Container, "DataItem.price", "{0:C}") %>&nbsp;'>
							</asp:Label>
						</itemtemplate>
                    </asp:TemplateColumn>
                </Columns>
            </cc2:SortedDataGrid>
        </td>
    </tr>
    <tr>
        <td>
            <table id="tblProduct" cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td colspan="2">
                        <br>
                        <asp:Label ID="lblTotal" runat="server" Font-Bold="true" ForeColor="#003366" Font-Size="X-Small"
                            Font-Name="Verdana">
							Number of Product(s):
                        </asp:Label>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
