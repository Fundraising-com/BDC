USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[DocsStatus]    Script Date: 06/07/2017 09:19:26 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[DocsStatus]

@OrderID int,
@Status   int output

AS

-- Saqib Shah - 12 Sep 2005
--this proc return 1 if all the pdf files for an order are generated or 0 if atleast one pdf is not generated


Begin


   Declare @IsGen int, @ID int , @NotGen int

   Set @IsGen = NULL
   Set @Status = 0
   Set @NotGen = -1

   Select top 1 @ID =  ID
   From QspCanadaOrderManagement.dbo.ReportRequestBatch
   Where batchOrderID = @OrderID


IF @ID IS NOT NULL -- if order is qued 

BEGIN
   

   Select @IsGen = 0 
   From QspCanadaOrderManagement.dbo.ReportRequestBatch_PrintPickList   
   where ReportRequestBatchID = @ID
   and pReportType = 1
   and RunDateStart is  Null  

   Set @IsGen = isnull(@IsGen,-1)

   IF @IsGen  = 0 
      BEGIN
          SET  @NotGen = 0
      END 



   Select @IsGen = 0 
   From QspCanadaOrderManagement.dbo.ReportRequestBatch_PrintPickList   
   where ReportRequestBatchID = @ID
   and pReportType = 2 
   and RunDateStart is  Null  

   Set @IsGen = isnull(@IsGen,-1)

   IF @IsGen  = 0 
      BEGIN
          SET  @NotGen = 0
      END 



   Select @IsGen = 0 
   From QspCanadaOrderManagement.dbo.ReportRequestBatch_BHEShippingLabelsReport   
   where ReportRequestBatchID = @ID 
   and RunDateStart is  null   

   Set @IsGen = isnull(@IsGen,-1)

   IF  @IsGen = 0 
      BEGIN
          SET  @NotGen = 0
      END 



   Select @IsGen = 0 
   From QspCanadaOrderManagement.dbo.ReportRequestBatch_ParticipantListing   
   where ReportRequestBatchID = @ID
   and RunDateStart is  null  

   Set @IsGen = isnull(@IsGen,-1)

   IF @IsGen  = 0 
      BEGIN
          SET  @NotGen = 0
      END 



   Select @IsGen = 0 
   From QspCanadaOrderManagement.dbo.ReportRequestBatch_HomeroomSummaryReport   
   where ReportRequestBatchID = @ID
   and RunDateStart is  null  

   Set @IsGen = isnull(@IsGen,-1)

   IF @IsGen  = 0 
      BEGIN
          SET  @NotGen = 0
      END 




   Select @IsGen = 0 
   From QspCanadaOrderManagement.dbo.ReportRequestBatch_GroupSummaryReport   
   where ReportRequestBatchID = @ID
   and RunDateStart is  null  

   Set @IsGen = isnull(@IsGen,-1)

   IF @IsGen  = 0 
      BEGIN
          SET  @NotGen = 0
      END 



   Select @IsGen = 0 
   From QspCanadaOrderManagement.dbo.ReportRequestBatch_MagazineItemsSummary   
   where ReportRequestBatchID = @ID
   and RunDateStart is  null  

   Set @IsGen = isnull(@IsGen,-1)

   IF @IsGen  = 0 
      BEGIN
          SET  @NotGen = 0
      END 



   Select @IsGen = 0 
   From QspCanadaOrderManagement.dbo.ReportRequestBatch_ProblemSolverReport   
   where ReportRequestBatchID = @ID
   and RunDateStart is  null  

   Set @IsGen = isnull(@IsGen,-1)

   IF @IsGen  = 0 
      BEGIN
          SET  @NotGen = 0
      END 




   Select @IsGen = 0 
   From QspCanadaOrderManagement.dbo.ReportRequestBatch_TeacherBoxLabelsReport   
   where ReportRequestBatchID = @ID
   and RunDateStart is  null  

   Set @IsGen = isnull(@IsGen,-1)

   IF @IsGen  = 0 
      BEGIN
          SET  @NotGen = 0
      END 



   Select @IsGen = 0 
   From QspCanadaOrderManagement.dbo.ReportRequestBatch_OrderEntryFollowupReport   
   where ReportRequestBatchID = @ID
   and RunDateStart is  null  

   Set @IsGen = isnull(@IsGen,-1)

   IF @IsGen  = 0 
      BEGIN
          SET  @NotGen = 0
      END 




   Select @IsGen = 0 
   From QspCanadaOrderManagement.dbo.ReportRequestBatch_PriceDiscrepancyReport   
   where ReportRequestBatchID = @ID
   and RunDateStart is  null  

   Set @IsGen = isnull(@IsGen,-1)

   IF @IsGen  = 0 
      BEGIN
          SET  @NotGen = 0
      END 

  
  --sending value in output variable

     IF @NotGen  = 0 
         BEGIN
            SET  @Status = 0 --atleast one pdf is not generated
        END 
    ELSE
        BEGIN
            SET  @Status = 1 -- all pdf are generated
        END 
    


END 

ELSE --if order is not even qued yet

  Set @Status = 0 --no pdf is generated

End
GO
