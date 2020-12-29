USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_countriess]    Script Date: 02/14/2014 13:04:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Countries
CREATE PROCEDURE [dbo].[efrcrm_get_countriess] AS
begin

select Country_code, Country_name, Long_country_code, Numeric_code, Currency_code from Countries

end
GO
