<%@ Register TagPrefix="uc1" TagName="CAttributeControl" Src="CAttributeControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CInventoryControl" Src="CInventoryControl.ascx" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ProductBundleDetail.ascx.vb" Inherits="StoreFront.StoreFront.ProductBundleDetail" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<div class="bundlewrap">
	<h2>Select from the options below</h2>
	<TABLE id="tblScott" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server" class="bundletbl">
		<tr>
			<td>
			<asp:repeater id="rptBundle" EnableViewState="true" runat="server">
			<ItemTemplate>
				<table id="tblRepeater" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server" class="bundle">
					<tr>
						<td class="Content" valign="top">
							<table id="tblImgCell" runat="server" cellspacing="0" cellpadding="0" border="0">
								<tr>
									<td class="Content" valign="top" id="tdRImageCell">
										<asp:HyperLink Runat=server ID="lnkImage" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"detaillink") %>'>
											<img border=0 src='<%#DataBinder.Eval(Container.DataItem,"smallimage") %>'>
										</asp:HyperLink>
										<asp:Panel Runat="server" ID="SmallImage">
											<img src='<%#DataBinder.Eval(Container.DataItem,"smallimage") %>'>
										</asp:Panel>
									</td>
								</tr>
							</table>
						</td>
						<td valign="top">
							<table id="Table6" width="100%" runat="server" cellSpacing="0" cellPadding="0" border="0">
								<tr id="trRProductName">
									<td align="left" class="Content">
										<asp:Label ID="lblProductName2" Runat="server">Product Name:&nbsp;<%# DataBinder.Eval(Container.DataItem,"name") %></asp:Label>
									</td>
								</tr>
								<tr>
									<td class="Content">
										<asp:Label ID="lblQty" Runat="server">Qty Included:&nbsp;<%# DataBinder.Eval(Container.DataItem, "bundledquantity") %></asp:Label>												  
									</td>
								</tr>
								<tr id="trStockInfo">
									<td class="Content">
										<asp:Label ID="lblStockInfo" ForeColor="#0033ff" Runat="server">
											<%# DataBinder.Eval(Container.DataItem, "stock") %>
										</asp:Label>
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr id="trRShortdescription">
						<td class="Content" colspan="2">
							<asp:Label ID="lblDescription2" Runat="server">Description:&nbsp;</asp:Label><%# DataBinder.Eval(Container.DataItem, "shortdescription") %>
						</td>
					</tr>
					<tr>
						<td colspan="2" class="attributecell">
							<%-- Tee 9/12/2007 product configurator --%>
							<input type="hidden" id="hidProdId" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "ProductID")%>'>
							<input type="hidden" id="hidQty" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "bundledquantity") %>'>
							<%-- end Tee --%>
							<uc1:cattributecontrol id="CAttributeControl1" runat="server"></uc1:cattributecontrol>														
						</td>
					</tr>
				</table>
			</ItemTemplate>
			</asp:repeater>
			</td>
		</tr>
	</TABLE>
</div>