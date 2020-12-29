USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[coupon_sheets]    Script Date: 02/14/2014 13:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  VIEW [dbo].[coupon_sheets](Coupon_Sheet_ID ,
                               Product_Code ,
                               Description ,
                               sheets_assigned ) AS
SELECT dbo.Sales_Item_Coupon_Sheet.Coupon_Sheet_ID,
       dbo.Coupon_Sheet.Product_Code,
       dbo.Coupon_Sheet.Description,
       SUM(cast(dbo.Sales_Item_Coupon_Sheet.Sheet_Per_Booklet as int) * (dbo.Sales_Item.Quantity_Free + dbo.Sales_Item.Quantity_Sold)) AS sheets_assigned
FROM         dbo.Coupon_Sheet INNER JOIN

dbo.Sales_Item_Coupon_Sheet INNER JOIN dbo.Sale ON

dbo.Sales_Item_Coupon_Sheet.Sales_ID = dbo.Sale.Sales_ID ON

dbo.Coupon_Sheet.Coupon_Sheet_ID = dbo.Sales_Item_Coupon_Sheet.Coupon_Sheet_ID INNER JOIN dbo.Sales_Item ON

dbo.Sales_Item.Sales_Item_No = dbo.Sales_Item_Coupon_Sheet.Sales_Item_No

AND

dbo.Sales_Item.Sales_ID = dbo.Sales_Item_Coupon_Sheet.Sales_ID

AND

dbo.Sale.Sales_ID = dbo.Sales_Item.Sales_ID

WHERE     (dbo.Sale.Actual_Ship_Date BETWEEN

'3/31/03' AND '4/2/03')

GROUP BY dbo.Sales_Item_Coupon_Sheet.Coupon_Sheet_ID,

dbo.Coupon_Sheet.Product_Code,

dbo.Coupon_Sheet.Description
GO
