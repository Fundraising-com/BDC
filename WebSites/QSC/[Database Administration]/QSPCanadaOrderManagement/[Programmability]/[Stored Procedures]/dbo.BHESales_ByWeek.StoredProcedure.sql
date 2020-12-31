USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[BHESales_ByWeek]    Script Date: 06/07/2017 09:19:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[BHESales_ByWeek] @Fromdate DateTime, @ToDate DateTime 
AS

SET NOCOUNT ON

/********************************* **********************************************
MS Oct 25, 2007
*********************************************************************************/
BEGIN

DECLARE @Now DateTime
DECLARE @StartDate DateTime

SET @Now = CONVERT(DateTime,CONVERT(Varchar(10),GETDATE(),101))
SET @StartDate =  @Now  -7

IF IsNull(@Fromdate, '01/01/1955')='01/01/1955'
BEGIN
	SET @Fromdate=@StartDate
END

IF IsNull(@ToDate, '01/01/1955')='01/01/1955'
BEGIN
	SET @ToDate=@Now
END

SELECT @StartDate StartDate, @Now EndDate,
 	b.OrderQualifierid,
	c.IsStaffOrder,
	-------------------------- BOOK ----------------------------------
	d6.ProductType ProductTypeBook,
	d6.ProductName ProductNameBook, 
	d6.productCode productCodeBook,
	----------------Online -------------------------------------------
	 --- Online Staff
	 CASE OrderQualifierId
	 WHEN  39009 THEN CASE C.IsStaffOrder
			  WHEN 1 THEN SUM(IsNull(d6.Quantity,0))
			  ELSE 0
			  END
	 ELSE 0 
 	 END BookOnlineQtyStaff,
	 CASE OrderQualifierId
	 WHEN  39009 THEN CASE C.IsStaffOrder
			  WHEN 1 THEN SUM(IsNull(d6.Price,0))
			  ELSE 0
			  END
	 ELSE 0 
 	 END BookOnlineSalesStaff,
	 ---Online Regular
	 CASE OrderQualifierId
	 WHEN  39009 THEN CASE C.IsStaffOrder
			  WHEN 0 THEN SUM(IsNull(d6.Quantity,0))
			  ELSE 0
			  END
	 ELSE 0 
 	 END BookOnlineQtyReg,
	 CASE OrderQualifierId
	 WHEN  39009 THEN CASE C.IsStaffOrder
			  WHEN 0 THEN SUM(IsNull(d6.Price,0))
			  ELSE 0
			  END
	 ELSE 0 
 	 END BookOnlineSalesReg,
	 ------------------------ Landed -----------------------
	 ---- Landed Staff
	 CASE OrderQualifierId
	 WHEN  39009 Then 0
	 ELSE  CASE C.IsStaffOrder
			  WHEN 1 THEN SUM(IsNull(d6.Quantity,0))
			  ELSE 0
			  END
 	 END BookLandedQtyStaff,
	 CASE OrderQualifierId
	 WHEN  39009 THEN 0
	 ELSE  CASE C.IsStaffOrder
			  WHEN 1 THEN SUM(IsNull(d6.Price,0))
			  ELSE 0
			  END 
 	 END BookLandedSalesStaff,
         --- Landed Regular
	 CASE OrderQualifierId
	 WHEN  39009 Then 0
	 ELSE  CASE C.IsStaffOrder
			  WHEN 1 THEN 0
			  ELSE SUM(IsNull(d6.Quantity,0))
			  END
 	 END BookLandedQtyReg,
	 CASE OrderQualifierId
	 WHEN  39009 THEN 0
	 ELSE  CASE C.IsStaffOrder
			  WHEN 1 THEN 0
			  ELSE SUM(IsNull(d6.Price,0))
			  END 
 	 END BookLandedSalesReg,
	--------------------------------Music -------------------------------
	d7.ProductType ProductTypeCD,
	d7.ProductName ProductNameCD, 
	d7.productCode productCodeCD,
	----------------Online -------------------------------------------
	 --- Online Staff
	 CASE OrderQualifierId
	 WHEN  39009 THEN CASE C.IsStaffOrder
			  WHEN 1 THEN SUM(IsNull(d7.Quantity,0))
			  ELSE 0
			  END
	 ELSE 0 
 	 END CDOnlineQtyStaff,
	 CASE OrderQualifierId
	 WHEN  39009 THEN CASE C.IsStaffOrder
			  WHEN 1 THEN SUM(IsNull(d7.Price,0))
			  ELSE 0
			  END
	 ELSE 0 
 	 END CDOnlineSalesStaff,
	 ---Online Regular
	 CASE OrderQualifierId
	 WHEN  39009 THEN CASE C.IsStaffOrder
			  WHEN 0 THEN SUM(IsNull(d7.Quantity,0))
			  ELSE 0
			  END
	 ELSE 0 
 	 END CDOnlineQtyReg,
	 CASE OrderQualifierId
	 WHEN  39009 THEN CASE C.IsStaffOrder
			  WHEN 0 THEN SUM(IsNull(d7.Price,0))
			  ELSE 0
			  END
	 ELSE 0 
 	 END CDOnlineSalesReg,
	 ------------------------ Landed -----------------------
	 ---- Landed Staff
	 CASE OrderQualifierId
	 WHEN  39009 Then 0
	 ELSE  CASE C.IsStaffOrder
			  WHEN 1 THEN SUM(IsNull(d7.Quantity,0))
			  ELSE 0
			  END
 	 END CDLandedQtyStaff,
	 CASE OrderQualifierId
	 WHEN  39009 THEN 0
	 ELSE  CASE C.IsStaffOrder
			  WHEN 1 THEN SUM(IsNull(d7.Price,0))
			  ELSE 0
			  END 
 	 END CDLandedSalesStaff,
         --- Landed Regular
	 CASE OrderQualifierId
	 WHEN  39009 Then 0
	 ELSE  CASE C.IsStaffOrder
			  WHEN 1 THEN 0
			  ELSE SUM(IsNull(d7.Quantity,0))
			  END
 	 END CDLandedQtyReg,
	 CASE OrderQualifierId
	 WHEN  39009 THEN 0
	 ELSE  CASE C.IsStaffOrder
			  WHEN 1 THEN 0
			  ELSE SUM(IsNull(d7.Price,0))
			  END 
 	 END CDLandedSalesReg,
	-------------------------- Video --------------------------------
	d12.ProductType ProductTypeVideo,
	d12.ProductName ProductNameVideo, 
	d12.productCode productCodeVideo,
	----------------Online -------------------------------------------
	 --- Online Staff
	 CASE OrderQualifierId
	 WHEN  39009 THEN CASE C.IsStaffOrder
			  WHEN 1 THEN SUM(IsNull(d12.Quantity,0))
			  ELSE 0
			  END
	 ELSE 0 
 	 END VideoOnlineQtyStaff,
	 CASE OrderQualifierId
	 WHEN  39009 THEN CASE C.IsStaffOrder
			  WHEN 1 THEN SUM(IsNull(d12.Price,0))
			  ELSE 0
			  END
	 ELSE 0 
 	 END VideoOnlineSalesStaff,
	 ---Online Regular
	 CASE OrderQualifierId
	 WHEN  39009 THEN CASE C.IsStaffOrder
			  WHEN 0 THEN SUM(IsNull(d12.Quantity,0))
			  ELSE 0
			  END
	 ELSE 0 
 	 END VideoOnlineQtyReg,
	 CASE OrderQualifierId
	 WHEN  39009 THEN CASE C.IsStaffOrder
			  WHEN 0 THEN SUM(IsNull(d12.Price,0))
			  ELSE 0
			  END
	 ELSE 0 
 	 END VideoOnlineSalesReg,
	 ------------------------ Landed -----------------------
	 ---- Landed Staff
	 CASE OrderQualifierId
	 WHEN  39009 Then 0
	 ELSE  CASE C.IsStaffOrder
			  WHEN 1 THEN SUM(IsNull(d12.Quantity,0))
			  ELSE 0
			  END
 	 END VideoLandedQtyStaff,
	 CASE OrderQualifierId
	 WHEN  39009 THEN 0
	 ELSE  CASE C.IsStaffOrder
			  WHEN 1 THEN SUM(IsNull(d12.Price,0))
			  ELSE 0
			  END 
 	 END VideoLandedSalesStaff,
         --- Landed Regular
	 CASE OrderQualifierId
	 WHEN  39009 Then 0
	 ELSE  CASE C.IsStaffOrder
			  WHEN 1 THEN 0
			  ELSE SUM(IsNull(d12.Quantity,0))
			  END
 	 END VideoLandedQtyReg,
	 CASE OrderQualifierId
	 WHEN  39009 THEN 0
	 ELSE  CASE C.IsStaffOrder
			  WHEN 1 THEN 0
			  ELSE SUM(IsNull(d12.Price,0))
			  END 
 	 END VideoLandedSalesReg
INTO #Sales
FROM    QSPCanadaOrderManagement.dbo.CustomerOrderHeader H		(NOLOCK)
	LEFT JOIN QSPCanadaOrderManagement.dbo.CustomerOrderDetail D6	(NOLOCK) ON H.Instance = D6.CustomerOrderHeaderInstance and D6.ProductType=46006 AND D6.DelFlag=0 And  D6.statusInstance in (508)
	LEFT JOIN QSPCanadaOrderManagement.dbo.CustomerOrderDetail D7	(NOLOCK) ON H.Instance = D7.CustomerOrderHeaderInstance and D7.ProductType=46007 AND D7.DelFlag=0 And     D7.statusInstance in (508)
	LEFT JOIN QSPCanadaOrderManagement.dbo.CustomerOrderDetail D12	(NOLOCK) ON H.Instance = D12.CustomerOrderHeaderInstance and D12.ProductType=46012 AND D12.DelFlag=0 And     D12.statusInstance in (508)
	INNER JOIN QSPcanadaOrdermanagement.dbo.Batch B	(NOLOCK) ON B.Id=H.OrderBatchId AND B.Date=H.OrderBatchDate
	INNER JOIN QSPCanadaCommon.dbo.CAccount A 	(NOLOCK) ON B.AccountID =A.Id 
     	INNER JOIN QSPCanadaCommon.dbo.Campaign C	(NOLOCK) ON B.CampaignId=C.Id
WHERE   B.StatusInstance <> 40005
And     b.OrderQualifierID in (39001,39002,39003,39009,39013,39015,39020,39021)
AND 	CONVERT(DateTime,CONVERT(Varchar(10),B.DATE,101)) BETWEEN @Fromdate AND @ToDate
AND     C.FMID= IsNull(Null,C.FMID) 
GROUP BY c.IsStaffOrder,
	b.OrderQualifierid,d6.ProductType,d6.ProductName, d6.productCode,
	d7.ProductType,d7.ProductName, d7.productCode,
	d12.ProductType,d12.ProductName, d12.productCode
--ORDER BY F.lastname, F.firstname --, A.id 

--Select * from #sales

Create table #BHESalesOutput (  Type Varchar(15),
			             Productname varchar(50),
				ProductCode varchar(10),
				QTYOnlineStaff  Numeric(14,2),
				GrossOnlineStaff Numeric(14,2),
				QTYOnlineReg  Numeric(14,2),
				GrossOnlineReg Numeric(14,2),
				QtyLandedStaff Numeric(14,2),
				GrossLandedStaff Numeric(14,2),
				QTYLandedReg Numeric(14,2),
				GrossLandedReg Numeric(14,2))

Insert #BHESalesOutput
Select 'Books' Type,ProductNameBook ProductName,ProductCodeBook ProductCode,
	Sum(BookOnlineQtyStaff)QTYOnlineStaff, 
	Sum(BookOnlineSalesStaff)GrossOnlineStaff,
	Sum(BookOnlineQtyReg)  QTYOnlineReg,
	Sum(BookOnlineSalesReg) GrossOnlineReg,
	--Landed
	Sum(BooklandedQtyStaff) QtyLandedStaff,
	Sum(BookLandedSalesStaff) GrossLandedStaff,
	Sum(BookLandedQtyReg)  QTYLandedReg,
	Sum(BookLandedSalesReg) GrossLandedReg
from #SALES Where ProductTypeBook=46006
Group BY ProductNameBook,ProductCodeBook
UNION
Select 'Music' Type,ProductNameCD ProductName,ProductCodeCD ProductCode,
	Sum(CDOnlineQtyStaff)QTYOnlineStaff, 
	Sum(CDOnlineSalesStaff)GrossOnlineStaff,
	Sum(CDOnlineQtyReg)  QTYOnlineReg,
	Sum(CDOnlineSalesReg) GrossOnlineReg,
	--Landed
	Sum(CDlandedQtyStaff) QtyLandedStaff,
	Sum(CDLandedSalesStaff) LandedSalesStaff,
	Sum(CDLandedQtyReg)  QTYLandedReg,
	Sum(CDLandedSalesReg) GrossLandedReg
from #SALES Where ProductTypeCD=46007 --Music
Group BY ProductNameCD,ProductCodeCD
UNION
Select 'Video' Type,ProductNameVideo ProductName,ProductCodeVideo ProductCode,
	Sum(VideoOnlineQtyStaff)QTYOnlineStaff, 
	Sum(VideoOnlineSalesStaff)GrossOnlineStaff,
	Sum(VideoOnlineQtyReg)  QTYOnlineReg,
	Sum(VideoOnlineSalesReg) GrossOnlineReg,
	--Landed
	Sum(VideoLandedQtyStaff) QtyLandedStaff,
	Sum(VideoLandedSalesStaff) LandedSalesStaff,
	Sum(VideoLandedQtyReg)  QTYLandedReg,
	Sum(VideoLandedSalesReg) GrossLandedReg
from #SALES Where ProductTypeVideo=46012 --Video
Group BY ProductNameVideo,ProductCodeVideo


Select @Fromdate FromDate, @ToDate ToDate,* from #BHESalesOutput

Drop Table #BHESalesOutput
Drop Table #Sales

End
GO
