<%@ Control Language="vb" AutoEventWireup="false" Codebehind="PaymentMethodsControl.ascx.vb" Inherits="StoreFront.StoreFront.PaymentMethodsControl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<TABLE id="tblPaymentMethods" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
	<TBODY>
		<tr>
			<TD class="Content" width="1" colSpan="6">&nbsp;
				<asp:label id="ErrorMessage" runat="server" Visible="False" ForeColor="Red" Font-Bold="True"></asp:label></TD>
		</tr>
		<TR>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="ContentTableHeader" noWrap align="left" colSpan="4">&nbsp;&nbsp;Credit 
				Cards&nbsp;
			</TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" align="right">Accept Credit Cards:&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="content" align="left"><asp:checkbox id="AcceptCC" runat="server"></asp:checkbox></td>
			<td class="content" align="right">Processing Method:&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="content" align="left"><asp:dropdownlist id="CCProcMethod" runat="server"></asp:dropdownlist></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" colSpan="6" height="1"><IMG height="1" src="images/clear.gif"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" noWrap align="right" colSpan="1">Delete Credit 
				Cards:&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="content" align="left" colSpan="3"><asp:dropdownlist id="CCDeleteSchedule" runat="server"></asp:dropdownlist></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" noWrap align="right" colSpan="1">Require Security 
				Code:&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="content" align="left" colSpan="3"><asp:checkbox id="RequireSecurity" runat="server"></asp:checkbox></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" colSpan="6" height="1"><IMG height="1" src="images/clear.gif"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR id="trCCs" runat="server">
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" vAlign="top" noWrap align="right" colSpan="1">Accept Credit 
				Card Types:&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="content" vAlign="top" align="left" colSpan="3">
				<TABLE id="tblCCs" cellSpacing="0" cellPadding="0" width="60%" border="0">
					<TBODY>
						<asp:repeater id="AcceptedCCs" runat="server">
							<HeaderTemplate>
							</HeaderTemplate>
							<ItemTemplate>
								<TR>
									<TD class="Content" align="left">&nbsp;&nbsp;
										<asp:textbox id="Name" MaxLength=50 runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Name") %>'>
										</asp:textbox>&nbsp;&nbsp;</TD>
									<TD class="Content" align="left">&nbsp;&nbsp;
										<asp:LinkButton ID="cmdDelete" Runat="server" OnClick="deleteRow" CommandName='<%# Container.ItemIndex %>'>
											<asp:Image BorderWidth="0" ID="imgDelete" runat="server" ImageUrl="../images/icon_delete.gif" AlternateText="Delete"></asp:Image>
										</asp:LinkButton>
										&nbsp;&nbsp;</TD>
								</TR>
							</ItemTemplate>
							<FooterTemplate>
							</FooterTemplate>
						</asp:repeater>
						<TR>
							<TD class="Content" align="left">&nbsp;&nbsp;
								<asp:textbox id="NewCCName" runat="server" MaxLength="50"></asp:textbox>&nbsp;&nbsp;</TD>
							<TD class="Content" align="left">&nbsp;&nbsp;
								<asp:LinkButton ID="cmdAdd" Runat="server" CommandName="Add">
									<asp:Image BorderWidth="0" ID="imgAdd" runat="server" ImageUrl="../images/icon_add.gif" AlternateText="Add"></asp:Image>
								</asp:LinkButton>
								&nbsp;&nbsp;</TD>
						</TR>
						<tr>
							<TD class="Content" width="1" colSpan="4">&nbsp;</TD>
						</tr>
					</TBODY></TABLE>
			</td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" colSpan="6" height="1"><IMG height="1" src="images/clear.gif"></TD>
		</TR>
		<tr>
			<TD class="Content" width="1" colSpan="6">&nbsp;</TD>
		</tr>
		<TR>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="ContentTableHeader" noWrap align="left" colSpan="4">&nbsp;&nbsp;E-Checks&nbsp;
			</TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" noWrap align="right">Accept E-Checks:&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="content" align="left"><asp:checkbox id="AcceptEcheck" runat="server"></asp:checkbox></td>
			<td class="content" align="right">Processing Method:&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="content" align="left"><asp:dropdownlist id="EcheckProcMethod" runat="server"></asp:dropdownlist></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" colSpan="6" height="1"><IMG height="1" src="images/clear.gif"></TD>
		</TR>
		<tr>
			<TD class="Content" width="1" colSpan="6">&nbsp;</TD>
		</tr>
		<TR>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="ContentTableHeader" noWrap align="left" colSpan="4">&nbsp;&nbsp;COD&nbsp;
			</TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" noWrap align="right">Accept COD Orders:&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="content" align="left"><asp:checkbox id="AcceptCOD" runat="server"></asp:checkbox></td>
			<td class="content" align="right">COD Amount:&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="content" align="left"><asp:textbox id="CODAmount" runat="server"></asp:textbox></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" colSpan="6" height="1"><IMG height="1" src="images/clear.gif"></TD>
		</TR>
		<tr>
			<TD class="Content" width="1" colSpan="6">&nbsp;</TD>
		</tr>
		<TR>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="ContentTableHeader" noWrap align="left" colSpan="4">&nbsp;&nbsp;Purchase 
				Orders&nbsp;
			</TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" noWrap align="right" colSpan="1">Accept Purchase 
				Orders:&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="content" align="left" colSpan="3"><asp:checkbox id="AcceptPO" runat="server"></asp:checkbox></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" colSpan="6" height="1"><IMG height="1" src="images/clear.gif"></TD>
		</TR>
		<tr>
			<TD class="Content" width="1" colSpan="6">&nbsp;</TD>
		</tr>
		<TR>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="ContentTableHeader" noWrap align="left" colSpan="4">&nbsp;&nbsp;Mail/Fax&nbsp;
			</TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" noWrap align="right">Accept Mail/Fax 
				Orders:&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="content" align="left"><asp:checkbox id="AcceptMailFax" runat="server"></asp:checkbox></td>
			<td class="content" align="right">Type:&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="content" align="left"><asp:dropdownlist id="MailFaxType" runat="server"></asp:dropdownlist></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" colSpan="6" height="1"><IMG height="1" src="images/clear.gif"></TD>
		</TR>
		<tr>
			<TD class="Content" width="1" colSpan="6">&nbsp;</TD>
		</tr>
		<TR>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="ContentTableHeader" noWrap align="left" colSpan="4">&nbsp;&nbsp;PayPal&nbsp;
			</TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" noWrap align="right">Accept PayPal 
				Orders:&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="content" align="left" colSpan="1"><asp:checkbox id="AcceptPayPal" runat="server"></asp:checkbox></td>
			<td class="content" noWrap align="right">PayPal Merchant 
				ID:&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="content" align="left" colSpan="1"><asp:textbox id="PayPalID" runat="server" MaxLength="100"></asp:textbox></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR runat="server" id="bo">
			<TD class="ContentTable" colSpan="6" height="1"><IMG height="1" src="images/clear.gif"></TD>
		</TR>
		<tr id="bo0" runat="server">
			<TD class="Content" width="1" colSpan="6">&nbsp;</TD>
		</tr>
		<TR id="bo1" runat="server">
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="ContentTableHeader" noWrap align="left" colSpan="4">&nbsp;&nbsp;Back 
				Order &nbsp;
			</TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr id="bo2" runat="server">
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR id="bo3" runat="server">
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" noWrap align="right" colSpan="1">Do Not Include Back Ordered 
				Items in Order Totals:
			</td>
			<td class="content" align="left" colSpan="3">&nbsp;<asp:checkbox id="chkAllowBO" runat="server"></asp:checkbox></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr id="bo4" runat="server">
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" colSpan="6" height="1"><IMG height="1" src="images/clear.gif"></TD>
		</TR>
		<tr>
			<TD class="Content" width="1" colSpan="6">&nbsp;</TD>
		</tr>
		<TR>
			<td class="content" align="right" width="75%" colSpan="6">
				<asp:LinkButton ID="cmdSave" Runat="server">
					<asp:Image BorderWidth="0" ID="imgSave" runat="server" ImageUrl="../images/save.jpg" AlternateText="Save"></asp:Image>
				</asp:LinkButton>
			</td>
		</TR>
	</TBODY></TABLE>
