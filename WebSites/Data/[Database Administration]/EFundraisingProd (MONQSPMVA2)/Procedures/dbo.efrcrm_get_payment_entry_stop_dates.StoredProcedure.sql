USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_payment_entry_stop_dates]    Script Date: 02/14/2014 13:05:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Payment_Entry_Stop_Date
CREATE PROCEDURE [dbo].[efrcrm_get_payment_entry_stop_dates] AS
begin

select Payment_Entry_Stop_Date from Payment_Entry_Stop_Date

end
GO
