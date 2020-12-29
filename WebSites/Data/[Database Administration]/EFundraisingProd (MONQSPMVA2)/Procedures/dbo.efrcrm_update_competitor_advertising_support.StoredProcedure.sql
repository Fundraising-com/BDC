USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_competitor_advertising_support]    Script Date: 02/14/2014 13:07:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Competitor_Advertising_Support
CREATE PROCEDURE [dbo].[efrcrm_update_competitor_advertising_support] @Advertising_Support_ID int, @Competitor_Advertising_ID int AS
begin

update Competitor_Advertising_Support set Competitor_Advertising_ID=@Competitor_Advertising_ID where Advertising_Support_ID=@Advertising_Support_ID

end
GO
