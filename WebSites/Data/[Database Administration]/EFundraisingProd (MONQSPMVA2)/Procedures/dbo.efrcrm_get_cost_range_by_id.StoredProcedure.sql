USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_cost_range_by_id]    Script Date: 02/14/2014 13:04:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Cost_range
CREATE PROCEDURE [dbo].[efrcrm_get_cost_range_by_id] @Cost_range_id int AS
begin

select Cost_range_id, Scratch_book_id, Service_type_id, Minimum, Maximum, Cost, Margin_plan from Cost_range where Cost_range_id=@Cost_range_id

end
GO
