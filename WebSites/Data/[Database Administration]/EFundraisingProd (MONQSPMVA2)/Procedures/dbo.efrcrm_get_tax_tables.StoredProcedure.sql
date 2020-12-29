USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_tax_tables]    Script Date: 02/14/2014 13:06:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Tax_Table
CREATE PROCEDURE [dbo].[efrcrm_get_tax_tables] AS
begin

select Tax_Code, Description, Tax_Account_Number, Description_francaise from Tax_Table

end
GO
