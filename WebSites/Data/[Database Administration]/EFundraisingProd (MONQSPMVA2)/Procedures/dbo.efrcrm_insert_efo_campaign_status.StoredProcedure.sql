USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_efo_campaign_status]    Script Date: 02/14/2014 13:06:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for EFO_Campaign_Status
CREATE PROCEDURE [dbo].[efrcrm_insert_efo_campaign_status] @Campaign_ID int OUTPUT, @Date_To_Change smalldatetime, @Status_ID int, @Email_Type_ID int AS
begin

insert into EFO_Campaign_Status(Date_To_Change, Status_ID, Email_Type_ID) values(@Date_To_Change, @Status_ID, @Email_Type_ID)

select @Campaign_ID = SCOPE_IDENTITY()

end
GO
