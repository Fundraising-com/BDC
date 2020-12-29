USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_targeted_markets]    Script Date: 02/14/2014 13:06:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Targeted_Market
CREATE PROCEDURE [dbo].[efrcrm_get_targeted_markets] AS
begin

select Targeted_Market_ID, Targeted_Market_Type_ID, Advertising_Support_ID, Target_Market_Type_ID, Seasoner, Age_Range, Education_Level, Description, Comments from Targeted_Market

end
GO
