<%@ Control Language="vb" AutoEventWireup="false" enableViewState="True" Codebehind="StandardSearch.ascx.vb" Inherits="StoreFront.StoreFront.StandardSearch" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<TABLE cellSpacing="0" cellPadding="0" border="0" width="100%">
	<TR height="10">
		<TD colSpan="3" class="Headings">&nbsp;Standard Search</TD>
	</TR>
	<TR>
		<TD colSpan="3"></TD>
	</TR>
	<TR>
		<TD width="10">&nbsp;</TD>
		<TD align="center" class="Content">
			<TABLE id="Table3" cellSpacing="0" cellPadding="0" border="0" width="100%">
				<TR>
					<TD noWrap align="center" class="Content">Keyword(s):&nbsp;
						<asp:TextBox id="SimpleKeyword" runat="server" Columns="20" CssClass="Inputs"></asp:TextBox>&nbsp;&nbsp;In:&nbsp;&nbsp;
						<asp:DropDownList id="SimpleCategory" runat="server" DataMember="Categories" DataValueField="ID" DataTextField="Name"
							CssClass="Inputs">
							<asp:ListItem Value="-1" Selected="True">All Categories</asp:ListItem>
						</asp:DropDownList>&nbsp;
						<asp:LinkButton ID="btnSearch" Runat="server">
							<asp:Image BorderWidth="0" ID="imgSearch" Runat="server" AlternateText="Search"></asp:Image>
						</asp:LinkButton>
					</TD>
				</TR>
				<TR>
					<TD align="center" class="Content">
						<asp:RadioButton id="SimpleKeywordGroup1" runat="server" Checked="True" Text="Any Words" GroupName="KeywordOption"></asp:RadioButton>
						<asp:RadioButton id="SimpleKeywordGroup2" runat="server" Text="Exact Phrase" GroupName="KeywordOption"></asp:RadioButton>
						<asp:RadioButton id="SimpleKeywordGroup3" runat="server" Text="All Words" GroupName="KeywordOption"></asp:RadioButton></TD>
				</TR>
				<TR>
					<TD align="center"></TD>
				</TR>
			</TABLE>
			<a href="search.aspx?Advanced=1">Advanced Search</a>
		</TD>
		<TD width="10">&nbsp;</TD>
	</TR>
	<TR height="10">
		<TD colSpan="3">&nbsp;</TD>
	</TR>
</TABLE>
