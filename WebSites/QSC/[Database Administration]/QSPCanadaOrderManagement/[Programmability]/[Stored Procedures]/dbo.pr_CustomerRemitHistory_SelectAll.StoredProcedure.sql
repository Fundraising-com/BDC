USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_CustomerRemitHistory_SelectAll]    Script Date: 06/07/2017 09:19:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------
-- Stored procedure that will select all rows from the table 'CustomerRemitHistory'
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_CustomerRemitHistory_SelectAll]

AS
SET NOCOUNT ON
-- SELECT all rows from the table.
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
ORDER BY 
	[RemitBatchID] ASC
	, [Instance] ASC
GO
