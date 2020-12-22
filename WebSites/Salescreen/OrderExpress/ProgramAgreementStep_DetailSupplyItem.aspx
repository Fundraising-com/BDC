<%@ Page Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" Inherits="QSP.OrderExpress.Web.ProgramAgreementStep_DetailSupplyItem"  Codebehind="ProgramAgreementStep_DetailSupplyItem.aspx.cs" %> 
<%@ Register Src="UserControls/ProgramAgreementStep_DetailSupplyItem.ascx" TagName="ProgramAgreementStep_DetailSupplyItem" TagPrefix="uc1" %> 
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
                            <uc1:ProgramAgreementStep_DetailSupplyItem id="ctrlProgramAgreementStep_DetailSupplyItem" runat="server"></uc1:ProgramAgreementStep_DetailSupplyItem> 
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
