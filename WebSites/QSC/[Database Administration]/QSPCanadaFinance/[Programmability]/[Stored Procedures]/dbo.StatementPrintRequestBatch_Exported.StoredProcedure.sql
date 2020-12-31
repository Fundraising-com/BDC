USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[StatementPrintRequestBatch_Exported]    Script Date: 06/07/2017 09:17:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[StatementPrintRequestBatch_Exported]

	@StatementPrintRequestBatchID			INT,
	@StatementPrintRequestBatchFilename		VARCHAR(150)

AS

UPDATE	StatementPrintRequestBatch
SET		[Filename] = @StatementPrintRequestBatchFilename
WHERE	StatementPrintRequestBatchID = @StatementPrintRequestBatchID
GO
