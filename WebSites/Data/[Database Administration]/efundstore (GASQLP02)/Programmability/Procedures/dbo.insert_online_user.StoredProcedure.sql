USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[insert_online_user]    Script Date: 02/14/2014 13:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	Paolo De Rosa
Created On:	June 4, 2004
Description:	This store procedure inserts an online user into online_users table
*/
CREATE  PROCEDURE [dbo].[insert_online_user]
	@intCartID INT
	, @strClientSeqCode CHAR(2)
	, @intClientID INT
	, @strEmail VARCHAR(75)
	, @strPassword 	VARBINARY(30)
	, @intVisitorsLogID INT
AS
SET NOCOUNT ON

DECLARE @intOnlineUserID INT
DECLARE @intErrorCode INT
SET @intErrorCode = @@ERROR

BEGIN TRANSACTION

IF @intErrorCode = 0
BEGIN
	INSERT INTO dbo.online_users (
		client_sequence_code
		, client_id
		, email
		, online_user_pwd
	)
	VALUES (
		@strClientSeqCode
		, @intClientID
		, @strEmail
		, @strPassword
	)
	SET @intErrorCode = @@ERROR
END

IF @intErrorCode = 0
BEGIN
	SET @intOnlineUserID = @@IDENTITY 
	UPDATE 
		shopping_cart
	SET 	online_user_id = @intOnlineUserID
	WHERE 
		shopping_cart_id = @intCartID
END

IF @intErrorCode = 0
	EXEC @intErrorCode = web_tracking.dbo.identify_visitor @intVisitorsLogID, 6, @intOnlineUserID

IF @intErrorCode = 0
BEGIN
	COMMIT TRANSACTION
	RETURN( @intOnlineUserID )
END
ELSE
BEGIN
	ROLLBACK TRANSACTION
	RETURN( @intErrorCode )
END
GO
