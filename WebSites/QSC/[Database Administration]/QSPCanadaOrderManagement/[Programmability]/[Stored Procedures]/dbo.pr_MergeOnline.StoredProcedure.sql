USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_MergeOnline]    Script Date: 06/07/2017 09:20:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[pr_MergeOnline]
	@orderid int
as
set nocount on
declare @campaignid int
select @campaignid = campaignid from batch where orderid=@orderid
/*
**  All the kids in this order
*/
/*
SELECT distinct
		
		--*
--		A.CampaignId
		D.StudentInstance
		,C.FirstName
		,C.LastName
		,C.TeacherInstance 
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

*/
/*
**  Look for online kids where we match
**
*/
/*
SELECT
		TOP 2000 
		--*
		A.OrderID as OnLineOrderID,
		@orderid as LandedID,
		D.Instance,
		E.TransID,
		D.StudentInstance as OnlineStudentInstance
		,N.StudentInstance as LandedStudentInstance	
		,C.TeacherInstance as OnlineTeacherInstance
		,N.TeacherInstance as LandedTeacherInstance
		into #kidswefound
	FROM
		Batch A
		INNER JOIN Teacher B ON A.AccountID = B.AccountID 
		INNER JOIN Student C ON B.Instance = C.TeacherInstance
		Inner Join #Names N on C.LastName = N.LastName
			and C.FirstName = N.FirstName  
			
		INNER JOIN CustomerOrderHeader D ON C.Instance =  D.StudentInstance
		INNER JOIN CustomerOrderDetail E ON D.Instance = E.CustomerOrderHeaderInstance
	WHERE
		 OrderBatchDate=date and OrderbatchId = id
		and A.CampaignID=@campaignid
		and orderqualifierid=39009
	order by n.lastname,n.firstname
*/

/*
select * from #kidswefound where 
OnlineTeacherInstance=LandedTeacherInstance
and OnlineStudentInstance<>LandedStudentInstance
*/
/*
update CustomerOrderheader set studentinstance=landedstudentinstance  
from CustomerOrderheader,#kidswefound,Batch
	where studentinstance=onlinestudentinstance
	and  OrderBatchDate=date 
	and OrderbatchId = id
	and Batch.CampaignID = @campaignid
	and  orderqualifierid=39009
	and OnlineTeacherInstance=LandedTeacherInstance
	and OnlineStudentInstance<>LandedStudentInstance

*/

insert Into OnLineOrderMappingTable
SELECT
		
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
	WHERE
		 OrderBatchDate=date and OrderbatchId = id
		and A.CampaignID=@campaignid
		and orderqualifierid=39009
		and ordertypecode <> 41012 --KT 12/22/09 Exlude Free subs

		and E.DelFlag = 0
	order by c.lastname,c.firstname

--drop table #names
--drop table #kidswefound
GO
