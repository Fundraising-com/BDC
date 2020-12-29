USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[Total_Payment]    Script Date: 02/14/2014 13:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  View dbo.Total_Payment    Script Date: 2003-02-22 20:34:18 ******/


/****** Object:  View dbo.Total_Payment    Script Date: 2/11/2003 12:27:44 PM ******/
create view [dbo].[Total_Payment](Sales_ID,Payment_Amount)
--fA
--fA-41,B-15
  as(select Sales_ID,cast(SUM(Payment_Amount) as numeric(15,4)) from Payment group by Sales_ID)
GO
