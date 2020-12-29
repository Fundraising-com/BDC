USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_befree_historys]    Script Date: 02/14/2014 13:03:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for BeFree_History
CREATE PROCEDURE [dbo].[efrcrm_get_befree_historys] AS
begin

select Merchant_ID, Record_Type, Date_Insert, Source_ID, Transaction_ID, Product_Key, Qty_Product, Unit_Price, Currency_Type, Merchandise_Type from BeFree_History

end
GO
