USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_PhoneList_Update]    Script Date: 06/07/2017 09:33:27 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
---------------------------------------------------------------------------------
-- Stored procedure that will update an existing row in the table 'PhoneList'
-- Gets: @iID int
-- Gets: @daCreateDate datetime
-- Gets: @bDeletedTF bit
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_PhoneList_Update]
	@iID int,
	@daCreateDate datetime,
	@bDeletedTF bit
AS
SET NOCOUNT ON
-- UPDATE an existing row in the table.
UPDATE [dbo].[PhoneList]
SET 
	[CreateDate] = @daCreateDate,
	[DeletedTF] = @bDeletedTF
WHERE
	[ID] = @iID
GO
