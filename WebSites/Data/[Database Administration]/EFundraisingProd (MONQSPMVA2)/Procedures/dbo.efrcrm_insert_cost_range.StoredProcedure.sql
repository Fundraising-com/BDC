USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_cost_range]    Script Date: 02/14/2014 13:06:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Cost_range
CREATE PROCEDURE [dbo].[efrcrm_insert_cost_range] @Cost_range_id int OUTPUT, @Scratch_book_id int, @Service_type_id tinyint, @Minimum int, @Maximum int, @Cost float, @Margin_plan decimal AS
begin

insert into Cost_range(Scratch_book_id, Service_type_id, Minimum, Maximum, Cost, Margin_plan) values(@Scratch_book_id, @Service_type_id, @Minimum, @Maximum, @Cost, @Margin_plan)

select @Cost_range_id = SCOPE_IDENTITY()

end
GO
