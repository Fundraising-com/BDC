USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_ins_CUserProfile_Light]    Script Date: 06/07/2017 09:33:26 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE  PROCEDURE  [dbo].[pr_ins_CUserProfile_Light]
 @UserName  varchar(20),
 @Password  varchar(20),
 @FirstName  varchar(20),
 @LastName  varchar(30),
 @Region  varchar(2),
 @FMNumber  varchar(4),
 @DistManager  varchar(4),
 @PersonalEmail varchar(50),
 @CorporateEmail varchar(40),
 @ReturnCode  integer OUT
 AS
----------------------------------------------------------------------------
--   Author: Joshua Caesar, jcaesar@rd.com
--     Date: October 31, 2003
--  Comment: Allow for manual insertion of new hires into Account Track

--JLC 2/24/2004  - added 8888, 9999 exception for FMID, added insertion of email addrs
--JLC 09/21/2004 - copied from QSPCommon.dbo.pr_CreateATuser
-- into QSP CA Fulfillment project database: 
-- QSPCanadaCommon.dbo.pr_ins_CUserProfile_Light
----------------------------------------------------------------------------

--check to see if the username is taken
DECLARE @cnt int
SELECT @cnt = count(UserName) FROM CUserProfile WHERE UserName = @UserName
IF @cnt > 0
begin
  SET @ReturnCode = -1;
  return -1;
end

--check to see if the FMNumber is taken
SELECT @cnt = count(FMNumber) FROM CUserProfile WHERE  FMNumber =  @FMNumber AND FMNumber NOT IN('8888', '9999', '')
IF @cnt > 0
begin
  SET @ReturnCode = -2;
  return -2;
end

DECLARE @Instance int
SELECT @Instance = max(Instance) + 1 FROM CUserProfile

--insert @DistManager into AreaManager, we dont really have "Area Managers" anymore
INSERT INTO CUserProfile(Instance, UserName, [Password], FirstName, LastName, FMNumber, Region, AreaManager, Email, CorporateEmail, Created_By, LoggedIn, FailedAttempts, Locked, ChangePasswordOnNextLogin, Hold)
VALUES( @Instance, @UserName, @Password, @FirstName, @LastName, @FMNumber, @Region, @DistManager, @PersonalEmail, @CorporateEmail, 1979, 0, 0, 0, 1, 0)

IF @@rowcount <> 1
begin
  SET @ReturnCode = -3;
  return -3;
end
ELSE
begin
  SET @ReturnCode = 1;
  return 1;
end
GO
