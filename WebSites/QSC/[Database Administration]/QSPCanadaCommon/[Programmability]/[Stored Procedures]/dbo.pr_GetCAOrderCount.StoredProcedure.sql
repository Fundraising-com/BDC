USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_GetCAOrderCount]    Script Date: 06/07/2017 09:33:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_GetCAOrderCount]

	@CampaignID	INT

AS

DECLARE @Cnt INT

SELECT	COUNT(OrderID) AS CNT
FROM	QSPCanadaOrderManagement..Batch b
WHERE	b.CampaignID = @CampaignID
AND		b.StatusInstance <> 40005
AND		b.OrderQualifierID IN (39001, 39002, 39003, 39009)
GO
