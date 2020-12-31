USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_UpdateProductCategory]    Script Date: 06/07/2017 09:18:06 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_UpdateProductCategory]

	@iCategoryID	int,
	@zDescription	varchar(64)

AS

	UPDATE	QSPCanadaCommon..CodeDetail
	SET		Description = @zDescription
	WHERE	Instance = @iCategoryID
GO
