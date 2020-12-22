<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DocumentInformationEdit.ascx.cs"
    Inherits="QSP.OrderExpress.Web.V2.UserControls.DocumentInformationEdit" %>
<table width="100%">
    <tr>
        <td width="25%">
            <span class="Text_Title">QSP Id:</span>
        </td>
        <td width="75%" colspan="3">
            <asp:Label ID="lblQspId" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td width="25%">
            <span class="Text_Title">Type:</span>
        </td>
        <td width="75%" colspan="3">
            <asp:Label ID="lblType" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td width="25%">
            <span class="Text_Title">Name:</span>
        </td>
        <td width="75%" colspan="3">
            <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td width="25%">
            <span class="Text_Title">Approved:</span>
        </td>
        <td width="75%" colspan="3">
            <asp:CheckBox ID="cbApproved" runat="server" />
        </td>
    </tr>
    <tr id="trExeptionNumber" runat="server">
        <td width="25%">
            <span class="Text_Title">Exeption number:</span>
        </td>
        <td width="75%" colspan="3">
            <asp:TextBox ID="tbExeptionNumber" runat="server" Width="500px"></asp:TextBox>
        </td>
    </tr>
    <tr id="trExeptionDates" runat="server">
        <td width="25%">
            <span class="Text_Title">Exeption start date:</span>
        </td>
        <td width="25%">
            <asp:TextBox ID="tbExeptionStartDate" runat="server" Width="100px"></asp:TextBox>
            <ajaxtoolkit:CalendarExtender ID="tbExeptionStartDate_CalendarExtender" runat="server"
                Enabled="True" TargetControlID="tbExeptionStartDate">
            </ajaxtoolkit:CalendarExtender>
        </td>
        <td width="25%">
            <span class="Text_Title">Exeption end date:</span>
        </td>
        <td width="25%">
            <asp:TextBox ID="tbExeptionEndDate" runat="server" Width="100px"></asp:TextBox>
            <ajaxtoolkit:CalendarExtender ID="tbExeptionEndDate_CalendarExtender" runat="server"
                Enabled="True" TargetControlID="tbExeptionEndDate">
            </ajaxtoolkit:CalendarExtender>
        </td>
    </tr>
</table>
