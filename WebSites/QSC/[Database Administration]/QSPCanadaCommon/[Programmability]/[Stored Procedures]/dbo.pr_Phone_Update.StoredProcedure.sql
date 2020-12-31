USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_Phone_Update]    Script Date: 06/07/2017 09:33:26 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
---------------------------------------------------------------------------------
-- Stored procedure that will update an existing row in the table 'Phone'
-- Gets: @iID int
-- Gets: @iType int
-- Gets: @iPhoneListID int
-- Gets: @sPhoneNumber varchar(50)
-- Gets: @sBestTimeToCall varchar(2000)
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_Phone_Update]
	@iID int,
	@iType int,
	@iPhoneListID int,
	@sPhoneNumber varchar(50),
	@sBestTimeToCall varchar(2000)
AS
SET NOCOUNT ON
-- UPDATE an existing row in the table.
UPDATE [dbo].[Phone]
SET 
	[Type] = @iType,
	[PhoneListID] = @iPhoneListID,
	[PhoneNumber] = @sPhoneNumber,
	[BestTimeToCall] = @sBestTimeToCall
WHERE
	[ID] = @iID
GO
