USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectCatalogCount]    Script Date: 06/07/2017 09:18:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_SelectCatalogCount]

@iCatalogID		int = 0,
@sCatalogCode		varchar(10) = ''

AS

SELECT 		COUNT(Program_ID)
FROM 		PROGRAM_MASTER
WHERE 		Code = @sCatalogCode
AND 		(@iCatalogID = 0 OR @iCatalogID != Program_ID)
GO
