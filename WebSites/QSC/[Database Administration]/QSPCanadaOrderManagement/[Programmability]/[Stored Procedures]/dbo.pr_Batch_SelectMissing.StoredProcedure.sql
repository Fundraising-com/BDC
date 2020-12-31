USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Batch_SelectMissing]    Script Date: 06/07/2017 09:19:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Batch_SelectMissing]

AS

DECLARE	@SeasonStartDate 	DATETIME
DECLARE	@SeasonEndDate   	DATETIME

SELECT	@SeasonStartDate = Startdate,
		@SeasonEndDate = Enddate
FROM	QSPCanadaCommon..Season
WHERE	GETDATE() BETWEEN StartDate AND EndDate
AND		Season <> 'Y'

SELECT	ost.OrderID,
		ost.CampaignID,
		ost.GroupID,
		ost.GroupName,
		ost.FMID
FROM	OrderStageTracking ost
WHERE	ost.Stage = 59005
AND		OrderID NOT IN (SELECT OrderID FROM Batch)
AND		OrderID NOT IN (SELECT OrderID FROM OrderStageTracking WHERE Stage = 59008)
AND		TransmitDate <= DATEADD(DAY, -2, GETDATE())
AND		StageDate >= @SeasonStartDate
GO
