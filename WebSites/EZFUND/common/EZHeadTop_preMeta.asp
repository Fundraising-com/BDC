<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<%
	' if page a page does NOT define META DATA information we will use these defaults
	If nvs(sPageCtlMetaKeywords) = "" Then
		sPageCtlMetaKeywords = "EZFund.com, fundraising, fundraiser, school fundraiser, cookie dough, cookie dough fundraiser, frozen products, brochure fundraiser, candy, candy fundraiser, prize programs, 50% profit, quality fundraising products, quality fundraising service, non-profit fundraising, Smencils, Gourmet Lollipops"
	End If
	If nvs(sPageCtlMetaDescription) = "" Then
		sPageCtlMetaDescription = "EZFund.com specializes in providing the best quality fundraising products and services available in the fundraising industry."
	End If
%>
<META CONTENT="<%=sPageCtlMetaKeywords%>" NAME="keywords">
<META CONTENT="<%=sPageCtlMetaDescription%>" NAME="description">
<LINK REL=stylesheet HREF="/images/EZFund_Site.css" TYPE="text/css">
<!--#include virtual="js/SiteMapJSInclude.js"-->