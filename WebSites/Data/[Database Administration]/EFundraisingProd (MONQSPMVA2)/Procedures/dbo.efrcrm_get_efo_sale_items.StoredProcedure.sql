USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_efo_sale_items]    Script Date: 02/14/2014 13:04:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for EFO_Sale_Item
CREATE PROCEDURE [dbo].[efrcrm_get_efo_sale_items] AS
begin

select Item_ID, Sale_ID, Quantity from EFO_Sale_Item

end
GO
