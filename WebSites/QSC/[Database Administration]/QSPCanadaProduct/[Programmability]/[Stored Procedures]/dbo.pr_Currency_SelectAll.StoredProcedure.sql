USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_Currency_SelectAll]    Script Date: 06/07/2017 09:17:50 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Currency_SelectAll] 

AS

SELECT Instance as ID,
	 Description
FROM 	 QSPCanadaCommon..CodeDetail
WHERE CodeHeaderInstance = 800
GO
