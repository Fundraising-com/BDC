<%@ Control Language="c#" AutoEventWireup="True" Codebehind="NewSub.ascx.cs" Inherits="QSPFulfillment.CustomerService.action.NewSub" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="NewSubStep2" Src="NewSubStep2.ascx" %>
<%@ Register TagPrefix="uc2" TagName="NewSubStep1" Src="NewSubStep1.ascx" %>
<uc1:NewSubStep2 runat=server visible=false id="ctrlNewSubStep2"></uc1:NewSubStep2>
<uc2:NewSubStep1 runat=server visible=false id="ctrlNewSubStep1"></uc2:NewSubStep1>
