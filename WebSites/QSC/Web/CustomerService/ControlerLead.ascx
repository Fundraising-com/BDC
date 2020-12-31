<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ControlerLead.ascx.cs" Inherits="QSPFulfillment.CustomerService.ControlerLead" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CustomerService" Assembly="QSPFulfillment" %>
<cc2:datagridobject id="dtgMain" AllowPaging="True" CssClass="CSTableItems" SearchMode="0" AutoGenerateColumns="False"
	runat="server" width="100%" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" BackColor="White"
	CellPadding="3" DataKeyField="Instance">
	<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
	<ItemStyle ForeColor="#000066" CssClass="CSSearchResult"></ItemStyle>
	<HeaderStyle Font-Bold="True" ForeColor="White" CssClass="CSSearchResult" BackColor="#006699"></HeaderStyle>
	<FooterStyle ForeColor="#000066" CssClass="CSSearchResult" BackColor="White"></FooterStyle>
	<Columns>
		<asp:BoundColumn Visible="False" DataField="UserID" ReadOnly="True" HeaderText="User ID"></asp:BoundColumn>
		<asp:BoundColumn DataField="ContactName" ReadOnly="True" HeaderText="Contact Name"></asp:BoundColumn>
		<asp:BoundColumn DataField="ContactHomePhoneNumber" ReadOnly="True" HeaderText="Home Phone #"></asp:BoundColumn>
		<asp:BoundColumn DataField="ContactWorkPhoneNumber" ReadOnly="True" HeaderText="Work Phone #"></asp:BoundColumn>
		<asp:BoundColumn DataField="ContactFaxNumber" ReadOnly="True" HeaderText="Fax #"></asp:BoundColumn>
		<asp:BoundColumn DataField="ContactEMail" ReadOnly="True" HeaderText="Email"></asp:BoundColumn>
		<asp:BoundColumn DataField="SchoolGroup" ReadOnly="True" HeaderText="School/Group"></asp:BoundColumn>
		<asp:BoundColumn DataField="CityTown" ReadOnly="True" HeaderText="City"></asp:BoundColumn>
		<asp:BoundColumn DataField="Province" ReadOnly="True" HeaderText="Province"></asp:BoundColumn>
		<asp:BoundColumn DataField="InterestedInWhat" ReadOnly="True" HeaderText="What are you interested in?"></asp:BoundColumn>
		<asp:BoundColumn DataField="WhereHearAboutQSP" ReadOnly="True" HeaderText="Where Did you hear about QSP?"></asp:BoundColumn>
		<asp:boundcolumn datafield="Date" readonly="True" headertext="Date Received" dataformatstring="{0:dd/MM/yyyy}"></asp:boundcolumn>
		<asp:BoundColumn DataField="DateSent" ReadOnly="True" HeaderText="Date Sent" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundColumn>
		<asp:boundcolumn datafield="UserName" readonly="True" headertext="User Name"></asp:boundcolumn>
		<asp:boundcolumn datafield="Comments" readonly="True" headertext="Comments"></asp:boundcolumn>
		<asp:TemplateColumn HeaderText="FMID">
			<ItemTemplate>
				<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FieldManagerName") %>'>
				</asp:Label>
			</ItemTemplate>
			<EditItemTemplate>
				<asp:DropDownList runat="server" ID="ddlFieldManager" SelectedIndex='<%#GetIndex(DataBinder.Eval(Container, "DataItem.FMID").ToString()) %>'>
				</asp:DropDownList>
			</EditItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn>
			<ItemTemplate>
				<asp:LinkButton runat="server" Text="Edit" CommandName="Edit" CommandArgument='<%#DataBinder.Eval(Container, "DataItem.Instance") %>' CausesValidation="false" ID="Linkbutton1">
				</asp:LinkButton>
			</ItemTemplate>
			<FooterTemplate>
				<asp:LinkButton runat="server" Text="Insert" CommandName="Insert" CausesValidation="true" ID="Linkbutton2"></asp:LinkButton>
			</FooterTemplate>
			<EditItemTemplate>
				<table>
					<tr>
						<td>
							<asp:LinkButton runat="server" Text="Update" CommandName="Update" CausesValidation="true" CommandArgument='<%#DataBinder.Eval(Container, "DataItem.Instance") %>' ID="Linkbutton3">
							</asp:LinkButton>
						<td>
							<asp:LinkButton runat="server" Text="Cancel" CommandName="Cancel" CausesValidation="false" ID="Linkbutton4"></asp:LinkButton></td>
					</tr>
				</table>
			</EditItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn>
			<ItemTemplate>
				<asp:LinkButton CommandName="send" CommandArgument='<%#DataBinder.Eval(Container, "DataItem.FMID")+";"+DataBinder.Eval(Container, "DataItem.Instance")%>' Enabled='<%#DataBinder.Eval(Container, "DataItem.FMID").ToString()==""?false:true%>' runat="server" ID="btnlSend">Send by Email</asp:LinkButton>
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
	<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" CssClass="CSPager"
		Mode="NumericPages"></PagerStyle>
</cc2:datagridobject>
<asp:Label id="lblMessage" runat="server"></asp:Label>
