<%@ Page Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" Inherits="QSP.OrderExpress.Web.BusinessForm_Step" Codebehind="BusinessForm_Step.aspx.cs" %>

<%@ Register TagPrefix="uc2" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="uc1" TagName="BusinessFormStep_Information" Src="~/UserControls/BusinessFormStep_Information.ascx" %>
<%@ Register TagPrefix="uc1" TagName="BusinessFormStep_BusinessRule" Src="~/UserControls/BusinessFormStep_BusinessRule.ascx" %>
<%@ Register TagPrefix="uc1" TagName="BusinessFormStep_BusinessException" Src="~/UserControls/BusinessFormStep_BusinessException.ascx" %>
<%@ Register TagPrefix="uc1" TagName="BusinessFormStep_BusinessTask" Src="~/UserControls/BusinessFormStep_BusinessTask.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="~/UserControls/HeaderControl.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentHolder" runat="Server">
    <table cellpadding="4" width="100%">
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
                            <asp:ValidationSummary ID="valSum" runat="server" HeaderText="<br>Correct the following error to proceed."
                                CssClass="LabelError"></asp:ValidationSummary>
                            <asp:Label ID="lblMessage" runat="server" CssClass="LabelError" ForeColor="Red"></asp:Label></td>
                    </tr>
                    <tr id="trFormInfoTitle" runat="server">
                        <td align="left">
                            <!--Section Body -->
                            <br>
                            <table id="tblFormInfoTitle" cellspacing="0" cellpadding="0" border="0">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" CssClass="StandardLabel">
																										Base Form :
                                        </asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblBaseFormCode" runat="server" CssClass="StandardLabel" ForeColor="#993300">
																										00000
                                        </asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;-&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblBaseFormName" runat="server" CssClass="StandardLabel" ForeColor="#993300">
																										Form Name
                                        </asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <br>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc1:BusinessFormStep_Information ID="Information_Step" runat="server"></uc1:BusinessFormStep_Information>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc1:BusinessFormStep_BusinessRule ID="BusinessRule_Step" runat="server"></uc1:BusinessFormStep_BusinessRule>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc1:BusinessFormStep_BusinessException ID="BusinessException_Step" runat="server"></uc1:BusinessFormStep_BusinessException>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc1:BusinessFormStep_BusinessTask ID="BusinessTask_Step" runat="server"></uc1:BusinessFormStep_BusinessTask>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
