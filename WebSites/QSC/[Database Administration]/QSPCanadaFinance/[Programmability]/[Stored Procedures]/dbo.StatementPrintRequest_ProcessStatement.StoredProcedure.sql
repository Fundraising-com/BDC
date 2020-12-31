USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[StatementPrintRequest_ProcessStatement]    Script Date: 06/07/2017 09:17:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[StatementPrintRequest_ProcessStatement]

	@StatementID	INT

AS

BEGIN TRANSACTION

INSERT INTO StatementPrintRequest
(
	StatementID,
	CreationDate
)
VALUES
(
	@StatementID,
	GETDATE()
)

COMMIT TRANSACTION
GO
