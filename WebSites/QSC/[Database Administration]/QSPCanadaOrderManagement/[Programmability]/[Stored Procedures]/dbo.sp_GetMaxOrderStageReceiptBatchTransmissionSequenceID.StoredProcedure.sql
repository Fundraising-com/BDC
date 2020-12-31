USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetMaxOrderStageReceiptBatchTransmissionSequenceID]    Script Date: 06/07/2017 09:20:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* --------------------------------------------------------------------------------------------------
   [dbo].[sp_GetMaxOrderStageReceiptBatchTransmissionSequenceID]
   
   Gets the max TransmissionSequenceID from OrderStageReceiptBatch table
   
   Date        Author(s)            Description
   ----------  -------------------  -----------------------------------------------------------------
   11/7/2014  venu               initial version
   -------------------------------------------------------------------------------------------------- */

create PROCEDURE [dbo].[sp_GetMaxOrderStageReceiptBatchTransmissionSequenceID]
AS
 BEGIN
  DECLARE @maxTransmisstionSequenceID int

  SELECT @maxTransmisstionSequenceID = (isnull(max(TransmissionSequenceID),1))  
  FROM dbo.OrderStageReceiptBatch 

  SELECT @maxTransmisstionSequenceID as maxTransmisstionSequenceID
   
 END
GO
