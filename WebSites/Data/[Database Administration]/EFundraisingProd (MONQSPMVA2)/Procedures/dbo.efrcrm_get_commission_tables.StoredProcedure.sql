USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_commission_tables]    Script Date: 02/14/2014 13:04:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Commission_Table
CREATE PROCEDURE [dbo].[efrcrm_get_commission_tables] AS
begin

select Promotion_Type_Code, Channel_Code, Commission_Rate from Commission_Table

end
GO
