<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UpdateEmail.ascx.cs" Inherits="QSPFulfillment.CustomerService.action.UpdateEmail" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<br />
<table width="100%">
    
    <tbody>
        <tr>
            <td><asp:label id="Label1" cssclass="csPlainText" runat="server">Existing Email Address</asp:label></td>
            <td><asp:TextBox id="txtEmail" cssclass="csPlainText" runat="server" Enabled="false"></asp:TextBox></td>
        </tr>
        <br/><br/>
        <tr>
            <td><asp:label id="Label3" cssclass="csPlainText" runat="server">New Email Address</asp:label></td>
            <td><asp:TextBox runat="server" ID="txtNewEmail" cssclass="csPlainText"></asp:TextBox></td>
        </tr>
        <br/><br/>      
        <tr>
			<td colSpan="2" style="HEIGHT: 45px">				
                <asp:button id="btnValidateEmail" runat="server" Text="Validate Email" onclick="btnValidateEmail_Click"></asp:button>
			</td>
        </tr>                
		<tr>
			<td colspan="2" style="HEIGHT: 22px">		
				<asp:Label id="lblEmailError" runat="server" ForeColor="Red"></asp:Label></td>
		</tr>
    </tbody>    

</table>