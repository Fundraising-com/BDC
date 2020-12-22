<%@ Reference Control="AccountDetailInfo.ascx" %>
<%@ Reference Control="~/UserControls/SearchModule.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.AccountTransferControl" Codebehind="AccountTransferControl.ascx.cs" %>
<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl.DataGridControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="uc1" TagName="SearchModule" Src="~/UserControls/SearchModule.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<table id="Table5" cellspacing="0" cellpadding="0" align="left" border="0">
    <tr id="dkfjks" visible="false" runat="server">
        <td>
            <asp:Label ID="abclabel" runat="server" Text="Account Name"></asp:Label></td>
    </tr>
    <tr>
        <td>
            <table cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td valign="top" style="width: 614px">
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
																Filter By:
                                                            </asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                <tr>
                                                                    <td>
                                                                        <table cellspacing="0" cellpadding="0" border="0">
                                                                            <tr>
                                                                                <td width="100px" nowrap>
                                                                                    <asp:Label ID="lblState" runat="server" CssClass="ModuleSearchText">
																						State:&nbsp;
                                                                                    </asp:Label></td>
                                                                                <td align="left">
                                                                                    <asp:DropDownList ID="ddlState" runat="server" CssClass="boxes">
                                                                                    </asp:DropDownList></td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td id="qspprogram" runat="server" visible="false">
                                                                        <table cellspacing="0" cellpadding="0" border="0">
                                                                            <tr>
                                                                                <td width="100px" nowrap>
                                                                                    <asp:Label ID="Label1" runat="server" CssClass="ModuleSearchText">
																						QSP&nbsp;Program:&nbsp;
                                                                                    </asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlProgramType" runat="server" CssClass="boxes">
                                                                                    </asp:DropDownList></td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td runat="server" visible="false">
                                                                    </td>
                                                                </tr>
                                                                <tr id="trFieldSupportFilterOption" runat="server">
                                                                    <td>
                                                                        <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                                                            <tr>
                                                                                <td>
                                                                                    <table cellspacing="0" cellpadding="0" border="0">
                                                                                        <tr>
                                                                                            <td width="100px" nowrap>
                                                                                                <asp:Label ID="Label6" runat="server" CssClass="ModuleSearchText">
																						            FSM&nbsp;ID:&nbsp;
                                                                                                </asp:Label></td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtFSMID" runat="server" Width="75px" MaxLength="4" CssClass="StandardTextBox"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td>
                                                                        <table cellspacing="0" cellpadding="0" border="0" id="FMNameTable" visible="false" runat="server">
                                                                            <tr>
                                                                                <td align="left">
                                                                                    <table cellspacing="0" cellpadding="0" border="0">
                                                                                        <tr>
                                                                                            <td width="100px" nowrap>
                                                                                                <asp:Label ID="Label4" runat="server" CssClass="ModuleSearchText">
																				                    FSM&nbsp;Name:&nbsp;
                                                                                                </asp:Label></td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtFSMName" runat="server" Width="200px" MaxLength="100" CssClass="StandardTextBox"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td>
                                                                    <table cellspacing="0" cellpadding="0" border="0" visible="false" id="FmTable" runat="server">
                                                                            <tr>
                                                                                <td align="left">
                                                                                   <asp:ImageButton ID="SelectFMButton" runat="server" ImageUrl="~/images/BtnSelect.gif" />
                                                                                </td>
                                                                                <td align="left">
                                                                                   <asp:ImageButton ID="ClearFMButton" runat="server" ImageUrl="~/images/btnClear.gif" CausesValidation="false" OnClientClick= "ClearFM();" />
                                                                                </td>
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
                    <td>
                    </td>
                </tr>
                <tr>
                    <td height="5px" style="width: 614px">
                    </td>
                </tr>
            </table>
            <table cellspacing="0" cellpadding="0" width="700" border="0">
                <tr>
                    <td style="width: 155px; height: 24px;">
                        <asp:Label ID="Label5" runat="server" Text="To FM" CssClass="StandardLabel"></asp:Label></td>
                    <td style="width: 400px; height: 24px;">
                        <asp:TextBox ID="txtFMID1" runat="server" CssClass="StandardTextBox" Width="50px"></asp:TextBox>
                        <asp:TextBox ID="txtFMName1" runat="server" CssClass="StandardTextBox" Width="230px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFMID1"
                            CssClass="LabelError" ErrorMessage="The FSM is required.">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtFMID1"
                            CssClass="LabelError" ErrorMessage="The FM ID is invalid (must be a number)."
                            Operator="DataTypeCheck" Type="Integer">*</asp:CompareValidator>
                    </td>
                    <td style="width: 159px; height: 24px;">
                        <asp:ImageButton ID="imgBtnSelectFM1" runat="server" ImageUrl="~/images/BtnSelect.gif"
                            CausesValidation="false" /></td>
                </tr>
                <tr>
                    <td style="width: 155px">
                        <asp:Label ID="Label11" runat="server" Text="Sales To FM" CssClass="StandardLabel"></asp:Label></td>
                    <td style="width: 400px">
                        <asp:TextBox ID="txtFMID2" runat="server" CssClass="StandardTextBox" Width="50px"></asp:TextBox>
                        <asp:TextBox ID="txtFMName2" runat="server" CssClass="StandardTextBox" Width="230px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtFMID2"
                            CssClass="LabelError" ErrorMessage="The FSM is required.">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtFMID2"
                            CssClass="LabelError" ErrorMessage="The FM ID is invalid (must be a number)."
                            Operator="DataTypeCheck" Type="Integer">*</asp:CompareValidator>
                    </td>
                    <td style="width: 159px">
                        <asp:ImageButton ID="imgBtnSelectFM2" runat="server" ImageUrl="~/images/BtnSelect.gif" /></td>
                </tr>
                <tr>
                    <td colspan="3">
                        <hr width="87%" color="#666666" size="1">
                    </td>
                </tr>
                <tr>
                    <td style="width: 155px; height: 36px;">
                        <asp:Label ID="Label12" runat="server" Text="Effective Date" CssClass="StandardLabel"></asp:Label></td>
                    <td style="width: 323px; height: 36px;" colspan="2">
                        <asp:TextBox ID="txtDate" runat="server" CssClass="StandardTextBox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtDate"
                            CssClass="LabelError" ErrorMessage="Please Enter the Date">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="txtDate"
                            CssClass="LabelError" ErrorMessage="Please Enter a Valid Date" Operator="DataTypeCheck"
                            Type="Date">*</asp:CompareValidator>
                        <asp:ImageButton ID="Image1" runat="Server" AlternateText="Click to show calendar"
                            CausesValidation="false" ImageUrl="~/images/Calendar.gif" /><br />
                        <ajaxToolkit:CalendarExtender ID="calendarButtonExtender" runat="server" PopupButtonID="Image1"
                            TargetControlID="txtDate">
                        </ajaxToolkit:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td style="width: 155px">
                        <asp:Label ID="Label13" runat="server" Text="Reason" CssClass="StandardLabel"></asp:Label></td>
                    <td style="width: 323px" colspan="2">
                        <asp:TextBox ID="txtReason" runat="server" CssClass="StandardTextBox" TextMode="MultiLine"
                            Width="297px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtReason"
                            CssClass="LabelError" ErrorMessage="Please Enter the Reason">*</asp:RequiredFieldValidator><br />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <hr width="87%" color="#666666" size="1">
                    </td>
                </tr>
            </table>
            <table cellspacing="0" cellpadding="0" width="400" border="0">
                <tr>
                    <td align="left" colspan="2">
                        <asp:ImageButton ID="btnTransferAccount" runat="server" AlternateText="Click here to Transfer Accounts !"
                            ImageUrl="~/images/TransferAccounts.gif" OnClick="btnTransferAccount_Click"></asp:ImageButton><br>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <hr width="100%" color="#666666" size="1">
                    </td>
                </tr>
                <tr>
                    <td height="5px" colspan="2">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="height: 47px">
                        <asp:ImageButton ID="CheckAllbtn" runat="server" AlternateText="Click here to UnCheck All Accounts !"
                            CausesValidation="false" ImageUrl="~/images/btncheckAll.gif" OnClick="btnCheckAll_Click">
                        </asp:ImageButton><br>
                    </td>
                    <td align="center" style="height: 47px">
                        <asp:ImageButton ID="UnCheckAllbtn" runat="server" AlternateText="Click here Check All Accounts !"
                            CausesValidation="false" ImageUrl="~/images/btnuncheckAll.gif" OnClick="btnUnCheckAll_Click">
                        </asp:ImageButton><br>
                    </td>
                </tr>
                <tr>
                    <td height="5px" colspan="2">
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lblCurrentIndex" runat="server" CssClass="CurrentPageIndexLabel">Page&nbsp;1&nbsp;of&nbsp;1</asp:Label></td>
                    <td align="right" style="width: 459px">
                        <asp:Label ID="Labelsss4" runat="server" CssClass="FilterNoteDesc">Click&nbsp;on&nbsp;Column&nbsp;Headings&nbsp;to&nbsp;Resort&nbsp;Data.&nbsp;</asp:Label></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td width="100%">
            <!--DataGrid  -->
            <cc2:SortedDataGrid ID="dtgAccount" runat="server" SearchMode="0" ShowFooter="True"
                DataSource="<%# DVAccount %>" AutoGenerateColumns="False" CssClass="GridStyle"
                BorderColor="#CCCCCC" CellPadding="3" AllowSorting="True" AllowPaging="True"
                Width="100%" PageSize="30" DataKeyField="fulf_account_id">
                <PagerStyle CssClass="PagerItemStyle" Mode="NumericPages"></PagerStyle>
                <AlternatingItemStyle CssClass="AlternatingItemStyle_off" Wrap="False"></AlternatingItemStyle>
                <FooterStyle CssClass="FooterItemStyle" Font-Size="10px" Wrap="False"></FooterStyle>
                <SelectedItemStyle CssClass="SelectedItemStyle" Wrap="False"></SelectedItemStyle>
                <ItemStyle CssClass="ItemStyle_off" Font-Size="11px" Wrap="False"></ItemStyle>
                <HeaderStyle Wrap="False" CssClass="HeaderItemStyle" ForeColor="White"></HeaderStyle>
                <Columns>
                    <asp:TemplateColumn SortExpression="organization_name" HeaderText="Organization&#160;Name">
                        <itemtemplate>
							<asp:HyperLink id="hypLnkOrgName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.organization_name") %>' ForeColor="#336699">
							</asp:HyperLink>
						
</itemtemplate>
                        <headerstyle wrap="False" width="250px"></headerstyle>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn SortExpression="account_id" HeaderText="QSP&#160;Acct&#160;ID&#160;#">
                        <itemtemplate>
							<asp:Label id="Label8" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.account_id")%>'>
							</asp:Label>
						
</itemtemplate>
                        <headerstyle width="50px"></headerstyle>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn SortExpression="fulf_account_id" HeaderText="EDS&#160;Acct&#160;#">
                        <itemtemplate>
							<asp:Label id="lblAccountNumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.fulf_account_id", "{0:D9}")%>'>
							</asp:Label>
						
</itemtemplate>
                        <headerstyle width="50px"></headerstyle>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn SortExpression="account_name" HeaderText="Account&#160;Name">
                        <itemtemplate>
							<asp:HyperLink id="hypLnkName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.account_name") %>' ForeColor="#336699">
							</asp:HyperLink>
						
</itemtemplate>
                        <headerstyle wrap="False" width="250px"></headerstyle>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn SortExpression="fm_id" HeaderText="FSM&#160;ID&#160;#">
                        <itemstyle wrap="False" width="50px"></itemstyle>
                        <itemtemplate>
							<asp:Label id=lblFMID runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.fm_id") %>'>
							</asp:Label>
						
</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn SortExpression="fmname" HeaderText="FSM&#160;Name">
                        <itemstyle wrap="False" width="150px"></itemstyle>
                        <itemtemplate>
							<asp:Label id=lblFMName runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.fmname") %>'>
							</asp:Label>
						
</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="city" SortExpression="city" ReadOnly="True" HeaderText="City">
                        <itemstyle wrap="False" width="130px"></itemstyle>
                    </asp:BoundColumn>
                    <asp:TemplateColumn SortExpression="State" HeaderText="ST">
                        <itemstyle wrap="False" width="40px"></itemstyle>
                        <itemtemplate>
							<asp:Label id="Label7" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.State").ToString().Replace("US-","") %>'>
							</asp:Label>
						
</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn SortExpression="Zip" HeaderText="Zip">
                        <itemstyle wrap="False" width="90px"></itemstyle>
                        <itemtemplate>
							<asp:Label id=lblZip runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Zip") %>'>
							</asp:Label>
						
</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Select&#160;Account">
                        <itemstyle horizontalalign="Center" wrap="False" width="60px" />
                        <itemtemplate>
                            <asp:CheckBox ID="AccountCheckBox" runat="server" OnClick="javascript: AccountCounter(this.checked);"  />
                        
</itemtemplate>
                    </asp:TemplateColumn>
                </Columns>
                <EditItemStyle Wrap="False" />
            </cc2:SortedDataGrid></td>
    </tr>
    <tr>
        <td>
            <table cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td>
                        <br>
                        <asp:Label ID="lblTotal" runat="server" CssClass="TotalListLabel">
							Number of Account(s):
                        </asp:Label></td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<input id="hidbox" type="hidden" value="0" name="hidbox" runat="server">

<script type="text/javascript">
  function AccountNumber()
   {
   var ans1;
   var ans = false;	
   if (document.getElementById('<%=this.hidbox.ClientID%>') != null)
    {
    ans1 = document.getElementById('<%=this.hidbox.ClientID%>').value;
    if(ans1 == "0")
    alert("You have No Accounts Selected");
    else
    {
     ans = window.confirm('Are you sure you want to transfer  ' + ans1 + ' Accounts?');
    }
    return ans;
   
    }
	        
   }
function AccountCounter(value)
{
if(value == true)
{
var count = document.getElementById('<%=this.hidbox.ClientID%>').value;
count++;
document.getElementById('<%=this.hidbox.ClientID%>').value = count;
}
else
{
var count = document.getElementById('<%=this.hidbox.ClientID%>').value;
count--;
document.getElementById('<%=this.hidbox.ClientID%>').value = count;
}
}

function ClearFM()
{
document.getElementById('<%=this.txtFSMID.ClientID%>').value = "";
document.getElementById('<%=this.txtFSMName.ClientID%>').value = "";
}

</script>
