USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_accounting_periods]    Script Date: 02/14/2014 13:03:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Accounting_Period
CREATE PROCEDURE [dbo].[efrcrm_get_accounting_periods] AS
begin

select Closing_Date from Accounting_Period

end
GO
