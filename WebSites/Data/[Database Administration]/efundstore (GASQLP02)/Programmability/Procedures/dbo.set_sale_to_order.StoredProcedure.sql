USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[set_sale_to_order]    Script Date: 02/14/2014 13:06:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[set_sale_to_order]
	@intOrderID INT
	, @intSalesID INT
AS
INSERT efundstore..orders_sale (
	order_id	
	, sales_id
)
VALUES (
	@intOrderID
	, @intSalesID
)
IF @@ERROR = 0
	RETURN( 0 )
ELSE
	RETURN( -1 )
GO
