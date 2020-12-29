USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_promotion_group]    Script Date: 02/14/2014 13:08:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Promotion_Group
CREATE PROCEDURE [dbo].[efrcrm_update_promotion_group] @Promo_Group_ID int, @Description nvarchar(100) AS
begin

update Promotion_Group set Description=@Description where Promo_Group_ID=@Promo_Group_ID

end
GO
