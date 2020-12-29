USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[validate_promotion]    Script Date: 02/14/2014 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	JP Lahaie
Created On:	May 14, 2004
Description:	This stored procedure ?
*/
CREATE PROCEDURE [dbo].[validate_promotion]
	@intPromotionID INT
AS
/* TODO: point to the local version of the promotion table */
	SELECT
		promotion_id 
	FROM	promotion 
	WHERE
		promotion_id = @intPromotionId
GO
