USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[update_cart_with_user_info]    Script Date: 02/14/2014 13:06:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	jf lavigne
Created On:	June 4, 2004
Description:	This store procedure updates a cart with a user
*/
CREATE PROCEDURE [dbo].[update_cart_with_user_info]
	@intCartID INT
	, @intOnlineUserID INT
AS
UPDATE 
	shopping_cart
SET 	online_user_id = @intOnlineUserID
WHERE 
	shopping_cart_id = @intCartID
GO
