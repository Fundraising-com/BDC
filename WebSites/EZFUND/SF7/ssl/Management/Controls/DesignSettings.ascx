<%@ Register TagPrefix="uc1" TagName="SFExpressUploadControl" Src="SFExpressUploadControl.ascx"%>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="DesignSettings.ascx.vb" Inherits="StoreFront.StoreFront.DesignSettings" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<asp:CheckBox id="chkVisible" runat="server" Visible="False" Text="Visible" CssClass="Content"></asp:CheckBox>
<%--
<table class="Content" width="100%" cellpadding="0" cellspacing="0">
	<tr>
		<td colSpan="4" class="ContentTableHeader">Design Settings
		</td>
	</tr>
	<tr>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
		<td colSpan="2" class="ContentTableSubHeader">General
		</td>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
	</tr>
	<tr>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
		<td colSpan="2" class="Content">&nbsp;
		</td>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
	</tr>
	<tr>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
		<td align="right">&nbsp;
			<asp:label id="lblBackGroundColor" runat="server">Background Color:</asp:label></td>
		<td align="left"><asp:textbox id="txtBackgroundColor" runat="server"></asp:textbox><OP:OPCOLORPICKER id="BackgroundColor" runat="server" AutoPostBack="True" ColorType="FillColor"></OP:OPCOLORPICKER></td>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
	<tr>
		<td class="ContentTable" height="1"><IMG src="images/clear.gif"></td>
		<td align="right"><asp:label id="lblBackGroundImage" runat="server">Background Image:</asp:label></td>
		<td align="left"><asp:textbox id="txtBackgroundImage" runat="server"></asp:textbox>
			<asp:ImageButton ID="btnBrowse" OnClick="UploadImage" ImageUrl="../Images/icon_browse.gif" Runat="server"></asp:ImageButton></td>
		<td class="ContentTable" height="1"><IMG src="images/clear.gif"></td>
	</tr>
	<tr>
		<td class="ContentTable" height="1"><IMG src="images/clear.gif"></td>
		<td colspan="2"><uc1:SFExpressUploadControl id="ucUploadImage" Visible="False" runat="server"></uc1:SFExpressUploadControl></td>
		<td class="ContentTable" height="1"><IMG src="images/clear.gif"></td>
	</tr>
	<tr>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
		<td align="right"><asp:label id="lblAlignment" runat="server">Alignment:</asp:label></td>
		<td align="left"><asp:dropdownlist id="ddlAlignment" runat="server" Width="88px">
				<asp:ListItem Value="left">left</asp:ListItem>
				<asp:ListItem Value="center">center</asp:ListItem>
				<asp:ListItem Value="right">right</asp:ListItem>
			</asp:dropdownlist></td>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
	</tr>
	<asp:Panel Runat="server" ID="pnlWidth" Visible="False">
		<TR>
			<TD class="ContentTable" height="1"><IMG src="images/clear.gif"></TD>
			<TD align="right">
				<asp:label id="Label1" runat="server">Width:</asp:label></TD>
			<TD align="left">
				<asp:textbox id="txtWidth" runat="server"></asp:textbox></TD>
			<TD class="ContentTable" height="1"><IMG src="images/clear.gif"></TD>
		</TR>
	</asp:Panel>
	<tr>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
		<td colSpan="2" class="Content">&nbsp;
		</td>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
	</tr>
	<tr>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
		<td class ="ContentTableSubHeader" colSpan="2">Font
		</td>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
	</tr>
	<tr>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
		<td colSpan="2" class="Content">&nbsp;
		</td>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
	</tr>
	<tr>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
		<td align="right"><asp:label id="lblFont" runat="server">Font:</asp:label></td>
		<td align="left"><asp:DropDownList ID="ddlFontList" runat="server"></asp:DropDownList></td>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
	</tr>
	<tr>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
		<td align="right"><asp:label id="lblSize" runat="server">Size:</asp:label></td>
		<td align="left"><asp:textbox id="txtSize" runat="server" Width="40px"></asp:textbox></td>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
	</tr>
	<tr>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
		<td align="right"><asp:label id="lblColor" runat="server">Color:</asp:label></td>
		<td align="left"><asp:textbox id="txtFontColor" runat="server"></asp:textbox><op:opcolorpicker id="FontColor" runat="server" AutoPostBack="True"></op:opcolorpicker></td>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
	</tr>
	<tr>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
		<td align="right"><asp:label id="lblStyle" runat="server">Style:</asp:label></td>
		<td align="left"><asp:dropdownlist id="ddlStyle" runat="server">
				<asp:ListItem Value="Normal">Normal</asp:ListItem>
				<asp:ListItem Value="Italic">Italic</asp:ListItem>
				<asp:ListItem Value="Bold">Bold</asp:ListItem>
				<asp:ListItem Value="Bold Italic">Bold Italic</asp:ListItem>
			</asp:dropdownlist></td>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
	</tr>
	<tr>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
		<td colSpan="2" class="Content">&nbsp;
		</td>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
	</tr>
	<tr>
		<td class="ContentTable" colspan="4" height="1"><IMG height="1" src="images/clear.gif"></td>
	</tr>
</table>
--%>
