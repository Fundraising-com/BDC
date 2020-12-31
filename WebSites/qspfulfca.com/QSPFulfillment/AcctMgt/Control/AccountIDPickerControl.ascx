<%@ Control Language="c#" AutoEventWireup="True" Codebehind="AccountIDPickerControl.ascx.cs" Inherits="QSPFulfillment.AcctMgt.Control.AccountIDPickerControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<cc1:textboxinteger id="tbxAccountID" runat="server" errormsgrequired="The field Account ID is mandatory."
	errormsgregexp="The field Account ID has to be a number." maxlength="25"></cc1:textboxinteger>
<asp:hyperlink id="hylPicker" runat="server" imageurl="../images/find.gif" navigateurl=""></asp:hyperlink>
