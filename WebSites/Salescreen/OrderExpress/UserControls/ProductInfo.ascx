<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.ProductInfo" Codebehind="ProductInfo.ascx.cs" %>
<table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
    <tr>
        <td align="left">
            <!--Section Title -->
        </td>
    </tr>
    <tr>
        <td align="center">
            <table id="Table3" cellspacing="0" width="700" cellpadding="2" border="0">
                <tr align="left">
                    <td class="SectionPageTitleInfo" colspan="2">
                        <asp:Label ID="Label5" runat="server">
							Product Information
                        </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <span class="StandardLabel">ID&nbsp;:</span>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblID" runat="server" CssClass="DescInfoLabel" Width="400px"></asp:Label></td>
                </tr>
                <tr id="trProductType" runat="server" visible="False">
                    <td align="left">
                        <span class="StandardLabel">Product Type ID&nbsp;:</span>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblProductTypeID" runat="server" CssClass="DescInfoLabel"></asp:Label></td>
                </tr>
                <tr>
                    <td align="left">
                        <span class="StandardLabel">Product Type Name&nbsp;:</span>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblProductTypeName" runat="server" CssClass="DescInfoLabel"></asp:Label></td>
                </tr>
                <tr>
                    <td align="left">
                        <span class="StandardLabel">Product&nbsp;Code&nbsp;:</span>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblProductCode" runat="server" CssClass="DescInfoLabel"></asp:Label></td>
                </tr>
                <tr>
                    <td align="left">
                        <span class="StandardLabel">Product Name&nbsp;:</span>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblProductName" runat="server" CssClass="DescInfoLabel"></asp:Label></td>
                </tr>
                <!--
				<tr>
					<td><span class="StandardLabel">Vendor ID&nbsp;:</span>
					</td>
					<td><asp:label id="lblVendorID" runat="server" CssClass="DescInfoLabel"></asp:label></td>
				</tr>
				-->
                <tr>
                    <td align="left">
                        <span class="StandardLabel">Vendor Name&nbsp;:</span>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblVendorName" runat="server" CssClass="DescInfoLabel"></asp:Label></td>
                </tr>
                <tr>
                    <td class="StandardLabel" align="left">
                        Description:</td>
                    <td align="left">
                        <asp:Label ID="lblDescription" CssClass="DescInfoLabel" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td align="left" class="StandardLabel">
                        Number of Unit&nbsp;:</td>
                    <td align="left">
                        <asp:Label ID="lblNbUnit" CssClass="DescInfoLabel" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td align="left" class="StandardLabel">
                        Unit Cost&nbsp;:</td>
                    <td align="left">
                        <asp:Label ID="lblUnitCost" CssClass="DescInfoLabel" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td align="left">
                        <span class="StandardLabel">Number of day lead time:</span>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblNbDayLeadTime" CssClass="DescInfoLabel" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td align="left">
                        <span class="StandardLabel">Vendor Item Code&nbsp;:</span>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblVendorItem" CssClass="DescInfoLabel" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td align="left">
                        <span class="StandardLabel">Oracle code:</span>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblOracleCode" CssClass="DescInfoLabel" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td align="left">
                        <span class="StandardLabel">Commission&nbsp;:</span>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblCommission" runat="server" CssClass="DescInfoLabel"></asp:Label></td>
                </tr>
                <tr>
                    <td align="left">
                        <span class="StandardLabel">Is free sample&nbsp;:</span>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblIsFreeSample" runat="server" CssClass="DescInfoLabel"></asp:Label></td>
                </tr>
                <tr>
                    <td align="left">
                        <span class="StandardLabel">Image Url&nbsp;:</span>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblImageURL" runat="server" CssClass="DescInfoLabel"></asp:Label></td>
                </tr>
                <!--
				<tr>
					<td><span class="StandardLabel">Business Division ID&nbsp;:</span>
					</td>
					<td><asp:label id="lblBusinessDivisionID" runat="server" CssClass="DescInfoLabel"></asp:label></td>
				</tr>
				-->
                <tr>
                    <td align="left">
                        <span class="StandardLabel">Business Division Name&nbsp;:</span>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblBusinesDivisionName" runat="server" CssClass="DescInfoLabel"></asp:Label></td>
                </tr>
            </table>
            <table id="tblAudit" runat="server" visible="false">
                <tr>
                    <td class="StandardLabel">
                        Created by :</td>
                    <td>
                        <asp:Label ID="lbCreateBy" CssClass="DescInfoLabel" runat="server"></asp:Label></td>
                    <td class="StandardLabel">
                        Created date :</td>
                    <td>
                        <asp:Label ID="lbCreateDT" CssClass="DescInfoLabel" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td class="StandardLabel">
                        Updated by :</td>
                    <td>
                        <asp:Label ID="lbUpdateBy" CssClass="DescInfoLabel" runat="server"></asp:Label></td>
                    <td class="StandardLabel">
                        Update date :</td>
                    <td>
                        <asp:Label ID="lbUpdateDT" CssClass="DescInfoLabel" runat="server"></asp:Label></td>
                </tr>
            </table>
        </td>
    </tr>
</table>
