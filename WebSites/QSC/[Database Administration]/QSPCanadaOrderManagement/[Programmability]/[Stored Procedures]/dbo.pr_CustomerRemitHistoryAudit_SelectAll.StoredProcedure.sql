USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_CustomerRemitHistoryAudit_SelectAll]    Script Date: 06/07/2017 09:19:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------
-- Stored procedure that will select all rows from the table 'CustomerRemitHistoryAudit'
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_CustomerRemitHistoryAudit_SelectAll]

AS
SET NOCOUNT ON
-- SELECT all rows from the table.
SELECT
	[AuditDate],
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
FROM [dbo].[CustomerRemitHistoryAudit]
ORDER BY 
	[AuditDate] ASC
	, [RemitBatchID] ASC
	, [Instance] ASC
GO
