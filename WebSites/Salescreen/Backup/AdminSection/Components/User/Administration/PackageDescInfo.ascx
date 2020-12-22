<%@ Control Language="c#" AutoEventWireup="false" Codebehind="PackageDescInfo.ascx.cs" Inherits="AdminSection.Components.User.Administration.PackageDescInfo" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc1" Namespace="efundraising.Web.UI.InputControls" Assembly="efundraising.Web.UI.InputControls" %>
<TABLE id="Table1" style="WIDTH: 376px; HEIGHT: 256px" cellSpacing="0" cellPadding="5"
	width="376" border="0">
	<TR>
		<TD style="HEIGHT: 291px" vAlign="top" colSpan="1">
			<TABLE class="NormalText" id="Table4" style="WIDTH: 584px; HEIGHT: 304px" cellSpacing="0"
				cellPadding="0" width="584" border="0">
				<TR>
					<TD style="FONT-WEIGHT: normal; WIDTH: 143px; HEIGHT: 19px" vAlign="top">Name</TD>
					<TD style="WIDTH: 425px; HEIGHT: 19px"><cc1:stringtextbox id="NameTextBox" Width="200px" runat="server" Columns="50"></cc1:stringtextbox></TD>
				</TR>
				<TR>
					<TD style="FONT-WEIGHT: normal; WIDTH: 143px; HEIGHT: 16px" vAlign="top">Page Name</TD>
					<TD style="WIDTH: 425px; HEIGHT: 16px"><cc1:stringtextbox id="PageNameTextBox" Width="200px" runat="server" Columns="50" Nullable="True"></cc1:stringtextbox></TD>
				</TR>
				<TR>
					<TD style="FONT-WEIGHT: normal; WIDTH: 143px; HEIGHT: 2px" vAlign="top">Page Title</TD>
					<TD style="WIDTH: 425px; HEIGHT: 2px"><cc1:stringtextbox id="PageTitleTextBox" Width="200px" runat="server" Columns="50" Nullable="True"></cc1:stringtextbox></TD>
				</TR>
				<TR>
					<TD style="FONT-WEIGHT: normal; WIDTH: 143px; HEIGHT: 16px" vAlign="top">Template</TD>
					<TD style="WIDTH: 425px; HEIGHT: 16px"><asp:dropdownlist id="TemplateDropDownList" Width="328px" runat="server"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 143px; HEIGHT: 44px" vAlign="top">Short Description</TD>
					<TD style="WIDTH: 425px; HEIGHT: 44px" align="left"><asp:textbox id="ShortDescriptionTextBox" runat="server" TextMode="MultiLine" Columns="50" Rows="10"></asp:textbox></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 143px; HEIGHT: 42px" vAlign="top">Long Description</TD>
					<TD style="WIDTH: 425px; HEIGHT: 42px" vAlign="top" align="left"><asp:textbox id="LongDescriptionTextBox" runat="server" TextMode="MultiLine" Columns="50" Rows="10"></asp:textbox></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 143px; HEIGHT: 26px" vAlign="top">Extra Description</TD>
					<TD style="WIDTH: 425px; HEIGHT: 26px" align="left"><asp:textbox id="ExtraDescriptionTextBox" runat="server" TextMode="MultiLine" Columns="50" Rows="10"></asp:textbox></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 143px; HEIGHT: 19px" vAlign="top">Image Name</TD>
					<TD style="WIDTH: 425px; HEIGHT: 19px" align="left"><cc1:stringtextbox id="ImageNameTextBox" Width="200px" runat="server" Nullable="True"></cc1:stringtextbox></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 143px; HEIGHT: 19px" vAlign="top">Image Alt Text</TD>
					<TD style="WIDTH: 425px; HEIGHT: 19px" align="left"><cc1:stringtextbox id="ImageAltTextTextBox" Width="200px" runat="server" Nullable="True"></cc1:stringtextbox></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 143px; HEIGHT: 19px" vAlign="top">Upload Small Image</TD>
					<TD style="WIDTH: 425px; HEIGHT: 19px" align="left"><INPUT id="FileUpload" style="WIDTH: 272px; HEIGHT: 22px" type="file" size="26" name="filename"
							runat="server">
						<asp:button id="UploadButton" runat="server" Height="22px" CausesValidation="False" Text="Upload"></asp:button><asp:button id="DisplayButton" runat="server" Height="22px" CausesValidation="False" Text="Display"></asp:button></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 143px" vAlign="top">Upload Large Image</TD>
					<TD style="WIDTH: 425px" align="left"><INPUT id="FileLargeUpload" style="WIDTH: 272px; HEIGHT: 22px" type="file" size="26" name="filename"
							runat="server">
						<asp:button id="UploadLargeButton" runat="server" Height="22px" CausesValidation="False" Text="Upload"></asp:button><asp:button id="DisplayLargeButton" runat="server" Height="22px" CausesValidation="False" Text="Display"></asp:button></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 143px" vAlign="top"></TD>
					<TD style="WIDTH: 425px" align="left"><asp:label id="ErrorLabel" runat="server" ForeColor="Red" Visible="False">Label</asp:label></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 143px" vAlign="top">Display Order</TD>
					<TD style="WIDTH: 425px" align="left"><cc1:integertextbox id="DisplayOrderTextBox" runat="server" Nullable="True"></cc1:integertextbox></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 143px" vAlign="top">Enabled</TD>
					<TD style="WIDTH: 425px" align="left"><asp:dropdownlist id="EnabledDropdownlist" Runat="server"></asp:dropdownlist></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>
