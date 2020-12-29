USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[cc_check_cashflow]    Script Date: 02/14/2014 13:04:51 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
create PROCEDURE [dbo].[cc_check_cashflow] 
	@intCheckPeriod INT
AS


SELECT
	 [name]
	,paid_amount
	,cheque_number
FROM	 payment
WHERE
	payment_period_id = @intCheckPeriod
ORDER BY
	cheque_number
GO
