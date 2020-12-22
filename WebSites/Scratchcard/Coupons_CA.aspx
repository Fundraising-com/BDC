<%@ Register TagPrefix="efundraising" Namespace="GA.BDC.Core.Web.UI.MasterPages" Assembly="GA.BDC.Core.Web.UI.MasterPages" %>
<%@ Register TagPrefix="contentpanel" Namespace="GA.BDC.Core.Web.UI.UIControls" Assembly="GA.BDC.Core.Web.UI.UIControls" %>
<%@ Page language="c#" Codebehind="Coupons_CA.aspx.cs" AutoEventWireup="false" Inherits="GA.BDC.WEB.ScratchcardWeb.Coupons_CA" %>
<efundraising:MASTERPAGE id="MasterPage1" runat="server" master="~/MasterPage/SiteTemplate1.ascx">
	<efundraising:Content id="Content1" runat="server" ContentPlaceHolderID="cph_PageContent">
		<contentpanel:ContentPanelControl id="ContentPanelControl1" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Coupons_CA.aspx"></contentpanel:ContentPanelControl>
		<BR>
		<asp:image id="Image1" runat="server" ImageUrl="Resources/images/_ScratchcardWeb_/_classic_/en-US/common/Title/doted_line.gif"></asp:image>
		<BR>
		<P>
			<TABLE class="normal_text" id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD height="136">
						<TABLE class="normal_text" id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD colspan="2">
									<P><B>
											<contentpanel:ContentPanelControl id="ContentPanelControl2" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Coupons_CA.aspx"></contentpanel:ContentPanelControl></B></P>
									<P>
										<TABLE class="normal_text" cellSpacing="0" cellPadding="0" border="0">
											<TR>
												<TD>
													<contentpanel:ContentPanelControl id="ContentPanelControl3" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Coupons_CA.aspx"></contentpanel:ContentPanelControl></TD>
												<TD></TD>
											</TR>
										</TABLE>
									</P>
								</TD>
							</TR>
							<TR>
							<TD align="center" valign="top"><BR>
									<strong>Quebec Coupons</strong><BR>
									<img src="Resources/images/_scratchcardweb_/_classic_/en-us/coupons/QC_coupon.jpg" border="0" style="width:186px; height:334px" />
                                    <br><br>
									</TD>
								<TD align="center">
									<br>
									<strong><contentpanel:ContentPanelControl id="ContentPanelControl5" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Coupons_CA.aspx"></contentpanel:ContentPanelControl></strong><BR>
									<contentpanel:ContentPanelControl id="ContentPanelControl4" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Coupons_CA.aspx"></contentpanel:ContentPanelControl><br><br>
									<strong><contentpanel:ContentPanelControl id="ContentPanelControl6" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Coupons_CA.aspx"></contentpanel:ContentPanelControl></strong><BR>
									<contentpanel:ContentPanelControl id="ContentPanelControl7" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Coupons_CA.aspx"></contentpanel:ContentPanelControl><BR>
									</TD>
							</TR>
							<TR>
							<TD align="center" valign="top" colspan="2">
									<BR>
									<A href="SampleKit.aspx"><IMG src="Resources/images/_ScratchcardWeb_/_classic_/en-US/common/b_ordernow.gif" border="0"></A>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</P>
	</efundraising:Content>
</efundraising:MASTERPAGE>
<ContentPanel:PagePanelControl id="PagePanelControl1" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Coupons_CA.aspx"></ContentPanel:PagePanelControl>
