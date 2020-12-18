<%@ Control Language="C#" AutoEventWireup="true" Inherits="QSP.OrderExpress.Web.UserControls.VendorInfo" Codebehind="VendorInfo.ascx.cs" %>
<link href="Styles.css" rel="stylesheet" type="text/css" />
<table border="0" cellpadding="0" cellspacing="0">
    <tr class="">
        <td>
            <asp:Label ID="lblLabelVendorName" runat="server" CssClass="StandardLabel">Vendor&nbsp;Name&nbsp;:&nbsp;</asp:Label></td>
        <td>
            <asp:Label ID="lblVendorName" runat="server" CssClass="DescLabel"></asp:Label>
            <asp:textbox ID="txtVendorName" runat="server" CssClass="AddressInfoDescLabel"></asp:textbox>
        </td>
    </tr>
    <tr class="">
        <td>
            <asp:Label ID="lblLabelFirstName" runat="server" CssClass="StandardLabel">First&nbsp;Name&nbsp;:&nbsp;</asp:Label></td>
        <td>
            <asp:Label ID="lblFirstName" runat="server" CssClass="DescLabel"></asp:Label>
            <asp:textbox ID="txtFirstName" runat="server" CssClass="AddressInfoDescLabel"></asp:textbox>
        </td>
    </tr>
    <tr class="">
        <td>
            <asp:Label ID="lblLabelLastName" runat="server" CssClass="StandardLabel">Last&nbsp;Name&nbsp;:&nbsp;</asp:Label></td>
        <td>
            <asp:Label ID="lblLastName" runat="server" CssClass="DescLabel"></asp:Label>
            <asp:textbox ID="txtLastName" runat="server" CssClass="AddressInfoDescLabel"></asp:textbox>
        </td>
    </tr>
    <tr class="">
        <td valign="top">
            <asp:Label ID="lblLabelAddressLine1" runat="server" CssClass="StandardLabel">Address&nbsp;Line&nbsp;1&nbsp;:&nbsp;</asp:Label></td>
        <td>
            <asp:Label ID="lblAddressLine1" runat="server" CssClass="DescLabel"></asp:Label>
            <asp:textbox ID="txtAddressLine1" runat="server" CssClass="AddressInfoDescLabel"></asp:textbox>
        </td>
    </tr>
    <tr class="">
        <td valign="top">
            <asp:Label ID="lblLabelAddressLine2" runat="server" CssClass="StandardLabel">Address&nbsp;Line&nbsp;2&nbsp;:&nbsp;</asp:Label></td>
        <td>
            <asp:Label ID="lblAddressLine2" runat="server" CssClass="DescLabel"></asp:Label>
            <asp:textbox ID="txtAddressLine2" runat="server" CssClass="AddressInfoDescLabel"></asp:textbox>
        </td>
    </tr>
    <tr class="">
        <td>
            <asp:Label ID="lblLabelCity" runat="server" CssClass="StandardLabel">City&nbsp;:&nbsp;</asp:Label>
        </td>
        <td valign="top">
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Label ID="lblCity" runat="server" CssClass="DescLabel" Width="100px"></asp:Label>
                        <asp:textbox ID="txtCity" runat="server" CssClass="AddressInfoDescLabel"></asp:textbox>
                    </td>
                    <td>
                        <asp:Label ID="lblLabelCounty" runat="server" CssClass="StandardLabel">&nbsp;County&nbsp;:&nbsp;</asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblCounty" runat="server" CssClass="DescLabel"></asp:Label>
                        <asp:textbox ID="txtCounty" runat="server" CssClass="AddressInfoDescLabel"></asp:textbox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr class="">
        <td>
            <asp:Label ID="lblLabelState" runat="server" CssClass="StandardLabel">State&nbsp;:&nbsp;</asp:Label>
        </td>
        <td valign="top">
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Label ID="lblState" runat="server" CssClass="DescLabel" Width="100px"></asp:Label>
                        <asp:textbox ID="txtState" runat="server" CssClass="AddressInfoDescLabel"></asp:textbox>
                    </td>
                    <td>
                        <asp:Label ID="lblLabelZip" runat="server" CssClass="StandardLabel">&nbsp;Zip&nbsp;Code&nbsp;:&nbsp;</asp:Label></td>
                        <asp:textbox ID="txtLabelZip" runat="server" CssClass="AddressInfoDescLabel"></asp:textbox><td>
                        <asp:Label ID="lblZip" runat="server" CssClass="DescLabel"></asp:Label>
                        <asp:textbox ID="txtZip" runat="server" CssClass="AddressInfoDescLabel"></asp:textbox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr class="">
        <td>
            <asp:Label ID="lblLabelBillingPhoneNumber" runat="server" CssClass="StandardLabel">Phone&nbsp;Number&nbsp;:&nbsp;</asp:Label></td>
        <td>
            <table id="Table6" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Label ID="lblPhoneNumber" runat="server" CssClass="DescLabel" Width="100px"></asp:Label>
                        <asp:textbox ID="txtPhoneNumber" runat="server" CssClass="AddressInfoDescLabel"></asp:textbox></td>
                    <td>
                        <asp:Label ID="lblLabelFaxNumber" runat="server" CssClass="StandardLabel">&nbsp;Fax&nbsp;Number&nbsp;:&nbsp;</asp:Label>
                        </td>
                    <td>
                        <asp:Label ID="lblFaxNumber" runat="server" CssClass="DescLabel" Width="90px"></asp:Label>
                        <asp:textbox ID="txtFaxNumber" runat="server" CssClass="AddressInfoDescLabel"></asp:textbox>
                    </td>
                                                      
                </tr>
            </table>
        </td>
    </tr>
    <tr class="">
        <td>
            <asp:Label ID="lblLabelEmailAddress" runat="server" CssClass="StandardLabel">Email&nbsp;Address&nbsp;:&nbsp;</asp:Label></td>
        <td>
            <asp:Label ID="lblEmailAddress" runat="server" CssClass="DescLabel"></asp:Label>
            <asp:textbox ID="txtEmailAddress" runat="server" CssClass="AddressInfoDescLabel"></asp:textbox>
            </td>
            
    </tr>
    <tr class="">
        <td>
            <asp:Label ID="lblLabelOracleCode" runat="server" CssClass="StandardLabel">Oracle&nbsp;Code&nbsp;:&nbsp;</asp:Label></td>
        <td>
            <asp:Label ID="lblOracleCode" runat="server" CssClass="DescLabel"></asp:Label>
            <asp:textbox ID="txtOracleCode" runat="server" CssClass="AddressInfoDescLabel"></asp:textbox>
        </td>
    </tr>                
</table>
