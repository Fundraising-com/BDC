USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_partner_fixed_cost]    Script Date: 02/14/2014 13:07:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Partner_Fixed_Cost
CREATE PROCEDURE [dbo].[efrcrm_insert_partner_fixed_cost] @Partner_ID int OUTPUT, @Cost_By_Lead decimal AS
begin

insert into Partner_Fixed_Cost(Cost_By_Lead) values(@Cost_By_Lead)

select @Partner_ID = SCOPE_IDENTITY()

end
GO
