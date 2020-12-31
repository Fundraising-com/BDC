--Report
SELECT		sdCamp.AccountID,
			ISNULL(SUM(sdCamp.TransactionAmount), 0) Balance,
			sdCamp.CampaignID,
			fm.FMID,
			fm.FirstName,
			fm.LastName
FROM		qspcanadafinance.dbo.UDF_Statement_GetDetails_WithBusLogic('2017-07-22') sdCamp
join		QSPCanadaCommon..CAccount acc on acc.Id = sdCamp.AccountID
join		QSPCanadaCommon..Campaign camp on camp.BillToAccountID = acc.Id and camp.Status = 37002
join		QSPCanadaCommon..FieldManager fm on fm.FMID = camp.FMID
where		acc.CAccountCodeGroup = 'Comm'
group by	sdCamp.AccountID,
			sdCamp.CampaignID,
			fm.FMID,
			fm.FirstName,
			fm.LastName
having		SUM(sdCamp.TransactionAmount) <> 0.00
order by	fm.FMID

--For resetting their CA
SELECT		'INSERT INTO #Adjustment VALUES(',
			sdCamp.AccountID,
			',',
			NULL OrderID,
			',',
			CASE WHEN ISNULL(SUM(sdCamp.TransactionAmount), 0) > 0 THEN '''FM Commission Reduction''' ELSE '''FM Commission Payout''' END InternalComment,
			',',
			ABS(ISNULL(SUM(sdCamp.TransactionAmount), 0)) Balance,
			',',
			sdCamp.CampaignID,
			',',
			CASE WHEN ISNULL(SUM(sdCamp.TransactionAmount), 0) > 0 THEN 49060 ELSE 49061 END AdjustmentType,
			')'
FROM		qspcanadafinance.dbo.UDF_Statement_GetDetails_WithBusLogic('2017-07-22') sdCamp
join		QSPCanadaCommon..CAccount acc on acc.Id = sdCamp.AccountID
join		QSPCanadaCommon..Campaign camp on camp.BillToAccountID = acc.Id and camp.Status = 37002
join		QSPCanadaCommon..FieldManager fm on fm.FMID = camp.FMID
where		acc.CAccountCodeGroup = 'Comm'
group by	sdCamp.AccountID,
			sdCamp.CampaignID,
			fm.FMID,
			fm.FirstName,
			fm.LastName
having		SUM(sdCamp.TransactionAmount) <> 0.00
order by	fm.FMID

--Detailed list of non-commission run transactions since end of fiscal year
SELECT		sdCamp.AccountID,
			sdCamp.CampaignID,
			fm.FMID,
			fm.FirstName,
			fm.LastName,
			sdCamp.TransactionID,
			sdCamp.TransactionDate,
			sdCamp.TransactionTypeName TransactionType,
			sdCamp.TransactionAmount,
			sdCamp.OrderID,
			cd.Description OrderType,
			adj.INTERNAL_COMMENT AdjustmentComment
FROM		qspcanadafinance.dbo.UDF_Statement_GetDetails_WithBusLogic('2099-06-30') sdCamp
join		QSPCanadaCommon..CAccount acc on acc.Id = sdCamp.AccountID
join		QSPCanadaCommon..Campaign camp on camp.BillToAccountID = acc.Id and camp.Status = 37002
join		QSPCanadaCommon..FieldManager fm on fm.FMID = camp.FMID
left join	QSPCanadaOrderManagement..Batch b ON b.OrderID = sdCamp.OrderID
left join	QSPCanadaCommon..CodeDetail cd ON cd.Instance = b.OrderQualifierID
left join	QSPCanadaFinance..ADJUSTMENT adj ON adj.ADJUSTMENT_ID = sdCamp.TransactionID
where		acc.CAccountCodeGroup = 'Comm'
and			sdCamp.TransactionDate >= '2017-06-30'
and			sdCamp.TransactionTypeName NOT IN ('Prize Income - FM','Prize Cost - FM','Loyalty Bonus Debit - FM','FM Commission Payout','FM Commission Reduction')
and			sdCamp.TransactionID NOT IN (136514,136515,136516,136517,136518,136519,136520,136521,136522,136523,136524,136525,136526,136527,136528,136529,136530,136531,136532,136533,136534,136535,136536,136537,136540,136541)
and			ISNULL(adj.INTERNAL_COMMENT,'') NOT LIKE 'Reverse Commission Given On%'
order by	sdCamp.TransactionDate

begin tran

update ADJUSTMENT
set ADJUSTMENT_EFFECTIVE_DATE = '2017-07-28'
where ADJUSTMENT_ID IN (136391, 136393, 136396)

update GL_ENTRY
set GL_ENTRY_DATE = '2017-07-28'
where ADJUSTMENT_ID IN (136391, 136393, 136396)

--commit tran
