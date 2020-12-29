USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_credit_card_refund_request]    Script Date: 02/14/2014 13:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--[dbo].[efrcrm_insert_credit_card_refund_request] 69747,111,null,10,''
CREATE PROC [dbo].[efrcrm_update_credit_card_refund_request]
@credit_card_refund_request_id INT,
@sale_id INT,
@bpps_id INT,
@request_date DATETIME = null,
@refund_amount MONEY,
@status_code CHAR(3),
@processed BIT,
@credit_card_type_id TINYINT,
@cancelled BIT
AS

	IF @request_date is null
	   SET @request_date = GETDATE()

	UPDATE dbo.credit_card_refund_request
		SET sale_id = @sale_id,
			bpps_id = @bpps_id,
			request_date = @request_date,
			refund_amount = @refund_amount,
			status_code = @status_code,
			processed = @processed,
			credit_card_type_id = @credit_card_type_id,
			cancelled = @cancelled
	WHERE credit_card_refund_request_id = @credit_card_refund_request_id
GO
