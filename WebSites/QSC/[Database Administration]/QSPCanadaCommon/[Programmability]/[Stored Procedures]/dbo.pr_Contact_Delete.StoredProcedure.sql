USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_Contact_Delete]    Script Date: 06/07/2017 09:33:15 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
---------------------------------------------------------------------------------
-- Stored procedure that will delete an existing row from the table 'Contact'
-- using the Primary Key. 
-- Gets: @iId int
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_Contact_Delete]
	@iId int
AS
SET NOCOUNT ON
-- DELETE an existing row from the table.
UPDATE	[dbo].[Contact]
SET		[DeletedTF] = 1
WHERE	[Id] = @iId
GO
