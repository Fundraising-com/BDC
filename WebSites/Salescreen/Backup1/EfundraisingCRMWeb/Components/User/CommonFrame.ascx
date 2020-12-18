<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommonFrame.ascx.cs" Inherits="EFundraisingCRMWeb.Components.User.CommonFrame" %>
<table cellpadding=0 cellspacing=0 border=0>
<tr>
	<td width=1 height=1 class="FrameBorder"></td>
	<td height=1 class="FrameBorder"></td>
	<td width=1 height=1 class="FrameBorder"></td>
</tr>
<tr>
	<td width=1 class="FrameBorder"></td>
	<td class="FrameBody">
		<table cellpadding="0" cellspacing="0" border="0">
			<tr>
				<td class="FrameTopBar NormalTextBold"><asp:Label ID="TitleLabel" Runat="server"></asp:Label></td>
			</tr>
			<tr>
				<td>
					<table cellpadding=0 cellspacing=3 border=0>
					<tr>
						<td class="NormalText"><asp:PlaceHolder ID="InnerControlsPlaceHolder" Runat="server"></asp:PlaceHolder></td>
					</tr>
					</table>
				</td>
			</tr>
		</table>
	</td>
	<td width=1 class="FrameBorder"></td>
</tr>
<tr>
	<td width=1 height=1 class="FrameBorder"></td>
	<td height=1 class="FrameBorder"></td>
	<td width=1 height=1 class="FrameBorder"></td>
</tr>
</table>
