<%@ Control Language="vb" AutoEventWireup="false" Codebehind="attMainctrl.ascx.vb" Inherits="StoreFront.StoreFront.attMainctrl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<%@ Register TagPrefix="uc1" TagName="UploadControl" Src="UploadControl.ascx" %>
<P id="ErrorAlignment" runat="server"><asp:label id="ErrorMessage" runat="server" Visible="False" CssClass="ErrorMessages"></asp:label></P>
<P id="MessageAlignment" runat="server"><asp:label id="Message" runat="server" Visible="False" CssClass="Messages"></asp:label></P>
<table cellSpacing="0" cellPadding="0" width="100%">
	<tr>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<td class="ContentTableHeader" colSpan="3">&nbsp;<asp:label id="lblTitle" CssClass="ContentTableHeader" Runat="server">Attribute</asp:label>
			<table cellSpacing="0" cellPadding="0" width="100%" align="center">
				<TR>
					<td class="content" colSpan="5">&nbsp;</td>
				</TR>
				<TR>
					<td class="content" colSpan="5">
						<TABLE cellSpacing="1" cellPadding="2" width="100%" align="center">
							<TR>
								<TD class="content" colSpan="3">&nbsp;</TD>
							</TR>
							<TR>
								<TD class="content" align="right"><asp:label id="lblAttName" CssClass="content" Runat="server">Name:</asp:label></TD>
								<TD class="content"><asp:textbox id="txtAttName" CssClass="content" Runat="server" MaxLength="50"></asp:textbox><input id="InvPrompt" type="hidden" Runat="server"></TD>
							</TR>
							<TR id="TypeRow" runat="server">
								<TD class="content" align="right">Type:</TD>
								<TD class="content"><asp:dropdownlist id="attType" CssClass="content" Runat="server" AutoPostBack="True">
										<asp:ListItem Value="0">Merchant Defined</asp:ListItem>
										<asp:ListItem Value="1">Customer Defined</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD class="content">&nbsp;</TD>
							</TR>
							<TR id="requiredRow" runat="server">
								<TD class="content" align="right">Required:</TD>
								<TD class="content"><asp:checkbox id="chkRequired" CssClass="content" Runat="server"></asp:checkbox></TD>
								<TD class="content">&nbsp;</TD>
							</TR>
							<asp:panel id="CustomDetails" Runat="server">
								<TR>
									<TD class="content" noWrap align="right">Price Change:</TD>
									<TD class="content">
										<asp:dropdownlist id="PriceType" CssClass="content" Runat="server">
											<asp:ListItem Value="0">Same</asp:ListItem>
											<asp:ListItem Value="1">Increase By</asp:ListItem>
											<asp:ListItem Value="2">Decrease By</asp:ListItem>
										</asp:dropdownlist></TD>
									<TD class="content" align="left" width="100%">Amount:
										<asp:textbox id="txtPrice" CssClass="content" Runat="server" Width="60%"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="content" noWrap align="right">Weight Change:</TD>
									<TD class="content">
										<asp:dropdownlist id="WeightType" CssClass="content" Runat="server">
											<asp:ListItem Value="0">Same</asp:ListItem>
											<asp:ListItem Value="1">Increase By</asp:ListItem>
											<asp:ListItem Value="2">Decrease By</asp:ListItem>
										</asp:dropdownlist></TD>
									<TD class="content" align="left" width="100%">Amount:
										<asp:textbox id="txtWeight" CssClass="content" Runat="server" Width="60%"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="content" width="90%" colSpan="5">
										<UC1:UPLOADCONTROL id="UploadControl1" runat="server"></UC1:UPLOADCONTROL></TD>
								</TR>
							</asp:panel>
							<TR>
								<TD class="content" colSpan="3">&nbsp;</TD>
							</TR>
						</TABLE>
					</td>
				<TR>
					<TD class="Content" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<TD class="Content" align="right" width="100%" colSpan="3"><asp:linkbutton id="CmdCancel" Runat="server">
							<asp:Image BorderWidth="0" ID="imgCancel" runat="server" ImageUrl="../images/cancel.jpg" AlternateText="Cancel"></asp:Image>
						</asp:linkbutton>&nbsp;
						<asp:linkbutton id="cmdSave" Runat="server">
							<asp:Image BorderWidth="0" ID="imgSave" runat="server" ImageUrl="../images/save.jpg" AlternateText="Save"></asp:Image>
						</asp:linkbutton></TD>
					<TD class="Content" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<tr>
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</tr>
			</table>
		</td>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</tr>
</table>
