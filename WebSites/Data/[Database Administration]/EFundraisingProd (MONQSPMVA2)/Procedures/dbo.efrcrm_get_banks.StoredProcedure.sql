USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_banks]    Script Date: 02/14/2014 13:03:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Bank
CREATE PROCEDURE [dbo].[efrcrm_get_banks] AS
begin

select Bank_ID, Name, Contact, Street_Address, State_Code, City, Zip_Code, Country_Code, Telephone, Fax from Bank

end
GO
