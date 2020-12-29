USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[update_cart_item_quantity]    Script Date: 02/14/2014 13:06:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	JF Lavigne
Created On:	May 13, 2004
Description:	This stored procedure adds an entry into the table shopping_cart_item
*/
CREATE PROCEDURE [dbo].[update_cart_item_quantity] 
	@intCartID INT
	, @intProductID INT
	, @intQuantity SMALLINT
AS
DECLARE @intErrorCode INT
SET @intErrorCode = @@ERROR
SET NOCOUNT ON

IF @intErrorCode = 0
BEGIN
	UPDATE 
		shopping_cart_items 
	SET 	quantity = @intQuantity 
	WHERE
		shopping_cart_id = @intCartID 
	 AND	 scratch_book_id = @intProductID
	SET @intErrorCode = @@ERROR
END

RETURN( @intErrorCode )
GO
