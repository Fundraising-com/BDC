<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.UserHeaderForm" Codebehind="UserHeaderForm.ascx.cs" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>

<table id="Table3" cellSpacing="0" cellPadding="0" border="0">
	<tr>
		<td><br>
		</td>
	</tr>
	<tr>
		<td>
			<table cellSpacing="0" cellPadding="2" border="0" runat="server" id="htmlTableUserHeader" width=650>
				<tr>
					<td noWrap class="StandardLabel">User ID :</td>
					<td noWrap>
						<asp:Label id="lbUserID" runat="server" CssClass="DescLabel" />
					</td>
					<td class="StandardLabel">Role&nbsp;:</td>
					<td>
						<asp:DropDownList id="ddlType" runat="server" />
						<asp:RequiredFieldValidator id="RequiredFieldValidator3" runat="server" ErrorMessage="Type is mandatory" ControlToValidate="ddlType">*</asp:RequiredFieldValidator>
					</td>
				</tr>
				<tr>
					<td class="StandardLabel">First Name :</td>
					<td>
						<asp:TextBox id="tbFirstN" runat="server" />
						<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ErrorMessage="First Name is mandatory"
							ControlToValidate="tbFirstN">*</asp:RequiredFieldValidator>
					</td>
					<td class="StandardLabel">Last Name :</td>
					<td>
						<asp:TextBox id="tbLastN" runat="server" />
						<asp:RequiredFieldValidator id="RequiredFieldValidator5" runat="server" ErrorMessage="Last Name is mandatory"
							ControlToValidate="tbLastN">*</asp:RequiredFieldValidator>
					</td>
				</tr>
				<tr>
					<td class="StandardLabel">User Name :</td>
					<td>
						<asp:TextBox id="tbUserName" runat="server" />
						<asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ErrorMessage="User Name is mandatory"
							ControlToValidate="tbUserName">*</asp:RequiredFieldValidator>
					</td>
					<td class="StandardLabel">Password :</td>
					<td>
						<asp:TextBox id="tbPassword" runat="server" />
						<asp:RequiredFieldValidator id="RequiredFieldValidator6" runat="server" ErrorMessage="Password is mandatory"
							ControlToValidate="tbPassword">*</asp:RequiredFieldValidator>
					</td>
					<%--
					<td class="StandardLabel">Password Confirmation:</td>
					<td>
						<asp:TextBox id="tbPassword2" runat="server" />
					</td>
					--%>
				</tr>
				<tr>
					<td class="StandardLabel">Title :</td>
					<td>
						<asp:TextBox id="tbTitle" runat="server" />
					</td>
					<td class="StandardLabel">Email :</td>
					<td>
						<cc1:Email id="tbEmail" runat="server" Required="false" />
					</td>
				</tr>
				<tr>
					<td class="StandardLabel">Phone # (Day) :</td>
					<td>
						<cc1:Phone id="tbDayPH" Runat="server" Required="false" />
					</td>
					<td class="StandardLabel">Phone # (Evening) :</td>
					<td>
						<cc1:Phone id="tbEveningPH" Runat="server" Required="false" />
					</td>
				</tr>
				<tr>
					<td class="StandardLabel">Best time to call :</td>
					<td>
						<asp:TextBox ID="tbBestPH" Runat="server" />
					</td>
					<td class="StandardLabel">Fax :</td>
					<td>
						<cc1:Phone id="tbFaxPH" Runat="server" Required="false" />
					</td>
				</tr>
				<tr id="trCreatedBy" runat="server" visible="false">
					<td class="StandardLabel">Created by :</td>
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
