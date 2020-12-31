<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CustomerService" Assembly="QSPFulfillment" %>
<%@ Register TagPrefix="cc3" Namespace="QSPFulfillment.CommonWeb" Assembly="QSPFulfillment" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ControlerSwitchLetter.ascx.cs" Inherits="QSPFulfillment.CustomerService.ControlerSwitchLetter" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<P><asp:label id="lblMessage" runat="server"></asp:label><cc2:datagridobject id="dtgMain" runat="server" cellpadding="3" backcolor="White" borderwidth="1px"
		borderstyle="None" bordercolor="#CCCCCC" allowpaging="True" searchmode="0" autogeneratecolumns="False" showfooter="True">
		<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
		<ItemStyle ForeColor="#000066" CssClass="CSSearchResult"></ItemStyle>
		<HeaderStyle Font-Bold="True" ForeColor="White" CssClass="CSSearchResult" BackColor="#006699"></HeaderStyle>
		<FooterStyle ForeColor="#000066" CssClass="CSSearchResult" BackColor="White"></FooterStyle>
		<Columns>
			<asp:TemplateColumn HeaderText="Instance">
				<ItemTemplate>
					<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Instance") %>'>
					</asp:Label>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:BoundColumn DataField="ProductCode" HeaderText="Title Code"></asp:BoundColumn>
			<asp:BoundColumn DataField="MagazineTitle" HeaderText="Magazine Title"></asp:BoundColumn>
			<asp:BoundColumn DataField="LanguageCode" HeaderText="Language Code"></asp:BoundColumn>
			<asp:BoundColumn DataField="Quantity" HeaderText="Quantity"></asp:BoundColumn>
			<asp:BoundColumn DataField="DateCreated" HeaderText="Date Created" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
			<asp:BoundColumn DataField="UserName" HeaderText="Created By"></asp:BoundColumn>
			<asp:BoundColumn DataField="IsPrinted" HeaderText="Is Printed"></asp:BoundColumn>
			<asp:BoundColumn DataField="DatePrinted" HeaderText="Date Printed"></asp:BoundColumn>
			<asp:BoundColumn DataField="IsLocked" HeaderText="Status"></asp:BoundColumn>
			<asp:TemplateColumn Visible="False">
				<ItemTemplate>
					<asp:LinkButton id=Label2 runat="server" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Instance") %>' CommandName="ResetSWL">Cancel
								</asp:LinkButton>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="Action">
				<ItemTemplate>
					<asp:LinkButton id=hylReprint runat="server" CommandName="ReprintSWL" CommandArgument='<%#DataBinder.Eval(Container, "DataItem.Instance")%>'>Print</asp:LinkButton>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="Mark As">
				<ItemTemplate>
					<asp:LinkButton id=hylMark runat="server" CommandName="Mark" CommandArgument='<%#DataBinder.Eval(Container, "DataItem.Instance")%>'>Printed</asp:LinkButton>
				</ItemTemplate>
			</asp:TemplateColumn>
		</Columns>
		<PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="#999999" CssClass="CSPager"
			Mode="NumericPages"></PagerStyle>
	</cc2:datagridobject><cc3:rsgeneration id="rsGenerationSwitchLetter" runat="server" reportname="SwitchLetter"></cc3:rsgeneration></P>
<P>&nbsp;</P>
