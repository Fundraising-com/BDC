USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_sites]    Script Date: 02/14/2014 13:06:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Site
CREATE PROCEDURE [dbo].[efrcrm_get_sites] AS
begin

select Site_Id, Site_Title, Site_Content from Site

end
GO
