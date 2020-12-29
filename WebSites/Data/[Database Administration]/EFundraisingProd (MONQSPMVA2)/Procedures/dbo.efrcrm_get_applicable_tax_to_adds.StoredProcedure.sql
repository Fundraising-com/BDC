USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_applicable_tax_to_adds]    Script Date: 02/14/2014 13:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Applicable_Tax_To_Add
CREATE PROCEDURE [dbo].[efrcrm_get_applicable_tax_to_adds] AS
begin

select Tax_Code, Sale_To_Add_ID, Tax_Amount from Applicable_Tax_To_Add

end
GO
