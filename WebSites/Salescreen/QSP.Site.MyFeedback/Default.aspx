<%@ Page Language="C#" MasterPageFile="~/Templates/Default.Master" AutoEventWireup="true" Codebehind="Default.aspx.cs" Inherits="QSP.Site.MyFeedback.Default" Title="My Feedback to QSP" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <table border="0" cellpadding="10" cellspacing="0" style="width: 100%">
        <tr>
            <td valign="top" style="width: 60%">
                <asp:Panel ID="TopFeedbackPanel" runat="server" Width="100%" CssClass="TopFeedback_Panel">
                    <table border="0" cellpadding="10" cellspacing="0" style="width: 100%">
                        <tr>
                            <td>
                                <asp:Label ID="Label7" runat="server" CssClass="TopFeedback_Title" Text="Latest Feedback"></asp:Label></td>
                        </tr>
                        <tr>
                            <td valign="top">
                            <div style="height:350px; overflow:auto">
                                <asp:UpdatePanel ID="TopFeedbackUpdatePanel" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                <asp:GridView ID="TopFeedbackGridView" runat="server" AutoGenerateColumns="False"
                    DataSourceID="ObjectDataSource1" Width="98%" ShowHeader="False">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <table border="0" cellpadding="5" cellspacing="0" style="width: 100%">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label9" runat="server" CssClass="TopFeedback_Message" Text='"'></asp:Label><asp:Label ID="Label2" runat="server" Text='<%# Eval("ShortMessage") %>' CssClass="TopFeedback_Message"></asp:Label><asp:Label
                                                    ID="Label10" runat="server" CssClass="TopFeedback_Message" Text='..."'></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                <tr>
                                                    <td align="left">
                                                        <asp:Label ID="Label11" runat="server" CssClass="TopFeedback_Date" Text='<%# Eval("CreateDate") %>'></asp:Label></td>
                                                    <td align="right">
                                                        <asp:Label ID="Label8" runat="server" CssClass="TopFeedback_Name" Text='<%# Eval("Name") %>'></asp:Label></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <AlternatingRowStyle BackColor="#FFFFC0" />
                </asp:GridView>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetTopFeedbackList"
                    TypeName="QSP.Site.MyFeedback.Feedback">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="20" Name="maxResults" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                                        <asp:Timer ID="TopFeedbackTimer" runat="server" Interval="600000" OnTick="TopFeedbackTimer_Tick">
                                        </asp:Timer>
                                <asp:LinkButton ID="RefreshTopFeedbackLinkButton" runat="server" OnClick="RefreshTopFeedbackLinkButton_Click" CausesValidation="False">Refresh</asp:LinkButton>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <cc1:DropShadowExtender ID="DropShadowExtender2" runat="server" TargetControlID="TopFeedbackPanel" Rounded="true" Opacity="0.3" Width="10" TrackPosition="true">
                </cc1:DropShadowExtender>
            </td>
            <td align="right" valign="top">
	<asp:Panel ID="FeedbackFormPanel" runat="server" CssClass="FeedbackForm_Panel" Width="450px">
        <asp:UpdatePanel ID="FeedbackFormUpdatePanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
<TABLE cellSpacing=0 cellPadding=10 width="100%" border=0><TBODY><TR><TD><TABLE cellSpacing=0 cellPadding=5 width="100%" border=0><TBODY><TR><TD align=left><asp:Label id="Label1" runat="server" CssClass="FeedbackForm_Title" Text="Write Feedback or Comment"></asp:Label></TD></TR><TR><TD align=left><asp:DropDownList id="SubjectDropDownList" runat="server" CssClass="FeedbackForm_TextBox1"><asp:ListItem Value=" ">---- Pick a subject ----</asp:ListItem>
<asp:ListItem Value=" "></asp:ListItem>
<asp:ListItem Value=" ">GENERAL FEEDBACK:</asp:ListItem>
<asp:ListItem Value="In what ways are IT a barrier to success?">&#160;&#160;&#160;&#160;In what ways are IT a barrier to success?</asp:ListItem>
<asp:ListItem Value="What's your biggest issue?">&#160;&#160;&#160;&#160;What's your biggest issue?</asp:ListItem>
<asp:ListItem Value="Where can IT solution help you achive quick wins?">&#160;&#160;&#160;&#160;Where can IT solution help you achive quick wins?</asp:ListItem>
<asp:ListItem Value="Other">&#160;&#160;&#160;&#160;Other...</asp:ListItem>
<asp:ListItem Value=" "></asp:ListItem>
<asp:ListItem Value=" ">APPLICATION SPECIFIC:</asp:ListItem>
<asp:ListItem Value="AccountTrack">&#160;&#160;&#160;&#160;AccountTrack</asp:ListItem>
<asp:ListItem Value="Order Express">&#160;&#160;&#160;&#160;Order Express</asp:ListItem>
<asp:ListItem Value="QCAP">&#160;&#160;&#160;&#160;QCAP</asp:ListItem>
<asp:ListItem Value="QSP.com/QSP.ca">&#160;&#160;&#160;&#160;QSP.com/QSP.ca</asp:ListItem>
<asp:ListItem Value="QStart">&#160;&#160;&#160;&#160;QStart</asp:ListItem>
<asp:ListItem Value="Other">&#160;&#160;&#160;&#160;Other...</asp:ListItem>
</asp:DropDownList> <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" CssClass="Error_Label" ErrorMessage="Invalid Subject." InitialValue=" " Display="Dynamic" ControlToValidate="SubjectDropDownList">*</asp:RequiredFieldValidator> <asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" CssClass="Error_Label" ErrorMessage="Invalid Message." InitialValue=" " Display="Dynamic" ControlToValidate="MessageTextBox">*</asp:RequiredFieldValidator></TD></TR><TR><TD vAlign=top align=left colSpan=1><STRONG><asp:TextBox id="MessageTextBox" runat="server" CssClass="FeedbackForm_TextBox1" Width="100%" MaxLength="2000" TextMode="MultiLine" Rows="5"></asp:TextBox></STRONG></TD></TR><TR><TD align=left><cc1:TextBoxWatermarkExtender id="TextBoxWatermarkExtender1" watermarkText="Write your feedback or comment here..." runat="server" TargetControlID="MessageTextBox" WatermarkCssClass="FeedbackForm_Watermark">
                </cc1:TextBoxWatermarkExtender> </TD></TR></TBODY></TABLE><asp:Panel id="FeedbackFormNamePanel" runat="server" Width="100%">
	<table border="0" cellpadding="5" cellspacing="0" style="width: 100%">
		<tr>
			<td style="width: 150px" align="left">
				<asp:Label ID="Label3" runat="server" CssClass="FeedbackForm_Label1" Text="Name:"></asp:Label></td>
			<td align="left">
				<asp:TextBox ID="NameTextBox" runat="server" CssClass="FeedbackForm_TextBox2" Width="100%"></asp:TextBox></td>
		</tr>
		<tr>
			<td align="left">
				<asp:Label ID="Label4" runat="server" CssClass="FeedbackForm_Label1" Text="City, State:"></asp:Label></td>
			<td align="left">
				<asp:TextBox ID="LocationTextBox" runat="server" CssClass="FeedbackForm_TextBox2" Width="100%"></asp:TextBox></td>
		</tr>
		<tr>
			<td align="left">
				<asp:Label ID="Label5" runat="server" CssClass="FeedbackForm_Label1" Text="Email:"></asp:Label></td>
			<td align="left">
				<asp:TextBox ID="EmailTextBox" runat="server" CssClass="FeedbackForm_TextBox2" Width="100%"></asp:TextBox></td>
		</tr>
		<tr>
			<td align="left">
				<asp:Label ID="Label6" runat="server" CssClass="FeedbackForm_Label1" Text="Phone:"></asp:Label></td>
			<td align="left">
				<asp:TextBox ID="PhoneTextBox" runat="server" CssClass="FeedbackForm_TextBox2" Width="100%"></asp:TextBox></td>
		</tr>
	</table>
        </asp:Panel> <TABLE style="WIDTH: 100%" cellSpacing=0 cellPadding=5 border=0><TBODY><TR><TD align=left><asp:CheckBox id="PublishCheckBox" runat="server" CssClass="FeedbackForm_CheckBox1" Text="Publish my feedback here for all to read." Checked="True"></asp:CheckBox></TD></TR><TR><TD align=right><asp:Button id="SubmitFeedbackButton" onclick="SubmitFeedbackButton_Click" runat="server" CssClass="FeedbackForm_Button1" Text="Submit Feedback"></asp:Button> <asp:Button id="CancelButton" onclick="CancelButton_Click" runat="server" CssClass="FeedbackForm_Button1" Text="Cancel" CausesValidation="False"></asp:Button> </TD></TR><TR><TD align=center><asp:ValidationSummary id="ValidationSummary1" runat="server" CssClass="Error_Label" DisplayMode="List"></asp:ValidationSummary> <asp:Label id="SummaryLabel" runat="server" CssClass="Error_Label"></asp:Label></TD></TR></TBODY></TABLE></TD></TR></TBODY></TABLE>
</ContentTemplate>
        </asp:UpdatePanel>
	</asp:Panel>
                <cc1:DropShadowExtender ID="DropShadowExtender1" runat="server" TargetControlID="FeedbackFormPanel" Rounded="true" Opacity="0.3" Width="10" TrackPosition="true">
                </cc1:DropShadowExtender>
            </td>
        </tr>
    </table>
</asp:Content>
