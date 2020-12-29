USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_param_email_397]    Script Date: 02/14/2014 13:05:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
-- ==========================================
--  Author: Philippe Girard
--	ALTER  date: 2008/02/07
--	Description: Get param for biz rule 128
-- ==========================================
*/
CREATE  PROCEDURE [dbo].[es_get_param_email_397]
	@identification int
AS
BEGIN
	SELECT 0 as source_id
		  , 397 as email_template_id
		  , @identification as identification
		  , a.account_name
		  , fsm.first_name + ' ' + fsm.last_name as fsm
		  , ea.food_account_id as food_acct_id
	FROM external_account ea with(nolock)
		INNER JOIN QSPFulfillment..account a with(nolock)
			ON a.account_id = ea.food_account_id
		INNER JOIN QSPFulfillment..field_sales_manager fsm with(nolock)
			ON fsm.fm_id = a.fm_id
           AND fsm.[deleted] = 0
	WHERE ea.touch_id = @identification
END
GO
