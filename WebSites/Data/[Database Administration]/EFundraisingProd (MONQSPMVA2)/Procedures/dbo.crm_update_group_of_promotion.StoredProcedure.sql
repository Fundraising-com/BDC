USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[crm_update_group_of_promotion]    Script Date: 02/14/2014 13:03:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  procedure [dbo].[crm_update_group_of_promotion]
            @promo_group_id as int,
            @promotion_id as int            
   
           
as

if @promo_group_id = 0
begin

delete from promotion_group_promotion 
where  promotion_id = @promotion_id


end
else
begin

insert into promotion_group_promotion 
values (@promo_group_id, @promotion_id)

end
GO
