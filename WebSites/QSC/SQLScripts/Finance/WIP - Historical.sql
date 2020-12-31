SELECT		fm.FirstName + ' ' + fm.LastName FMName, pst.Description,
			SUM((ISNULL(invs.Total_Tax_Excluded, 0) - ISNULL(invs.Group_Profit_Amount, 0) - ISNULL(invs.US_Postage_Amount, 0)) * CASE invs.ProgramType WHEN 30332 THEN 0.5 ELSE 1 END) NetSaleOfInProgressSalesOn11292016
FROM		qspcanadaordermanagement.dbo.batch b
JOIN		qspcanadafinance.dbo.invoice i on i.order_id = orderid
JOIN		qspcanadafinance.dbo.invoice_section invs on i.invoice_id = invs.invoice_id
JOIN		qspcanadafinance.dbo.gl_entry ge on ge.invoice_id = i.invoice_id
JOIN		QSPCanadaCommon..Campaign camp WITH (NOLOCK) ON camp.ID = b.CampaignID
JOIN		QSPCanadaCommon..Season seas WITH (NOLOCK) ON camp.StartDate BETWEEN seas.StartDate AND seas.EndDate AND seas.Season IN ('F','S')
JOIN		QSPCanadaProduct..ProgramSectionType pst WITH (NOLOCK) ON pst.ID = invs.SECTION_TYPE_ID
JOIN		QSPCanadaCommon..FieldManager fm WITH (NOLOCK) ON fm.FMID = camp.FMID
JOIN		QSPCanadaCommon..FieldManager dm WITH (NOLOCK) ON dm.FMID = CASE fm.DMIndicator WHEN 'N' THEN fm.DMID ELSE fm.FMID END
WHERE		pst.ID IN (1,2,9,10,11,13,14,15) -- Gift, Mag, CD, Jewelry, Candle, Trt, Entertainment
AND			fm.FMID NOT IN ('0508')
AND			camp.ExcludeFromSalesBase = 0
AND			i.invoice_date >= '2016-11-29'
AND			b.date between '2015-11-29' and '2016-11-28'
AND			b.OrderQualifierID NOT IN (39007,39012,39011,39008,39018,39019)
GROUP BY	fm.FirstName + ' ' + fm.LastName, pst.Description

/*select SUM((ISNULL(invs.Total_Tax_Excluded, 0) - ISNULL(invs.Group_Profit_Amount, 0) - ISNULL(invs.US_Postage_Amount, 0)) * CASE invs.ProgramType WHEN 30332 THEN 0.5 ELSE 1 END) NetSale
from invoice i
join INVOICE_SECTION invs on invs.INVOICE_ID = i.INVOICE_ID
join QSPCanadaOrderManagement..Batch b on b.OrderID = i.ORDER_ID
where invoice_date >= '2016-11-04'
and b.date < '2016-11-04'
and b.date > '2015-11-04'
and invs.SECTION_TYPE_ID IN (1,2,9,10,11,13,14,15)
AND	b.OrderQualifierID NOT IN (39007,39012,39011,39008,39018,39019)
AND			fm.FMID NOT IN ('0508')*/