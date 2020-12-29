USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_accounting_period_results]    Script Date: 02/14/2014 13:03:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Accounting_period_result
CREATE PROCEDURE [dbo].[efrcrm_get_accounting_period_results] AS
begin

select Accounting_period_result_id, Accounting_class_id, Period, Amount, Budgeted_amount from Accounting_period_result

end
GO
