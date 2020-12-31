<%@ Register  TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Page language="c#" Codebehind="OrderReports2.aspx.cs" AutoEventWireup="false" Inherits="QSPFulfillment.OrderMgt.OrderReports2" %>
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
	<body leftmargin="0" topmargin="0">
		<CENTER>
			<form id="Form1" method="post" runat="server">
				<!-- #include virtual="/Qspfulfillment/Includes/Menu.inc" -->
				<P>
					<STRONG><FONT size="3">Report Printing and History<BR>
							<BR>
						</FONT></STRONG>
					<asp:TextBox id="tbCriteria" runat="server"></asp:TextBox>&nbsp;
					<asp:DropDownList id="ddlbSearchBy" runat="server">
						<asp:ListItem Value="1" Selected="True">Order Id</asp:ListItem>
						<asp:ListItem Value="2">Campaign ID</asp:ListItem>
					</asp:DropDownList>
					<asp:Button id="Button1" runat="server" Text="Search"></asp:Button>
				<p>
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
							<asp:TemplateColumn HeaderText="Account Name" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center"
								ItemStyle-HorizontalAlign="Center">
								<ItemTemplate>
									<%# DataBinder.Eval(Container.DataItem,"AccountName")%>
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
									<%# DataBinder.Eval(Container.DataItem,"OrderTypeCodeDesc")%>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Qualifier" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center"
								ItemStyle-HorizontalAlign="Center">
								<ItemTemplate>
									<%# DataBinder.Eval(Container.DataItem,"OrderQualifierDesc")%>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center"
								ItemStyle-HorizontalAlign="Center">
								<ItemTemplate>
									<a style="text-decoration: true;font-size: xx-small;" href='ReportStatus.aspx?BatchOrderId=<%# DataBinder.Eval(Container.DataItem,"OrderId")%>'>
										REPORT STATUS</a>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center"
								ItemStyle-HorizontalAlign="Center">
								<ItemTemplate>
									<a style="text-decoration: true;font-size: xx-small;" href='PrintReports.aspx?BatchOrderId=<%# DataBinder.Eval(Container.DataItem,"OrderId")%>'>
										PRINT NEW</a>
								</ItemTemplate>
							</asp:TemplateColumn>
							
							<asp:TemplateColumn HeaderText="" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center"
								ItemStyle-HorizontalAlign="Center">
								<ItemTemplate>
									<asp:Literal visible=False ID="ltOrderID" Runat=server Text='<%# DataBinder.Eval(Container.DataItem,"OrderID")%>' />
									<asp:Literal visible=False ID="ltBatchID" Runat=server Text='<%# DataBinder.Eval(Container.DataItem,"BatchID")%>' />
									<asp:Literal visible=False ID="ltBatchDate" Runat=server Text='<%# DataBinder.Eval(Container.DataItem,"BatchDate")%>' />
									<asp:HyperLink ID="hlGroupSummaryRpt" runat=server style="text-decoration: true;font-size: xx-small;"     NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"OrderId")%>' Text="Group Summary"  />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center"
								ItemStyle-HorizontalAlign="Center">
								<ItemTemplate>
									<asp:HyperLink ID="hlHomeRoomSummaryRpt" runat=server style="text-decoration: true;font-size: xx-small;"  NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"OrderId")%>' Text="Home Room Summary" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Language" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
								<ItemTemplate>
									<asp:Label ID="lbLanguage" runat=server Text='<%# DataBinder.Eval(Container.DataItem,"Language")%>' />
								</ItemTemplate>
							</asp:TemplateColumn>
						</Columns>
					</asp:DataGrid>
				</p>
			</form>
		</CENTER>
	</body>
</HTML>
