<%@ Control Language="c#" AutoEventWireup="True" Codebehind="NewItem.ascx.cs" Inherits="QSPFulfillment.CustomerService.action.NewItem" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="NewItemStep2" Src="NewItemStep2.ascx" %>
<%@ Register TagPrefix="uc2" TagName="NewItemStep1" Src="NewItemStep1.ascx" %>
<uc1:NewItemStep2 runat=server visible=false id="ctrlNewItemStep2"></uc1:NewItemStep2>
<uc2:NewItemStep1 runat=server visible=false id="ctrlNewItemStep1"></uc2:NewItemStep1>
