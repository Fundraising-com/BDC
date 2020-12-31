USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectAllProductCategories]    Script Date: 06/07/2017 09:18:01 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_SelectAllProductCategories] AS

SELECT	cd.Instance,
		cd.Description

FROM		QSPCanadaOrderManagement..CodeDetail cd

WHERE	CodeHeaderInstance = 30700

ORDER BY	cd.Description
GO
