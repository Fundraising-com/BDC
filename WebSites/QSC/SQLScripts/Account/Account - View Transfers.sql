USE QSPCanadaCommon
GO

SELECT	*
FROM	FieldManager
WHERE	firstname like 'krystal'

SELECT		DISTINCT fmNotAssigned.FirstName + ' ' + fmNotAssigned.LastName OriginalFM,
			fmAssigned.FirstName + ' ' + fmAssigned.LastName NewFM,
			acc.Id AccountID, acc.Name AccountName, c.ID CampaignIDThatCreatedTheTransfer, c.Status, c.StartDate CampaignStartDate, c.EndDate CampaignEndDate,
			seas.StartDate TransferEffectiveDate, c.ApprovedStatusDate DateAccountTransferWasInputted			
FROM		Campaign campOld
JOIN		Campaign campNew ON campNew.BillToAccountID = campOld.BillToAccountID AND campNew.IsStaffOrder = campold.IsStaffOrder AND campNew.StartDate > campOld.StartDate
JOIN		FieldManager fmAssigned ON fmAssigned.FMID = campNew.FMID
JOIN		FieldManager fmNotAssigned ON fmNotAssigned.FMID = campOld.FMID
JOIN		Campaign c ON c.ID = campNew.ID
JOIN		CAccount acc ON acc.Id = c.BillToAccountID
JOIN		QSPCanadaCommon..Season seas WITH (NOLOCK) ON campNew.StartDate BETWEEN seas.StartDate AND seas.EndDate AND seas.Season IN ('F','S')
WHERE		campOld.FMID <> campNew.FMID
AND			(campOld.FMID = '1553' OR campNew.FMID = '1553')
AND			campOld.StartDate BETWEEN '2015-01-01' AND '2015-12-31'
AND			campNew.StartDate BETWEEN '2016-01-01' AND '2016-12-31'
AND			acc.BusinessUnitID <> 2
ORDER BY	c.ApprovedStatusDate, seas.StartDate, acc.ID

------
select	distinct acc.Id
from	Campaign camp
join	CAccount acc ON acc.ID = camp.BillToAccountID
where	camp.FMID = '1542'
and		camp.StartDate >= '2013-01-01'
and		acc.Id in
(
	SELECT	distinct acc.Id
	from	Campaign camp
	join	CAccount acc ON acc.ID = camp.BillToAccountID
	where	camp.FMID = '0005'
	and		camp.StartDate < '2013-01-01'
)