USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_competitor_advertising_supports]    Script Date: 02/14/2014 13:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Competitor_Advertising_Support
CREATE PROCEDURE [dbo].[efrcrm_get_competitor_advertising_supports] AS
begin

select Advertising_Support_ID, Competitor_Advertising_ID from Competitor_Advertising_Support

end
GO
