<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.OrderList" Codebehind="OrderList.ascx.cs" %>
<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl.DataGridControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="uc1" TagName="SearchModule" Src="~/UserControls/SearchModule.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<table id="Table5" cellspacing="0" cellpadding="0" align="left" border="0">
    <tr>
        <td>
            <table cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td>
                        <uc1:SearchModule ID="QSPFormSearchModule" CausesValidation="True" runat="server"
                            MaxLengthValidate="0"></uc1:SearchModule>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0" runat="server">
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
                                                <asp:Label ID="Label6" runat="server" CssClass="StandardLabel">
													Filter&nbsp;By:
                                                </asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label5" runat="server" CssClass="ModuleSearchText">
																QSP&nbsp;Program:
                                                            </asp:Label>
                                                        </td>
                                                        <td nowrap>
                                                            <asp:DropDownList ID="ddlProgramType" runat="server">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="LblState" runat="server" CssClass="ModuleSearchText">
																State :
                                                            </asp:Label>
                                                        </td>
                                                        <td nowrap>
                                                            <asp:DropDownList ID="ddlState" runat="server">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label10" runat="server" CssClass="ModuleSearchText">
																Status&nbsp;Category:
                                                            </asp:Label>
                                                        </td>
                                                        <td nowrap>
                                                            <asp:DropDownList ID="ddlStatusCategory" runat="server">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td nowrap>
                                                            <asp:TextBox ID="MinimumStartDateTextBox" runat="server" Font-Names="Verdana, Arial, Tahoma" Font-Size="9px"
                                                                Height="14px" Width="100px" Visible="false"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label7" runat="server" CssClass="ModuleSearchText">
																Start&nbsp;Date&nbsp;:
                                                            </asp:Label>
                                                        </td>
                                                        <td>
                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtStartDate" runat="server" Font-Size="9px" Height="14px" Font-Names="Verdana, Arial, Tahoma"
                                                                            Width="100px"></asp:TextBox>
                                                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtStartDate"
                                                                            Mask="99/99/9999" MessageValidatorTip="false" MaskType="Date" DisplayMoney="Left"
                                                                            AcceptNegative="Left" CultureName="en-US" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:HyperLink ID="hypLnkStartDate" runat="server" NavigateUrl="javascript:void(0);"
                                                                            ImageUrl="~/images/Calendar.gif" ToolTip="Click here to select the start date from a popup calendar !">HyperLink</asp:HyperLink>&nbsp;
                                                                    </td>
                                                                    <td>
                                                                        <asp:RequiredFieldValidator ID="reqFldVal_StartDate" CssClass="LabelError" runat="server"
                                                                            ErrorMessage="The Start Date is required." ControlToValidate="txtStartDate">*</asp:RequiredFieldValidator>
                                                                        <asp:CompareValidator ID="compVal_StartDate" runat="server" CssClass="LabelError"
                                                                            ErrorMessage="The Start Date is invalid." ControlToValidate="txtStartDate" Operator="DataTypeCheck"
                                                                            Type="Date">*</asp:CompareValidator>
                                                                        <asp:CompareValidator ID="Comparevalidator1" runat="server" CssClass="LabelError"
                                                                            ErrorMessage="The Start Date must be less or equal than the End Date." ControlToValidate="txtStartDate"
                                                                            Operator="LessThanEqual" Type="Date" ControlToCompare="txtEndDate">*</asp:CompareValidator>
                                                                            <asp:CompareValidator ID="Comparevalidator2" runat="server" CssClass="LabelError"
                                                                            ErrorMessage="Orders prior to July 1, 2007 are not available in Order Express." ControlToValidate="txtStartDate"
                                                                            Operator="GreaterThanEqual" Type="Date" ControlToCompare="MinimumStartDateTextBox">*</asp:CompareValidator>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label8" runat="server" CssClass="ModuleSearchText">
																End&nbsp;Date&nbsp;:
                                                            </asp:Label>
                                                        </td>
                                                        <td>
                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtEndDate" runat="server" Font-Size="9px" Height="14px" Font-Names="Verdana, Arial, Tahoma"
                                                                            Width="100px"></asp:TextBox>
                                                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtEndDate"
                                                                            Mask="99/99/9999" MessageValidatorTip="false" MaskType="Date" DisplayMoney="Left"
                                                                            AcceptNegative="Left" CultureName="en-US" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:HyperLink ID="hypLnkEndDate" runat="server" NavigateUrl="javascript:void(0);"
                                                                            ImageUrl="~/images/Calendar.gif" ToolTip="Click here to select the end date from a popup calendar !">HyperLink</asp:HyperLink>&nbsp;
                                                                    </td>
                                                                    <td>
                                                                        <asp:RequiredFieldValidator ID="reqFldVal_EndDate" CssClass="LabelError" runat="server"
                                                                            ErrorMessage="The End Date is required." ControlToValidate="txtEndDate">*</asp:RequiredFieldValidator>
                                                                        <asp:CompareValidator ID="compVal_EndDate" runat="server" CssClass="LabelError" ErrorMessage="The Start Date is invalid."
                                                                            ControlToValidate="txtEndDate" Operator="DataTypeCheck" Type="Date">*</asp:CompareValidator>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr id="trFieldSupportFilterOption" runat="server">
                                                        <td>
                                                            <asp:Label ID="Label11" runat="server" CssClass="ModuleSearchText">
												                FSM&nbsp;ID:&nbsp;
                                                            </asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtFSMID" runat="server" Width="100px" MaxLength="4" CssClass="StandardTextBox"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;</td>
                                                        <td>
                                                            <asp:Label ID="Label12" runat="server" CssClass="ModuleSearchText">
										                        FSM&nbsp;Name:&nbsp;
                                                            </asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtFSMName" runat="server" Width="200px" MaxLength="100" CssClass="StandardTextBox"></asp:TextBox>
                                                        </td>
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
            <table cellspacing="0" cellpadding="0" width="600" border="0">
                <tr>
                    <td align="left" valign="top">
                        <asp:ImageButton ID="imgbtnAddOrder" runat="server" AlternateText="Click here to Add new Order !"
                            ImageUrl="~/images/btnAddOrder.gif"></asp:ImageButton>
                    </td>
                    <td>
                    </td>
                    <td align="right">
                        <asp:ImageButton ID="imgBtnPrint" runat="server" ImageUrl="~/images/btnPrint.jpg"
                            AlternateText="Click here to have a printable version of the order." Visible="False">
                        </asp:ImageButton><br>
                        <br>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lblCurrentIndex" runat="server" CssClass="CurrentPageIndexLabel">Page&nbsp;1&nbsp;of&nbsp;1&nbsp;</asp:Label></td>
                    <td align="right">
                        <asp:Label ID="Labelsss4" runat="server" CssClass="FilterNoteDesc">Click&nbsp;on&nbsp;Column&nbsp;Headings&nbsp;to&nbsp;Resort&nbsp;Data.&nbsp;</asp:Label></td>
                    <td>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <!--DataGrid  -->
            <cc2:SortedDataGrid ID="dtgOrder" runat="server" ShowFooter="True" DataSource="<%# DVOrders %>"
                AutoGenerateColumns="False" BorderColor="#CCCCCC" CssClass="Gridstyle" CellPadding="3"
                AllowSorting="True" AllowPaging="True" PageSize="30" Criteria="" FilterExpression=""
                SearchMode="0" SortExpression="">
                <PagerStyle CssClass="PagerItemStyle" Mode="NumericPages"></PagerStyle>
                <AlternatingItemStyle CssClass="AlternatingItemStyle_off"></AlternatingItemStyle>
                <FooterStyle CssClass="FooterItemStyle" Font-Size="10px"></FooterStyle>
                <SelectedItemStyle CssClass="SelectedItemStyle"></SelectedItemStyle>
                <ItemStyle CssClass="ItemStyle_off" Font-Size="11px"></ItemStyle>
                <HeaderStyle Wrap="False" CssClass="HeaderItemStyle" Font-Size="11px" ForeColor="White">
                </HeaderStyle>
                <Columns>
                    <asp:TemplateColumn>
                        <itemtemplate>
							<ASP:IMAGEBUTTON id="imgBtnDetail" runat="server" height=15px ImageUrl="~/images/BtnDetail.gif" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Order_ID") %>' CommandName="Select" CausesValidation="False">
							</ASP:IMAGEBUTTON>
						
</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <itemtemplate>
							<asp:Label id="lblStatusRead" runat="server" BackColor='<%# System.Drawing.Color.FromName(DataBinder.Eval(Container, "DataItem.Color_Code").ToString()) %>' BorderWidth="1px" BorderStyle="Solid" BorderColor="black" CssClass="StatusLabel">
								&nbsp;&nbsp;
							</asp:Label>
						
</itemtemplate>
                        <headerstyle width="10px" horizontalalign="Center"></headerstyle>
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="order_status_short_description" SortExpression="order_status_short_description"
                        ReadOnly="True" HeaderText="Status&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;">
                        <headerstyle width="100px" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="order_id" SortExpression="order_id" ReadOnly="True" HeaderText="QSP&#160;Order&#160;ID">
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="fulf_order_id" SortExpression="fulf_order_id" ReadOnly="True"
                        HeaderText="EDS&#160;Order&#160;#"></asp:BoundColumn>
                    <asp:BoundColumn DataField="order_type_name" SortExpression="order_type_name" ReadOnly="True" HeaderText="Order&#160;Type" ItemStyle-Wrap="false">
                        
                    </asp:BoundColumn>
                    <asp:TemplateColumn SortExpression="order_date" HeaderText="Order&#160;Date">
                        <itemtemplate>
							<asp:Label id="Label1" runat="server" width="100px" Text='<%# DataBinder.Eval(Container, "DataItem.order_date", "{0:MM/dd/yyyy}") %>'>
							</asp:Label>
						
</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn SortExpression="account_id" HeaderText="QSP&#160;Acct&#160;ID&#160;#">
                        <itemtemplate>
							<asp:Label id="Label3" width="50px" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.account_id")%>'>
							</asp:Label>
						
</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn SortExpression="fulf_account_id" HeaderText="EDS&#160;Acct&#160;#">
                        <itemtemplate>
							<asp:Label id="lblAccountNumber" width="50px" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.fulf_account_id", "{0:D9}")%>'>
							</asp:Label>
						
</itemtemplate>
                        <headerstyle></headerstyle>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn SortExpression="Campaign_Name" HeaderText="Account&#160;Name">
                        <itemstyle wrap="False"></itemstyle>
                        <itemtemplate>
							<asp:HyperLink id="hypLnkName" Width="250px" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.campaign_name") %>' ForeColor="#336699" NavigateUrl="javascript:void(0);">
							</asp:HyperLink>
						
</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="fm_id" SortExpression="fm_id" ReadOnly="True" HeaderText="FSM&#160;ID&#160;#">
                    </asp:BoundColumn>
                    <asp:TemplateColumn SortExpression="FM_Name" HeaderText="FSM&#160;Name">
                        <itemtemplate>
							<asp:Label id="Label2" width="150px" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FM_Name") %>'>
							</asp:Label>
						
</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn SortExpression="program_type_name" HeaderText="QSP&#160;Program">
                        <itemstyle wrap="False" width="100px"></itemstyle>
                        <itemtemplate>
							<asp:Label id="Label9" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.program_type_name").ToString().Replace(" ", "&nbsp;") %>'>
							</asp:Label>
						
</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="city" SortExpression="city" ReadOnly="True" HeaderText="City&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;">
                    </asp:BoundColumn>
                    <asp:TemplateColumn SortExpression="subdivision_code" HeaderText="ST">
                        <itemtemplate>
							<asp:Label id="Label4" runat="server" width="40px" Text='<%# DataBinder.Eval(Container, "DataItem.subdivision_code").ToString().Replace("US-","") %>'>
							</asp:Label>
						
</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn SortExpression="Zip" HeaderText="Zip">
                        <itemtemplate>
							<asp:Label id="lblZip" runat="server" width="40px" Text='<%# DataBinder.Eval(Container, "DataItem.Zip")%>'>
							</asp:Label>
						
</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn SortExpression="create_last_name, create_first_name" HeaderText="Create&#160;By">
                        <itemtemplate>
							<asp:Label id="lblCreatorName" runat="server" Text=''>
							</asp:Label>
						
</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn SortExpression="create_date" HeaderText="Create&#160;At">
                        <itemtemplate>
							<asp:Label id="lblCreateDate" width="140px"  runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.create_date", "{0:MM/dd/yyyy hh:mm}") %>'>
							</asp:Label>
						
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
							Number of Order(s):
                        </asp:Label></td>
                </tr>
            </table>
        </td>
    </tr>
</table>
