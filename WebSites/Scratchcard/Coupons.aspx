<%@ Register TagPrefix="efundraising" Namespace="GA.BDC.Core.Web.UI.MasterPages" Assembly="GA.BDC.Core.Web.UI.MasterPages" %>
<%@ Register TagPrefix="contentpanel" Namespace="GA.BDC.Core.Web.UI.UIControls" Assembly="GA.BDC.Core.Web.UI.UIControls" %>
<%@ Page language="c#" Codebehind="Coupons.aspx.cs" AutoEventWireup="false" Inherits="GA.BDC.WEB.ScratchcardWeb.Coupons" %>
<efundraising:MASTERPAGE id="MasterPage1" runat="server" master="~/MasterPage/SiteTemplate1.ascx">
	<efundraising:Content id="Content1" runat="server" ContentPlaceHolderID="cph_PageContent">
		<SCRIPT language="JavaScript">
		<!-- Begin
		function popUp(URL) {
		day = new Date();
		id = day.getTime();
		eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=0,scrollbars=0,location=0,statusbar=0,menubar=0,resizable=0,width=320,height=240,left = 400,top = 420');");
		}
		// End -->
		</SCRIPT>
		<MAP name="logos">
			<AREA shape="RECT" alt="" coords="1,-2,105,89" href="javascript:popUp('PopUp/PopCoupons.aspx?coupon=pizzahut')">
			<AREA shape="RECT" alt="" coords="1,93,102,162" href="javascript:popUp('PopUp/PopCoupons.aspx?coupon=aw')">
			<AREA shape="RECT" alt="" coords="4,170,105,222" href="javascript:popUp('PopUp/PopCoupons.aspx?coupon=tcby')">
			<AREA shape="RECT" alt="" coords="-7,219,105,264" href="javascript:popUp('PopUp/PopCoupons.aspx?coupon=field')">
			<AREA shape="RECT" alt="" coords="-10,271,107,334" href="javascript:popUp('PopUp/PopCoupons.aspx?coupon=ftd')">
			<AREA shape="RECT" alt="" coords="-10,341,105,421" href="javascript:popUp('PopUp/PopCoupons.aspx?coupon=blockbuster')">
			<AREA shape="RECT" alt="" coords="0,428,106,478" href="javascript:popUp('PopUp/PopCoupons.aspx?coupon=jiffy')">
			<AREA shape="RECT" alt="" coords="4,481,102,544" href="javascript:popUp('PopUp/PopCoupons.aspx?coupon=amc')">
		</MAP>
		<contentpanel:ContentPanelControl id="ContentPanelControl1" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Coupons.aspx"></contentpanel:ContentPanelControl>
		<BR>
		<asp:image id="Image1" runat="server" ImageUrl="Resources/images/_ScratchcardWeb_/_classic_/en-US/common/Title/doted_line.gif"></asp:image>
		<BR>
		<P>
			<TABLE class="normal_text" id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD height="136">
						<TABLE class="normal_text" id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD>
									<P><B>
											<contentpanel:ContentPanelControl id="ContentPanelControl2" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Coupons.aspx"></contentpanel:ContentPanelControl></B></P>
									<P>
										<TABLE class="normal_text" cellSpacing="0" cellPadding="0" border="0">
											<TR>
												<TD>
													<contentpanel:ContentPanelControl id="ContentPanelControl3" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Coupons.aspx"></contentpanel:ContentPanelControl></TD>
												<TD>
													<contentpanel:ContentPanelControl id="ContentPanelControl6" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Coupons.aspx"></contentpanel:ContentPanelControl></TD>
											</TR>
										</TABLE>
									</P>
								</TD>
								<TD vAlign="top" rowSpan="2">
									<contentpanel:ContentPanelControl id="ContentPanelControl5" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Coupons.aspx"></contentpanel:ContentPanelControl></TD>
							</TR>
							<TR>
								<TD align="center"><BR>
									<P>
										<contentpanel:ContentPanelControl id="ContentPanelControl4" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Coupons.aspx"></contentpanel:ContentPanelControl></P>
									<BR>
									&nbsp;
									<contentpanel:ButtonPanelControl id="ButtonPanelControl2" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Coupons.aspx"
										ButtonType="IMAGE" CodeName="btnOrderNow"></contentpanel:ButtonPanelControl><A href="SampleKit.aspx"><IMG src="Resources/images/_ScratchcardWeb_/_classic_/en-US/common/b_ordernow.gif" border="0"></A></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</P>
	</efundraising:Content>
</efundraising:MASTERPAGE>
<ContentPanel:PagePanelControl id="PagePanelControl1" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Coupons.aspx"></ContentPanel:PagePanelControl>
