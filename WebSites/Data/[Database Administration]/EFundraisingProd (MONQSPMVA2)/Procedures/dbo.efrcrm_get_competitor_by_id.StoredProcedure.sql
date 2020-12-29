USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_competitor_by_id]    Script Date: 02/14/2014 13:04:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Competitor
CREATE PROCEDURE [dbo].[efrcrm_get_competitor_by_id] @Competitor_ID int AS
begin

select Competitor_ID, Business_Name, Comments from Competitor where Competitor_ID=@Competitor_ID

end
GO
