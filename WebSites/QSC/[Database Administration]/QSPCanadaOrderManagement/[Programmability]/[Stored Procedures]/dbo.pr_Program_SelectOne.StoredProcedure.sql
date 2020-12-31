USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Program_SelectOne]    Script Date: 06/07/2017 09:20:21 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Program_SelectOne] 
	@ID int
AS
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
Where ID=@ID
ORDER BY [ID]
GO
