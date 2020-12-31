USE [QSPCanadaCommon]
GO

SELECT	*
FROM	FieldManager
WHERE	lastname like 'ny%'

--View all transfers
SELECT		DISTINCT fmAssigned.FirstName + ' ' + fmAssigned.LastName NewFM,
			fmNotAssigned.FirstName + ' ' + fmNotAssigned.LastName OriginalFM,
			acc.Id AccountID, acc.Name AccountName, c.ID CampaignID, c.StartDate NewCampaignStartDate, c.EndDate NewCampaignEndDate,
			dbo.UDF_GetCampaignPrograms(c.ID) Programs
FROM		Campaign campOld
JOIN		Campaign campNew ON campNew.BillToAccountID = campOld.BillToAccountID AND campNew.IsStaffOrder = campold.IsStaffOrder
JOIN		FieldManager fmAssigned ON fmAssigned.FMID = campNew.FMID
JOIN		FieldManager fmNotAssigned ON fmNotAssigned.FMID = campOld.FMID
JOIN		Campaign c ON c.ID = campNew.ID
JOIN		CAccount acc ON acc.Id = c.BillToAccountID
WHERE		campOld.FMID <> campNew.FMID
AND			campOld.FMID = '0035'
--AND			campOld.StartDate BETWEEN '2012-07-01' AND '2013-06-30'
AND			campNew.StartDate BETWEEN '2018-01-01' AND '2018-06-30'
AND			acc.BusinessUnitID <> 2
AND			c.Status = 37002
ORDER BY	c.StartDate

--Get all campaigns with split commissions, should select only top 1 from old campaigns
SELECT		campNew.ID CampaignID, campOld.FMID, 50.00 CommissionPercentage
--INTO		#Details
FROM		Campaign campOld
JOIN		Campaign campNew ON campNew.BillToAccountID = campOld.BillToAccountID AND campNew.IsStaffOrder = campold.IsStaffOrder
WHERE		campOld.FMID = '0095'
AND			campOld.StartDate < '2012-12-31'
AND			campNew.FMID = '1543'
AND			campNew.StartDate BETWEEN '2013-07-01' AND '2013-12-31'
UNION ALL
SELECT		campNew.ID CampaignID, campNew.FMID, 50.00 CommissionPercentage
FROM		Campaign campOld
JOIN		Campaign campNew ON campNew.BillToAccountID = campOld.BillToAccountID AND campNew.IsStaffOrder = campold.IsStaffOrder
WHERE		campOld.FMID = '0095'
AND			campOld.StartDate < '2012-12-31'
AND			campNew.FMID = '1543'
AND			campNew.StartDate BETWEEN '2013-07-01' AND '2013-12-31'
ORDER BY	campNew.ID

BEGIN TRAN

INSERT INTO CampaignCommissionSplit
SELECT		CampaignID, FMID, CommissionPercentage, GETDATE() DateCreated, NULL DateModified
FROM		#Details
ORDER BY	CampaignID, FMID

--COMMIT

--Commission Run Report
SELECT		acc.Id AccountID, acc.Name AccountName, ccs.CampaignID, fmOwner.FirstName + ' ' + fmOwner.LastName CampaignOwner,
			fm.FirstName + ' ' + fm.LastName FMName, c.StartDate CampaignStartDate, c.EndDate CampaignEndDate, ccs.CommissionPercentage, dbo.UDF_GetCampaignPrograms(c.ID) Programs
FROM		CampaignCommissionSplit ccs
JOIN		FieldManager fm ON fm.FMID = ccs.FMID
JOIN		Campaign c ON c.ID = ccs.CampaignID
JOIN		FieldManager fmOwner ON fmOwner.FMID = c.FMID
JOIN		CAccount acc ON acc.Id = c.BillToAccountID
WHERE 		c.StartDate >= '2017-07-01'
AND			c.EndDate <= '2018-06-30'
--and (ccs.FMID = '0064' or fmOwner.fmid = '0064')
ORDER BY	fmOwner.FirstName,	AccountID, c.ID, FMName

DROP TABLE #Details

----

select *
from Campaign
where BillToAccountID IN (
200,
389,
432,
540,
741,
1815,
34041
)
order by StartDate

select *
from FieldManager
where FirstName = 'sandy'

select *
from FieldManager
where FirstName = 'claire'

select acc.Id AccountID,  acc.Name AccountName, c.ID CampaignID, fm.FirstName + ' ' + fm.LastName CampaignOwner, c.StartDate CampaignStartDate, c.EndDate CampaignEndDate, dbo.UDF_GetCampaignPrograms(c.ID) Programs,ly.NetSale
from Campaign c
join CAccount acc ON acc.Id = c.BillToAccountID
join FieldManager fm ON fm.FMID = c.FMID
LEFT JOIN	(SELECT		v.CampaignID, SUM(ISNULL(v.NetSale, 0)) NetSale
			FROM		QSPCanadaFinance..vw_GetNetForReporting v
			JOIN		QSPCanadaProduct..ProgramSectionType pst WITH (NOLOCK) ON pst.ID = v.section_type_id
			WHERE		pst.ID IN (1,2,9,10,11,13,14,15) -- Gift, Mag, CD, Jewelry, Candle, Trt, Entertainment
			GROUP BY	v.campaignid) ly ON ly.CampaignID = c.ID
where c.StartDate between '2017-07-01' and '2018-06-30'
and c.BillToAccountID in (
159,
161,
163,
194,
198,
200,
202,
219,
235,
241,
271,
273,
367,
389,
422,
431,
432,
433,
441,
446,
447,
452,
454,
475,
488,
509,
519,
521,
540,
561,
577,
579,
600,
635,
641,
700,
735,
736,
739,
741,
748,
749,
751,
753,
759,
760,
761,
763,
771,
773,
775,
783,
786,
1780,
1794,
1812,
1815,
1850,
1854,
1882,
2027,
16176,
16829,
30168,
34103,
34115,
34440)
order by c.StartDate, BillToAccountID

select *
from CampaignCommissionSplit
where campaignid in (
103085,
101594,
103094,
103754,
103755,
103097,
103099,
103095,
103101,
104015,
104244	
)

begin tran

insert CampaignCommissionSplit values (98474 , '0003', 50.00, GETDATE(), NULL, '2015-12-31')
insert CampaignCommissionSplit values (98474 , '1553', 50.00, GETDATE(), NULL, '2015-12-31')

--commit tran

begin tran

insert CampaignCommissionSplit
select camp.ID, '0046', 33.33, GETDATE(), NULL, '2017-12-31'
from Campaign camp
where camp.ID in (
104857,
105402)

UNION ALL

select camp.ID, '1560', 66.66, GETDATE(), NULL, '2017-12-31'
from Campaign camp
where camp.ID in (
104857,
105402)

ORDER BY camp.ID, 2

--commit tran

select *
from CampaignCommissionSplit
where campaignid = 100936
