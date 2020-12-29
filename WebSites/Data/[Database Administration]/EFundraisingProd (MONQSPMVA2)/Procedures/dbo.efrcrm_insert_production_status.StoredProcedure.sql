USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_production_status]    Script Date: 02/14/2014 13:07:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Production_Status
CREATE PROCEDURE [dbo].[efrcrm_insert_production_status] @Production_Status_ID int OUTPUT, @Description varchar(50) AS
begin

insert into Production_Status(Description) values(@Description)

select @Production_Status_ID = SCOPE_IDENTITY()

end
GO
