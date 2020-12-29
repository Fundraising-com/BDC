USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_efo_sale_item]    Script Date: 02/14/2014 13:06:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for EFO_Sale_Item
CREATE PROCEDURE [dbo].[efrcrm_insert_efo_sale_item] @Item_ID int OUTPUT, @Sale_ID int, @Quantity decimal AS
begin

insert into EFO_Sale_Item(Sale_ID, Quantity) values(@Sale_ID, @Quantity)

select @Item_ID = SCOPE_IDENTITY()

end
GO
