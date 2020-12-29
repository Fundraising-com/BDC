USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_efo_item]    Script Date: 02/14/2014 13:07:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for EFO_Item
CREATE PROCEDURE [dbo].[efrcrm_update_efo_item] @Item_ID int, @Title varchar(20), @Price decimal, @Amount2Supplier decimal, @Amount2Group decimal, @Description varchar(150) AS
begin

update EFO_Item set Title=@Title, Price=@Price, Amount2Supplier=@Amount2Supplier, Amount2Group=@Amount2Group, Description=@Description where Item_ID=@Item_ID

end
GO
