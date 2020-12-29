USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_lead_priority]    Script Date: 02/14/2014 13:07:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Lead_Priority
CREATE PROCEDURE [dbo].[efrcrm_insert_lead_priority] @Lead_Priority_Id int OUTPUT, @Description varchar(50) AS
begin

insert into Lead_Priority(Description) values(@Description)

select @Lead_Priority_Id = SCOPE_IDENTITY()

end
GO
