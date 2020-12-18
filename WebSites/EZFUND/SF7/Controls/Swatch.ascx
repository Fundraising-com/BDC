<%@ Register TagPrefix="uc1" TagName="SwatchDataList" Src="SwatchDataList.ascx" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="Swatch.ascx.vb" Inherits="StoreFront.StoreFront.CSwatch" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
	<TR>
		<TD class="Content">&nbsp;</TD>
		<td align="right"><uc1:swatchdatalist id="LeftSW" runat="server"></uc1:swatchdatalist></td>
		<td id="ImageCell" vAlign="top" align="middle"><uc1:swatchdatalist id="TopSW" runat="server"></uc1:swatchdatalist><br>
			<asp:panel id="ProductImageLink" Runat="server">
				<asp:hyperlink id="imgHyperlink" runat="server" NavigateUrl="Javascript:CloseView()" CssClass="content">
					<IMG border="0" Class="content" name="imgProductImage" id="imgProductImage" runat="server"></asp:hyperlink>
			</asp:panel><BR>
			<asp:hyperlink CssClass="content" id="CloseUp" Runat="server" NavigateUrl="Javascript:CloseView()"></asp:hyperlink><BR>
			<BR>
			<uc1:swatchdatalist id="BottomSw" runat="server"></uc1:swatchdatalist></td>
		<td align="left"><uc1:swatchdatalist id="RightSW" runat="server"></uc1:swatchdatalist></td>
	</TR>
</TABLE>
