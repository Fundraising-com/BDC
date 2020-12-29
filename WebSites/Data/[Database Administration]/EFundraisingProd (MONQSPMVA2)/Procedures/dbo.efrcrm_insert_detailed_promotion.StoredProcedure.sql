USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_detailed_promotion]    Script Date: 02/14/2014 13:06:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Detailed_Promotion
CREATE PROCEDURE [dbo].[efrcrm_insert_detailed_promotion] @Promotion_ID int OUTPUT, @Promotion_Type_Code varchar(4), @Target_Age_Group_Code varchar(4), @Target_Gender_Group_Code varchar(4), @Target_Group_Code varchar(4), @Promotion_Year smallint, @Promotion_Month smallint, @Description varchar(50), @Quantity_Sent int, @Call_Goal int, @Card_Budget int AS
begin

insert into Detailed_Promotion(Promotion_Type_Code, Target_Age_Group_Code, Target_Gender_Group_Code, Target_Group_Code, Promotion_Year, Promotion_Month, Description, Quantity_Sent, Call_Goal, Card_Budget) values(@Promotion_Type_Code, @Target_Age_Group_Code, @Target_Gender_Group_Code, @Target_Group_Code, @Promotion_Year, @Promotion_Month, @Description, @Quantity_Sent, @Call_Goal, @Card_Budget)

select @Promotion_ID = SCOPE_IDENTITY()

end
GO
