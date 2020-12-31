USE [QSPCanadaFinance]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_instring]    Script Date: 06/07/2017 09:17:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION  [dbo].[UDF_instring]  (
	@input varchar(100),
	@lookfor varchar(1),
	@position int
	
)
RETURNS int  AS  
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  Joshua Caesar
-- mimics the instr function available in some databases
-- given a string and a char, returns the position where the xth occurence of that char is
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
BEGIN

	DECLARE @location  int
	DECLARE @prev int
	DECLARE @count int
	SELECT @location = 0, @prev = 0, @count = 0
	SELECT @location =CHARINDEX(@lookfor,@input,0)
	while( (@location <> @prev) AND (@location <> 0) )
	begin
		select @prev = @location, @count = @count + 1
		SELECT @location = CHARINDEX(@lookfor,@input,@prev + 1)
		if(@count = @position)
		begin
			return @prev
		end
	end

	DECLARE @RETURNVAL INT
	
	IF ( @count < @position ) 
	BEGIN
		set  @RETURNVAL = 0;
	END
	ELSE
	BEGIN
		set @RETURNVAL = @prev;
	END

	return @RETURNVAL;
end
GO
