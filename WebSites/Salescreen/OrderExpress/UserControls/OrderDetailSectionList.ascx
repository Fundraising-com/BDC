<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.OrderDetailSectionList" Codebehind="OrderDetailSectionList.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="OrderDetailList" Src="OrderDetailList.ascx" %>

<table id="Table1" cellSpacing="0" cellPadding="0" border="0">
    <tr >
		<td align="center"> <!--Section Body -->			
		</td>
	</tr>
    <tr id="trBusinessMessage" runat="server" visible="false">
		<td align="left"> <!--Section Body -->
			<table id="Table533" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td><asp:label id="lblBusinessMessage" runat="server" CssClass="BizRuleLabel"></asp:label>
						
					</td>
					<td vAlign="top"></td>
				</tr>
			</table><br>
		</td>
	</tr>
	<tr id="trValSum" runat="server" visible="false">
		<td>
			<asp:validationsummary id="ValSum" runat="server" CssClass="LabelError" HeaderText="Correct the following error to proceed." ></asp:validationsummary>
			<asp:CustomValidator id="CustVal_MinQty" runat="server" CssClass="Label Error" ErrorMessage="The Minimum for an order is [MinTotalQuantity] cases.">*</asp:CustomValidator>
		</td>
	</tr>
	<tr>
		<td align="left">
			<table id="tblProfitRate" runat=server cellSpacing="0" cellPadding="0"  border="0">
				<tr>
					<td>
						<asp:label id="lblProfitPercentage" runat="server" CssClass="StandardLabel">
						    Account&nbsp;Profit&nbsp;:&nbsp;
					    </asp:label>
					</td>
					<td>
                        <asp:RadioButtonList ID="radBtnLstProfitRate" runat="server" AutoPostBack="True" CssClass=DescInfoLabel RepeatDirection=Horizontal OnSelectedIndexChanged="radBtnLstProfitRate_SelectedIndexChanged">
                            <asp:ListItem Selected=True Value=50>50.00%</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
				</tr>
			</table> 
	    </td>
	</tr>
	<tr>
		<td>
		    <table id=tblSection1 runat=server border=0 cellpadding=0 cellspacing=0>
		        <tr>
		            <td>
		                <uc1:OrderDetailList id="OrderDetailList_Section1" runat="server"></uc1:OrderDetailList>
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
                        <uc1:OrderDetailList id="OrderDetailList_Section2" runat="server"></uc1:OrderDetailList>
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
                        <uc1:OrderDetailList id="OrderDetailList_Section3"  runat="server"></uc1:OrderDetailList>
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
		    <table id=Table2 runat=server border=0 cellpadding=0 cellspacing=0 width=615px>
		        <tr>
	                <td width=350px nowrap>
						<asp:label id="Label1" runat="server" CssClass="StandardSectionLabel">
						    Grand Total :&nbsp;
					    </asp:label>
					</td>
					<td align=right width=115px nowrap>
					    <asp:label id="lblTotalQuantity" runat="server" CssClass="StandardSectionLabel">
						    0
					    </asp:label>
					    <asp:HiddenField ID="hdnMinTotalQuantity" runat="server" Value="0" />
					</td>
					<td align=right width=150px nowrap>
					    <asp:label id="lblTotalAmount" runat="server" CssClass="StandardSectionLabel">
						    0
					    </asp:label>
					</td>				
	            </tr>	            
		    </table>       
		</td>	
	</tr>			
</table>
