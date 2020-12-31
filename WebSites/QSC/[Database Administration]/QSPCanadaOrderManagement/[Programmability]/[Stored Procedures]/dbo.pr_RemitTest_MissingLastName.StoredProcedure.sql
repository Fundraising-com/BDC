USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_RemitTest_MissingLastName]    Script Date: 06/07/2017 09:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Missing Names
create procedure [dbo].[pr_RemitTest_MissingLastName]

@iRunID		int = 0

AS

IF EXISTS
(
	SELECT		crh.*
	FROM		CustomerRemitHistory crh,
			RemitBatch rb
	WHERE		rb.ID = crh.RemitBatchID
	AND		(LEN(LTRIM(RTRIM(COALESCE(crh.LastName, '')))) = 0
	OR		ISNUMERIC(SUBSTRING(crh.LastName, 1, 1)) = 1)
	AND		rb.RunID = @iRunID
)
	SELECT 1
ELSE 
	SELECT 0
GO
