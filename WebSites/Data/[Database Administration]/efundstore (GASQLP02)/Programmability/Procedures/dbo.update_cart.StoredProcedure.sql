USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[update_cart]    Script Date: 02/14/2014 13:06:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	JF Lavigne
Created On:	May 13, 2004
Description:	This stored procedure updates an item in the shopping_cart_item table 
*/
CREATE  PROCEDURE [dbo].[update_cart] 
	@intCartID INT
	, @intProductID INT
	, @intQuantity SMALLINT = NULL
	, @intCarrierID TINYINT 
	, @intShippingOptionID TINYINT 
AS
DECLARE @intErrorCode INT
SET @intErrorCode = @@ERROR
SET NOCOUNT ON

IF @intErrorCode = 0
BEGIN
	UPDATE 
		shopping_cart_items 
	SET	carrier_id = @intCarrierID
		, shipping_option_id = @intShippingOptionID 
	WHERE
		shopping_cart_id = @intCartID
	 AND	scratch_book_id = @intProductID
	SET @intErrorCode = @@ERROR
END

RETURN( @intErrorCode )
GO
