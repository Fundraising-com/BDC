USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_tags_by_id]    Script Date: 02/14/2014 13:06:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Tags
CREATE PROCEDURE [dbo].[efrcrm_get_tags_by_id] @Tags_ID int AS
begin

select Tags_ID, Label, Control_Name from Tags where Tags_ID=@Tags_ID

end
GO
