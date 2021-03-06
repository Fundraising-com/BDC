USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[cm_generate_random_password]    Script Date: 02/14/2014 13:05:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[cm_generate_random_password]
    @length_password int = 8
    ,@password_type char(7) = 'simple'
    ,@password varchar(25) OUTPUT
AS
BEGIN
    DECLARE @type tinyint
    DECLARE @bitmap char(6)
    
    SET @password='' 
    SET @bitmap = 'uaeioy' 
    --@bitmap contains all the vowels, which are a, e, i, o, u and y. 
    --These vowels are used to generate slightly readable/rememberable simple passwords

    WHILE @length_password > 0
    BEGIN
    	IF @password_type = 'simple' --Generating a simple password
    	BEGIN
    	IF (@length_password%2) = 0  --Appending a random vowel to @password
    		
    		SET @password = @password + SUBSTRING(@bitmap,CONVERT(int,ROUND(1 + (RAND() * (5)),0)),1)
    	ELSE --Appending a random alphabet
    		SET @password = @password + CHAR(ROUND(97 + (RAND() * (25)),0))
    		
    	END
    	ELSE --Generating a complex password
    	BEGIN
    		SET @type = ROUND(1 + (RAND() * (3)),0)
    
    		IF @type = 1 --Appending a random lower case alphabet to @password
    			SET @password = @password + CHAR(ROUND(97 + (RAND() * (25)),0))
    		ELSE IF @type = 2 --Appending a random upper case alphabet to @password
    			SET @password = @password + CHAR(ROUND(65 + (RAND() * (25)),0))
    		ELSE IF @type = 3 --Appending a random number between 0 and 9 to @password
    			SET @password = @password + CHAR(ROUND(48 + (RAND() * (9)),0))
    		ELSE IF @type = 4 --Appending a random special character to @password
    			SET @password = @password + CHAR(ROUND(33 + (RAND() * (13)),0))
    	END
    
    	SET @length_password = @length_password - 1
    END
    
    RETURN 0
END
GO
