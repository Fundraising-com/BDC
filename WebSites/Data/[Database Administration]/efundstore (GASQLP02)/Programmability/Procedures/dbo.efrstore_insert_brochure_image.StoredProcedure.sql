USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_brochure_image]    Script Date: 02/14/2014 13:05:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Brochure_image
CREATE PROCEDURE [dbo].[efrstore_insert_brochure_image] @Brochure_image_id int OUTPUT, @Product_id int, @Base_filename varchar(100), @File_ext varchar(5), @Number_page tinyint AS
begin

insert into Brochure_image(Product_id, Base_filename, File_ext, Number_page) values(@Product_id, @Base_filename, @File_ext, @Number_page)

select @Brochure_image_id = SCOPE_IDENTITY()

end
GO
