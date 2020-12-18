USE [QSPFulfillment]
GO
/****** Object:  StoredProcedure [dbo].[pr_rpt_order_not_submitted]    Script Date: 05/19/2010 13:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[pr_rpt_order_not_submitted]
AS
BEGIN

	SELECT 
		s.source_name
		, order_id AS qsp_order_id
		, ot.order_type_name
		, os.order_status_name
		, c.account_id AS qsp_account_id
		, a.fulf_account_id AS eds_account_id
		, accs.account_status_name
		, a.fm_id
		, fsm.first_name
		, fsm.last_name
		, c.campaign_name
		, c.fiscal_year
		, c.deleted
		, o.create_date
		-- Instead of subracting adjustment_quantity, we should be adding adjustment_quantity as it is a negative number.
		, ( SELECT SUM((od.quantity + od.adjustment_quantity) * price) AS total FROM [order_detail] od WHERE od.order_id = o.order_id ) AS order_total
	FROM [ORDER] o
		INNER JOIN order_type ot
			ON ot.order_type_id = o.order_type_id
		INNER JOIN campaign c
			ON c.campaign_id = o.campaign_id
		INNER JOIN order_status os
			ON os.order_status_id = o.order_status_id
		INNER JOIN account a
			ON a.account_id = c.account_id
		INNER JOIN account_status accs
			ON accs.account_status_id = a.account_status_id
		INNER JOIN field_sales_manager fsm
			ON fsm.fm_id = a.fm_id
		INNER JOIN source s
			ON o.source_id = s.source_id
	WHERE 
		(
			o.source_id = 1
			AND c.program_type_id IN (7,11)
			AND c.fiscal_year >= 2009
			AND o.order_status_id NOT IN (1,5,9,109,309)
			AND o.deleted = 0
			AND fsm.deleted = 0
			AND (
				(o.order_type_id = 1 AND o.fulf_order_id IS NULL)
				OR (o.order_type_id = 3 AND o.order_status_id < 301)
				)
		)
		OR
		(
			o.source_id = 20
			AND c.fiscal_year >= 2010
			AND o.order_status_id NOT IN (1,5,9,109,309)
			AND o.deleted = 0
			AND fsm.deleted = 0
			AND o.order_type_id = 1 AND o.fulf_order_id IS NULL
		)
	GROUP BY
		order_id
		, ot.order_type_name
		, os.order_status_name
		, c.account_id
		, a.fulf_account_id
		, accs.account_status_name
		, a.fm_id
		, fsm.first_name
		, fsm.last_name
		, c.campaign_name
		, c.fiscal_year
		, c.deleted
		, o.create_date
		, s.source_name
		, o.source_id
-- Having clause is commented completely since the OE Sync does not check 
-- for source_id (1,20), so the report should not restrict itself
--
--	HAVING
--		o.source_id = 1
--		OR 
--			(
--				o.source_id = 20
-- Commented out TOTAL, since order with price $0 are not getting listed in this report (which are not sync'ed to AS400).
--				AND ( SELECT SUM((od.quantity + od.adjustment_quantity) * price) AS total FROM [order_detail] od WHERE od.order_id = o.order_id ) > 0
--			)
	ORDER BY 
		s.source_name DESC, 
		o.create_date DESC

END
