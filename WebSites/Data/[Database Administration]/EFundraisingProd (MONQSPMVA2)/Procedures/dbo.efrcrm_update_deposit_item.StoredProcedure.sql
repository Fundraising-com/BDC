USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_deposit_item]    Script Date: 02/14/2014 13:07:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Deposit_Item
CREATE PROCEDURE [dbo].[efrcrm_update_deposit_item] @Deposit_ID int, @Sales_ID int, @Paiement_No int AS
begin

update Deposit_Item set Sales_ID=@Sales_ID, Paiement_No=@Paiement_No where Deposit_ID=@Deposit_ID

end
GO
