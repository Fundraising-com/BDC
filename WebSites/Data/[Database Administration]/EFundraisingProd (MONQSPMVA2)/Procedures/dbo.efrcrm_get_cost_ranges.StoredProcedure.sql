USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_cost_ranges]    Script Date: 02/14/2014 13:04:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Cost_range
CREATE PROCEDURE [dbo].[efrcrm_get_cost_ranges] AS
begin

select Cost_range_id, Scratch_book_id, Service_type_id, Minimum, Maximum, Cost, Margin_plan from Cost_range

end
GO
