USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetMaxPaymentID]    Script Date: 06/07/2017 09:20:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE  PROCEDURE [dbo].[sp_GetMaxPaymentID]
	@d datetime
AS

Declare @id int
Declare @date datetime

SELECT @date = paymentdate, @id = (isnull(max(paymentid),1)) from PaymentBatch where paymentdate=@d and paymentid between 1000 and 19999 group by paymentdate

If (@id is NULL)
Begin
	Select paymentdate = @d, paymentid = 1000
End
ELSE
Begin
	SELECT paymentdate, isnull(max(paymentid),1000) as paymentid from PaymentBatch where paymentdate=@d and paymentid between 1000 and 19999 group by paymentdate
End
GO
