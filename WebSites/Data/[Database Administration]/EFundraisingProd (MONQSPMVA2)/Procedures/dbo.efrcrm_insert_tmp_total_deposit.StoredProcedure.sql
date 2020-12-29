USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_tmp_total_deposit]    Script Date: 02/14/2014 13:07:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Tmp_total_deposit
CREATE PROCEDURE [dbo].[efrcrm_insert_tmp_total_deposit] @Sales_ID int OUTPUT, @Total_Deposit decimal AS
begin

insert into Tmp_total_deposit(Total_Deposit) values(@Total_Deposit)

select @Sales_ID = SCOPE_IDENTITY()

end
GO
