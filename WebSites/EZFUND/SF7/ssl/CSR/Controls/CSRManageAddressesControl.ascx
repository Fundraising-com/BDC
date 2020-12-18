<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="CSRManageAddressesControl.ascx.vb" Inherits="StoreFront.StoreFront.CSRManageAddressesControl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<%@ Register TagPrefix="uc1" TagName="CSRAddressBook" Src="../controls/CSRAddressBook.ascx" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" border="0" width="100%">
	<TR>
		<TD vAlign="top" width="50%">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" border="0" width="100%">
				<TR>
					<TD height="10">&nbsp;</TD>
					<TD class="Content" noWrap colSpan="2"><b>
							<asp:Label id="NewEditLabel" runat="server">Add An Address</asp:Label></b></TD>
					<TD height="10">&nbsp;</TD>
				</TR>
				<tr>
					<TD height="10">&nbsp;</TD>
					<td class="Content" noWrap align="right">Save As:</td>
					<td><asp:textbox id="NickName" runat="server" MaxLength="50"></asp:textbox><asp:label id="Label5" CssClass="ErrorMessages" Runat="server">*</asp:label></td>
					<TD height="10">&nbsp;</TD>
				</tr>
				<TR>
					<TD height="10">&nbsp;</TD>
					<TD class="Content" noWrap align="right">First Name:</TD>
					<TD><asp:textbox id="FirstName" runat="server" MaxLength="100"></asp:textbox><asp:label id="Label2" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
					<TD height="10">&nbsp;</TD>
				</TR>
				<TR>
					<TD height="10">&nbsp;</TD>
					<TD class="Content" noWrap align="right">Middle Initial:</TD>
					<TD><asp:textbox id="MI" runat="server" Width="33px" MaxLength="2"></asp:textbox></TD>
					<TD height="10">&nbsp;</TD>
				</TR>
				<TR>
					<TD height="10">&nbsp;</TD>
					<TD class="Content" noWrap align="right">Last Name:</TD>
					<TD><asp:textbox id="LastName" runat="server" MaxLength="100"></asp:textbox><asp:label id="Label3" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
					<TD height="10">&nbsp;</TD>
				</TR>
				<TR>
					<TD height="10"></TD>
					<TD class="Content" noWrap align="right">Company:</TD>
					<TD>
						<asp:TextBox id="Company" runat="server" MaxLength="75"></asp:TextBox></TD>
					<TD height="10"></TD>
				</TR>
				<TR>
					<TD height="10">&nbsp;</TD>
					<TD class="Content" noWrap align="right">Address 1:</TD>
					<TD><asp:textbox id="Address1" runat="server" MaxLength="255"></asp:textbox><asp:label id="Label6" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
					<TD height="10">&nbsp;</TD>
				</TR>
				<TR>
					<TD height="10">&nbsp;</TD>
					<TD class="Content" noWrap align="right">Address 2:</TD>
					<TD><asp:textbox id="Address2" runat="server" MaxLength="255"></asp:textbox></TD>
					<TD height="10">&nbsp;</TD>
				</TR>
				<TR>
					<TD height="10">&nbsp;</TD>
					<TD class="Content" noWrap align="right">City:</TD>
					<TD><asp:textbox id="City" runat="server" MaxLength="50"></asp:textbox><asp:label id="Label7" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
					<TD height="10">&nbsp;</TD>
				</TR>
				<TR>
					<TD height="16">&nbsp;</TD>
					<TD class="Content" noWrap align="right" height="16">State/Province:</TD>
					<TD height="16">
						<cc1:SelectValControl id="State" runat="server" Width="206px"></cc1:SelectValControl><asp:label id="Label8" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
					<TD height="16">&nbsp;</TD>
				</TR>
				<TR>
					<TD height="10">&nbsp;</TD>
					<TD class="Content" noWrap align="right">Postal Code:</TD>
					<TD><asp:textbox id="Zip" runat="server" MaxLength="50"></asp:textbox></TD>
					<TD height="10">&nbsp;</TD>
				</TR>
				<TR>
					<TD height="10">&nbsp;</TD>
					<TD class="Content" noWrap align="right">Country:</TD>
					<TD>
						<cc1:SelectValControl id="Country" runat="server" Width="203px" DisplaySelect="Country"></cc1:SelectValControl></TD>
					<TD height="10">&nbsp;</TD>
				</TR>
				<TR>
					<TD height="10">&nbsp;</TD>
					<TD class="Content" noWrap align="right">Phone:</TD>
					<TD><asp:textbox id="Phone" runat="server"></asp:textbox><asp:label id="Label9" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
					<TD height="10">&nbsp;</TD>
				</TR>
				<TR>
					<TD height="10">&nbsp;</TD>
					<TD class="Content" noWrap align="right">Fax:</TD>
					<TD><asp:textbox id="Fax" runat="server"></asp:textbox></TD>
					<TD height="10">&nbsp;</TD>
				</TR>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<TR>
					<TD height="10">&nbsp;</TD>
					<TD class="Content" noWrap align="right" colSpan="2">
						<asp:TextBox id="AddressID" runat="server" Visible="False"></asp:TextBox>
						<asp:LinkButton ID="btnAdd" Runat="server">
							<asp:Image BorderWidth="0" ID="imgAdd" Runat="server" AlternateText="Add"  ImageUrl="../images/Add.jpg"></asp:Image>
						</asp:LinkButton>
						&nbsp;
						<asp:LinkButton ID="btnClear" Runat="server">
							<asp:Image BorderWidth="0" ID="imgClear" Runat="server" AlternateText="Clear"  ImageUrl="../images/clear.jpg"></asp:Image>
						</asp:LinkButton>
					</TD>
					<TD height="10">&nbsp;</TD>
				</TR>
				<TR>
					<TD height="10">&nbsp;</TD>
					<TD class="Content" noWrap align="right" colSpan="2">
						<asp:LinkButton ID="btnSave" Runat="server">
							<asp:Image BorderWidth="0" ID="imgSave" Runat="server" AlternateText="Save" ImageUrl="../images/save.jpg"></asp:Image>
						</asp:LinkButton>
						&nbsp;
						<asp:LinkButton ID="btnCancel" Runat="server">
							<asp:Image BorderWidth="0" ID="imgCancel" Runat="server" AlternateText="Cancel" ImageUrl="../images/cancel.jpg"></asp:Image>
						</asp:LinkButton>
					</TD>
					<TD height="10">&nbsp;</TD>
				</TR>
			</TABLE>
		</TD>
		<TD width="1" bgColor="#000000"><IMG height="1" src="images/black.gif" width="1"></TD>
		<TD vAlign="top" width="50%">
			<TABLE id="Table3" cellSpacing="0" cellPadding="0" border="0" width="100%">
				<TR>
					<TD height="10">&nbsp;</TD>
					<TD class="Content" noWrap><b>Saved Addresses</b></TD>
					<TD height="10">&nbsp;</TD>
				</TR>
				<tr>
					<TD height="10">&nbsp;</TD>
					<td>
								<uc1:CSRAddressBook id="CSRAddressBook" runat="server"></uc1:CSRAddressBook>
					</td>
					<TD height="10">&nbsp;</TD>
				</tr>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD class="Content" vAlign="top" align="right" width="50%">
			<asp:LinkButton ID="btnContinue" Runat="server">
				<asp:Image BorderWidth="0" ID="imgContinue" Runat="server" ImageUrl="../images/continue.jpg" AlternateText="Continue"></asp:Image>
			</asp:LinkButton>
		</TD>
		<TD width="1" bgColor="#000000"></TD>
		<TD vAlign="top" width="50%"></TD>
	</TR>
</TABLE>
