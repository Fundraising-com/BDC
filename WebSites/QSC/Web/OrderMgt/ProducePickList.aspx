<%@ Page language="c#" Codebehind="ProducePickList.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.OrderMgt.ProducePickList" %>
<%@ Register  TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BatchList</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="../Includes/QSPFulfillment.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body leftmargin=0 topmargin=0>
		<CENTER>
			<form id="Form1" method="post" runat="server">
			<!-- #include virtual="/Qspfulfillment/Includes/Menu.inc" -->
				<P>
					<STRONG><FONT size="3">Orders Available for Picking</FONT></STRONG></P>
				<P>Choose Distribution Center:
					<asp:DropDownList id="ddDC" runat="server" CssClass="boxLookW" DataTextField="Name" DataValueField="Id"></asp:DropDownList>
					<BR>
				</P>
				<asp:DataGrid id="DataGrid1" runat="server" AutoGenerateColumns="False" BorderStyle="Solid" BorderColor="black"
					BorderWidth="1px">
					<HeaderStyle BackColor="#ffffcc" Font-Size="xx-small" Font-Name="Verdana" Font-Bold="True"></HeaderStyle>
					<ItemStyle Font-Size="xx-small"></ItemStyle>
					<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
					<Columns>
						<asp:TemplateColumn HeaderText="Order ID" HeaderStyle-Font-Bold="True" HeaderStyle-VerticalAlign="Bottom"
							HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
							<ItemTemplate>
								<a href='/QSPFulfillment/OrderMgt/BatchViewer.aspx?BatchOrderId=<%# DataBinder.Eval(Container.DataItem,"OrderId")%>' target=_blank>
									<%# DataBinder.Eval(Container.DataItem,"OrderId")%>
									<input type=hidden name="HOrderId" id="HOrderId" value='<%# DataBinder.Eval(Container.DataItem,"OrderId")%>' runat=server>
								</a>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Date Order<BR>Received" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center"
							ItemStyle-HorizontalAlign="Center">
							<ItemTemplate>
								<%# DataBinder.Eval(Container.DataItem,"DateOrdered")%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Campaign Id" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center"
							ItemStyle-HorizontalAlign="Center">
							<ItemTemplate>
								<%# DataBinder.Eval(Container.DataItem,"CampaignId")%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Order Type" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center"
							ItemStyle-HorizontalAlign="Center">
							<ItemTemplate>
								<%# DataBinder.Eval(Container.DataItem,"OrderType")%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Estimated<BR>Order Amount" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center"
							ItemStyle-HorizontalAlign="Center">
							<ItemTemplate>
								<%# DataBinder.Eval(Container.DataItem,"ItemTotalCost", "{0:c}")%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Item Quantity" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center"
							ItemStyle-HorizontalAlign="Center">
							<ItemTemplate>
								<%# DataBinder.Eval(Container.DataItem,"ItemQuantity")%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Qualifier" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center"
							ItemStyle-HorizontalAlign="Center">
							<ItemTemplate>
								<%# DataBinder.Eval(Container.DataItem,"OrderQualifier")%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Select<BR>For<BR>Picking" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center"
							ItemStyle-HorizontalAlign="Center">
							<ItemTemplate>
								<asp:CheckBox id="CheckBox1" runat="server" Font-Size="xx-small"></asp:CheckBox>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
				</asp:DataGrid><BR>
				<asp:Button id="Button1" runat="server" Text="Print Picklists and Reports" CssClass="Button4" onclick="Button1_Click"></asp:Button>
			</form>
		</CENTER>
	</body>
</HTML>
