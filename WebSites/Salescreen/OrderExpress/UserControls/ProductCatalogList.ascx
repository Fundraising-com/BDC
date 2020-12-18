<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.ProductCatalogList" Codebehind="ProductCatalogList.ascx.cs" %>

<meta content="False" name="vs_snapToGrid">
<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
    <tr>
		<td align="left"> 
		    <table border= 0 cellpadding=0 cellspacing=0 width="100%" >
		        <tr>
		            <td align=center> 
		                <table border= 0 cellpadding=0 cellspacing=0 width=250px>
		                    <tr>
		                        <td Class="HeaderItemStyle" align="center" >
                                    <asp:Label ID="lblTitle" runat="server" Text="Label"> Selected Catalog</asp:Label>
		                        </td>
            		        </tr>
	                        <tr>
		                        <td>
                                    <asp:ListBox ID="lstSelected" Height=350px runat="server" Width="100%"></asp:ListBox>
                                </td>
		                    </tr>            		    
		                </table>
		            </td>	
		            <td align=center>
		                <table border= 0 cellpadding=0 cellspacing=0>
		                    <tr>
		                        <td>		                        
                                    <asp:Button ID="btnSelect" runat="server" Text="Select  -->" Width=100px CssClass="HeaderItemStyle" Font-Bold="True" OnClick="btnSelect_Click" />
        		                </td>  
        		             </tr>
	                        <tr>                                   
                               <td>
                                    <asp:Button ID="btnUnSelect" runat="server" Text="<--Unselect" Width=100px CssClass="HeaderItemStyle" Font-Bold="True" OnClick="btnUnSelect_Click"  />
                               </td>
                            </tr>
                        </table>
		            </td>            
		            <td align=center>
		                <table border= 0 cellpadding=0 cellspacing=0 width=250px>
		                    <tr>
		                        <td Class="HeaderItemStyle" align="center">
		                            
                                    <asp:Label ID="lblTitle1" runat="server" Text="Label">Available Catalog</asp:Label>
		                        
		                            
		                        </td>
            		        </tr>
	                        <tr>            		            
		                        <td>
                                    <asp:ListBox ID="lstAvailable" Height=350px runat="server" Width="100%"></asp:ListBox></td>
		                    </tr>
            		    
		                </table>
		            </td>
		        </tr>
		    
		    </table>
		</td>
	</tr>
	<tr>
		<td align="left"> <!--Section Title --></td>
	</tr>
</table>
