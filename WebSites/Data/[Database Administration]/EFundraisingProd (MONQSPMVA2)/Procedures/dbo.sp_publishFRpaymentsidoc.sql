SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Javier Arellano
-- Create date: December 4, 2014
-- Description:	Updates the payment id and the sales external id with the given status
-- =============================================
CREATE PROCEDURE sp_publishFRpaymentsidoc
	@salesId INT,
	@paymentNo INT, 
	@paymentStatus INT,
	@salesExternalStatus INT
AS
BEGIN
	UPDATE payment
	SET payment_status_id = @paymentStatus
	WHERE sales_id = @salesId
	AND payment_no = @paymentNo;
	
	UPDATE sale
	SET ext_sales_status_id = @salesExternalStatus
	WHERE sales_id = @salesId;
END
GO
--grant exec on sp_publishFRpaymentsidoc to db_stored_proc_exec 