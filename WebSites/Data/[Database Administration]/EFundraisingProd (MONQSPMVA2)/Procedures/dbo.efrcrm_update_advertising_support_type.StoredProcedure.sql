USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_advertising_support_type]    Script Date: 02/14/2014 13:07:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Advertising_Support_Type
CREATE PROCEDURE [dbo].[efrcrm_update_advertising_support_type] @Advertising_Support_Type_ID int, @Description varchar(50), @Comments varchar(255) AS
begin

update Advertising_Support_Type set Description=@Description, Comments=@Comments where Advertising_Support_Type_ID=@Advertising_Support_Type_ID

end
GO
