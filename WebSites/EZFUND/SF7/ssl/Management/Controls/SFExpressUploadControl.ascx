<%@ Control Language="vb" AutoEventWireup="false" Codebehind="SFExpressUploadControl.ascx.vb" Inherits="StoreFront.StoreFront.SFExpressUploadControl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<asp:panel id="pnlfile" Runat="server">
	<TABLE cellPadding="4" align="center" border="0">
		<TR>
			<TD class="content" vAlign="top" width="100" colSpan="2"></TD>
		</TR>
		<TR>
			<TD vAlign="top" align="left" width="100" colSpan="2"><INPUT class="content" id="filename" type="file" name="filename" runat="server">
			</TD>
		</TR>
		<TR>
			<TD vAlign="top" align="left" width="50%">
				<asp:LinkButton id="uploadBtn" onclick="uploadBtn_Click" Runat="server">
					<asp:Image BorderWidth="0" ID="imgUploadBtn" runat="server" ImageUrl="../images/save.jpg" AlternateText="Save"></asp:Image>
				</asp:LinkButton></TD>
			<TD class="Content" align="left" width="50%">
				<asp:LinkButton id="CmdCancel" Runat="server">
					<asp:Image BorderWidth="0" ID="imgCancel" runat="server" ImageUrl="../images/cancel.jpg" AlternateText="Cancel"></asp:Image>
				</asp:LinkButton></TD>
		</TR>
		<TR>
			<TD vAlign="top" align="center" colSpan="2">
				<asp:Label id="status" runat="server" cssclass="ErrorMessages"></asp:Label></TD>
		</TR>
	</TABLE>
</asp:panel>
