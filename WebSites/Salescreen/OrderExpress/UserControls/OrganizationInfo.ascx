<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.OrganizationInfo" Codebehind="OrganizationInfo.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="AccountSubList" Src="AccountSubList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AddressControlInfo" Src="AddressControlInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="OrganizationHeaderInfo" Src="OrganizationHeaderInfo.ascx" %>

<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td align="left"> <!--Section Title --></td>
	</tr>
	<tr>
		<td  align=center>
			<table id="Table1e" cellSpacing="0" cellPadding="0" width="400px" border="0">
				<tr align="left">
					<td Class="SectionPageTitleInfo">
						<asp:label id="Label5" runat="server">
							Organization Information
						</asp:label>			
					</td>
				</tr>
				<tr>
					<td>
					</td>
				</tr>
				<tr>
					<td>
						<uc1:OrganizationHeaderInfo id="OrganizationHeaderInfo_Final" runat="server"></uc1:OrganizationHeaderInfo></td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td>
			<br>
		</td>
	</tr>
	<tr>
		<td align="left">
			<table id="Table3" cellSpacing="0" cellPadding="0" border="0">
				<tr class="HeaderItemStyle">
					<td width="350"><asp:label id="Label8" runat="server">
							&nbsp;Bill To
						</asp:label></td>
					<td width="350"><asp:label id="Label18" runat="server">
							&nbsp;Ship To
						</asp:label>
					</td>
				</tr>
				<tr>
					<td><uc1:AddressControlInfo id="AddressInfo_Billing" runat="server"></uc1:AddressControlInfo></td>
					<td><uc1:AddressControlInfo id="AddressInfo_Shipping" runat="server"></uc1:AddressControlInfo></td>
				</tr>
			</table>			
		</td>
	</tr>
	<tr>
		<td>
			<br>
		</td>
	</tr>
	<tr>
		<td Class="SectionPageTitleInfo">
			<asp:label id="Label4" runat="server">
				Account List
			</asp:label></td>
	</tr>
	
	<tr>
		<td>
			<uc1:AccountSubList id="AccountSubList_Final"   runat="server"></uc1:AccountSubList>
		</td>
	</tr>
	<tr>
		<td>
			<br>
			<br>
		</td>
	</tr>
</table>
