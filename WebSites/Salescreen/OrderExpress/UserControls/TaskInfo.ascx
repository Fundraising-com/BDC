<%@ Reference Control="TemplateEmailDetailInfo.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.TaskInfo" Codebehind="TaskInfo.ascx.cs" %>

<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td class="SectionPageTitleInfo" colSpan="2"><asp:label id="Label3" runat="server">
			Task Information
			</asp:label></td>
	</tr>	
	<tr>
		<td><br>
			<table id="Table3" cellSpacing="0" cellPadding="3" width="600" border="0">
				<tr>
					<td class="StandardLabel" width="150">Task&nbsp;ID&nbsp;#&nbsp;:&nbsp;</td>
					<td width="90%"><asp:label id="lblTaskID" CssClass="DescInfoLabel" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td class="StandardLabel">Task&nbsp;Name&nbsp;:&nbsp;</td>
					<td><asp:label id="lblTaskName" CssClass="DescLabel" runat="server" Font-Bold="True"></asp:label></td>
				</tr>
				<tr>
					<td class="StandardLabel">Task&nbsp;Type&nbsp;:&nbsp;</td>
					<td><asp:label id="lblTaskTypeName" CssClass="DescInfoLabel" runat="server"></asp:label></td>
				</tr>
				<tr id="trNoteType" runat="server">
					<td class="StandardLabel">Note&nbsp;Type&nbsp;:&nbsp;</td>
					<td><asp:label id="lblNoteType" CssClass="DescInfoLabel" runat="server"></asp:label></td>
				</tr>
				<tr id="trTemplateEmail" runat="server">
					<td valign="top" class="StandardLabel" style="height: 46px">Template&nbsp;Email&nbsp;:&nbsp;</td>
					<td valign="top" style="height: 46px">
						<table border="0" cellpadding="0" cellspacing="0">
							<tr>
								<td valign=top>
									<asp:label id="lblTemplateName" runat="server" CssClass="DescInfoLabel"></asp:label>
								</td>
								<td>
									&nbsp;&nbsp;
								</td>
								<td>
									<asp:imagebutton id="imgBtnDetailTmplEmail" runat="server" CausesValidation="False" ImageUrl="~/images/BtnDetail.gif" Height="15px"></asp:imagebutton>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr id="trProcName" runat="server">
					<td valign="top" class="StandardLabel">Stored&nbsp;Proc&nbsp;Name&nbsp;:&nbsp;</td>
					<td valign="top"><asp:label id="lblStoredProcName" runat="server" CssClass="DescInfoLabel"></asp:label></td>
				</tr>
				<tr id="trParamName" runat="server">
					<td valign="top" class="StandardLabel">Stored&nbsp;Proc&nbsp;Parameter&nbsp;Name&nbsp;:&nbsp;</td>
					<td valign="top"><asp:label id="lblStoredProcParameterName" runat="server" CssClass="DescInfoLabel"></asp:label></td>
				</tr>				
				<tr>
					<td valign="top" class="StandardLabel">Description&nbsp;:&nbsp;</td>
					<td valign="top" height="70px"><asp:label id="lblDescription" CssClass="DescInfoLabel" runat="server"></asp:label></td>
				</tr>				
				<tr>
					<td><br>
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table>
