USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_promotion_group_promotion]    Script Date: 02/14/2014 13:07:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Promotion_Group_Promotion
CREATE PROCEDURE [dbo].[efrcrm_insert_promotion_group_promotion] @Promo_Group_ID int OUTPUT, @Promotion_ID int AS
begin

insert into Promotion_Group_Promotion(Promotion_ID) values(@Promotion_ID)

select @Promo_Group_ID = SCOPE_IDENTITY()

end
GO
