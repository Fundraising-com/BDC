<HTML>
<HEAD>
<TITLE>Pay with InternetCash(TM)</TITLE>
</HEAD>

<% Dim SC_MERCH_ID
Call SetMerchantID
%>

<!--#include file="SFLib/sc_merch_conf.inc"-->

<BODY topmargin=0 leftmargin=0 marginwidth=0 marginheight=0>
	<APPLET NAME="SCApplet"
			CODE="SCApplet"
			CODEBASE="."
			WIDTH=400
			HEIGHT=320>
	<PARAM NAME="merchantID" VALUE="<% = SC_MERCH_ID %>">		
	<PARAM NAME="merchTransID" VALUE="<% =Request("ORDER_ID")%>">
	<PARAM NAME="currencyCode" VALUE="USD">
	<PARAM NAME="amount" VALUE="<% = FormatNumber(Request("GRAND_TOTAL"), 2)%>">
	<PARAM NAME="description" VALUE="Description of items">
	<PARAM NAME="target" VALUE="SC_APPLETWINDOW">
	<PARAM NAME="location" VALUE="confirm.asp">
	<PARAM NAME="closeURL" VALUE="SCAppletClose.html">
	<PARAM NAME="pass" VALUE="<% =Request.QueryString %>">
	It appears that you don't have Java Turned on.  You must
	have Java on to use Internet Cash. 
	</APPLET>

</BODY>
</HTML>
