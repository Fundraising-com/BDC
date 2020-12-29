USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[crm_get_promotion_detail]    Script Date: 02/14/2014 13:03:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  procedure [dbo].[crm_get_promotion_detail]
            @promotion_id as int            
   
           
as

SELECT     dbo.promotion.promotion_id, dbo.partner.partner_name as partner, dbo.Advertiser.Advertiser_Name as advertiser, dbo.promotion.script_name, 
                      dbo.promotion.description AS promotion, dbo.Promotion_Type.Description AS [promo_type], dbo.Promotion_Group.Description as promo_group, dbo.Promotion.promotion_type_code
FROM         dbo.Promotion_Group INNER JOIN
                      dbo.Promotion_Group_Promotion ON dbo.Promotion_Group.Promo_Group_ID = dbo.Promotion_Group_Promotion.Promo_Group_ID RIGHT OUTER JOIN
                      dbo.promotion LEFT OUTER JOIN
                      dbo.Promotion_Type ON dbo.promotion.promotion_type_code = dbo.Promotion_Type.Promotion_Type_Code ON 
                      dbo.Promotion_Group_Promotion.Promotion_ID = dbo.promotion.promotion_id LEFT OUTER JOIN
                      dbo.Advertiser ON dbo.promotion.advertiser_id = dbo.Advertiser.Advertiser_ID LEFT OUTER JOIN
                      dbo.partner ON dbo.promotion.partner_id = dbo.partner.partner_id
where dbo.promotion.promotion_id = @promotion_id
GO
