USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_DeleteCatalogByCatalogID]    Script Date: 06/07/2017 09:17:50 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_DeleteCatalogByCatalogID]

@iCatalogID	int

AS

DELETE FROM PROGRAM_MASTER WHERE Program_ID = @iCatalogID
GO
