USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[validate_email]    Script Date: 02/14/2014 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	jf lavigne
Created On:	June 9, 2004
Description:	Check if email entered already exists
*/
CREATE PROCEDURE [dbo].[validate_email]
	@strEmail VARCHAR(75)
AS
IF EXISTS (
	SELECT *
	FROM 	online_users 
	WHERE 
		email = @strEmail
)
	RETURN 1
ELSE
	RETURN 0

/*
SELECT online_user_id
FROM 	online_users 
WHERE 
	email = @strEmail
*/
GO
