<%@ Register  TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.OrderMgt" Assembly="QSPFulfillment" %>
<%@ Register TagPrefix="cc3" Namespace="QSPFulfillment.OrderMgt" Assembly="QSPFulfillment" %>
<%@ Register TagPrefix="cc4" Namespace="QSPFulfillment.OrderMgt" Assembly="QSPFulfillment" %>
<%@ Page language="c#" Codebehind="OrderHistory.aspx.cs" AutoEventWireup="false" Inherits="QSPFulfillment.OrderMgt.OrderHistory" %>
<%@ Register TagPrefix="UC" TagName="Date" Src="../Common/DateEntry.ascx" %>
<%@ Register TagPrefix="UC" TagName="OrderQualifier" Src="../OrderMgt/UC/OrderQualifier.ascx" %>
<%@ Register TagPrefix="UC" TagName="OrderStatus" Src="../OrderMgt/UC/OrderStatus.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>Order History</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
  </HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" onload="return window_onunload();"
		rightMargin="0" marginheight="0" marginwidth="0">
		<!--#include file="../CustomerService/fctjavascriptall.js"-->
		<form id="Form1" method="post" runat="server">
			<!-- #include file="../Includes/Menu.inc" --><asp:label id="Label7" style="Z-INDEX: 100; LEFT: 160px; POSITION: absolute; TOP: 94px" runat="server"
				cssclass="Font8BoldVBlue" font-bold="True" font-size="X-Small" font-names="Verdana">Account ID</asp:label><asp:textbox id="fAccountID" style="Z-INDEX: 101; LEFT: 248px; POSITION: absolute; TOP: 94px"
				runat="server" width="86px"></asp:textbox><asp:label id="Label8" style="Z-INDEX: 102; LEFT: 384px; POSITION: absolute; TOP: 94px" runat="server"
				cssclass="Font8BoldVBlue" font-bold="True" font-size="X-Small" font-names="Verdana">Campaign ID</asp:label><asp:textbox id="fCampaignID" style="Z-INDEX: 103; LEFT: 480px; POSITION: absolute; TOP: 94px"
				runat="server" width="86px"></asp:textbox><asp:label id="Label5" style="Z-INDEX: 104; LEFT: 174px; POSITION: absolute; TOP: 134px" runat="server"
				cssclass="Font8BoldVBlue" font-bold="True" font-size="X-Small" font-names="Verdana">Order ID</asp:label><asp:textbox id="fOrderID" style="Z-INDEX: 105; LEFT: 248px; POSITION: absolute; TOP: 126px"
				runat="server" width="91px"></asp:textbox><asp:label id="Label6" style="Z-INDEX: 106; LEFT: 431px; POSITION: absolute; TOP: 126px" runat="server"
				cssclass="Font8BoldVBlue" font-bold="True" font-size="X-Small" font-names="Verdana">Status</asp:label><asp:label id="Label2" style="Z-INDEX: 107; LEFT: 97px; POSITION: absolute; TOP: 158px" runat="server"
				cssclass="Font8BoldVBlue" font-bold="True" font-size="X-Small" font-names="Verdana">Date Received From</asp:label><asp:label id="Label3" style="Z-INDEX: 108; LEFT: 455px; POSITION: absolute; TOP: 158px" runat="server"
				cssclass="Font8BoldVBlue" font-bold="True" font-size="X-Small" font-names="Verdana">To</asp:label><asp:label id="Label4" style="Z-INDEX: 109; LEFT: 152px; POSITION: absolute; TOP: 190px" runat="server"
				cssclass="Font8BoldVBlue" font-bold="True" font-size="X-Small" font-names="Verdana" height="5px">Ware House</asp:label><asp:dropdownlist id="fWareHouse" style="Z-INDEX: 112; LEFT: 248px; POSITION: absolute; TOP: 190px"
				runat="server" cssclass="boxLookW" width="126px" datavaluefield="Id" datatextfield="Name">
				<asp:listitem value="0" selected="True">All</asp:listitem>
				<asp:listitem value="1">QSP</asp:listitem>
				<asp:listitem value="2">Unigistix</asp:listitem>
			</asp:dropdownlist>
			<asp:label id="LabelQualifier" style="Z-INDEX: 112; LEFT: 410px; POSITION: absolute; TOP: 190px"
				runat="server" cssclass="Font8BoldVBlue" font-bold="True" font-size="X-Small" font-names="Verdana">Qualifier</asp:label>
			<asp:panel id="Panel1" style="Z-INDEX: 113; LEFT: 480px; POSITION: absolute; TOP: 126px" runat="server"
				width="176px" height="24px">
<uc:orderstatus id=ucOHOrderStatus runat="server" cssclass="boxlookW" allstatusesoption="true"></uc:orderstatus>
			</asp:panel><asp:panel id="Panel2" style="Z-INDEX: 114; LEFT: 248px; POSITION: absolute; TOP: 158px" runat="server"
				width="144px" height="24px">
<uc:date id=fFromDate runat="server" required="True"></uc:date>
			</asp:panel><asp:panel id="Panel3" style="Z-INDEX: 115; LEFT: 480px; POSITION: absolute; TOP: 158px" runat="server"
				width="128px" height="24px" enableviewstate="False">
<uc:date id=fToDate runat="server" required="True"></uc:date>
			</asp:panel>
			<asp:panel id="PanelQualifier" style="Z-INDEX: 113; LEFT: 480px; POSITION: absolute; TOP: 190px"
				runat="server" width="154px" height="24px">
<uc:orderqualifier id=ucOHOrderQualifier runat="server" cssclass="boxlookW" AllQualifiersOption="true"></uc:orderqualifier>
			</asp:panel>
			<div style="Z-INDEX: 116; LEFT: 304px; WIDTH: 232px; POSITION: absolute; TOP: 38px; HEIGHT: 40px"
				ms_positioning="FlowLayout">
				<p><font face="Verdana" color="#2f4f88" size="5">Order History</font></p>
			</div>
			<asp:button id="Button1" style="Z-INDEX: 117; LEFT: 675px; POSITION: absolute; TOP: 190px" runat="server"
				width="96px" text="Search"></asp:button>
			<asp:datagrid id="DataGrid1" style="Z-INDEX: 118; LEFT: 104px; POSITION: absolute; TOP: 238px"
				runat="server" font-size="X-Small" width="480px" height="104px" autogeneratecolumns="False"
				borderstyle="Solid" bordercolor="Black" borderwidth="1px" allowsorting="True" allowpaging="True"
				pagesize="15">
				<alternatingitemstyle backcolor="WhiteSmoke"></alternatingitemstyle>
				<itemstyle font-size="X-Small" horizontalalign="Left"></itemstyle>
				<headerstyle font-size="X-Small" font-names="Verdana" font-bold="True" horizontalalign="Center"
					verticalalign="Bottom" backcolor="#FFFFCC"></headerstyle>
				<columns>
					<asp:templatecolumn headertext="Order ID">
						<itemstyle wrap="False" horizontalalign="Center"></itemstyle>
						<itemtemplate>
							<a href='/QSPFulfillment/OrderMgt/KanataOrderEntry.aspx?ProductType=Kanata&OrderId=<%# DataBinder.Eval(Container.DataItem,"OrderId")%>' target=_blank>
								<%# DataBinder.Eval(Container.DataItem,"OrderId")%>
								<input type="hidden" name="EditOrder" id="lnkEditOrder" value='Edit' runat="server">
							</a>
						</itemtemplate>
					</asp:templatecolumn>
					<asp:templatecolumn headertext="Campaign Id">
						<headerstyle horizontalalign="Center" verticalalign="Bottom"></headerstyle>
						<itemstyle horizontalalign="Center"></itemstyle>
						<itemtemplate>
							<%# DataBinder.Eval(Container.DataItem,"CampaignId")%>
						</itemtemplate>
					</asp:templatecolumn>
					<asp:templatecolumn headertext="Programs">
						<headerstyle horizontalalign="Center" verticalalign="Bottom"></headerstyle>
						<itemstyle wrap="False" horizontalalign="Left"></itemstyle>
						<itemtemplate>
							<%# DataBinder.Eval(Container.DataItem,"Programs")%>
						</itemtemplate>
					</asp:templatecolumn>
					<asp:templatecolumn headertext="Ware House">
						<headerstyle font-bold="True" horizontalalign="Center" verticalalign="Bottom"></headerstyle>
						<itemstyle wrap="False"></itemstyle>
						<itemtemplate>
							<asp:Label id=Label1 runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Warehouse") %>'>
							</asp:Label>
						</itemtemplate>
					</asp:templatecolumn>
					<asp:templatecolumn headertext="Date Received">
						<headerstyle font-bold="True" horizontalalign="Center" verticalalign="Bottom"></headerstyle>
						<itemtemplate>
							<asp:Label id="Label9" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"DateOrderReceived") %>'>
							</asp:Label>
						</itemtemplate>
					</asp:templatecolumn>
					<asp:templatecolumn headertext="Date Printed">
						<headerstyle horizontalalign="Center" verticalalign="Bottom"></headerstyle>
						<itemstyle horizontalalign="Center"></itemstyle>
						<itemtemplate>
							<%# DataBinder.Eval(Container.DataItem,"DatePrinted")%>
						</itemtemplate>
					</asp:templatecolumn>
					<asp:templatecolumn headertext="Status">
						<headerstyle horizontalalign="Center" verticalalign="Bottom"></headerstyle>
						<itemstyle wrap="False" horizontalalign="Center"></itemstyle>
						<itemtemplate>
							<%# DataBinder.Eval(Container.DataItem,"BatchStatus")%>
						</itemtemplate>
					</asp:templatecolumn>
					<asp:templatecolumn headertext="Order Qualifier">
						<headerstyle horizontalalign="Center" verticalalign="Bottom"></headerstyle>
						<itemstyle wrap="False" horizontalalign="Center"></itemstyle>
						<itemtemplate>
							<%# DataBinder.Eval(Container.DataItem,"OrderQualifier")%>
						</itemtemplate>
					</asp:templatecolumn>
					<asp:templatecolumn headertext="Order Type">
						<headerstyle horizontalalign="Center" verticalalign="Bottom"></headerstyle>
						<itemstyle wrap="False" horizontalalign="Center"></itemstyle>
						<itemtemplate>
							<%# DataBinder.Eval(Container.DataItem,"OrderType")%>
						</itemtemplate>
					</asp:templatecolumn>
					<asp:templatecolumn headertext="Force Close Order">
						<itemstyle wrap="False" horizontalalign="Center"></itemstyle>
						<itemtemplate>
							<cc2:OrderHistoryButtonForceCloseOrder id="btnForceCloseOrder" runat="server" OrderID='<%# DataBinder.Eval(Container.DataItem, "OrderID")%>'>
							</cc2:OrderHistoryButtonForceCloseOrder>
						</itemtemplate>
					</asp:templatecolumn>
					<asp:templatecolumn headertext="Approve Order">
						<itemstyle wrap="False" horizontalalign="Center"></itemstyle>
						<itemtemplate>
							<cc3:OrderHistoryButtonApproveDisapproveOrder id="btnApproveOrder" runat="server" ButtonType = '0' OrderID='<%# DataBinder.Eval(Container.DataItem, "OrderID")%>'>
							</cc3:OrderHistoryButtonApproveDisapproveOrder>
						</itemtemplate>
					</asp:templatecolumn>
					<asp:templatecolumn headertext="Cancel Order">
						<itemstyle wrap="False" horizontalalign="Center"></itemstyle>
						<itemtemplate>
							<cc4:OrderHistoryButtonApproveDisapproveOrder id="btnDisapproveOrder" runat="server" ButtonType = '1' OrderID='<%# DataBinder.Eval(Container.DataItem, "OrderID")%>'>
							</cc4:OrderHistoryButtonApproveDisapproveOrder>
						</itemtemplate>
					</asp:templatecolumn>
				</columns>
				<pagerstyle mode="NumericPages"></pagerstyle>
			</asp:datagrid><asp:validationsummary id="ValidationSummary1" runat="server" showmessagebox="True" showsummary="False"></asp:validationsummary></form>
		<!--#include file="../CustomerService/errorwindow.js"-->
	</body>
</HTML>
