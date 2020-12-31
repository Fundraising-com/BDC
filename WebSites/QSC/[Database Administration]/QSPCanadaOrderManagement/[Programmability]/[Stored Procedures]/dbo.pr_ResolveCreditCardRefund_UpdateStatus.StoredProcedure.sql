USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_ResolveCreditCardRefund_UpdateStatus]    Script Date: 06/07/2017 09:20:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_ResolveCreditCardRefund_UpdateStatus]

	@zCreditCardNumber	varchar(25),
	@iRefundStatus		int

AS

UPDATE	ResolveCreditCardRefund
SET		RefundStatus = @iRefundStatus
WHERE	CreditCardNumber = @zCreditCardNumber
GO
