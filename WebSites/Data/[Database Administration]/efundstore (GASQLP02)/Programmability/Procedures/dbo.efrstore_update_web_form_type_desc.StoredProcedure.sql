USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_web_form_type_desc]    Script Date: 02/14/2014 13:06:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Web_form_type_desc
CREATE PROCEDURE [dbo].[efrstore_update_web_form_type_desc] @Web_form_type_id int, @Culture_code nvarchar(10), @Description varchar(256) AS
begin

update Web_form_type_desc set Culture_code=@Culture_code, Description=@Description where Web_form_type_id=@Web_form_type_id

end
GO
