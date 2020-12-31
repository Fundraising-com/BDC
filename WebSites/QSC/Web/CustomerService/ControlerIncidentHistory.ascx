<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CustomerService" Assembly="QSPFulfillment" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ControlerIncidentHistory.ascx.cs" Inherits="QSPFulfillment.CustomerService.ControlerIncidentHistory" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<script language="javascript">

	function SetIDIncident(varv)
	{
		self.close();
		window.opener.SetIDIncident(varv);
	
	}
	function Close()
	{
		self.close();
	}

</script>
<center>
<asp:Label runat=server id="lblMessage"></asp:Label>
<cc2:DataGridObject id="dtgMain" runat="server" width="100%" cssClass="CSSearchResult" AutoGenerateColumns="False" AllowPaging="True" ShowFooter="False"
	SearchMode="0" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
		BackColor="White" CellPadding="3" GridLines="Vertical">
		<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#006699" cssClass="CSSearchResult"></HeaderStyle>
	
		<ItemStyle ForeColor="#000066" cssClass="CSSearchResult"></ItemStyle>
		<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White"  cssClass="CSPager" Mode="NumericPages"></PagerStyle>
	<Columns>
		<asp:TemplateColumn HeaderText="Incident ID">
			<ItemTemplate>
				<asp:Label runat="server" ID="lblIncidentInstance" Text='<%#DataBinder.Eval(Container, "DataItem.IncidentInstance")%>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:BoundColumn HeaderText="Refer Incident" DataField="refertoincidentinstance"></asp:BoundColumn>
		<asp:BoundColumn HeaderText="Logged By" DataField="UserIDCreated"></asp:BoundColumn>
		<asp:BoundColumn DataField="UserNameCreated"></asp:BoundColumn>
		<asp:BoundColumn HeaderText="Date Logged" DataField="DateCreated" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
		<asp:BoundColumn HeaderText="Problem Code" DataField="ProblemCodeInstance"></asp:BoundColumn>
		<asp:BoundColumn HeaderText="Problem Comments" DataField="IncidentComments"></asp:BoundColumn>
		<asp:BoundColumn HeaderText="Action" DataField="ActionDescription"></asp:BoundColumn>
		<asp:TemplateColumn HeaderText="Action Comment">
			<ItemTemplate>
				<asp:hyperlink runat="server" id="hypEditComment" cssclass="CSSearchResult" NavigateUrl="javascript:void(0);" Text='<%# DataBinder.Eval(Container, "DataItem.IncidentActionComments") == String.Empty ? "none" : DataBinder.Eval(Container, "DataItem.IncidentActionComments") %>' font-bold="false" font-underline="true"></asp:hyperlink>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:BoundColumn Visible="False" HeaderText="Action Comment" DataField="IncidentActionComments"></asp:BoundColumn>
		<asp:BoundColumn HeaderText="Channel" DataField="CommunicationChannel"></asp:BoundColumn>
		<asp:TemplateColumn visible=false>
			<ItemTemplate>
				<a href='javascript:void(0);' onclick="<%#"javascript:SetIDIncident('"+DataBinder.Eval (Container,"DataItem.IncidentInstance")+"');"%>">
					Select</a>
			</ItemTemplate>
			<EditItemTemplate></EditItemTemplate>
		</asp:TemplateColumn>
	</Columns>
</cc2:DataGridObject>
</center>
