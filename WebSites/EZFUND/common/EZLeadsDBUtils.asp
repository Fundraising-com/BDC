<%
' ----------------------------------------------------------------------------
' EZLeadsDBUtils.asp		Utilities for the EZLeads database
'
' THIS DATABASE MODULE REQUIRES EZCommonDBUTILS.asp!!!
'
' Revision history:
' 05-Feb-01   MLM   0102    created
' ----------------------------------------------------------------------------

' ----- CONSTANTS -----

' Database connection strings
Const EZLeadsDB_ConnectString = "DSN=EZLeads; UID=sa; PWD=rockets97"

' Timeout for database connection
Const EZLeadsDB_ConnectTimeout = 20
Const EZLeadsDB_CommandTimeout = 120

' ----- GLOBAL VARIABLES -----

' Database connection objects

' These are the one and only global objects defined for this database.
' When calling these database specific functions, you are accessing 
' one of these global objects.  It is possible to define and create 
' your own unique database object and use the generic functions.
Dim objEZLeadsDB

' Initialize the global connection objects
Set objEZLeadsDB = Nothing

%>
<script language=VBScript runat=Server>
' ---------------------------------------------------
' D a t a b a s e   U t i l i t y   F u n c t i o n s
' ---------------------------------------------------

' ----- EZLeads database -----

Function OpenEZLeadsDB()
	OpenEZLeadsDB = OpenDatabaseConnection(objEZLeadsDB, EZLeadsDB_ConnectString)
End Function

Function CloseEZLeadsDB()
	Call CloseDatabaseConnection(objEZLeadsDB)
End Function

Function CreateEZLeadsRS(RS, SQLStmt)
	' returns True when successful create
	If Not OpenEZLeadsDB Then CreateEZLeadsRS = False: Exit Function
	On Error Resume Next
	objEZLeadsDB.CommandTimeout = EZLeadsDB_CommandTimeout
	Set RS = objEZLeadsDB.Execute(SQLStmt)
	If Err Then
		Set RS = Nothing
		CreateEZLeadsRS = False
	Else	
		CreateEZLeadsRS = True
	End If	
End Function

Function GetEZLeadsRS(RS, SQLStmt)
	' returns True when non-empty recordset
	If Not OpenEZLeadsDB Then GetEZLeadsRS = False: Exit Function
	If Not CreateEZLeadsRS(RS, SQLStmt) Then
		' Create failed!
		GetEZLeadsRS = False
	ElseIf Not CheckRS(RS) Then 
		' Empty recordset!
		Set RS = Nothing
		GetEZLeadsRS = False
	Else
		' Data is available
		GetEZLeadsRS = True
	End If	
End Function
</script>