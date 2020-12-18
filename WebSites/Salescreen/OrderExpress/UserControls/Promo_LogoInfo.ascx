<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.Promo_logoInfo" Codebehind="Promo_LogoInfo.ascx.cs" %>

<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td align="left"> <!--Section Title --></td>
	</tr>
	<tr>
		<td align="left">
			<table id="Table3" cellSpacing="0" cellPadding="0" width="600" border="0">
				<tr>
					<td class="SectionPageTitleInfo" colSpan="2"><asp:label id="lblTitle" runat="server"></asp:label>
					</td>
				</tr>
				<tr>
					<td valign="top"><span class="StandardLabel">ID&nbsp;:</span>
					</td>
					<td valign="top"><asp:label id="lblID" runat="server" Width="200px" CssClass="DescInfoLabel"></asp:label></td>
				</tr>
				<tr>
					<td valign="top"><span class="StandardLabel">Name&nbsp;:</span>
					</td>
					<td valign="top"><asp:Label ID="lblName" Runat="server" CssClass="DescInfoLabel"></asp:Label></td>
				</tr>
				<tr>
					<td valign="top"><span class="StandardLabel">Category&nbsp;:</span>
					</td>
					<td valign="top"><asp:Label ID="lblCategory" Runat="server" CssClass="DescInfoLabel"></asp:Label></td>
				</tr>
				<tr>
					<td valign="top"><span class="StandardLabel">Image: &nbsp;:</span>
					</td>
					<td valign="top">
						<ASP:IMAGEBUTTON id="imgBtnDetail" runat="server" ImageUrl='' CommandName="Select" CausesValidation="False"
							Height="100"></ASP:IMAGEBUTTON>
							<br />
							<asp:Label ID="Label1" runat="server" Text='*Click for larger preview' CssClass="AddressInfoDescLabel"></asp:Label>
					</td>
				</tr>
				<tr>
					<td valign="top"><span class="StandardLabel">FM ID&nbsp;:</span>
					</td>
					<td valign="top"><asp:Label ID="lblFMID" Runat="server" CssClass="DescInfoLabel"></asp:Label></td>
				</tr>
				<tr runat="server" id="trNational">
					<td valign="top"><span class="StandardLabel">National&nbsp;:</span>
					</td>
					<td valign="top"><asp:CheckBox Runat="server" ID="chkNational" Enabled="False"></asp:CheckBox></td>
				</tr>
				<tr>
					<td valign="top"><span class="StandardLabel">Enabled&nbsp;:</span>
					</td>
					<td valign="top"><asp:CheckBox Runat="server" ID="chkEnabled" Enabled="False"></asp:CheckBox></td>
				</tr>
				<tr runat="server" id="trRegion">
					<td valign="top"><span class="StandardLabel">Subdivision&nbsp;:</span>
					</td>
					<td valign="top">
						<asp:ListBox id="lbxCurrentSubdivision" runat="server" Width="100%" Enabled="False"></asp:ListBox>
					</td>
				</tr>
				<tr>
					<td valign="top"><span class="StandardLabel">Description&nbsp;:</span>
					</td>
					<td valign="top"><asp:Label ID="lblDescription" Runat="server" CssClass="DescInfoLabel"></asp:Label></td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td align="left">
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
<script>
function opendetail()
{
    var id = document.getElementById('<%=lblID.ClientID%>').innerText;
    window.open("ImageViewer.aspx?imgID="+id+"&imgType=2",null,"location=no,menubar=no,status=no,titlebar=no,toolbar=no,width=800,heigth=600");
}
</script>
