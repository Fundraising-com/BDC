USE [QSPCanadaOrderManagement]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_GetHomeRoomCount]    Script Date: 06/07/2017 09:21:03 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION  [dbo].[UDF_GetHomeRoomCount]  (	@OrderId 		Int,
							@BatchId	 	Int,
             							@BatchDate                   DateTime,
							@Type			Varchar(10)
						  )
Returns Int
  As  
Begin
Declare	@Cnt 	Int

Select @Cnt =  (  Case @Type
		 When  'CLASS'           Then Count(Distinct t.Classroom)
		 When  'STUDENT'     Then Count(Distinct s.LastName + ' ' + s.FirstName) 
		 Else
		         0
	              End )
From     QSPCanadaOrderManagement.dbo.Batch b Inner Join
             QSPCanadaOrderManagement.dbo.CustomerOrderHeader oh On b.ID = oh.OrderBatchID And b.[Date] = oh.OrderBatchDate Inner Join
             QSPCanadaOrderManagement.dbo.Student s On oh.StudentInstance = s.Instance Inner Join
             QSPCanadaOrderManagement.dbo.Teacher t On s.TeacherInstance = t.Instance
Where   (b.OrderID = @OrderId)
And	(b.Id=@BatchId OR @BatchId IS NULL)
And 	(b.Date=@BatchDate OR @BatchDate IS NULL)

	Return IsNull(@Cnt,0)
End
GO
