USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_postponed_sales]    Script Date: 02/14/2014 13:05:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Postponed_Sale
CREATE PROCEDURE [dbo].[efrcrm_get_postponed_sales] AS
begin

select Sales_ID, Postponed_Status_ID, Comments from Postponed_Sale

end
GO
