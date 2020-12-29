USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_partner_fixed_cost_by_id]    Script Date: 02/14/2014 13:05:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Partner_Fixed_Cost
CREATE PROCEDURE [dbo].[efrcrm_get_partner_fixed_cost_by_id] @Partner_ID int AS
begin

select Partner_ID, Cost_By_Lead from Partner_Fixed_Cost where Partner_ID=@Partner_ID

end
GO
