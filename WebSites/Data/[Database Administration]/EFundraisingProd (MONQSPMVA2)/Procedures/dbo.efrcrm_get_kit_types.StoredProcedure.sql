USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_kit_types]    Script Date: 02/14/2014 13:04:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Kit_Type
CREATE PROCEDURE [dbo].[efrcrm_get_kit_types] AS
begin

select Kit_Type_ID, Description, Delivery_Time, Comments, Is_Default from Kit_Type

end
GO
