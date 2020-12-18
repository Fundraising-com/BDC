
' *
' * EditLive.vbs
' * Copyright (c) 1999-2000 Ephox
' * 
' * $Author: Dan $ 
' * $Date: 16/02/00 2:03p $
' * $Revision: 2 $ 
' *
' * This software is provided "AS IS," without a warranty of any kind.
' *



function GotEditLive()
	on error resume next
	dim oTest
	set oTest = CreateObject("EphoxEditLive.EditLive")
	GotEditLive = IsObject(oTest)
	set oTest = Nothing
end function