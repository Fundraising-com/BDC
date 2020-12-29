USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_payment_no_by_sale_id]    Script Date: 02/14/2014 13:05:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Payment
CREATE PROCEDURE [dbo].[efrcrm_get_payment_no_by_sale_id]  --1002
@Sales_id int,
@payment_no int OUTPUT
 AS
begin

set @payment_no = 1

if exists(select * from payment where Sales_id = @Sales_id)
    select @payment_no = max(payment_no) + 1 from payment
    where Sales_id = @Sales_id
 


end
GO
