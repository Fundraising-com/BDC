USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[applicable_tax_rate]    Script Date: 02/14/2014 13:01:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  View dbo.applicable_tax_rate    Script Date: 2003-02-22 20:34:18 ******/


/****** Object:  View dbo.applicable_tax_rate    Script Date: 2/11/2003 12:27:44 PM ******/
create view [dbo].[applicable_tax_rate] /* view column name,... */
  as select applicable_tax_code.Sales_ID,applicable_tax_code.Tax_Code,State_Tax.Tax_Rate,applicable_tax_code.Tax_Order from
    applicable_tax_code join State_Tax on(applicable_tax_code.Max_Effective_Date = State_Tax.Effective_Date) and(applicable_tax_code.State_Code = State_Tax.State_Code) and(applicable_tax_code.Tax_Code = State_Tax.Tax_Code)
GO
