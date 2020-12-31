<%@ Control Language="c#" AutoEventWireup="True" Codebehind="TrackingOrderDetail.ascx.cs" Inherits="QSPFulfillment.OrderMgt.TrackingOrderDetail" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<asp:datagrid id="dgOrderDetail" Width="920px" templatedatamode="Table" LoadControlMode="UserControl"
	TemplateCachingBase="Tablename" runat="server" AutoGenerateColumns="False">
	<HeaderStyle Font-Size="9pt" Font-Names="Verdana" Font-Bold="True" ForeColor="White" BackColor="CadetBlue"></HeaderStyle>
	<Columns>
		<asp:TemplateColumn HeaderText="Tote ID">
		    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
		    <ItemStyle Font-Size="9pt"></ItemStyle>
		    <ItemTemplate>
			    <asp:Label id=lblToteId runat="server" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem, "ToteId") %>'>
			    </asp:Label>
		    </ItemTemplate>
	    </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="Receipt Date">
			<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
			<ItemStyle Font-Size="9pt"></ItemStyle>
			<ItemTemplate>
				<asp:Label id=lblReceiptDate runat="server" Width="80px" Text='<%# (DataBinder.Eval(Container.DataItem, "ReceiptDate"))  %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="Image Date">
			<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
			<ItemStyle Font-Size="9pt"></ItemStyle>
			<ItemTemplate>
				<asp:Label id=lblImageDate Width="80px" runat="server" Text='<%# (DataBinder.Eval(Container.DataItem, "ImageDate")) %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="DateCapture Date">
			<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
			<ItemStyle Font-Size="9pt"></ItemStyle>
			<ItemTemplate>
				<asp:Label id=lblCaptureDate Width="80px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DataCaptureDate") %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="Verification Date">
			<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
			<ItemStyle Font-Size="9pt"></ItemStyle>
			<ItemTemplate>
				<asp:Label id=lblVerificationDate Width="80px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "VerificationDate") %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="Edit Date">
			<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
			<ItemStyle Font-Size="9pt"></ItemStyle>
			<ItemTemplate>
				<asp:Label id=lblEditDate Width="80px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "EditDate") %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="Transmit Date">
			<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
			<ItemStyle Font-Size="9pt"></ItemStyle>
			<ItemTemplate>
				<asp:Label id=lblTransmitDate Width="80px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TransmitDate") %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
</asp:datagrid>
