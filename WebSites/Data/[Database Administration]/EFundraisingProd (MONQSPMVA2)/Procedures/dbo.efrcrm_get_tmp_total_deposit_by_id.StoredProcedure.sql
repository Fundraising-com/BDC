USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_tmp_total_deposit_by_id]    Script Date: 02/14/2014 13:06:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Tmp_total_deposit
CREATE PROCEDURE [dbo].[efrcrm_get_tmp_total_deposit_by_id] @Sales_ID int AS
begin

select Sales_ID, Total_Deposit from Tmp_total_deposit where Sales_ID=@Sales_ID

end
GO
