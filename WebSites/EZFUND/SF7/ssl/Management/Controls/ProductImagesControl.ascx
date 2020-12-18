<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ProductImagesControl.ascx.vb" Inherits="StoreFront.StoreFront.ProductImagesControl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<%@ Register TagPrefix="uc1" TagName="UploadControl" Src="UploadControl.ascx" %>
<input id="ProdUID" type="hidden" name="ProdUID" runat="server">
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
	<TBODY>
		<tr>
			<TD class="Content" width="1" colSpan="6">&nbsp;</TD>
		</tr>
		<TR>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="ContentTableHeader" noWrap align="left" colSpan="4">&nbsp;&nbsp;Product&nbsp;Swatches
			</TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" rowspan="2">
				Attributes with Images:<br>
				<asp:checkboxlist id="cblAttributes" CssClass="content" Runat="server" Width="100%" RepeatColumns="1">
				</asp:checkboxlist>
				<br>
				<asp:LinkButton ID="cmdApply" Runat="server">
					<asp:Image BorderWidth="0" ID="imgApply" runat="server" ImageUrl="../images/apply.jpg" AlternateText="Apply"></asp:Image>
				</asp:LinkButton>
			</td>
			<td class="content" align="left">Swatches&nbsp;Displayed&nbsp;Per&nbsp;Row:&nbsp;&nbsp;&nbsp;&nbsp;<asp:textbox id="SwatchesPerRow" runat="server" Width="50px"></asp:textbox></td>
			<td class="content" align="left" colSpan="2">Allignment&nbsp;To&nbsp;Image<asp:dropdownlist id="SwatchAllignment" Runat="server">
					<asp:ListItem Value="Left">Left</asp:ListItem>
					<asp:ListItem Value="Right">Right</asp:ListItem>
					<asp:ListItem Value="Top">Top</asp:ListItem>
					<asp:ListItem Value="Bottom">Bottom</asp:ListItem>
				</asp:dropdownlist></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" align="left"><asp:checkbox id="ShowCloseUpLink" runat="server" checked='' Text="Text&nbsp;Link&nbsp;To&nbsp;Close&nbsp;Up&nbsp;View"></asp:checkbox>&nbsp;&nbsp;<br>
				&nbsp;&nbsp;&nbsp;&nbsp;Link Text
				<asp:textbox id="CloseUpLinkText" Runat="server" Text=''></asp:textbox><br>
				<asp:checkbox id="LinkBigImage" runat="server" checked='' Text="Link&nbsp;Large&nbsp;Image&nbsp;To&nbsp;Close&nbsp;Up&nbsp;View"></asp:checkbox>&nbsp;&nbsp;
			</td>
			<td class="content" align="left" colSpan="2">
				Description&nbsp;Alignment
				<asp:dropdownlist id="DescriptionAllignment" Runat="server">
					<asp:ListItem Value="Left">Left</asp:ListItem>
					<asp:ListItem Value="Center">Center</asp:ListItem>
					<asp:ListItem Value="Right">Right</asp:ListItem>
				</asp:dropdownlist><br>
				<asp:RadioButton id="ChangeOnClick" GroupName="ChangeType" runat="server" Text="Change&nbsp;Swatch&nbsp;On&nbsp;Click"></asp:RadioButton>&nbsp;&nbsp;<br>
				<asp:RadioButton id="ChangeOnMouseover" runat="server" GroupName="ChangeType" Text="Change&nbsp;Swatch&nbsp;On&nbsp;Mouseover"></asp:RadioButton></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" colSpan="6" height="1"><IMG height="1" src="images/clear.gif"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" vAlign="top" align="left" colSpan="4">
				<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<asp:repeater id="Swatches" runat="server">
					<ItemTemplate>
						<TR>
							<TD class="ContentTableHeader" noWrap align="left" colSpan="4">&nbsp;&nbsp;<%# DataBinder.Eval(Container.DataItem,"Name") %></TD>
							<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
						</TR>
						<TR>
							<TD align="left" valign="middle">
								<asp:LinkButton ID="lnkMoveUp" Runat="server" OnClick="MoveUp" CommandName='<%# DataBinder.Eval(Container.DataItem,"SwatchID") %>'>
									<asp:Image BorderWidth="0" ID="Image2" runat="server" ImageUrl="../images/moveup.jpg" AlternateText="Move Down"></asp:Image>
								</asp:LinkButton><br>
								<br>
								<asp:LinkButton ID="lnkMoveDown" Runat="server" OnClick="MoveDown" CommandName='<%# DataBinder.Eval(Container.DataItem,"SwatchID") %>'>
									<asp:Image BorderWidth="0" ID="Image3" runat="server" ImageUrl="../images/movedown.jpg" AlternateText="Move Down"></asp:Image>
								</asp:LinkButton>
							</TD>
							<TD class="Content" valign="top" align="left" colspan="2">
								&nbsp;Description:<br>
								&nbsp;&nbsp;
								<asp:textbox Runat="server" ID="SwatchDescription" Text='<%# DataBinder.Eval(Container.DataItem,"Description") %>'>
								</asp:textbox><br>
								<asp:checkbox id="ShowDescription" Text="Display&nbsp;Description" runat="server" checked='<%# DataBinder.Eval(Container.DataItem,"ShowDescription") %>'>
								</asp:checkbox>&nbsp;&nbsp;<br>
								<asp:checkbox id="ShowImage" Text="Display&nbsp;Image" runat="server" checked='<%# DataBinder.Eval(Container.DataItem,"ShowImage") %>'>
								</asp:checkbox><br>
								<br>
							</TD>
							<td align="right">
								<uc1:UploadControl FileType="1" LabelDisplay="Swatch&nbsp;Image" FileText='<%# DataBinder.Eval(Container.DataItem,"LittleImage") %>' id="LittleImage" runat="server">
								</uc1:UploadControl><br>
								<uc1:UploadControl FileType="1" LabelDisplay="Cart&nbsp;Image" FileText='<%# DataBinder.Eval(Container.DataItem,"ThumbnailImage") %>' id="ThumbnailImage" runat="server">
								</uc1:UploadControl><br>
								<uc1:UploadControl FileType="1" LabelDisplay="Large&nbsp;Image" FileText='<%# DataBinder.Eval(Container.DataItem,"LargeImage") %>' id="BigImage" runat="server">
								</uc1:UploadControl><br>
								<uc1:UploadControl FileType="1" LabelDisplay="Close&nbsp;Up&nbsp;Image" FileText='<%# DataBinder.Eval(Container.DataItem,"CloseUpImage") %>' id="CloseUpImage" runat="server">
								</uc1:UploadControl>
							</td>
							<td>
								<asp:LinkButton ID="cmdDelete" Runat="server" OnClick="DeleteSwatch" CommandName='<%# DataBinder.Eval(Container.DataItem,"SwatchID") %>'>
									<asp:Image BorderWidth="0" ID="imgDelete" runat="server" ImageUrl="../images/icon_delete.gif" AlternateText="Delete"></asp:Image>
								</asp:LinkButton>
							</td>
						</TR>
						<tr>
							<td colspan="5"><hr></td>
						</tr>
					</ItemTemplate>
				</asp:repeater>
					<tr><TD class="Content" width="1" colSpan="5">&nbsp;</TD></tr>
					<TR><TD class="ContentTable" colSpan="5" height="1"><IMG height="1" src="images/clear.gif"></TD></TR>
					<tr><TD class="Content" width="1" colSpan="5">&nbsp;</TD></tr>
					<TR>
						<TD class="ContentTableHeader" noWrap align="left" colSpan="4">&nbsp;&nbsp;New&nbsp;Image</TD>
						<TD class="ContentTableHeader" width="1">&nbsp;</TD>
					</TR>
					<TR>
						<TD class="Content" width="1">&nbsp;</TD>
						<TD class="Content" vAlign="top" align="left" colspan="2">
							&nbsp;Name:<br>
							&nbsp;&nbsp;<asp:textbox id="NewName" Runat="server"></asp:textbox><br>
							&nbsp;Description:<br>
							&nbsp;&nbsp;<asp:textbox id="NewDescription" Runat="server"></asp:textbox><br>
							<asp:checkbox id="NewDisplayDescription" runat="server" checked="false" Text="Display&nbsp;Description"></asp:checkbox>&nbsp;&nbsp;<br>
							<asp:checkbox id="NewDisplayImage" runat="server" checked="true" Text="Display&nbsp;Image"></asp:checkbox><br>
							<br>
						</TD>
						<td align="right">
							<uc1:uploadcontrol id="NewLittleImage" runat="server" FileText="" LabelDisplay="Swatch&nbsp;Image" FileType="1">
							</uc1:uploadcontrol><br>
							<uc1:uploadcontrol id="NewThumbnailImage" runat="server" FileText="" LabelDisplay="Cart&nbsp;Image" FileType="1">
							</uc1:uploadcontrol><br>
							<uc1:uploadcontrol id="NewBigImage" runat="server" FileText="" LabelDisplay="Large&nbsp;Image" FileType="1">
							</uc1:uploadcontrol><br>
							<uc1:uploadcontrol id="NewCloseUpImage" runat="server" FileText="" LabelDisplay="Close&nbsp;Up&nbsp;Image" FileType="1">
							</uc1:uploadcontrol>
						</td>
						<TD align="left">&nbsp;&nbsp;
							<asp:linkbutton id="cmdAdd" Runat="server">
								<asp:Image BorderWidth="0" ID="Image1" runat="server" ImageUrl="../images/icon_Add.gif" AlternateText="Add"></asp:Image>
							</asp:linkbutton>&nbsp;&nbsp;
						</TD>
					</TR>
					<tr><TD class="Content" width="1" colSpan="5">&nbsp;</TD></tr>
				</TABLE>
			</td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" colSpan="6" height="1"><IMG height="1" src="images/clear.gif"></TD>
		</TR>
		<tr>
			<TD class="Content" width="1" colSpan="6">&nbsp;</TD>
		</tr>
		<TR>
			<td class="content" align="right" width="75%" colSpan="6">
				<asp:LinkButton ID="cmdSave" Runat="server">
					<asp:Image BorderWidth="0" ID="imgSave" runat="server" ImageUrl="../images/save.jpg" AlternateText="Save"></asp:Image>
				</asp:LinkButton>
			</td>
		</TR>
	</TBODY>
</TABLE>
