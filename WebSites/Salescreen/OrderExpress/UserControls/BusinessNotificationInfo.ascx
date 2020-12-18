<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.BusinessNotificationInfo" Codebehind="BusinessNotificationInfo.ascx.cs" %>

<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td align="left"> <!--Section Title --></td>
	</tr>
	<tr>
		<td>
			<table id="Table3" cellSpacing="0" cellPadding="3" width="600" border="0">
				<tr id=trBizNoteID runat=server>
					<td class="StandardLabel" width="150">Note&nbsp;ID&nbsp;#:&nbsp;</td>
					<td width="90%"><asp:label id="lblBusinessNotificationID" CssClass="DescInfoLabel" runat="server"></asp:label></td>
				</tr>
				<tr id=trBizName runat=server>
					<td class="StandardLabel">Name:&nbsp;</td>
					<td><asp:label id="lblBusinessNotificationName" CssClass="DescInfoLabel" runat="server" Font-Bold="True"></asp:label></td>
				</tr>
				<tr id=trBizNoteType runat=server>
					<td class="StandardLabel">Note&nbsp;Type:&nbsp;</td>
					<td><asp:label id="lblNoteType" CssClass="DescInfoLabel" runat="server" Font-Bold="True"></asp:label></td>
				</tr>
				<tr >
					<td class="StandardLabel">Received&nbsp;Date:&nbsp;</td>
					<td><asp:label id="lblReceivedDate" CssClass="DescInfoLabel" runat="server" Font-Bold="True"></asp:label></td>
				</tr>
				<tr id=trFromName runat=server>
					<td class="StandardLabel">From:&nbsp;</td>
					<td><asp:TextBox id="txtCreatorUserName" CssClass="DescInfoLabel" runat="server"  ReadOnly="True" width="400px" ></asp:TextBox></td>
				</tr>
				<tr id=trToName runat=server>
					<td class="StandardLabel">To:&nbsp;</td>
					<td><asp:TextBox id="txtAssignedUserName" CssClass="DescInfoLabel" runat="server"  ReadOnly="True" width="400px" ></asp:TextBox></td>
				</tr>
				<tr id=trContextInfo runat=server>
					<td><asp:label id="lblContext" CssClass="StandardLabel" runat="server">Context :</asp:label></td>
					<td>
						<asp:label id="lblContextInfo" CssClass="DescInfoLabel" runat="server" Font-Bold="True"></asp:label>
					</td>
				</tr>
				<tr>
					<td class="StandardLabel" vAlign="top">Subject:&nbsp;</td>
					<td>
						<asp:TextBox id="txtSubject" ReadOnly="True" width="400px" Runat="server" CssClass="DescInfoLabel"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td class="StandardLabel" vAlign="top">Message:&nbsp;</td>
					<td>
						<div id="divMessage" runat="server" style="BORDER-RIGHT: thin inset; BORDER-TOP: thin inset; BORDER-LEFT: thin inset; WIDTH: 400px; BORDER-BOTTOM: thin inset; HEIGHT: 150px; BACKGROUND-COLOR: white; overflow: auto;">
							<table border="0" cellpadding="0" cellspacing="0">
								<tr>
									<td align="left">
										<asp:Label id="lblMessage" Runat="server" CssClass="DescInfoLabel" ></asp:Label>
									</td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
				<tr vAlign="top" id=trDescription runat=server>
					<td><asp:label id="Label1" CssClass="StandardLabel" runat="server">Description :</asp:label></td>
					<td>
						<asp:label id="lblDescription" CssClass="DescInfoLabel" runat="server"></asp:label>
					</td>
				</tr>
				<tr id=trComplete runat=server>
					<td class="StandardLabel">Is&nbsp;Complete&nbsp;:&nbsp;</td>
					<td>
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td><asp:checkbox id="chkBoxCompleted" Enabled="False" runat="server" CssClass="DescInfoLabel"></asp:checkbox></td>
								<td class="StandardLabel" align="right" width="100%">Completion&nbsp;Date&nbsp;:&nbsp;</td>
								<td><asp:label id="lblCompletionDate" Width="150px" CssClass="DescInfoLabel" Runat="server"></asp:label></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table>
