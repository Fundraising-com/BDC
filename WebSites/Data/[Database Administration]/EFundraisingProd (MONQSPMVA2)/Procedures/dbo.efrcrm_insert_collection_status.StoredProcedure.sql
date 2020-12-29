USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_collection_status]    Script Date: 02/14/2014 13:06:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Collection_Status
CREATE PROCEDURE [dbo].[efrcrm_insert_collection_status] @Collection_Status_ID int OUTPUT, @Description varchar(50) AS
begin

insert into Collection_Status(Description) values(@Description)

select @Collection_Status_ID = SCOPE_IDENTITY()

end
GO
