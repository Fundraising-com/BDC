
--AccountID's together:
--6149 , 32916
--17444, 17443, 17442, 31909

SELECT  acc.Id AS AccountID, 
	acc.Name AS AccountName, 
	c.ID AS CampaignID, 
	Convert(varchar,c.StartDate,101) CampaignStartDate,
	Convert(varchar,c.EndDate,101) CampaignEndDate, 
	QspCanadaOrderManagement.dbo.Udf_ProgramsByCampaign(C.ID) Programs,
	fm.FirstName FMFirstName,
	fm.LastName FMLastName, 
	b.OrderID,
	d.producttype,
	Convert(varchar,b.Date,101) AS OrderDate, 
	i.INVOICE_ID AS InvoiceID, 
	CD.Description AS CAStatus, 
    CD1.Description AS BatchStatus, 
	CD2.Description AS OrderType, 
	CD3.Description AS OrderQualifier, 

	Case d.producttype
	When 46008 Then 0	
	Else SUM(d.Price) 
	End AS GrossSales,

	Case d.producttype
	When 46008 Then 0	
	Else SUM(d.Price) * .06
	End AS FivePercentOfGrossSales,

	Case WHEN d.producttype = 46008 AND acc.CAccountCodeClass <> 'FM' THEN SUM(d.Price)
	Else 0
	End AS PrizeCost

Into #Orders
FROM		QSPCanadaOrderManagement.dbo.Batch b
JOIN		QSPCanadaOrderManagement.dbo.CustomerOrderHeader h ON b.ID = h.OrderBatchID AND b.[Date] = h.OrderBatchDate
JOIN		QSPCanadaOrderManagement.dbo.CustomerOrderDetail d ON h.Instance = d.CustomerOrderHeaderInstance
LEFT JOIN	QSPCanadaFinance.dbo.INVOICE i ON i.Invoice_ID = d.InvoiceNumber
LEFT JOIN	QSPCanadaCommon.dbo.CodeDetail CD1 ON b.StatusInstance = CD1.Instance
LEFT JOIN	QSPCanadaCommon.dbo.CodeDetail CD2 ON b.OrderTypeCode = CD2.Instance
LEFT JOIN	QSPCanadaCommon.dbo.CodeDetail CD3 ON b.OrderQualifierID = CD3.Instance
JOIN		QSPCanadaCommon.dbo.Campaign c ON b.CampaignID = c.ID
JOIN		QSPCanadaCommon.dbo.CAccount acc ON c.BillToAccountID = acc.Id
LEFT JOIN	QSPCanadaCommon.dbo.CodeDetail CD ON CD.Instance = c.Status
LEFT JOIN	QSPCanadaCommon.dbo.FieldManager fm ON c.FMID = fm.FMID 
WHERE		(b.OrderQualifierID IN (39001, 39002, 39006, 39009, 39015, 39020))
AND			c.StartDate BETWEEN '2012-07-01' AND '2012-12-31'
and			b.OrderTypeCode <> 41012
AND			(d.ProductType NOT IN (46013, 46014, 46015))
AND			b.StatusInstance <> 40005
AND			d.DelFlag = 0
AND			d.StatusInstance NOT IN (500)
and b.AccountID in (6149 , 32916 )
GROUP BY acc.Id, acc.Name, b.AccountID, acc.Name, c.ID, c.StartDate, c.EndDate,
		fm.FirstName, fm.LastName, b.OrderID, b.[Date],i.INVOICE_ID, acc.CAccountCodeClass, d.producttype,
		CD.Description, CD1.Description, CD2.Description, CD3.Description

SELECT		o.AccountID,
			o.AccountName,
			o.CampaignID,
			o.Programs,
			o.FMFirstname,
			o.FMLastname,
			o.OrderID,
			o.OrderDate OrderDate,
			o.InvoiceID,
			pt.[Description] AS ProductType,
			o.GrossSales AS GrossSales,
			o.FivePercentOfGrossSales AS FivePercentOfGrossSales,
			o.PrizeCost AS PrizeCost
FROM		#Orders o
JOIN		QSPCanadaCommon..CodeDetail pt ON	pt.Instance = o.ProductType
ORDER BY	o.FMLastName, o.CampaignID, o.OrderID, o.ProductType

SELECT		SUM(o.GrossSales) AS GrossSales,
			SUM(o.FivePercentOfGrossSales) AS FivePercentOfGrossSales,
			SUM(o.PrizeCost) AS PrizeCost,
			SUM(o.PrizeCost) - SUM(o.FivePercentOfGrossSales) AS AmountOwing
FROM		#Orders o

drop table #Orders