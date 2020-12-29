USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[v_ageing]    Script Date: 02/14/2014 13:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_ageing]
AS
SELECT     Sales_ID, dbo.fn_Return_Ageing_By_Sale(Sales_ID, GETDATE()) AS ageing
FROM         dbo.Sale
GO
