USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_comment_by_sales_id_and_lead_id]    Script Date: 02/14/2014 13:04:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Comments
create  PROCEDURE [dbo].[efrcrm_get_comment_by_sales_id_and_lead_id] 
                 @Sales_ID int,
                 @Lead_id int
AS
begin

select top 1 Comments_ID, Priority_ID, Sales_ID, Consultant_ID, Lead_ID, Department_ID, Entry_Date, Comments 
from Comments 
where Sales_ID= @Sales_ID and lead_id = @lead_id
order by entry_date desc

end
GO
