USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[KanataPrizeManagementReport]    Script Date: 06/07/2017 09:17:24 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE  PROCEDURE [dbo].[KanataPrizeManagementReport] @AccountId Int, 
							@CampaignId Int, 
							@FMId Int , 
							@CAProgram Int,
							@OrderDateFrom Datetime, 
							@OrderDateTo Datetime ,
							@SortBy Varchar(10)
 AS


If @OrderDateFrom = '01/01/1995'
Begin
	Set @OrderDateFrom = null;
End

If @OrderDateTo = '01/01/1995'
Begin
	Set @OrderDateTo = null;
End

Declare @AllOrders Table(
			BillAccountId 	Int, 
			BillAccount    	Varchar(50),   
			CA		Int, 
			CAStatus	Varchar(50), 
		        	StartDate	Varchar(10), 
			EndDate	Varchar(10),  
			Programs	Varchar(120),
			FMId		Int,
			FM		Varchar(100), 
			OrderID		Int, 
			OrderDate	Varchar(10),  
		        	BatchStatus	Varchar(50),  
			OrderType	Varchar(50),  
			Qualifier		Varchar(50),  
			OrderAmount	Numeric(12,2),
			FivePercentAllow Numeric(12,2),
			TotalAwards	Numeric(12,2)
			)	
Declare @Summary Table(
			BillAccountId 	Int, 
			BillAccount    	Varchar(50),   
			CA		Int, 
			CAStatus	Varchar(50), 
		        	StartDate	Varchar(10), 
			EndDate	Varchar(10),  
			Programs	Varchar(120),
			FMId		Int,
			FM		Varchar(100), 
			OrderAmount	Numeric(12,2),
			FivePercentAllow Numeric(12,2),
			TotalAwards	Numeric(12,2),
			BillableAmount   Numeric(12,2)
			)	

Insert  Into @AllOrders
Select	
	b.AccountID AS BillAccountId, 
	BillToAccount.Name AS BillAccount, 
	c.ID AS CA, 
	CD.Description AS CAStatus, 
        	Convert(varchar,c.StartDate,101) StartDate,
	Convert(varchar,c.EndDate,101) EndDate, 
	QspCanadaOrderManagement.dbo.Udf_ProgramsByCampaign(C.ID) Programs,
	fm.FMId,
	fm.FirstName+' '+fm.LastName FM, 
	b.OrderID, 
	Convert(varchar,b.Date,101) AS OrderDate, 
        	CD1.Description AS BatchStatus, 
	CD2.Description AS OrderType, 
	CD3.Description AS Qualifier, 
	Case b.OrderQualifierId
	When 39006 Then 0	
	Else Round(SUM(d.Price),0) 
	End AS OrderAmount,

	Case b.OrderQualifierId
	When 39006 Then 0	
	Else Round((SUM(d.Price)* .05) ,0)
	End AS FivePercentAllow,

	Case b.OrderQualifierId
	When 39006 Then  SUM(d.CatalogPrice*quantity) 
	Else 0
	End AS TotalAwards
From 
             QSPCanadaOrderManagement.dbo.CreditCardPayment CCP INNER JOIN
             QSPCanadaOrderManagement.dbo.CustomerPaymentHeader cph ON CCP.CustomerPaymentHeaderInstance = cph.Instance RIGHT OUTER JOIN
             QSPCanadaOrderManagement.dbo.Batch b INNER JOIN
             QSPCanadaOrderManagement.dbo.CustomerOrderHeader h ON b.ID = h.OrderBatchID AND b.[Date] = h.OrderBatchDate INNER JOIN
             QSPCanadaOrderManagement.dbo.CustomerOrderDetail d ON h.Instance = d.CustomerOrderHeaderInstance INNER JOIN
             QSPCanadaCommon.dbo.CodeDetail CD1 ON b.StatusInstance = CD1.Instance INNER JOIN
             QSPCanadaCommon.dbo.CodeDetail CD2 ON b.OrderTypeCode = CD2.Instance INNER JOIN
             QSPCanadaCommon.dbo.CodeDetail CD3 ON b.OrderQualifierID = CD3.Instance ON cph.CustomerOrderHeaderInstance = h.Instance LEFT OUTER JOIN
             QSPCanadaCommon.dbo.CAccount BillToAccount ON b.AccountID = BillToAccount.Id LEFT OUTER JOIN
             QSPCanadaCommon.dbo.CodeDetail CD INNER JOIN
             QSPCanadaCommon.dbo.Campaign c ON CD.Instance = c.Status ON b.CampaignID = c.ID LEFT OUTER JOIN
             QSPCanadaCommon.dbo.FieldManager fm ON c.FMID = fm.FMID
/*   
        QSPCanadaOrderManagement.dbo.Batch b INNER JOIN
        QSPCanadaOrderManagement.dbo.CustomerOrderHeader h ON b.ID = h.OrderBatchID AND b.[Date] = h.OrderBatchDate INNER JOIN
        QSPCanadaOrderManagement.dbo.CustomerOrderDetail d ON h.Instance = d.CustomerOrderHeaderInstance INNER JOIN
        QSPCanadaCommon.dbo.CodeDetail CD1 ON b.StatusInstance = CD1.Instance INNER JOIN
        QSPCanadaCommon.dbo.CodeDetail CD2 ON b.OrderTypeCode = CD2.Instance INNER JOIN
        QSPCanadaCommon.dbo.CodeDetail CD3 ON b.OrderQualifierID = CD3.Instance LEFT OUTER JOIN
        QSPCanadaCommon.dbo.CAccount BillToAccount ON b.AccountID = BillToAccount.Id LEFT OUTER JOIN
        QSPCanadaCommon.dbo.CodeDetail CD INNER JOIN
        QSPCanadaCommon.dbo.Campaign c ON CD.Instance = c.Status ON b.CampaignID = c.ID LEFT OUTER JOIN
        QSPCanadaCommon.dbo.FieldManager fm ON c.FMID = fm.FMID
*/
Where  b.OrderQualifierID NOT IN (39008, 39012,39014,39017,39018,39019) 	--Cust Sevice, OrderCorrect, CCreprocess Courtesy, Signing Bonus,Kanata Psolver,Gift Psolver
And b.AccountId = IsNull(@AccountId, b.AccountId)
And b.CampaignId = IsNull(@CampaignId, b.CampaignId)
And fm.FMId = IsNull(@FmId,fm.FMId)
And b.OrderTypeCode not in( 41006,41007,41011)	--FM ,FMBULK,Closeout
And d.Delflag =0			   		 --Deleted
And b.StatusInstance Not In(40005) 		--Cancelled
--And d.StatusInstance Not In(501,506)		--Error , Void
And d.StatusInstance  In(507,508,512,513,514)		--Error , Void
And b.Date >=  IsNull(@OrderDateFrom,  b.Date) 
And b.Date <=  IsNull(@OrderDateTo,  b.Date) 
And IsNull(c.IsstaffOrder,0) <> 1			--Not Staff CA (No awards)
And (	(
			h.PaymentMethodInstance <> 50002 --Credit Cards = 50003 Visa and 50004 MC
			And  ISNULL(ccp.StatusInstance,19001) IN (19000)  -- Credit Card good payment --should be 19000 for good. 
	)
	Or
	(
			h.PaymentMethodInstance = 50002 --Cash/Check
	)
       )
And c.Id In (Select CampaignId From QSPCanadaCommon.dbo.CampaignProgram 
	   	           Where ProgramID = IsNull(@CAProgram, ProgramID) and DeletedTF=0 )	
Group By
	b.AccountID, 
	BillToAccount.Name, 
	c.ID, c.StartDate, 
	c.EndDate, fm.FMId,
	fm.FirstName, 
	fm.LastName, 
	b.OrderID, 
	b.[Date], 
             b.OrderQualifierId,
	CD.Description, 
	CD1.Description, 
	CD2.Description, 
	CD3.Description
Order By b.AccountID,c.ID,orderId

Insert into @Summary
Select   BillAccountId, 
	BillAccount, 
	CA, 
	CAStatus, 
        	StartDate,
	EndDate, 
	Programs,
	FMId,
	FM,
	Sum(OrderAmount)OrderTotal,
	Sum(FivePercentAllow) TotalFivePercent,
	Sum(TotalAwards) TotalAwards,
	Case Sign( (Sum(FivePercentAllow)- Sum(TotalAwards))  )
	When -1 Then (Sum(TotalAwards)- Sum(FivePercentAllow))		--If Total Awards amount is greater than Allowance
	Else 0
	End  As BillableAmount
from @AllOrders
Group By BillAccountId, 
	BillAccount, 
	CA, 
	CAStatus, 
        	StartDate,
	EndDate, 
	Programs,
	FMId,
	FM


If Upper(@SortBy) = 'ACCOUNT'
Begin
	Select * From @Summary Where BillableAmount > 0 
	Order By BillAccount
End
Else If Upper(@SortBy) = 'FM'
	Begin
	Select * From @Summary Where BillableAmount > 0 
	Order By FM
	
	End
Else
	Select * From @Summary Where BillableAmount > 0 
	Order By BillAccountId
GO
