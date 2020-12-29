USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_sale_to_local_sponsor]    Script Date: 02/14/2014 13:07:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Sale_To_Local_Sponsor
CREATE PROCEDURE [dbo].[efrcrm_insert_sale_to_local_sponsor] @Sales_ID int OUTPUT, @Brand_ID int, @Local_Sponsor_ID int, @Assigned_Date datetime, @Comments varchar(255) AS
begin

insert into Sale_To_Local_Sponsor(Brand_ID, Local_Sponsor_ID, Assigned_Date, Comments) values(@Brand_ID, @Local_Sponsor_ID, @Assigned_Date, @Comments)

select @Sales_ID = SCOPE_IDENTITY()

end
GO
