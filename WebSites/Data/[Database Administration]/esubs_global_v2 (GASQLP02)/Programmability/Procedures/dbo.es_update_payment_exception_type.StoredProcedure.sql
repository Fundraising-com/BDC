USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_payment_exception_type]    Script Date: 02/14/2014 13:07:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Payment_exception_type
CREATE  PROCEDURE [dbo].[es_update_payment_exception_type] @Payment_id int, @Exception_type_id int, @Create_date datetime, @Validated_date datetime, @is_corrected bit AS
begin

update Payment_exception_type set Exception_type_id=@Exception_type_id, Create_date=@Create_date, Validated_date=@Validated_date, is_corrected = @is_corrected where Payment_id=@Payment_id AND exception_type_id = @exception_type_id

end
GO
