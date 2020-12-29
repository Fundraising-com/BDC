USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_payment_exception_types]    Script Date: 02/14/2014 13:06:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Payment_exception_type
CREATE  PROCEDURE [dbo].[es_get_payment_exception_types] AS
begin

select Payment_id, Exception_type_id, Create_date, Validated_date, is_corrected from Payment_exception_type

end
GO
