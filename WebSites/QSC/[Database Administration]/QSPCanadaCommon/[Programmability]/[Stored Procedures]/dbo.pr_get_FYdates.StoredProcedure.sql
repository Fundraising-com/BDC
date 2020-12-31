USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_get_FYdates]    Script Date: 06/07/2017 09:33:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_get_FYdates]
  @FiscalStart DATETIME OUTPUT
  , @FiscalEnd DATETIME OUTPUT
AS

DECLARE @CurrentFiscal INT

IF DATEPART(mm, getdate()) > 6
BEGIN
	SELECT @CurrentFiscal = DATEPART(yyyy, getdate()) + 1
END
ELSE
BEGIN
	SELECT @CurrentFiscal = DATEPART(yyyy, getdate())
END
SET @FiscalStart = '07/01/' + Convert( varchar, @CurrentFiscal - 1 )
SET @FiscalEnd = '06/30/' + Convert( varchar, @CurrentFiscal )
GO
