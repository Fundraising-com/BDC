USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_AdPageSize_SelectAll]    Script Date: 06/07/2017 09:17:49 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_AdPageSize_SelectAll] 

AS

SELECT ID, Description FROM AdPageSize
GO
