USE [QSPCanadaFinance]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_RemoveAccent]    Script Date: 06/07/2017 09:17:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Muhammad Siddiqui
-- Create date: March 25, 2008
-- Description:	Remove Accent and special characters from data supplied
-- =============================================
CREATE FUNCTION [dbo].[UDF_RemoveAccent] ( @CharData VARCHAR(60), @AlphaNumOnly int )
RETURNS VARCHAR(60) 
AS 
BEGIN

--SET @CharData=	'ÀÁÂÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ'	--	'Châtelaine'

DECLARE @AccentSpecialChar TABLE(Id INT, SpecialChar VARCHAR(2),ReplaceBy VARCHAR(2))

DECLARE  	@Index INT,
         		@ASCIIData VARCHAR(60),
         		@ASCIIChar INT,
         		@REPLACEBY VARCHAR(2),
	 	@SpecialChar Varchar(2)

SET @Index = 1
SET @ASCIIData = ''
SET @REPLACEBY = ''
SET @SpecialChar=''

WHILE @Index < 256
BEGIN
	IF
	      @Index in (138,140,142,146,154,156,158,159,161,255)
	  OR (@Index between 192 and 214)
	  OR (@Index between 217 and 221)
	  OR (@Index between 224 and 239)
	  OR (@Index between 241 and 246)
	  OR (@Index between 249 and 253)
	BEGIN
		INSERT INTO @AccentSpecialChar VALUES(@Index,CHAR(@Index),'')
	END
	SET @Index=@Index+1
END


UPDATE @AccentSpecialChar SET ReplaceBy = 'a' Where ID IN(224,225,226,227,228,229)
UPDATE @AccentSpecialChar SET ReplaceBy = 'A' Where ID IN(192,193,194,195,196,197)
UPDATE @AccentSpecialChar SET ReplaceBy = 'AE' Where ID =      198
UPDATE @AccentSpecialChar SET ReplaceBy = 'ae' Where ID =      230
UPDATE @AccentSpecialChar SET ReplaceBy = 'c' Where ID =       231
UPDATE @AccentSpecialChar SET ReplaceBy = 'C' Where ID =       199
UPDATE @AccentSpecialChar SET ReplaceBy = 'ce' Where ID=      156
UPDATE @AccentSpecialChar Set ReplaceBy = 'CE' Where ID=      140
UPDATE @AccentSpecialChar Set ReplaceBy = 'D' Where ID=       208
UPDATE @AccentSpecialChar Set ReplaceBy = 'e' Where ID IN(232,233,234,235)
UPDATE @AccentSpecialChar Set ReplaceBy = 'E' Where ID IN(200,201,202,203)
UPDATE @AccentSpecialChar Set ReplaceBy = 'i' Where ID IN(161,236,237,238,239)
UPDATE @AccentSpecialChar Set ReplaceBy = 'I' Where ID IN(204,205,206,207)
UPDATE @AccentSpecialChar Set ReplaceBy = 'n' Where ID=       241
UPDATE @AccentSpecialChar Set ReplaceBy = 'N' Where ID=       209
UPDATE @AccentSpecialChar Set ReplaceBy = 'o' Where ID IN(242,243,244,245,246)
UPDATE @AccentSpecialChar Set ReplaceBy = 'O' Where ID IN( 210,211,212,213,214)
UPDATE @AccentSpecialChar Set ReplaceBy = 'S' Where ID=       138
UPDATE @AccentSpecialChar Set ReplaceBy = 's' Where ID=       154
UPDATE @AccentSpecialChar Set ReplaceBy = 'u' Where ID IN(249,250,251,252)
UPDATE @AccentSpecialChar Set ReplaceBy = 'U' Where ID IN(217,218,219,220)
UPDATE @AccentSpecialChar Set ReplaceBy = 'Y' Where ID=       159
UPDATE @AccentSpecialChar Set ReplaceBy = 'Y' Where ID=       221
UPDATE @AccentSpecialChar Set ReplaceBy = 'y' Where ID IN(253,255)
UPDATE @AccentSpecialChar Set ReplaceBy = 'Z' Where ID=       142
UPDATE @AccentSpecialChar Set ReplaceBy = 'z' Where ID=       158
UPDATE @AccentSpecialChar Set ReplaceBy = '' Where ID= 146

--Select id,ASCII(SpecialChar),SpecialChar,ReplaceBY from @AccentSpecialChar

--Replace Accent Characters
SET @Index = 1

WHILE @Index < LEN(@CharData)+1
BEGIN
     SET @ASCIIChar=ASCII(SUBSTRING(@CharData, @Index, 1))

     SELECT @ReplaceBy=ReplaceBy, @SpecialChar=SpecialChar FROM @AccentSpecialChar WHERE ID =@ASCIIChar

     IF @@Rowcount > 0
     BEGIN
	SELECT @CharData= Replace (@CharData,@SpecialChar,@ReplaceBy)
     END
     SET @Index = @Index + 1
END


--Remove Special Charcters
SET @Index = 1
SET @ASCIIData = ''
 
IF ISNULL(@AlphaNumOnly, 0) = 0
BEGIN
  	WHILE @Index < LEN(@CharData)+1
    BEGIN
        SET @ASCIIChar=ASCII(SUBSTRING(@CharData, @Index, 1))
        --Restrict the result to A-Z and a-z or Numbers 0-9 or apostrophy,comma,period
        IF @ASCIIChar BETWEEN 48 and 57 or  
			@ASCIIChar BETWEEN 65 and 90 or 
			@ASCIIChar BETWEEN 97 and 122 or 
			@ASCIIChar IN (39,44,46)
        BEGIN

			SET @ASCIIData = @ASCIIData + CHAR(@ASCIIChar)
        END
        SET @Index = @Index + 1
	END
END
ELSE IF @AlphaNumOnly = 1 ---Only Alpha numeric
BEGIN
	WHILE @Index < LEN(@CharData)+1
	BEGIN
		SET @ASCIIChar=ASCII(SUBSTRING(@CharData, @Index, 1))
		--Restrict the result to A-Z and a-z or Numbers 0-9
		IF @ASCIIChar BETWEEN 48 and 57 or  
			@ASCIIChar BETWEEN 65 and 90 or 
			@ASCIIChar BETWEEN 97 and 122 
		BEGIN
  			SET @ASCIIData = @ASCIIData + CHAR(@ASCIIChar)
		END
		SET @Index = @Index + 1
	END
END
ELSE
BEGIN
WHILE @Index < LEN(@CharData)+1
	BEGIN
		SET @ASCIIChar=ASCII(SUBSTRING(@CharData, @Index, 1))
		--Restrict the result to A-Z and a-z or Numbers 0-9 or apostrophy,comma,period,space
		IF @ASCIIChar BETWEEN 48 and 57 or  
			@ASCIIChar BETWEEN 65 and 90 or 
			@ASCIIChar BETWEEN 97 and 122 or 
			@ASCIIChar IN (32,39,44,46)
		BEGIN

			SET @ASCIIData = @ASCIIData + CHAR(@ASCIIChar)
		END
        SET @Index = @Index + 1
	END
END
   
RETURN  @ASCIIData 
END
GO
