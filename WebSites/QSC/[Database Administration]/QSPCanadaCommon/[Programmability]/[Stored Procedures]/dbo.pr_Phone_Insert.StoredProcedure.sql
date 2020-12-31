USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_Phone_Insert]    Script Date: 06/07/2017 09:33:26 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
---------------------------------------------------------------------------------
-- Stored procedure that will insert 1 row in the table 'Phone'
-- Gets: @iType int
-- Gets: @iPhoneListID int
-- Gets: @sPhoneNumber varchar(50)
-- Gets: @sBestTimeToCall varchar(2000)
-- Returns: @iID int
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_Phone_Insert]
	@iType int,
	@iPhoneListID int,
	@sPhoneNumber varchar(50),
	@sBestTimeToCall varchar(2000),
	@iID int OUTPUT
AS
-- INSERT a new row in the table.
INSERT [dbo].[Phone]
(
	[Type],
	[PhoneListID],
	[PhoneNumber],
	[BestTimeToCall]
)
VALUES
(
	@iType,
	@iPhoneListID,
	@sPhoneNumber,
	@sBestTimeToCall
)
-- Get the IDENTITY value for the row just inserted.
SELECT @iID=SCOPE_IDENTITY()
GO
