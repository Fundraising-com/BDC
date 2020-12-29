USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[get_brochure_info]    Script Date: 02/14/2014 13:06:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	Jp Lahaie
Created On: July 13, 2004
Description:	This stored procedure ?
*/
CREATE PROCEDURE [dbo].[get_brochure_info]
	@intProductID int,
	@strCulture char(6)
AS
/* TODO: point to the local version of the promotion table */
	SELECT
		brochures_images.base_filename,
		brochures_images.file_ext,
		brochures_images.number_pages,
		product_desc.product_name
	FROM brochures_images Left outer Join product_desc on brochures_images.product_id = product_desc.product_id
		Left outer Join cultures on cultures.language_id = product_desc.language_id
	
	WHERE
		brochures_images.product_id = @intProductID 
		And cultures.culture_name =	@strCulture
GO
