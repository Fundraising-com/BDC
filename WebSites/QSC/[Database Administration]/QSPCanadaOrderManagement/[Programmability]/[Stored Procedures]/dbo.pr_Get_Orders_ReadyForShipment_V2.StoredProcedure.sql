USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Get_Orders_ReadyForShipment_V2]    Script Date: 06/07/2017 09:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE      PROCEDURE [dbo].[pr_Get_Orders_ReadyForShipment_V2]

@Top int,
@ProdLine varchar(250),
@IndividualOrders int =0,
@OrderList Varchar(500),
@PrintStatus int,
@ProductCode varchar(500),
@BackOrderOnly int,
@ShipmentGroupID int

AS

Declare @NewproductCode varchar(600)
Declare @StartLoc int
Declare @CommaLoc int


--to remove extra ',' from the Order list
--MS Aug 15, 2005
Select @OrderList=Replace(@orderList,',,',',')

if Len(@OrderList) > 0 And Substring(@orderList,Len(@orderList),1) = ','
Begin
     Select @orderList = Substring(@orderList,1,Len(@orderList)-1)
End


--ProductCode list, make them string
If IsNull(@productCode,'') <> ''
Begin

Set  @productCode=@productCode+','
Select @productCode=Replace(@productCode,',,',',')

Set @StartLoc =1

While CharIndex(',',@productCode,@StartLoc) >0
Begin
	Select @CommaLoc = CharIndex(',',@productCode,@StartLoc)
	Select @NewproductCode= Isnull(@NewproductCode,'') +  ','+''''+ Substring(@productCode,@StartLoc,@CommaLoc-1)+'''' 
	Select @productCode = Substring (@productCode,@CommaLoc+1,len(@productCode))
End

If CharIndex(',',@NewproductCode,1)=1
Begin
   Set @NewproductCode= substring(@NewproductCode,2,Len(@NewproductCode))
End


Set @ProductCode = @NewproductCode
End

/*
Select @ProductCode=Replace(@ProductCode,',,',',')

if Len(@ProductCode) > 0 And Substring(@ProductCode,Len(@ProductCode),1) = ','
Begin
     Select @ProductCode = Substring(@ProductCode,1,Len(@ProductCode)-1)
End
*/


 set nocount on
 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[#TempShip]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
 drop table [dbo].[#TempShip]

CREATE TABLE [dbo].[#TempShip] (
	[OrderId] [int] NOT NULL ,
	[BatchID] [int] NOT NULL,
	[BatchDate] [datetime] NOT NULL,
	[ShipToGroupId] [int] NULL ,
	[CampaignId] [int] NOT NULL ,
	[For] [varchar] (500) NULL ,
	[ShipToFMId] [varchar](4) NULL ,
	[CustomerOrderHeaderInstance] [int] NOT NULL ,
	[TransID] [int] NOT NULL ,
	[IsSplit] [bit] NULL,
	[DatePrinted] [datetime] NULL,
	[SuppliesDeliveryDate] [datetime] NULL,
	[IsPrinted] [int] NULL,
	[ShipmentGroupID] [int] NULL,
	[ShipmentGroupName] [nvarchar] (50) NULL,
	[OrderShippingDate] [char] (12) NULL
) ON [PRIMARY]


declare @str varchar(4000), @PrintClause varchar(200)

IF @PrintStatus = -1 -- both, printed & unprinted
  BEGIN
     SET @PrintStatus = Null
  END  


if(@IndividualOrders <> 0)
begin
	select @str ='INSERT INTO
		#TempShip
	SELECT DISTINCT
		TOP ' + cast(  @top as varchar(10)) + '
		 
		 b.OrderId
		, b.ID
		, b.Date
		, b.ShipToAccountId
		, b.CampaignId
		, ''''
		, b.ShipToFMID
		, C.CustomerOrderHeaderInstance
		, C.TransID
		, 
		IsSplit = Case
			WHEN b.StatusInstance = 40010 THEN 0
			WHEN b.StatusInstance = 40014 THEN 1
		END
		,rrb.DatePrinted
		,camp.SuppliesDeliveryDate
		,IsNull(rrb.IsPrinted,0) IsPrinted
		, sg.ShipmentGroupID
		, sg.ShipmentGroupName
		, CONVERT(char(12), DATEADD(dd, -(prov.Lapse_Days_Delivery + prov.Lapse_Days_Field_Supply_Prep), ISNULL(b.OrderDeliveryDate, b.date)), 101)
	FROM		Batch b
	JOIN		CustomerOrderHeader coh
					ON	coh.OrderBatchID = b.ID
					AND	coh.OrderBatchDate = b.Date
	JOIN		CustomerOrderDetail cod
					ON	cod.CustomerOrderHeaderInstance = coh.Instance
	JOIN		BatchDistributionCenter bdc
					ON	bdc.BatchDate = b.Date
					AND	bdc.BatchID = b.ID
	LEFT JOIN	ReportRequestBatch rrb
					ON	rrb.BatchOrderID = b.OrderID
	LEFT JOIN	ShipmentGroup sg 
					ON sg.ShipmentGroupID = rrb.ShipmentGroupID
	JOIN		QSPCanadaCommon..Campaign camp
					ON	camp.ID = b.CampaignID
	LEFT JOIN	QSPCanadaCommon..QSPProductLine pl
					ON	pl.ID = bdc.QSPProductLine --cod.ProductType
	LEFT JOIN	QSPCanadaCommon..CAccount acc ON acc.ID = camp.ShipToAccountID
	LEFT JOIN	QSPCanadaCommon..Address ad ON ad.AddressListID = acc.AddressListID AND ad.address_type = 54001
	LEFT JOIN	QSPCanadaCommon..Province prov ON prov.Province_Code = ad.stateProvince
	LEFT JOIN   (ShipmentOrder so
               JOIN Shipment s ON s.ID = so.ShipmentID AND s.ProcessedDate IS NULL)
                  ON    so.OrderID = b.OrderID
                  AND   s.ShipmentGroupID = sg.ShipmentGroupID
   WHERE		bdc.StatusInstance IN (40010,40014)
	AND			b.StatusInstance NOT IN (40013,40005)
	AND			b.Date >=''7/1/05''
	AND			cod.StatusInstance NOT IN (508, 513)
	AND			cod.delFlag <> 1
	AND			(sg.ShipmentGroupID IS NULL OR pl.ShipmentGroupID IS NULL OR sg.ShipmentGroupID = pl.ShipmentGroupID)
   AND         (s.ID IS NULL)
	/*AND			((cod.DistributionCenterID in (1, 2) AND cod.ProductType in ('+@ProdLine+')) OR b.OrderID IN (SELECT DISTINCT LandedOrderID  
																								FROM OnlineOrderMappingTable  
																								WHERE OnlineOrderID IN (SELECT	DISTINCT b2.OrderID
																														FROM	Batch b2
																														JOIN	CustomerOrderHeader coh2
																																	ON	coh2.OrderBatchID = b2.ID
																																	AND	coh2.OrderBatchDate = b2.Date
																														JOIN	CustomerOrderDetail cod2
																																	ON	cod2.CustomerOrderHeaderInstance = coh2.Instance
																														JOIN	QSPCanadaCommon..QSPProductLine pl2
																																	ON	pl2.ID = cod2.ProductType
																														WHERE	cod2.IsShippedToAccount = 1 
																														AND		b2.OrderQualifierID = 39009
																														AND		b2.StatusInstance not in (40005, 40013)
																														AND		cod2.StatusInstance not in ( 508, 513)
																														AND		cod2.DelFlag <> 1
																														AND		cod2.ProductType in ('+@ProdLine+')
																														AND		sg.ShipmentGroupID = pl2.ShipmentGroupID
																														--AND		(ISNULL(sg.ShipmentGroupID, pl2.ShipmentGroupID) = pl2.ShipmentGroupID)
																														AND		cod2.DistributionCenterID in (1, 2))))*/
	AND		(b.OrderQualifierID <> 39009 OR (cod.IsShippedToAccount = 0 AND cod.DistributionCenterID IN (1, 2) AND cod.StatusInstance NOT IN (508, 513)))'

end
else
begin
	select @str ='INSERT INTO
		#TempShip
	SELECT DISTINCT
		TOP ' + cast(  @top as varchar(10)) + '
		 
		 b.OrderId
		, b.ID
		, b.Date
		, b.ShipToAccountId
		, b.CampaignId
		, ''''
		, b.ShipToFMID
		, 0
		, 0
		, 
		IsSplit = Case
			WHEN b.StatusInstance = 40010 THEN 0
			WHEN b.StatusInstance = 40014 THEN 1
		END
		,rrb.DatePrinted
		,camp.SuppliesDeliveryDate
		,IsNull(rrb.IsPrinted,0) IsPrinted
		, sg.ShipmentGroupID
		, sg.ShipmentGroupName
		, CONVERT(char(12), DATEADD(dd, -(prov.Lapse_Days_Delivery + prov.Lapse_Days_Field_Supply_Prep), ISNULL(b.OrderDeliveryDate, b.date)), 101)
	FROM		Batch b
	JOIN		CustomerOrderHeader coh
					ON	coh.OrderBatchID = b.ID
					AND	coh.OrderBatchDate = b.Date
	JOIN		CustomerOrderDetail cod
					ON	cod.CustomerOrderHeaderInstance = coh.Instance
	JOIN		BatchDistributionCenter bdc
					ON	bdc.BatchDate = b.Date
					AND	bdc.BatchID = b.ID
	LEFT JOIN	ReportRequestBatch rrb
					ON	rrb.BatchOrderID = b.OrderID
	LEFT JOIN	ShipmentGroup sg 
					ON sg.ShipmentGroupID = rrb.ShipmentGroupID
	JOIN		QSPCanadaCommon..Campaign camp
					ON	camp.ID = b.CampaignID
	LEFT JOIN	QSPCanadaCommon..QSPProductLine pl
					ON	pl.ID = bdc.QSPProductLine --cod.ProductType
	LEFT JOIN	QSPCanadaCommon..CAccount acc ON acc.ID = camp.ShipToAccountID
	LEFT JOIN	QSPCanadaCommon..Address ad ON ad.AddressListID = acc.AddressListID AND ad.address_type = 54001
	LEFT JOIN	QSPCanadaCommon..Province prov ON prov.Province_Code = ad.stateProvince
	LEFT JOIN   (ShipmentOrder so
               JOIN Shipment s ON s.ID = so.ShipmentID AND s.ProcessedDate IS NULL)
                  ON    so.OrderID = b.OrderID
                  AND   s.ShipmentGroupID = sg.ShipmentGroupID
	WHERE		bdc.StatusInstance IN (40010,40014)
	AND			b.StatusInstance NOT IN (40013,40005)
	AND			b.Date >=''7/1/05''
	--AND			cod.delFlag <> 1
	AND			(sg.ShipmentGroupID IS NULL OR pl.ShipmentGroupID IS NULL OR sg.ShipmentGroupID = pl.ShipmentGroupID)
   AND         (s.ID IS NULL)
	/*AND			((cod.DistributionCenterID in (1, 2) AND cod.ProductType in ('+@ProdLine+')) OR b.OrderID IN (SELECT DISTINCT LandedOrderID  
																								FROM OnlineOrderMappingTable  
																								WHERE OnlineOrderID IN (SELECT	DISTINCT b2.OrderID
																														FROM	Batch b2
																														JOIN	CustomerOrderHeader coh2
																																	ON	coh2.OrderBatchID = b2.ID
																																	AND	coh2.OrderBatchDate = b2.Date
																														JOIN	CustomerOrderDetail cod2
																																	ON	cod2.CustomerOrderHeaderInstance = coh2.Instance
																														JOIN	QSPCanadaCommon..QSPProductLine pl2
																																	ON	pl2.ID = cod2.ProductType
																														WHERE	cod2.IsShippedToAccount = 1 
																														AND		b2.OrderQualifierID = 39009
																														AND		b2.StatusInstance not in (40005, 40013)
																														AND		cod2.StatusInstance not in ( 508, 513)
																														AND		cod2.DelFlag <> 1
																														AND		cod2.ProductType in ('+@ProdLine+')
																														AND		sg.ShipmentGroupID = pl2.ShipmentGroupID
																														--AND		(ISNULL(sg.ShipmentGroupID, pl2.ShipmentGroupID) = pl2.ShipmentGroupID)
																														AND		cod2.DistributionCenterID in (1, 2))))*/
	AND		(b.OrderQualifierID <> 39009 OR (cod.IsShippedToAccount = 0 AND cod.DistributionCenterID IN (1, 2) AND cod.StatusInstance NOT IN (508, 513)))'

end

if Isnull(@BackOrderOnly,0)=1
Begin
       set @str=@str + ' and bdc.StatusInstance IN (40014) ' 	
End

if Isnull(@ShipmentGroupID,0) > 0
Begin
       set @str=@str + ' and sg.ShipmentGroupID = ' + CONVERT(varchar(10), @ShipmentGroupID) + ' ' 	
End

If Len(@OrderList) >0
Begin
	Set @str = @str + ' and b.OrderId in('+@OrderList+')' 	      
End	

if Isnull(@ProductCode,'')<>''
Begin
       set @str=@str + ' and cod.ProductCode In ' +'('+@ProductCode+ ' ) ' 	
end
	
--print @str
EXEC( @str )

--- UPDATE THE For Field with FMs Information
UPDATE
	#TempShip
SET
	[For] = 'FM: ' + A.ShipToFMId + ' ' + B.FirstName + ' ' + B.LastName
FROM
	#TempShip A
	INNER JOIN QSPCanadaCommon..FieldManager B ON B.FMID = A.ShipToFMID
WHERE
	A.ShipToFMId IS NOT NULL

--- UPDATE THE For Field with Account Information
UPDATE
	#TempShip
SET
	[For] = 'GROUP: ' + B.Name
FROM
	#TempShip A
	INNER JOIN QSPCanadaOrderManagement..Account B ON B.ID = A.ShipToGroupID
WHERE
	A.ShipToGroupId IS NOT NULL

-- UPDATE THE For Field w/Customer Info

if( Patindex('46006',@ProdLine)  <> 0 or Patindex('46007',@ProdLine)  <> 0)
begin

	UPDATE
		#TempShip
	SET
		[For] = 'Customer: ' + D.Recipient
	FROM
		#TempShip A, QSPCanadaOrderManagement..CustomerOrderHeader B ,
		 	QSPCanadaOrderManagement..CustomerOrderHeader C,
			QSPCanadaOrderManagement..CustomerOrderDetail D
		where B.Instance = A.CustomerOrderHeaderInstance 
			and D.CustomerOrderHeaderInstance = B.Instance
			and D.TransID = A.TransID

end

Select 	OrderId  ,	BatchID ,	BatchDate ,	ShipToGroupId  ,	CampaignId ,
	[For]  ,	ShipToFMId  ,	CustomerOrderHeaderInstance  ,	TransID  ,	IsSplit ,
	Convert(varchar(10),DatePrinted,101) DatePrinted,
	Convert(varchar(10),SuppliesDeliveryDate,101) SuppliesDeliveryDate,
	ShipmentGroupID, ShipmentGroupName, OrderShippingDate
FROM #TempShip
Where IsPrinted = IsNull(@PrintStatus,IsPrinted)
Order By OrderShippingDate, Orderid
GO
