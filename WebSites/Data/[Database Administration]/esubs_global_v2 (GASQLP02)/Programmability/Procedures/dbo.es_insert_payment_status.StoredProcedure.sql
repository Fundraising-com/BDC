USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_insert_payment_status]    Script Date: 02/14/2014 13:06:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Payment_status
CREATE PROCEDURE [dbo].[es_insert_payment_status] @Payment_status_id int OUTPUT, @Description varchar(50) AS
begin

insert into Payment_status(Description) values(@Description)

select @Payment_status_id = SCOPE_IDENTITY()

end
GO
