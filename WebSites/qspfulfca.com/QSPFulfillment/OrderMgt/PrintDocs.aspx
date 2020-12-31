<%@ Register TagPrefix="UC" TagName="OrderQualifier" Src="UC/OrderQualifier.ascx" %>
<%@ Register TagPrefix="UC" TagName="ShipmentGroup" Src="UC/ShipmentGroup.ascx" %>
<%@ Register TagPrefix="UC" TagName="Date" Src="../Common/DateEntry.ascx" %>
<%@ Register  TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CommonWeb" Assembly="QSPFulfillment"  %>
<%@ Page language="c#" Codebehind="PrintDocs.aspx.cs" AutoEventWireup="false" Inherits="QSPFulfillment.OrderMgt.PrintDocs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BatchList</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Includes/QSPFulfillment.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body leftMargin="0" topMargin="0">
		<center>
			<form id="Form1" method="post" runat="server">
				<!-- #include file="../Includes/Menu.inc" -->
				<!--#include file="../CustomerService/fctjavascriptall.js"-->
				<p><STRONG><FONT size="3">Orders Available for Printing</FONT></STRONG></p>
				<asp:label id="Label7" style="Z-INDEX: 100; LEFT: 160px; POSITION: absolute; TOP: 94px" runat="server"
					cssclass="Font8BoldVBlue" font-names="Verdana" font-size="X-Small" font-bold="True">Account ID</asp:label><asp:textbox id="fAccountID" style="Z-INDEX: 101; LEFT: 248px; POSITION: absolute; TOP: 94px"
					runat="server" width="86px"></asp:textbox><asp:label id="Label8" style="Z-INDEX: 102; LEFT: 384px; POSITION: absolute; TOP: 94px" runat="server"
					cssclass="Font8BoldVBlue" font-names="Verdana" font-size="X-Small" font-bold="True">Campaign ID</asp:label><asp:textbox id="fCampaignID" style="Z-INDEX: 103; LEFT: 480px; POSITION: absolute; TOP: 94px"
					runat="server" width="86px"></asp:textbox><asp:label id="Label5" style="Z-INDEX: 104; LEFT: 174px; POSITION: absolute; TOP: 126px" runat="server"
					cssclass="Font8BoldVBlue" font-names="Verdana" font-size="X-Small" font-bold="True" Height="16px">Order ID</asp:label><asp:textbox id="fOrderID" style="Z-INDEX: 105; LEFT: 248px; POSITION: absolute; TOP: 126px"
					runat="server" width="91px"></asp:textbox><asp:label id="Label6" style="Z-INDEX: 106; LEFT: 407px; POSITION: absolute; TOP: 126px" runat="server"
					cssclass="Font8BoldVBlue" font-names="Verdana" font-size="X-Small" font-bold="True" Width="65px">Qualifier</asp:label><asp:label id="Label4" style="Z-INDEX: 107; LEFT: 97px; POSITION: absolute; TOP: 158px" runat="server"
					cssclass="Font8BoldVBlue" font-names="Verdana" font-size="X-Small" font-bold="True">Date Received From</asp:label><asp:label id="Label9" style="Z-INDEX: 108; LEFT: 455px; POSITION: absolute; TOP: 158px" runat="server"
					cssclass="Font8BoldVBlue" font-names="Verdana" font-size="X-Small" font-bold="True">To</asp:label><asp:label id="Label10" style="Z-INDEX: 107; LEFT: 120px; POSITION: absolute; TOP: 185px" runat="server"
					cssclass="Font8BoldVBlue" font-names="Verdana" font-size="X-Small" font-bold="True">Shipment Group</asp:label><asp:label id="Label11" style="Z-INDEX: 107; LEFT: 425px; POSITION: absolute; TOP: 185px" runat="server"
					cssclass="Font8BoldVBlue" font-names="Verdana" font-size="X-Small" font-bold="True">Has Shipment</asp:label><asp:panel id="PanelQualifier" style="Z-INDEX: 113; LEFT: 480px; POSITION: absolute; TOP: 126px"
					runat="server" width="176px" height="24px">
					<UC:ORDERQUALIFIER id="ucOHOrderQualifier" runat="server" cssclass="boxlookW" AllQualifiersOption="true"></UC:ORDERQUALIFIER>
				</asp:panel><asp:panel id="Panel2" style="Z-INDEX: 114; LEFT: 248px; POSITION: absolute; TOP: 158px" runat="server"
					width="144px" height="24px">
					<UC:DATE id="fFromDate" runat="server" required="True"></UC:DATE>
				</asp:panel><asp:panel id="Panel3" style="Z-INDEX: 115; LEFT: 480px; POSITION: absolute; TOP: 158px" runat="server"
					width="128px" height="24px" enableviewstate="False">
					<UC:DATE id="fToDate" runat="server" required="True"></UC:DATE>
				</asp:panel>
				<asp:button id="Button3" style="Z-INDEX: 117; LEFT: 675px; POSITION: absolute; TOP: 160px" runat="server"
					width="96px" text="Search"></asp:button>
                <asp:panel id="PanelShipmentGroup" style="Z-INDEX: 113; LEFT: 248px; POSITION: absolute; TOP: 186px"
					runat="server" width="156px" height="24px">
					<UC:ShipmentGroup id="ucShipmentGroup" runat="server" cssclass="boxlookW" AllShipmentGroupsOption="true"></UC:ShipmentGroup>
				</asp:panel>
                <asp:panel id="PanelHasShipment" style="Z-INDEX: 113; LEFT: 518px; POSITION: absolute; TOP: 186px"
					runat="server" width="156px" height="24px">
					<asp:dropdownlist id="ddlHasShipment" runat="server" font-size="XX-Small" width="92px">
												<asp:listitem value="-1">All</asp:listitem>
												<asp:listitem value="1">Yes</asp:listitem>
												<asp:listitem value="0">No</asp:listitem>
											</asp:dropdownlist>
				</asp:panel>
				<p id="P1">&nbsp;</p>
				<P>&nbsp;</P>
				<P>&nbsp;</P>
				<P>&nbsp;</P>
				<P>&nbsp;</P>
				<asp:datagrid id="DataGrid1" runat="server" borderwidth="1px" bordercolor="Black" borderstyle="Solid"
					autogeneratecolumns="False">
					<alternatingitemstyle backcolor="WhiteSmoke"></alternatingitemstyle>
					<itemstyle font-size="XX-Small"></itemstyle>
					<headerstyle font-size="XX-Small" font-names="Verdana" font-bold="True" backcolor="#FFFFCC"></headerstyle>
					<columns>
						<asp:templatecolumn headertext="Order ID">
							<headerstyle font-bold="True" horizontalalign="Center" verticalalign="Bottom"></headerstyle>
							<itemstyle horizontalalign="Center"></itemstyle>
							<itemtemplate>
								<a href='/QSPFulfillment/OrderMgt/BatchViewer.aspx?BatchOrderId=<%# DataBinder.Eval(Container.DataItem,"OrderId")%>' target=_blank>
									<%# DataBinder.Eval(Container.DataItem,"OrderId")%>
									<input type=hidden name="HOrderId" id="HOrderId" value='<%# DataBinder.Eval(Container.DataItem,"OrderId")%>' runat=server>
								</a>
							</itemtemplate>
						</asp:templatecolumn>
						<asp:templatecolumn headertext="Account Name">
							<headerstyle horizontalalign="Center" verticalalign="Bottom"></headerstyle>
							<itemstyle horizontalalign="Center"></itemstyle>
							<itemtemplate>
								<%# DataBinder.Eval(Container.DataItem,"AccountName")%>
							</itemtemplate>
						</asp:templatecolumn>
						<asp:templatecolumn headertext="Campaign Id">
							<headerstyle horizontalalign="Center" verticalalign="Bottom"></headerstyle>
							<itemstyle horizontalalign="Center"></itemstyle>
							<itemtemplate>
								<%# DataBinder.Eval(Container.DataItem,"CampaignId")%>
							</itemtemplate>
						</asp:templatecolumn>
						<asp:templatecolumn headertext="Order Type">
							<headerstyle horizontalalign="Center" verticalalign="Bottom"></headerstyle>
							<itemstyle horizontalalign="Center"></itemstyle>
							<itemtemplate>
								<%# DataBinder.Eval(Container.DataItem,"OrderType")%>
							</itemtemplate>
						</asp:templatecolumn>
						<asp:templatecolumn headertext="Qualifier">
							<headerstyle horizontalalign="Center" verticalalign="Bottom"></headerstyle>
							<itemstyle horizontalalign="Center"></itemstyle>
							<itemtemplate>
								<%# DataBinder.Eval(Container.DataItem,"OrderQualifier")%>
							</itemtemplate>
						</asp:templatecolumn>
						<asp:templatecolumn headertext="Shipment Group">
							<headerstyle horizontalalign="Center" verticalalign="Bottom"></headerstyle>
							<itemstyle horizontalalign="Center"></itemstyle>
							<itemtemplate>
								<%# DataBinder.Eval(Container.DataItem,"ShipmentGroupName")%>
								<input type=hidden name="HShipmentGroupID" id="HShipmentGroupID" value='<%# DataBinder.Eval(Container.DataItem,"ShipmentGroupID")%>' runat=server>
							</itemtemplate>
						</asp:templatecolumn>
						<asp:templatecolumn headertext="Has Shipment">
							<headerstyle horizontalalign="Center" verticalalign="Bottom"></headerstyle>
							<itemstyle horizontalalign="Center"></itemstyle>
							<itemtemplate>
								<%# DataBinder.Eval(Container.DataItem,"HasShipment")%>
							</itemtemplate>
						</asp:templatecolumn>
						<asp:templatecolumn headertext="Language">
							<headerstyle font-bold="True" horizontalalign="Center" verticalalign="Bottom"></headerstyle>
							<itemtemplate>
								<asp:Label id=Label1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Lang") %>'>
								</asp:Label>
							</itemtemplate>
						</asp:templatecolumn>
						<asp:templatecolumn headertext="Programs">
							<headerstyle font-bold="True" horizontalalign="Center" verticalalign="Bottom"></headerstyle>
							<itemtemplate>
								<asp:Label id=Label2 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Programs") %>'>
								</asp:Label>
							</itemtemplate>
						</asp:templatecolumn>
						<asp:templatecolumn headertext="Order Delivery Date">
							<headerstyle font-bold="True" horizontalalign="Center" verticalalign="Bottom"></headerstyle>
							<itemstyle horizontalalign="Center"></itemstyle>
							<itemtemplate>
								<asp:Label id="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.OrderDeliveryDate") %>'>
								</asp:Label>
							</itemtemplate>
						</asp:templatecolumn>
						<asp:templatecolumn headertext="Order Shipping Date">
							<headerstyle font-bold="True" horizontalalign="Center" verticalalign="Bottom"></headerstyle>
							<itemstyle horizontalalign="Center"></itemstyle>
							<itemtemplate>
								<asp:Label id="LabelOrderShippingDate" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.OrderShippingDate") %>'>
								</asp:Label>
							</itemtemplate>
						</asp:templatecolumn>
						<asp:templatecolumn headertext="Order Received Date">
							<headerstyle font-bold="True" horizontalalign="Center" verticalalign="Bottom"></headerstyle>
							<itemstyle horizontalalign="Center"></itemstyle>
							<itemtemplate>
								<asp:Label id="OrderReceivedLabel" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.OrderReceivedDate") %>'>
								</asp:Label>
							</itemtemplate>
						</asp:templatecolumn>
						<asp:templatecolumn headertext="Select&lt;BR&gt;For&lt;BR&gt;Printing">
							<headerstyle horizontalalign="Center" verticalalign="Bottom"></headerstyle>
							<itemstyle horizontalalign="Center"></itemstyle>
							<itemtemplate>
								<asp:checkbox id="CheckBox1" runat="server" font-size="xx-small"></asp:checkbox>
							</itemtemplate>
						</asp:templatecolumn>
					</columns>
				</asp:datagrid>
				<P><asp:button id="Button1" runat="server" cssclass="Button4" text="Print Documents" Height="24px"
						Width="120px"></asp:button>
                    <asp:button id="Button8" runat="server" cssclass="Button4" text="Print Labels" Height="24px"
						Width="120px"></asp:button>
                    <asp:button id="Button2" runat="server" cssclass="Button4" text="Mark as Printed" Height="24px"
						Width="120px"></asp:button></P>
				<p>&nbsp;</p>
				<cc2:pdfstoremerger id="PDFStoreMergerPrintDocs" runat="server"></cc2:pdfstoremerger><asp:validationsummary id="ValidationSummary1" runat="server" showsummary="False" showmessagebox="True"></asp:validationsummary></form>
		</center>
		<!--#include file="../CustomerService/errorwindow.js"-->
	</body>
</HTML>
