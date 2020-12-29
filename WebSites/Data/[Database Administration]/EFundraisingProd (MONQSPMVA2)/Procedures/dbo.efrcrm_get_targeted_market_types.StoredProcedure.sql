USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_targeted_market_types]    Script Date: 02/14/2014 13:06:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Targeted_market_type
CREATE PROCEDURE [dbo].[efrcrm_get_targeted_market_types] AS
begin

select Targeted_market_type_id, Description, Decision_maker, Group_type_id, Comments from Targeted_market_type

end
GO
