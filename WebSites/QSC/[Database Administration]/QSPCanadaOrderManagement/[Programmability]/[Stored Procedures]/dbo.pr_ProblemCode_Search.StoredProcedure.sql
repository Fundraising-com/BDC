USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_ProblemCode_Search]    Script Date: 06/07/2017 09:20:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_ProblemCode_Search]
	@Description  nvarchar(200) = '',
	@Query nvarchar(4000) output

AS

declare @sqlStatement nvarchar(4000)

set @sqlStatement  = 'SELECT * FROM ProblemCode where '

if(@Description <> '')
BEGIN
	
	set @sqlStatement = @sqlStatement +  ' Description  like  ''%'+ @description + '%'''
	
END

set @Query =@sqlStatement
exec(@sqlStatement)
GO
