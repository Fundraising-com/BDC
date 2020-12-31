<%@ Control Language="c#" AutoEventWireup="True" Codebehind="NewItemStep1.ascx.cs" Inherits="QSPFulfillment.CustomerService.action.NewItemStep1" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CustomerService" Assembly="QSPFulfillment" %>
<%@ Register TagPrefix="uc1" TagName="ControlerMagazineTerm" Src="../ControlerMagazineTerm.ascx" %>
<br>
<uc1:ControlerMagazineTerm id="ctrlControlerMagazineTerm" runat="server" ProductType="0" ShowNewRenew="false"></uc1:ControlerMagazineTerm>
