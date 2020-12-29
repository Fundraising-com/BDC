USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_accounting_period_result]    Script Date: 02/14/2014 13:06:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Accounting_period_result
CREATE PROCEDURE [dbo].[efrcrm_insert_accounting_period_result] @Accounting_period_result_id int OUTPUT, @Accounting_class_id tinyint, @Period smalldatetime, @Amount decimal, @Budgeted_amount decimal AS
begin

insert into Accounting_period_result(Accounting_class_id, Period, Amount, Budgeted_amount) values(@Accounting_class_id, @Period, @Amount, @Budgeted_amount)

select @Accounting_period_result_id = SCOPE_IDENTITY()

end
GO
