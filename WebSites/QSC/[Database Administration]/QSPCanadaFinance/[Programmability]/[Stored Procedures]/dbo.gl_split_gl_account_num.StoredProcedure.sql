USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[gl_split_gl_account_num]    Script Date: 06/07/2017 09:17:22 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[gl_split_gl_account_num]
  @pGlAccountNum		varchar(50),
  @pLegalEntity			varchar(50) OUTPUT, 
  @pNaturalAccount		varchar(50) OUTPUT,
  @pSubAccount		varchar(50) OUTPUT,
  @pProductLineDept		varchar(50) OUTPUT,
  @pLanguageMarket		varchar(50) OUTPUT,
  @pChannel			varchar(50) OUTPUT,
  @pSegment7			varchar(50) OUTPUT
AS

-- =~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~
-- Description: 
-- This procedure takes in a full General Ledger account number and returns 
-- (via OUTPUT parameters) it's various components.
-- 
-- Revision History:
-- June 2004 - Joshua Caesar 
-- Inital Revision based upon om_proc_split_gl_acc_num from previous Oracle system.
-- =~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~

DECLARE @sString varchar(50)
DECLARE @iPosition int
DECLARE @Seperator varchar(1) 

-- Setup intial values
------------------------------------------
SELECT
	@Seperator			= '.',
	@sString			= @pGlAccountNum,
	@pLegalEntity		= NULL, 
	@pNaturalAccount	= NULL, 
	@pSubAccount		= NULL,
	@pProductLineDept	= NULL,
	@pLanguageMarket	= NULL,
	@pChannel			= NULL,
	@pSegment7			= NULL;
	
-- populate each OUT variable based upon it's position in the input string
--------------------------------------------------------------------------
SELECT @iPosition = CHARINDEX(@Seperator, @sString);
IF @iPosition = 0 
begin
	SELECT @pLegalEntity = @sString;
	return (0);
end
SELECT @pLegalEntity = substring(@sString, 1, @iPosition - 1);
SELECT @sString = substring(@sString, @iPosition + 1, len(@sString) - @iPosition);

SELECT @iPosition = CHARINDEX(@Seperator, @sString);
IF @iPosition = 0 
begin
	SELECT @pNaturalAccount = @sString;
	return (0);
end
SELECT @pNaturalAccount = substring(@sString, 1, @iPosition - 1);
SELECT @sString = substring(@sString, @iPosition + 1, len(@sString) - @iPosition);

SELECT @iPosition = CHARINDEX(@Seperator, @sString);
IF @iPosition = 0 
begin
	SELECT @pSubAccount = @sString;
	return (0);
end
SELECT @pSubAccount = substring(@sString, 1, @iPosition - 1);
SELECT @sString = substring(@sString, @iPosition + 1, len(@sString) - @iPosition);

SELECT @iPosition = CHARINDEX(@Seperator, @sString);
IF @iPosition = 0 
begin
	SELECT @pProductLineDept = @sString;
	return (0);
end
SELECT @pProductLineDept = substring(@sString, 1, @iPosition - 1);
SELECT @sString = substring(@sString, @iPosition + 1, len(@sString) - @iPosition);

SELECT @iPosition = CHARINDEX(@Seperator, @sString);
IF @iPosition = 0 
begin
	SELECT @pLanguageMarket = @sString;
	return (0);
end
SELECT @pLanguageMarket = substring(@sString, 1, @iPosition - 1);
SELECT @sString = substring(@sString, @iPosition + 1, len(@sString) - @iPosition);

SELECT @iPosition = CHARINDEX(@Seperator, @sString);
IF @iPosition = 0 
begin
	SELECT @pChannel = @sString;
	return (0);
end
SELECT @pChannel = substring(@sString, 1, @iPosition - 1);
SELECT @sString = substring(@sString, @iPosition + 1, len(@sString) - @iPosition);

SELECT @iPosition = CHARINDEX(@Seperator, @sString);
IF @iPosition = 0 
begin
	SELECT @pSegment7 = @sString;
	return (0);
end
SELECT @pSegment7 = substring(@sString, 1, @iPosition - 1);
GO
