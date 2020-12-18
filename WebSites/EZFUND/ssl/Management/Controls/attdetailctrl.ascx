<%@ Control Language="vb" AutoEventWireup="false" Codebehind="attdetailctrl.ascx.vb" Inherits="StoreFront.StoreFront.attdetailctrl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<%@ Register TagPrefix="uc1" TagName="UploadControl" Src="UploadControl.ascx" %>
<P id="ErrorAlignment" runat="server">
	<asp:label id="ErrorMessage" CssClass="ErrorMessages" runat="server" Visible="False"></asp:label></P>
<P id="MessageAlignment" runat="server">
	<asp:label id="Message" CssClass="Messages" runat="server" Visible="False"></asp:label></P>
<table cellSpacing="0" cellPadding="0" width="100%">
	<tr>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<td class="ContentTableHeader" colSpan="3">&nbsp;<asp:label id="lblTitle" CssClass="ContentTableHeader" Runat="server">Attribute Detail</asp:label>
			<table cellSpacing="0" cellPadding="0" width="100%" align="center">
				<TR>
					<td class="content" colSpan="5">&nbsp;</td>
				</TR>
				<TR>
					<td class="content" colSpan="5">
						<TABLE cellSpacing="1" cellPadding="2" width="100%" align="center">
							<TR>
								<TD class="content" colSpan="5">&nbsp;</TD>
							</TR>
							<TR>
								<TD class="content" noWrap colSpan="2">
									<asp:label id="lblDetailName" Runat="server" CssClass="content">Option:</asp:label>
									<asp:textbox id="txtDetailName" MaxLength=50 Runat="server" CssClass="content" Width="70%"></asp:textbox></TD>
								<TD class="content" noWrap align="right">Display Order:</TD>
								<TD class="content" colSpan="2">
									<asp:dropdownlist id="ddDetailOrder" Runat="server" CssClass="content" DataTextField="name" DataValueField="id" AutoPostBack="True"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="content" align="right">Price Change:</TD>
								<TD class="content">
									<asp:dropdownlist id="PriceType" Runat="server" CssClass="content">
										<asp:ListItem Value="0">Same</asp:ListItem>
										<asp:ListItem Value="1">Increase By</asp:ListItem>
										<asp:ListItem Value="2">Decrease By</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD class="content" align="right">Amount:</TD>
								<TD class="content" colSpan="2">
									<asp:textbox id="txtPrice" Runat="server" CssClass="content" Width="60%"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="content" align="right">Weight Change:</TD>
								<TD class="content">
									<asp:dropdownlist id="WeightType" Runat="server" CssClass="content">
										<asp:ListItem Value="0">Same</asp:ListItem>
										<asp:ListItem Value="1">Increase By</asp:ListItem>
										<asp:ListItem Value="2">Decrease By</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD class="content" align="right">Amount:</TD>
								<TD class="content" colSpan="2">
									<asp:textbox id="txtWeight" Runat="server" CssClass="content" Width="60%"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="content" width="90%" colSpan="5">
									<uc1:uploadcontrol id="UploadControl1" runat="server"></uc1:uploadcontrol></TD>
							</TR>
						</TABLE>
					<td></td>
				<TR>
					<TD class="Content" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<TD class="Content" align="left" width="80%">
						<asp:LinkButton ID="cmdAddOption" Runat="server">
							<asp:Image BorderWidth="0" ID="imgAddOption" runat="server" ImageUrl="../images/add_option.jpg" AlternateText="Add Option"></asp:Image>
						</asp:LinkButton>
					</TD>
					<TD class="Content" align="left" colSpan="2" nowrap>
						<asp:LinkButton ID="CmdCancel" Runat="server">
							<asp:Image BorderWidth="0" ID="imgCancel" runat="server" ImageUrl="../images/cancel.jpg" AlternateText="Cancel"></asp:Image>
						</asp:LinkButton>
						&nbsp;
						<asp:LinkButton ID="cmdSave" Runat="server">
							<asp:Image BorderWidth="0" ID="imgSave" runat="server" ImageUrl="../images/save.jpg" AlternateText="Save"></asp:Image>
						</asp:LinkButton>
					</TD>
					<TD class="Content" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<tr>
					<TD class="ContentTableHeader" width="1" colSpan="7"><IMG src="images/clear.gif" width="1"></TD>
				</tr>
			</table>
		</td>
	</tr>
</table>
