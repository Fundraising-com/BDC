USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_promotion_group_promotion]    Script Date: 02/14/2014 13:08:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Promotion_Group_Promotion
CREATE PROCEDURE [dbo].[efrcrm_update_promotion_group_promotion] @Promo_Group_ID int, @Promotion_ID int AS
begin

update Promotion_Group_Promotion set Promotion_ID=@Promotion_ID where Promo_Group_ID=@Promo_Group_ID

end
GO
