<%@ Page Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true"
    SmartNavigation="False" Inherits="QSP.OrderExpress.Web.OrderForm_Step" Codebehind="OrderForm_Step.aspx.cs" %>
<%@ MasterType VirtualPath="~/MainMaster.master" %>
<%@ Register TagPrefix="uc2" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentHolder" runat="Server">
    <table cellpadding="4" width="100%">
        <tr>
            <td align="left">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr id="trHeaderDisplay" runat="server" visible="true">
                        <td colspan="2">
                            <asp:Label ID="lblSectionTitle" runat="server" CssClass="SectionTitleLabel">Section Title :</asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="lblPageTitle" runat="server" CssClass="PageTitleLabel">Page Title </asp:Label>
                        </td>
                    </tr>
                    <tr id="trDirectionsTitle" runat="server" visible="true" style="height: 2px;">
                        <td colspan="2" style="height: 2px;">
                            <asp:Label ID="lblDirection" runat="server" CssClass="DirectionTitleLabel">Directions :</asp:Label>
                        </td>
                    </tr>
                    <tr id="trDirectionDesc" runat="server" visible="true" colspan="2" style="height: 30px;">
                        <td>
                            <asp:Label ID="lblInstruction" runat="server" CssClass="DirectionLabel" Width="800px"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td>
                            <asp:ValidationSummary ID="valSum" runat="server" HeaderText="Correct the following error to proceed."
                                CssClass="LabelError" Visible="False"></asp:ValidationSummary>
                            <asp:Label ID="lblMessage" runat="server" CssClass="LabelError" ForeColor="Red"></asp:Label></td>
                    </tr>
                    <tr id="trCampInfoTitle" runat="server">
                        <td align="left">
                            <!--Section Body -->
                            <br>
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:Image ID="imgBusinessForm" Height="80px" runat="server" />
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <table id="tblCampInfoTitle" cellspacing="0" cellpadding="0" border="0">
                                            <tr id="trAccountInfoTitle" runat="server">
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" CssClass="FormTitleLabel"> Account :
                                                    </asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblAccountNumber" runat="server" CssClass="FormTitleDescLabel">
																										            00000
                                                    </asp:Label></td>
                                                <td>
                                                    &nbsp;-&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblAccountName" runat="server" CssClass="FormTitleDescLabel">
																										            Account Name
                                                    </asp:Label></td>
                                            </tr>
                                            <tr id="trFormInfoTitle" runat="server">
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" CssClass="FormTitleLabel"> Order Form :
                                                    </asp:Label></td>
                                                <td align="right">
                                                    <asp:Label ID="lblFormID" runat="server" CssClass="FormTitleDescLabel">
																										            23
                                                    </asp:Label></td>
                                                <td>
                                                    &nbsp;-&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblFormName" runat="server" CssClass="FormTitleDescLabel">
																										            WFC WarehouseStock Order Form
                                                    </asp:Label></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:PlaceHolder ID="plHoldBodyPage" runat="server"></asp:PlaceHolder>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
