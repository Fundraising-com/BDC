USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_Campaign_SalesBaseFMID_Update]    Script Date: 10/29/2017 2:31:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Campaign_SalesBaseFMID_Update]

AS

UPDATE		Campaign
SET			SalesBaseFMID = camp.NewSalesBaseFMID
FROM
(
	SELECT	camp.Id CampaignID, camp.SalesBaseFMID OriginalSalesBaseFMID,
			ISNULL((SELECT TOP 1 campCurrent.FMID SalesBaseFMID
					FROM	QSPCanadaCommon..Campaign campCurrent
					WHERE	campCurrent.BillToAccountID = camp.BillToAccountID
					AND		campCurrent.Status IN (37002, 37004)
					AND		campCurrent.StartDate < (	SELECT	s.EndDate
														FROM	QSPCanadaCommon..Season s
														WHERE	s.Season IN ('F', 'S')
														AND		DATEADD(yy, 1, camp.StartDate) BETWEEN s.StartDate AND s.EndDate
													)
					ORDER BY	campCurrent.StartDate DESC
				), camp.FMID) NewSalesBaseFMID
	FROM	Campaign camp
) camp
WHERE	ID = camp.CampaignID
AND		ISNULL(camp.OriginalSalesBaseFMID,'0000') <> camp.NewSalesBaseFMID
GO
