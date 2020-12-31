USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_CUserProfile_CreateUser]    Script Date: 06/07/2017 09:33:16 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_CUserProfile_CreateUser]

	@Firstname		varchar(50),
	@Lastname		varchar(50),
	@FMID			varchar(4) = '9999',
	@CreatedByID	int,
	@Instance		int OUTPUT

AS

DECLARE	@UserName	varchar(50)
DECLARE	@Password	varchar(50)

SET		@UserName = UPPER(SUBSTRING(@Firstname, 1, 1)) + UPPER(SUBSTRING(@LastName, 0, 6))

SELECT	@Instance = max(Instance) + 1
FROM	CUserProfile
WHERE	Instance < 15000

SET		@Password = @UserName + CONVERT(varchar(50), FLOOR(@Instance * 2.75))

INSERT INTO [QSPCanadaCommon].[dbo].[CUserProfile](
        [Instance],
        [UserName],
        [Password],
        [FirstName],
        [LastName],
        [Locked],
        [Hold],
        [FailedAttempts],
        [ChangePasswordOnNextLogin],
        [LoggedIn],
        [MakeCheckPayableTo],
        [Email],
        [LockedCounter],
        [Region],
        [InvoiceTerm],
        [UnitsGoal],
        [Modified_By],
        [Created_By],
        [Deleted_TF],
        [DateCreated],
        [DateModified],
        [CorporateEmail],
        [CUserProfileStatusId],
        [DefectorTF],
        [TimeZone],
        [DST],
        [DefaultLang],
        [DateChanged],
        [FMNumber])
VALUES (  @Instance             --AS [Instance]
        , @UserName             --AS [UserName]
        , @Password		        --AS [Password]
        , @FirstName            --AS [FirstName]
        , @LastName             --AS [LastName]
        , 0                     --AS [Locked]
        , 0                     --AS [Hold]
        , 0                     --AS [FailedAttempts]
        , 1                     --AS [ChangePasswordOnNextLogin]
        , 0                     --AS [LoggedIn]
        , 'QSP Canada'			--AS [MakeCheckPayableTo]
        , NULL					--AS [Email]
        , 0                     --AS [LockedCounter]
        , 0                     --AS [Region]
        , 30                    --AS [InvoiceTerm]
        , 0                     --AS [UnitsGoal]
        , @CreatedByID          --AS [Modified_By]
        , @CreatedByID          --AS [Created_By]
        , 0                     --AS [Deleted_TF]
        , getdate()             --AS [DateCreated]
        , getdate()             --AS [DateModified]
        , 'Crystal.Cameron@resolvecorporation.com'      --AS [CorporateEmail]
        , 1                     --AS [CUserProfileStatusId],
        , 0                     --AS [DefectorTF]
        , -4                    --AS [TimeZone]
        , 1                     --AS [DST]
        , 'en'                  --AS [DefaultLang]
        , '1995-01-01 00:00:00.000'     --AS [DateChanged]
        , @FMID                --AS [FMNumber]
)

PRINT 'First Name = ' + @FirstName
PRINT 'Last Name = ' + @LastName
PRINT 'Username = ' + @UserName
PRINT 'Password = ' + @Password
GO
