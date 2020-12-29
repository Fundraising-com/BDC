USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_partner_fixed_cost]    Script Date: 02/14/2014 13:08:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Partner_Fixed_Cost
CREATE PROCEDURE [dbo].[efrcrm_update_partner_fixed_cost] @Partner_ID int, @Cost_By_Lead decimal AS
begin

update Partner_Fixed_Cost set Cost_By_Lead=@Cost_By_Lead where Partner_ID=@Partner_ID

end
GO
