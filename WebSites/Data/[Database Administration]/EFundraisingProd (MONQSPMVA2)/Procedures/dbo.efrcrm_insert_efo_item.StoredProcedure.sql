USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_efo_item]    Script Date: 02/14/2014 13:06:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for EFO_Item
CREATE PROCEDURE [dbo].[efrcrm_insert_efo_item] @Item_ID int OUTPUT, @Title varchar(20), @Price decimal, @Amount2Supplier decimal, @Amount2Group decimal, @Description varchar(150) AS
begin

insert into EFO_Item(Title, Price, Amount2Supplier, Amount2Group, Description) values(@Title, @Price, @Amount2Supplier, @Amount2Group, @Description)

select @Item_ID = SCOPE_IDENTITY()

end
GO
