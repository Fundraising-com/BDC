USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Program_SelectAll]    Script Date: 06/07/2017 09:20:21 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_Program_SelectAll] AS
SET NOCOUNT ON


SELECT 
	[ID],
	[Country],
	[FundraisingProcedureID],
	[ProgramTypeID],
	[Name],
	[MajorProductLineID],
	[DefaultProfit],
	[MinProfit],
	[MaxProfit],
	[OrdefuPrintInAR],
	[ActiveForFiscal_TF],
	[Abr]
FROM [QSPCanadaCommon]..[Program]
ORDER BY [ID]
GO
