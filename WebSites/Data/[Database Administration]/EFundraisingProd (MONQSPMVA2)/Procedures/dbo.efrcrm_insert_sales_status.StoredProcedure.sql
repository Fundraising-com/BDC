USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_sales_status]    Script Date: 02/14/2014 13:07:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Sales_Status
CREATE PROCEDURE [dbo].[efrcrm_insert_sales_status] @Sales_Status_ID int OUTPUT, @Description varchar(50) AS
begin

insert into Sales_Status(Description) values(@Description)

select @Sales_Status_ID = SCOPE_IDENTITY()

end
GO
