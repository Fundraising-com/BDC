<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.SearchModule" Codebehind="SearchModule.ascx.cs" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>

<TABLE id="Table1" cellSpacing="0" cellPadding="0" border="0">
	<TR>
		<TD>
			<TABLE cellSpacing="0" cellPadding="0">
				<TR>
					<TD valign="top">
						<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD colSpan="6">
									<asp:label id="lblHeader" Visible="False" runat="server" CssClass="StandardLabel"></asp:label>
								</TD>
							</TR>
							<TR>
								<TD><asp:label id="lblText" CssClass="StandardLabel" Runat="server">Search By:&nbsp; </asp:label></TD>
								<TD><asp:dropdownlist id="ddlSearchBy" runat="server" Width="170px"></asp:dropdownlist></TD>
								<TD><asp:label id="Label1" runat="server" CssClass="ModuleSearchText">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Containing:&nbsp;</asp:label></TD>
								<TD><asp:textbox id="txtCriteria" CssClass="StandardTextBox" Runat="server" Width="170px"></asp:textbox>&nbsp;&nbsp;</TD>
								<TD>
									<TABLE cellSpacing="0" cellPadding="0">
										<TR>
											<TD vAlign="middle" align="center">
												<asp:imagebutton id="imgBtnRefresh" CausesValidation="False" runat="server" ImageUrl="~/images/btnRefresh.gif"></asp:imagebutton>
											</TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
							<TR>
								<TD colSpan="6">
									<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0" >
										<TR>
											<TD align="center" width="45%">
												<HR WIDTH="100%" SIZE="1" color="#006699">
											</TD>
											<TD align="center" width="10%">
												<asp:label id="Label2" runat="server" CssClass="StandardLabel" Font-Bold=true Font-Size="12px">&nbsp;OR&nbsp;</asp:label>
											</TD>
											<TD width="45%">
												<HR WIDTH="100%" SIZE="1" color="#006699">
											</TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
							<TR>
								<TD><asp:label id="lblSearchBy" CssClass="StandardLabel" Runat="server">Search By: </asp:label></TD>
								<TD colSpan="5"><FONT size="2"><STRONG><asp:label id="lblSearchByAlpha" runat="server" CssClass="ModuleSearchText" Font-Bold="True"></asp:label></STRONG></FONT></TD>
							</TR>
							<TR>
								<TD noWrap><asp:label id="lblBeginningWith" runat="server" CssClass="ModuleSearchText">Beginning with:&nbsp;</asp:label></TD>
								<TD colSpan="5"><cc1:alphasearch id="ctrlAlphaSearch" runat="server" CssClass="AlphaSearch" Width=100%></cc1:alphasearch></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>
<script>
	ToSetFocus = document.getElementById('<%=txtCriteria.ClientID%>');
</script>
