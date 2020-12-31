<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CustomerService" Assembly="QSPFulfillment" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ControlerSubscriptionForChadd.ascx.cs" Inherits="QSPFulfillment.CustomerService.ControlerSubscriptionForChadd" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE cellSpacing="0" cellPadding="1" width="100%" bgColor="#000000" border="0">
	<TBODY>
		<tr>
			<td>
				<TABLE bgcolor="#ffffff" cellspacing="0" border="0" width="100%">
					<TBODY>
						<tr>
							<td nowrap class="CSTableHeader">Subscription Affected by the CHADD</td>
						</tr>
						<tr>
							<td>
								<asp:Label runat="server" id="lblMessage"></asp:Label>
								<cc2:DataGridObject id="dtgMain" SearchMode="0" runat="server" AutoGenerateColumns="False" ShowFooter="True"
									width="100%" BorderStyle="None" GridLines="None" cssClass="CSTableSubHeader">
<ItemStyle CssClass="CSTableSubHeader">
</ItemStyle>

<HeaderStyle Font-Bold="True" CssClass="CSTableSubHeader">
</HeaderStyle>

<Columns>
<asp:BoundColumn DataField="CustomerOrderHeaderInstance" HeaderText="COH Instance">
<HeaderStyle Wrap="False">
</HeaderStyle>
</asp:BoundColumn>
<asp:BoundColumn DataField="TransID" HeaderText="Trans ID">
<HeaderStyle Wrap="False">
</HeaderStyle>
</asp:BoundColumn>
<asp:BoundColumn DataField="SubscriptionDate" HeaderText="Subscription Date" DataFormatString="{0:MM/dd/yyyy}">
<HeaderStyle Wrap="False">
</HeaderStyle>
</asp:BoundColumn>

<asp:BoundColumn DataField="TitleCode" HeaderText="Title Code">
<HeaderStyle Wrap="False">
</HeaderStyle>
</asp:BoundColumn>
<asp:BoundColumn DataField="Title" HeaderText=" Title">
<HeaderStyle Wrap="False">
</HeaderStyle>
</asp:BoundColumn>
</Columns>
								</cc2:DataGridObject>
							</td>
						</tr></TBODY>
				</TABLE>
			</td>
		</tr></TBODY>
</TABLE>
