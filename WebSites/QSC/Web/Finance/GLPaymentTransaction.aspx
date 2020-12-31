<%@ Page language="c#" Codebehind="GLPaymentTransaction.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.Finance.GLPaymentTransaction" %>
<HTML>
	<HEAD>
		<title>CA Fulfill System - GL Transactions For Payment</title>
		<link REL="stylesheet" HREF="../Includes/MagSysStyle.css" TYPE="text/css">
	</HEAD>
	<body topmargin="0" leftmargin="0">
		<form method="post" runat="server" ID="InvoiceForm">
			<br>
			<center><h3><font face="Verdana" color="#2f4f88">GL Transactions For Payment</font></h3>
			</center>
			<p></p>
			<!--GL -->
			<table BORDER="0" CELLSPACING="0" CELLPADDING="2" WIDTH="100%" ALIGN="center">
				<tr>
					<td>
						<asp:DataGrid ID="GLDG" OnItemDataBound="DataGrid_ItemDataBound"
							OnItemCreated="DataGrid_ItemCreated" SelectedItemStyle-ForeColor="black" SelectedItemStyle-BackColor="#AAAAAA"
							HeaderStyle-Font-Bold="True" AllowSorting="true" AllowPaging="True" PageSize="10" PagerStyle-Position="Bottom"
							PagerStyle-Mode="NumericPages" PagerStyle-HorizontalAlign="Center" PagerStyle-PageButtonCount="20"
							PagerStyle-Width="100%" PagerStyle-BackColor="#2f4f88" PagerStyle-ForeColor="white" BackColor="#CCCCCC"
							runat="server" DataKeyField="" AutoGenerateColumns="False" Width="100%" BorderColor="black"
							BorderWidth="1" GridLines="Both" CellPadding="2" CellSpacing="0" Font-Name="Verdana" Font-Size="8pt"
							HeaderStyle-BackColor="#2f4f88" ShowFooter="True">
							<Columns>
								<asp:TemplateColumn FooterStyle-Width="30" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top"
									ItemStyle-VerticalAlign="Top" HeaderText="<font color='white'>Add</font>" HeaderStyle-Wrap="False">
									<FooterTemplate>
										<asp:ImageButton CommandName="Add" ImageUrl="../Images/add.gif" ID="btnAdd" Runat="server" />
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderStyle-VerticalAlign="Top" ItemStyle-VerticalAlign="Top" HeaderText="<font color='white'>Invoice<br>ID</font>"
									HeaderStyle-Wrap="False">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "Invoice_ID") %>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Label width="30%" ID="add_InvoiceID" Runat="Server" />
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderStyle-VerticalAlign="Top" ItemStyle-VerticalAlign="Top" HeaderText="<font color='white'>Payment<br>ID</font>"
									HeaderStyle-Wrap="False">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "Payment_ID") %>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Label width="30%" ID="add_PaymentID" Runat="Server" />
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn FooterStyle-VerticalAlign="Bottom" HeaderStyle-VerticalAlign="Top" ItemStyle-VerticalAlign="Top"
									HeaderText="<font color='white'>Description</font>" HeaderStyle-Wrap="False">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "Description") %>
									</ItemTemplate>
									<FooterTemplate>
										<asp:TextBox width="100%" MaxLength="30" TextMode="MultiLine" Rows="2" ID="add_Description" CssClass="boxlookW"
											Runat="Server" />
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn FooterStyle-VerticalAlign="Bottom" HeaderStyle-VerticalAlign="Top" ItemStyle-VerticalAlign="Top"
									HeaderText="<font color='white'>GL Acct Number</font>" HeaderStyle-Wrap="False">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "GL_Account_Number") %>
									</ItemTemplate>
									<FooterTemplate>
										<br>
										<asp:TextBox width="100%" TextMode="MultiLine" Rows="2" ID="add_GlAcctNumber" ReadOnly="true"
											CssClass="boxlookW" Runat="Server" /><br>
										<asp:RequiredFieldValidator Runat="server" id="Requiredfieldvalidator1" ControlToValidate="add_GlAcctNumber"
											ErrorMessage="GL Acct Number" Display="None" />
										<input type="hidden" id="add_GlAcctNumberValue" runat="server" NAME="add_GlAcctNumberValue" />
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn FooterStyle-VerticalAlign="Bottom" HeaderStyle-VerticalAlign="Top" ItemStyle-VerticalAlign="Top"
									HeaderText="<font color='white'>Gl Acct Description</font>" HeaderStyle-Wrap="False">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "GL_Account_Description") %>
									</ItemTemplate>
									<FooterTemplate>
										<asp:HyperLink id="lnk_add_GlAcctNumber" runat="server" ImageUrl="../images/up.gif" />
										<asp:TextBox width="100%" TextMode="MultiLine" Rows="2" ID="add_GlAcctDecription" ReadOnly="true"
											CssClass="boxlookW" Runat="Server" /><br>
										<asp:RequiredFieldValidator Runat="server" id="Requiredfieldvalidator2" ControlToValidate="add_GlAcctDecription"
											ErrorMessage="GL Acct Description" Display="None" />
										<input type="hidden" id="add_GlAcctDescriptionValue" runat="server" NAME="add_GlAcctDescriptionValue" />
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderStyle-VerticalAlign="Top" ItemStyle-VerticalAlign="Top" HeaderText="<font color='white'>Debit/Credit</font>"
									HeaderStyle-Wrap="False">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "Debit_Credit") %>
									</ItemTemplate>
									<FooterTemplate>
										<asp:DropDownList width="100%" class="boxlookW" id="add_DebitCredit" runat="server">
											<asp:ListItem Text="Credit" Value="C" />
											<asp:ListItem Text="Debit" Value="D" />
										</asp:DropDownList>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderStyle-VerticalAlign="Top" ItemStyle-VerticalAlign="Top" HeaderText="<font color='white'>Amount</font>"
									HeaderStyle-Wrap="False">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "Amount") %>
									</ItemTemplate>
									<FooterTemplate>
										<asp:TextBox width="100%" ID="add_Amount" CssClass="boxlookW" Runat="Server" />
									</FooterTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:DataGrid>
					</td>
				</tr>
				<tr>
					<td align="center">
						<asp:Label Runat="server" ID="lblMsg" CssClass="ClearTextBoxR" />
					</td>
				</tr>
			</table>
			<p></p>
			<center><a href="javascript:window.close()" class="boxlook">Close Window</a></center>
		</form>
		</FORM>
	</body>
</HTML>
