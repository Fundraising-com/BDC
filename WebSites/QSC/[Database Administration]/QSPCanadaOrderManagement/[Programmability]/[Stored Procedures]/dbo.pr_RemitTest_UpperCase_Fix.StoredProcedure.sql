USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_RemitTest_UpperCase_Fix]    Script Date: 06/07/2017 09:20:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Check for lower case
create procedure [dbo].[pr_RemitTest_UpperCase_Fix]

@iRunID		int = 0

AS

UPDATE		crh
SET		crh.FirstName = UPPER(crh.FirstName),
		crh.LastName = UPPER(crh.LastName),
		crh.Address1 = UPPER(crh.Address1),
		crh.Address2 = UPPER(crh.Address2),
		crh.City = UPPER(crh.City),
		crh.State = UPPER(crh.State),
		crh.Zip = UPPER(crh.Zip)
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
GO
