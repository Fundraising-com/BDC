<%@ Register TagPrefix="uc1" TagName="SFExpressUploadControl" Src="SFExpressUploadControl.ascx"%>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="PageSettings.ascx.vb" Inherits="StoreFront.StoreFront.PageSettings" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<table width="100%" align="center">
	<tr>
		<td>
			<table class="Content" width="100%" align="center" cellpadding="0" cellspacing="0">
				<TBODY>
					<tr>
						<td colSpan="4" class="ContentTableHeader">Theme Info
						</td>
					</tr>
					<tr>
						<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
						<td colSpan="2">&nbsp;
						</td>
						<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
					</tr>
					<tr>
						<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
						<td align="right" width="50%">&nbsp;
							<asp:label id="lblThemeName" runat="server">
								<b>Theme Name:&nbsp;</b></asp:label></td>
						<td align="left" width="50%"><asp:label id="ThemeName" runat="server"></asp:label></td>
						<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
					</tr>
					<tr>
						<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
						<td colSpan="2">&nbsp;
						</td>
						<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
					</tr>
					<tr>
						<td class="ContentTable" height="1" colspan="4"><IMG src="images/clear.gif"></td>
		</td>
	</tr>
</table>
</TD></TR>
<tr>
	<td>
		<table class="Content" width="100%" align="center" cellpadding="0" cellspacing="0">
			<tr>
				<td colSpan="4" class="ContentTableHeader">Design Settings
				</td>
			</tr>
			<tr>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
				<td colSpan="2" class="ContentTableSubHeader">Page Settings&nbsp;
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
				<td align="right"><asp:label id="Label1" runat="server">Top Margin:</asp:label></td>
				<td align="left"><asp:textbox id="txtTopMargin" runat="server"></asp:textbox></td>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
			</tr>
			<tr>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
				<td align="right"><asp:label id="Label2" runat="server">Right Margin:</asp:label></td>
				<td align="left"><asp:textbox id="txtRightMargin" runat="server"></asp:textbox></td>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
			</tr>
			<tr>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
				<td align="right"><asp:label id="Label3" runat="server">Bottom Margin:</asp:label></td>
				<td align="left"><asp:textbox id="txtBottomMargin" runat="server"></asp:textbox></td>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
			</tr>
			<tr>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
				<td align="right"><asp:label id="Label4" runat="server">Left Margin:</asp:label></td>
				<td align="left"><asp:textbox id="txtLeftMargin" runat="server"></asp:textbox></td>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
			</tr>
			<tr class="Content">
				<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
				<td align="right">&nbsp;
					<asp:label id="lblBackGroundColor" runat="server">Background Color:</asp:label></td>
				<td align="left"><asp:textbox id="txtBackgroundColor" runat="server"></asp:textbox><%-- <OP:OPCOLORPICKER id="BackgroundColor" runat="server" AutoPostBack="True" ColorType="FillColor"></OP:OPCOLORPICKER> --%></td>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
			<tr>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
				<td align="right"><asp:label id="lblBackGroundImage" runat="server">Background Image:</asp:label></td>
				<td align="left"><asp:textbox id="txtBackgroundImage" runat="server"></asp:textbox>
					<asp:ImageButton ID="btnBrowse" OnClick="UploadImage" ImageUrl="../Images/icon_browse.gif" Runat="server"></asp:ImageButton>
				</td>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
			</tr>
			<tr>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
				<td colspan="2"><uc1:SFExpressUploadControl id="ucUploadImage" Visible="False" runat="server"></uc1:SFExpressUploadControl></td>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
			</tr>
			<tr>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
				<td align="right"></td>
				<td align="left"></td>
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
				<td colSpan="2" class="ContentTableSubHeader">Table Settings&nbsp;
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
				<td align="right"><asp:label id="lblFont" runat="server">Width:</asp:label></td>
				<td align="left">
					<asp:textbox id="txtWidth" runat="server" Width="40px"></asp:textbox></td>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
			</tr>
			<tr>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
				<td align="right"><asp:label id="lblSize" runat="server">Cell Padding:</asp:label></td>
				<td align="left"><asp:textbox id="txtCellPadding" runat="server" Width="40px"></asp:textbox></td>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
			</tr>
			<tr>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
				<td align="right"><asp:label id="Label5" runat="server">Cell Spacing:</asp:label></td>
				<td align="left"><asp:textbox id="txtCellSpacing" runat="server" Width="40px"></asp:textbox></td>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
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
			<tr>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
				<td align="right"><asp:label id="lblColor" runat="server">Border Color:</asp:label></td>
				<td align="left"><asp:textbox id="txtBorderColor" runat="server"></asp:textbox><%-- <op:opcolorpicker id="BorderColor" runat="server" AutoPostBack="True"></op:opcolorpicker> --%></td>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
			</tr>
			<tr>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
				<td align="right"><asp:label id="Label6" runat="server">Border Size:</asp:label></td>
				<td align="left"><asp:textbox id="txtBorderSize" runat="server" Width="40px"></asp:textbox></td>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
			</tr>
			<tr>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
				<td colSpan="2" class="Content">&nbsp;
				</td>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
			</tr>
			<tr>
				<td class="ContentTable" height="1" colspan="4"><IMG src="images/clear.gif"></td>
			</tr>
		</table>
	</td>
</tr>
</TBODY></TABLE>
