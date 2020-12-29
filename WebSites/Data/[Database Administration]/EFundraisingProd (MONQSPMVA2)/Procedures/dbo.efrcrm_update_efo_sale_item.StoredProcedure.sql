USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_efo_sale_item]    Script Date: 02/14/2014 13:07:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for EFO_Sale_Item
CREATE PROCEDURE [dbo].[efrcrm_update_efo_sale_item] @Item_ID int, @Sale_ID int, @Quantity decimal AS
begin

update EFO_Sale_Item set Sale_ID=@Sale_ID, Quantity=@Quantity where Item_ID=@Item_ID

end
GO
