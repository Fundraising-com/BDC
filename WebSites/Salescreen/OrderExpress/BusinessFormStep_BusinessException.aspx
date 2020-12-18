<%@ Page Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" Inherits="QSP.OrderExpress.Web.BusinessFormStep_BusinessException"  Codebehind="BusinessFormStep_BusinessException.aspx.cs" %> 
<%@ Register Src="UserControls/BusinessFormStep_BusinessException.ascx" TagName="BusinessFormStep_BusinessException" TagPrefix="uc1" %> 
<%@ Register TagPrefix="uc1" TagName="Header" Src="~/UserControls/HeaderControl.ascx" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentHolder" Runat="Server"> 
<table id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td align="left">
                <uc1:Header ID="Header" runat="server"></uc1:Header>
            </td>
        </tr>
        <tr>
            <td align="left">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;
                     margin-left: 5px;">
                    <tr>
                        <td align="left">
                            <uc1:BusinessFormStep_BusinessException id="ctrlBusinessFormStep_BusinessException" runat="server"></uc1:BusinessFormStep_BusinessException> 
                            <br>
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
