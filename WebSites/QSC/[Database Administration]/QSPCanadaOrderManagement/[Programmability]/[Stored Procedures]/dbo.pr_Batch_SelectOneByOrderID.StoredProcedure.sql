USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Batch_SelectOneByOrderID]    Script Date: 06/07/2017 09:19:44 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_Batch_SelectOneByOrderID] 

	@iOrderID int

AS

SELECT	Convert(varchar(10), Batch.Date, 101) AS Date,
	    ID,
		campaignID,
		accountID,
		statusInstance,
		OrderQualifierID,
		OrderDeliveryDate
FROM	batch
WHERE	orderID = @iOrderID
GO
