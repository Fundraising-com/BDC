USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[AR_Amount]    Script Date: 02/14/2014 13:01:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/****** Object:  View dbo.AR_Amount    Script Date: 2003-02-22 20:34:16 ******/
create view [dbo].[AR_Amount](Sales_ID,AR_Amount)
--fA
  as(select Sale.Sales_ID,cast(Total_Amount-COALESCE(Payment_Amount,0)-COALESCE(Adjustment_Amount,0) as numeric(10,4)) from(
    Sale left outer join Total_Adjustment on Sale.Sales_ID = Total_Adjustment.Sales_ID) left outer join
    Total_Payment on Sale.Sales_ID = Total_Payment.Sales_ID)
GO
