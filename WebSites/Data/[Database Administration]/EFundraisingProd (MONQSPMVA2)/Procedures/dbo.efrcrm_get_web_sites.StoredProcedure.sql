USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_web_sites]    Script Date: 02/14/2014 13:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Web_Site
CREATE PROCEDURE [dbo].[efrcrm_get_web_sites] AS
begin

select Web_Site_Id, Web_Site_Name from Web_Site

end
GO
