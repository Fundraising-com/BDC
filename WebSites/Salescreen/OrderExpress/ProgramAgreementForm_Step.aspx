<%@ Register TagPrefix="uc2" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Page language="c#" MasterPageFile="~/MainMaster.master" AutoEventWireup="false" Inherits="QSP.OrderExpress.Web.ProgramAgreementForm_Step" Codebehind="ProgramAgreementForm_Step.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="~/UserControls/HeaderControl.ascx" %> 

<%@ MasterType VirtualPath="~/MainMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentHolder" Runat="Server"> 
<table id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td align="left">
                <uc1:Header ID="Header" runat="server"></uc1:Header>
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td align="left">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;
                     margin-left: 5px;">
                    <tr>
                        <td align="left">
                            <asp:placeholder id="plHoldBodyPage" runat="server"></asp:placeholder>
                            <br />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
            </td>
        </tr>
    </table>
</asp:Content>
