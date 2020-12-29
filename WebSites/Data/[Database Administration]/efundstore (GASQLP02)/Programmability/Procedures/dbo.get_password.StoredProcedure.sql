USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[get_password]    Script Date: 02/14/2014 13:06:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	Sam Abdel-Malek 
Created On:	June 1, 2004
Description:	Check if user name and password are valid
*/
CREATE PROCEDURE [dbo].[get_password]
	@strEmail VARCHAR(75)
AS
SELECT 
	online_user_pwd
	, online_user_id
	, client_id
FROM 	online_users 
WHERE 
	email = @strEmail
GO
