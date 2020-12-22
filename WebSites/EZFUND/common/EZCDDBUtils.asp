<%
' ----------------------------------------------------------------------------
' EZCDDBUtils.asp		Utilities for the Cookie Dough tables
'
' THIS DATABASE MODULE REQUIRES EZCommonDBUTILS.asp!!!
' NOTE! Here, "Cookie" means the cookie dough product, NOT browser cookies!
'
' Revision history:
' 05-Mar-02   SB   0202    created
' ----------------------------------------------------------------------------

' ----- CONSTANTS -----

' Database connection strings
Const EZCDDB_ConnectString = "DSN=EZLeads; UID=sa; PWD=rockets97"	

' Timeout for database connection
Const EZCDDB_ConnectTimeout = 20
Const EZCDDB_CommandTimeout = 120

' ----- GLOBAL VARIABLES -----

' Database connection objects

' These are the one and only global objects defined for this database.
' When calling these database specific functions, you are accessing 
' one of these global objects.  It is possible to define and create 
' your own unique database object and use the generic functions.
Dim objEZCDDB

' Initialize the global connection objects
Set objEZCDDB = Nothing

%>
<script language=VBScript runat=Server>
' ---------------------------------------------------
' D a t a b a s e   U t i l i t y   F u n c t i o n s
' ---------------------------------------------------

' ----- EZCD database -----

Function OpenEZCDDB()
	OpenEZCDDB = OpenDatabaseConnection(objEZCDDB, EZCDDB_ConnectString)
End Function

Function CloseEZCDDB()
	Call CloseDatabaseConnection(objEZCDDB)
End Function

Function CreateEZCDRS(RS, SQLStmt)
	' returns True when successful create
	If Not OpenEZCDDB Then CreateEZCDRS = False: Exit Function
	On Error Resume Next
	objEZCDDB.CommandTimeout = EZCDDB_CommandTimeout
	Set RS = objEZCDDB.Execute(SQLStmt)
	If Err Then
		Set RS = Nothing
		CreateEZCDRS = False
	Else	
		CreateEZCDRS = True
	End If	
End Function

Function GetEZCDRS(RS, SQLStmt)
	' returns True when non-empty recordset
	If Not OpenEZCDDB Then GetEZCDRS = False: Exit Function
	If Not CreateEZCDRS(RS, SQLStmt) Then
		' Create failed!
		GetEZCDRS = False
	ElseIf Not CheckRS(RS) Then 
		' Empty recordset!
		Set RS = Nothing
		GetEZCDRS = False
	Else
		' Data is available
		GetEZCDRS = True
	End If	
End Function

Function GetEZCDShipAmtForPdctState(ByVal sPdctCde, ByVal sStCde, ByVal nOrdrQty)
	' Returns the shipping (freight) charge for a given
	'	product code (always "CD" as of this writing),
	'	state code (e.g., "TX") and
	'	number of tubs shipped.
	' If shipping is free, returns 0.
	' If shipping must be quoted by EZ, returns -1.
	' Remember, returned value is type Double; shipping may contain pennies!

	Dim RS, SQLStmt

	SQLStmt = ""
	SQLStmt = SQLStmt & " Select EZ_SHIP_PRCE_TBL.SHIP_AMT "
	SQLStmt = SQLStmt & " From EZ_SHIP_ST_MAP_TBL Inner Join EZ_SHIP_PRCE_TBL "
	SQLStmt = SQLStmt & " On EZ_SHIP_ST_MAP_TBL.SHIP_ZONE_NBR = EZ_SHIP_PRCE_TBL.SHIP_ZONE_NBR "
	SQLStmt = SQLStmt & " Where EZ_SHIP_ST_MAP_TBL.PDCT_CDE = '" & sPdctCde & "' "
	SQLStmt = SQLStmt & " And   EZ_SHIP_PRCE_TBL.PDCT_CDE = '" & sPdctCde & "' "
	SQLStmt = SQLStmt & " And   EZ_SHIP_ST_MAP_TBL.ST_CDE = '" & sStCde & "' "
	SQLStmt = SQLStmt & " And   EZ_SHIP_PRCE_TBL.MIN_ORDR_QTY <= " & nOrdrQty & " "
	SQLStmt = SQLStmt & " And   EZ_SHIP_PRCE_TBL.MAX_ORDR_QTY >= " & nOrdrQty & " "

	If GetEZCDRS(RS, SQLStmt) Then
		GetEZCDShipAmtForPdctState = CDbl(RS("SHIP_AMT"))
	Else
		GetEZCDShipAmtForPdctState = CDbl(-1)
	End If
	
	On Error Resume Next
	If Not RS Is Nothing Then
		RS.Close
		Set RS = Nothing
	End If

End Function

</script>