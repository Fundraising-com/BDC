<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl.DataGridControl" Assembly="QSP.WebControl" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.FormList" Codebehind="FormList.ascx.cs" %>
<table id="Table5" cellspacing="0" cellpadding="0" align="left" border="0">
    <tr>
        <td>
            <table cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td valign="top">
                        <table id="Table2" cellspacing="0" cellpadding="0" width="80%" border="0">
                            <tr>
                                <td>
                                    <table id="tblFilter" cellspacing="0" cellpadding="0" width="600" border="0" runat="server">
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
                                                        <td>
                                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                <tr>
                                                                    <td id="tdIncludeBaseForm" runat="server">
                                                                        <table cellspacing="0" cellpadding="0" border="0">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label3" runat="server" CssClass="ModuleSearchText">
																						&nbsp;Include&nbsp;Base&nbsp;Form&nbsp;:&nbsp;
                                                                                    </asp:Label></td>
                                                                                <td>
                                                                                    <asp:CheckBox ID="chkIncludeBaseForm" runat="server" CssClass="boxes" AutoPostBack="True"
                                                                                        OnCheckedChanged="chkIncludeBaseForm_CheckedChanged"></asp:CheckBox></td>
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
                                                <table id="tblFilterNote" runat="server" visible="false" cellspacing="0" cellpadding="0"
                                                    width="100%" border="0">
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
        </td>
    </tr>
    <tr>
        <td>
            <asp:ImageButton ID="imgBtnAddNew" ImageUrl="~/images/BtnAdd.gif" runat="server"></asp:ImageButton>
        </td>
    </tr>
    <tr>
        <td>
            <br>
            <table cellspacing="0" cellpadding="0" width="400" border="0">
                <tr>
                    <td align="left">
                        <asp:Label ID="lblCurrentIndex" runat="server" CssClass="CurrentPageIndexLabel">Page 1 of 1</asp:Label></td>
                    <td align="right">
                        <asp:Label ID="Labelsss4" runat="server" CssClass="FilterNoteDesc">Click on Column Headings to resort list.</asp:Label></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <!--DataGrid  -->
            <cc2:SortedDataGrid ID="dtgForm" runat="server" PageSize="30" AllowPaging="True"
                AllowSorting="True" CellPadding="3" BackColor="White" BorderWidth="1px" BorderStyle="None"
                BorderColor="#CCCCCC" AutoGenerateColumns="False" DataSource="<%# DVForms %>"
                ShowFooter="True" Font-Size="10pt">
                <PagerStyle CssClass="PagerItemStyle" Mode="NumericPages"></PagerStyle>
                <AlternatingItemStyle CssClass="AlternatingItemStyle_off"></AlternatingItemStyle>
                <FooterStyle CssClass="FooterItemStyle" Font-Size="10px"></FooterStyle>
                <SelectedItemStyle CssClass="SelectedItemStyle"></SelectedItemStyle>
                <ItemStyle CssClass="ItemStyle_off" Font-Size="12px"></ItemStyle>
                <HeaderStyle Wrap="False" CssClass="HeaderItemStyle" Font-Size="11px" ForeColor="White">
                </HeaderStyle>
                <Columns>
                    <asp:TemplateColumn>
                        <itemtemplate>
							<ASP:IMAGEBUTTON id="imgBtnDetail" runat="server" ImageUrl="~/images/BtnDetail.gif" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Form_ID") %>' CommandName="Select" CausesValidation="False">
							</ASP:IMAGEBUTTON>
						</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="form_id" SortExpression="form_id" ReadOnly="True" HeaderText="ID">
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="form_code" visible="false" SortExpression="form_code"
                        ReadOnly="True" HeaderText="Code">
                        <itemstyle wrap="False" width="100px"></itemstyle>
                    </asp:BoundColumn>
                    <asp:TemplateColumn SortExpression="form_name" HeaderText="Form Name">
                        <itemtemplate>
							<ASP:LINKBUTTON id="lnkBtnForm" Width="350px" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.form_name") %>' CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Form_ID") %>' CommandName="Select">
							</ASP:LINKBUTTON>
						</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <itemtemplate>
							<asp:image id="imgLogo" height=80px runat="server" Visible='<%# (DataBinder.Eval(Container, "DataItem.image_url").ToString() != "") %>' ImageUrl='<%# "~/" + DataBinder.Eval(Container, "DataItem.image_url") %>'>
							</asp:image>
						</itemtemplate>
                    </asp:TemplateColumn>
                </Columns>
            </cc2:SortedDataGrid></td>
    </tr>
    <tr>
        <td>
            <table cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td>
                        <br>
                        <asp:Label ID="lblTotal" runat="server" CssClass="TotalListLabel">
							Number of Form(s):
                        </asp:Label></td>
                </tr>
            </table>
        </td>
    </tr>
</table>
