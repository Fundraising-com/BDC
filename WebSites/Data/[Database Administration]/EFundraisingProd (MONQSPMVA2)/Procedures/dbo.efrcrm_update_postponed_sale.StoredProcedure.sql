USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_postponed_sale]    Script Date: 02/14/2014 13:08:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Postponed_Sale
CREATE PROCEDURE [dbo].[efrcrm_update_postponed_sale] @Sales_ID int, @Postponed_Status_ID int, @Comments varchar(255) AS
begin

update Postponed_Sale set Postponed_Status_ID=@Postponed_Status_ID, Comments=@Comments where Sales_ID=@Sales_ID

end
GO
