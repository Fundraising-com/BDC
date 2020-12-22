<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Scratchcards_Grid.ascx.cs" Inherits="GA.BDC.WEB.ScratchcardWeb.Components.User.Controls.Common.Scratchcards_Grid" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<SCRIPT language="javascript" src="resources/javascript/common.js"></SCRIPT>
<table class="normal_text" cellPadding="0" border="0">
	<TBODY>
		<TR>
			<TD style="HEIGHT: 24px"></TD>
		</TR>
		<tr>
			<td><asp:label id="lblCategory" CssClass="SmallTextBold" Runat="server" Font-Bold="True" ForeColor="Red"></asp:label></td>
		</tr>
		<TR>
			<TD style="HEIGHT: 8px"><asp:label class="small_text_bold" id="shortDescLabel" Runat="server" Font-Bold="True" Width="424px"></asp:label></TD>
		</TR>
		<tr>
			<td style="HEIGHT: 1px"><asp:image id="Image2" runat="server" ImageUrl="../../../../Resources/images/_ScratchcardWeb_/_classic_/en-US/common/Title/doted_line.gif"></asp:image></td>
		<TR>
			<TD vAlign="top"></TD>
		</TR>
		<tr>
		<tr>
			<td vAlign="top" align="left"><asp:datalist id="dtlScratchcardsPics" Runat="server" RepeatDirection="Horizontal">
					<ItemTemplate>
						<TABLE class="small_red_bold" style="WIDTH: 160px; HEIGHT: 91px" cellSpacing="2" cols="1"
							width="134" align="center" border="0">
							<TR vAlign="top">
								<TD vAlign="top" align="center">
									<asp:HyperLink Runat="Server" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "ScratchCardImageUrl") %>' Text='<%# DataBinder.Eval(Container.DataItem, "ProductDescription.ImageAltText") %>'  ID="Hyperlink1"/>
								</TD>
							</TR>
							<TR>
								<TD align="center">
									<asp:Label class="small_red_bold" id="Label1" runat="server">NEW! - </asp:Label><span class=small_black_underlined href='<%# DataBinder.Eval(Container.DataItem, "ScratchCardPageURL") %>'><%# DataBinder.Eval(Container.DataItem, "Name") %></span></TD>
							</TR>
						</TABLE>
					</ItemTemplate>
				</asp:datalist></td>
		</tr>
	</TBODY>
</table>
