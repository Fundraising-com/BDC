USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[delete_cart_item]    Script Date: 02/14/2014 13:05:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	JF Lavigne
Created On:	May 12, 2004
Description:	This stored procedure deletes a specified item from the table shopping_cart_item
*/
CREATE  PROCEDURE [dbo].[delete_cart_item] 
	@intCartID INT
	, @intProductID INT 
AS 
DECLARE @intErrorCode INT
SET @intErrorCode = @@ERROR
SET NOCOUNT ON

IF @intErrorCode = 0
BEGIN
	DELETE 
	FROM shopping_cart_items 
	WHERE
		shopping_cart_id = @intCartID 
	 AND	scratch_book_id = @intProductID
	SET @intErrorCode = @@ERROR
END

RETURN( @intErrorCode )
GO
