USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[IS_Receivable_By_Shipping]    Script Date: 02/14/2014 13:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  View dbo.IS_Receivable_By_Shipping    Script Date: 2003-02-22 20:34:17 ******/


/****** Object:  View dbo.IS_Receivable_By_Shipping    Script Date: 2/11/2003 12:27:44 PM ******/
create view [dbo].[IS_Receivable_By_Shipping] as(
--fA
  (select Sales_ID from Receivable_Ship_Sub_View) union
  (select Sales_ID from Receivable_Reship_Sub_View))
GO
