USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_payment_exception_types_by_group_id]    Script Date: 02/14/2014 13:06:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Payment_exception_type
create PROCEDURE [dbo].[es_get_payment_exception_types_by_group_id] @group_id int AS
begin

select pet.Payment_id, Exception_type_id, pet.Create_date, pet.Validated_date, pet.is_corrected
FROM         dbo.payment_exception_type pet INNER JOIN
              dbo.payment ON pet.payment_id = dbo.payment.payment_id INNER JOIN
              dbo.payment_info ON dbo.payment.payment_info_id = dbo.payment_info.payment_info_id
where group_id = @group_id
end
GO
