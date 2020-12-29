USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_efo_status]    Script Date: 02/14/2014 13:06:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for EFO_Status
CREATE PROCEDURE [dbo].[efrcrm_insert_efo_status] @Status_ID int OUTPUT, @Status varchar(50) AS
begin

insert into EFO_Status(Status) values(@Status)

select @Status_ID = SCOPE_IDENTITY()

end
GO
