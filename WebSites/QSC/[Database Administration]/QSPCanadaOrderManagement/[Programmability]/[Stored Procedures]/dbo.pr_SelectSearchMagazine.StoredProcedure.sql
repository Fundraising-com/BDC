USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectSearchMagazine]    Script Date: 06/07/2017 09:20:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_SelectSearchMagazine] 

	@iStatus int = 0,	
	@MagazineTitle nvarchar(100) = '',
	@ProductType int = 46001
AS


declare @sqlStatement nvarchar(4000)

set @sqlStatement = 'select distinct product_code,product_sort_name,status from QSPCanadaProduct..Product pr where type = '+ convert(nvarchar, @ProductType)



if(@iStatus  <>0)
BEGIN
	
	set @sqlStatement = @sqlStatement + ' and '
	set @sqlStatement = @sqlStatement+ 'pr.Status =  '+ convert(nvarchar,@iStatus)
	
END

if(@MagazineTitle  <>'')
BEGIN
	
	set @sqlStatement = @sqlStatement + ' and '
	set @sqlStatement = @sqlStatement+ 'pr.product_sort_name  like  ''%'+ @MagazineTitle+'%'''
	
END
exec(@sqlStatement)
GO
