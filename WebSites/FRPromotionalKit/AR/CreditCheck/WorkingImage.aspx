<%@ Import Namespace="System.Threading" %>
<%@ Page Language="c#" AutoEventWireup="false" CodeBehind="WorkingImage.aspx.cs" Inherits="efundraising.EFundraisingCRMWeb.AR.CreditCheck.WorkingImage" %>
<HTML>
	<HEAD>
		<title>"Working" animated Image Demo</title>
		<script language='javascript'> 
function showDiv() 
{
document.getElementById('myHiddenDiv').style.display =""; 
setTimeout('document.images["myAnimatedImage"].src = "work.gif"', 200); 
} 
		</script>
		<script language="C#" runat="server"> 
        protected void Button1_Click(object sender, EventArgs e)
        {
            Thread.Sleep(5000); // this is a surrogate for your long-running method call
            lblMessage.Text = "Done!";
        }
		</script>
	</HEAD>
	<body>
		<form id="form1" runat="server">
			<asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
			<span id='myHiddenDiv' style='DISPLAY:none'>
				<img src='work.gif' id='myAnimatedImage' align='absMiddle'>
			</span>
			<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" OnClientClick="showDiv();" Text="Search" />
			<asp:Label ID="lblMessage" runat="server"></asp:Label>
		</form>
	</body>
</HTML>
