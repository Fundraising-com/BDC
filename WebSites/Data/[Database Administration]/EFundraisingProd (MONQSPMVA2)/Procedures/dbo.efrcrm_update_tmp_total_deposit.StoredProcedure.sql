USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_tmp_total_deposit]    Script Date: 02/14/2014 13:08:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Tmp_total_deposit
CREATE PROCEDURE [dbo].[efrcrm_update_tmp_total_deposit] @Sales_ID int, @Total_Deposit decimal AS
begin

update Tmp_total_deposit set Total_Deposit=@Total_Deposit where Sales_ID=@Sales_ID

end
GO
