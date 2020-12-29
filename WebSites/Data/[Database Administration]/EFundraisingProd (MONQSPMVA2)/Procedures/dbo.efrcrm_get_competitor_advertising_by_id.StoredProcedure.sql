USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_competitor_advertising_by_id]    Script Date: 02/14/2014 13:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Competitor_Advertising
CREATE PROCEDURE [dbo].[efrcrm_get_competitor_advertising_by_id] @Competitor_Advertising_ID int AS
begin

select Competitor_Advertising_ID, Competitor_ID, Description, Publicity_Duration, Comments from Competitor_Advertising where Competitor_Advertising_ID=@Competitor_Advertising_ID

end
GO
