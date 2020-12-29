USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_postponed_sale_by_id]    Script Date: 02/14/2014 13:05:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Postponed_Sale
CREATE PROCEDURE [dbo].[efrcrm_get_postponed_sale_by_id] @Sales_ID int AS
begin

select Sales_ID, Postponed_Status_ID, Comments from Postponed_Sale where Sales_ID=@Sales_ID

end
GO
