<%
' ----------------------------------------------------------------------------
' EZMainDBUtils.asp		Utilities for the EZMain database
'
' NOTE: This module requires CommonDBUTILS.asp!
'
' Revision history:
' 05-Feb-01   MLM   0102    created
' 15-Aug-02   TRX           cobbed directly from LeadsDBUtils.asp
' ----------------------------------------------------------------------------

' ----- CONSTANTS -----

' Database connection strings - defined in application variables

' Database connection strings
Const EZMainDB_ConnectString = "DSN=EZMain; UID=EZ_User; PWD=texans02"

' Timeout for database connection
Const EZMainDB_ConnectTimeout = 20
Const EZMainDB_CommandTimeout = 120

' ----- GLOBAL VARIABLES -----

' Database connection objects

' These are the one and only global objects defined for this database.
' When calling these database specific functions, you are accessing 
' one of these global objects.  It is possible to define and create 
' your own unique database object and use the generic functions.
Dim objEZMainDB

' Initialize the global connection objects
Set objEZMainDB = Nothing

%>
<script language=VBScript runat=Server>
' ---------------------------------------------------
' D a t a b a s e   U t i l i t y   F u n c t i o n s
' ---------------------------------------------------

' ----- EZMain database -----

Function OpenEZMainDB()
	OpenEZMainDB = OpenDatabaseConnection(objEZMainDB, EZMainDB_ConnectString)
End Function

Function CloseEZMainDB()
	Call CloseDatabaseConnection(objEZMainDB)
End Function

Function CreateEZMainRS(RS, SQLStmt)
	' returns True when successful create
	If Not OpenEZMainDB Then CreateEZMainRS = False: Exit Function
	On Error Resume Next
	objEZMainDB.CommandTimeout = EZMainDB_CommandTimeout
	Set RS = objEZMainDB.Execute(SQLStmt)
	If Err Then
		Set RS = Nothing
		CreateEZMainRS = False
	Else	
		CreateEZMainRS = True
	End If	
End Function

Function GetEZMainRS(RS, SQLStmt)
	' returns True when non-empty recordset
	If Not OpenEZMainDB Then GetEZMainRS = False: Exit Function
	If Not CreateEZMainRS(RS, SQLStmt) Then
		' Create failed!
		GetEZMainRS = False
	ElseIf Not CheckRS(RS) Then 
		' Empty recordset!
		Set RS = Nothing
		GetEZMainRS = False
	Else
		' Data is available
		GetEZMainRS = True
	End If	
End Function
</script>