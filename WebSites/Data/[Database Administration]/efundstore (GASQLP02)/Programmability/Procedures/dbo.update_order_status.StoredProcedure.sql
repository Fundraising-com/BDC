USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[update_order_status]    Script Date: 02/14/2014 13:06:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	Paolo De Rosa
Created On:	June 8, 2004
Description:	This stored procedure tests whether an order has been
		converted into a sale.  If it is, the order_submitted
		flag is set to true.
*/
CREATE PROCEDURE [dbo].[update_order_status]
	@intOrderID INT
AS
DECLARE @intErrorCode INT
SET @intErrorCode = @@ERROR

BEGIN TRANSACTION

IF EXISTS(
	SELECT * 
	FROM orders_sale
	WHERE order_id = @intOrderID
)
BEGIN
	IF @intErrorCode = 0
	BEGIN
		UPDATE orders
		SET order_submitted = 1
		WHERE order_id = @intOrderID
		SET @intErrorCode = @@ERROR
	END
END
/*ELSE
BEGIN
	-- do something
END*/

IF @intErrorCode = 0
BEGIN
	COMMIT TRANSACTION
	RETURN( 0 )
END
ELSE
BEGIN
	ROLLBACK TRANSACTION
	RETURN( -1 )
END
GO
