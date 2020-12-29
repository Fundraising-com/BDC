USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[add_cart_item]    Script Date: 02/14/2014 13:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	JF Lavigne
Created On:	May 12, 2004
Description:	This stored procedure adds an entry into the table shopping_cart_item
*/
CREATE PROCEDURE [dbo].[add_cart_item] 
	@intCartID INT
	, @intProductID INT
	, @intQuantity SMALLINT
	, @intCarrier TINYINT = NULL
	, @intShippingOptionID TINYINT = NULL 
	, @strClientUploadedImg VARCHAR(50) = NULL
	, @strGroupName VARCHAR(50) = NULL
AS
DECLARE @intErrorCode INT
SET @intErrorCode = @@ERROR
SET NOCOUNT ON

IF EXISTS (
	SELECT * 
	FROM shopping_cart_items
	WHERE
		scratch_book_id = @intProductID
	 AND	shopping_cart_id = @intCartID
) 
BEGIN
	IF @intErrorCode = 0
	BEGIN
		UPDATE
			shopping_cart_items 
		SET 	quantity = @intQuantity
		WHERE
			scratch_book_id = @intProductID
		 AND	shopping_cart_id = @intCartID
		SET @intErrorCode = @@ERROR
	END
END
ELSE
BEGIN
	IF @intErrorCode = 0
	BEGIN
		INSERT INTO shopping_cart_items ( 
			shopping_cart_id
			, scratch_book_id
			, carrier_id
			, shipping_option_id 
			, quantity
			, client_uploaded_img
			, group_name
		) 
		SELECT
			@intCartID
			, p.scratch_book_id
			, ac.carrier_id
			, ac.shipping_option_id 
			, @intQuantity
			, @strClientUploadedImg
			, @strGroupName
		FROM 	dbo.scratch_book p 
			INNER JOIN dbo.product_class pc 
				ON pc.product_class_id = p.product_class_id
			INNER JOIN accounting_class ac
				ON ac.accounting_class_id = pc.accounting_class_id 
		WHERE 
			p.scratch_book_id = @intProductID
/*		VALUES ( 
			@intCartID
			, @intProductID
			, @intCarrier
			, @intShippingOptionID 
			, @intQuantity
			, @strClientUploadedImg
			, @strGroupName
		)
*/
		SET @intErrorCode = @@ERROR
	END
END
 
RETURN( @intErrorCode )
GO
