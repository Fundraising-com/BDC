USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_alias_promotions]    Script Date: 02/14/2014 13:03:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Alias_Promotion
CREATE PROCEDURE [dbo].[efrcrm_get_alias_promotions] AS
begin

select Cookie_Content, Promotion_ID from Alias_Promotion

end
GO
