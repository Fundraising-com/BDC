USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_UpdateSubscriptionForChadd]    Script Date: 06/07/2017 09:20:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_UpdateSubscriptionForChadd]
	
	@iCustomerOrderHeaderInstance		int,
	@iTransID				int,
	@iCustomerInstance			int

AS

	UPDATE CustomerOrderDetail
	SET CustomerShipToInstance = @iCustomerInstance
	WHERE	CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
	AND		TransID = @iTransID
GO
