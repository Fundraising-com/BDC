USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_CodeDetail_SelectAll]    Script Date: 06/07/2017 09:19:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------
-- Stored procedure that will select all rows from the table 'CodeDetail'
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_CodeDetail_SelectAll]

AS
SET NOCOUNT ON
-- SELECT all rows from the table.
SELECT
	[Instance],
	[CodeHeaderInstance],
	[Description],
	[Gross],
	[ADPCode]
FROM [dbo].[CodeDetail]
ORDER BY 
	[Instance] ASC
GO
