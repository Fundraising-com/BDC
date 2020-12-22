<%
Function ValAmt(amt)
	remove = amt
	remove = Replace(remove, "1", "")
	remove = Replace(remove, "2", "")
	remove = Replace(remove, "3", "")
	remove = Replace(remove, "4", "")
	remove = Replace(remove, "5", "")
	remove = Replace(remove, "6", "")
	remove = Replace(remove, "7", "")
	remove = Replace(remove, "8", "")
	remove = Replace(remove, "9", "")
	remove = Replace(remove, "0", "")
	remove = Replace(remove, ",", "")
	remove = Trim(Replace(remove, ".", ""))
	amt = Trim(Replace(amt, remove, ""))
	ValAmt = amt
	
End Function

Function XValAmt(amt)
	remove = amt
	remove = Replace(remove, "1", "")
	remove = Replace(remove, "2", "")
	remove = Replace(remove, "3", "")
	remove = Replace(remove, "4", "")
	remove = Replace(remove, "5", "")
	remove = Replace(remove, "6", "")
	remove = Replace(remove, "7", "")
	remove = Replace(remove, "8", "")
	remove = Replace(remove, "9", "")
	remove = Replace(remove, "0", "")
	remove = Replace(remove, ",", "")
	remove = Trim(Replace(remove, ".", ""))
	amt = Trim(Replace(amt, remove, ""))
	XValAmt = amt
End Function
%>