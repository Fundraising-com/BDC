<%@ Control Language="vb" AutoEventWireup="false" Codebehind="OnlineChatControl.ascx.vb" Inherits="StoreFront.StoreFront.OnlineChatControl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0" ID="Table1">
	<TBODY>
		<tr>
			<TD class="Content" width="1" colSpan="4">&nbsp;</TD>
		</tr>
		<tr>
			<TD class="Content" colSpan="4" align="middle"></TD>
		</tr>
		<tr>
			<TD class="Content" colSpan="4" align="middle">
				<table>
					<tr>
						<td class="Content" align="middle"><img src="../images/liveperson.jpg">
						</td>
						<td class="Content" align="left">Add LivePerson, the leading live help solution, to 
							your web store.
						</td>
					</tr>
					<tr>
						<td class="Content" align="left" colspan="2">
							<p>
							When a shopper visits your web store, you will immediately be notified that a 
							visitor is at your “front door.'' As customers move throughout your site, you 
							can proactively greet them and invite them to chat online. Customers in need of 
							assistance can also reach you by clicking the LivePerson button. With 
							LivePerson, you can also view the content of your visitor's shopping cart in 
							real time.
							<p>Take advantage of special pricing exclusively for StoreFront merchants – up to 
								40% off.</p>
							<p><a href="http://www.liveperson.com/ref/sf/trial.asp" target="_blank">Click Here</a>
								to learn more about LivePerson Pro.</p>
							<p>&nbsp;</p>
						</td>
					</tr>
				</table>
			</TD>
		</tr>
		<tr>
			<TD class="Content" width="1" colSpan="4">&nbsp;</TD>
		</tr>
		<TR>
			<TD class="Content" colSpan="4">
				<table runat="server" id="tblTrial" cellpadding="0" cellspacing="0" border="0" width="100%">
					<TR>
						<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
						<TD class="ContentTableHeader" noWrap align="left" colSpan="2">&nbsp;&nbsp;Free 
							Trial Account&nbsp;
						</TD>
						<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
					</TR>
					<tr>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
						<TD class="content" width="1" colSpan="2"><IMG height="5" src="images/clear.gif"></TD>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					</tr>
					<tr>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
						<TD class="content" colSpan="2">&nbsp;&nbsp;&nbsp;<a href="http://www.liveperson.com/ref/sf/" target="_blank">Click 
								Here</a> to register for a free 7 day trial account with LivePerson.</TD>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					</tr>
					<tr>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
						<TD class="content" width="1" colSpan="2"><IMG height="5" src="images/clear.gif"></TD>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					</tr>
					<TR>
						<TD class="ContentTable" colSpan="4" height="1"><IMG height="1" src="images/clear.gif"></TD>
					</TR>
				</table>
			</TD>
		</TR>
		<tr>
			<TD class="Content" width="1" colSpan="4">&nbsp;</TD>
		</tr>
		<TR>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="ContentTableHeader" noWrap align="left" colSpan="2">&nbsp;&nbsp;Enter 
				Your LivePerson Account Number (if available)</TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="2"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" align="right" colspan="1">LivePerson Account Number:</td>
			<td class="content" align="left" colspan="1">&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="LivePersonID" Runat="server" MaxLength="50"></asp:TextBox></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="2"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" colSpan="4" height="1"><IMG height="1" src="images/clear.gif"></TD>
		</TR>
		<tr>
			<TD class="Content" width="1" colSpan="4">&nbsp;</TD>
		</tr>
		<TR>
			<td class="content" align="middle" width="75%" colSpan="4">
				<asp:LinkButton ID="cmdSave" Runat="server" CausesValidation="False">
					<asp:Image BorderWidth="0" ID="imgSave" runat="server" ImageUrl="../images/save.jpg" AlternateText="Save"></asp:Image>
				</asp:LinkButton>
			</td>
		</TR>
	</TBODY></TABLE>
