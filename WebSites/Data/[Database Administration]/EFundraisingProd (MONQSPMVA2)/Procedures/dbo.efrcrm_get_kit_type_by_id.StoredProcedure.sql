USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_kit_type_by_id]    Script Date: 02/14/2014 13:04:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Kit_Type
CREATE PROCEDURE [dbo].[efrcrm_get_kit_type_by_id] @Kit_Type_ID int AS
begin

select Kit_Type_ID, Description, Delivery_Time, Comments, Is_Default from Kit_Type where Kit_Type_ID=@Kit_Type_ID

end
GO
