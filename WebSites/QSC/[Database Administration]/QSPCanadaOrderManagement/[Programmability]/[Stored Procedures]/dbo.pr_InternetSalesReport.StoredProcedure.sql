USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_InternetSalesReport]    Script Date: 06/07/2017 09:20:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_InternetSalesReport] 

	@DateFrom datetime	= '1995-01-01',
	@DateTo datetime	= '2010-01-01',
	@zFMID varchar(4)	= ''

AS

SET NOCOUNT ON
SET @zFMID = CASE @zFMID WHEN '' THEN NULL 
			            ELSE @zFMID
			END

BEGIN
SELECT		b.CampaignID, 	
			acc.ID GroupID, 
			acc.Name GroupName,	
			camp.FMID, 
			fm.FirstName + ' ' + fm.LastName FMName,
			SUM(CASE cod.ProductType
				WHEN 46001 THEN 1
				ELSE cod.Quantity
			END) AS SubsOrdered,
			SUM(cod.Price) GrossSales
			/*SUM(invSec.ITEM_COUNT) SubsOrdered,
			SUM(invSec.TOTAL_TAX_INCLUDED) GrossSales,
			SUM(invSec.NET_BEFORE_TAX - ISNULL(invSec.US_Postage_Amount, 0.00)) NetSales,
			SUM(invSec.GROUP_PROFIT_AMOUNT) ProfitEarned*/
FROM		Batch B (NOLOCK)
JOIN		CustomerOrderHeader coh (NOLOCK) ON coh.OrderBatchID = b.ID AND coh.OrderBatchDate = b.Date
JOIN		CustomerOrderDetail cod (NOLOCK) ON cod.CustomerOrderHeaderInstance = coh.Instance
--JOIN		QSPCanadaFinance..Invoice inv (NOLOCK) ON inv.ORDER_ID = b.OrderID
--JOIN		QSPCanadaFinance..INVOICE_SECTION invSec (NOLOCK) ON invSec.INVOICE_ID = inv.INVOICE_ID
JOIN		QSPCanadaCommon..CAccount acc (NOLOCK) ON b.AccountID = acc.Id
JOIN		QSPCanadaCommon..Campaign camp (NOLOCK) ON camp.ID = b.CampaignID
JOIN		QSPCanadaCommon..FieldManager fm (NOLOCK) ON fm.FMID = camp.FMID	
WHERE		b.OrderQualifierId = 39009
AND			cod.ProductType NOT IN (46017, 46021) --Processing Fees, Shipping
AND			cod.IsVoucherRedemption = 0
--AND			invSec.SECTION_TYPE_ID NOT IN (8, 12)
AND			B.[Date] BETWEEN @DateFrom AND @DateTo
AND			fm.FMID = ISNULL(@zFMID, camp.FMID)
GROUP BY	B.CampaignID, 	
			acc.ID,
			acc.Name,
			camp.ID,
			camp.FMID,
			fm.FirstName,
			fm.LastName 
ORDER BY	fm.lastname,
			fm.firstname,
			acc.id,
			B.CampaignID
END
GO
