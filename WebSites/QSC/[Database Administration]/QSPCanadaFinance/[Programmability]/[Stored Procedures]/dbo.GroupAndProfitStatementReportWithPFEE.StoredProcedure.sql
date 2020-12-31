USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GroupAndProfitStatementReportWithPFEE]    Script Date: 06/07/2017 09:17:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GroupAndProfitStatementReportWithPFEE]

	@AccountID 	INT,
	@StartDate	DATETIME,
	@EndDate	DATETIME,
	@Account	VARCHAR(50) = NULL,
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

-- Constants PFEE
DECLARE @InvoiceSectionTypePFEE int
SET @InvoiceSectionTypePFEE = 8

-- Variables PFEE
DECLARE
	@ProcessingFeeItemCount		INT,
	@ProcessingFeeTotalAmount	NUMERIC(10,2)


CREATE TABLE #Items
(
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
	PST_Rate				NUMERIC(14,2), 
	PST_Total				NUMERIC(14,2), 
	HST						INT,
	HST_Rate				NUMERIC(14,2), 
	HST_Total				NUMERIC(14,2), 
	Total_Tax_Amount		NUMERIC(14,2), 
	Due_Amount				NUMERIC(14,2), 
	MagSalesTax				NUMERIC(14,2), 
	MagSalesTaxrate			NUMERIC(14,2), 
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
	PFEE_TotalWTax			NUMERIC(14,2)
)

-- BEGIN: Get the COUNT AND the TOTAL for the PFEE
SELECT	 @ProcessingFeeItemCount	= SUM(Item_Count)
		,@ProcessingFeeTotalAmount	= SUM(Total_Tax_Included)
FROM		QSPCanadaOrdermanagement..Batch B (NOLOCK)
LEFT JOIN	QSPCanadaCommon..CAccount acc
				ON	b.AccountID = acc.ID
JOIN		QSPCanadaCommon..Campaign camp (NOLOCK)
				ON	camp.ID = b.CampaignID
JOIN		QSPCanadaFinance..Invoice inv
				ON	inv.Order_ID = b.OrderID
				AND	inv.Invoice_Date BETWEEN CONVERT(NVARCHAR, @StartDate, 101) AND CONVERT(NVARCHAR, @EndDate, 101)
JOIN		QSPCanadaFinance..Invoice_Section iSec (NOLOCK)
				ON	 iSec.Invoice_ID = inv.Invoice_ID
WHERE		b.AccountID = ISNULL(@AccountID, b.AccountID)
AND			camp.FMID = ISNULL(@FMID, camp.FMID)
AND			iSec.Section_Type_ID = @InvoiceSectionTypePFEE
-- END: Get the COUNT AND the TOTAL for the PFEE


INSERT INTO #Items
SELECT		camp.IsStaffOrder, 
			b.OrderQualifierID, 
			b.OrderTypeCode, 
			inv.Invoice_ID, 
			acc.ID AS AcctID, 
			phone.PhoneNumber,
			CampaignID, 
			inv.Order_ID, 
			camp.FMID, 
			fm.FirstName + ' ' + FM.LastName AS FMName, 
			cont.FirstName + ' ' + cont.LastName  AS ContactName,
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
			WHEN 'BC'	THEN 
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
			Section_Type_ID, 
			Total_Tax_Included, 
			Total_Tax_Excluded, 
			Item_Count,
			Group_Profit_Rate, 
			Group_Profit_Amount,
			Total_Taxable_Amount, 
			Net_Before_Tax,
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
			CASE Section_Type_ID
			WHEN 2 THEN 
				CASE ISNULL(gst.Tax_ID, 0)
				WHEN 0 THEN 
					  CASE ISNULL(hst.Tax_ID, 0)
						WHEN 0 THEN (pst.Tax_Amount)
						ELSE hst.Tax_Amount
					  END
				ELSE gst.Tax_Amount
				END
			ELSE 0
			End AS MagSalesTax,
			CASE Section_Type_ID
			WHEN 2 THEN 
				CASE ISNULL(gst.Tax_ID, 0)
				WHEN 0 THEN
					  CASE ISNULL(hst.Tax_ID, 0)
					   WHEN 0 THEN CAST(pst.Tax_Rate AS INT)
					   ELSE CAST(hst.Tax_Rate AS INT)
					  END
				ELSE CAST(gst.Tax_Rate AS INT)
				END
			Else 0
			End MagSalesTaxrate,
			CASE Section_Type_ID
			 WHEN 2 THen 0
			 WHEN 1 THEN 1
			 WHEN 6 THEN 2
			 ELSE 9
			 END AS	MySortOrder,
			 0 AS StaffItemCount,
			 CAST(0 AS NUMERIC(10,2)) AS StaffItemAmount, 
			 0 ExclusiveItemCount,
			 cast(0 AS NUMERIC(10,2)) AS ExclusiveItemAmount,
			 0 StaffExclusiveItemCount,
			 cast(0 AS NUMERIC(10,2)) AS StaffExclusiveItemAmount,
			CASE ISNULL(camp.Lang, 'EN') 
				WHEN 'FR' THEN '6600, route Transcanadienne - bureau 750' 
			ELSE	'695 Riddell Road' 	
			END AS QSPAddress1Label,
			CASE ISNULL(camp.Lang, 'EN') 
				WHEN 'FR' THEN 'Pointe-Claire, QC   H9R 4S2' 
			ELSE 'Orangeville, ON   L9W 4Z5 ' 
			END AS QSPAddress2Label,
			CASE ISNULL(camp.Lang, 'EN') 
			WHEN 'FR' THEN '1-800-667-2536' 
			ELSE '1-800-667-2536' 
			END AS QSPPhoneLabel,
			0 AS PFEE_ItemCount, 
			0.00 AS PFEE_TotalWTax 

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
JOIN		QSPCanadaFinance..Invoice inv
				ON	inv.Order_ID = b.OrderID
				AND	inv.Invoice_Date BETWEEN CONVERT(NVARCHAR, @StartDate, 101) AND CONVERT(NVARCHAR, @EndDate, 101)
JOIN		QSPCanadaFinance..Invoice_Section iSec (NOLOCK)
				ON	 iSec.Invoice_ID = inv.Invoice_ID
JOIN		QSPCanadaProduct..ProgramSectionType ps	(NOLOCK)
				ON	ps.ID = iSec.Section_Type_ID 
LEFT JOIN	QSPCanadaFinance..Invoice_Section_Tax GST (NOLOCK)
				ON	iSec.Invoice_Section_ID = GST.Invoice_Section_ID
				AND	GST.Tax_ID = 1
LEFT JOIN	QSPCanadaFinance..Invoice_Section_Tax PST (NOLOCK)
				ON	iSec.Invoice_Section_ID = PST.Invoice_Section_ID
				AND	pst.Tax_ID NOT IN (1, 2, 4, 5, 6, 10)
LEFT JOIN	QSPCanadaFinance..Invoice_Section_Tax HST (NOLOCK)
				ON	iSec.Invoice_Section_ID = hst.Invoice_Section_ID
				AND	HST.Tax_ID IN (2, 4, 5, 6, 10),
			QSPCanadaCommon..Tax tax (NOLOCK)
WHERE		b.AccountID = ISNULL(@AccountID, b.AccountID)
AND			camp.FMID = ISNULL(@FMID, camp.FMID)
AND			iSec.Section_Type_ID <> @InvoiceSectionTypePFEE
GROUP BY	iSec.Section_Type_ID,
			gst.Tax_ID,			pst.Tax_ID,
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
			camp.FMID,
			fm.FirstName + ' ' + fm.LastName,
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
			PST.TAX_AMOUNT
ORDER BY	MySortOrder

DECLARE @StaffItemCount int
DECLARE @StaffItemTotalWithTax Numeric(10,2)
DECLARE @ExclusiveItemCount int
DECLARE @ExclusiveItemTotal Numeric(10,2)
DECLARE @StaffExclusiveItemCount int
DECLARE @StaffExclusiveItemTotal Numeric(10,2)

SELECT  @StaffItemCount=SUM(Item_Count), @StaffItemTotalWithTax=SUM(Total_Tax_Included) 
FROM #Items WHERE IsStaffOrder=1


SELECT  @StaffExclusiveItemCount = SUM(CASE d.producttype
					WHEN 46001 THEN 1
					 ELSE d.quantity
					  End ),
	@StaffExclusiveItemTotal = SUM(d.price)
FROM QSPCanadaOrderManagement..Batch b,
     QSPCanadaOrderManagement..CustomerorderHeader h,
     QSPCanadaOrderManagement..CustomeroRderDetail d,
     QSPcanadaproduct..pricing_Details pd,
     QSPcanadaproduct..product p,
     #Items
WHERE b.id=OrderBatchId
AND	b.date=orderBatchdate
AND	h.instance=d.customerorderheaderinstance
AND     pd.magprice_Instance=d.pricingDetailsId
AND 	p.product_instance=pd.product_instance
AND 	d.Delflag =0
AND 	#Items.order_id =b.orderid 
AND 	#Items.Section_Type_Id=2
AND     d.statusInstance in (507,508,512,513,514)
AND     p.IsQSPExclusive =1
AND     #Items.IsstaffOrder =1

SELECT  @ExclusiveItemCount=   sum(CASE d.producttype
				WHEN 46001 THEN 1
				ELSE d.quantity
				End ),
	@ExclusiveItemTotal=   SUM(d.price) 
FROM QSPCanadaOrderManagement..Batch b,
     QSPCanadaOrderManagement..CustomerorderHeader h,
     QSPCanadaOrderManagement..CustomeroRderDetail d,
     QSPcanadaproduct..pricing_Details pd,
     QSPcanadaproduct..product p,
     #Items
WHERE b.id=OrderBatchId
AND	b.date=orderBatchdate
AND	h.instance=d.customerorderheaderinstance
AND     pd.magprice_Instance=d.pricingDetailsId
AND 	p.product_instance=pd.product_instance
AND 	d.Delflag =0
AND 	#Items.order_id =b.orderid 
AND 	#Items.Section_Type_Id=2
AND     d.statusInstance in (507,508,512,513,514)
AND     p.IsQSPExclusive =1
AND     #Items.IsstaffOrder =0

UPDATE #Items
SET 	StaffItemCount=IsNull(@StaffItemCount,0),
	StaffItemAmount=@StaffItemTotalWithTax,
	ExclusiveItemCount=IsNull(@ExclusiveItemCount,0),
	ExclusiveItemAmount=IsNull(@ExclusiveItemTotal,0),
	StaffExclusiveItemCount=IsNull(@StaffExclusiveItemCount,0),
	StaffExclusiveItemAmount=IsNull(@StaffExclusiveItemTotal,0)

-- BEGIN: Update the first section of the table with the PFEE
IF @ProcessingFeeItemCount > 0
	UPDATE #Items
	SET PFEE_ItemCount = @ProcessingFeeItemCount,
		PFEE_TotalWTax = @ProcessingFeeTotalAmount
	WHERE MySortOrder = (SELECT MIN(MySortOrder) FROM #Items)
-- END: Update the first section of the table with the PFEE


SELECT AcctID,	AcctName,
	ShippingAddress,
	ShippingAddress2,
	ShippingCity,
	ShippingState,
	ShippingZip,
	ShippingZip4,
	phoneNumber,	TAXregion,
	--MAX(ContactId),
	ContactName,
	FMID,
	FMName,
	Lang, 
	Section_Type_Id,
	--Description,
	SUM(CASE OrderQualifierId
	WHEN 39009 THEN 0
	ELSE Item_Count
	END) RegularCount,
	SUM(CASE OrderQualifierId
	WHEN 39009 THEN Item_Count
	ELSE 0
	END) OnlineCount,

	SUM(CASE OrderQualifierId
	WHEN 39009 THEN 0
	ELSE Total_Tax_Included
	END) RegTotalWithTax,

	SUM(CASE OrderQualifierId
	WHEN 39009 THEN Total_Tax_Included
	ELSE 0
	END) OnlineTotalWithTax,
	
	Sum(GST_Total) GST_Total,
	sum(PST_Total)PST_Total,
	Sum(HST_Total)HST_Total,
	GST_rate,
	PST_rate,
	HSt_RAte,

	SUM(CASE OrderQualifierId
	WHEN 39009 THEN MagSalesTax
	ELSE 0
	END) OnlineMagSalesTax,

	SUM(CASE OrderQualifierId
	WHEN 39009 THEN 0
	ELSE MagSalesTax
	END) RegMagSalesTax,

	SUM(CASE OrderQualifierId
	WHEN 39009 THEN PST_Total
	ELSE 0
	END) OnlineMagQSTTax,

	SUM(CASE OrderQualifierId
	WHEN 39009 THEN 0
	ELSE PST_Total
	END) RegMagQSTTax,
	--
	SUM(CASE OrderQualifierId
	WHEN 39009 THEN 0
	ELSE Total_Tax_Excluded
	END) RegTotalWithOutTax,

	SUM(CASE OrderQualifierId
	WHEN 39009 THEN Total_Tax_Excluded
	ELSE 0
	END) OnlineTotalWithOutTax,
	--
	SUM(CASE OrderQualifierId
	WHEN 39009 THEN 0
	ELSE Group_Profit_Amount
	END) RegTotalGP,

	SUM(CASE OrderQualifierId
	WHEN 39009 THEN Group_Profit_Amount
	ELSE 0
	END) OnlineTotalGP,

	Round(Sum(Group_profit_amount)*100/sum(Total_Tax_Excluded),0) GPrate,
	Convert(Numeric(10,2), (Round(Sum(Group_profit_amount)*100/sum(Total_Tax_Excluded),0)/100)*ExclusiveItemAmount  )   ExclusiveGP,
	--Staff
	StaffItemCount,
	StaffItemAmount,
	ExclusiveItemCount,ExclusiveItemAmount,
	StaffExclusiveItemCount,StaffExclusiveItemAmount,
	QSPAddress1Label,
	QSPAddress2Label,
	QSPPhoneLabel,
	MySortOrder,
	PFEE_ItemCount,
	PFEE_TotalWTax
FROM #Items
WHERE IsStaffOrder <> 1
GROUP BY AcctID,
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
	GST_rate,
	PST_rate,
	HSt_RAte,
	--Description,
	StaffItemCount,
	StaffItemAmount,
	ExclusiveItemCount,ExclusiveItemAmount,
	StaffExclusiveItemCount,StaffExclusiveItemAmount,
	QSPAddress1Label,
	QSPAddress2Label,
	QSPPhoneLabel,
	MySortOrder,
	PFEE_ItemCount,
	PFEE_TotalWTax
ORDER BY MySortOrder 

DROP TABLE #Items
GO
