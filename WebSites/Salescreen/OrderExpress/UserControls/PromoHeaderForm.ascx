<%@ Register TagPrefix="uc1" TagName="SubdivisionSelector" Src="SubdivisionSelector.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.PromoHeaderForm" Codebehind="PromoHeaderForm.ascx.cs" %>

<meta content="False" name="vs_snapToGrid">
<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td align="left"> <!--Section Title --></td>
	</tr>
	<tr>
		<td align="center">
			<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="600" border="0">
				<TR>
					<TD class="SectionPageTitleInfo" colSpan="2"><asp:label id="lblTitle" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 139px" vAlign="top"><SPAN class="StandardLabel">ID&nbsp;:</SPAN>
					</TD>
					<TD vAlign="top"><asp:label id="lblID" runat="server" Width="200px" CssClass="DescInfoLabel"></asp:label></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 139px" vAlign="top"><SPAN class="StandardLabel">Image&nbsp;&nbsp;:</SPAN>
					</TD>
					<TD vAlign="top">
						<asp:Image id="imgDetail" runat="server" Height="100" ImageUrl=""></asp:Image></TD>
				</TR>
				<TR id="trUpload" runat="server" style="DISPLAY:block">
					<TD style="WIDTH: 139px" vAlign="top"><SPAN class="StandardLabel">Upload&nbsp;Image&nbsp;:</SPAN>
					</TD>
					<TD vAlign="top"><INPUT id="ctrlUpload" style="WIDTH: 250px; HEIGHT: 22px" type="file" size="19" name="ctrlUpload"
							runat="server">
						<asp:Button id="btnUpload" runat="server" Text="Upload" Width="72" Height="22" CausesValidation="False" onclick="btnUpload_Click"></asp:Button>
					</TD>
				</TR>
			</TABLE>
		</td>
	</tr>
</table>
