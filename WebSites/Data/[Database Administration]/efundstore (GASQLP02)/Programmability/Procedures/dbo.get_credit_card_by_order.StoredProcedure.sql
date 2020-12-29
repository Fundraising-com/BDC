USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[get_credit_card_by_order]    Script Date: 02/14/2014 13:06:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	Paolo De Rosa
Created On:	June 9, 2004
Description:	This stored procedure returns the credit card number that was used when 
		the passed order took place.  This information is used in the insert sale web service.
*/
CREATE PROCEDURE [dbo].[get_credit_card_by_order]
	@intOrderID INT
AS
SELECT 
	cc.credit_card
FROM	orders o
	INNER JOIN credit_cards cc
		ON o.online_user_id = cc.online_user_id
		 AND o.credit_card_id = cc.credit_card_id
WHERE
	o.order_id = @intOrderID
GO
