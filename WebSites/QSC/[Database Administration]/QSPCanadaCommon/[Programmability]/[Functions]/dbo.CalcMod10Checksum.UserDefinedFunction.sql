USE [QSPCanadaCommon]
GO
/****** Object:  UserDefinedFunction [dbo].[CalcMod10Checksum]    Script Date: 06/07/2017 09:33:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION [dbo].[CalcMod10Checksum]
(@sP varchar(10))
RETURNS int AS
BEGIN

	DECLARE	@sTot varchar(20),
		@i int,
		@iMult int,
		@iTot bigint,
		@iMod int,
		@iDigit int
	
	SELECT	@sTot = '',
		@iMult = 2,
		@iTot = 0

	-- build the string of digits
	SET @i = Len(@sP)
	WHILE (@i > 0)
	BEGIN
		SET @iDigit = Cast(SubString(@sP, @i, 1) as int)
		SET @sTot = @sTot + Cast((@iDigit * @iMult) as varchar)
		SET @i = @i - 1
		SET @iMult = Abs(@iMult - 3) --alternate between values 1 and 2
	END
	--DEBUG: PRINT '@sTot = ' + @sTot
	
	-- now sum the string of digits
	SET @i = Len(@sTot)
	WHILE (@i > 0)
	BEGIN
		SET @iTot = @iTot + Cast(Substring(@sTot, @i, 1) as int)
		SET @i = @i - 1
	END 
	--DEBUG: PRINT '@iTot = ' + cast(@iTot as varchar)
	
	-- get the check digit
	SET @iMod = (10 - (@iTot % 10)) % 10
	--DEBUG: PRINT '@iMod = ' + cast(@iMod as varchar)
	
	RETURN @iMod

END
GO
