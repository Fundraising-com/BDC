select DISTINCT b.AccountID, b.CampaignID OldCampaignOrderTiedTo, c.StartDate OldCampaignStartDate, c.EndDate OldCampaignEndDate, fm.FirstName + ' ' + fm.LastName FMLastName, b.Date OrderDate, b.OrderID, c2.ID NewCampaignOrderMovedTo, c2.StartDate NewCampaignStartDate, c2.EndDate NewCampaignEndDate
into #subs
from batch b
join qspcanadacommon..Campaign c on c.id = b.CampaignID
join QSPCanadaCommon..FieldManager fm on fm.FMID = c.FMID

join (QSPCanadaCommon..Campaign c2 
			join QSPCanadaCommon..CampaignProgram cp on cp.CampaignID = c2.ID)
													 on c2.StartDate >= '2015-07-01' 
													 and c2.StartDate < '2015-12-31' 
													 and DATEADD(dd, 1, c2.ApprovedStatusDate) <= b.Date 
													 and c2.Status = 37002 
													 and cp.DeletedTF = 0 
													 and cp.ProgramID in (1,2,50,52,54)
													 and c2.BillToAccountID = c.BillToAccountID

where OrderQualifierID in (39001, 39002, 39009)
and b.Date >= '2015-07-01'
and c.StartDate < '2015-07-01'
--and c.BillToAccountID in (select BillToAccountID from QSPCanadaCommon..Campaign c2 join QSPCanadaCommon..CampaignProgram cp on cp.CampaignID = c2.ID where c2.StartDate >= '2015-07-01'and c2.StartDate < '2015-12-31' and DATEADD(dd, 1, c2.ApprovedStatusDate) <= b.Date and Status = 37002 and cp.DeletedTF = 0 and cp.ProgramID in (1,2,50,52,54))
and c2.id not in (100182)
order by b.OrderID

select *
from #subs
order by FMLastName, OrderID

begin tran

UPDATE	B
SET		CampaignID = o.NewCampaignOrderMovedTo
FROM	CustomerOrderHeader coh
JOIN	Batch b
			ON	b.ID = coh.OrderBatchID 
			AND	b.Date = coh.OrderBatchDate
JOIN	#subs o
			ON	o.OrderID = b.OrderID

UPDATE	coh
SET		CampaignID = o.NewCampaignOrderMovedTo
FROM	CustomerOrderHeader coh
JOIN	Batch b
			ON	b.ID = coh.OrderBatchID 
			AND	b.Date = coh.OrderBatchDate
JOIN	#subs o
			ON	o.OrderID = b.OrderID
			
UPDATE	pmt
SET		Campaign_ID = o.NewCampaignOrderMovedTo
FROM	QSPCanadaFinance..Payment pmt
JOIN	Batch b
			ON	b.OrderID = pmt.Order_ID
JOIN	#subs o
			ON	o.OrderID = b.OrderID

--commit tran
