USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetAllOrdersNotInvoiced]    Script Date: 06/07/2017 09:17:13 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE  PROCEDURE [dbo].[GetAllOrdersNotInvoiced] 
	@FromDate 	datetime,
	@ToDate 	datetime
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 11/9/2004 
--   Get List of Orders Not Yet Invoiced For Canada Finance System
--   MTC 12/16/2004
--   Do Not include Cancelled Orders or Internet Fix orders
--   MS  May 09,2005
--   if the price is zero donot list as invoiceable
--   Payment method should be credit card or Cheque/Cash and payment status good
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON
SELECT B.OrderID, 
	B.CampaignID, 
	B.StatusInstance,
	B.AccountID,  
         	Name as GroupName, 
	--CONVERT(datetime, CONVERT(varchar,DateBatchCompleted,112)) as DateBatchCompleted,
	Case CONVERT(varchar,DateBatchCompleted,101) 
		When '01/01/1995' Then 'Not Completed'
		Else CONVERT(varchar,DateBatchCompleted,101) 
	End as DateBatchCompleted,
         	CD.Description as OrderType
FROM   QSPCanadaCommon.dbo.CodeDetail CD INNER JOIN
        	QSPCanadaOrderManagement.dbo.Batch B INNER JOIN
        	QSPCanadaCommon.dbo.CAccount A ON B.AccountID = A.Id INNER JOIN
        	QSPCanadaOrderManagement.dbo.CustomerOrderHeader OH ON B.ID = OH.OrderBatchID AND B.[Date] = OH.OrderBatchDate INNER JOIN
        	QSPCanadaOrderManagement.dbo.CustomerOrderDetail OD ON OH.Instance = OD.CustomerOrderHeaderInstance ON CD.Instance = B.OrderQualifierID LEFT OUTER JOIN
        	QSPCanadaOrderManagement.dbo.CreditCardPayment CCP INNER JOIN
        	QSPCanadaOrderManagement.dbo.CustomerPaymentHeader CPH ON CCP.CustomerPaymentHeaderInstance = CPH.Instance ON  OH.Instance = CPH.CustomerOrderHeaderInstance
WHERE IsNull(IsInvoiced,0) = 0
AND 	OrderTypeCode <> 41009 --Exclude Magnet
AND 	B.StatusInstance NOT IN(40001, 40005,40015) -- New, Cancelled 
AND 	B.OrderQualifierID  IN( 39001,39002,39003,39009,39013,39015,39020) -- Main.Suppl,Staff,Internet, CC Re-process/toInvoice
AND 	CONVERT(datetime, CONVERT(varchar,Date,112)) BETWEEN @FromDate AND @ToDate
AND 	OH.PaymentMethodInstance in (50002,50003,50004)
AND 	( 
		(OH.PaymentMethodInstance <> 50002 	AND CCP.StatusInstance = 19000 )
		OR
		(OH.PaymentMethodInstance = 50002 )
	)
AND 	Price > 0	
AND     OD.DelFlag =0
AND 	B.OrderQualifierID  NOT IN( 39009)
AND  NOT EXISTS (SELECT CampaignId FROM QSPCanadaCommon.dbo.CampaignProgram cp
		      WHERE cp.ProgramId =24 AND cp.DeletedTf=0
		      AND b.campaignID=cp.CampaignID)
AND  B.Orderid   Not IN  (307670) --IssueTracker#303
GROUP BY B.OrderID, 	B.CampaignID, 	B.StatusInstance,B.AccountID,  Name,DateBatchCompleted,CD.Description
ORDER BY DateBatchCompleted


/* Disabled MS May 09, 2005
SELECT B.OrderID, 
	B.CampaignID, 
	B.StatusInstance,
	B.AccountID,  
         	Name as GroupName, 
	--CONVERT(datetime, CONVERT(varchar,DateBatchCompleted,112)) as DateBatchCompleted,
	Case CONVERT(varchar,DateBatchCompleted,101) 
		When '01/01/1995' Then 'Not Completed'
		Else CONVERT(varchar,DateBatchCompleted,101) 
	End as DateBatchCompleted,
         	CD.Description as OrderType
FROM QSPCanadaOrderManagement..Batch B
LEFT JOIN QSPCanadaCommon..CAccount A on B.AccountID = A.ID
LEFT JOIN QSPCanadaCommon..CodeDetail CD on CD.Instance = B.OrderQualifierID
WHERE CONVERT(datetime, CONVERT(varchar,Date,112)) BETWEEN @FromDate AND @ToDate
	AND ISInvoiced = 0
	AND OrderTypeCode <> 41009 --Exclude Magnet
	AND OrderQualifierID <> 39008 --Exclude Cust Svc
	AND B.StatusInstance <> 40015 --Cancelled Order
	AND B.OrderQualifierID <> 39011 -- Internet Fix
	AND B.OrderQualifierID <> 39012
	AND B.OrderQualifierID <> 39014 --CC Re-process courtesy
	AND B.StatusInstance <> 40001
ORDER BY DateBatchCompleted
*/
SET NOCOUNT OFF
GO
