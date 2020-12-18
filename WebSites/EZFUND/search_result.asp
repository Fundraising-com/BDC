<%@ LANGUAGE="VBScript" %>
<%
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'   System      :   StoreFront 2000 Version 4.04.4
'   Date        :   8.03.2000
'   Author      :   LaGarde, Incorporated
'   Description :   StoreFront Search Engine Routines
'   Notes       :   There are no configurable elements in this file.
'
'                         COPYRIGHT NOTICE
'
'   The contents of this file is protected under the United States
'   copyright laws as an unpublished work, and is confidential and
'   proprietary to LaGarde, Incorporated.  Its use or disclosure in 
'   whole or in part without the expressed written permission of 
'   LaGarde, Incorporated is expressely prohibited.
'
'   (c) Copyright 1998 by LaGarde, Incorporated.  All rights reserved.
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
%>
<%

session.LCID = Session("LCID")

Const adOpenForwardOnly = 0
Const adOpenKeyset = 1
Const adOpenDynamic = 2
Const adOpenStatic = 3

'---- LockTypeEnum Values ----
Const adLockReadOnly = 1
Const adLockPessimistic = 2
Const adLockOptimistic = 3
Const adLockBatchOptimistic = 4

		SQLStmt = Request("SQLStmt")
		SQLStmtTest = UCASE(SQLStmt)
		If Instr(SQLStmtTest, "DELETE") or Instr(SQLStmtTest, "INSERT") or Instr(SQLStmtTest, "UPDATE") or Instr(SQLStmtTest, "CUSTOMER") or Instr(SQLStmtTest, "DROP") or Instr(SQLStmtTest, "ALTER") or Instr(SQLStmtTest, "CREATE") OR Instr(sSQLTest, "TRUNCATE") Then
				Response.Write "Invalid Query String"
				Response.End 
		End If
		If SQLStmt = "" Then
	If (Request.Form.Count = 0 AND Request.QueryString.Count = 0) Then
	ReturnPg = Request.ServerVariables("HTTP_REFERER")
	 ReturnPg = Replace(ReturnPg,"?ERROR=1","")
	 Response.Redirect "search.asp"
	End If


		

		SRCH_DESCRIPTION = Replace(Request("SRCH_DESCRIPTION"),"'","''")
		SRCH_MANUFACTURER = Replace(Request("SRCH_MANUFACTURER"),"'","''")
		SRCH_CATEGORY = Replace(Request("SRCH_CATEGORY"),"'","''") 
		DESCRIPTION = Replace(Request("DESCRIPTION"),"'","''")
		CATEGORY = Replace(Request("CATEGORY"),"'","''")
		MANUFACTURER = Replace(Request("MANUFACTURER"),"'","''")
		PRODUCT_ID = Request("PRODUCT_ID")
	
		
		If Request("ORDER_FLAG") = "1" Then
		

		If Request("SRCH_MANUFACTURER") = "ALL" Then
		 MANUFACTURER = "product.MFG LIKE '%%'"
		Else
		 MANUFACTURER = "product.MFG = '"& SRCH_MANUFACTURER &"'"
		End If

		PRODUCT_ID = Request("SRCH_PRODUCT_ID")

		If Request("SRCH_CATEGORY") = "ALL" Then
		 CATEGORY = "product.CATEGORY LIKE '%%'"
		Else
		 CATEGORY = "product.CATEGORY = '"& SRCH_CATEGORY &"'"
		End If

		Else

		If Request("CATEGORY") = "ALL" OR Request("CATEGORY") = "" Then
		 CATEGORY = "(product.CATEGORY LIKE '%%') OR (product.CATEGORY = '')"
		Else
		 CATEGORY = "product.CATEGORY = '"& CATEGORY &"'"
		End If
	

		If Request("MANUFACTURER") = "ALL" OR Request("MANUFACTURER") = "" Then
		 MANUFACTURER = "(product.MFG LIKE '%%') OR (product.MFG = '')"
		Else
		 MANUFACTURER = "product.MFG = '"& MANUFACTURER &"'"
		End If
		End If
		
		SQLStmt = "SELECT * FROM PRODUCT WHERE " _
		& "((DESCRIPTION LIKE '%" & DESCRIPTION & "%' OR LONG_DESCRIPTION LIKE '%" & DESCRIPTION & "%') " _
		& "AND (PRODUCT_ID LIKE '%" & PRODUCT_ID & "%') " _
		& "AND ((" & CATEGORY & ") " _
		& "AND (" & MANUFACTURER & "))) " _
		& "ORDER BY PRODUCT_ID DESC "
		 End If


	DSN_Name = Session("DSN_Name")
	
	Set Connection = Server.CreateObject("ADODB.Connection")
	Set RSSearchResult = Server.CreateObject("ADODB.RecordSet")

	Connection.Open DSN_Name

	
	RSSearchResult.Open SQLStmt,Connection,adOpenKeyset,adLockReadOnly

	intPageSize = 10
	RSSearchResult.PageSize = intPageSize
	intNumRecs = RSSearchResult.RecordCount

	If Request("Action") = "" Then
	 FormAction = "search_result.asp"
	Else
	 Response.Redirect("search.asp")
	End If
	
	ScrollAction = Request("ScrollAction")
	If ScrollAction <> "" Then
		PageNo = mid(ScrollAction,5)
		If PageNo < 1 Then
		PageNo = 1
		End If
	Else
		PageNo = 1
	End If
	
	If Not RSSearchResult.EOF Then
		
	RSSearchResult.AbsolutePage = PageNo
	
	Else
		
	End If

	
%>

<!--#include file="search_output.asp"-->