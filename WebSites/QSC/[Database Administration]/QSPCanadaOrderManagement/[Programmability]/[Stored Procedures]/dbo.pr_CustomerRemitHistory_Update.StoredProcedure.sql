USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_CustomerRemitHistory_Update]    Script Date: 06/07/2017 09:19:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------
-- Stored procedure that will update an existing row in the table 'CustomerRemitHistory'
-- Gets: @iRemitBatchID int
-- Gets: @iInstance int
-- Gets: @iCustomerInstance int
-- Gets: @iStatusInstance int
-- Gets: @sLastName varchar(50)
-- Gets: @sFirstName varchar(50)
-- Gets: @sAddress1 varchar(50)
-- Gets: @sAddress2 varchar(50)
-- Gets: @sCity varchar(50)
-- Gets: @sState varchar(10)
-- Gets: @sZip varchar(20)
-- Gets: @sZipPlusFour varchar(4)
-- Gets: @daDateModified datetime
-- Gets: @sUserIDModified varchar(4)
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_CustomerRemitHistory_Update]
	@iRemitBatchID int,
	@iInstance int,
	@iCustomerInstance int,
	@iStatusInstance int,
	@sLastName varchar(50),
	@sFirstName varchar(50),
	@sAddress1 varchar(50),
	@sAddress2 varchar(50),
	@sCity varchar(50),
	@sState varchar(10),
	@sZip varchar(20),
	@sZipPlusFour varchar(4),
	@daDateModified datetime,
	@sUserIDModified varchar(4)
AS
SET NOCOUNT ON
-- UPDATE an existing row in the table.
UPDATE [dbo].[CustomerRemitHistory]
SET 
	[CustomerInstance] = @iCustomerInstance,
	[StatusInstance] = @iStatusInstance,
	[LastName] = @sLastName,
	[FirstName] = @sFirstName,
	[Address1] = @sAddress1,
	[Address2] = @sAddress2,
	[City] = @sCity,
	[State] = @sState,
	[Zip] = @sZip,
	[ZipPlusFour] = @sZipPlusFour,
	[DateModified] = @daDateModified,
	[UserIDModified] = @sUserIDModified
WHERE
	[RemitBatchID] = @iRemitBatchID
	AND [Instance] = @iInstance
GO
