USE [eFundweb]
GO
/****** Object:  StoredProcedure [dbo].[efr_get_promo_by_script_name]    Script Date: 02/14/2014 13:04:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[efr_get_promo_by_script_name]
    @script_name varchar(1024)
as
begin
    
    SELECT [Promotion_ID]
		 , [URL]
		, [partner_id]
    FROM [eFundweb].[dbo].[Promotion] [promo]
		LEFT JOIN [eFundweb].[dbo].[Destinations] dest
			ON dest.destination_id = promo.destination_id
    WHERE [promo].[script_name] = @script_name
    

    IF @@error <> 0
    BEGIN
        RETURN -1
    END
    
    RETURN 0
end
GO
