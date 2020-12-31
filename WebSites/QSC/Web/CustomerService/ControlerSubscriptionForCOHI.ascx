<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CustomerService" Assembly="QSPFulfillment" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ControlerSubscriptionForCOHI.ascx.cs" Inherits="QSPFulfillment.CustomerService.ControlerSubscriptionForCOHI" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>

<TABLE cellSpacing="0" width="100%" border="0">
	<TBODY>
		<tr>
			<td><asp:label id="lblMessage" runat="server"></asp:label><cc2:datagridobject id="dtgMain" runat="server" SearchMode="0" AutoGenerateColumns="False" ShowFooter="True"
					width="100%" BorderStyle="None" GridLines="None" cssClass="CSTableSubHeader" PageSize="300">
					<ItemStyle CssClass="CSTableSubHeader"></ItemStyle>
					<HeaderStyle Font-Bold="True" CssClass="CSTableSubHeader"></HeaderStyle>
					<Columns>
						<asp:TemplateColumn>
							<HeaderTemplate>
								<input id="chkAllItems" runat="server" type="checkbox" checked="true" />
							</HeaderTemplate>
							<ItemTemplate>
								<asp:CheckBox Runat="server" ID="chkSelect" Checked="True"></asp:CheckBox>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Price">
							<ItemTemplate>
								<asp:Label ID="lblPrice" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Price") %>'>
								</asp:Label>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox ID="tbxPrice" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Price") %>'>
								</asp:TextBox>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:BoundColumn DataField="CustomerOrderHeaderInstance" HeaderText="COH Instance">
							<HeaderStyle Wrap="False"></HeaderStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="TransID" HeaderText="Trans ID">
							<HeaderStyle Wrap="False"></HeaderStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="SubscriptionDate" HeaderText="Subscription Date" DataFormatString="{0:MM/dd/yyyy}">
							<HeaderStyle Wrap="False"></HeaderStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="ProductCode" HeaderText="Title Code">
							<HeaderStyle Wrap="False"></HeaderStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="ProductName" HeaderText=" Title">
							<HeaderStyle Wrap="False"></HeaderStyle>						
                        </asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="Price to Charge">
							<ItemTemplate>
                               <asp:TextBox ID="tbxPriceToCharge"  runat="server" Text='<%# Convert.ToInt32(DataBinder.Eval(Container, "DataItem.Price")) %>'>
								</asp:TextBox>
							</ItemTemplate>							
						</asp:TemplateColumn>                        
					</Columns>
				</cc2:datagridobject></td>
		</tr>
	</TBODY>
</TABLE>
