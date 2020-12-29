USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_sale_to_local_sponsor]    Script Date: 02/14/2014 13:08:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Sale_To_Local_Sponsor
CREATE PROCEDURE [dbo].[efrcrm_update_sale_to_local_sponsor] @Sales_ID int, @Brand_ID int, @Local_Sponsor_ID int, @Assigned_Date datetime, @Comments varchar(255) AS
begin

update Sale_To_Local_Sponsor set Brand_ID=@Brand_ID, Local_Sponsor_ID=@Local_Sponsor_ID, Assigned_Date=@Assigned_Date, Comments=@Comments where Sales_ID=@Sales_ID

end
GO
