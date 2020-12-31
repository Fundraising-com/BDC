<%@ Page language="c#" Codebehind="OrderReports.aspx.cs" AutoEventWireup="false" Inherits="QSPFulfillment.OrderMgt.OrderReports" %>
<%@ Register  TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Register  TagPrefix="cc2" Namespace="QSPFulfillment.CommonWeb" Assembly="QSPFulfillment"  %>
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
			<p>
				<form id="Form1" method="post" runat="server">
					<!-- #include virtual="/Qspfulfillment/Includes/Menu.inc" -->
					<br>
					<STRONG><FONT size="3">Order Reports Printing and History<BR>
							<BR>
							<br>
						</FONT></STRONG>
					<asp:TextBox id="tbCriteria" runat="server"></asp:TextBox>&nbsp;
					<asp:DropDownList id="ddlbSearchBy" runat="server">
						<asp:ListItem Value="1" Selected="True">Order Id</asp:ListItem>
						<asp:ListItem Value="2">Campaign ID</asp:ListItem>
						<asp:ListItem Value="3">Group ID</asp:ListItem>
						<asp:ListItem Value="4">Group Name</asp:ListItem>
					</asp:DropDownList>
					<asp:Button id="Button1" runat="server" Text="Search"></asp:Button>
			</p>
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
						<asp:TemplateColumn HeaderText="Shipment Group" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center"
							ItemStyle-HorizontalAlign="Center">
							<ItemTemplate>
								<%# DataBinder.Eval(Container.DataItem,"ShipmentGroupName")%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center"
							ItemStyle-HorizontalAlign="Center">
							<ItemTemplate>
								<a style="text-decoration: true;font-size: xx-small;" href='BatchReportStatus.aspx?BatchOrderId=<%# DataBinder.Eval(Container.DataItem,"OrderId")%>&ShipmentGroupID=<%# DataBinder.Eval(Container.DataItem,"ShipmentGroupID")%>'>
									View Reports</a>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center"
							ItemStyle-HorizontalAlign="Center" Visible="False">
							<ItemTemplate>
								<a style="text-decoration: true;font-size: xx-small;" href="OrderReports.aspx">
									Generate New</a>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center"
							ItemStyle-HorizontalAlign="Center" Visible="False">
							<ItemTemplate>
								<asp:Literal visible=False ID="ltOrderID" Runat=server Text='<%# DataBinder.Eval(Container.DataItem,"OrderID")%>' />
								<asp:Literal visible=False ID="ltBatchID" Runat=server Text='<%# DataBinder.Eval(Container.DataItem,"BatchID")%>' />
								<asp:Literal visible=False ID="ltBatchDate" Runat=server Text='<%# DataBinder.Eval(Container.DataItem,"BatchDate")%>' />
								<cc2:RSGenerationLinkButton id="rsGenerationGroupSummaryReport" runat="server" CausesValidation="False" style="text-decoration: true;font-size: xx-small;" Text="Group Summary" />
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center"
							ItemStyle-HorizontalAlign="Center" Visible="False">
							<ItemTemplate>
								<cc2:RSGenerationLinkButton id="rsGenerationHomeroomSummaryReport" runat="server" CausesValidation="False" style="text-decoration: true;font-size: xx-small;" Text="Home Room Summary" />
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Language" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center"
							ItemStyle-HorizontalAlign="Center" Visible="False">
							<ItemTemplate>
								<asp:Label ID="lbLanguage" runat=server Text='<%# DataBinder.Eval(Container.DataItem,"Language")%>' />
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
				</asp:DataGrid>
			</p>
			<br>
			<STRONG><FONT size="3"><!-- TEMPORARILY REMOVED UNTIL NEW REPORTS ARE UP - QSP.CA Reports --><BR>
					<BR>
					<asp:DataGrid id="dgQSPCAList" runat="server" AutoGenerateColumns="False" BorderStyle="Solid"
						BorderColor="Black" BorderWidth="1px">
						<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
						<ItemStyle Font-Size="XX-Small"></ItemStyle>
						<HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" BackColor="#FFFFCC"></HeaderStyle>
						<Columns>
						<asp:TemplateColumn HeaderText="Report" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center"
							ItemStyle-HorizontalAlign="Center" Visible="True">
							<ItemTemplate>
								<asp:Label ID="Label1" runat=server Text='<%# DataBinder.Eval(Container.DataItem,"MenuTitle")%>' />
							</ItemTemplate>
						</asp:TemplateColumn>						
											
						<asp:TemplateColumn HeaderText="" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center"
							ItemStyle-HorizontalAlign="Center" Visible="True">
							<ItemTemplate>
								<asp:HyperLink Target=_blank ID="Hyperlink1" runat=server style="text-decoration: true;font-size: xx-small;" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"Link")%>' Text="View Report" />
							</ItemTemplate>
						</asp:TemplateColumn>							
						</Columns>
					</asp:DataGrid>
					<BR>
					<br>
				</FONT></STRONG>
			</FORM>
		</CENTER>
	</body>
</HTML>
