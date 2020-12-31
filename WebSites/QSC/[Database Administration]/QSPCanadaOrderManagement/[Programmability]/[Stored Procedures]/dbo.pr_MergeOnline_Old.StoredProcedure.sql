USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_MergeOnline_Old]    Script Date: 06/07/2017 09:20:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE    Procedure [dbo].[pr_MergeOnline_Old]
	@orderid int
as
set nocount on
declare @campaignid int
select @campaignid = campaignid from batch where orderid=@orderid
/*
**  All the kids in this order
*/
SELECT distinct
		TOP 10000 
		--*
--		A.CampaignId
		D.StudentInstance
		,C.FirstName
		,C.LastName
		into #names
	
	FROM
		Batch A
		INNER JOIN Teacher B ON A.AccountID = B.AccountID 
		INNER JOIN Student C ON B.Instance = C.TeacherInstance
		INNER JOIN CustomerOrderHeader D ON 								
			 C.Instance =  D.StudentInstance
		INNER JOIN CustomerOrderDetail E ON D.Instance = E.CustomerOrderHeaderInstance
	WHERE
		IsNull(A.IncentiveCalculationStatus, 0) <> 1 
		
		AND E.ProductType NOT IN (46008)      -- NOT AN INCENTIVE 
		AND A.OrderId = @orderid
		and OrderBatchDate=date and OrderbatchId = id


/*
**  Look for online kids where we match
**
*/
SELECT
		TOP 2000 
		--*
		A.OrderID as OnLineOrderID,
		@orderid as LandedID,
		D.Instance,
		E.TransID,
		D.StudentInstance as OnlineStudentInstance
		,N.StudentInstance as LandedStudentInstance	
		into #kidswefound
	FROM
		Batch A
		INNER JOIN Teacher B ON A.AccountID = B.AccountID 
		INNER JOIN Student C ON B.Instance = C.TeacherInstance
		Inner Join #Names N on C.LastName = N.LastName
			and C.FirstName = N.FirstName  
		INNER JOIN CustomerOrderHeader D ON C.Instance =  D.StudentInstance
		INNER JOIN CustomerOrderDetail E ON D.Instance = E.CustomerOrderHeaderInstance
		--INNER JOIN #Temp1 F ON F.CampaignId = A.CampaignId
	WHERE
--		IsNull(A.IncentiveCalculationStatus, 0) = 1 
--							AND A.Date = B.OrderBatchDate
		 OrderBatchDate=date and OrderbatchId = id
--		and A.orderid=85061 
		and A.CampaignID=@campaignid
		and orderqualifierid=39009
	order by n.lastname,n.firstname


/*
**  Change the order to landed student instance for kids we found
**  to be safe only include online batches
*/
--select * from #kidswefound
update CustomerOrderHeader set studentinstance=landedstudentinstance   
	from CustomerOrderheader,#kidswefound,Batch
	where studentinstance=onlinestudentinstance
	and  OrderBatchDate=date 
	and OrderbatchId = id
	and Batch.CampaignID = @campaignid
	and  orderqualifierid=39009



/*
**  All the online kids into the mapping table
*/
insert Into OnLineOrderMappingTable
SELECT
		TOP 2000 
		--*
		@OrderID as LandedOrderID,
		A.OrderID as OnlineOrderID,
		D.Instance,
		E.Transid		
		,D.StudentInstance
		
	FROM
		Batch A
		INNER JOIN Teacher B ON A.AccountID = B.AccountID 
		INNER JOIN Student C ON B.Instance = C.TeacherInstance		
		INNER JOIN CustomerOrderHeader D ON C.Instance =  D.StudentInstance
		INNER JOIN CustomerOrderDetail E ON D.Instance = E.CustomerOrderHeaderInstance
		--INNER JOIN #Temp1 F ON F.CampaignId = A.CampaignId
	WHERE
--		IsNull(A.IncentiveCalculationStatus, 0) = 1 
--							AND A.Date = B.OrderBatchDate
		 OrderBatchDate=date and OrderbatchId = id
		and A.CampaignID=@campaignid
		and orderqualifierid=39009
--		and c.instance not in (select OnlineStudentInstance from #kidswefound)
	order by c.lastname,c.firstname
GO
