USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[count_cart_items]    Script Date: 02/14/2014 13:05:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	JF Lavigne
Created On:	May 18, 2004
Description:	This stored procedure gets the number of items in a cart
*/
CREATE PROCEDURE [dbo].[count_cart_items] 
	@intCartID INT
AS
SELECT
	SUM( quantity ) AS total_quantity
FROM	dbo.shopping_cart_items
WHERE
	shopping_cart_id = @intCartID
GO
