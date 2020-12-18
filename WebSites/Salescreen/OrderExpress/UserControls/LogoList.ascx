<%@ Reference Control="LogoDetailInfo.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.LogoList" Codebehind="LogoList.ascx.cs" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl.DataGridControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="uc1" TagName="SearchModule" Src="~/UserControls/SearchModule.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ToolBar" Src="~/UserControls/ToolBar.ascx" %>
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
                        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0" height="20px"
                            runat="server">
                            <tr>
                                <td>
                                    <hr width="100%" color="#666666" size="1">
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 86px">
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td colspan="2" style="height: 14px">
                                                <asp:Label ID="Label6" CssClass="StandardLabel" runat="server">
													Filter&nbsp;By:
                                                </asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 30px">
                                                <table cellspacing="0" cellpadding="0" width="100%" border="0" height="20px">
                                                    <tr valign="middle" id="FMSelector" runat="server">
                                                        <td style="height: 45px">
                                                            <asp:Label ID="Label55" runat="server" CssClass="ModuleSearchText">Field&nbsp;Sales&nbsp;Manager:
                                                            </asp:Label></td>
                                                        <td colspan="6" valign="middle" style="height: 45px">
                                                            <table id="Table4" cellspacing="0" cellpadding="0" border="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtFMID" runat="server" Width="50px" Enabled="True"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:CompareValidator ID="CompValFMID" runat="server" CssClass="LabelError" ControlToValidate="txtFMID"
                                                                            ErrorMessage="The FM ID is invalid (must be a number)." Type="Integer" Operator="DataTypeCheck">*</asp:CompareValidator></td>
                                                                    <td>
                                                                        &nbsp;
                                                                        <asp:TextBox ID="txtFMName" runat="server" Width="230px" Enabled="True"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:RequiredFieldValidator ID="ReqFldVal_FMID" runat="server" CssClass="LabelError"
                                                                            Enabled="False" ControlToValidate="txtFMID" ErrorMessage="The FSM is required.">*</asp:RequiredFieldValidator></td>
                                                                    <td align="right">
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:ImageButton ID="imgBtnSelectFM" runat="server" ImageUrl="~/images/BtnSelect.gif"
                                                                                        CausesValidation="False"></asp:ImageButton></td>
                                                                                <td>
                                                                                    <img src="images/btnReset.gif" style="cursor: hand" onclick="ResetFM();"></td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr valign="top">
                                                        <td>
                                                            <asp:Label ID="Label10" runat="server" CssClass="ModuleSearchText">Status&nbsp;Category:
                                                            </asp:Label></td>
                                                        <td nowrap>
                                                            <asp:DropDownList ID="ddlDisplayStatus" runat="server">
                                                                <asp:ListItem Value="-1">--SELECT--</asp:ListItem>
                                                                <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
                                                                <asp:ListItem Value="0">Inactive</asp:ListItem>
                                                            </asp:DropDownList></td>
                                                        <td>
                                                            <asp:Label ID="LblIsNational" runat="server" CssClass="ModuleSearchText">Logo&nbsp;Type:
                                                            </asp:Label></td>
                                                        <td nowrap>
                                                            <asp:DropDownList ID="ddlLogoType" runat="server">
                                                                <asp:ListItem Selected="True" Value="-1">--SELECT--</asp:ListItem>
                                                                <asp:ListItem Value="2">Favorites</asp:ListItem>
                                                                <asp:ListItem Value="0">Personal Images</asp:ListItem>
                                                                <asp:ListItem Value="1">QSP Images</asp:ListItem>
                                                            </asp:DropDownList></td>
                                                        <td>
                                                            <asp:Label ID="Label2" runat="server" CssClass="ModuleSearchText">Logo Category:</asp:Label></td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlCategory" runat="server">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <!--
													<tr style="display:none">
														<TD id="tdFilterFMReportedTo" align="right" colSpan="2" runat="server">
															<table cellSpacing="0" cellPadding="0" border="0">
																<tr>
																	<td><asp:label id="Label3" runat="server" CssClass="ModuleSearchText">
																			&nbsp;All&nbsp;FM&nbsp;Reported&nbsp;To&nbsp;:&nbsp;
																		</asp:label></td>
																	<td><asp:checkbox id="chkReportedTo" runat="server" CssClass="boxes"></asp:checkbox></td>
																</tr>
															</table>
														</TD>
													</tr>
													<tr style="display:none">
													    <td visible=false><asp:label id="Label5" runat="server" CssClass="ModuleSearchText" style="display:none">State&nbsp;:
															</asp:label></td>
														<TD noWrap visible=false><asp:dropdownlist id="ddlSubdivision" runat="server" style="display:none"></asp:dropdownlist></TD>
														<td></td>
														<td></td>
													</tr>
													-->
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
            <br>
            <asp:HyperLink ID="hypLnkAddNew" runat="server" ImageUrl="~/images/BtnAdd.gif" NavigateUrl="javascript:void(0);"></asp:HyperLink></td>
    </tr>
    <tr>
        <td>
            <br>
            <table cellspacing="0" cellpadding="0" width="400" border="0">
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
        <td>
            <!--DataGrid  -->
            <cc2:SortedDataGrid ID="dtgLogo" runat="server" SearchMode="0" Width="700px" AllowPaging="True"
                AllowSorting="True" CellPadding="3" BackColor="White" BorderWidth="1px" BorderStyle="None"
                BorderColor="#CCCCCC" DataSource="<%# DVLogo %>" AutoGenerateColumns="False"
                ShowFooter="True" Font-Size="10pt" Criteria="" FilterExpression="" SortExpression="">
                <SelectedItemStyle CssClass="SelectedItemStyle"></SelectedItemStyle>
                <AlternatingItemStyle CssClass="AlternatingItemStyle_off"></AlternatingItemStyle>
                <FooterStyle CssClass="FooterItemStyle" Font-Size="10px"></FooterStyle>
                <ItemStyle CssClass="ItemStyle_off" Font-Size="12px"></ItemStyle>
                <HeaderStyle Wrap="False" CssClass="HeaderItemStyle" Font-Size="11px" ForeColor="White">
                </HeaderStyle>
                <Columns>
                    <asp:TEMPLATECOLUMN>
                        <itemstyle wrap="False" width="100px"></itemstyle>
                        <itemtemplate>
<ASP:IMAGEBUTTON id="imgBtnDetail" runat="server" ImageUrl="~/images/BtnDetail.gif" CommandName="Select" CausesValidation="False" Width="100" __designer:wfdid="w1"></ASP:IMAGEBUTTON> 
</itemtemplate>
                    </asp:TEMPLATECOLUMN>
                    <asp:TEMPLATECOLUMN HeaderText="ID" SortExpression="logo_id">
                        <itemtemplate>
							<asp:Label id=lblID CssClass=lblID runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.logo_id") %>'>
							</asp:Label>
						
</itemtemplate>
                    </asp:TEMPLATECOLUMN>
                    <asp:TEMPLATECOLUMN HeaderText="Name" SortExpression="logo_name">
                        <itemtemplate>
							<asp:Label id=Name CssClass=lblName runat="server" Text='<%# ((String)DataBinder.Eval(Container, "DataItem.logo_name")).Replace(" ","&nbsp;") %>'>
							</asp:Label>
						
</itemtemplate>
                    </asp:TEMPLATECOLUMN>
                    <asp:TEMPLATECOLUMN HeaderText="Description" SortExpression="Description">
                        <itemtemplate>
							<asp:Label id=Description CssClass=lblName runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.description") %>'>
							</asp:Label>
						
</itemtemplate>
                    </asp:TEMPLATECOLUMN>
                    <asp:TEMPLATECOLUMN HeaderText="" visible="False">
                        <itemtemplate>
							<CENTER>
								<asp:Label id=lblFMID runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FM_ID")%>'>
								</asp:Label>
								<<asp:Label id="lblFMName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.last_name") + (DataBinder.Eval(Container, "DataItem.last_name").ToString().Length > 0 ? ", " : String.Empty) + DataBinder.Eval(Container, "DataItem.first_name") %>'>
							</asp:Label>
								</CENTER>
						
</itemtemplate>
                    </asp:TEMPLATECOLUMN>
                    <asp:TEMPLATECOLUMN HeaderText="">
                        <itemtemplate>
							<CENTER>
								<asp:Label id=lblLogoInformation  CssClass="lblInformation" runat="server" Text=''>
								</asp:Label></CENTER>
						
</itemtemplate>
                    </asp:TEMPLATECOLUMN>
                    <asp:TEMPLATECOLUMN HeaderText="Deleted" visible="False">
                        <itemtemplate>
							<asp:CheckBox id=chkArchived Runat="server" Checked='<%# DataBinder.Eval(Container, "DataItem.deleted")%>'>
							</asp:CheckBox>
						
</itemtemplate>
                        <headerstyle width="10px"></headerstyle>
                    </asp:TEMPLATECOLUMN>
                    <asp:TEMPLATECOLUMN>
                        <itemstyle width="50px"></itemstyle>
                        <itemtemplate>
						            <center>
                                    <ASP:IMAGEBUTTON id="imgBtnAddToFavorite" runat="server" ImageUrl="~/images/BtnAddToFavorite.gif" CommandName="AddToFavorite" CausesValidation="False" __designer:wfdid="w2"></ASP:IMAGEBUTTON> 
                                    <ASP:IMAGEBUTTON id="imgBtnRemoveFromFavorite" runat="server" ImageUrl="~/images/BtnRemoveFromFavorite.gif" CommandName="RemoveFromFavorite" CausesValidation="False" __designer:wfdid="w3"></ASP:IMAGEBUTTON>
                                    <asp:Label id="lblDefaultFavorite" runat="server" Text='From&nbsp;QSP&nbsp;Favorites'></asp:Label>
                                    </center>
</itemtemplate>
                    </asp:TEMPLATECOLUMN>
                </Columns>
                <PagerStyle CssClass="PagerItemStyle" Mode="NumericPages"></PagerStyle>
            </cc2:SortedDataGrid></td>
    </tr>
    <tr>
        <td>
            <table cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td>
                        <br>
                        <asp:Label ID="lblTotal" runat="server" CssClass="TotalListLabel">
							Number of Logo(s):
                        </asp:Label></td>
                </tr>
            </table>
        </td>
    </tr>
</table>

<script>
function ResetFM()
{
	var fmID = document.getElementById('<%=this.txtFMID.ClientID%>');
	var fmName = document.getElementById('<%=this.txtFMName.ClientID%>');
	fmID.value = "";
	fmName.value = "";
}
</script>
