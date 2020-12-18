<%@ Register TagPrefix="uc1" TagName="FooterGroupType" Src="../Components/User/Controls/Menu/Footer/FooterGroupType.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Top" Src="../Components/User/Controls/Menu/Top/Top.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="SiteTemplate1.ascx.cs" Inherits="GA.BDC.WEB.ScratchcardWeb.MasterPage.SiteTemplate1" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../Components/User/Controls/Menu/Footer/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftMenu" Src="../Components/User/Controls/Menu/Left/LeftMenu.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CreditCards" Src="../Components/User/Controls/Menu/Left/CreditCards.ascx" %>
<%@ Register TagPrefix="buttonpanel" Namespace="GA.BDC.Core.Web.UI.UIControls" Assembly="GA.BDC.Core.Web.UI.UIControls" %>
<%@ Register TagPrefix="uc1" TagName="SuccessStory" Src="../Components/User/Controls/Common/SuccessStory.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Newsletter" Src="../Components/User/Controls/Common/Newsletter.ascx" %>
<%@ Register TagPrefix="efundraising" Namespace="GA.BDC.Core.Web.UI.MasterPages" Assembly="GA.BDC.Core.Web.UI.MasterPages" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>
			<asp:Literal id="TitleLiteral" runat="server"></asp:Literal></title>
		<asp:literal id="MetaContentLiteral" runat="server"></asp:literal>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="Resources/css/_ScratchcardWeb_/classic/en-US/Standard.css" type="text/css"
			rel="stylesheet">
		<script language="JavaScript" type="text/JavaScript">
		<!--
		function MM_swapImgRestore() { //v3.0
		var i,x,a=document.MM_sr; for(i=0;a&&i<a.length&&(x=a[i])&&x.oSrc;i++) x.src=x.oSrc;
		}
		
		function MM_preloadImages() { //v3.0
		var d=document; if(d.images){ if(!d.MM_p) d.MM_p=new Array();
			var i,j=d.MM_p.length,a=MM_preloadImages.arguments; for(i=0; i<a.length; i++)
			if (a[i].indexOf("#")!=0){ d.MM_p[j]=new Image; d.MM_p[j++].src=a[i];}}
		}
		
		function MM_findObj(n, d) { //v4.01
		var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
			d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
		if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
		for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);
		if(!x && d.getElementById) x=d.getElementById(n); return x;
		}
		
		function MM_swapImage() { //v3.0
		var i,j=0,x,a=MM_swapImage.arguments; document.MM_sr=new Array; for(i=0;i<(a.length-2);i+=3)
		if ((x=MM_findObj(a[i]))!=null){document.MM_sr[j++]=x; if(!x.oSrc) x.oSrc=x.src; x.src=a[i+2];}
		}
		//-->
		</script>
	</HEAD>
	<body background="Resources/images/_ScratchcardWeb_/_classic_/en-US/common/fond.gif" onload="MM_preloadImages('Resources/images/_ScratchcardWeb_/_classic_/en-US/common/icones/B_soccer.gif','Resources/images/_ScratchcardWeb_/_classic_/en-US/common/icones/B_basket.gif','Resources/images/_ScratchcardWeb_/_classic_/en-US/common/icones/B_foot.gif','Resources/images/_ScratchcardWeb_/_classic_/en-US/common/icones/B_softball.gif','Resources/images/_ScratchcardWeb_/_classic_/en-US/common/icones/B_baseball.gif','Resources/images/_ScratchcardWeb_/_classic_/en-US/common/icones/B_hockey.gif','Resources/images/_ScratchcardWeb_/_classic_/en-US/common/icones/B_volley.gif','Resources/images/_ScratchcardWeb_/_classic_/en-US/common/icones/B_cheer.gif','Resources/images/_ScratchcardWeb_/_classic_/en-US/common/icones/B_bowling.gif','Resources/images/_ScratchcardWeb_/_classic_/en-US/common/icones/B_elementary.gif','Resources/images/_ScratchcardWeb_/_classic_/en-US/common/icones/B_highschool.gif','Resources/images/_ScratchcardWeb_/_classic_/en-US/common/icones/B_university.gif','Resources/images/_ScratchcardWeb_/_classic_/en-US/common/icones/B_church.gif','Resources/images/_ScratchcardWeb_/_classic_/en-US/common/icones/B_other.gif')">
		<form id="Form1" method="post" runat="server">
			<div align="center">
				<TABLE id="TableMain" cellSpacing="0" cellPadding="0" width="754" border="0">
					<TR>
						<TD style="HEIGHT: 125px" vAlign="top" align="left"><uc1:top id="Top1" runat="server"></uc1:top></TD>
						<TD style="HEIGHT: 125px" vAlign="top" align="left"><IMG src="Resources/images/_ScratchcardWeb_/_classic_/en-US/common/middle.gif" align="top"></TD>
						<TD style="HEIGHT: 125px" vAlign="top" align="left"><a href="SpecialOffer.aspx?tc=imgTopRight"><efundraising:contentplaceholder id="cph_CornerImage" runat="server" BorderWidth="0px" BorderStyle="None"><IMG src="Resources/images/_ScratchcardWeb_/_classic_/en-US/common/top_right.gif" border="0"></efundraising:contentplaceholder></a></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 218px" vAlign="top" colSpan="3"><asp:image id="eifijief" runat="server" ImageUrl="Resources/images/_ScratchcardWeb_/_classic_/en-US/common/dot_top.gif"></asp:image></TD>
					</TR>
					<TR>
						<TD vAlign="top" colSpan="3">
							<TABLE id="TableCenter" cellSpacing="0" cellPadding="0" width="100%" bgColor="#ffffff"
								border="0">
								<TR>
									<TD vAlign="top" width="213">
										<TABLE id="TableLeftMenu" cellSpacing="0" cellPadding="0" width="100%" bgColor="#ffffff"
											border="0">
											<TR>
												<TD align="center"><uc1:leftmenu id="LeftMenu1" runat="server"></uc1:leftmenu></TD>
											</TR>
											<TR>
												<TD align="center"><iframe src="Newsletter.aspx" width="210" height="120" frameborder="0" scrolling="no" marginheight="0"
														marginwidth="0"></iframe>
												</TD>
											</TR>
											<TR>
												<TD align="center">
													<asp:image id="hefholclkjdf" runat="server" ImageUrl="Resources/images/_ScratchcardWeb_/_classic_/en-US/common/dotedline_small.gif"></asp:image></TD>
											</TR>
											<TR>
												<TD align="center">
													<P><efundraising:contentplaceholder id="cph_DidYouKnow" runat="server"></efundraising:contentplaceholder></P>
													<P><uc1:successstory id="SuccessStory1" runat="server"></uc1:successstory><uc1:creditcards id="CreditCards1" runat="server"></uc1:creditcards></P>
												</TD>
											</TR>
										</TABLE>
									</TD>
									<TD vAlign="top" width="1" bgColor="#666666"></TD>
									<TD vAlign="top">
										<table cellSpacing="0" cellPadding="15" width="100%" border="0">
											<tr>
												<td><efundraising:contentplaceholder id="cph_PageContent" runat="server">CONTENT 
                  HERE</efundraising:contentplaceholder></td>
											</tr>
										</table>
									</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD vAlign="bottom" align="center" bgColor="#dddcdc" colSpan="3"><uc1:footergrouptype id="FooterGroupType1" runat="server"></uc1:footergrouptype></TD>
					</TR>
					<TR>
						<TD vAlign="top" align="center" bgColor="#dddcdc" colSpan="3"><uc1:footer id="Footer1" runat="server"></uc1:footer></TD>
					</TR>
				</TABLE>
			</div>
		</form>
	</body>
</HTML>
