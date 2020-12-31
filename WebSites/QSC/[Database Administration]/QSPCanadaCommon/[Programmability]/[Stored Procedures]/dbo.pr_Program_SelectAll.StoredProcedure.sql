USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_Program_SelectAll]    Script Date: 06/07/2017 09:33:27 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------
-- Stored procedure that will select all rows from the table 'Program'
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_Program_SelectAll]

@isSelectActive bit = 1

AS

SET NOCOUNT ON
-- SELECT all rows from the table.
SELECT
	[ID],
	[Country],
	[FundraisingProcedureID],
	[ProgramTypeID],
	[Name],
	[FrenchName],
	[MajorProductLineID],
	[DefaultProfit],
	[MinProfit],
	[MaxProfit],
	[OrdefuPrintInAR],
	[ActiveForFiscal_TF],
	[Abr],
	[PrintInvoice]
FROM [dbo].[Program]
Where @isSelectActive <> 1 OR ActiveForFiscal_TF=1
ORDER BY 
	ListPriority, ID
GO
