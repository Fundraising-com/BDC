<%@ Control Language="vb" AutoEventWireup="false" Codebehind="InventoryOptCtrl.ascx.vb" Inherits="StoreFront.StoreFront.InventoryOptCtrl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<table cellSpacing="0" cellPadding="0" width="100%">
	<tr>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<td class="ContentTableHeader" width="100%">&nbsp;<asp:label id="lblTitle" Runat="server" CssClass="ContentTableHeader">&nbsp;Inventory Options</asp:label>
			<table cellSpacing="0" cellPadding="0" width="100%" align="center">
				<TR>
					<TD class="Content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				</TR>
				<TR>
					<td class="content" colSpan="5">
						<TABLE cellSpacing="1" cellPadding="2" width="100%" align="center">
							<TR>
								<TD class="content" noWrap align="left">&nbsp;Track Inventory:<asp:checkbox id="chkTrack" Runat="server" CssClass="content" AutoPostBack="True"></asp:checkbox></TD>
								<TD class="content" noWrap align="left">&nbsp; Set default Qty In Stock At:</TD>
								<TD class="content" align="left" width="187">&nbsp;<asp:textbox id="txtDefaultQty" Runat="server" CssClass="content" Width="54px"></asp:textbox></TD>
								<TD class="content" align="left" width="100%">&nbsp;
									<asp:LinkButton ID="cmdApplyInventory" Runat="server">
										<asp:Image BorderWidth="0" ID="imgApplyInventory" runat="server" ImageUrl="../images/apply.jpg" AlternateText="Apply"></asp:Image>
									</asp:LinkButton>
								</TD>
							</TR>
							<TR>
								<TD class="Content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
							</TR>
							<TR>
								<TD class="content" align="left" colSpan="1" nowrap width="50%">&nbsp;Allow 
									Backorder:<asp:checkbox id="chkAllowBO" Runat="server" CssClass="content" Text="" AutoPostBack="True"></asp:checkbox>
								</TD>
								<TD class="content" align="left" colSpan="3" nowrap width="50%">&nbsp;Show Status:<asp:checkbox id="chkStatus" Runat="server" CssClass="content" Text="" AutoPostBack="True"></asp:checkbox>
								</TD>
							</TR>
							<TR>
								<TD class="Content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
							</TR>
							<TR>
								<TD class="content" noWrap align="left">&nbsp;Send Low Stock Notice:<asp:checkbox id="chkNotify" Runat="server" CssClass="content" Text=""></asp:checkbox></TD>
								<TD class="content" noWrap align="left">&nbsp; Set Default Low Qty Flag At:</TD>
								<TD class="content" align="left" width="187">&nbsp;<asp:textbox id="txtLowFlag" Runat="server" CssClass="content" Width="58px"></asp:textbox></TD>
								<TD class="content" align="left" width="100%">&nbsp;
									<asp:LinkButton ID="cmdApplyNotify" Runat="server">
										<asp:Image BorderWidth="0" ID="imgApplyNotify" runat="server" ImageUrl="../images/apply.jpg" AlternateText="Apply"></asp:Image>
									</asp:LinkButton>
								</TD>
							</TR>
							<TR>
								<TD class="Content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
							</TR>
						</TABLE>
					</td>
				</TR>
			</table>
		</td>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</tr>
	<TR>
		<TD class="ContentTable" colSpan="3" height="1"><IMG height="1" src="images/clear.gif"></TD>
	</TR>
</table>
