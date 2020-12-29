USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_efo_sale_item_by_id]    Script Date: 02/14/2014 13:04:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for EFO_Sale_Item
CREATE PROCEDURE [dbo].[efrcrm_get_efo_sale_item_by_id] @Item_ID int AS
begin

select Item_ID, Sale_ID, Quantity from EFO_Sale_Item where Item_ID=@Item_ID

end
GO
