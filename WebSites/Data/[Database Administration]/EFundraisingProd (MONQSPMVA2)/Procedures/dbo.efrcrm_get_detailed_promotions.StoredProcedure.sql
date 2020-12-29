USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_detailed_promotions]    Script Date: 02/14/2014 13:04:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Detailed_Promotion
CREATE PROCEDURE [dbo].[efrcrm_get_detailed_promotions] AS
begin

select Promotion_ID, Promotion_Type_Code, Target_Age_Group_Code, Target_Gender_Group_Code, Target_Group_Code, Promotion_Year, Promotion_Month, Description, Quantity_Sent, Call_Goal, Card_Budget from Detailed_Promotion

end
GO
