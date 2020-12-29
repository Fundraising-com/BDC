USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_efo_item_by_id]    Script Date: 02/14/2014 13:04:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for EFO_Item
CREATE PROCEDURE [dbo].[efrcrm_get_efo_item_by_id] @Item_ID int AS
begin

select Item_ID, Title, Price, Amount2Supplier, Amount2Group, Description from EFO_Item where Item_ID=@Item_ID

end
GO
