USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_package_template]    Script Date: 02/14/2014 13:06:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Package_template
CREATE PROCEDURE [dbo].[efrstore_update_package_template] @Package_template_id tinyint, @Package_template_desc varchar(50) AS
begin

update Package_template set Package_template_desc=@Package_template_desc where Package_template_id=@Package_template_id

end
GO
