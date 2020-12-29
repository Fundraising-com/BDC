USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[applicable_tax_code]    Script Date: 02/14/2014 13:01:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  View dbo.applicable_tax_code    Script Date: 2003-02-22 20:34:18 ******/


/****** Object:  View dbo.applicable_tax_code    Script Date: 2/11/2003 12:27:44 PM ******/

create view [dbo].[applicable_tax_code] /* view column name,... */
  as select Sale.Sales_ID,Tax_Table.Tax_Code,State_Tax.State_Code,Max(State_Tax.Effective_Date) as Max_Effective_Date,State_Tax.Tax_Order from
    Tax_Table join(Sale join(Client_Address join State_Tax on Client_Address.State_Code = State_Tax.State_Code) on(Sale.Client_Sequence_Code = Client_Address.Client_Sequence_Code) and(Sale.Client_ID = Client_Address.Client_ID)) on Tax_Table.Tax_Code = State_Tax.Tax_Code where
    (((Client_Address.Address_Type) = 'BT') and((State_Tax.Effective_Date) <= Sale.Sales_Date))
--fA-41,B-15
    group by Sale.Sales_ID,Tax_Table.Tax_Code,State_Tax.State_Code,State_Tax.Tax_Order
GO
