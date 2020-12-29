USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_competitor_advertising_support_by_id]    Script Date: 02/14/2014 13:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Competitor_Advertising_Support
CREATE PROCEDURE [dbo].[efrcrm_get_competitor_advertising_support_by_id] @Advertising_Support_ID int AS
begin

select Advertising_Support_ID, Competitor_Advertising_ID from Competitor_Advertising_Support where Advertising_Support_ID=@Advertising_Support_ID

end
GO
