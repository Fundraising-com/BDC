<%@ Reference Control="BusinessNotificationDetailInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ToolBar" Src="~/UserControls/ToolBar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SearchModule" Src="~/UserControls/SearchModule.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl.DataGridControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.BusinessNotificationList" Codebehind="BusinessNotificationList.ascx.cs" %>
<table id="Table5" cellspacing="0" cellpadding="0" align="left" border="0">
    <tr>
        <td>
            <table id="Table6" cellspacing="0" cellpadding="0" width="20%" border="0">
                <tr>
                    <td>
                        <uc1:SearchModule ID="QSPFormSearchModule" runat="server"></uc1:SearchModule>
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
                                <td colspan="2">
                                    <asp:Label ID="Label3" runat="server" CssClass="StandardLabel">
										Filter By:
                                    </asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td>
                                                <table cellspacing="0" cellpadding="0" border="0">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label5" runat="server" CssClass="ModuleSearchText">
																Status:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            </asp:Label></td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlStatus" runat="server">
                                                                <asp:ListItem Value="0">---SELECT---</asp:ListItem>
                                                                <asp:ListItem Value="2">Read</asp:ListItem>
                                                                <asp:ListItem Value="1" Selected="True">Unread</asp:ListItem>
                                                            </asp:DropDownList></td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <table cellspacing="0" cellpadding="0" border="0" id="Table1" runat="server">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" CssClass="ModuleSearchText">
																Note&nbsp;Relates&nbsp;To:&nbsp;
                                                            </asp:Label></td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlEntityType" runat="server" CssClass="boxes">
                                                                <asp:ListItem Selected="True" Value="0">--SELECT--</asp:ListItem>
                                                                <asp:ListItem Value="2">Account</asp:ListItem>
                                                                <asp:ListItem Value="4">Order</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table cellspacing="0" cellpadding="0" border="0" id="tblFilterNoteType" runat="server">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblNoteType" runat="server" CssClass="ModuleSearchText">
																Note&nbsp;Type:&nbsp;
                                                            </asp:Label></td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlNoteType" runat="server" CssClass="boxes">
                                                            </asp:DropDownList></td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td>
                                                &nbsp;&nbsp;
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
                                                <asp:Label ID="lblNote" runat="server" CssClass="FilterNoteTitle">Note:</asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblNoteDesc" runat="server" CssClass="FilterNoteDesc">All&nbsp;criteria&nbsp;is&nbsp;considered&nbsp;when&nbsp;refreshing&nbsp;the&nbsp;list.</asp:Label></td>
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
        <td height="5px">
        </td>
    </tr>
    <tr>
        <td>
            <table cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td>
                        <asp:HyperLink ID="hypLnkAddNew" runat="server" ImageUrl="~/images/BtnAdd.gif" NavigateUrl="javascript:void(0);"
                            ToolTip="Click here to Add a New Note!"></asp:HyperLink>
                    </td>
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td>
                        <asp:ImageButton ID="imgBtnDelete" runat="server" ImageUrl="~/images/BtnDelete.gif"
                            ToolTip="Click here to Delete All Selected Notes!" OnClick="imgBtnDelete_Click" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td height="5px">
        </td>
    </tr>
    <tr>
        <td>
            <table cellspacing="0" cellpadding="0" width="400" border="0">
                <tr>
                    <td align="left">
                        <asp:Label ID="lblCurrentIndex" runat="server" CssClass="CurrentPageIndexLabel">Page&nbsp;1&nbsp;of&nbsp;1</asp:Label></td>
                    <td align="right">
                        <asp:Label ID="Labelsss4" runat="server" CssClass="FilterNoteDesc">Click&nbsp;on&nbsp;Column&nbsp;Headings&nbsp;to&nbsp;Resort&nbsp;Data.</asp:Label></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <!--DataGrid  -->
            <cc2:SortedDataGrid ID="dtgBusinessNotificationItems" runat="server" Width="700px"
                PageSize="30" AllowPaging="True" AllowSorting="True" CellPadding="3" CssClass="GridStyle"
                BorderColor="#CCCCCC" DataSource="<%# DVNote %>" AutoGenerateColumns="False"
                ShowFooter="True">
                <PagerStyle CssClass="PagerItemStyle" Mode="NumericPages"></PagerStyle>
                <AlternatingItemStyle CssClass="AlternatingItemStyle_off"></AlternatingItemStyle>
                <FooterStyle CssClass="FooterItemStyle"></FooterStyle>
                <SelectedItemStyle CssClass="SelectedItemStyle"></SelectedItemStyle>
                <ItemStyle CssClass="ItemStyle_off"></ItemStyle>
                <HeaderStyle Wrap="False" CssClass="HeaderItemStyle" ForeColor="White"></HeaderStyle>
                <Columns>
                    <asp:TemplateColumn>
                        <headerstyle></headerstyle>
                        <itemtemplate>
							<asp:Image id ="imgNote" imageurl='<%# GetNoteImageURL(DataBinder.Eval(Container, "DataItem.business_notification_type_id").ToString(), DataBinder.Eval(Container, "DataItem.is_complete").ToString()) %>' runat="server" >
							</asp:Image>
						</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <headerstyle></headerstyle>
                        <itemtemplate>
							<asp:checkbox id ="chkSelected"  runat="server" >
							</asp:checkbox>
						</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn SortExpression="create_user_name" HeaderText="From">
                        <itemtemplate>
							<asp:Label id="lblFromName" Width="150px" Height="23px" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.create_user_name") %>' />
						</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn SortExpression="assigned_user_name" HeaderText="To">
                        <itemtemplate>
							<asp:Label id="lbAssigned" runat="server" Width="150px" Height="23px" Text='<%# DataBinder.Eval(Container, "DataItem.assigned_user_name") %>' />
						</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn SortExpression="Subject" HeaderText="Subject">
                        <itemstyle wrap="False" width="100%"></itemstyle>
                        <itemtemplate>
							<asp:HyperLink id="hypLnkSubject" runat="server" NavigateUrl="javascript:void(0);" ToolTip="Click here to open this Note!" Text='<%# DataBinder.Eval(Container, "DataItem.subject") %>' />
						</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn SortExpression="create_date" HeaderText="Date&nbsp;Received">
                        <itemtemplate>
							<asp:Label id="lbcomplete_date" runat="server" Width="90px" Height="23px" Text='<%# DataBinder.Eval(Container, "DataItem.create_date", "{0:d}") %>' />
						</itemtemplate>
                    </asp:TemplateColumn>
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
							Number of BusinessNotification(s):
                        </asp:Label></td>
                </tr>
            </table>
        </td>
    </tr>
</table>

<script language="javascript">
<!--
    function CheckedAll(IDRefCtrl) {
    
        var oCtl = window.document.getElementById(IDRefCtrl);
        
        for (i=1; i < document.forms(0).elements.length; i++){	
            if (document.forms(0).elements[i].id.match(/chkSelected/)) { 
                var oCtlItems = document.forms(0).elements[i];
                oCtlItems.checked = oCtl.checked;												
            }																
         }	
    
    }
-->
</script>
