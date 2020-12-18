<%@ Register TagPrefix="contentpanel" Namespace="GA.BDC.Core.Web.UI.UIControls" Assembly="GA.BDC.Core.Web.UI.UIControls" %>
<%@ Page language="c#" Codebehind="HowItWorks.aspx.cs" AutoEventWireup="false" Inherits="GA.BDC.WEB.ScratchcardWeb.HowItWorks" %>
<%@ Register TagPrefix="efundraising" Namespace="GA.BDC.Core.Web.UI.MasterPages" Assembly="GA.BDC.Core.Web.UI.MasterPages" %>
<efundraising:MASTERPAGE id="MasterPage1" runat="server" master="~/MasterPage/SiteTemplate1.ascx">
	<efundraising:Content id="Content1" runat="server" ContentPlaceHolderID="cph_PageContent">
		<contentpanel:ContentPanelControl id="ContentPanelControl1" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\HowItWorks.aspx"></contentpanel:ContentPanelControl>
		<BR>
		<asp:image id="Image1" runat="server" ImageUrl="Resources/images/_ScratchcardWeb_/_classic_/en-US/common/Title/doted_line.gif"></asp:image>
		<BR>
		<TABLE class="normal_text" id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
			<TR>
				<TD><BR>
					<B>
						<contentpanel:ContentPanelControl id="ContentPanelControl2" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\HowItWorks.aspx"></contentpanel:ContentPanelControl></B><BR>
					<contentpanel:ContentPanelControl id="ContentPanelControl3" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\HowItWorks.aspx"></contentpanel:ContentPanelControl></TD>
			</TR>
			<TR>
				<TD align="center"><BR>
					<TABLE class="normal_text" id="Table2" cellSpacing="0" cellPadding="0" width="90%" border="0">
						<TR>
							<TD width="118" rowSpan="3">
								<asp:Image id="Image2" runat="server" ImageUrl="Resources/images/_ScratchcardWeb_/_classic_/en-US/howitworks/123.gif"></asp:Image></TD>
							<TD>
								<contentpanel:ContentPanelControl id="ContentPanelControl4" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\HowItWorks.aspx"></contentpanel:ContentPanelControl></TD>
						</TR>
						<TR>
							<TD>
								<contentpanel:ContentPanelControl id="ContentPanelControl5" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\HowItWorks.aspx"></contentpanel:ContentPanelControl></TD>
						</TR>
						<TR>
							<TD>
								<contentpanel:ContentPanelControl id="ContentPanelControl6" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\HowItWorks.aspx"></contentpanel:ContentPanelControl></TD>
						</TR>
					</TABLE>
				</TD>
			</TR>
			<TR>
				<TD align="center"><BR>
					<contentpanel:ContentPanelControl id="ContentPanelControl7" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\HowItWorks.aspx"></contentpanel:ContentPanelControl></TD>
			</TR>
			<TR>
				<TD align="center"><BR>
					<A href="SampleKit.aspx"></A>
					<contentpanel:ButtonPanelControl id="ButtonPanelControl2" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\HowItWorks.aspx"
						CodeName="btnOrderNow" ButtonType="IMAGE"></contentpanel:ButtonPanelControl><A href="SampleKit.aspx"><IMG src="Resources/images/_ScratchcardWeb_/_classic_/en-US/common/b_ordernow.gif" border="0"></A></TD>
			</TR>
		</TABLE>
	</efundraising:Content>
</efundraising:MASTERPAGE>
<ContentPanel:PagePanelControl id="PagePanelControl1" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\HowItWorks.aspx"></ContentPanel:PagePanelControl>
