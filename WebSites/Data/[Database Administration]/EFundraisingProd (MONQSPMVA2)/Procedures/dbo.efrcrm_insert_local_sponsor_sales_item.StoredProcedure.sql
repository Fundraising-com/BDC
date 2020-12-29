USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_local_sponsor_sales_item]    Script Date: 02/14/2014 13:07:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Local_Sponsor_Sales_Item
CREATE PROCEDURE [dbo].[efrcrm_insert_local_sponsor_sales_item] @Brand_ID int OUTPUT, @Local_Sponsor_ID int, @Sales_ID int, @Sales_Item_No smallint AS
begin

insert into Local_Sponsor_Sales_Item(Local_Sponsor_ID, Sales_ID, Sales_Item_No) values(@Local_Sponsor_ID, @Sales_ID, @Sales_Item_No)

select @Brand_ID = SCOPE_IDENTITY()

end
GO
