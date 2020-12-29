USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_applicable_tax_by_id]    Script Date: 02/14/2014 13:03:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Applicable_Tax
CREATE PROCEDURE [dbo].[efrcrm_get_applicable_tax_by_id] @Sales_ID int AS
begin

select Sales_ID, Tax_Code, Tax_Amount from Applicable_Tax where Sales_ID=@Sales_ID

end
GO
