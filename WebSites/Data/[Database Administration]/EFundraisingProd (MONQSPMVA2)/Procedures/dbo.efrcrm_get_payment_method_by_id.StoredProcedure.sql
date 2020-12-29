USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_payment_method_by_id]    Script Date: 02/14/2014 13:05:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Payment_method
CREATE PROCEDURE [dbo].[efrcrm_get_payment_method_by_id] @Payment_method_id tinyint AS
begin

select Payment_method_id, 
	Description, 
	Is_negative, 
	Discount_percentage 
from Payment_method
	where Payment_method_id = @Payment_method_id

end
GO
