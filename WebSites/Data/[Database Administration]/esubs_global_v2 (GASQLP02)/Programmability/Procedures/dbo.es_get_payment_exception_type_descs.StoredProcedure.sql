USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_payment_exception_type_descs]    Script Date: 02/14/2014 13:06:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Payment_item
CREATE  PROCEDURE [dbo].[es_get_payment_exception_type_descs] AS
begin

select Payment_exception_type_desc_id, [description] from Payment_exception_type_desc

end
GO
