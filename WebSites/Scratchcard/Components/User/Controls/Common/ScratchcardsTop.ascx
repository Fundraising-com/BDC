<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ScratchcardsTop.ascx.cs" Inherits="GA.BDC.WEB.ScratchcardWeb.Components.User.Controls.Common.ScratchcardsTop" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE class="normal_text" id="Table1" style="WIDTH: 480px; HEIGHT: 373px" cellSpacing="1"
	cellPadding="1" width="480" border="0">
	<TR>
		<TD><asp:label id="FirstPartLabel" Runat="server" CssClass="NormalText"></asp:label></TD>
	</TR>
	<TR>
		<TD height="10"></TD>
	</TR>
	<TR>
		<TD>
			<ul>
				<asp:repeater id="bulletList" Runat="server">
					<ItemTemplate>
						<li>
							<asp:label id="Source" text='<%# Container.DataItem %>' runat="server" />
						</li>
					</ItemTemplate>
				</asp:repeater><asp:repeater id="Repeater1" runat="server"></asp:repeater></ul>
		</TD>
	</TR>
	<TR>
		<TD><asp:label id="SecondPartLabel" Runat="server" CssClass="NormalText"></asp:label></TD>
	</TR>
	<TR>
		<TD style="HEIGHT: 35px">
			<asp:Image id="imgScratchcard" runat="server"></asp:Image></TD>
	</TR>
	<TR>
		<TD align="center" style="HEIGHT: 52px"><A href="SampleKit.aspx"></A>
			<asp:ImageButton id="orderNowButton" runat="server" ImageUrl="../../../../Resources/images/_ScratchcardWeb_/_classic_/en-US/common/ordernow.gif"></asp:ImageButton><A href="SampleKit.aspx"><IMG src="Resources/images/_ScratchcardWeb_/_classic_/en-US/common/b_ordernow.gif" border="0"></A></TD>
	</TR>
	<TR>
		<TD class="big_black_bold"><asp:label id="ThirdPartTitleLabel" Runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD class="big_black_bold"></TD>
	</TR>
	<TR>
		<TD><asp:label id="ThirdPartLabel" Runat="server" CssClass="NormalText"></asp:label></TD>
	</TR>
	<TR>
		<TD style="HEIGHT: 23px"></TD>
	</TR>
	
</TABLE>
