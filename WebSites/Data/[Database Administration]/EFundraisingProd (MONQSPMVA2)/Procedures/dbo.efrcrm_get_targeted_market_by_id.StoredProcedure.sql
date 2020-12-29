USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_targeted_market_by_id]    Script Date: 02/14/2014 13:06:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Targeted_Market
CREATE PROCEDURE [dbo].[efrcrm_get_targeted_market_by_id] @Targeted_Market_ID int AS
begin

select Targeted_Market_ID, Targeted_Market_Type_ID, Advertising_Support_ID, Target_Market_Type_ID, Seasoner, Age_Range, Education_Level, Description, Comments from Targeted_Market where Targeted_Market_ID=@Targeted_Market_ID

end
GO
