<%
' ----------------------------------------------------------------------------
' EZCommonDBUtils.asp    Primitives for dealing with database
'
' This module contains general database routines.  These routines
' are not geared towards any specific database, but instead are
' common among all EZFund databases.  

' Specific EZFund database routines are in dbnameDBUtils.asp modules.
'
' General database routines such as OpenDatabaseConnection() and
' CloseDatabaseConnection() should be placed in this module.
' Database specific routines should be placed in a separate
' module named dbnameDBUtils.asp.
'
' Any page that required database routines will have to include
' this module, as well as, any database specific modules.
'
' Revision history:
' 05-Feb-01   MLM   0102    created
' ----------------------------------------------------------------------------

' ----- CONSTANTS -----

' None defined at this time.

%>
<script language=VBScript runat=Server>
' ---------------------------------------------------
' D a t a b a s e   U t i l i t y   F u n c t i o n s
' ---------------------------------------------------

' ----- GENERIC functions

Function OpenDatabaseConnection(objConnection, sConnectString)

	On Error Resume Next    
    If Not objConnection Is Nothing Then 
        OpenDatabaseConnection = True
        Exit Function
    End If    
    
    Set objConnection = Server.CreateObject("ADODB.Connection")
    objConnection.Open sConnectString
    If Err <> 0 Then
'''		Response.Write "<br>" & vbCrLf
'''		Response.Write "OpenDatabase Error:" & "<p>" & vbCrLf
'''		Response.Write "Connection:" & nbsp(3) & sConnectString & "<br>" & vbCrLf
'''		Response.Write "Error number:" & nbsp(1) & Err.Number & "<br>" & vbCrLf
'''		Response.Write "Description:" & nbsp(2) & Err.Description & "<br>" & vbCrLf
'''		Response.Write "Error Source:" & nbsp(1) & Err.Source & "<br>" & vbCrLf
		Err.Clear
        Set objConnection = Nothing
        OpenDatabaseConnection = False
    Else    
        OpenDatabaseConnection = True
    End If

End Function

Function CloseDatabaseConnection(objConnection)
    ' If you use database access, call this routine before you exit!!!
	On Error Resume Next    
    If objConnection Is Nothing Then Exit Function
    objConnection.Close
    Set objConnection = Nothing
End Function

Function CheckRequest(RS, ReqParam)
	On Error Resume Next
	If IsEmpty(Request(ReqParam)) Then
		CheckRequest = RS(ReqParam)
		If Err Then CheckRequest = ""
	Else
		CheckRequest = Request(ReqParam)
	End If
End Function

Function CheckRS(RS)
	Dim bEOF
	On Error Resume Next
	bEOF = RS.EOF
	If Err Or bEOF Then
		CheckRS = False
	Else
		CheckRS = True
	End If
End Function

Function CheckNextRS(RS)
	Dim bEOF
	On Error Resume Next
	If RS Is Nothing Then
		CheckNextRS = False
	Else
		bEOF = RS.EOF
		If Err Or bEOF Then
			CheckNextRS = False
		Else
			CheckNextRS = True
		End If
	End If
End Function

Public Function GetMemoField(fldSource)
    ' Return the current value of a Memo field.
    ' Args:
    '   fldSource
    '       must be a Field object referring to a Memo field
    '       in a recordset which is open and has a
    '       valid current record.
    '       (We don't declare it As Field because this lives
    '       in the utility functions module and we don't want to
    '       force every project to call out the database
    '       components in its references.)
    ' NOTE: if you're using ODBCDirect, you must use the
    ' dbOpenDynamic option in your OpenRecordset call
    ' for this to work, and the data source must support
    ' that kind of cursor. (Current version of SQL Server does.)
    ' NOTE: at present, error trapping is up to the caller.
    ' Set size of chunk in bytes.
    Const cChunkSize = 2048

    Dim nOffset
    Dim nTotalSize
    Dim sChunk
    Dim sResult

    ' Copy the field from one field to the return value in
    ' chunks until the entire field is copied.
    sResult = ""
    nOffset = 0
    nTotalSize = fldSource.ActualSize
    Do While nOffset < nTotalSize
        sChunk = fldSource.GetChunk(cChunkSize)
        sResult = sResult & sChunk
        nOffset = nOffset + cChunkSize
    Loop
    GetMemoField = sResult
End Function

Function WildCardPunct (ByVal s, ByVal SQLType)
	' replace all punctuations, spaces etc. with the single placement wildcard for ODBC/SQL...
	' suitable for matching user search string "9402/2032", "9402-2032", "9402 2032"
	' (may cause some false positives, but better than missing some)
   
	Dim ch, uch, t, NonAlphaNumCount

	s = Trim(s)
	t = ""
	NonAlphaNumCount = 0

	Do While s <> ""
        
		ch = Left(s, 1): uch = UCase(ch)
		s = Mid(s, 2)
        
		Select Case True
            
			Case InStr("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", uch) > 0
				' alphanum: if preceded by 1 or more punctuations,
				' emit a "?" (if 1) or a "*" (if more than 1)
				' in any case, emit the character
				If NonAlphaNumCount > 0 Then
					If NonAlphaNumCount = 1 Then
						If SQLType Then 
							t = t & "_" 
						Else 
							t = t & "?"    ' single placement char
						End If
					Else
						If SQLType Then 
							t = t & "%" 
						Else 
							t = t & "*"    ' wildcard char
						End If
					End If
					NonAlphaNumCount = 0
				End If
				t = t & ch
            
			Case InStr("%_", uch) > 0
				' user-supplied SQL wildcard: either allow or convert 
				If SQLType Then
					t = t & ch
				Else
					If uch = "%" Then 
						t = t & "*" 
					Else 
						t = t & "?"
					End If
				End If
            
			Case InStr("*?", uch) > 0
				' user-supplied wildcard: either allow or convert
				If SQLType Then
					If uch = "*" Then 
						t = t & "%" 
					Else 
						t = t & "_"
					End If
				Else
					t = t & ch
				End If

			Case Else
				' anything else: just note that we've seen a non-alphanumeric
				NonAlphaNumCount = NonAlphaNumCount + 1
        
		End Select
    
	Loop
    
	' NOTE: we intentionally ignore trailing punctuations;
	' they will be caught in the * always appended to the LIKE clause.
   
	WildCardPunct = t

End Function

</script>