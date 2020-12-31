USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[AP_Cheque_Batch_Exported]    Script Date: 06/07/2017 09:17:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AP_Cheque_Batch_Exported]

	@AP_Cheque_Batch_ID			INT,
	@AP_Cheque_Batch_Filename	VARCHAR(150)

AS

UPDATE	AP_Cheque_Batch
SET		[Filename] = @AP_Cheque_Batch_Filename
WHERE	AP_Cheque_Batch_ID = @AP_Cheque_Batch_ID
GO
