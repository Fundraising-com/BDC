USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_ListingLevel_SelectAll]    Script Date: 06/07/2017 09:17:56 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_ListingLevel_SelectAll] 

AS

SELECT ID, Description FROM ListingLevel
GO
