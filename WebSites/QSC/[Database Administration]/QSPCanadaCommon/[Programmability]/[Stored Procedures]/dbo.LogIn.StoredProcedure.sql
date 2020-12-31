USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[LogIn]    Script Date: 06/07/2017 09:33:09 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE  PROCEDURE [dbo].[LogIn]
	@UserName varchar(50)	,
	@Password varchar(50)

as
	--select * from CUserProfile where Username=@UserName and Password=@Password
SELECT
	Instance,
	UserName,
	[Password],
--	EmployeeInstance,
	FirstName,
	LastName,
--	Locked,
--	Hold,
--	FailedAttempts,
--	ChangePasswordOnNextLogin,
--	LoggedIn,
--	MakeCheckPayableTo,
--	Email,
--	FMNumber,
	FMNumber AS FMID,
--	MailAddress1,
--	MailAddress2,
--	MailCity,
--	MailState,
--	MailPostalCode,
--	ShipAddress1,
--	ShipAddress2,
--	ShipCity,
--	ShipState,
--	ShipPostalCode,
--	VoiceMailExt,
--	HomePhone,
--	WorkPhone,
--	FaxPhone,
--	TollFreePhone,
--	MobilePhone,
--	PagerPhone,
--	SignificantOther,
--	LockedCounter,
--	LockedDateTimeStamp,
--	Region,
--	AreaManager,
--	InvoiceTerm,
--	DefaultInvMsg1,
--	DefaultInvMsg2,
--	DefaultInvMsg3,
--	UnitsGoal,
	Modified_By,
	Created_By,
	Deleted_TF,
	DateCreated,
	DateModified,
--	CorporateEmail,
--	CUserProfileStatusId,
--	TopNodeId,
--	DefectorTF,
--	InvCompany,
--	InvAddress1,
--	InvAddress2,
--	InvCity,
--	InvState,
--	InvPostalCode,
--	InvPhone,
--	TimeZone,
--	DST,
--	DefaultLang,
	DateChanged,
	UserIDChanged/*,
	UserIDChanged AS UserIDModified*/
FROM 
	CUserProfile
WHERE
	Username=@UserName 
	and [Password] =@Password
	and Deleted_TF <> 1
	and Locked <> 1
GO
