USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_applicable_adjustment_tax_by_id]    Script Date: 02/14/2014 13:03:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Applicable_Adjustment_Tax
CREATE PROCEDURE [dbo].[efrcrm_get_applicable_adjustment_tax_by_id] @Sales_Id int AS
begin

select Sales_Id, Adjustement_No, Tax_Code, Tax_Amount from Applicable_Adjustment_Tax where Sales_Id=@Sales_Id

end
GO
