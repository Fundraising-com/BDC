USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_title_descs]    Script Date: 02/14/2014 13:06:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Title_desc
CREATE PROCEDURE [dbo].[efrcrm_get_title_descs] AS
begin

select Title_id, Language_id, Description from Title_desc

end
GO
