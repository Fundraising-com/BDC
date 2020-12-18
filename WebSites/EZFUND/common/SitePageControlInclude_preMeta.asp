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
					
			Case (rsFuncCde = "META" And rsAttribCde = "KEYWORDS")
					' META DATA - keywords
					sPageCtlMetaKeywords = nvs(RS.Fields("DESC_TXT"))
					
			Case (rsFuncCde = "META" And rsAttribCde = "DESCRIPTION")
					' META DATA - description
					sPageCtlMetaDescription = nvs(RS.Fields("DESC_TXT"))
					
			Case (rsFuncCde = "HTML" And rsAttribCde = "TITLE")
					' HTML Title
					sPageCtlHTMLTitle = nvs(RS.Fields("DESC_TXT"))
					
			Case Else
					' unknown function and/or attribute for this page
		End Select

		RS.MoveNext
	Loop
	RS.Close
	Set RS = Nothing

	LoadControlParamsForPage = (Err.number = 0)
End Function

%>
