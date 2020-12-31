USE QSPCanadaCommon
GO

SELECT		DISTINCT fm.FirstName + ' ' + fm.LastName FM, acc.ID AccountID, acc.Name AccountName, camp.ID CampaignID, camp.StartDate, camp.EndDate, campLY.ID LastYearCampaignID
FROM		Campaign camp
JOIN		CampaignProgram cpDough ON cpDough.CampaignID = camp.ID
JOIN		CampaignProgram cpGift ON cpGift.CampaignID = camp.ID
JOIN		CAccount acc ON acc.ID = camp.BillToAccountID
JOIN		FieldManager fm ON fm.FMID = camp.FMID
LEFT JOIN	(Campaign campLY
	JOIN	CampaignProgram cpDoughLY ON cpDoughLY.CampaignID = campLY.ID AND cpDoughLY.ProgramID = 44 AND cpDoughLY.DeletedTF = 0
	JOIN	CampaignProgram cpGiftLY ON cpGiftLY.CampaignID = campLY.ID AND cpGiftLY.ProgramID IN (53, 55, 59) AND cpGiftLY.DeletedTF = 0)
				ON campLY.BillToAccountID = acc.ID AND campLY.StartDate BETWEEN '2016-07-01' AND '2017-06-30' AND campLY.Status = 37002 AND cpGiftLY.ProgramID = cpGift.ProgramID
WHERE		camp.StartDate >= '2017-07-01'
AND			camp.Status = 37002
AND			cpDough.ProgramID IN (44)
AND			cpDough.DeletedTF = 0
AND			cpDough.OnlineOnly = 0
AND			cpGift.ProgramID IN (53, 55, 59)
AND			cpGift.DeletedTF = 0
AND			cpGift.OnlineOnly = 0
AND			camp.OnlineOnlyPrograms = 0
AND			ISNULL(camp.Notes,'') NOT LIKE '%EXCEPTION%'
AND			camp.ID NOT IN (105724,106590,106050,106122,106732,106123,106172,105814,105885,106180,105630,105574,107101,108040)
ORDER BY	fm.FirstName + ' ' + fm.LastName,
			acc.ID

select		v.CampaignID, SUM(ISNULL(v.NetSale, 0))
FROM		QSPCanadaFinance..vw_GetNetForReporting v
JOIN		QSPCanadaProduct..ProgramSectionType pst WITH (NOLOCK) ON pst.ID = v.section_type_id
WHERE		pst.ID IN (1,2,9,10,11,13,14,15) -- Gift, Mag, CD, Jewelry, Candle, Trt, Entertainment
and			v.campaignid = 102538
group by	v.campaignid
