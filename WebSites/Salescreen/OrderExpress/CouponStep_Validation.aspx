<%@ Page Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" Inherits="QSP.OrderExpress.Web.CouponStep_Validation"  Codebehind="CouponStep_Validation.aspx.cs" %> 
<%@ Register Src="UserControls/CouponStep_Validation.ascx" TagName="CouponStep_Validation" TagPrefix="uc1" %> 
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
                            <uc1:CouponStep_Validation id="ctrlCouponStep_Validation" runat="server"></uc1:CouponStep_Validation> 
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
