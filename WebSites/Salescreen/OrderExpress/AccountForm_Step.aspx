<%@ Page Language="C#" MasterPageFile="~/MainMaster.master" SmartNavigation="False"
    AutoEventWireup="true" Inherits="QSP.OrderExpress.Web.AccountForm_Step" Codebehind="AccountForm_Step.aspx.cs" %>

<%@ MasterType VirtualPath="~/MainMaster.master" %>
<%@ Register TagPrefix="uc1" TagName="AccountStep_Information" Src="~/UserControls/AccountStep_Information.ascx" %>
<%@ Register TagPrefix="uc2" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="uc1" TagName="AccountStep_Validation" Src="~/UserControls/AccountStep_Validation.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentHolder" runat="Server">
    <table id="TblStep1" height="100%" cellspacing="0" cellpadding="0" width="100%" align="center"
        border="0">
        <tr>
            <br />
            <td align="left">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr id="trHeaderDisplay" runat="server" visible="true">
                        <td colspan="2">
                            <asp:Label ID="lblSectionTitle" runat="server" CssClass="SectionTitleLabel">Section Title :</asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="lblPageTitle" runat="server" CssClass="PageTitleLabel">Page Title </asp:Label>
                        </td>
                    </tr>
                    <tr id="trOrg" runat="server" visible="false">
                        <td>
                            <asp:Label ID="lblNewOrg" runat="server" CssClass="SectionTitleLabel">New Organization</asp:Label>
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
            <td align="left" width="100%" height="100%">
                <table height="100%" cellspacing="0" cellpadding="0" width="100%" align="center"
                    border="0">
                    <%--<tr>
                        <td>
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
                    </tr>--%>
                    <tr>
                        <td>
                            <table id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td align="left">
                                        <asp:ValidationSummary ID="valSum" runat="server" HeaderText="<br>Correct the following error to proceed."
                                            CssClass="LabelError"></asp:ValidationSummary>
                                        <asp:Label ID="lblMessage" runat="server" CssClass="LabelError" ForeColor="Red"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td height="5px">
                                    </td>
                                </tr>
                                <tr id="trCampInfoTitle" runat="server">
                                    <td align="left">
                                        <!--Section Body -->
                                        <table id="tblCampInfoTitle" cellspacing="0" cellpadding="0" border="0">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblOrganizationTitle" runat="server" CssClass="FormTitleLabel"> QSP&nbsp;Organization&nbsp;ID&nbsp;#:&nbsp;
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblOrganizationID" runat="server" CssClass="FormTitleDescLabel">
																										00000
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                    &nbsp;-&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblOrganizationName" runat="server" CssClass="FormTitleDescLabel">
																										Organization Name
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                        <br>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <uc1:AccountStep_Information ID="Information_Step" runat="server"></uc1:AccountStep_Information>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <uc1:AccountStep_Validation ID="Validation_Step" runat="server"></uc1:AccountStep_Validation>
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
            </td>
        </tr>
    </table>
</asp:Content>
