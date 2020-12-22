<%

' === Load Page Control params

' Assumptions:
'
'	* caller must include the EZMainDBUtils.asp include file!
'	* all sPageCtlxxxx params are defined in EZPageTop!

Function LoadControlParamsForPage(ByVal sPageCde)
	Dim RS, SQLStmt
	Dim rsFuncCde, rsAttribCde

	On Error Resume Next
	Call OpenEZMainDB()

	SQLStmt = "SITE_GetControlParamsForPage @PageCde=" & SQS(sPageCde)
	Set RS = Nothing
	Set RS = objEZMainDB.Execute(SQLStmt)

	Do While CheckRS(RS)

		rsFuncCde = nvs(RS.Fields("FUNC_CDE"))
		rsAttribCde = nvs(RS.Fields("ATTRIB_CDE"))

		Select Case True
			'
			' These control params are defined in SITE_PAGE_CTL_TBL
			'
			Case (rsFuncCde = "MENU" And rsAttribCde = "FOOTER")
					' menu footer
					sPageCtlMenuFooter = nvs(RS.Fields("DESC_TXT"))

			Case (rsFuncCde = "HTML" And rsAttribCde = "TITLE")
					' HTML Title
					sPageCtlHTMLTitle = nvs(RS.Fields("DESC_TXT"))

			Case (rsFuncCde = "META")
					' META DATA - gather all META DATA
					Call AddMETADATATags(rsAttribCde, nvs(RS.Fields("DESC_TXT")))
					
			Case Else
					' unknown function and/or attribute for this page
		End Select

		RS.MoveNext
	Loop
	RS.Close
	Set RS = Nothing

	LoadControlParamsForPage = (Err.number = 0)
End Function

Function InitMETADATATags(ByVal sHeader, ByVal sText)
	' initialize the array
	Dim i
	For i = 1 To cMaxMETADATATags
		METADATATags(i, cxMETAName) = ""
		METADATATags(i, cxMETAContent) = ""
	Next
End Function

Function AddMETADATATags(ByVal sName, ByVal sContent)
	' add/edit an entry to the array
	Dim i
	For i = 1 To cMaxMETADATATags
		If METADATATags(i, cxMETAName) = "" Then
			' ADD to first available slot
			METADATATags(i, cxMETAName) = sName
			METADATATags(i, cxMETAContent) = sContent
			Exit For
		End If
		If UCase(METADATATags(i, cxMETAName)) = UCase(sName) Then
			' if exists, EDIT slot with new content
			' NB: on duplicate META names, the last one gets saved
			METADATATags(i, cxMETAName) = sName
			METADATATags(i, cxMETAContent) = sContent
			Exit For
		End If
	Next
End Function

%>
