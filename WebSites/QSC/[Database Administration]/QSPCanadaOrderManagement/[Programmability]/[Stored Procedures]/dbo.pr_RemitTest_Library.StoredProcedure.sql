USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_RemitTest_Library]    Script Date: 06/07/2017 09:20:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- J SMITH or Library orders
CREATE procedure [dbo].[pr_RemitTest_Library]

@iRunID		int = 0

AS

IF EXISTS
(
SELECT		1
FROM		CustomerRemitHistory crh,
		RemitBatch rb
WHERE		rb.ID = crh.RemitBatchID
AND		((crh.FirstName = 'J'
AND		crh.LastName = 'SMITH')
OR		(crh.FirstName LIKE 'MEDIA%'
AND		crh.LastName LIKE 'CENTER%')
OR		(crh.FirstName LIKE '%LIBRAR%'
OR		crh.FirstName LIKE '%BIBLIOT%'
OR		crh.LastName LIKE '%LIBRAR%'
OR		crh.LastName LIKE '%BIBLIOT%'))
AND		rb.RunID = @iRunID
)
	SELECT 1
ELSE
	SELECT 0
GO
