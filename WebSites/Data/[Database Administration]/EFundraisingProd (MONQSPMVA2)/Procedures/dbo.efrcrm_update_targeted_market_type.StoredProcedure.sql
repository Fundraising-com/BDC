USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_targeted_market_type]    Script Date: 02/14/2014 13:08:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Targeted_market_type
CREATE PROCEDURE [dbo].[efrcrm_update_targeted_market_type] @Targeted_market_type_id int, @Description varchar(50), @Decision_maker bit, @Group_type_id tinyint, @Comments varchar(255) AS
begin

update Targeted_market_type set Description=@Description, Decision_maker=@Decision_maker, Group_type_id=@Group_type_id, Comments=@Comments where Targeted_market_type_id=@Targeted_market_type_id

end
GO
