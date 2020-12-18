<%

Const EZMastMenuDelim = "<span class=""MastMenuDelim"">&nbsp;|&nbsp;</span>"			' delimiter between MAST menu items
Const EZBottomMenuDelim = "<span class=""BottomMenuDelim"">&nbsp;|&nbsp;</span>"		' delimiter between FOOTER menu items


' ------------------------------  F E A T U R E S

' Home
Const cEZFeatureHome =			"HOME"
' Other features
Const cEZFeatureContactUs =		"CONTACTUS"
Const cEZFeatureComments =		"COMMENTS"
Const cEZFeatureSupport =		"SUPPORT"
Const cEZFeatureFAQ =			"FAQ"
Const cEZFeatureWhatsNew =		"WHATSNEW"
Const cEZFeatureAboutUs =		"ABOUTUS"
Const cEZFeatureMyEZFund =		"MYEZFUND"
Const cEZFeatureGroups =		"GROUPS"
Const cEZFeaturePreSeller =		"CAMPAIGN"
Const cEZFeatureDirectSeller =		"DIRECT"
Const cEZFeatureProducts =		"PRODUCTS"
Const cEZFeaturePrograms =		"PROGRAMS"
Const cEZFeatureServices =		"SERVICES"
Const cEZFeaturePrivacy =		"PRIVACY"
Const cEZFeatureSiteMap =		"SITEMAP"


Dim sEZSelectedFeature:	sEZSelectedFeature = ""	' selected feature

Function SetEZSelectedFeature(ByVal theFeature)
	sEZSelectedFeature = theFeature
End Function

Function GetEZSelectedFeature()
	GetEZSelectedFeature = sEZSelectedFeature
End Function

' ------------------------------  M E N U  F U N C T I O N S

Function EmitEZMastMenu()
	Call EmitEZVisitorMastMenu()
End Function

Function EmitEZBottomMenu()
	Call EmitEZVisitorBottomMenu()
End Function

Function EZMastMenuItem(theName, theLink, theTitle, theAttr, bSelected)
	If bSelected Then
		EZMastMenuItem = "<A class=""MastMenuSelect"" href=" & DQS(theLink) & " title=" & DQS(theTitle) & theAttr & ">" & theName & "</A>" & vbCrLf
	Else
		EZMastMenuItem = "<A class=""MastMenu"" href=" & DQS(theLink) & " title=" & DQS(theTitle) & theAttr & ">" & theName & "</A>" & vbCrLf
	End If	
End Function

Function EZBottomMenuItem(theName, theLink, theTitle, theAttr, bSelected)
	If bSelected Then
		EZBottomMenuItem = "<A class=""BottomMenuSelect"" href=" & DQS(theLink) & " title=" & DQS(theTitle) & ">" & theName & "</A>" & vbCrLf
	Else
		EZBottomMenuItem = "<A class=""BottomMenu"" href=" & DQS(theLink) & " title=" & DQS(theTitle) & ">" & theName & "</A>" & vbCrLf
	End If	
End Function


' ------------------------------  V I S I T O R   M E N U

Function EmitEZVisitorMastMenu()
	Dim sMenu: sMenu = ""

	' === MAST MENU
	sMenu = sMenu & "<div class=" & DQS("MastMenuRow") & ">"
	
sMenu = sMenu &	EZMastMenuItem("Home", HomePageASP, "Return to EZFund.com home page", "", (sEZSelectedFeature = cEZFeatureHome))
	
sMenu = sMenu & EZMastMenuDelim & EZMastMenuItem("Group Offerings", GroupProgramASP, "Select from a list of products designed especially for your group size.", "", (sEZSelectedFeature = cEZFeatureGroups))
	
sMenu = sMenu & EZMastMenuDelim & EZMastMenuItem("Order-Takers",	FoodProgramASP, "View a list of order-taker products offered by EZFund.com", "", (sEZSelectedFeature = cEZFeaturePreSeller))
	
sMenu = sMenu & EZMastMenuDelim & EZMastMenuItem("In-Hand Sellers",	DirectSellerASP, "View a list of in-hand seller products offered by EZFund.com", "", (sEZSelectedFeature = cEZFeatureDirectSeller))
	
sMenu = sMenu & EZMastMenuDelim & EZMastMenuItem("My EZFund",		MyEZFundASP, "Log in to My EZFund.com to process your cookie dough orders", NoFollowLinkAttribute, (sEZSelectedFeature = cEZFeatureMyEZFund))

sMenu = sMenu & EZMastMenuDelim & EZMastMenuItem("Site Map",		SiteMapASP, "www.EZFund.com Site Map", "", (sEZSelectedFeature = cEZFeatureSiteMap))
	sMenu = sMenu & "&nbsp;" & "</div>"

	Response.Write sMenu
End Function

Function EmitEZVisitorBottomMenu()
	Dim sMenu: sMenu = ""

	' === BOTTOM MENU
	sMenu = sMenu & "<div class=" & DQS("BottomMenuRow") & ">"
	sMenu = sMenu &						EZBottomMenuItem("Home",			HomePageASP, "Return to EZFund.com home page", "", (sEZSelectedFeature = cEZFeatureHome))
	sMenu = sMenu & EZBottomMenuDelim & EZBottomMenuItem("About Us",		AboutUsASP, "Information about EZFund.com", "", (sEZSelectedFeature = cEZFeatureAboutUs))
	sMenu = sMenu & EZBottomMenuDelim & EZBottomMenuItem("Group Offerings",	GroupProgramASP, "Select from a list of products designed especially for your group size.", "", (sEZSelectedFeature = cEZFeatureGroups))
	sMenu = sMenu & EZBottomMenuDelim & EZBottomMenuItem("Order-Takers",	FoodProgramASP, "View a list of order-taker products offered by EZFund.com", "", (sEZSelectedFeature = cEZFeaturePreSeller))
	sMenu = sMenu & EZBottomMenuDelim & EZBottomMenuItem("In-Hand Sellers",	DirectSellerASP, "View a list of in-hand seller products offered by EZFund.com", "", (sEZSelectedFeature = cEZFeatureDirectSeller))
	sMenu = sMenu & EZBottomMenuDelim & EZBottomMenuItem("Contact Us",		ContactUsASP, "Our mailing address and telephone numbers", "", (sEZSelectedFeature = cEZFeatureContactUs))
	sMenu = sMenu & EZBottomMenuDelim & EZBottomMenuItem("Privacy Policy",	PrivacyPolicyASP, "Review EZFund.com's Privacy Policy", "", (sEZSelectedFeature = cEZFeaturePrivacy))
	sMenu = sMenu & "</div>"

	Response.Write sMenu
End Function
%>
