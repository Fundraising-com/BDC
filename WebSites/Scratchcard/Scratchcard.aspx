<%@ Register TagPrefix="uc1" TagName="DidYouKnow" Src="Components/User/Controls/Common/DidYouKnow.ascx" %>
<%@ Page language="c#" Codebehind="Scratchcard.aspx.cs" AutoEventWireup="true" Inherits="GA.BDC.WEB.ScratchcardWeb.Scratchcard" %>
<%@ Register TagPrefix="contentpanel" Namespace="GA.BDC.Core.Web.UI.UIControls" Assembly="GA.BDC.Core.Web.UI.UIControls" %>
<%@ Register TagPrefix="efundraising" Namespace="GA.BDC.Core.Web.UI.MasterPages" Assembly="GA.BDC.Core.Web.UI.MasterPages" %>
<efundraising:MASTERPAGE id="MasterPage1" runat="server" master="~/MasterPage/SiteTemplate1.ascx">
	<efundraising:Content id="Content1" runat="server" ContentPlaceHolderID="cph_PageContent">
		<TABLE id="Table6" cellSpacing="0" cellPadding="0" width="100%" border="0">
			<TR>
				<TD width="50">
					<asp:Image id="imgIcone" runat="server"></asp:Image></TD>
				<TD vAlign="middle">
					<asp:Image id="imgTitre" runat="server"></asp:Image><BR>
					<IMG src="Resources/images/_ScratchcardWeb_/_classic_/en-US/Scratchcards/doted_line_GT.gif"></TD>
			</TR>
		</TABLE>
		<BR>
		<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
			<TR>
				<TD>
					<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="300" border="0">
						<TR>
							<TD>
								<asp:ImageButton id="imgCardLeft" runat="server" ImageUrl="Resources/images/_ScratchcardWeb_/_classic_/en-US/Scratchcards/back_pannel.gif"></asp:ImageButton></TD>
							<TD>
								<asp:image id="imgCard1" runat="server"></asp:image></TD>
							<TD>
								<asp:Image id="imgYourLogo" runat="server" ImageUrl="Resources/images/_scratchcardweb_/_classic_/en-us/scratchcards/logohere.gif"></asp:Image></TD>
						</TR>
					</TABLE>
					<P>
						<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="300" border="0">
							<TR>
								<TD>
									<asp:ImageButton id="ImageButton1" runat="server" ImageUrl="Resources/images/_ScratchcardWeb_/_classic_/en-US/Scratchcards/back_pannel.gif"></asp:ImageButton></TD>
								<TD>
									<asp:Image id="imgCard2" runat="server" Visible="False"></asp:Image></TD>
								<TD>
									<contentpanel:ContentPanelControl id="ContentPanelControl4" runat="server" Visible="False" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Scratchcard.aspx"></contentpanel:ContentPanelControl></TD>
							</TR>
						</TABLE>
					</P>
					<P>
						<TABLE class="normal_text" id="Table4" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD width="95">&nbsp;</TD>
								<TD>
									<P>
										<contentpanel:ContentPanelControl id="ContentPanelControl5" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Scratchcard.aspx"></contentpanel:ContentPanelControl></P>
									<P>&nbsp;&nbsp;
										<contentpanel:ButtonPanelControl id="ButtonPanelControl3" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Scratchcard.aspx"
											CodeName="btnOrderNow" ButtonType="IMAGE"></contentpanel:ButtonPanelControl><A href="SampleKit.aspx"><IMG src="Resources/images/_ScratchcardWeb_/_classic_/en-US/common/b_ordernow.gif" border="0"></A></P>
								</TD>
							</TR>
						</TABLE>
					</P>
					<P>
						<asp:image id="Image1" runat="server" ImageUrl="Resources/images/_ScratchcardWeb_/_classic_/en-US/common/Title/doted_line.gif"></asp:image></P>
					<P>
						<TABLE class="normal_text" id="Table5" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD vAlign="top"><B>
										<contentpanel:ContentPanelControl id="ContentPanelControl6" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Scratchcard.aspx"></contentpanel:ContentPanelControl></B><BR>
									<contentpanel:ContentPanelControl id="ContentPanelControl7" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Scratchcard.aspx"></contentpanel:ContentPanelControl></TD>
								<TD align="center">
									<contentpanel:ContentPanelControl id="ContentPanelControl8" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Scratchcard.aspx"></contentpanel:ContentPanelControl>
									<contentpanel:ContentPanelControl id="ContentPanelControl9" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Scratchcard.aspx"></contentpanel:ContentPanelControl></TD>
							</TR>
						</TABLE>
					</P>
				</TD>
			</TR>
			<TR>
				<TD>
					<P>
						<DIV class="normal_text" style="FONT-WEIGHT: bold; MARGIN-LEFT: 40px">
							<contentpanel:ContentPanelControl id="ContentPanelControl10" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Scratchcard.aspx"></contentpanel:ContentPanelControl></SPAN></DIV>
					<P></P>
					<P>
						<DIV align="center">
							<contentpanel:ContentPanelControl id="ContentPanelControl11" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Scratchcard.aspx"></contentpanel:ContentPanelControl></DIV>
					<P></P>
				</TD>
			</TR>
		</TABLE>
	</efundraising:Content>
	<efundraising:Content id="Content2" runat="server" ContentPlaceHolderID="cph_CornerImage">
		<asp:Image id="imgTopRight" runat="server" ImageAlign="AbsBottom" BorderStyle="None" BorderWidth="0px"></asp:Image>
	</efundraising:Content>
	<efundraising:Content id="Content3" runat="server" ContentPlaceHolderID="cph_DidYouKnow">
		<uc1:DidYouKnow id="DidYouKnow1" runat="server"></uc1:DidYouKnow>
	</efundraising:Content>
</efundraising:MASTERPAGE>
<ContentPanel:PagePanelControl id="PagePanelControl1" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Scratchcard.aspx"></ContentPanel:PagePanelControl>
