USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_Category_SelectAll]    Script Date: 06/07/2017 09:17:50 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Category_SelectAll] 

AS

SELECT	cd.Instance,
		cd.Description

FROM		QSPCanadaOrderManagement..CodeDetail cd

WHERE	CodeHeaderInstance = 30700
ORDER BY	cd.Description
GO
