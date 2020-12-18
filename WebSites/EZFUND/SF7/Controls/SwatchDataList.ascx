<%@ Control Language="vb" AutoEventWireup="false" Codebehind="SwatchDataList.ascx.vb" Inherits="StoreFront.StoreFront.SwatchDataList" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<TABLE class="Content" id="Table4" border="0" runat="server">
	<TR>
		<TD vAlign="center" align="right"></TD>
		<TD vAlign="center" align="left">
			<asp:datalist id="dlSwatch" Runat="server" Width="100%" ItemStyle-VerticalAlign="Top" RepeatDirection="Horizontal">
				<ItemTemplate>
					<table border="0" width="100%" cellpadding="0" cellspacing="0" class="Content" runat="server" ID="MainTable">
						<tr>
							<td align="Center">
								<%#"<SCRIPT language=JavaScript>preload(""thumb" & Container.ItemIndex & """,""" & IIf(Eval("LargeImage").ToLower.StartsWith("http://") OrElse Eval("LargeImage").ToLower.StartsWith("https://"), Eval("LargeImage"), ResolveUrl("~/" & DataBinder.Eval(Container.DataItem, "LargeImage"))) & """)</SCRIPT>"%>
								<asp:HyperLink Runat="server" NavigateUrl='' onmouseover='' Visible='<%#DataBinder.Eval(Container.DataItem,"ShowImage") %>' ID="LinkSwatch">
									<asp:Image Runat="Server" ID="SwatchImage" borderwidth=0 ImageUrl='<%#IIf(Eval("LittleImage").ToLower.StartsWith("http://") OrElse Eval("LittleImage").ToLower.StartsWith("https://"), Eval("LittleImage"), ResolveURL("../" & DataBinder.Eval(Container.DataItem,"LittleImage")))%>'>
									</asp:Image>
								</asp:HyperLink>
							</td>
						</tr>
						<tr>
							<td runat="server" id="tdSwatch">
								<asp:HyperLink Runat="server" NavigateUrl='' onmouseover='' Visible='<%#DataBinder.Eval(Container.DataItem,"ShowDescription") %>' ID="LinkSwatchText">
									<asp:Label Runat="Server" ID="SwatchDescription" borderwidth="0">
										<%#DataBinder.Eval(Container.DataItem,"Description") %>
									</asp:Label>
								</asp:HyperLink>
							</td>
						</tr>
					</table>
				</ItemTemplate>
			</asp:datalist>
		</TD>
		<TD vAlign="left"></TD>
	</TR>
</TABLE>
