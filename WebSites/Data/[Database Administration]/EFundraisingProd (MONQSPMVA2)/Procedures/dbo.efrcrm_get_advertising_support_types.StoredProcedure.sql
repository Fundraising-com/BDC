USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_advertising_support_types]    Script Date: 02/14/2014 13:03:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Advertising_Support_Type
CREATE PROCEDURE [dbo].[efrcrm_get_advertising_support_types] AS
begin

select Advertising_Support_Type_ID, Description, Comments from Advertising_Support_Type

end
GO
