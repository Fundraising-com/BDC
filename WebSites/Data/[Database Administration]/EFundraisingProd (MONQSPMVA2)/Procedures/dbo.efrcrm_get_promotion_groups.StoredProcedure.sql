USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_promotion_groups]    Script Date: 02/14/2014 13:05:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Promotion_Group
CREATE PROCEDURE [dbo].[efrcrm_get_promotion_groups] AS
begin

select Promo_Group_ID, Description from Promotion_Group

end
GO
