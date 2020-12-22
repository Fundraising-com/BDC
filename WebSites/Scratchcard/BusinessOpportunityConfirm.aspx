<%@ Register TagPrefix="efundraising" Namespace="GA.BDC.Core.Web.UI.MasterPages" Assembly="GA.BDC.Core.Web.UI.MasterPages" %>
<%@ Register TagPrefix="contentpanel" Namespace="GA.BDC.Core.Web.UI.UIControls" Assembly="GA.BDC.Core.Web.UI.UIControls" %>
<%@ Page language="c#" Codebehind="BusinessOpportunityConfirm.aspx.cs" AutoEventWireup="false" Inherits="GA.BDC.WEB.ScratchcardWeb.BusinessOpportunityConfirm" %>
<efundraising:MASTERPAGE id="MasterPage1" runat="server" master="~/MasterPage/SiteTemplate1.ascx">
	<efundraising:Content id="Content1" runat="server" ContentPlaceHolderID="cph_PageContent">
		<TABLE id="Table1" cellSpacing="0" cellPadding="3" width="100%" border="0">
			<TR>
				<TD class="TopForm"><STRONG>Business Opportunity</STRONG></TD>
			</TR>
			<TR>
				<TD>
					<P class="NormalText">Thank you for your interest in the Give ‘N’ Take Scratch Card 
						Agent Program. Your request is being processed and your free fundraising agent 
						information kit will be mailed to you shortly.
					</P>
					<P class="NormalText">If you have any questions, please do not hesitate to call 
						toll-free 1-866-517-2372 and we will be more than happy to assist you.</P>
				</TD>
			</TR>
		</TABLE>
	</efundraising:Content>
</efundraising:MASTERPAGE>
