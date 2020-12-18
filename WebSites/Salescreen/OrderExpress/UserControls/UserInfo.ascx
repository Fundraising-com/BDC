<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.UserInfo" Codebehind="UserInfo.ascx.cs" %>

<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td align="left"> <!--Section Title --></td>
	</tr>
	<tr>
		<td>
			<table id="Table3" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td>
						<span class="StandardLabel">First&nbsp;Name &nbsp;:</span>
					</td>
					<td>
						<asp:Label id="lblFName" runat="server" CssClass="DescLabel" Width="200px" />
					</td>
					<td>
						<span class="StandardLabel">Last&nbsp;Name&nbsp; :</span>
					</td>
					<td>
						<asp:Label id="lblLName" runat="server" CssClass="DescLabel" Width="100px" />
					</td>
				</tr>
				<tr>
					<td>
						<span class="StandardLabel">User&nbsp;ID&nbsp;# &nbsp;:</span>
					</td>
					<td>
						<asp:Label id="lbUserID" runat="server" CssClass="DescLabel" />
					</td>
				</tr>
				<tr>
					<td>
						<span class="StandardLabel">User&nbsp;Name&nbsp;:</span>
					</td>
					<td>
						<asp:Label id="lbUserName" runat="server" CssClass="DescLabel" Width="200px" />
					</td>
					<td>
						<span class="StandardLabel">Password&nbsp;:</span>
					</td>
					<td>
						<asp:Label id="lbPassword" runat="server" CssClass="DescLabel" Width="100px" />
					</td>
				</tr>
				<tr>
					<td>
						<span class="StandardLabel">Email&nbsp;:</span>&nbsp;
					</td>
					<td>
						<asp:HyperLink ID="hlEmail" Runat="server" CssClass="DescLabel" style='COLOR: blue; TEXT-DECORATION: none' />
						<asp:Label ID="lbEmail_none" Runat="server" CssClass="DescLabel" Text="none" />
					</td>
				</tr>
				<tr>
					<td class="StandardLabel">Phone&nbsp;#&nbsp;(Day)&nbsp;:</td>
					<td>
						<asp:Label ID="lbDayPH" Runat="server" CssClass="DescLabel" />
					</td>
					<td class="StandardLabel">Phone&nbsp;#&nbsp;(Evening)&nbsp;:</td>
					<td>
						<asp:Label ID="lbEveningPH" Runat="server" CssClass="DescLabel" />
					</td>
				</tr>
				<tr>
					<td class="StandardLabel">Best&nbsp;time&nbsp;to&nbsp;call&nbsp;:</td>
					<td>
						<asp:Label ID="lbBestPH" Runat="server" CssClass="DescLabel" />
					</td>
					<td>
						<span class="StandardLabel">Fax&nbsp;:</span>
					</td>
					<td>
						<asp:Label ID="lbFaxPH" Runat="server" CssClass="DescLabel" />
					</td>
				</tr>
				<tr>
					<td>
						<span class="StandardLabel">Title &nbsp;:</span>
					</td>
					<td>
						<asp:Label ID="lbTitle" Runat="server" CssClass="DescLabel" />
					</td>
					<td>
						<span class="StandardLabel">System &nbsp;Role&nbsp; :</span>
					</td>
					<td>
						<asp:Label ID="lbRole" Runat="server" CssClass="DescLabel" />
					</td>
				</tr>
				<tr id="trCreatedBy" runat="server" visible="false">
					<td class="StandardLabel">Created&nbsp;by&nbsp;:</td>
					<td>
						<asp:Label ID="lbCreateBy" Runat="server" CssClass="DescLabel" />
					</td>
					<td class="StandardLabel">Created date :</td>
					<td>
						<asp:Label ID="lbCreateDT" Runat="server" CssClass="DescLabel" />
					</td>
				</tr>
				<tr id="trUpdatedBy" runat="server" visible="false">
					<td class="StandardLabel">Updated by :</td>
					<td>
						<asp:Label ID="lbUpdateBy" Runat="server" CssClass="DescLabel" />
					</td>
					<td class="StandardLabel">Update date :</td>
					<td>
						<asp:Label ID="lbUpdateDT" Runat="server" CssClass="DescLabel" />
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table>
