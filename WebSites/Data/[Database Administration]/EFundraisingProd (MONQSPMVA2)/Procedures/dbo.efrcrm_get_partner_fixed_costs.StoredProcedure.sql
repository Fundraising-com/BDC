USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_partner_fixed_costs]    Script Date: 02/14/2014 13:05:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Partner_Fixed_Cost
CREATE PROCEDURE [dbo].[efrcrm_get_partner_fixed_costs] AS
begin

select Partner_ID, Cost_By_Lead from Partner_Fixed_Cost

end
GO
