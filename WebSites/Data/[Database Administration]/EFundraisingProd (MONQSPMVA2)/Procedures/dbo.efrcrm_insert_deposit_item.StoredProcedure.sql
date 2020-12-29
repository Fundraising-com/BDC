USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_deposit_item]    Script Date: 02/14/2014 13:06:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Deposit_Item
CREATE PROCEDURE [dbo].[efrcrm_insert_deposit_item] @Deposit_ID int OUTPUT, @Sales_ID int, @Paiement_No int AS
begin

insert into Deposit_Item(Sales_ID, Paiement_No) values(@Sales_ID, @Paiement_No)

select @Deposit_ID = SCOPE_IDENTITY()

end
GO
