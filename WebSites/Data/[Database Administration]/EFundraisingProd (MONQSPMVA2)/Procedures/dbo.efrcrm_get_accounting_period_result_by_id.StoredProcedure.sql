USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_accounting_period_result_by_id]    Script Date: 02/14/2014 13:03:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Accounting_period_result
CREATE PROCEDURE [dbo].[efrcrm_get_accounting_period_result_by_id] @Accounting_period_result_id int AS
begin

select Accounting_period_result_id, Accounting_class_id, Period, Amount, Budgeted_amount from Accounting_period_result where Accounting_period_result_id=@Accounting_period_result_id

end
GO
