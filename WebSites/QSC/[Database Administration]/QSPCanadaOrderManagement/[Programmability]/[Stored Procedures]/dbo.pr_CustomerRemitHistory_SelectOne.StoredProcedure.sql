USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_CustomerRemitHistory_SelectOne]    Script Date: 06/07/2017 09:19:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------
-- Stored procedure that will select an existing row from the table 'CustomerRemitHistory'
-- based on the Primary Key.
-- Gets: @iRemitBatchID int
-- Gets: @iInstance int
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_CustomerRemitHistory_SelectOne]
	@iRemitBatchID int,
	@iInstance int
AS
SET NOCOUNT ON
-- SELECT an existing row from the table.
SELECT
	[RemitBatchID],
	[Instance],
	[CustomerInstance],
	[StatusInstance],
	[LastName],
	[FirstName],
	[Address1],
	[Address2],
	[City],
	[State],
	[Zip],
	[ZipPlusFour],
	[DateModified],
	[UserIDModified]
FROM [dbo].[CustomerRemitHistory]
WHERE
	[RemitBatchID] = @iRemitBatchID
	AND [Instance] = @iInstance
GO
