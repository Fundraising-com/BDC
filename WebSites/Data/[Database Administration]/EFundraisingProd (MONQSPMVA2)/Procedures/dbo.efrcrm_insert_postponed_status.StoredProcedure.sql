USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_postponed_status]    Script Date: 02/14/2014 13:07:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Postponed_Status
CREATE PROCEDURE [dbo].[efrcrm_insert_postponed_status] @Postponed_Status_ID int OUTPUT, @Description varchar(30) AS
begin

insert into Postponed_Status(Description) values(@Description)

select @Postponed_Status_ID = SCOPE_IDENTITY()

end
GO
