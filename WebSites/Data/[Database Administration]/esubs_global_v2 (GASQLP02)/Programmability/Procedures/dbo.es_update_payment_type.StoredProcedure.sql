USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_payment_type]    Script Date: 02/14/2014 13:07:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Payment_type
CREATE PROCEDURE [dbo].[es_update_payment_type] @Payment_type_id int, @Payment_type_name varchar(50), @Create_date datetime AS
begin

update Payment_type set Payment_type_name=@Payment_type_name, Create_date=@Create_date where Payment_type_id=@Payment_type_id

end
GO
