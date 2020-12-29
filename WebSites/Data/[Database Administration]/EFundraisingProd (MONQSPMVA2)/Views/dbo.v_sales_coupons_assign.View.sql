USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[v_sales_coupons_assign]    Script Date: 02/14/2014 13:02:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_sales_coupons_assign]
AS
SELECT     dbo.Sale.Sales_ID, 1 AS Coupon_Sheet_Assigned
FROM         dbo.Sale INNER JOIN
                      dbo.Sales_Item_Coupon_Sheet ON dbo.Sale.Sales_ID = dbo.Sales_Item_Coupon_Sheet.Sales_ID
GROUP BY dbo.Sale.Sales_ID
GO
