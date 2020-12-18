<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.OrderDetailSectionListInfo" Codebehind="OrderDetailSectionListInfo.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="OrderDetailListInfo" Src="OrderDetailListInfo.ascx" %>

<table id="Table1" cellSpacing="0" cellPadding="0" border="0">
    <tr >
		<td align="center"> <!--Section Body -->			
		</td>
	</tr>
	<tr>
		<td>
		    <table id=tblSection1 runat=server border=0 cellpadding=0 cellspacing=0>
		        <tr>
		            <td>
		                <uc1:OrderDetailListInfo id="OrderDetailListInfo_Section1" runat="server"></uc1:OrderDetailListInfo>
                        <br>   
		            </td>
		        </tr>
		    </table> 
		</td>
	</tr>
	<tr>
		<td>   
		    <table id=tblSection2 runat=server border=0 cellpadding=0 cellspacing=0>
		        <tr>
		            <td>         
                        <uc1:OrderDetailListInfo id="OrderDetailListInfo_Section2" runat="server"></uc1:OrderDetailListInfo>
                        <br>   
                    </td>
		        </tr>
		    </table>     
		</td>
	</tr>
	<tr>
		<td> 
		    <table id=tblSection3 runat=server border=0 cellpadding=0 cellspacing=0>
		        <tr>
		            <td>              
                        <uc1:OrderDetailListInfo id="OrderDetailListInfo_Section3"  runat="server"></uc1:OrderDetailListInfo>
                        <br>     
                     </td>
		        </tr>
		    </table>       
		</td>	
	</tr>
	<tr>
	    <td>
	        <hr size=1px color=black width=100% />
	    </td>
	</tr>
	<tr>
		<td> 
		    <table id=Table2 runat=server border=0 cellpadding=0 cellspacing=0 width=645px>
		        <tr>
	                <td width=375px nowrap>
						<asp:label id="lblGTotal_Title" runat="server" CssClass="StandardSectionLabel">
						    Grand Total :&nbsp;
					    </asp:label>
					</td>
					<td align=right width=100px nowrap>
					    <asp:label id="lblTotalQuantity" runat="server" CssClass="StandardSectionLabel">
						    0
					    </asp:label>
					</td>
					<td align=right width=170px nowrap>
					    <asp:label id="lblTotalAmount" runat="server" CssClass="StandardSectionLabel">
						    0
					    </asp:label>
					</td>				
	            </tr>	            
		    </table>       
		</td>	
	</tr>			
</table>
