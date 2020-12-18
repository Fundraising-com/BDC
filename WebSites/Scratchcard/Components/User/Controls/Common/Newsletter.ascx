<%@ Register TagPrefix="contentpanel" Namespace="GA.BDC.Core.Web.UI.UIControls" Assembly="GA.BDC.Core.Web.UI.UIControls" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Newsletter.ascx.cs" Inherits="GA.BDC.WEB.ScratchcardWeb.Components.User.Controls.Common.Newsletter" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="152" border="0">
	<TR height="32">
		<TD colspan="2" align="center">
			<contentpanel:ContentPanelControl id="ContentPanelControl3" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Components\User\Controls\Common\Newsletter.ascx"></contentpanel:ContentPanelControl></TD>
	</TR>
	<TR>
		<TD align="right">
			<span class="small_black_bold">
				<contentpanel:ContentPanelControl id="ContentPanelControl1" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Components\User\Controls\Common\Newsletter.ascx"></contentpanel:ContentPanelControl></span>&nbsp;&nbsp;
		</TD>
		<td width="70%" vAlign="middle">
			<asp:TextBox id="txtName" runat="server" Width="79px" size="10"></asp:TextBox>
		</td>
	</TR>
	<TR>
		<TD align="right">
			<span class="small_black_bold">
				<contentpanel:ContentPanelControl id="ContentPanelControl2" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Components\User\Controls\Common\Newsletter.ascx"></contentpanel:ContentPanelControl></span>&nbsp;&nbsp;
		</TD>
		<td width="70%" vAlign="middle">
			<asp:TextBox id="txtEmail" runat="server" Width="79px" size="10"></asp:TextBox>
			<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
		</td>
	</TR>
	<TR>
		<TD align="center" colSpan="2">
			<asp:RegularExpressionValidator id="EmailValidator" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
				ControlToValidate="txtEmail" ErrorMessage="Email is not valid." Font-Size="X-Small" Display="Dynamic"></asp:RegularExpressionValidator></TD>
	</TR>
	<TR>
		<TD align="right"></TD>
		<TD vAlign="middle" width="60%">
			<contentpanel:ButtonPanelControl id="btnSubmit" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Components\User\Controls\Common\Newsletter.ascx"
				ButtonType="IMAGE" CodeName="btnSubmitNewsletter" CausesValidation="True"></contentpanel:ButtonPanelControl></TD>
	</TR>
</TABLE>
<ContentPanel:PagePanelControl id="PagePanelControl1" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Components\User\Controls\Common\Newsletter.ascx"></ContentPanel:PagePanelControl>
