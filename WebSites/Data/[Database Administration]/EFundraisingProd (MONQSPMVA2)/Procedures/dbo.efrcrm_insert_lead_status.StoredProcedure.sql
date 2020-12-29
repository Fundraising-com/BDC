USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_lead_status]    Script Date: 02/14/2014 13:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Lead_Status
CREATE PROCEDURE [dbo].[efrcrm_insert_lead_status] @Lead_Status_ID int OUTPUT, @Description varchar(50) AS
begin

insert into Lead_Status(Description) values(@Description)

select @Lead_Status_ID = SCOPE_IDENTITY()

end
GO
