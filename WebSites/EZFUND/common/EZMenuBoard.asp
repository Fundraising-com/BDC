<%	' NB: the left-hand column will be structured as a menu / message board
	' NB: some pages may choose to disable the MenuBoard (ie. Call DisableMenuBoard())

	' configure a left hand menu/message board
	If PageHasMenuBoard() = False Then
		Response.Write "<table border=0 cols=1 cellspacing=0 cellpadding=0>"
		Response.Write "<tr>"
		Response.Write "<td valign=top class=PageContentFull>"
		ContentWindowWidth = ContentWindowWidthNoMenu		' full page content window
	Else
		Response.Write "<table border=0 cols=3 cellspacing=0 cellpadding=0>"
		Response.Write "<tr><td valign=top align=left class=PageContentCol1>"
		' 1/21/08 - link to Info page
		Response.Write "<br><center><a href=""/ezforms/ProductInfoRequest.asp"" title=""Request FREE information on any of our fundraising products.""><img src=" & QS("/images/EZMenuInfoRequest.gif") & " alt=" & QS("Request FREE fundraising information") & " border=0></a></center>"
		' Main Menu gets top billing...
		Call EmitMainMenu()
		' next come the message boards...
		Call EmitMessageBoard()
		Response.Write "</td>"
		Response.Write "<td class=PageContentCol2>&nbsp;</td>"
		Response.Write "<td valign=top class=PageContentCol3>"
		ContentWindowWidth = ContentWindowWidthWithMenu		' smaller content window
	End If

	' 130px 8px 580px = 718px
	' 140px 8px 580px = 728px

	Response.Write "<table border=0 cellpadding=0 cellspacing=0 width=""98%"">"
	Response.Write "<tr><td>"

%>
<script language=VBScript runat=Server>

' default main menu called by various pages

Function ConstructMainMenu()

	Call InitMenuItems("", "", "", "")

	Call AddMenuItem(GoGreenMenuSection,		GoGreenASP,			"View a complete list of our environment-friendly Go-Green! fundraising products.", GoGreenMenuSection)
	Call AddMenuItem(HomePageMenuSection,		HOME_URL,			"EZFund.com home page", HomePageMenuSection)
	Call AddMenuItem(GroupProgramMenuSection,	GroupProgramASP,	"Find the perfect product offering for your size group.", HomePageMenuSection)
	Call AddMenuItem(FoodProgramMenuSection,	FoodProgramASP,		"View a list of our Food fundraising programs.", HomePageMenuSection)
	Call AddMenuItem(BrochureProgramMenuSection,BrochureProgramASP,	"View a complete list of our Brochure fundraising programs.", HomePageMenuSection)
	Call AddMenuItem(DirectSellerMenuSection,	DirectSellerASP,	"View a complete list of our In-Hand Seller fundraising products.", HomePageMenuSection)
	Call AddMenuItem(MagazineProgramMenuSection,MagazineProgramASP,	"View a complete list of our Magazine fundraising programs.", HomePageMenuSection)
	Call AddMenuItem(CustomProductMenuSection,	CustomProductASP,	"View a complete list of our Customized fundraising products.", HomePageMenuSection)
	Call AddMenuItem(PrizeIncentiveMenuSection,	PrizeIncentiveASP,	"View a list of our Prize incentive programs.", HomePageMenuSection)
	Call AddMenuItem(HolidayStoreMenuSection,	HolidayStoreASP,	"The In-School Holiday Store for children to shop for family and friends.", HomePageMenuSection)
	Call AddMenuItem(OrderOnlineMenuSection,	OrderOnlineASP,		"Order your order-takers or in-hand seller products online!", HomePageMenuSection)
	Call AddMenuItem(FundraisingServicesMenuSection, FundraisingServicesASP, "Check out the Full Fundraising Services offered by EZFund.com", HomePageMenuSection)
	Call AddMenuItem(LinksMenuSection,			LinksASP,			"Link sharing offered by EZFund.com", HomePageMenuSection)
	Call AddMenuItem(ResourcesMenuSection,		ResourcesASP,		"Resources offered by EZFund.com", HomePageMenuSection)

	' default menu footer
	' NB: individual pages can overwrite the default with their own version by calling AddMenuFooter("their_message")
	Call AddMenuFooter("Call <b>" & EZSalesPhone & "</b> today to get started with your fundraiser!")

End Function

Function EmitMainMenu()
	' construct and emit the menu items
	Dim i, si
	Dim sDelim
	Dim MainMenu: MainMenu = ""
	Dim prevSection: prevSection = ""
	Dim bMenuItemHasSubMenu: bMenuItemHasSubMenu = False
	Dim bMenuItemHasImage: bMenuItemHasImage = False

	If MenuItems(1, cxMenuSection) = "" Then Exit Function

	' MAIN MENU	
	MainMenu = "<div class=Menu>"
	For i = 1 To cMaxMenuItems
		If MenuItems(i, cxMenuSection) = "" Then Exit For

		If MenuItems(i, cxMenuSection) <> prevSection Then
			If MainMenu <> "" Then MainMenu = MainMenu & "<br>"		' extra space between sections
			prevSection = MenuItems(i, cxMenuSection)
		End If

		bMenuItemHasSubMenu = (SubMenuItems(1, cxSubMenuSection) <> "" And SubMenuItems(1, cxSubMenuSection) = MenuItems(i, cxMenuText))
		bMenuItemHasImage = (MenuItems(i, cxMenuImage) <> "")

		' SUB-MENU
		If bMenuItemHasSubMenu = True Then
			MainMenu = MainMenu & "<div class=SubMenu>"
			MainMenu = MainMenu & "<div class=MenuRow><a href=" & QS(MenuItems(i, cxMenuURL)) & " title=" & QS(MenuItems(i, cxMenuTitle)) & " class=MenuSelect>" & InclIf(MenuItems(i, cxMenuImage)<>"", MenuItems(i, cxMenuImage), MenuItems(i, cxMenuText)) & "</a></div>"
			MainMenu = MainMenu & BuildSubMenuItems()
			MainMenu = MainMenu & "</div>"		' SUB-MENU wrapper
		Else
			If MenuItems(i, cxMenuText) = LinksMenuSection Or MenuItems(i, cxMenuText) = ResourcesMenuSection Then
				' add nofollow attrib for these menu items
				MainMenu = MainMenu & "<div class=MenuRow><a href=" & QS(MenuItems(i, cxMenuURL)) & " title=" & QS(MenuItems(i, cxMenuTitle)) & NoFollowLinkAttribute & " class=MenuItem>" & MenuItems(i, cxMenuText) & "</a></div>"
			Else
				MainMenu = MainMenu & "<div class=MenuRow><a href=" & QS(MenuItems(i, cxMenuURL)) & " title=" & QS(MenuItems(i, cxMenuTitle)) & " class=MenuItem>" & InclIf(MenuItems(i, cxMenuImage)<>"", MenuItems(i, cxMenuImage), MenuItems(i, cxMenuText)) & "</a></div>"
			End If	
		End If
	Next
	MainMenu = MainMenu & "</div>"		' MENU wrapper

	' emit the menu
	Response.Write MainMenu & "<br>" & MenuFooter & "<br><br><hr class=MenuHR>"
End Function

Function BuildSubMenuItems()
	Dim si
	Dim SubMenu: SubMenu = ""
	Dim bSubMenuItemHasSubMenu

	For si = 1 To cMaxSubMenuItems
		If SubMenuItems(si, cxSubMenuText) = "" Then Exit For

		bSubMenuItemHasSubMenu = (SubItemMenuItems(1, cxSubItemMenuSection) <> "" And SubItemMenuItems(1, cxSubItemMenuSection) = SubMenuItems(si, cxSubMenuText))

		If bSubMenuItemHasSubMenu = True Then
			SubMenu = SubMenu & "<div class=SubItemMenu>"
			SubMenu = SubMenu & "<div class=SubMenuRow>&bull;&nbsp;<a href=" & QS(SubMenuItems(si, cxSubMenuURL)) & " title=" & QS(SubMenuItems(si, cxSubMenuTitle)) & " class=SubMenuSelect>" & SubMenuItems(si, cxSubMenuText) & "</a></div>"
			SubMenu = SubMenu & BuildSubItemMenuItems()
			SubMenu = SubMenu & "</div>"	' SUB-ITEM-MENU wrapper
		Else
			SubMenu = SubMenu & "<div class=SubMenuRow>&bull;&nbsp;<a href=" & QS(SubMenuItems(si, cxSubMenuURL)) & " title=" & QS(SubMenuItems(si, cxSubMenuTitle)) & " class=SubMenuItem>" & SubMenuItems(si, cxSubMenuText) & "</a></div>"
		End If
	Next
	BuildSubMenuItems = SubMenu
End Function

Function BuildSubItemMenuItems()
	Dim si
	Dim SubMenu: SubMenu = ""

	For si = 1 To cMaxSubItemMenuItems
		If SubItemMenuItems(si, cxSubItemMenuText) = "" Then Exit For
		SubMenu = SubMenu & "<div class=SubItemMenuRow>&raquo;&nbsp;<a href=" & QS(SubItemMenuItems(si, cxSubItemMenuURL)) & " title=" & QS(SubItemMenuItems(si, cxSubItemMenuTitle)) & " class=SubItemMenuItem>" & SubItemMenuItems(si, cxSubItemMenuText) & "</a></div>"
	Next
	BuildSubItemMenuItems = SubMenu
End Function

' ---------- MAIN MENU build routines ----------

' NB: all MenuItems() array defined in PageTop!
'
' Menu hierarchy: (3 levels)
'
'	MenuItem
'		* Sub-MenuItem
'			* Sub-Item-MenuItem
'			* Sub-Item-MenuItem
'			* Sub-Item-MenuItem
'		* Sub-MenuItem
'		* Sub-MenuItem
'	MenuItem
'		* Sub-MenuItem
'
'	(etc.)

' ----- MENU

Function InitMenuItems(ByVal sText, ByVal sURL, ByVal sTitle, ByVal sSection)
	' initialize the array
	Dim i
	For i = 1 To cMaxMenuItems
		MenuItems(i,cxMenuSection) = ""
		MenuItems(i,cxMenuText) = ""
		MenuItems(i,cxMenuURL) = ""
		MenuItems(i,cxMenuTitle) = ""
		MenuItems(i,cxMenuImage) = ""
	Next
	If sText <> "" Then Call AddMenuItem(sText, sURL, sTitle, sSection)	' add our first entry
End Function

Function AddMenuItem(ByVal sText, ByVal sURL, ByVal sTitle, ByVal sSection)
	' add an entry to the MENU array
	Dim i
	Dim sImage: sImage = ""

	' WRITE THIS! hard-coded for now; create db field when we externalize the main menu
	If sText = GoGreenMenuSection Then sImage = "<center><img src=""/images/Go-Green_HomePG.gif"" border=0></center>"
		
	For i = 1 To cMaxMenuItems
		If MenuItems(i, cxMenuSection) = "" Then
			' add to first available slot
			MenuItems(i, cxMenuSection) = sSection
			MenuItems(i, cxMenuText) = sText
			MenuItems(i, cxMenuURL) = sURL
			MenuItems(i, cxMenuTitle) = sTitle
			MenuItems(i, cxMenuImage) = sImage
			Exit For
		End If
	Next
End Function

' ----- SUB-MENU

Function InitSubMenuItems(ByVal sText, ByVal sURL, ByVal sTitle, ByVal sSection)
	' initialize the array
	Dim i
	For i = 1 To cMaxSubMenuItems
		SubMenuItems(i,cxSubMenuSection) = ""
		SubMenuItems(i,cxSubMenuText) = ""
		SubMenuItems(i,cxSubMenuURL) = ""
		SubMenuItems(i,cxSubMenuTitle) = ""
	Next
	If sText <> "" Then Call AddMenuItem(sText, sURL, sTitle, sSection)	' add our first entry
End Function

Function AddSubMenuItem(ByVal sText, ByVal sURL, ByVal sTitle, ByVal sSection)
	' add an entry to the SUB-MENU array
	Dim i
	For i = 1 To cMaxSubMenuItems
		If SubMenuItems(i, cxSubMenuSection) = "" Then
			' add to first available slot
			SubMenuItems(i, cxSubMenuSection) = sSection
			SubMenuItems(i, cxSubMenuText) = sText
			SubMenuItems(i, cxSubMenuURL) = sURL
			SubMenuItems(i, cxSubMenuTitle) = sTitle
			Exit For
		End If
	Next
End Function

' ----- SUB-ITEM-MENU

Function InitSubItemMenuItems(ByVal sText, ByVal sURL, ByVal sTitle, ByVal sSection)
	' initialize the array
	Dim i
	For i = 1 To cMaxSubItemMenuItems
		SubItemMenuItems(i,cxSubItemMenuSection) = ""
		SubItemMenuItems(i,cxSubItemMenuText) = ""
		SubItemMenuItems(i,cxSubItemMenuURL) = ""
		SubItemMenuItems(i,cxSubItemMenuTitle) = ""
	Next
	If sText <> "" Then Call AddMenuItem(sText, sURL, sTitle, sSection)	' add our first entry
End Function

Function AddSubItemMenuItem(ByVal sText, ByVal sURL, ByVal sTitle, ByVal sSection)
	' add an entry to the SUB-ITEM-MENU array
	Dim i
	For i = 1 To cMaxSubItemMenuItems
		If SubItemMenuItems(i, cxSubItemMenuSection) = "" Then
			' add to first available slot
			SubItemMenuItems(i, cxSubItemMenuSection) = sSection
			SubItemMenuItems(i, cxSubItemMenuText) = sText
			SubItemMenuItems(i, cxSubItemMenuURL) = sURL
			SubItemMenuItems(i, cxSubItemMenuTitle) = sTitle
			Exit For
		End If
	Next
End Function


Function AddMenuFooter(ByVal sText)
	MenuFooter = sText
End Function


' ---------- MESSAGE BOARD build routines ----------

' NB: MessageBoardEntries() array defined in PageTop!

Function InitMessageBoardEntries(ByVal sHeader, ByVal sText)
	' initialize the array
	Dim i
	For i = 1 To cMaxMessageBoardEntries
		MessageBoardEntries(i,cxMBHeader) = ""
		MessageBoardEntries(i,cxMBText) = ""
	Next
	If sText <> "" Then Call AddMessageBoardEntry(sHeader, sText)	' add our first entry
End Function

Function AddMessageBoardEntry(ByVal sHeader, ByVal sText)
	' add an entry to the array
	Dim i
	For i = 1 To cMaxMessageBoardEntries
		If MessageBoardEntries(i, cxMBHeader) = "" Then
			' add to first available slot
			MessageBoardEntries(i, cxMBHeader) = sHeader
			MessageBoardEntries(i, cxMBText) = sText
			Exit For
		End If
	Next
End Function

Function EmitMessageBoard()
	' construct and emit the message board entries
	Dim i
	Dim MessageBoard: MessageBoard = ""

	For i = 1 To cMaxMessageBoardEntries
		If MessageBoardEntries(i, cxMBHeader) = "" Then Exit For

		MessageBoard = MessageBoard & "<br><div class=MsgBdTextBold>" & MessageBoardEntries(i, cxMBHeader) & "</div><br>"
		MessageBoard = MessageBoard & MessageBoardEntries(i, cxMBText) & "<br><br><hr class=MsgBdHR>"
	Next
	' emit the message board
	Response.Write MessageBoard
End Function

Function ConstructMessageBoard(ByVal sPageCde)
	' default message board called by various pages
	Dim sRebateTxt
	Dim i, sTestimonial

	Call InitMessageBoardEntries("", "")

	' -- MESSAGES
	Call LoadMessagesForPage(sPageCde)

	' -- TESTIMONIALS
	Call LoadTestimonialsForPage(sPageCde)

End Function

' === Load Page Testimonials (for display on Menu Board)

Function LoadTestimonialsForPage(ByVal sPageCde)
	Dim RS, SQLStmt
	Dim sTestmlTxt, sOrgNme, sCtctNme
	Dim sTestimonial, nTestimonials

	On Error Resume Next
	Call OpenEZMainDB()

	SQLStmt = "SITE_GetTestimonialsForPage @PageCde=" & SQS(sPageCde)
	Set RS = Nothing
	Set RS = objEZMainDB.Execute(SQLStmt)

	Do While CheckRS(RS)

		sTestmlTxt = nvs(RS.Fields("TESTML_TXT"))
		sOrgNme = nvs(RS.Fields("ORG_NME"))
		sCtctNme = nvs(RS.Fields("CTCT_NME"))

		' format our testimonial
		sTestimonial = DQS(sTestmlTxt) & "<br><br>"
		If nvs(sOrgNme) <> "" Then
			sTestimonial = sTestimonial & "<div class=MsgBdTestmlOrgNme>" & sOrgNme & "</div>"
		End If	
		sTestimonial = sTestimonial & "<div class=MsgBdTestmlCtctNme>" & sCtctNme & "</div>"
		' add it to the Message Board
		Call AddMessageBoardEntry("Testimonial", sTestimonial)

		RS.MoveNext
	Loop
	RS.Close
	Set RS = Nothing

	LoadTestimonialsForPage = (Err.number = 0)
End Function

' === Load Page Messages (for display on Menu Board)

Function LoadMessagesForPage(ByVal sPageCde)
	Dim RS, SQLStmt
	Dim sMsgHdrTxt, sMsgTxt

	On Error Resume Next
	Call OpenEZMainDB()

	SQLStmt = "SITE_GetMessagesForPage @PageCde=" & SQS(sPageCde)
	Set RS = Nothing
	Set RS = objEZMainDB.Execute(SQLStmt)

	Do While CheckRS(RS)

		sMsgHdrTxt = nvs(RS.Fields("MSG_HDR_TXT"))
		sMsgTxt = nvs(RS.Fields("MSG_TXT"))
		Call AddMessageBoardEntry(sMsgHdrTxt, sMsgTxt)

		RS.MoveNext
	Loop
	RS.Close
	Set RS = Nothing

	LoadMessagesForPage = (Err.number = 0)
End Function

</script>