<div class="ContentData">
<b>Mailing and remittance address:</b><br>
<%=EZInfoMailingAddress(1)%><br>
<%=EZInfoMailingAddress(2)%><br>
<%=EZInfoMailingAddress(3)%>
</div><p>

<div class="ContentData">
<b>Physical address:</b><br>
<%=EZInfoMailingAddress(1)%><br>
<%=EZInfoMailingAddress(2)%><br>
<%=EZInfoMailingAddress(3)%>
</div><p>

<table border=0 cellspacing=0 cellpadding=1 class="ContentData">
<tr><td align=right>Phone</td><td>&nbsp;</td><td><%=EZMainLocalPhone%></td></tr>
<tr><td align=right>&nbsp;</td><td>&nbsp;</td><td><%=EZSalesPhone%>&nbsp;&nbsp;(Sales - Toll-Free)</td></tr>
<tr><td align=right>&nbsp;</td><td>&nbsp;</td><td><%=EZSalesLocalPhone%>&nbsp;&nbsp;(Sales - Toll)</td></tr>
<tr><td align=right>&nbsp;</td><td>&nbsp;</td><td><%=EZHelpDeskPhone%>&nbsp;&nbsp;(Help Desk - Toll-Free)</td></tr>
<tr><td align=right>&nbsp;</td><td>&nbsp;</td><td><%=EZHelpDeskLocalPhone%>&nbsp;&nbsp;(Help Desk - Toll)</td></tr>
<tr><td align=right>Fax</td><td>&nbsp;</td><td><%=EZMainLocalFax%></td></tr>
<tr><td align=right>Email</td><td>&nbsp;</td><td><a href="mailto:<%=EZHelpDeskEmail%>"><%=EZHelpDeskEmail%></a></td></tr>
</table>
