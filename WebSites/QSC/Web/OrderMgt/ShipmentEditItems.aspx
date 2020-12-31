<%@ Page language="c#" Codebehind="ShipmentEditItems.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.OrderMgt.ShipmentEditItems" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>QSP Canada Fulfillment - Edit Order Items</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Includes/QSPFulfillment.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<CENTER>
			<form id="Form1" method="post" runat="server">
				<P><STRONG><FONT size="3">Edit Order Items for Shipping</FONT></STRONG></P>
				<table class="boxlook" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<tr>
						<td style="HEIGHT: 13px" bgColor="#ffffcc"><STRONG><FONT face="Verdana" size="2">Order 
									Information</FONT></STRONG></td>
					</tr>
					<tr>
						<td bgColor="#ffffff">
							<table cellSpacing="0" cellPadding="0" width="100%" border="0">
								<tr bgColor="ghostwhite">
									<td vAlign="top"><STRONG><FONT size="2"><FONT face="Verdana">Order Id:</FONT> </FONT></STRONG>
									</td>
									<td vAlign="top"><STRONG><FONT size="2"><FONT face="Verdana">Ship To Group Id:</FONT> </FONT>
										</STRONG>
									</td>
									<td vAlign="top"><STRONG><FONT size="2"><FONT face="Verdana">For:</FONT> </FONT></STRONG>
									</td>
									<td vAlign="top"><STRONG><FONT size="2"><FONT face="Verdana">Ship To FMId:</FONT> </FONT>
										</STRONG>
									</td>
									<td vAlign="top"><STRONG><FONT size="2"><FONT face="Verdana">Campaign Id:</FONT> </FONT>
										</STRONG>
									</td>
								</tr>
								<tr>
									<td><asp:label id="lblOrderId" runat="server" Font-Size="XX-Small"></asp:label></td>
									<td><asp:label id="lblShipToGroupId" runat="server" Font-Size="XX-Small"></asp:label></td>
									<td><asp:label id="lblFor" runat="server" Font-Size="XX-Small"></asp:label></td>
									<td><asp:label id="lblFMId" runat="server" Font-Size="XX-Small"></asp:label></td>
									<td><asp:label id="lblCampaignId" runat="server" Font-Size="XX-Small"></asp:label></td>
								</tr>
							</table>
							<BR>
							<asp:label id="lblMessage" runat="server" Font-Size="X-Small" Font-Names="Verdana" ForeColor="#00C000"
								Font-Bold="True"></asp:label></td>
					</tr>
					<tr>
						<td bgColor="#ffffcc"><STRONG><FONT face="Verdana" size="2">Item Information</FONT></STRONG></td>
					</tr>
					<tr>
						<td bgColor="#ffffff"><asp:datagrid id="DataGrid1" runat="server" AutoGenerateColumns="False" BorderStyle="Solid" BorderColor="black"
								BorderWidth="1px" Width="100%">
								<HeaderStyle BackColor="#ffffcc" Font-Size="xx-small" Font-Name="Verdana" Font-Bold="True"></HeaderStyle>
								<ItemStyle Font-Size="xx-small"></ItemStyle>
								<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="Order Id" HeaderStyle-Font-Bold="True" HeaderStyle-VerticalAlign="Bottom"
										HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
										<ItemTemplate>
											<%# DataBinder.Eval(Container.DataItem,"COHInstance")%>
											-
											<%# DataBinder.Eval(Container.DataItem,"TransId")%>
											<input type=hidden name="HCOHInstance" id="HCOHInstance" value='<%# DataBinder.Eval(Container.DataItem,"COHInstance")%>' runat=server>
											<input type=hidden name="HTransId" id="HTransId" value='<%# DataBinder.Eval(Container.DataItem,"TransId")%>' runat=server>
											<input type=hidden name="HIsEditable" id="HIsEditable" value='<%# DataBinder.Eval(Container.DataItem,"IsEditable")%>' runat=server>
											<input type=hidden name="HIsFromThisSession" id="HIsFromThisSession" value='<%# DataBinder.Eval(Container.DataItem,"IsFromThisSession")%>' runat=server>
											</a>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Product Code" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center"
										ItemStyle-HorizontalAlign="Center">
										<ItemTemplate>
											<%# DataBinder.Eval(Container.DataItem,"ProductCode")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Qty Ordered " HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center"
										ItemStyle-HorizontalAlign="Center">
										<ItemTemplate>
											<%# DataBinder.Eval(Container.DataItem,"QtyOrdered")%>
											<input type=hidden name="HQtyOrdered" id="HQtyOrdered" value='<%# DataBinder.Eval(Container.DataItem,"QtyOrdered")%>' runat=server>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Qty Shipped" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center"
										ItemStyle-HorizontalAlign="Center">
										<ItemTemplate>
											<asp:Label id="lblQuantityShipped" runat="server">
												<%# DataBinder.Eval(Container.DataItem,"QtyShipped")%>
											</asp:Label>
											<asp:TextBox id="tbQuantityShipped" runat="server" CssClass="TextBoxNumeric" Width="48px"></asp:TextBox>
											<input type=hidden name="HOriginalQuantityShipped" id="HOriginalQuantityShipped" value='<%# DataBinder.Eval(Container.DataItem,"QtyShipped")%>' runat=server>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Replacement Qty" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center"
										ItemStyle-HorizontalAlign="Center">
										<ItemTemplate>
											<asp:Label id="lblReplacementQty" runat="server"></asp:Label>
											<asp:TextBox id="tbReplacementQty" runat="server" CssClass="TextBoxNumeric" Width="48px"></asp:TextBox>
											<input type=hidden name="HOriginalQuantityReplaced" id="HOriginalQuantityReplaced" value='<%# DataBinder.Eval(Container.DataItem,"QtyReplaced")%>' runat=server>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Replacement SAP Material Number" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center"
										ItemStyle-HorizontalAlign="Center">
										<ItemTemplate>
											<asp:Label id="lblReplacementItem" runat="server"></asp:Label>
                                            <asp:TextBox id="tbReplacementItem" runat="server" CssClass="TextBox" Width="88px"></asp:TextBox>
											<input type=hidden name="HReplacementItemId" id="HReplacementItemId" value='<%# DataBinder.Eval(Container.DataItem,"ReplacementItemId")%>' runat=server>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Ship Item?" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center"
										ItemStyle-HorizontalAlign="Center">
										<ItemTemplate>
											<asp:CheckBox id="cbShipItem" runat="server" Font-Size="xx-small"></asp:CheckBox>
											<input type=hidden name="HShipTF" id="HShipTF" value='<%# DataBinder.Eval(Container.DataItem,"ShipItemTF")%>' runat=server>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Comment" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center"
										ItemStyle-HorizontalAlign="Center">
										<ItemTemplate>
											<asp:Label id="lblComment" runat="server"></asp:Label>
											<asp:TextBox id="tbComment" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
											<asp:Label id="HComment" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Customer Comment" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center"
										ItemStyle-HorizontalAlign="Center">
										<ItemTemplate>
											<asp:Label id="lblCustomerComment" runat="server"></asp:Label>
											<asp:TextBox id="tbCustomerComment" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
											<asp:Label id="HCustomerComment" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Split Qty" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center"
										ItemStyle-HorizontalAlign="Center">
										<ItemTemplate>
											<asp:TextBox id="tbSplitQuantity" runat="server" CssClass="TextBoxNumeric" Width="48px"></asp:TextBox>
											<asp:button id="btnSplit" runat="server" CssClass="Button4" Text="Split Item" CommandName="SplitCOD" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"COHInstance") + "," + DataBinder.Eval(Container.DataItem,"TransId")%>' Font-Size="xx-small" Width="60px">
											</asp:button>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:datagrid><BR>
						</td>
					</tr>
				</table>
				<BR>
				<asp:label id="lblOrderIdHidden" runat="server" Visible="False"></asp:label>
                <asp:label id="lblShipmentGroupIDHidden" runat="server" Visible="False"></asp:label>
                <asp:button id="Button1" runat="server" Text="Save Changes" CssClass="Button4" onclick="Button1_Click"></asp:button></form>
		</CENTER>
	</body>
</HTML>
