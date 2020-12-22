<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="RelatedProductControl.ascx.vb" Inherits="StoreFront.StoreFront.RelatedProductControl" %>
<table width="100%" border="0" cellSpacing="0" cellPadding="0">
	<TBODY>
		<tr>
			<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
			<TD class="ContentTableHeader"></TD>
			<td class="ContentTableHeader">
				<div class="ContentTableHeader" style="LEFT:auto; FLOAT:left; POSITION:relative; TOP:auto">
					&nbsp;Recommended Items:
				</div>
				<div class="Content" style="LEFT:auto; FLOAT:right; COLOR:white; POSITION:relative; TOP:auto">
					Page:&nbsp;<% =CurrentPage %>&nbsp;of&nbsp;<% =TotalPageCount %>&nbsp;
				</div>
			</td>
			<TD class="ContentTableHeader"></TD>
			<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
		</tr>
		<tr>
			<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
			<TD class="Content"></TD>
			<td class="Content">
				<asp:DataList id="dlRelatedProd" runat="server" RepeatColumns="5" RepeatDirection="horizontal"
					Width="100%" Height="100%">
					<HeaderTemplate>
						<table id="tblDataList" cellspacing="0" cellpadding="0" border="0">
							<tr>
					</HeaderTemplate>
					<ItemTemplate>
						<TD class="Content" id="rptCell" runat="server" width='<%# WidthPercent.ToString & "%" %>' valign="bottom">
							<div class="Content" style='<%# IIf(DisplayProductImage = True, "position:relative; top:auto; left:auto; padding-left:5px; padding-right:5px;", "display:none") %>'>
								<asp:HyperLink Runat=server ID="lnkImage" NavigateUrl='<%# LinkImage(DataBinder.Eval(Container.DataItem,"DetailLink")) %>'>
									<img onload="Resize(this, '<%# DataBinder.Eval(Container.DataItem,"SmallImage") %>', '<%# FirstPageMaxItemIndex %>');" title='<%# DisplayShortDescription(DataBinder.Eval(Container.DataItem,"ShortDescription")) %>' src='<%# IIf(DataBinder.Eval(Container.DataItem,"SmallImage").ToLower.IndexOf("images/clear.gif") = -1, "images/Busy.gif", "images/NoImage.gif") %>'>
								</asp:HyperLink>
							</div>
							<div class="Content" style='<%# IIf(DisplayProductName = True, "position:relative; top:auto; left:auto; padding-left:5px; padding-right:5px;", "display:none") %>'>
								<asp:HyperLink id="lnkProductName" Runat="server" NavigateUrl='<%# LinkName(DataBinder.Eval(Container.DataItem,"DetailLink")) %>' style='<%# IIf(LinkName(DataBinder.Eval(Container.DataItem,"DetailLink")) = "", "text-decoration:none", "") %>'>
									<span class='<%# IIf(LinkName(DataBinder.Eval(Container.DataItem,"DetailLink")) = "", "Content", "") %>'><%# NameDisplay(DataBinder.Eval(Container.DataItem,"Name"))%></span>
								</asp:HyperLink>
							</div>
							<div class="Content" style='<%# IIf(DisplayProductCode = True, "position:relative; top:auto; left:auto; padding-left:5px; float:left;", "display:none;") %>'>
								<asp:HyperLink id="lnkCode" runat="server" NavigateUrl='<%# LinkCode(DataBinder.Eval(Container.DataItem,"DetailLink")) %>' style='<%# IIf(LinkCode(DataBinder.Eval(Container.DataItem,"DetailLink")) = "", "text-decoration:none", "") %>'>
									<span class='<%# IIf(LinkName(DataBinder.Eval(Container.DataItem,"DetailLink")) = "", "Content", "") %>'><%#DataBinder.Eval(Container.DataItem,"ProductCode")%></span>
								</asp:HyperLink>
							</div>
							<div class="Content" style='<%# IIf(DisplayPrice = True, "position:relative; top:auto; left:auto; padding-right:5px; float:right;", "display:none;") %>'>
								<%# PriceDisplay2(DataBinder.Eval(Container.DataItem, "Price")) %>
							</div>
						</TD>
					</ItemTemplate>
					<SeparatorTemplate>
						<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
					</SeparatorTemplate>
					<FooterTemplate>
		</tr>
</table>
</FooterTemplate> </asp:DataList></TD>
<TD class="Content"></TD>
<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
</TR>
<tr>
	    <td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
		<TD class="ContentTableHeader"></TD>
	    <td class="Content">
	        <input type=hidden id=hdnStart runat=server value=0 NAME="hdnStart"/>
            <input type=hidden id=hdnEnd runat=server NAME="hdnEnd"/>
            <table width="100%" border="0" cellSpacing="0" cellPadding="2">
	            <tr>
	                <td class="ContentTableHeader" align="Left">
		                <div>
		                    <div class="Content" onmouseover='this.style.border="1px outset white"' onmouseout='this.style.border="1px solid white"' onmousedown='this.style.border="1px inset white"' onmouseup='this.style.border="1px outset white"' id="divFirst" style="position:relative; float:left; top:auto; left:auto; border:solid 1px white;"><asp:LinkButton ToolTip="Go to first group of related products" CssClass="Content" style="text-decoration:none; color:White;" ID="lnkFirst" runat=server>&nbsp;<&nbsp;</asp:LinkButton></div>
		                    <div class="Content" style="position:relative; float:left; top:auto; left:auto;">&nbsp;</div>
		                    <div class="Content" onmouseover='this.style.border="1px outset white"' onmouseout='this.style.border="1px solid white"' onmousedown='this.style.border="1px inset white"' onmouseup='this.style.border="1px outset white"' id="divPrev" style="position:relative; float:left; top:auto; left:auto; border:solid 1px white;"><asp:LinkButton ToolTip="Previous group of related products" CssClass="Content" Style="text-decoration:none; color:White;" ID=lnkPrev runat=server>&nbsp;Prev&nbsp;</asp:LinkButton></div>
		                </div>
		            </td>
		            <td class="ContentTableHeader" align="center">
		                <div class="Content" style="position:relative; float:left; top:auto; left:auto;" id="divPaging" runat="server"></div>
		            </td>
	                <td class="ContentTableHeader" align="Right">
	                    <div class="Content" onmouseover='this.style.border="1px outset white"' onmouseout='this.style.border="1px solid white"' onmousedown='this.style.border="1px inset white"' onmouseup='this.style.border="1px outset white"' id="divLast" style="position:relative; float:right; top:auto; left:auto; border:solid 1px white;"><asp:LinkButton ToolTip="Go to last group of related products" CssClass="Content" style="text-decoration:none; color:White;" ID="lnkLast" runat=server>&nbsp;>&nbsp;</asp:LinkButton></div>
	                    <div class="Content" style="position:relative; float:right; top:auto; left:auto;">&nbsp;</div>
	                    <div class="Content" onmouseover='this.style.border="1px outset white"' onmouseout='this.style.border="1px solid white"' onmousedown='this.style.border="1px inset white"' onmouseup='this.style.border="1px outset white"' id="divNext" style="position:relative; float:right; top:auto; left:auto; border:solid 1px white;"><asp:LinkButton ToolTip="Next group of related products" CssClass="Content" Style="text-decoration:none; color:White;" ID=lnkNext runat=server>&nbsp;Next&nbsp;</asp:LinkButton></div>
	                </td>
	            </tr>
            </table>
	    </td>
	    <TD class="ContentTableHeader"></TD>
	    <td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
	</tr>
</TBODY></TABLE>
