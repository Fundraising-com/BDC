USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_efo_status_email]    Script Date: 02/14/2014 13:06:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for EFO_Status_Email
CREATE PROCEDURE [dbo].[efrcrm_insert_efo_status_email] @Email_Type_ID int OUTPUT, @Status_ID int AS
begin

insert into EFO_Status_Email(Status_ID) values(@Status_ID)

select @Email_Type_ID = SCOPE_IDENTITY()

end
GO
