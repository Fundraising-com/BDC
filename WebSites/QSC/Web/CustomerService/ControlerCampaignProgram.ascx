<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ControlerCampaignProgram.ascx.cs" Inherits="QSPFulfillment.CustomerService.ControlerCampaignProgram" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CustomerService" Assembly="QSPFulfillment" %>
<cc2:DataGridObject id="dtgMain" runat="server" AutoGenerateColumns="False" SearchMode="0" GridLines="None"
	Width="100%">
	<ItemStyle CssClass="CSTableItems"></ItemStyle>
	<HeaderStyle Font-Bold="True" CssClass="CSTableItems"></HeaderStyle>
	<Columns>
		<asp:BoundColumn DataField="ProgramDescription" HeaderText="Name"></asp:BoundColumn>
		<asp:TemplateColumn HeaderText="PreCollect">
			<ItemTemplate>
				<asp:Label id="lblPreCollect" runat="server"><%#(DataBinder.Eval (Container,"DataItem.IsPreCollect")).ToString().ToUpper()=="N"?"No":"Yes"%>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
</cc2:DataGridObject>
<asp:Label id="lblMessage" runat="server"></asp:Label>
