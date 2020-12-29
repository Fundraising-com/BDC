USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[view_taxSummaryView]    Script Date: 02/14/2014 13:02:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[view_taxSummaryView]
AS
SELECT     Sales_ID, SUM(Tax_Amount) AS Total_Tax_Amount_Base
FROM         dbo.Applicable_Tax
GROUP BY Sales_ID
GO
