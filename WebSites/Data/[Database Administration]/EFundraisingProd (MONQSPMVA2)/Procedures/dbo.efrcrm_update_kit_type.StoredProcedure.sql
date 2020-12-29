USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_kit_type]    Script Date: 02/14/2014 13:08:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Kit_Type
CREATE PROCEDURE [dbo].[efrcrm_update_kit_type] @Kit_Type_ID int, @Description varchar(50), @Delivery_Time datetime, @Comments text, @Is_Default bit AS
begin

update Kit_Type set Description=@Description, Delivery_Time=@Delivery_Time, Comments=@Comments, Is_Default=@Is_Default where Kit_Type_ID=@Kit_Type_ID

end
GO
