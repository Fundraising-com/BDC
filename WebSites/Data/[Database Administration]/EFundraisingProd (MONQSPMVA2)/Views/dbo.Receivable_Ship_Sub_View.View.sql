USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[Receivable_Ship_Sub_View]    Script Date: 02/14/2014 13:02:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  View dbo.Receivable_Ship_Sub_View    Script Date: 2003-02-22 20:34:17 ******/


/****** Object:  View dbo.Receivable_Ship_Sub_View    Script Date: 2/11/2003 12:27:44 PM ******/
create view [dbo].[Receivable_Ship_Sub_View]
--fA
  as(select Sales_ID from Sale where Actual_Ship_Date is not null and
    Box_Return_Date is null)
GO
