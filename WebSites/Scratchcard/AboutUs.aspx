<%@ Register TagPrefix="efundraising" Namespace="GA.BDC.Core.Web.UI.MasterPages" Assembly="GA.BDC.Core.Web.UI.MasterPages" %>
<%@ Register TagPrefix="contentpanel" Namespace="GA.BDC.Core.Web.UI.UIControls" Assembly="GA.BDC.Core.Web.UI.UIControls" %>
<%@ Page language="c#" Codebehind="AboutUs.aspx.cs" AutoEventWireup="false" Inherits="GA.BDC.WEB.ScratchcardWeb.AboutUs" %>
<efundraising:MASTERPAGE id="MasterPage1" runat="server" master="~/MasterPage/SiteTemplate1.ascx">
	<efundraising:Content id="Content1" runat="server" ContentPlaceHolderID="cph_PageContent">
		<SCRIPT language="JavaScript">
		<!-- Begin
		function popUp(URL) {
		day = new Date();
		id = day.getTime();
		eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=0,scrollbars=0,location=0,statusbar=0,menubar=0,resizable=0,width=501,height=550,left = 390,top = 212');");
		}
		// End -->
		</SCRIPT>
		<MAP name="board">
			<AREA shape="RECT" alt="" coords="382,176,448,254" href="javascript:popUp('PopUp/aboutus/Kim.aspx')">
			<AREA shape="RECT" alt="" coords="390,60,460,150" href="javascript:popUp('PopUp/aboutus/Steve.aspx')">
			<AREA shape="RECT" alt="" coords="288,149,356,238" href="javascript:popUp('PopUp/aboutus/Alexandre.aspx')">
			<AREA shape="RECT" alt="" coords="304,30,374,122" href="javascript:popUp('PopUp/aboutus/Marc.aspx')">
			<AREA shape="RECT" alt="" coords="2,48,272,244" href="javascript:popUp('PopUp/aboutus/Team.aspx')">
		</MAP>
		<contentpanel:ContentPanelControl id="ContentPanelControl1" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\AboutUs.aspx"></contentpanel:ContentPanelControl>
		<BR>
		<asp:image id="Image1" runat="server" ImageUrl="Resources/images/_ScratchcardWeb_/_classic_/en-US/common/Title/doted_line.gif"></asp:image>
		<BR>
		<P>
			<TABLE class="normal_text" id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD align="center">
						<contentpanel:ContentPanelControl id="ContentPanelControl2" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\AboutUs.aspx"></contentpanel:ContentPanelControl></TD>
				</TR>
				<TR>
					<TD align="center">
						<TABLE class="normal_text" cellSpacing="0" cellPadding="0" width="97%" border="0">
							<TR>
								<TD align="center"><BR>
									<B>
										<contentpanel:ContentPanelControl id="ContentPanelControl3" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\AboutUs.aspx"></contentpanel:ContentPanelControl></B></TD>
							</TR>
							<TR>
								<TD align="left"><BR>
									<contentpanel:ContentPanelControl id="ContentPanelControl4" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\AboutUs.aspx"></contentpanel:ContentPanelControl></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</P>
	</efundraising:Content>
</efundraising:MASTERPAGE>
<ContentPanel:PagePanelControl id="PagePanelControl1" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\AboutUs.aspx"></ContentPanel:PagePanelControl>
