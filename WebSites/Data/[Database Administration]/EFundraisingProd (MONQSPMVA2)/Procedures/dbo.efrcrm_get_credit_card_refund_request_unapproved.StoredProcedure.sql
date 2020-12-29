USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_credit_card_refund_request_unapproved]    Script Date: 02/14/2014 13:04:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[efrcrm_get_credit_card_refund_request_unapproved]
AS

	SELECT credit_card_refund_request_id, 
			sale_id, 
			bpps_id, 
			request_date, 
			status_code, 
			refund_amount, 
			processed, 
			credit_card_type_id, 
			cancelled
	FROM credit_card_refund_request
	WHERE cancelled = 0 AND processed = 0 AND status_code != '100'
GO
