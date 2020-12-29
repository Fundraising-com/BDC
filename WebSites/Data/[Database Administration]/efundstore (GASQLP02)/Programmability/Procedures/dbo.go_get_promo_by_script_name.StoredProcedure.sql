USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[go_get_promo_by_script_name]    Script Date: 02/14/2014 13:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[go_get_promo_by_script_name]
    @script_name varchar(1024)
    , @partner_id int = 0
as
begin
    
    SELECT [Promotion_ID]
    FROM [eFundweb].[dbo].[Promotion] [promo]
    WHERE [promo].[partner_id] = @partner_id
      AND [promo].[script_name] = @script_name
    

    IF @@error <> 0
    BEGIN
        RETURN -1
    END
    
    RETURN 0
end
GO
