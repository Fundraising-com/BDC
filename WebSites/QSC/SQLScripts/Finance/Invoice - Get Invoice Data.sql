USE [QSPCanadaFinance]

DECLARE	@FromDate	DATETIME,
		@ToDate		DATETIME

SET @FromDate = '2008-07-01'
SET	@ToDate = '2009-07-01'

SELECT		inv.Invoice_ID,
			SUM(invSec.Total_Tax_Included) AS TotalWithTaxIncluded,
			SUM(invSec.Total_Tax_Excluded) AS TotalWithTaxExcluded,
			SUM(invSec.Total_Taxable_Amount) AS Total_Taxable_Amount,
			SUM(invSec.Net_Before_Tax) AS Net_Before_Tax,
			SUM(invSec.Total_Tax_Amount) AS Total_Tax_Amount,
			SUM(invSec.Due_Amount) AS Due_Amount,
			SUM(invSec.Item_Count) AS Item_Count,
			SUM(invSec.Group_Profit_Amount) AS Group_Profit_Amount
INTO		#InvoiceSection
FROM		Invoice inv
JOIN		Invoice_Section invSec
				ON	invSec.Invoice_ID = inv.Invoice_ID
WHERE		inv.Invoice_Date BETWEEN @FromDate AND @ToDate
GROUP BY	inv.Invoice_ID

SELECT		inv.Invoice_ID,
			inv.Invoice_Date,
			acc.ID AS AccountID,
			acc.Name AS AccountName,
			inv.Invoice_Amount,
			invSec.TotalWithTaxIncluded,
			invSec.TotalWithTaxExcluded,
			invSec.Total_Taxable_Amount,
			invSec.Net_Before_Tax,
			invSec.Total_Tax_Amount,
			invSec.Due_Amount,
			invSec.Item_Count,
			invSec.Group_Profit_Amount,
			inv.Order_ID,
			fm.FMID,
			fm.FirstName AS FMFirstName,
			fm.LastName AS FMLastName,
			[add].Street1 AS AccountAddress1,
			[add].Street2 AS AccountAddress2,
			[add].City AS AccountCity,
			[add].StateProvince AS AccountProvince,
			[add].Postal_Code AS AccountPostalCode
FROM		Invoice inv
JOIN		#InvoiceSection invSec
				ON	invSec.Invoice_ID = inv.Invoice_ID
JOIN		QSPCanadaOrderManagement..Batch batch
				ON	inv.Order_ID = batch.OrderID
JOIN		QSPCanadaCommon..Campaign camp
				ON	camp.ID = batch.CampaignID
JOIN		QSPCanadaCommon..CAccount acc
				ON	acc.ID = camp.BillToAccountID
JOIN		QSPCanadaCommon..Address [add]
				ON	[add].AddressListID = acc.AddressListID
				AND	[add].Address_Type = 54001
JOIN		QSPCanadaCommon..FieldManager fm
				ON	fm.FMID = camp.FMID
WHERE		inv.Invoice_Date BETWEEN @FromDate AND @ToDate
ORDER BY	inv.Invoice_ID

DROP TABLE #InvoiceSection