USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_payment_methods]    Script Date: 02/14/2014 13:05:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Payment_method
CREATE  PROCEDURE [dbo].[efrcrm_get_payment_methods] AS
begin

select Payment_method_id, Description, Is_negative, Discount_percentage from Payment_method
order by payment_method_id

end
GO
