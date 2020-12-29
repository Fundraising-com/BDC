USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_phone_number_tracking]    Script Date: 02/14/2014 13:07:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Phone_number_tracking
CREATE PROCEDURE [dbo].[efrcrm_insert_phone_number_tracking] @Phone_number_tracking_id int OUTPUT, @Phone_number_tracking_desc varchar(50) AS
begin

insert into Phone_number_tracking(Phone_number_tracking_desc) values(@Phone_number_tracking_desc)

select @Phone_number_tracking_id = SCOPE_IDENTITY()

end
GO
