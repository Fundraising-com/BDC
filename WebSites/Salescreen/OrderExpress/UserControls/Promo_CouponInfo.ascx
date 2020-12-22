<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.Promo_CouponInfo" Codebehind="Promo_CouponInfo.ascx.cs" %>

<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td align="left"> <!--Section Title --></td>
	</tr>
	<tr>
		<td align="center">
			<table id="Table3" cellSpacing="0" cellPadding="0" width="600" border="0">
				<tr>
					<td class="SectionPageTitleInfo" colSpan="2"><asp:label id="lblTitle" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td vAlign="top"><span class="StandardLabel">Contract&nbsp;Number:</span>
					</td>
					<td vAlign="top"><asp:label id="lblID" runat="server" CssClass="DescInfoLabel" Width="200px"></asp:label></td>
				</tr>
				<tr>
					<td vAlign="top"><span class="StandardLabel">Vendor ID: &nbsp;:</span>
					</td>
					<td vAlign="top"><asp:label id="lblVendorID" CssClass="DescInfoLabel" Runat="server"></asp:label></td>
				</tr>
				<tr>
					<td vAlign="top"><span class="StandardLabel">FM ID&nbsp;:</span>
					</td>
					<td vAlign="top"><asp:label id="lblFMID" CssClass="DescInfoLabel" Runat="server"></asp:label></td>
				</tr>
				<tr>
					<td vAlign="top"><span class="StandardLabel">Logo&nbsp;:</span>
					</td>
					<td vAlign="top"><asp:image id="imgLogo" runat="server" Width="100px" ImageUrl="" Height="100px"></asp:image></td>
				</tr>
				<tr>
					<td vAlign="top" style="HEIGHT: 31px"><span class="StandardLabel">Offer&nbsp;:</span>
					</td>
					<td vAlign="top" style="HEIGHT: 31px"><asp:label id="lblOffer" CssClass="DescInfoLabel" Runat="server"></asp:label></td>
				</tr>
                <tr>
					<td vAlign="top" style="HEIGHT: 31px"><span class="StandardLabel">Promotion&nbsp;:</span>
					</td>
					<td vAlign="top" style="HEIGHT: 31px"></td>
				</tr>
				<tr>
					<td vAlign="top" align="right" style="HEIGHT: 31px"><span class="DescInfoLabel">Landscape&nbsp;:</span>
					</td>
					<td vAlign="top" style="HEIGHT: 31px">
                        <asp:Image ID="imgLandscapte" runat="server" /></td>
				</tr>
				<tr>
					<td vAlign="top" align="right" style="HEIGHT: 31px"><span class="DescInfoLabel">Portrait&nbsp;:</span>
					</td>
					<td vAlign="top" style="HEIGHT: 31px"><asp:Image ID="imgPortrait" runat="server" /></td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td align="center">
			<table id="tblAudit" runat="server" visible="false">
				<tr>
					<td class="StandardLabel">Created by :</td>
					<td><asp:label id="lbCreateBy" CssClass="DescInfoLabel" Runat="server"></asp:label></td>
					<td class="StandardLabel">Created date :</td>
					<td><asp:label id="lbCreateDT" CssClass="DescInfoLabel" Runat="server"></asp:label></td>
				</tr>
				<tr>
					<td class="StandardLabel">Updated by :</td>
					<td><asp:label id="lbUpdateBy" CssClass="DescInfoLabel" Runat="server"></asp:label></td>
					<td class="StandardLabel">Update date :</td>
					<td><asp:label id="lbUpdateDT" CssClass="DescInfoLabel" Runat="server"></asp:label></td>
				</tr>
			</table>
		</td>
	</tr>
</table>
