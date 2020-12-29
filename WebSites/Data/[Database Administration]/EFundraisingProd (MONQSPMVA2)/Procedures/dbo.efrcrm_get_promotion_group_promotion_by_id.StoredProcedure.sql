USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_promotion_group_promotion_by_id]    Script Date: 02/14/2014 13:05:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Promotion_Group_Promotion
CREATE PROCEDURE [dbo].[efrcrm_get_promotion_group_promotion_by_id] @Promo_Group_ID int AS
begin

select Promo_Group_ID, Promotion_ID from Promotion_Group_Promotion where Promo_Group_ID=@Promo_Group_ID

end
GO
