USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[authenticate_user]    Script Date: 02/14/2014 13:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	Sam Abdel-Malek 
Created On:	June 1, 2004
Description:	Check if user name and password are valid
*/
CREATE PROCEDURE [dbo].[authenticate_user]
	@strEmail VARCHAR(75)
	, @strPassword VARBINARY(30)
AS
IF EXISTS (
	SELECT *
	FROM 	online_users 
	WHERE 
		email = @strEmail
	 AND 	online_user_pwd = @strPassword
)
	RETURN 0
ELSE
	RETURN 1

/*
DECLARE @intClientID INT

SELECT 
	@intClientID = client_id 
FROM 	online_users 
WHERE 
	email = @strEmail
 AND 	online_user_pwd = @strPassword

IF( @intClientID IS NULL )
	RETURN 0
ELSE
	RETURN 1
*/
GO
