USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_hear_about_us_descs]    Script Date: 02/14/2014 13:04:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Hear_about_us_desc
CREATE PROCEDURE [dbo].[efrcrm_get_hear_about_us_descs] AS
begin

select Hear_id, Language_id, Description from Hear_about_us_desc

end
GO
