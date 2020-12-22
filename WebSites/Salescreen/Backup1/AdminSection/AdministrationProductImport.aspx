<%@ Page language="c#" Codebehind="AdministrationProductImport.aspx.cs" AutoEventWireup="false" Inherits="AdminSection.AdministrationProductImport" %>
<%@ Register TagPrefix="buttonpanel" Namespace="efundraising.Web.UI.UIControls" Assembly="efundraising.Web.UI.UIControls" %>
<%@ Register TagPrefix="efundraising" Namespace="efundraising.Web.UI.MasterPages" Assembly="efundraising.Web.UI.MasterPages" %>
<%@ Register TagPrefix="contentpanel" Namespace="efundraising.Web.UI.UIControls" Assembly="efundraising.Web.UI.UIControls" %>

<body>
    <form id="form1" runat="server">
		<TABLE id="Table1" style="WIDTH: 480px; HEIGHT: 276px" cellSpacing="1" cellPadding="1"
			width="480" border="0">
			<TR>
				<TD vAlign="top" height="245">
					<TABLE class="NormalText" id="Table3" style="WIDTH: 480px; HEIGHT: 224px" cellSpacing="1"
						cellPadding="1" width="480" border="0">
						<TR>
							<TD class="ContentHeader" colSpan="2" height="23"><A style="COLOR: #333333" href="http://www.efundraising.com">Home</A>
								&gt; <A style="COLOR: #333333" href="AdministrationProductPackage.aspx">Administration</A>
								&gt; Create New Product
							</TD>
						<TR>
							<TD colSpan="2" height="22"><B>Import Product From CRM</B>
							</TD>
						<TR>
							<TD colSpan="2" height="28"></TD>
						</TR>
						<TR>
							<TD colSpan="2" height="2"><STRONG>1- Select the Product Class</STRONG></TD>
						</TR>
						<TR>
							<TD width="114" height="9">Product Class</TD>
							<TD align="left" height="9">
								<asp:dropdownlist id="ProductClassDropDownList" runat="server" Width="232px" AutoPostBack="True"></asp:dropdownlist></TD>
						</TR>
						<TR>
							<TD colSpan="2" height="25"></TD>
						</TR>
						<TR>
							<TD colSpan="2" height="2"><B>2- Select&nbsp;the Product Code or&nbsp;Product Name</B></TD>
						</TR>
						<TR>
							<TD width="114">Product Code</TD>
							<TD align="left">
								<asp:dropdownlist id="ProductCodeDropDownList" runat="server" Width="232px" AutoPostBack="True"></asp:dropdownlist></TD>
						</TR>
						<TR>
							<TD width="114">Product Name</TD>
							<TD align="left">
								<asp:dropdownlist id="ProductDropDownList" runat="server" Width="368px" AutoPostBack="True"></asp:dropdownlist></TD>
						</TR>
					</TABLE>
				</TD>
			</TR>
			<TR>
				<TD align="right" height="4">
					<asp:button id="CloseButton" runat="server" Visible="False" Text="Close"></asp:button>
					<asp:button id="OKButton" runat="server" Width="52px" Text="OK"></asp:button></TD>
			</TR>
		</TABLE>
	</form>
</body>
