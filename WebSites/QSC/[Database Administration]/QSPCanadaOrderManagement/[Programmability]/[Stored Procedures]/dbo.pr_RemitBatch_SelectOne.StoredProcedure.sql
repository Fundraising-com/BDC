USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_RemitBatch_SelectOne]    Script Date: 06/07/2017 09:20:24 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_RemitBatch_SelectOne]
	@iID int
AS
SET NOCOUNT ON
-- SELECT an existing row from the table.
SELECT
	*
FROM [dbo].[RemitBatch]
WHERE
	[RunID] = @iID
GO
