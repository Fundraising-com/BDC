USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[GetEstimatedSalesView]    Script Date: 06/07/2017 09:33:08 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetEstimatedSalesView]
			@p_program_id         int,
			@p_status_instance  int,
			@p_fm_id 	       varchar(4),
			@p_from_date 	       datetime,
			@p_to_date 	       datetime
As

 IF @p_program_id = 0
   BEGIN
     SET @p_program_id = NULL
   END

 IF @p_status_instance = 0
   BEGIN
     SET @p_status_instance = NULL
   END

 IF @p_fm_id = 0 or @p_fm_id = ''
   BEGIN
     SET @p_fm_id = NULL
   END


 Select Convert(varchar(10),DateReceived,101) DateReceived,
    batch.AccountId,ac.Name,CampaignId,fm.LastName +','+Firstname FMname,
    BillToFMId,OrderID,cd.Description Status, OrderDetailCount,ReportedEnvelopes,
    isnull(EnterredAmount,0) EnterredAmount , isnull(CalculatedAmount,0) CalculatedAmount,
   (EnterredAmount - CalculatedAmount) Variance
 From QspCanadaOrderManagement..batch batch, QspCanadaCommon..campaign cam,
      QspCanadaCommon..FieldManager fm,
      QspCanadaCommon..caccount ac,
      QspCanadaCommon..CodeDetail cd
 Where  batch.CampaignId = cam.ID
 and fm.fmid = cam.FMID
 and batch.AccountId = ac.ID
 and StatusInstance = cd.Instance
 and StatusInstance = isnull(@p_status_instance,StatusInstance)
 and fm.fmid = isnull(@p_fm_id,fm.fmid)
 and  ( DateReceived >= @p_from_date and DateReceived <= @p_to_date )
 and batch.CampaignId in ( select CampaignId
			   from   QspCanadaCommon.dbo.CampaignProgram
			   where ProgramId = isnull(@p_program_id,ProgramId) and DeletedTF <> 1);


--select @p_from_date as from_date, @p_to_date as to_date
GO
