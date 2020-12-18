<%@ Register TagPrefix="uc1" TagName="AddressControlForm" Src="AddressControlForm.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.OrderStep_OrderInformation" Codebehind="OrderStep_OrderInformation.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="OrderHeaderDetailForm" Src="OrderHeaderDetailForm.ascx" %>
<table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
    <tr>
        <td align="left">
            <!--Section Title -->
        </td>
    </tr>
    <tr>
        <td align="left">
        </td>
    </tr>
    <tr>
        <td>
            <!--Section Body -->
            <uc1:OrderHeaderDetailForm ID="HeaderDetail" runat="server" SectionAccount_Visible="False"
                SectionAccount_Order="True"></uc1:OrderHeaderDetailForm>
        </td>
    </tr>
    <tr>
        <td align="center">
            <br>
            <table cellspacing="0" cellpadding="0" border="0" id="Table2" width="100%">
			    <tr runat="server" id="trQCAPOrderIntimation">
				    <td colspan="3" align="right">
				        <asp:Label ID="lblQCAPOrderIntimation" runat="server" CssClass="NoteLabel" Text="NOTE: This order is initiated from QCAP"></asp:Label>
				        <br />
				    </td>
			    </tr>
                <tr>
                    <td>
                        <asp:ImageButton ID="imgBtnBack" runat="server" CausesValidation="False" ImageUrl="~/images/btnBack.gif"
                            AlternateText="Click here to go back to the previous STEP"></asp:ImageButton>
                    </td>
                    <td width="100%">
                        &nbsp;
                    </td>
                    <td align="right">
                        <asp:ImageButton ID="imgBtnNext" runat="server" CausesValidation="False" ImageUrl="~/images/btnNext.gif"
                            AlternateText="Click here to go to the next STEP" ToolTip="Click here to go to the next STEP">
                        </asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="right">
            <br>
            <asp:ImageButton ID="imgBtnSkip" Visible="false" runat="server" CausesValidation="False"
                ImageUrl="~/images/btnOrderNoSupplies.gif" AlternateText="Click here to go to the next STEP"
                ToolTip="Click here to go to the next STEP"></asp:ImageButton>
        </td>
    </tr>
</table>
