USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_tmp_total_deposits]    Script Date: 02/14/2014 13:06:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Tmp_total_deposit
CREATE PROCEDURE [dbo].[efrcrm_get_tmp_total_deposits] AS
begin

select Sales_ID, Total_Deposit from Tmp_total_deposit

end
GO
