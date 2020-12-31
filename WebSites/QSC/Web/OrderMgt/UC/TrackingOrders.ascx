<%@ Control Language="c#" AutoEventWireup="True" Codebehind="TrackingOrders.ascx.cs" Inherits="QSPFulfillment.OrderMgt.TrackingOrders" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="dbwc" Namespace="DBauer.Web.UI.WebControls" Assembly="DBauer.Web.UI.WebControls.HierarGrid" %>
<P><dbwc:hierargrid id="dgOrdersInFile" AllowPaging="True" AllowSorting="True" runat="server" rowexpanded="DBauer.Web.UI.WebControls.RowStates"
		templatedatamode="Table" LoadControlMode="UserControl" TemplateCachingBase="Tablename" AutoGenerateColumns="False"
		Width="848px">
		<PagerStyle VerticalAlign="Middle" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True"
			HorizontalAlign="Center" ForeColor="White" BackColor="SteelBlue" Mode="NumericPages"></PagerStyle>
		<FooterStyle Font-Size="XX-Small"></FooterStyle>
		<HeaderStyle Font-Size="9pt" Font-Names="Verdana" Font-Bold="True" ForeColor="White" BackColor="SteelBlue"></HeaderStyle>
		<Columns>
			<asp:TemplateColumn HeaderText="Order ID">
				<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
				<ItemStyle Font-Size="9pt" Font-Names="Verdana"></ItemStyle>
				<ItemTemplate>
					<asp:Label id=lblOrderId Width="45px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderId") %>'>
					</asp:Label>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="Order Type">
				<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
				<ItemStyle Font-Size="9pt" Font-Names="Verdana"></ItemStyle>
				<ItemTemplate>
					<asp:Label id=lblOrderType Width="105px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderQualifierID") %>'>
					</asp:Label>
				</ItemTemplate>
			</asp:TemplateColumn>			
			<asp:TemplateColumn HeaderText="Recent Stage">
				<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
				<ItemStyle Font-Size="9pt" Font-Names="Verdana"></ItemStyle>
				<ItemTemplate>
					<asp:Label id=lblStage Width="105px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>'>
					</asp:Label>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="Cheque Payment">
				<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
				<ItemStyle Font-Size="9pt" Font-Names="Verdana" HorizontalAlign="Right"></ItemStyle>
				<ItemTemplate>
					<asp:Label id=lblPaymentSend Width="60px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PaymentSend") %>'>
					</asp:Label>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="Scan Count">
				<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
				<ItemStyle Font-Size="9pt" HorizontalAlign="Right"></ItemStyle>
				<ItemTemplate>
					<asp:Label id=lblScanCount runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ScanCount") %>'>
					</asp:Label>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="Date Shipped">
				<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
				<ItemStyle Font-Size="9pt" Font-Names="Verdana"></ItemStyle>
				<ItemTemplate>
					<asp:Label id=lblDateShipped Width="160px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DateShipped") %>'>
					</asp:Label>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="Date Invoiced">
				<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
				<ItemStyle Font-Size="9pt" Font-Names="Verdana"></ItemStyle>
				<ItemTemplate>
					<asp:Label id=lblDateInvoiced Width="160px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DateInvoiced") %>' Font-Names="Verdana" Font-Size="9pt">
					</asp:Label>
				</ItemTemplate>
			</asp:TemplateColumn>
		</Columns>
	</dbwc:hierargrid></P>
