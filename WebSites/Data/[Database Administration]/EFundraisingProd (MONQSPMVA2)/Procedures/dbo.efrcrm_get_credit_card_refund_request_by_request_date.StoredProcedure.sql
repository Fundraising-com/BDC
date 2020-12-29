USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_credit_card_refund_request_by_request_date]    Script Date: 02/14/2014 13:04:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[efrcrm_get_credit_card_refund_request_by_request_date]
@fromDate DATETIME,
@toDate DATETIME,   
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
		WHERE DATEDIFF(day, @fromDate, ccrr.request_date) >= 0 AND DATEDIFF(day, ccrr.request_date, @toDate) >= 0 OR ccrr.cancelled = 1
		ORDER BY ccrr.request_date, ccrr.sale_id

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
		WHERE DATEDIFF(day, @fromDate, ccrr.request_date) >= 0 AND DATEDIFF(day, ccrr.request_date, @toDate) >= 0 AND ccrr.cancelled = 0
		ORDER BY ccrr.request_date, ccrr.sale_id
								
	END
GO
