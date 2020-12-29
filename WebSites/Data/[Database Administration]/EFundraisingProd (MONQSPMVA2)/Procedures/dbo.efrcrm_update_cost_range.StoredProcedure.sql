USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_cost_range]    Script Date: 02/14/2014 13:07:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Cost_range
CREATE PROCEDURE [dbo].[efrcrm_update_cost_range] @Cost_range_id int, @Scratch_book_id int, @Service_type_id tinyint, @Minimum int, @Maximum int, @Cost float, @Margin_plan decimal AS
begin

update Cost_range set Scratch_book_id=@Scratch_book_id, Service_type_id=@Service_type_id, Minimum=@Minimum, Maximum=@Maximum, Cost=@Cost, Margin_plan=@Margin_plan where Cost_range_id=@Cost_range_id

end
GO
