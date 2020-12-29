USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_organization_status]    Script Date: 02/14/2014 13:07:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Organization_Status
CREATE PROCEDURE [dbo].[efrcrm_insert_organization_status] @Organization_Status_ID int OUTPUT, @Description varchar(200) AS
begin

insert into Organization_Status(Description) values(@Description)

select @Organization_Status_ID = SCOPE_IDENTITY()

end
GO
