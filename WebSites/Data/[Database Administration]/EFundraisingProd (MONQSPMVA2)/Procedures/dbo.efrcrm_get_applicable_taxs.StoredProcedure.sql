USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_applicable_taxs]    Script Date: 02/14/2014 13:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Applicable_Tax
CREATE PROCEDURE [dbo].[efrcrm_get_applicable_taxs] AS
begin

select Sales_ID, Tax_Code, Tax_Amount from Applicable_Tax

end
GO
