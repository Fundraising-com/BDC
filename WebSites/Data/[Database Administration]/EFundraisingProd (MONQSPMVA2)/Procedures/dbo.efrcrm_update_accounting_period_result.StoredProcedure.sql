USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_accounting_period_result]    Script Date: 02/14/2014 13:07:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Accounting_period_result
CREATE PROCEDURE [dbo].[efrcrm_update_accounting_period_result] @Accounting_period_result_id smallint, @Accounting_class_id tinyint, @Period smalldatetime, @Amount decimal, @Budgeted_amount decimal AS
begin

update Accounting_period_result set Accounting_class_id=@Accounting_class_id, Period=@Period, Amount=@Amount, Budgeted_amount=@Budgeted_amount where Accounting_period_result_id=@Accounting_period_result_id

end
GO
