<%@ Register TagPrefix="uc1" TagName="UploadControl" Src="UploadControl.ascx" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ProductFulfillmentControl.ascx.vb" Inherits="StoreFront.StoreFront.ProductFulfillmentControl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<input id="ProdUID" type="hidden" name="ProdUID" runat="server">
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
	<TBODY>
		<tr>
			<TD class="Content" width="1" colSpan="10">&nbsp;</TD>
		</tr>
		<TR>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="ContentTableHeader" noWrap align="left" colSpan="8">&nbsp;&nbsp;Shipping 
				Options&nbsp;
			</TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="8"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" align="right" colspan="2">Ship This 
				Product:&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="content" align="left" colSpan="6"><asp:checkbox id="ShipProduct" runat="server"></asp:checkbox></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="8"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR id="Row8">
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" align="right" colspan="2">Ship From 
				Vendor:&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="content" align="left" colSpan="6"><asp:checkbox id="DropShip" runat="server"></asp:checkbox></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="8"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="8">&nbsp;</TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" align="left" colspan="8">&nbsp;&nbsp;&nbsp;<b>Product Weight and 
					Dimensions (For Carrier Based Shipping)</b></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="8"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" align="right">Weight:&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="content" align="left"><asp:TextBox id="Weight" width="45" Runat="server"></asp:TextBox></td>
			<td class="content" align="right">Length:&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="content" align="left"><asp:TextBox id="Length" width="45" Runat="server"></asp:TextBox></td>
			<td class="content" align="right">Width:&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="content" align="left"><asp:TextBox id="Width" width="45" Runat="server"></asp:TextBox></td>
			<td class="content" align="right">Height:&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="content" align="left"><asp:TextBox id="Height" width="45" Runat="server"></asp:TextBox>&nbsp;&nbsp;</td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="8">&nbsp;</TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" align="left" colspan="8">&nbsp;&nbsp;&nbsp;<b>Shipping Cost (For 
					Product Based Shipping)</b></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="8"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" align="right" colspan="2">Ship Price:&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="content" align="left" colspan="6"><asp:TextBox id="ShipPrice" Runat="server"></asp:TextBox></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="8"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" colSpan="10" height="1"><IMG height="1" src="images/clear.gif"></TD>
		</TR>
		<tr>
			<TD class="Content" width="1" colSpan="10">&nbsp;</TD>
		</tr>
		<TR>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="ContentTableHeader" noWrap align="left" colSpan="8">&nbsp;&nbsp;Gift 
				Wrap&nbsp;
			</TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="8"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" align="right" colspan="2">Gift Wrap This 
				Product:&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="content" align="left" colSpan="1"><asp:checkbox id="GiftWrap" runat="server"></asp:checkbox></td>
			<td class="content" align="right" colspan="2">Gift Wrap 
				Price:&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="content" align="left" colSpan="3"><asp:TextBox id="GiftWrapPrice" runat="server"></asp:TextBox></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<TR>
			<TD class="ContentTable" colSpan="10" height="1"><IMG height="1" src="images/clear.gif"></TD>
		</TR>
		<tr id="Row1">
			<TD class="Content" width="1" colSpan="10">&nbsp;</TD>
		</tr>
		<TR id="Row2">
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="ContentTableHeader" noWrap align="left" colSpan="8">&nbsp;&nbsp;Product 
				Downloads&nbsp;
			</TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr id="Row3">
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="8"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR id="Row4">
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" align="right" colspan="2">Download This 
				Product:&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="content" align="left" colSpan="1"><asp:checkbox id="Download" runat="server"></asp:checkbox></td>
			<td class="content" align="right" colspan="5">
				<uc1:UploadControl id="UploadControl1" runat="server"></uc1:UploadControl></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr id="Row5">
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="8"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR id="Row6">
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" align="right" colspan="2">Allow Multiple 
				Downloads:&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="content" align="left" colSpan="1"><asp:checkbox id="MultipleDownload" runat="server"></asp:checkbox></td>
			<td class="content" align="right" colspan="2">Download&nbsp;Expires:&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="content" align="left" colSpan="3"><asp:DropDownList id="Expires" runat="server"></asp:DropDownList></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<TR id="Row7">
			<TD class="ContentTable" colSpan="10" height="1"><IMG height="1" src="images/clear.gif"></TD>
		</TR>
		<tr>
			<TD class="Content" width="1" colSpan="10">&nbsp;</TD>
		</tr>
		<TR>
			<td class="content" align="right" width="75%" colSpan="10">
				<asp:LinkButton ID="cmdSave" Runat="server">
					<asp:Image BorderWidth="0" ID="imgSave" runat="server" ImageUrl="../images/save.jpg" AlternateText="Save"></asp:Image>
				</asp:LinkButton>
			</td>
		</TR>
	</TBODY></TABLE>
