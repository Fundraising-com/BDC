USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_Campaign_SelectCampaignsRunningGiftAndCookieDough]    Script Date: 06/07/2017 09:19:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Campaign_SelectCampaignsRunningGiftAndCookieDough]

AS

SELECT		fm.FirstName + ' ' + fm.LastName FM, acc.ID AccountID, acc.Name AccountName, camp.ID CampaignID, camp.StartDate, camp.EndDate, campLY.ID LastYearCampaignID, ly.LastYearNetSale
FROM		Campaign camp
JOIN		CampaignProgram cpDough ON cpDough.CampaignID = camp.ID
JOIN		CampaignProgram cpGift ON cpGift.CampaignID = camp.ID
JOIN		CAccount acc ON acc.ID = camp.BillToAccountID
JOIN		FieldManager fm ON fm.FMID = camp.FMID
JOIN		Season seasLY ON DATEADD(yy, -1, camp.StartDate) BETWEEN seasLY.StartDate AND seasLY.EndDate AND seasLY.Season IN ('Y')
LEFT JOIN	(Campaign campLY
	JOIN	CampaignProgram cpDoughLY ON cpDoughLY.CampaignID = campLY.ID AND cpDoughLY.ProgramID = 44 AND cpDoughLY.DeletedTF = 0
	JOIN	CampaignProgram cpGiftLY ON cpGiftLY.CampaignID = campLY.ID AND cpGiftLY.ProgramID IN (53, 55, 59, 67, 68, 69) AND cpGiftLY.DeletedTF = 0)
				ON campLY.BillToAccountID = acc.ID AND campLY.StartDate BETWEEN seasLY.StartDate AND seasLY.EndDate AND campLY.Status = 37002 AND cpGiftLY.ProgramID = cpGift.ProgramID
LEFT JOIN	(SELECT		v.CampaignID, SUM(ISNULL(v.NetSale, 0)) LastYearNetSale
			FROM		QSPCanadaFinance..vw_GetNetForReporting v
			JOIN		QSPCanadaProduct..ProgramSectionType pst WITH (NOLOCK) ON pst.ID = v.section_type_id
			WHERE		pst.ID IN (1,2,9,10,11,13,14,15,16) -- Gift, Mag, CD, Jewelry, Candle, Trt, Entertainment
			GROUP BY	v.campaignid) ly ON ly.CampaignID = campLY.ID
WHERE		camp.StartDate >= '2017-07-01'
AND			camp.Status = 37002
AND			cpDough.ProgramID IN (44)
AND			cpDough.DeletedTF = 0
AND			cpDough.OnlineOnly = 0
AND			cpGift.ProgramID IN (53, 55, 59, 67, 68, 69)
AND			cpGift.DeletedTF = 0
AND			cpGift.OnlineOnly = 0
AND			camp.OnlineOnlyPrograms = 0
AND			ISNULL(camp.Notes,'') NOT LIKE '%EXCEPTION%'
AND			ISNULL(ly.LastYearNetSale, 0.00) < 6000
ORDER BY	fm.FirstName + ' ' + fm.LastName,
			acc.ID
GO
