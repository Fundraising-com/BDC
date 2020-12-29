USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_payment_term_by_id]    Script Date: 02/14/2014 13:05:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Payment_term
create  PROCEDURE [dbo].[efrcrm_get_payment_term_by_id] 
                 @payment_term_id int
AS
begin

select Payment_term_id, Description, Discount_percent, Lead_days, Default_ar_status, Hide_from_consultants from Payment_term
where payment_term_id = @payment_term_id

end
GO
