<head>
<title><%=HTMLTitle%></title>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<%
	Const cDefaultMETAKeywords = "EZFund.com, fundraising, fundraiser, school fundraiser, cookie dough, cookie dough fundraiser, frozen products, brochure fundraiser, candy, candy fundraiser, prize programs, 50% profit, quality fundraising products, quality fundraising service, non-profit fundraising, Smencils, Gourmet Lollipops"
	Const cDefaultMETADescription = "EZFund.com specializes in providing the best quality fundraising products and services available in the fundraising industry."

	Dim bMETAKeywordsDefined: bMETAKeywordsDefined = False
	Dim bMETADescriptionDefined: bMETADescriptionDefined = False

	' construct and emit the META DATA tags
	Dim i
	Dim OurMETADATA: OurMETADATA = ""

	For i = 1 To cMaxMETADATATags
		If METADATATags(i, cxMETAName) = "" Then Exit For
		' check for two known tags
		If UCase(METADATATags(i, cxMETAName)) = "KEYWORDS"		Then bMETAKeywordsDefined = True
		If UCase(METADATATags(i, cxMETAName)) = "DESCRIPTION"	Then bMETADescriptionDefined = True
		OurMETADATA = OurMETADATA & "<meta name=""" & METADATATags(i, cxMETAName) & """ content=""" & METADATATags(i, cxMETAContent) & """>" & vbCrLf
	Next
	' emit the META DATA
	Response.Write OurMETADATA

	' if a page does NOT define META DATA information we will use these defaults
	If bMETAKeywordsDefined = False Then
		Response.Write "<meta name=""KEYWORDS"" content=""" & cDefaultMETAKeywords & """>" & vbCrLf
	End If
	If bMETADescriptionDefined = False then
		Response.Write "<meta name=""DESCRIPTION"" content=""" & cDefaultMETADescription & """>" & vbCrLf
	End If
%>
<LINK REL=stylesheet HREF="/images/EZFund_Site.css" TYPE="text/css">
<% ' 2/5/08 - no longer using dropdown <!--#include virtual="js/SiteMapJSInclude.js"-->%>