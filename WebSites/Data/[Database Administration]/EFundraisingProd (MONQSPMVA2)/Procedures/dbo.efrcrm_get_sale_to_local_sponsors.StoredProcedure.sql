USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_sale_to_local_sponsors]    Script Date: 02/14/2014 13:05:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Sale_To_Local_Sponsor
CREATE PROCEDURE [dbo].[efrcrm_get_sale_to_local_sponsors] AS
begin

select Sales_ID, Brand_ID, Local_Sponsor_ID, Assigned_Date, Comments from Sale_To_Local_Sponsor

end
GO
