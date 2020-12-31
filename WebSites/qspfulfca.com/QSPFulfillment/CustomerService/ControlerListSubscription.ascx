<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CustomerService" Assembly="QSPFulfillment" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ControlerListSubscription.ascx.cs" Inherits="QSPFulfillment.CustomerService.ControlerListSubscription" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<cc2:DataGridObject id="dtgMain" runat="server" AutoGenerateColumns="False" AllowPaging="True" SearchMode="0">
<Columns>
<asp:BoundColumn HeaderText="Title Code"></asp:BoundColumn>
<asp:BoundColumn HeaderText="Magazine Title"></asp:BoundColumn>
<asp:BoundColumn HeaderText="Issue Entered"></asp:BoundColumn>
<asp:BoundColumn HeaderText="Price Entered"></asp:BoundColumn>
<asp:BoundColumn HeaderText="Issue Sent"></asp:BoundColumn>
<asp:BoundColumn HeaderText="Catalog Price"></asp:BoundColumn>
<asp:BoundColumn HeaderText="Override Code"></asp:BoundColumn>
<asp:BoundColumn HeaderText="Status"></asp:BoundColumn>
<asp:BoundColumn HeaderText="First Name"></asp:BoundColumn>
<asp:BoundColumn HeaderText="Last Name"></asp:BoundColumn>
<asp:BoundColumn HeaderText="Remit Batch ID"></asp:BoundColumn>
<asp:BoundColumn HeaderText="Remit Batch Date"></asp:BoundColumn>
</Columns>

<PagerStyle Mode="NumericPages">
</PagerStyle>
</cc2:DataGridObject>
