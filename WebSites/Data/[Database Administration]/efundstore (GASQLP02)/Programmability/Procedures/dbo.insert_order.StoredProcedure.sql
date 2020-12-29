USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[insert_order]    Script Date: 02/14/2014 13:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	Paolo De Rosa
Created On:	June 1, 2004
Description:	This stored procedure inserts an order into the orders table.
*/
CREATE PROCEDURE [dbo].[insert_order]
	@intShoppingCartID INT
	, @strCultureName VARCHAR(10)
	, @numOrderTotal NUMERIC(9, 2)
	, @numShippingTotal NUMERIC(9, 2)
	, @numTaxTotal NUMERIC(9, 2)
	, @intRandomNumber INT OUTPUT
AS
SET NOCOUNT ON

DECLARE @intOrderID INT
DECLARE @intCultureID INT
DECLARE @intErrorCode INT

SET @intErrorCode = @@ERROR 

IF @intErrorCode = 0
BEGIN
	SET @intCultureID = (
		SELECT culture_id
		FROM cultures 
		WHERE LOWER( culture_name ) = LTRIM( RTRIM( LOWER ( @strCultureName ) ) )
	)
	SET @intErrorCode = @@ERROR 
END

IF @intErrorCode = 0
BEGIN
	INSERT INTO orders (
		shopping_cart_id
		, online_user_id
		, credit_card_id
		, culture_id
		, order_total
		, shipping_total
		, tax_total
	)
	SELECT 
		sc.shopping_cart_id 
		, sc.online_user_id
		, cc.credit_card_id
		, @intCultureID
		, @numOrderTotal
		, @numShippingTotal
		, @numTaxTotal 
	FROM	shopping_cart sc
		INNER JOIN online_users ou
			ON sc.online_user_id = ou.online_user_id 
		INNER JOIN credit_cards cc
			ON ou.online_user_id = cc.online_user_id 
		INNER JOIN (
			SELECT 
				cc.online_user_id
				, MAX( cc.date_created ) AS LastestCreditCard
			FROM 	credit_cards cc
			GROUP BY
				cc.online_user_id
		) v
			ON sc.online_user_id = v.online_user_id
			 AND cc.date_created = v.LastestCreditCard
	WHERE
		shopping_cart_id = @intShoppingCartID
	SET @intErrorCode = @@ERROR 
END

IF @intErrorCode = 0
BEGIN
	SET @intOrderID = @@IDENTITY
	SET @intRandomNumber = (
		SELECT random_number
		FROM orders
		WHERE order_id = @intOrderID
	)
	RETURN( @intOrderID )
END
ELSE
	RETURN( -1 )
GO
