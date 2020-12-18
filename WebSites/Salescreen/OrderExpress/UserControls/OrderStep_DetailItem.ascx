<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="uc1" TagName="OrderDetailSectionList" Src="OrderDetailSectionList.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.OrderStep_DetailItem" Codebehind="OrderStep_DetailItem.ascx.cs" %>
<table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
    <tr>
        <td align="left">
            <!--Section Title -->
            <asp:Label ID="Label4" CssClass="StandardLabel" runat="server" Font-Size="small"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="left">
            <!--Section Body -->
            <table id="Table5" cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" CssClass="StandardLabel" Visible="False">
							5 - Enter all Products to include in this order :
                        </asp:Label>
                    </td>
                    <td valign="top">
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="left">
            <!--Section Body -->
            <table id="Table4" cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td>
                        <td style="background-color: transparent">
                            <iewc:TabStrip ID="TbStrp_Form" runat="server" AutoPostBack="False" SepDefaultStyle="border-bottom:solid 1px #000000; background: transparent;"
                                TargetID="multPage_Form" TabDefaultStyle="border-bottom: #006699 2px solid; background-color: transparent;" BackColor="LightGoldenrodYellow">
                                <iewc:Tab DefaultImageUrl="images/tabForm_StandardProduct_off.gif" SelectedImageUrl="images/tabForm_StandardProduct_on.gif">
                                </iewc:Tab>
                                <iewc:TabSeparator></iewc:TabSeparator>
                                <iewc:Tab DefaultImageUrl="images/tabForm_OptionalProduct_off.gif" SelectedImageUrl="images/tabForm_OptionalProduct_on.gif">
                                </iewc:Tab>
                                <iewc:TabSeparator DefaultStyle="width:100%;background-color:transparent;"></iewc:TabSeparator>
                            </iewc:TabStrip>
                            <iewc:MultiPage ID="multPage_Form" Style="border-right: #bfbfbf 2px outset; padding-right: 5px;
                                border-top: medium none; padding-left: 5px; padding-bottom: 5px; border-left: #bfbfbf 2px outset;
                                padding-top: 5px; border-bottom: #bfbfbf 2px outset" runat="server" Height="100%"
                                Width="750px">
                                <iewc:PageView>
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td width="5px">
                                                &nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="lbl1" CssClass="NoteLabel">
                                                </asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="5px">
                                                &nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <table border=0 id="tblProfitRate" runat="server">
                                                    <tr>
                                                        <td><asp:Label ID="Label38" runat="server" CssClass="StandardLabel">Account&nbsp;Profit&nbsp;:</asp:Label></td>
                                                        <td><asp:DropDownList ID="ddlProfitRate" runat=server CssClass="StandardLabel" AutoPostBack=true></asp:DropDownList>
                                                        <asp:Label ID="Label1" runat="server" CssClass="StandardLabel"></asp:Label></td>
                                                    </tr>
                                                </table>
                                                <uc1:OrderDetailSectionList ID="OrderDetailSectionListStep" runat="server"></uc1:OrderDetailSectionList>
                                                <br>
                                            </td>
                                        </tr>
                                    </table>
                                </iewc:PageView>
                                <iewc:PageView>
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td width="5px">
                                                &nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:Label runat="server" Height="300px" ID="lblNonAvailableOptionalSectionType"
                                                    Font-Size="14px" CssClass="StandardLabel">
								                    <br><br><br>No Products are Available in this Type of Section.
                                                </asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="5px">
                                                &nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <uc1:OrderDetailSectionList ID="OrderDetailSectionListStep_Optional" runat="server">
                                                </uc1:OrderDetailSectionList>
                                                <br>
                                            </td>
                                        </tr>
                                    </table>
                                </iewc:PageView>
                            </iewc:MultiPage>
                        </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center">
            <br>
            <table cellspacing="0" cellpadding="0" border="0" id="Table2" width="100%">
			    <tr runat="server" id="trQCAPOrderIntimation">
				    <td colspan="3" align="right">
				        <asp:Label ID="lblQCAPOrderIntimation" runat="server" CssClass="NoteLabel" Text="NOTE: This order is initiated from QCAP"></asp:Label>
				        <br />
				    </td>
			    </tr>
                <tr>
                    <td>
                        <asp:ImageButton ID="imgBtnBack" runat="server" CausesValidation="False" ImageUrl="~/images/BtnBack.gif"
                            AlternateText="Click here to go back to the previous STEP"></asp:ImageButton>
                    </td>
                    <td width="100%">
                        &nbsp;
                    </td>
                    <td>
                        <asp:ImageButton ID="imgBtnNext" runat="server" CausesValidation="False" ImageUrl="~/images/btnNext.gif"
                            AlternateText="Click here to go to the next STEP" ToolTip="Click here to go to the next STEP">
                        </asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
      <tr>
        <td align="right">
            <br>
            <asp:ImageButton ID="imgBtnSkip" runat="server" CausesValidation="False" ImageUrl="~/images/btnOrderNoSupplies.gif"
                AlternateText="Click here to go to the next STEP" ToolTip="Click here to go to the next STEP"
                Visible="false"></asp:ImageButton>
        </td>
    </tr>
</table>
