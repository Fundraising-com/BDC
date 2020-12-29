USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_advertising_support_type_by_id]    Script Date: 02/14/2014 13:03:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Advertising_Support_Type
CREATE PROCEDURE [dbo].[efrcrm_get_advertising_support_type_by_id] @Advertising_Support_Type_ID int AS
begin

select Advertising_Support_Type_ID, Description, Comments from Advertising_Support_Type where Advertising_Support_Type_ID=@Advertising_Support_Type_ID

end
GO
