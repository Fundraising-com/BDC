USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_detailed_promotion]    Script Date: 02/14/2014 13:07:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Detailed_Promotion
CREATE PROCEDURE [dbo].[efrcrm_update_detailed_promotion] @Promotion_ID int, @Promotion_Type_Code varchar(4), @Target_Age_Group_Code varchar(4), @Target_Gender_Group_Code varchar(4), @Target_Group_Code varchar(4), @Promotion_Year smallint, @Promotion_Month smallint, @Description varchar(50), @Quantity_Sent int, @Call_Goal int, @Card_Budget int AS
begin

update Detailed_Promotion set Promotion_Type_Code=@Promotion_Type_Code, Target_Age_Group_Code=@Target_Age_Group_Code, Target_Gender_Group_Code=@Target_Gender_Group_Code, Target_Group_Code=@Target_Group_Code, Promotion_Year=@Promotion_Year, Promotion_Month=@Promotion_Month, Description=@Description, Quantity_Sent=@Quantity_Sent, Call_Goal=@Call_Goal, Card_Budget=@Card_Budget where Promotion_ID=@Promotion_ID

end
GO
