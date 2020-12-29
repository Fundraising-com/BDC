USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_competitor_advertisings]    Script Date: 02/14/2014 13:04:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Competitor_Advertising
CREATE PROCEDURE [dbo].[efrcrm_get_competitor_advertisings] AS
begin

select Competitor_Advertising_ID, Competitor_ID, Description, Publicity_Duration, Comments from Competitor_Advertising

end
GO
