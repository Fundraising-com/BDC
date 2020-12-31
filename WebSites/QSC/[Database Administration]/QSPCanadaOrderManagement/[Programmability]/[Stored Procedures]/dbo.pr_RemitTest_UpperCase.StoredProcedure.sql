USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_RemitTest_UpperCase]    Script Date: 06/07/2017 09:20:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Check for lower case
create procedure [dbo].[pr_RemitTest_UpperCase]

@iRunID		int = 0

AS

IF EXISTS
(
	SELECT		crh.*
	FROM		CustomerRemitHistory crh,
			RemitBatch rb
	WHERE		rb.ID = crh.RemitBatchID
	AND		(UPPER(crh.FirstName) <> crh.FirstName COLLATE sql_latin1_general_cp1_cs_as
	OR		UPPER(crh.LastName) <> crh.LastName COLLATE sql_latin1_general_cp1_cs_as
	OR		UPPER(crh.Address1) <> crh.Address1 COLLATE sql_latin1_general_cp1_cs_as
	OR		UPPER(crh.Address2) <> crh.Address2 COLLATE sql_latin1_general_cp1_cs_as
	OR		UPPER(crh.City) <> crh.City COLLATE sql_latin1_general_cp1_cs_as
	OR		UPPER(crh.State) <> crh.State COLLATE sql_latin1_general_cp1_cs_as
	OR		UPPER(crh.Zip) <> crh.Zip COLLATE sql_latin1_general_cp1_cs_as)
	AND		rb.RunID = @iRunID
)
	SELECT 1
ELSE
	SELECT 0
GO
