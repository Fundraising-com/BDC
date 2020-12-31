USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[AP_Cheque_StatusReceipt_Batch_Create]    Script Date: 06/07/2017 09:17:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AP_Cheque_StatusReceipt_Batch_Create]

	@Filename							NVARCHAR(255),
	@AP_Cheque_StatusReceipt_Batch_ID	INT OUTPUT

AS

INSERT	AP_Cheque_StatusReceipt_Batch
(
	[Filename]
)
VALUES	(@Filename)

SET @AP_Cheque_StatusReceipt_Batch_ID = SCOPE_IDENTITY()
GO
