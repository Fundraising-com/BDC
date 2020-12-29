USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_conversion_rate_tables]    Script Date: 02/14/2014 13:04:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Conversion_Rate_Table
CREATE PROCEDURE [dbo].[efrcrm_get_conversion_rate_tables] AS
begin

select Currency_Code, Conversion_Rate, Conversion_Date, Conversion_Rate_Id from Conversion_Rate_Table

end
GO
