USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_SwitchLetterBatch_SelectBySub]    Script Date: 06/07/2017 09:20:38 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_SwitchLetterBatch_SelectBySub]

	@iCustomerOrderHeaderInstance		int,
	@iTransID				int

AS

	SELECT	slb.Instance,
			slb.ProductCode,
			slb.DateCreated,
			slb.UserID,
			slb.Reason
	FROM		SwitchLetterBatch slb,
			SwitchLetterBatchCustomerOrderDetail slbcod
	WHERE	slbcod.SwitchLetterBatchInstance = slb.Instance
	AND		slbcod.CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
	AND		slbcod.TransID = @iTransID
	ORDER BY	slb.DateCreated
GO
