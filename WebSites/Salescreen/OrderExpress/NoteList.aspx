<%@ Page Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" Inherits="QSP.OrderExpress.Web.NoteList" Codebehind="NoteList.aspx.cs" %>
<%@ Register Src="~/UserControls/BusinessNotificationList.ascx" TagName="BusinessNotificationList"
    TagPrefix="uc1" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="~/UserControls/HeaderControl.ascx" %>
<%@ MasterType VirtualPath="~/MainMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContentHolder" Runat="Server">
<table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
         <tr>
            <td align="left">
                <uc1:Header ID="Header" runat="server"></uc1:Header>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentHolder" Runat="Server">
  <table id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0">
       
        <tr>
            <td align="left">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;
                     margin-left: 5px;">
                    <tr>
                        <td align="left">
                            <uc1:BusinessNotificationList ID="CtrlBusinessNotificationList" runat="server"></uc1:BusinessNotificationList>
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
    <asp:Label id="abcNote" Text="Note Subject" runat="server" Visible="false"></asp:Label>
</asp:Content>
