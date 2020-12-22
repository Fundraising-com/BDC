<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.ProgramAgreementStep_Selection"
    CodeBehind="ProgramAgreementStep_Selection.ascx.cs" %>
<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl.DataGridControl" Assembly="QSP.WebControl" %>
<table id="Table1222" cellspacing="0" cellpadding="0" width="100%" border="0">
    <tr id="trCampInfoTitle" runat="server">
        <td align="left">
            <!--Section Body -->
            <br>
            <table id="tblCampInfoTitle" cellspacing="0" cellpadding="0" border="0">
                <tr id="trAccountInfoTitle" runat="server">
                    <td>
                        <asp:Label ID="Label1" runat="server" CssClass="FormTitleLabel">
							Account&nbsp;:&nbsp;
                        </asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblAccountNumber" runat="server" CssClass="FormTitleDescLabel" ForeColor="#993300">
							00000
                        </asp:Label>
                    </td>
                    <td>
                        &nbsp;-&nbsp;
                    </td>
                    <td>
                        <asp:Label ID="lblAccountName" runat="server" CssClass="FormTitleDescLabel" ForeColor="#993300">
							Account Name
                        </asp:Label>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="left">
            <!--Section Body -->
            <table id="Table5" cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" CssClass="StandardLabel" Visible="False">
							3 - Select the Program Agreement Form you want to use :
                        </asp:Label>&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <cc2:SortedDataGrid ID="dtgForm" runat="server" PageSize="30" AllowPaging="True"
                AllowSorting="True" DataKeyField="formid" CellPadding="3" BorderColor="#CCCCCC"
                AutoGenerateColumns="False" ShowFooter="True" CssClass="GridStyle">
                <PagerStyle CssClass="PagerItemStyle" Mode="NumericPages"></PagerStyle>
                <AlternatingItemStyle CssClass="AlternatingItemStyle_off"></AlternatingItemStyle>
                <FooterStyle CssClass="FooterItemStyle"></FooterStyle>
                <SelectedItemStyle CssClass="SelectedItemStyle"></SelectedItemStyle>
                <ItemStyle CssClass="ItemStyle_off"></ItemStyle>
                <HeaderStyle Wrap="False" CssClass="HeaderItemStyle" ForeColor="White"></HeaderStyle>
                <Columns>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                            <asp:ImageButton ID="imgBtnDetail" runat="server" ImageUrl="~/images/BtnAddPA.gif"
                                CommandName="Select" CausesValidation="False"></asp:ImageButton>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="formid" SortExpression="formid" ReadOnly="True" HeaderText="ID">
                        <ItemStyle Wrap="False" Width="100px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:TemplateColumn SortExpression="formname" HeaderText="Program Agreement Form">
                        <ItemStyle Wrap="False" Width="350px"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkBtnForm" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.formname") %>'
                                CommandArgument='<%# DataBinder.Eval(Container, "DataItem.FormID") %>' CommandName="Select">
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                            <asp:Image ID="imgLogo" Height="80px" runat="server" ImageUrl='<%# "~/" + DataBinder.Eval(Container, "DataItem.imageurl") %>'>
                            </asp:Image>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </cc2:SortedDataGrid><br>
            <br>
            <br>
            <br>
        </td>
    </tr>
    <tr>
        <td align="center">
            <br>
            <table cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td>
                        <asp:ImageButton ID="imgBtnBack" runat="server" CausesValidation="False" AlternateText="Click here to go back to the previous STEP"
                            ImageUrl="~/images/btnBack.gif"></asp:ImageButton>
                    </td>
                    <td width="100%">
                        &nbsp;
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
