<%
'
' Go-Green menu include
'
	Dim BrochureGoGreenASP:	BrochureGoGreenASP = BrochureProgramASP
	Dim DirectSellerGoGreenASP:	DirectSellerGoGreenASP = DirectSellerASP
	
	Const cMaxGoGreenPrograms = 30	' pick a number; we should not reach this number
	Dim GoGreenPrograms(30,9)
		Const cxGoGreenPgmCde = 1
		Const cxGoGreenMenuTxt = 2
		Const cxGoGreenImagePrfxNme = 3
		Const cxGoGreenImageExtNme = 4
		Const cxGoGreenImageDescTxt = 5
		Const cxGoGreenShrtFeatTxt = 6
		Const cxGoGreenPortraitPageOrientFlg = 7	' 1=Portrait, 0=Landscape
		Const cxGoGreenFeatPgmDescTxt = 8
		Const cxGoGreenPdctGrpCde = 9		' BROCHURE, DIRECT, ???
	Dim nGoGreenPrograms


' === Load Site Program List

Function LoadSiteGoGreenProgramList(ByVal sPgmGrpCde)
	Dim RS, SQLStmt
	
	On Error Resume Next
	Call OpenEZMainDB()
	
	SQLStmt = "SITE_GetProgramList @PgmGrpCde=" & SQS(sPgmGrpCde)
	Set RS = Nothing
	Set RS = objEZMainDB.Execute(SQLStmt)
	
	nGoGreenPrograms = 0
	Do While CheckRS(RS)
		nGoGreenPrograms = nGoGreenPrograms + 1
		If nGoGreenPrograms > cMaxGoGreenPrograms Then nGoGreenPrograms = cMaxGoGreenPrograms: Exit Do

		GoGreenPrograms(nGoGreenPrograms, cxGoGreenPgmCde) = nvs(RS.Fields("PGM_CDE"))
		GoGreenPrograms(nGoGreenPrograms, cxGoGreenMenuTxt) = nvs(RS.Fields("MENU_TXT"))
		GoGreenPrograms(nGoGreenPrograms, cxGoGreenImagePrfxNme) = nvs(RS.Fields("IMAGE_PRFX_NME"))
		GoGreenPrograms(nGoGreenPrograms, cxGoGreenImageExtNme) = nvs(RS.Fields("IMAGE_EXT_NME"))
		GoGreenPrograms(nGoGreenPrograms, cxGoGreenImageDescTxt) = nvs(RS.Fields("IMAGE_DESC_TXT"))
		GoGreenPrograms(nGoGreenPrograms, cxGoGreenShrtFeatTxt) = nvs(RS.Fields("SHRT_FEAT_TXT"))
		GoGreenPrograms(nGoGreenPrograms, cxGoGreenPortraitPageOrientFlg) = nvn(RS.Fields("PAGE_ORIENT_PORT_FLG"))
		GoGreenPrograms(nGoGreenPrograms, cxGoGreenFeatPgmDescTxt) = nvs(RS.Fields("FEAT_PGM_DESC_TXT"))
		GoGreenPrograms(nGoGreenPrograms, cxGoGreenPdctGrpCde) = nvs(RS.Fields("PDCT_GRP_CDE"))
		
		RS.MoveNext
	Loop
	RS.Close
	Set RS = Nothing
	
	LoadSiteGoGreenProgramList = (Err.number = 0)
End Function

Function ConstructGoGreenMenu()
	Dim i

	' main menu
	Call ConstructMainMenu()
	' construct sub-menu
	For i = 1 To nGoGreenPrograms
		Select Case GoGreenPrograms(i, cxGoGreenPdctGrpCde)
			Case cDirectPdctGrpCde
					Call AddSubMenuItem(GoGreenPrograms(i, cxGoGreenMenuTxt), DirectSellerGoGreenASP & AddParam("?", cPdctCdeParam, GoGreenPrograms(i, cxGoGreenPgmCde)) & AddParam("&", cPdctGrpCdeParam, cGoGreenPgmGrpCde) & AddParam("&", cPdctCtgyCdeParam, cGoGreenPgmGrpCde), "View " & GoGreenPrograms(i, cxGoGreenMenuTxt) & " product", GoGreenMenuSection)
			Case Else:		
					Call AddSubMenuItem(GoGreenPrograms(i, cxGoGreenMenuTxt), BrochureGoGreenASP & AddParam("?", cPgmCdeParam, GoGreenPrograms(i, cxGoGreenPgmCde)) & AddParam("&", cPgmGrpCdeParam, cGoGreenPgmGrpCde), "View " & GoGreenPrograms(i, cxGoGreenMenuTxt) & " brochure", GoGreenMenuSection)
		End Select
	Next
	If nGoGreenPrograms = 0 Then 	Call AddSubMenuItem("", "", "", GoGreenMenuSection)	' NB: create empty sub-menu for this section (in case of DB error!)
	' menu footer
	' if defined (in database) use footer for this page, otherwise use the site default
	If sPageCtlMenuFooter <> "" Then Call AddMenuFooter(sPageCtlMenuFooter)

End Function

%>
