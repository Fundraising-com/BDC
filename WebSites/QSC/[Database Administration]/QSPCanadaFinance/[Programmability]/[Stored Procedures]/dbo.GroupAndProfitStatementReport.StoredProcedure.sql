USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GroupAndProfitStatementReport]    Script Date: 06/07/2017 09:17:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GroupAndProfitStatementReport]

	@AccountID 	INT,
	@StartDate	DATETIME,
	@EndDate	DATETIME,
	@FMID		VARCHAR(4) = NULL

AS

SET NOCOUNT ON

IF ISNULL(@AccountID, 0) = 0
BEGIN
	SET @AccountID = NULL
END

IF @FMID = '9999'
BEGIN
	SET @FMID = NULL
END

CREATE TABLE #Items
(
	ID						INT IDENTITY(1,1),
	IsStaffOrder			INT,
	OrderQualifierID		INT,
	OrderTypeCode			INT,
	Invoice_ID				INT,
	AcctID					INT,
	phoneNumber				VARCHAR(25),
	CampaignID				INT,
	Order_Id				INT,
	FMID					VARCHAR(4),
	FMName					VARCHAR(100),
	ContactName				VARCHAR(100),
	Invoice_Date			DATETIME,
	Invoice_Due_Date		DATETIME,
	AcctName				VARCHAR(50),
	ShippingAddress			VARCHAR(50),
	ShippingAddress2		VARCHAR(50),
	ShippingCity			VARCHAR(50),
	ShippingState			VARCHAR(5),
	ShippingZip				VARCHAR(10),
	ShippingZip4			VARCHAR(10),
	TAXregion				VARCHAR(10),
	ContactId				INT,
	Lang					VARCHAR(10), 
	Section_Type_Id			INT,
	Original_Section_Type_ID	INT,
	Total_Tax_Included		NUMERIC(14,2), 
	Total_Tax_Excluded		NUMERIC(14,2), 
	Item_Count				INT,
	Group_Profit_Rate		NUMERIC(8,2), 
	Group_Profit_Amount		NUMERIC(14,2), 
	Total_Taxable_Amount	NUMERIC(14,2), 
	Net_Before_Tax			NUMERIC(14,2), 
	GST						INT,
	GST_Rate				NUMERIC(8,2), 
	GST_Total				NUMERIC(14,2), 
	PST						INT,
	PST_Rate				NUMERIC(14,3), 
	PST_Total				NUMERIC(14,2), 
	HST						INT,
	HST_Rate				NUMERIC(14,3), 
	HST_Total				NUMERIC(14,2), 
	Total_Tax_Amount		NUMERIC(14,2), 
	Due_Amount				NUMERIC(14,2), 
	SalesTax				NUMERIC(14,2), 
	SalesTaxrate			NUMERIC(14,2), 
	MySortOrder				INT,
	StaffItemCount			INT,
 	StaffItemAmount			NUMERIC(14,2),
 	ExclusiveItemCount		INT,
 	ExclusiveItemAmount		NUMERIC(14,2), 
	StaffExclusiveItemCount	INT,
 	StaffExclusiveItemAmount NUMERIC(14,2),
	QSPAddress1Label		VARCHAR(50),
	QSPAddress2Label		VARCHAR(50),
	QSPPhoneLabel			VARCHAR(50),
	PFEE_ItemCount			INT,
	PFEE_TotalWithoutTax	NUMERIC(14,2),
	PFEE_TotalTax			NUMERIC(14,2),
	PFEE_GST				NUMERIC(14,2),
	PFEE_HST				NUMERIC(14,2),
	PFEE_PST				NUMERIC(14,2),
	US_Postage_Amount		NUMERIC(14,2),
	QSPAddressNameLabel		VARCHAR(50),
	SFEE_ItemCount			INT,
	SFEE_TotalWithoutTax	NUMERIC(14,2),
	SFEE_TotalTax			NUMERIC(14,2),
	SFEE_GST				NUMERIC(14,2),
	SFEE_HST				NUMERIC(14,2),
	SFEE_PST				NUMERIC(14,2),
	Invoice_Section_ID		INT
	/*TrtAdditionalSales		NUMERIC(14,2),
	TrtBonusSales			NUMERIC(14,2),
	TrtAdditionalSalesGP	NUMERIC(14,2),
	TrtBonusSalesGP			NUMERIC(14,2),*/
)

INSERT INTO #Items
SELECT		camp.IsStaffOrder, 
			b.OrderQualifierID, 
			b.OrderTypeCode, 
			inv.Invoice_ID, 
			acc.ID AS AcctID, 
			phone.PhoneNumber,
			CampaignID, 
			inv.Order_ID, 
			fm.FMID,
			fm.FirstName + ' ' + fm.LastName AS FMName,
			--fmAccountOwner.FMID, 
			--fmAccountOwner.FirstName + ' ' + fmAccountOwner.LastName AS FMName, 
			NULL AS ContactName,--cont.FirstName + ' ' + cont.LastName  AS ContactName,
			CONVERT(VARCHAR, Invoice_Date, 101) AS Invoice_Date, 
			CONVERT(VARCHAR, Invoice_Due_Date, 101) AS Invoice_Due_Date, 
			acc.Name AS AcctName,
			adShip.Street1 AS ShippingAddress,
			adShip.Street2 AS ShippingAddress2,
			adShip.City AS ShippingCity,
			adShip.StateProvince AS ShippingState,
			adShip.Postal_Code AS ShippingZip,
			adShip.Zip4 AS ShippingZip4,
			CASE adShip.StateProvince
			WHEN 'NB'	THEN CASE ISNULL(camp.Lang,'EN') 
						WHEN 'FR' THEN 	'TVH'
						ELSE 'HST'
			END
			WHEN 'NS'	THEN 
						 CASE ISNULL(camp.Lang,'EN') 
						WHEN 'FR' THEN 	'TVH'
						ELSE 'HST'
			END
			WHEN 'NL'	THEN 
						 CASE ISNULL(camp.Lang,'EN') 
						WHEN 'FR' THEN 	'TVH'
						ELSE 'HST'
			END
			WHEN 'PE'	THEN 
						 CASE ISNULL(camp.Lang,'EN') 
						WHEN 'FR' THEN 	'TVH'
						ELSE 'HST'
			END
			WHEN 'ON'	THEN 
						 CASE ISNULL(camp.Lang,'EN') 
						WHEN 'FR' THEN 	'TVH'
						ELSE 'HST'
			END
			WHEN 'QC' 	THEN 
						 CASE ISNULL(camp.Lang,'EN') 
						WHEN 'FR' THEN 	'TPS/TVQ'
						ELSE 'GST/QST'
			END			
			ELSE 
				CASE ISNULL(camp.Lang,'EN') 
					WHEN 'FR' THEN 	'TPS'
					ELSE 'GST'
				END
			END TAXRegion,
			MAX(cont.ID) AS ContactID,
			camp.Lang,
			CASE Section_Type_ID
				WHEN 11 THEN 1 --Add jewelry into gift
				WHEN 13 THEN 1 --Add Candles into gift
				WHEN 15 THEN 1 --Add Discount Cards into gift
				WHEN 16 THEN 1 --Add Gift Cards into gift
				WHEN 17 THEN 1 --Add $2 Pretzel Rods into gift
				WHEN 18 THEN 1 --Add $3 Pretzel Rods into gift
				ELSE Section_Type_ID
			END AS Section_Type_ID,
			Section_Type_ID Original_Section_Type_ID,
			/*CASE iSec.SECTION_TYPE_ID
				WHEN 14 THEN CASE WHEN trt.FormCode IN ('0745', '000A', '000B','0009') OR trt.FormCode IS NULL THEN iSec.Total_Tax_Included ELSE 0 END 
				ELSE iSec.Total_Tax_Included
			END AS Total_Tax_Included*/
			iSec.Total_Tax_Included,
			iSec.Total_Tax_Excluded,
			/*CASE iSec.SECTION_TYPE_ID
				WHEN 14 THEN CASE WHEN trt.FormCode IN ('0745', '000A', '000B','0009') OR trt.FormCode IS NULL THEN iSec.ITEM_COUNT ELSE 0 END 
				ELSE Item_Count
			END AS ItemCount*/
			iSec.Item_Count ItemCount,
			iSec.Group_Profit_Rate, 
			/*CASE iSec.SECTION_TYPE_ID
				WHEN 14 THEN CASE WHEN trt.FormCode IN ('0745', '000A', '000B','0009') OR trt.FormCode IS NULL THEN iSec.Group_Profit_Amount ELSE 0 END
				ELSE iSec.Group_Profit_Amount
			END AS Group_Profit_Amount*/
			iSec.Group_Profit_Amount,
			iSec.Total_Taxable_Amount, 
			iSec.Net_Before_Tax,
			ISNULL(gst.Tax_ID, 0) AS GST,
			ISNULL(gst.Tax_Rate, 0) AS GST_Rate,
			ISNULL(gst.Tax_Amount, 0) AS GST_Total,
			ISNULL(pst.Tax_ID, 0) AS PST,
			ISNULL(pst.Tax_Rate,0) AS PST_Rate,
			ISNULL(pst.Tax_Amount, 0)  PST_Total,
			ISNULL(hst.Tax_ID, 0) AS HST,
			ISNULL(hst.Tax_Rate, 0) AS HST_Rate,
			ISNULL(hst.Tax_Amount, 0) AS HST_Total,
			ISNULL(iSec.Total_Tax_Amount, 0) AS Total_Tax_Amount, 
			Due_Amount,
			SUM(ISNULL(gst.Tax_Amount,0.00) + ISNULL(hst.Tax_Amount,0.00) + ISNULL(pst.Tax_Amount,0.00)) SalesTax,
			AVG(ISNULL(gst.Tax_Rate,0.00) + ISNULL(hst.Tax_Rate,0.00) + ISNULL(pst.Tax_Rate,0.00)) SalesTaxRate,
			CASE Section_Type_ID
			 WHEN 2  THEN 0
			 WHEN 1  THEN 2
			 WHEN 11 THEN 2
			 WHEN 13 THEN 2
			 WHEN 14 THEN 2
			 WHEN 15 THEN 2
			 WHEN 16 THEN 2
			 WHEN 17 THEN 2
			 WHEN 18 THEN 2
			 ELSE 1
			END AS	MySortOrder,
			0 AS StaffItemCount,
			CAST(0 AS NUMERIC(10,2)) AS StaffItemAmount, 
			0 ExclusiveItemCount,
			cast(0 AS NUMERIC(10,2)) AS ExclusiveItemAmount,
			0 StaffExclusiveItemCount,
			cast(0 AS NUMERIC(10,2)) AS StaffExclusiveItemAmount,
			CASE ISNULL(camp.Lang, 'EN') 
				WHEN 'FR' THEN '33 Prince Street Suite 200' 
			ELSE	'33 Prince Street Suite 200' 	
			END AS QSPAddress1Label,
			CASE ISNULL(camp.Lang, 'EN') 
				WHEN 'FR' THEN 'Montreal, Quebec   H3C 2M7' 
			ELSE 'Montreal, Quebec   H3C 2M7' 
			END AS QSPAddress2Label,
			CASE ISNULL(camp.Lang, 'EN') 
			WHEN 'FR' THEN '1-800-667-2536' 
			ELSE '1-800-667-2536' 
			END AS QSPPhoneLabel,
			0 AS PFEE_ItemCount, 
			0.00 AS PFEE_TotalWithoutTax,
			0.00 AS PFEE_TotalTax,
			0.00 AS PFEE_TotalGST,
			0.00 AS PFEE_TotalHST,
			0.00 AS PFEE_TotalPST,
			iSec.US_Postage_Amount,
			CASE ISNULL(camp.Lang, 'EN') 
				WHEN 'FR' THEN 'QSP' 
			ELSE	'QSP' 	
			END AS QSPAddressNameLabel,
			0 AS SFEE_ItemCount, 
			0.00 AS SFEE_TotalWithoutTax,
			0.00 AS SFEE_TotalTax,
			0.00 AS SFEE_TotalGST,
			0.00 AS SFEE_TotalHST,
			0.00 AS SFEE_TotalPST,
			iSec.INVOICE_SECTION_ID
			/*CASE WHEN trt.FormCode IN ('000I') THEN iSec.Total_Tax_Included ELSE 0 END AS TrtAdditionalSales,
			CASE WHEN trt.FormCode IN ('000J') THEN iSec.Total_Tax_Included ELSE 0 END AS TrtBonusSales,
			CASE WHEN trt.FormCode IN ('000I') THEN iSec.Group_Profit_Amount ELSE 0 END AS TrtAdditionalSalesGP,
			CASE WHEN trt.FormCode IN ('000J') THEN iSec.Group_Profit_Amount ELSE 0 END AS TrtBonusSalesGP*/
FROM		QSPCanadaOrdermanagement..Batch B (NOLOCK)
LEFT JOIN	QSPCanadaCommon..CAccount acc
				ON	b.AccountID = acc.ID
LEFT JOIN	QSPCanadaCommon..Phone phone (NOLOCK)
				ON	phone.PhoneListID = acc.PhoneListID
				AND	phone.Type = 30505  --Main
LEFT JOIN	QSPCanadaCommon..Address adShip
				ON	adShip.AddressListID = acc.AddressListID
				AND	adShip.Address_Type = 54002
JOIN		QSPCanadaCommon..Campaign camp (NOLOCK)
				ON	camp.ID = b.CampaignID
LEFT JOIN	QSPCanadaCommon..Contact cont
				ON	cont.ID = camp.BillToCampaignContactID
JOIN		QSPCanadaCommon..FieldManager fm
				ON	fm.FMID = camp.FMID 
JOIN		QSPCanadaCommon..FieldManager dm
				ON	dm.FMID = fm.DMID
/*JOIN		QSPCanadaCommon..FieldManager fmAccountOwner
				ON	fmAccountOwner.FMID = QSPCanadaCommon.dbo.UDF_Account_GetFMID(camp.BillToAccountID, GETDATE())
JOIN		QSPCanadaCommon..FieldManager dmAccountOwner
				ON	dmAccountOwner.FMID = fmAccountOwner.DMID*/
JOIN		QSPCanadaFinance..Invoice inv
				ON	inv.Order_ID = b.OrderID
				AND	inv.Invoice_Date BETWEEN CONVERT(NVARCHAR, @StartDate, 101) AND CONVERT(NVARCHAR, DATEADD(dd, 1, @EndDate), 101)
JOIN		QSPCanadaFinance..Invoice_Section iSec (NOLOCK)
				ON	 iSec.Invoice_ID = inv.Invoice_ID
JOIN		QSPCanadaProduct..ProgramSectionType ps	(NOLOCK)
				ON	ps.ID = iSec.Section_Type_ID 
LEFT JOIN	(SELECT 1 Tax_ID, ist.INVOICE_SECTION_ID, AVG(ISNULL(Tax_Rate, 0.000)) Tax_Rate, SUM(ISNULL(Tax_Amount, 0.00)) Tax_Amount FROM QSPCanadaFinance.dbo.Invoice_Section_Tax ist (NOLOCK) WHERE ist.Tax_ID = 1 GROUP BY ist.INVOICE_SECTION_ID) AS GST ON iSec.Invoice_Section_ID = GST.Invoice_Section_ID
LEFT JOIN	(SELECT 3 Tax_ID, ist.INVOICE_SECTION_ID, AVG(ISNULL(Tax_Rate, 0.000)) Tax_Rate, SUM(ISNULL(Tax_Amount, 0.00)) Tax_Amount FROM QSPCanadaFinance.dbo.Invoice_Section_Tax ist (NOLOCK) WHERE ist.Tax_ID NOT IN (1,2,4,5,6,7) GROUP BY ist.INVOICE_SECTION_ID) AS PST ON iSec.Invoice_Section_ID = PST.Invoice_Section_ID
LEFT JOIN	(SELECT 6 Tax_ID, ist.INVOICE_SECTION_ID, AVG(ISNULL(Tax_Rate, 0.000)) Tax_Rate, SUM(ISNULL(Tax_Amount, 0.00)) Tax_Amount FROM QSPCanadaFinance.dbo.Invoice_Section_Tax ist (NOLOCK) WHERE ist.Tax_ID IN (2,4,5,6,7) GROUP BY ist.INVOICE_SECTION_ID) AS HST ON iSec.Invoice_Section_ID = HST.Invoice_Section_ID
--LEFT JOIN	QSPCanadaFinance..vw_TRTInvoiceSection trt ON trt.InvoiceSectionID = iSec.INVOICE_SECTION_ID
WHERE		b.AccountID = ISNULL(@AccountID, b.AccountID)
AND			fm.FMID = ISNULL(@FMID, fm.FMID)
--AND			(fm.FMID = ISNULL(@FMID, fm.FMID) OR @FMID = fmAccountOwner.FMID OR dm.FMID = ISNULL(@FMID, dm.FMID) OR @FMID = dmAccountOwner.FMID)
AND			iSec.Section_Type_ID NOT IN (3, 4, 5, 6, 7, 8, 12)
GROUP BY	iSec.Section_Type_ID,
			gst.Tax_ID,
			pst.Tax_ID,
			hst.Tax_ID,
			iSec.Total_Tax_Amount,
			Total_Tax_Included, 
			Total_Tax_Excluded, 
			Item_Count,
			Group_Profit_Rate, 
			Group_Profit_Amount, 
			Total_Taxable_Amount, 
			Net_Before_Tax,
			Total_Tax_Amount, 
			Due_amount,
			camp.Lang,
			camp.IsStaffOrder,  			
			b.OrderQualifierID,
			b.OrderTypeCode,
			inv.Invoice_ID,
			acc.ID,
			CampaignID,
			inv.Order_ID,
			fm.FMID,
			fm.FirstName + ' ' + fm.LastName,
			--fmAccountOwner.FMID,
			--fmAccountOwner.FirstName + ' ' + fmAccountOwner.LastName,
			cont.FirstName + ' ' + cont.LastName,
			CONVERT(VARCHAR, Invoice_Date, 101) , 
			CONVERT(VARCHAR, Invoice_Due_Date, 101), 
			acc.Name,
			adShip.Street1,
			adShip.Street2,
			adShip.City,
			adShip.StateProvince,
			adShip.Postal_Code,
			adShip.Zip4,
			Is_Printed,
			gst.tax_rate,
			pst.tax_rate,
			hst.tax_rate,
			phone.phoneNumber,
			GST.TAX_AMOUNT,
			HST.TAX_AMOUNT,
			PST.TAX_AMOUNT,
			iSec.US_Postage_Amount,
			iSec.INVOICE_SECTION_ID
			--trt.FormCode
ORDER BY	MySortOrder

UPDATE		i
SET			PFEE_ItemCount = iSecPFee.ITEM_COUNT,
			PFEE_TotalWithoutTax = iSecPFee.TOTAL_TAX_EXCLUDED,
			PFEE_TotalTax = iSecPFee.TOTAL_TAX_AMOUNT,
			PFEE_GST = GSTPFee.Tax_Amount,
			PFEE_HST = HSTPFee.Tax_Amount,
			PFEE_PST = PSTPFee.Tax_Amount
FROM		#Items i
JOIN		QSPCanadaFinance..Invoice_Section iSecPFee (NOLOCK)
				ON	iSecPFee.Invoice_ID = i.Invoice_ID
LEFT JOIN	(SELECT 1 Tax_ID, istPFee.INVOICE_SECTION_ID, SUM(ISNULL(Tax_Amount, 0.00)) Tax_Amount FROM QSPCanadaFinance.dbo.Invoice_Section_Tax istPFee (NOLOCK) WHERE istPFee.Tax_ID = 1 GROUP BY istPFee.INVOICE_SECTION_ID) AS GSTPFee ON iSecPFee.Invoice_Section_ID = GSTPFee.Invoice_Section_ID
LEFT JOIN	(SELECT 3 Tax_ID, istPFee.INVOICE_SECTION_ID, SUM(ISNULL(Tax_Amount, 0.00)) Tax_Amount FROM QSPCanadaFinance.dbo.Invoice_Section_Tax istPFee (NOLOCK) WHERE istPFee.Tax_ID NOT IN (1,2,4,5,6,7) GROUP BY istPFee.INVOICE_SECTION_ID) AS PSTPFee ON iSecPFee.Invoice_Section_ID = PSTPFee.Invoice_Section_ID
LEFT JOIN	(SELECT 6 Tax_ID, istPFee.INVOICE_SECTION_ID, SUM(ISNULL(Tax_Amount, 0.00)) Tax_Amount FROM QSPCanadaFinance.dbo.Invoice_Section_Tax istPFee (NOLOCK) WHERE istPFee.Tax_ID IN (2,4,5,6,7) GROUP BY istPFee.INVOICE_SECTION_ID) AS HSTPFee ON iSecPFee.Invoice_Section_ID = HSTPFee.Invoice_Section_ID
WHERE		iSecPFee.SECTION_TYPE_ID = 8 --PFee
AND			i.ID = (SELECT MIN(ID)
					FROM #Items i2
					WHERE i2.Invoice_ID = i.Invoice_ID)

UPDATE		i
SET			SFEE_ItemCount = iSecSFee.ITEM_COUNT,
			SFEE_TotalWithoutTax = iSecSFee.TOTAL_TAX_EXCLUDED,
			SFEE_TotalTax = iSecSFee.TOTAL_TAX_AMOUNT,
			SFEE_GST = GSTSFee.Tax_Amount,
			SFEE_HST = HSTSFee.Tax_Amount,
			SFEE_PST = PSTSFee.Tax_Amount
FROM		#Items i
JOIN		QSPCanadaFinance..Invoice_Section iSecSFee (NOLOCK)
				ON	iSecSFee.Invoice_ID = i.Invoice_ID
LEFT JOIN	(SELECT 1 Tax_ID, istSFee.INVOICE_SECTION_ID, SUM(ISNULL(Tax_Amount, 0.00)) Tax_Amount FROM QSPCanadaFinance.dbo.Invoice_Section_Tax istSFee (NOLOCK) WHERE istSFee.Tax_ID = 1 GROUP BY istSFee.INVOICE_SECTION_ID) AS GSTSFee ON iSecSFee.Invoice_Section_ID = GSTSFee.Invoice_Section_ID
LEFT JOIN	(SELECT 3 Tax_ID, istSFee.INVOICE_SECTION_ID, SUM(ISNULL(Tax_Amount, 0.00)) Tax_Amount FROM QSPCanadaFinance.dbo.Invoice_Section_Tax istSFee (NOLOCK) WHERE istSFee.Tax_ID NOT IN (1,2,4,5,6,7) GROUP BY istSFee.INVOICE_SECTION_ID) AS PSTSFee ON iSecSFee.Invoice_Section_ID = PSTSFee.Invoice_Section_ID
LEFT JOIN	(SELECT 6 Tax_ID, istSFee.INVOICE_SECTION_ID, SUM(ISNULL(Tax_Amount, 0.00)) Tax_Amount FROM QSPCanadaFinance.dbo.Invoice_Section_Tax istSFee (NOLOCK) WHERE istSFee.Tax_ID IN (2,4,5,6,7) GROUP BY istSFee.INVOICE_SECTION_ID) AS HSTSFee ON iSecSFee.Invoice_Section_ID = HSTSFee.Invoice_Section_ID
WHERE		iSecSFee.SECTION_TYPE_ID = 12 --SFee
AND			i.ID = (SELECT MAX(ID)
					FROM #Items i2
					WHERE i2.Invoice_ID = i.Invoice_ID)

SELECT		IsStaffOrder, 
			OrderQualifierID, 
			OrderTypeCode, 
			Invoice_ID, 
			AcctID, 
			PhoneNumber,
			CampaignID, 
			Order_ID, 
			FMID, 
			FMName, 
			ContactName,
			Invoice_Date, 
			Invoice_Due_Date, 
			AcctName,
			ShippingAddress,
			ShippingAddress2,
			ShippingCity,
			ShippingState,
			ShippingZip,
			ShippingZip4,
			TAXRegion,
			ContactID,
			Lang,
			Section_Type_ID,
			Original_Section_Type_ID,
			SUM(Item_Count) Item_Count,
			SUM(Group_Profit_Amount) Group_Profit_Amount,
			SUM(Total_Tax_Included) Total_Tax_Included,
			Sum(GST_Total) GST_Total,
			sum(PST_Total) PST_Total,
			Sum(HST_Total) HST_Total,
			AVG(GST_rate) GST_rate,
			AVG(PST_rate) PST_rate,
			AVG(HSt_RAte) HST_Rate,
			SUM(SalesTax) SalesTax,
			SUM(Total_Tax_Excluded) Total_Tax_Excluded,
			Round(Sum(Group_profit_amount)*100/sum(Total_Tax_Excluded),0) GPrate,
			Convert(Numeric(10,2), (Round(Sum(Group_profit_amount)*100/sum(Total_Tax_Excluded),0)/100)*ExclusiveItemAmount  )   ExclusiveGP,
			MySortOrder,
			SUM(PFEE_ItemCount) PFEE_ItemCount,
			SUM(PFEE_TotalWithoutTax) PFEE_TotalWithoutTax,
			SUM(PFEE_TotalTax) PFEE_TotalTax,
			SUM(PFEE_GST) PFEE_GST,
			SUM(PFEE_HST) PFEE_HST,
			SUM(PFEE_PST) PFEE_PST,
			QSPAddress1Label,
			QSPAddress2Label,
			QSPPhoneLabel,
			QSPAddressNameLabel,
			SUM(US_Postage_Amount) US_Postage_Amount,
			SUM(StaffItemCount) StaffItemCount,
			SUM(StaffItemAmount) StaffItemAmount,
			SUM(ExclusiveItemCount) ExclusiveItemCount,
			SUM(ExclusiveItemAmount) ExclusiveItemAmount,
			SUM(StaffExclusiveItemCount) StaffExclusiveItemCount,
			SUM(StaffExclusiveItemAmount) StaffExclusiveItemAmount,
			SUM(SFEE_ItemCount) SFEE_ItemCount,
			SUM(SFEE_TotalWithoutTax) SFEE_TotalWithoutTax,
			SUM(SFEE_TotalTax) SFEE_TotalTax,
			SUM(SFEE_GST) SFEE_GST,
			SUM(SFEE_HST) SFEE_HST,
			SUM(SFEE_PST) SFEE_PST,
			Invoice_Section_ID
			/*SUM(TrtAdditionalSales) TrtAdditionalSales,
			SUM(TrtBonusSales) TrtBonusSales,
			SUM(TrtAdditionalSalesGP) TrtAdditionalSalesGP,
			SUM(TrtBonusSalesGP) TrtBonusSalesGP*/
INTO		#ItemsGrouped	
FROM		#Items
GROUP BY	IsStaffOrder,
			OrderQualifierID,
			OrderTypeCode,
			Invoice_ID,
			CampaignID,
			Order_Id,
			Invoice_Date,
			Invoice_Due_Date,
			ContactID,
			AcctID,
			AcctName,
			ShippingAddress,
			ShippingAddress2,
			ShippingCity,
			ShippingState,
			ShippingZip,
			ShippingZip4,
			TAXregion,
			phoneNumber,
			FMID,
			FMName,
			Lang, 
			ContactName,
			Section_Type_Id,
			Original_Section_Type_ID,
			StaffItemCount,
			StaffItemAmount,
			ExclusiveItemCount,
			ExclusiveItemAmount,
			StaffExclusiveItemCount,
			StaffExclusiveItemAmount,
			QSPAddress1Label,
			QSPAddress2Label,
			QSPPhoneLabel,
			MySortOrder,
			QSPAddressNameLabel,
			Invoice_Section_ID
ORDER BY MySortOrder 

DECLARE @StaffItemCount int
DECLARE @StaffItemTotalWithTax Numeric(10,2)
DECLARE @ExclusiveItemCount int
DECLARE @ExclusiveItemTotal Numeric(10,2)
DECLARE @StaffExclusiveItemCount int
DECLARE @StaffExclusiveItemTotal Numeric(10,2)

SELECT  @StaffItemCount=SUM(Item_Count), @StaffItemTotalWithTax=SUM(Total_Tax_Included) 
FROM	#ItemsGrouped 
WHERE	IsStaffOrder = 1

SELECT  @StaffExclusiveItemCount = SUM(CASE d.producttype
					WHEN 46001 THEN 1
					 ELSE d.quantity
					  End ),
		@StaffExclusiveItemTotal = SUM(d.price)
FROM	QSPCanadaOrderManagement..Batch b,
		 QSPCanadaOrderManagement..CustomerorderHeader h,
		 QSPCanadaOrderManagement..CustomeroRderDetail d,
		 QSPcanadaproduct..pricing_Details pd,
		 QSPcanadaproduct..product p,
		 #ItemsGrouped
WHERE	b.id=OrderBatchId
AND		b.date=orderBatchdate
AND		h.instance=d.customerorderheaderinstance
AND     pd.magprice_Instance=d.pricingDetailsId
AND 	p.product_instance=pd.product_instance
AND 	d.Delflag =0
AND 	#ItemsGrouped.order_id =b.orderid 
AND 	#ItemsGrouped.Section_Type_Id in (2,9,13) -- changed from 2
AND     d.statusInstance in (507,508,512,513,514)
AND     p.IsQSPExclusive =1
AND     #ItemsGrouped.IsstaffOrder =1

SELECT  @ExclusiveItemCount=   sum(CASE d.producttype
				WHEN 46001 THEN 1
				ELSE d.quantity
				End ),
		@ExclusiveItemTotal=   SUM(d.price) 
FROM	QSPCanadaOrderManagement..Batch b,
		 QSPCanadaOrderManagement..CustomerorderHeader h,
		 QSPCanadaOrderManagement..CustomeroRderDetail d,
		 QSPcanadaproduct..pricing_Details pd,
		 QSPcanadaproduct..product p,
		 #ItemsGrouped
WHERE	b.id=OrderBatchId
AND		b.date=orderBatchdate
AND		h.instance=d.customerorderheaderinstance
AND     pd.magprice_Instance=d.pricingDetailsId
AND 	p.product_instance=pd.product_instance
AND 	d.Delflag =0
AND 	#ItemsGrouped.order_id = b.orderid 
AND 	#ItemsGrouped.Section_Type_Id in (2) -- changed from 2
AND     d.statusInstance in (507,508,512,513,514)
AND     p.IsQSPExclusive = 1
AND     #ItemsGrouped.IsstaffOrder =0

UPDATE #ItemsGrouped
SET 	StaffItemCount=IsNull(@StaffItemCount,0),
		StaffItemAmount=@StaffItemTotalWithTax,
		ExclusiveItemCount=IsNull(@ExclusiveItemCount,0),
		ExclusiveItemAmount=IsNull(@ExclusiveItemTotal,0),
		StaffExclusiveItemCount=IsNull(@StaffExclusiveItemCount,0),
		StaffExclusiveItemAmount=IsNull(@StaffExclusiveItemTotal,0)

DECLARE	@MaxContactID INT,
		@ContactName VARCHAR(100)

SELECT		@MaxContactID = MAX(ContactId)
FROM		#ItemsGrouped ig
LEFT JOIN	QSPCanadaCommon..Contact cont
				ON	cont.ID = ig.ContactId

SELECT		@ContactName = cont.FirstName + ' ' + cont.LastName
FROM		QSPCanadaCommon..Contact cont
WHERE		cont.Id = @MaxContactID

UPDATE		ig
SET			ig.ContactId = @MaxContactID,
			ig.ContactName = @ContactName
FROM		#ItemsGrouped ig

UPDATE	#ItemsGrouped
SET		Group_Profit_Amount = Group_Profit_Amount - SalesTax - SFEE_TotalWithoutTax - SFEE_TotalTax--,
		--Total_Tax_Included = Total_Tax_Included - SalesTax - SFEE_TotalWithoutTax - SFEE_TotalTax
WHERE	Original_Section_Type_Id = 17

SELECT		AcctID,
			AcctName,
			ShippingAddress,
			ShippingAddress2,
			ShippingCity,
			ShippingState,
			ShippingZip,
			ShippingZip4,
			phoneNumber,
			TAXregion,
			ContactName,
			FMID,
			FMName,
			Lang, 
			Section_Type_Id,
			Original_Section_Type_Id,
			Item_Count,
			Group_Profit_Amount,
			Total_Tax_Included,
			GST_Total,
			PST_Total,
			HST_Total,
			GST_rate,
			PST_rate,
			HSt_RAte,
			SalesTax,
			Total_Tax_Excluded,
			GPrate,
			ExclusiveGP,
			StaffItemCount,
			StaffItemAmount,
			ExclusiveItemCount,ExclusiveItemAmount,
			StaffExclusiveItemCount,StaffExclusiveItemAmount,
			QSPAddress1Label,
			QSPAddress2Label,
			QSPPhoneLabel,
			MySortOrder,
			PFEE_ItemCount,
			PFEE_TotalWithoutTax,
			PFEE_TotalTax,
			PFEE_GST,
			PFEE_HST,
			PFEE_PST,
			US_Postage_Amount,
			QSPAddressNameLabel,
			SFEE_ItemCount,
			SFEE_TotalWithoutTax,
			SFEE_TotalTax,
			SFEE_GST,
			SFEE_HST,
			SFEE_PST,
			OrderQualifierID,
			Invoice_ID,
			Invoice_Section_ID
			/*TrtAdditionalSales,
			TrtBonusSales,	
			TrtAdditionalSalesGP,
			TrtBonusSalesGP	*/
FROM		#ItemsGrouped
--WHERE		IsStaffOrder <> 1
ORDER BY	MySortOrder 

DROP TABLE #Items
DROP TABLE #ItemsGrouped
GO
