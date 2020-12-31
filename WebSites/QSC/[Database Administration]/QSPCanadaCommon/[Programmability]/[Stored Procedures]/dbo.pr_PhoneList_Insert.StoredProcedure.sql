USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_PhoneList_Insert]    Script Date: 06/07/2017 09:33:27 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
---------------------------------------------------------------------------------
-- Stored procedure that will insert 1 row in the table 'PhoneList'
-- Gets: @daCreateDate datetime
-- Gets: @bDeletedTF bit
-- Returns: @iID int
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_PhoneList_Insert]
	@daCreateDate datetime,
	@bDeletedTF bit,
	@iID int OUTPUT
AS
-- INSERT a new row in the table.
INSERT [dbo].[PhoneList]
(
	[CreateDate],
	[DeletedTF]
)
VALUES
(
	@daCreateDate,
	ISNULL(@bDeletedTF, (0))
)
-- Get the IDENTITY value for the row just inserted.
SELECT @iID=SCOPE_IDENTITY()
GO
