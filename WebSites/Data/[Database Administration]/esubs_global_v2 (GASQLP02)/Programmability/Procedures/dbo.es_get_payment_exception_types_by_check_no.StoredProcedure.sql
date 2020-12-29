USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_payment_exception_types_by_check_no]    Script Date: 02/14/2014 13:06:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Payment_exception_type
create  PROCEDURE [dbo].[es_get_payment_exception_types_by_check_no] @check_no int AS
begin

select pet.Payment_id, Exception_type_id, pet.Create_date, pet.Validated_date, pet.is_corrected
FROM         dbo.payment_exception_type pet INNER JOIN
              dbo.payment ON pet.payment_id = dbo.payment.payment_id
where cheque_number = @check_no
end
GO
