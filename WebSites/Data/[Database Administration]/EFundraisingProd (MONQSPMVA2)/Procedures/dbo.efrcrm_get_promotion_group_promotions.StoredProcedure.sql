USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_promotion_group_promotions]    Script Date: 02/14/2014 13:05:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Promotion_Group_Promotion
CREATE PROCEDURE [dbo].[efrcrm_get_promotion_group_promotions] AS
begin

select Promo_Group_ID, Promotion_ID from Promotion_Group_Promotion

end
GO
