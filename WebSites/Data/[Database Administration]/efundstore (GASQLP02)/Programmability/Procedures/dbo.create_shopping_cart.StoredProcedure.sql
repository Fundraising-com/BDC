USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[create_shopping_cart]    Script Date: 02/14/2014 13:05:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	JF Lavigne
Created On:	May 11, 2004
Description:	This stored procedure adds an entry into the shopping_cart table and returns the shopping_cart_id
*/
CREATE PROCEDURE [dbo].[create_shopping_cart]
	@intVisitorLogID INT
AS
SET NOCOUNT ON
INSERT INTO shopping_cart (
	visitor_log_id
	, shopping_cart_code
)
VALUES (
	@intVisitorLogID
	, 'F'
)
IF @@ERROR = 0
	RETURN( @@IDENTITY )
ELSE
	RETURN( -1 )
GO
