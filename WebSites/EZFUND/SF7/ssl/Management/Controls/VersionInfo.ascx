<%@ Control Language="vb" AutoEventWireup="false" Codebehind="VersionInfo.ascx.vb" Inherits="StoreFront.StoreFront.VersionInfo" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<p class="headings">Version Information</p>
<asp:DataGrid ID="dgVersionInfo" Runat="server"
	AutoGenerateColumns="False"
	BorderColor="#BBBBBB"
	CellPadding="3"
	GridLines="Horizontal"
	Width="100%">
	<HeaderStyle CssClass="ContentTableHeader" />
	<ItemStyle CssClass="Content" />
	<AlternatingItemStyle BackColor="#DDDDDD" />
	<Columns>
		<asp:BoundColumn HeaderText="Current Libraries" DataField="FileName" />
		<asp:BoundColumn HeaderText="Description" DataField="Comments" />
		<asp:BoundColumn HeaderText="Version" DataField="FileVersion" />
	</Columns>
</asp:DataGrid>
