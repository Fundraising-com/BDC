USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_web_form_type_descs]    Script Date: 02/14/2014 13:05:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Web_form_type_desc
CREATE PROCEDURE [dbo].[efrstore_get_web_form_type_descs] AS
begin

select Web_form_type_id, Culture_code, Description from Web_form_type_desc

end
GO
