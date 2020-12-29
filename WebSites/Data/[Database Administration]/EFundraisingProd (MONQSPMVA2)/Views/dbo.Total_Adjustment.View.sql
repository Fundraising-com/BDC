USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[Total_Adjustment]    Script Date: 02/14/2014 13:02:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  View dbo.Total_Adjustment    Script Date: 2003-02-22 20:34:18 ******/


/****** Object:  View dbo.Total_Adjustment    Script Date: 2/11/2003 12:27:44 PM ******/

create view [dbo].[Total_Adjustment](Sales_ID,Adjustment_Amount)
--fA
--fA-41,B-15
  as(select Sales_ID,cast(SUM(Adjustment_Amount) as numeric(15,4)) from Adjustment group by Sales_ID)
GO
