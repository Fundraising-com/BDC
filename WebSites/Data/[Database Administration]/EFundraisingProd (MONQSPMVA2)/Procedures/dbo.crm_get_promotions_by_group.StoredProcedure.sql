USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[crm_get_promotions_by_group]    Script Date: 02/14/2014 13:03:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE    procedure [dbo].[crm_get_promotions_by_group]
           @promo_group_id as int 
   
           
as

if @promo_group_id = 0
begin

SELECT     p.description,
           p.promotion_id,
           p.promotion_type_code,
           p.partner_id
FROM        dbo.promotion p LEFT OUTER JOIN
            dbo.Promotion_Group_Promotion pgp ON pgp.Promotion_ID = p.promotion_id 
WHERE     (pgp.Promo_Group_ID IS NULL)

end
else
begin
sELECT   p.description,
         p.promotion_id,
         p.promotion_type_code,
         p.partner_id
FROM     dbo.promotion p INNER JOIN
         dbo.Promotion_Group_Promotion pgp ON p.promotion_id = pgp.Promotion_ID 
WHERE    pgp.Promo_Group_ID = @promo_group_id
end
GO
