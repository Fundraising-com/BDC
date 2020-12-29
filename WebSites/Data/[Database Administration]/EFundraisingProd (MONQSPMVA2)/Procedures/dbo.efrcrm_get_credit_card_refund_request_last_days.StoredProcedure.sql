USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_credit_card_refund_request_last_days]    Script Date: 02/14/2014 13:04:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[efrcrm_get_credit_card_refund_request_last_days]
@days INT,
@cancelled BIT = 0        
AS

	IF @cancelled = 1
	BEGIN

		SELECT credit_card_refund_request_id, 
			ccrr.sale_id, 
			ccrr.bpps_id, 
			ccrr.request_date, 
			ccrr.status_code, 
			ccrrs.description,
			ccrr.refund_amount, 
			ccrr.processed, 
			ccrr.credit_card_type_id, 
			cct.credit_card_type_name,
			ccrr.cancelled
		FROM  dbo.credit_card_refund_request ccrr
		INNER JOIN dbo.credit_card_refund_request_status ccrrs ON ccrr.status_code = ccrrs.status_code
		INNER JOIN dbo.credit_card_types cct ON ccrr.credit_card_type_id = cct.credit_card_type_id
		WHERE ccrr.request_date > DATEADD(dd, @days, GETDATE()) OR ccrr.cancelled = 1

	END
	ELSE
	BEGIN

		SELECT credit_card_refund_request_id, 
			ccrr.sale_id, 
			ccrr.bpps_id, 
			ccrr.request_date, 
			ccrr.status_code, 
			ccrrs.description,
			ccrr.refund_amount, 
			ccrr.processed, 
			ccrr.credit_card_type_id, 
			cct.credit_card_type_name,
			ccrr.cancelled
		FROM  dbo.credit_card_refund_request ccrr
		INNER JOIN dbo.credit_card_refund_request_status ccrrs ON ccrr.status_code = ccrrs.status_code
		INNER JOIN dbo.credit_card_types cct ON ccrr.credit_card_type_id = cct.credit_card_type_id
		WHERE ccrr.request_date > DATEADD(dd, @days, GETDATE()) AND ccrr.cancelled = 0
								
	END
GO
