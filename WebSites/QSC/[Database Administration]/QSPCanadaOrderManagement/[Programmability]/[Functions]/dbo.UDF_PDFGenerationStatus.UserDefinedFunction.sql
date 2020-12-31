USE [QSPCanadaOrderManagement]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_PDFGenerationStatus]    Script Date: 06/07/2017 09:21:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[UDF_PDFGenerationStatus] (@OrderID int)
returns int 
as
begin
/*
Description:
   Function designed to calculate the number of business days 
between two dates.
*/

   Declare @IsGen int, @ID int , @NotGen int
   Declare @Status int 

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

  Set @Status = 1 --no pdf is generated

return @Status
End
GO
