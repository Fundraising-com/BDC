USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_countrys]    Script Date: 02/14/2014 13:04:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Country
CREATE PROCEDURE [dbo].[efrcrm_get_countrys] AS
begin

select Country_Code, Country_Name, Currency_Code from Country

end
GO
