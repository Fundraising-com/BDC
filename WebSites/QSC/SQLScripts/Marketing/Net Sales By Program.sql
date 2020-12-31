SELECT		dm.Firstname + ' ' + dm.Lastname DM,
			fm.FMID,
			fm.FirstName + ' ' + fm.LastName FM,
			pt.Description ProductLine,
			pst.ID ProgramSectionType,
			pm.SubType ProgramTypeID,
			b.CampaignID,
			b.OrderID,
			b.Date OrderDate,
			(cod.Price - ISNULL(PostageAmount,0.00) -
			CASE WHEN pst.ID IN (1, 3, 7, 10) THEN 0.00 ELSE (tax+Tax2) END) *
			(1 - CASE WHEN pst.ID = 1 THEN CASE WHEN acc.CAccountCodeClass = 'FM' THEN 0.00
														WHEN b.OrderQualifierID = 39002 THEN 0.00
														WHEN pm.SubType IN (30327) THEN 0.75
														WHEN pm.SubType IN (30323, 30329) THEN 0.45
														ELSE 0.40
													END
					  WHEN pst.ID = 2 THEN CASE camp.IsStaffOrder WHEN 1 THEN 0.00 ELSE 0.37 END
					  WHEN pst.ID = 9 THEN 0.40 --Todo
					  WHEN pst.ID = 11 THEN CASE WHEN b.OrderQualifierID = 39022 THEN 0.00 
														WHEN acc.CAccountCodeClass = 'FM' THEN 0.00
														ELSE 0.40
													END
					  WHEN pst.ID = 14 THEN CASE WHEN b.OrderQualifierID = 39022 THEN 0.00 
														WHEN acc.CAccountCodeClass = 'FM' THEN 0.00
														WHEN coh.TRTGenerationCode IN ('2') THEN 0.20
														WHEN coh.TRTGenerationCode IN ('0', 'N') THEN 0.00
														ELSE 0.37
													END
					  WHEN pst.ID = 15 THEN CASE WHEN b.OrderQualifierID = 39022 THEN 0.00 
														WHEN acc.CAccountCodeClass = 'FM' THEN 0.00
														ELSE 0.37
													END
					  ELSE 0
			END) - CASE WHEN pst.ID IN (1) THEN (tax+Tax2) ELSE 0.00 END NetSalesInProgress,
			cod.Price + CASE WHEN pst.ID IN (3, 7, 10) THEN (tax+Tax2) ELSE 0.00 END GrossSalesInProgress,
			CONVERT(NUMERIC(10, 6), NULL) CommissionRate,
			CONVERT(NUMERIC(10, 6), NULL) CommissionAmount,
			cod.Quantity
INTO		#WIP
FROM		Batch b
JOIN		CustomerOrderHeader coh
				ON	coh.OrderBatchID = b.ID
				AND	coh.OrderBatchDate = b.Date
JOIN		CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = coh.Instance
JOIN		QSPCanadaCommon..Campaign camp ON camp.ID = b.CampaignID
JOIN		QSPCanadaCommon..FieldManager fm ON fm.FMID = camp.FMID
JOIN		QSPCanadaCommon..FieldManager dm ON dm.FMID = fm.DMID
JOIN		QSPCanadaCommon..CodeDetail pt ON pt.Instance = cod.ProductType
join		qspcanadaproduct..pricing_details pd on pd.magprice_instance = cod.pricingdetailsid
join		qspcanadaproduct..programsection ps on ps.id = pd.programsectionid
join		qspcanadaproduct..programsectiontype pst on pst.ID = ps.Type
join		qspcanadaproduct..program_master pm on pm.program_id = ps.program_id
JOIN		qspcanadacommon..CAccount acc ON acc.ID = camp.BillToAccountID
where		b.Date between '2016-01-01' and '2016-06-30'
and			pm.program_ID in (342,346)
--Commissions - Mag/TRT - Split Commission, use higher FM commission
Update		wip 
Set			CommissionRate =  (SELECT	MAX(comm.PERCENT_COMM)
							FROM	#WIP wip2
							JOIN	QSPCanadaFinance..Commission comm ON comm.FM_ID = wip2.FMID AND comm.SECTION_TYPE_ID = 2 AND comm.COMMISSION_TYPE_CODE = 'PERCENT' AND comm.COMM_EFFECTIVE_DATE =
									(Select max (COMM_EFFECTIVE_DATE) from QSPCanadaFinance.dbo.Commission c where c.FM_ID = comm.FM_ID and  c.COMM_EFFECTIVE_DATE <= GETDATE() AND c.COMMISSION_TYPE_CODE='PERCENT')
							WHERE wip2.OrderID = wip.OrderID)
FROM		#WIP wip
WHERE		wip.ProgramSectionType IN (2, 14, 15)

--Commissions - TRT - Set to 12% when not comboed with Mag
UPDATE		wip
SET			CommissionRate =  12
FROM		#WIP wip
WHERE		wip.ProgramSectionType = 14
AND			wip.CampaignID NOT IN (SELECT	cp.CampaignID
									FROM	QSPCanadaCommon..CampaignProgram cp
									WHERE	cp.ProgramID IN (1, 2, 47)
									AND		cp.DeletedTF = 0)

--GIFT commission rates
SELECT Section_Type_Id, comm_effective_date, MAX(percent_comm) percent_comm
INTO   #NonMagCommRate
FROM   QSPCanadaFinance.dbo.Commission
WHERE  Section_Type_Id IN (1,9,10,11,13) -- Gift, Cookie Dough, Chocolate, Jewelry, Candles
AND    Commission_Type_Code = 'PERCENT'
GROUP BY Section_Type_Id, comm_effective_date 
ORDER BY Section_Type_Id, comm_effective_date 

--Multiple Commission rates (by Effective Date) for Gift 

DECLARE @Section_Type_Id int, 
		@EffectiveFrom DateTime, 
		@Rate Int

DECLARE	AllCommRate CURSOR FOR
SELECT	* 
FROM	#NonMagCommRate

OPEN AllCommRate
FETCH NEXT FROM AllCommRate INTO @Section_Type_Id,@EffectiveFrom, @Rate
				
WHILE(@@Fetch_status = 0)
BEGIN
	UPDATE #WIP
	SET CommissionRate = CASE #WIP.ProgramSectionType WHEN 30327 THEN 15 ELSE @Rate END --Exception for Donations
	FROM   #NonMagCommRate nm
	WHERE CONVERT(Datetime,Convert(Varchar(10),#WIP.OrderDate,101)) >= @EffectiveFrom
	AND #WIP.ProgramSectionType = @Section_Type_Id
		
	FETCH NEXT FROM AllCommRate  INTO @Section_Type_Id,@EffectiveFrom, @Rate
END
CLOSE AllCommRate
DEALLOCATE AllCommRate

--Exception for Sales Associates
UPDATE	#WIP
SET		CommissionRate = 3
WHERE	FMID IN ('1568')

--Processing Fee
UPDATE	#WIP
SET		CommissionRate = 0
FROM	QSPCanadaFinance..Commission
WHERE	ProgramSectionType = 8

UPDATE	#WIP
SET		CommissionRate = 0.00
WHERE	CommissionRate IS NULL

UPDATE	#WIP
SET		CommissionAmount = CommissionRate * NetSalesInProgress / 100 

SELECT		ProductLine,
			ROUND(SUM(NetSalesInProgress), 2) NetSales,
			ROUND(SUM(GrossSalesInProgress), 2) GrossSales,
			SUM(Quantity) Quantity,
			ROUND(AVG(CommissionRate), 2) CommissionRate,
			ROUND(SUM(CommissionAmount), 2) CommissionAmount
FROM		#WIP
GROUP BY	ProductLine
ORDER BY	ProductLine

DROP TABLE #WIP
DROP TABLE #NonMagCommRate