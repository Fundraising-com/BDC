USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[GetIncentiveLevelCount]    Script Date: 06/07/2017 09:19:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetIncentiveLevelCount] (  @OrderId 	Int,
						     --  @BatchId   Int,
						     --  @BatchDate DateTime,
						       @TeacherLastName Varchar(50),
						       @Class  Varchar(20),
						       @LevelCode Varchar(1))
					
AS
Begin
Declare @LevelCount Int,
	@IncEnvCount Int,
	@orderIdIncentive  Int,
	@IncentiveOrderBatch Int,
	@IncentiveOrderBatchDate DateTime

--Check if there is an Incentive envelope exists for the order (for teacher and class) 
SELECT     @IncEnvCount = COUNT(1)
FROM         dbo.CustomerOrderHeader oh INNER JOIN
                   dbo.OrderInEnvelopeMap emap ON oh.Instance = emap.CustomerOrderHeaderInstance INNER JOIN
                   dbo.Envelope e ON emap.EnvelopeID = e.Instance INNER JOIN
                   dbo.Batch b ON oh.OrderBatchID = b.ID AND oh.OrderBatchDate = b.[Date] INNER JOIN
                   dbo.Teacher t ON e.TeacherInstance = t.Instance
WHERE     (b.OrderID = @OrderId) 
	--AND (b.ID = @BatchId) AND (b.[Date] = @BatchDate) 
	AND (e.IsIncentive = 'Y') AND (IsNUll(t.LastName,' ') = IsNull(@TeacherLastName,' ')) 
	AND (IsNull(t.Classroom, ' ') = IsNull(@Class, ' '))

--If not found Incentive might have been calculated under a seperate order (supplementary etc) get the IncentiveOrderId
If  @IncEnvCount = 0 
Begin

     select @orderIdIncentive = OrderIdIncentive
     from dbo.Batch b
     where   (b.OrderID = @OrderId) --AND (b.ID = @BatchId) AND (b.[Date] = @BatchDate) 

     select Top 1 @IncentiveOrderBatch = b.Id, @IncentiveOrderBatchDate = b.date
     from dbo.Batch b
     where   (b.OrderID = @orderIdIncentive)

set @OrderId = @orderIdIncentive
--set @BatchId = @IncentiveOrderBatch
--Set @BatchDate = @IncentiveOrderBatchDate
End



SELECT      @LevelCount =(Case  PrizeL.Level_Code
			When  @LevelCode Then count(PrizeL.Level_Code)
	   		 End)
FROM         dbo.CustomerOrderHeader oh INNER JOIN
                      dbo.CustomerOrderDetail od ON oh.Instance = od.CustomerOrderHeaderInstance INNER JOIN
                      dbo.OrderInEnvelopeMap emap ON oh.Instance = emap.CustomerOrderHeaderInstance INNER JOIN
                      dbo.Envelope e ON emap.EnvelopeID = e.Instance INNER JOIN
                      dbo.Teacher t ON e.TeacherInstance = t.Instance INNER JOIN
                      dbo.Batch b ON oh.OrderBatchID = b.ID AND oh.OrderBatchDate = b.[Date] LEFT OUTER JOIN
                      QSPCanadaProduct.dbo.PROGRAM_MASTER ProgramM INNER JOIN
                      QSPCanadaProduct.dbo.PRICING_DETAILS PricingD ON ProgramM.Program_ID = PricingD.Program_ID INNER JOIN
                      QSPCanadaProduct.dbo.Prize_Level PrizeL ON ProgramM.Code = PrizeL.Catalog_Code ON od.PricingDetailsID = PricingD.MagPrice_Instance
WHERE  b.orderId =  @OrderId
--and b.Id =@BatchId
--and b.date =@BatchDate
and  e.isincentive = 'Y'
--and t.LastName = @TeacherLastName
--and t.classroom =@Class
group by t.LastName, t.Classroom,b.OrderID, emap.EnvelopeID,prizeL.level_Code

End
GO
