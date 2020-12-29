USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[get_online_user_info]    Script Date: 02/14/2014 13:06:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	JF Lavigne
Created On:	june 8, 2004
Description:	this gets all the info for an online user (personal info plus order info)
*/
--[dbo].[get_online_user_info] 100,'en-us'
CREATE      PROCEDURE [dbo].[get_online_user_info] 
	@intOnlineUserID INT
	, @strCultureName VARCHAR(10)
AS
-- get user's shipping information
EXEC dbo.get_online_user_shipping_info @intOnlineUserID

-- get shopping cart items
EXEC dbo.get_cart_items @intOnlineUserID, @strCultureName

-- get rest of the user's information
SELECT
	o.order_number
	, cct.credit_card_type_name as card_type
	, CASE
		WHEN cc.month_expire < 10 THEN '0' + CONVERT( VARCHAR(2), cc.month_expire ) + '/' + CONVERT( VARCHAR(4), cc.year_expire )
		ELSE CONVERT( VARCHAR(2), cc.month_expire ) + '/' + CONVERT( VARCHAR(4), cc.year_expire )
	  END AS exp_date
	, cc.last_digits
	, cc.credit_card
	, o.tax_total
	, o.order_total
	, o.shipping_total
        , convert(char(12),ou.date_created,109)as date_created
FROM
	dbo.orders o 
	INNER JOIN dbo.credit_cards cc 
		ON o.credit_card_id = cc.credit_card_id 
	INNER JOIN dbo.credit_card_types cct 
		ON cc.credit_card_type_id = cct.credit_card_type_id 
	INNER JOIN dbo.online_users ou 
		ON o.online_user_id = ou.online_user_id
WHERE
	o.online_user_id =  @intOnlineUserID

--get rest of the user's information (we know the replication has been done)
EXEC dbo.get_online_user_registered_info @intOnlineUserID, @strCultureName
GO
