USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_competitors]    Script Date: 02/14/2014 13:04:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Competitor
CREATE PROCEDURE [dbo].[efrcrm_get_competitors] AS
begin

select Competitor_ID, Business_Name, Comments from Competitor

end
GO
