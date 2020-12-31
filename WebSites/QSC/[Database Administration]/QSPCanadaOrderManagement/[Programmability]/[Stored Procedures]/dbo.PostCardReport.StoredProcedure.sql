USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[PostCardReport]    Script Date: 06/07/2017 09:19:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PostCardReport] @FMID varchar(4), 
									@StartDate DateTime, 
									@Enddate dateTime, 
									@OverAmount Numeric(14,2)
AS
BEGIN

/*
SET @FMID='0094'
SET @StartDate='07/01/2007'
SET @Enddate='06/30/2008'
SET @OverAmount=15000
*/

DECLARE @FiscalStartDate dateTime
DECLARE @Year varchar(4)

Select  @Year=DatePart(Year, ISNUll(@StartDate, GetDate()))
Select  @FiscalStartDate = '07/01/'+@year

--Select @FiscalStartDate

------------------------ Get Accounts
SELECT 	b.AccountID 
	,a.Name
	,b.campaignid
	,B.OrderId
	,Round(SUM(price),2) Price
	,Round(SUM(D.Tax + D.Tax2),2) Tax
Into #all
FROM    
	QSPcanadaOrdermanagement.dbo.Batch B	 	        
	LEFT  JOIN QSPCanadaCommon.dbo.CAccount A ON B.AccountID =A.Id,
	QSPCanadaCommon.dbo.Campaign C	, 	   	        
	QSPCanadaOrdermanagement.dbo.CustomerOrderHeader H   ,
	QSPCanadaOrdermanagement.dbo.CustomerOrderDetail    D   ,
	QSPCanadaproduct..pricing_Details PDet			,
	QSPCanadaproduct..product Prd				        
WHERE B.OrderQualifierId IN (39001,39002,39003,39009,39013,39015)
AND	B.Campaignid=C.[Id]
AND C.FMID= @FMID
AND B.[ID] = H.OrderBatchId
AND	B.[DATE]= H.OrderBatchDate
AND	D.CustomerOrderHeaderInstance = H.Instance
AND PDet.MagPrice_Instance=D.PricingDetailsId
AND Prd.Product_Instance=PDet.Product_Instance
AND d.ProductType not in (46013,46014,46015)
AND b.statusInstance <> 40005
AND D.DelFlag=0	
AND c.startDate >= @FiscalStartDate 
AND (EXISTS (SELECT  1
		 FROM QSPCanadaOrderManagement.dbo.CustomerOrderDetailRemitHistory RH
		 WHERE  RH.CustomerOrderHeaderInstance = D.CustomerOrderHeaderInstance 
		 AND RH.TransID = D.TransID
		 AND	RH.Status IN (42000, 42001, 42004)
		 AND DateChanged BETWEEN CONVERT(nvarchar, @StartDate,101)AND CONVERT(nvarchar, @Enddate,101)
		 ) 
	 OR D.StatusInstance=508 )
GROUP BY B.AccountID,a.Name,B.Campaignid,B.OrderId


--Get Account and Order more than 15k gross
Select sum(price)TotalPrice,sum(tax)TotalTax,AccountID,Name, count(distinct orderid)OrdCount
Into #PostCardAccount
from #all
Group BY AccountID,Name
Having sum(price)>= @OverAmount

--=======================================Items data
SELECT 	
	a.Name,b.AccountID 
	,st.FirstName StudentFname,st.LastName StudentLName
	,cu.FirstName CustomerFname,cu.LastName CustomerLname
	,F.FMID, F.FirstName FMFName,F.LastName FMLName
	,b.campaignid
	,B.OrderId
	,d.ProductName
	,d.price
	,h.customerbilltoInstance
	,St.Instance
Into #ITEM
FROM    
	QSPcanadaOrdermanagement.dbo.Batch B	 	        
	LEFT  JOIN QSPCanadaCommon.dbo.CAccount A 	        ON B.AccountID =A.Id,
	QSPCanadaCommon.dbo.Campaign C	, 	   	        
	QSPCanadaCommon.dbo.FieldManager F ,
	QSPCanadaOrdermanagement.dbo.CustomerOrderHeader H ,
	QSPCanadaOrdermanagement.dbo.CustomerOrderDetail    D ,
	QSPCanadaOrderManagement.dbo.Student St	,
	QSPCanadaOrderManagement.dbo.Customer cu        
WHERE B.OrderQualifierId IN (39001,39002,39009,39013,39015)
AND	B.Campaignid=C.[Id]
AND C.FMID = F.FMID
AND C.FMID= @FMID
AND B.[ID] = H.OrderBatchId
AND	B.[DATE]= H.OrderBatchDate
AND	D.CustomerOrderHeaderInstance = H.Instance
AND d.ProductType not in (46013,46014,46015)
AND b.statusInstance <> 40005
AND b.AccountID in(Select Distinct accountID from #PostCardAccount)
AND	St.Instance = H.StudentInstance
AND	cu.instance = h.customerbilltoInstance
AND 	D.DelFlag=0	
AND     c.startDate >=  @FiscalStartDate
AND 	(EXISTS (SELECT  1
		 FROM QSPCanadaOrderManagement.dbo.CustomerOrderDetailRemitHistory RH
		 WHERE  RH.CustomerOrderHeaderInstance = D.CustomerOrderHeaderInstance 
		 AND RH.TransID = D.TransID
		 AND	RH.Status IN (42000, 42001, 42004)
		 AND DateChanged BETWEEN CONVERT(nvarchar, @StartDate,101)AND CONVERT(nvarchar, @Enddate,101)
		 ) 
	 OR D.StatusInstance=508 )
AND EXISTS (SELECT 1 from #all
			WHERE #all.OrderID =b.OrderID)

----------Output
Select * from #ITEM


DROP TABLE #ALL
DROP TABLE #ITEM
DROP TABLE #PostCardAccount


END
GO
